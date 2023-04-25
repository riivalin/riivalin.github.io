---
layout: post
title: "[C# 筆記] 工具人下樓問題(Delegate+Event)"
date: 2012-01-06 00:03:10 +0800
categories: [Notes, C#]
tags: [C#, Delegate, Event]
---

假設一群人在宿舍，有人要下樓去買午餐，其他人也跟著請他買，並寫在清單上。
(可以發佈一個事件，大家都可以去訂閱。)

- 訂閱：訂閱這個事件，當工具人下樓的時候，就可以順便幫他們做一些事(買飯/東西、拿快遞、其他
- 發佈：發佈下樓的事件
- 事件：下樓事件

## Step1: 先定義兩個類別：工具人、懶人
```c#
internal class ToolMan //工具人
{
    public string Name { get; private set; }

    public ToolMan(string name) {
        this.Name = name;
    }
    public void DownStair() {
        Console.WriteLine($"工具人{Name}下樓了");
    }
}
internal class LazyMan //懶人
{
    public string Name { get; private set; }
    public LazyMan(string name) {
        this.Name = name;
    }

    public void TakeFood() {
        Console.WriteLine($"給{Name}拿外賣");
    }
    public void TakePackage() {
        Console.WriteLine($"給{Name}拿快遞");
    }
}
```
但這時候工具人下樓時，還不會幫室友們做事情
```c#
ToolMan toolMan = new ToolMan("小明");

LazyMan lazyMan1 = new LazyMan("張三");
LazyMan lazyMan2 = new LazyMan("李四");
LazyMan lazyMan3 = new LazyMan("王五");

toolMan.DownStair();
```
## Step2: 使用委派delegate
那麼，我們怎麼讓工具人下樓時幫室友們做事情呢？
這時候我們可以用委派delegate
(委派就是：有一件事情，我不親自去做，而是交給別人來做)

```c#
delegate void DownStairDelegate();//定義委派, 下樓時觸發
internal class ToolMan
{
    public string Name { get; private set; }
    public DownStairDelegate DownStairDelegate = null; //聲明委派，先設為空

    public ToolMan(string name)
    {
        this.Name = name;
    }
    public void DownStair()
    {
        Console.WriteLine($"工具人{Name}下樓了");
        if (DownStairDelegate != null) //先判斷是否為空，委派為空會出現異常
        {
            DownStairDelegate();//下樓的時候，調用委派方法
        }
    }
}
```
這時候每個懶人把要做的事情，都寫在清單上(委派加入多個方法，也叫做多播委派)，
再調用工具人下樓的方法，當工具人下樓的時候，就可以順便幫他們做一些事
```c#
internal class Program
{
    static void Main(string[] args)
    {
        ToolMan toolMan = new ToolMan("小明");

        LazyMan lazyMan1 = new LazyMan("張三");
        LazyMan lazyMan2 = new LazyMan("李四");
        LazyMan lazyMan3 = new LazyMan("王五");

        //每個懶人把要做的事情，都寫在清單上(委派加入多個方法，也叫做多播委派)
        toolMan.DownStairDelegate += lazyMan1.TakeFood;
        toolMan.DownStairDelegate += lazyMan2.TakePackage;
        toolMan.DownStairDelegate += lazyMan2.TakeFood;

        toolMan.DownStair(); //工具人下樓
    }
}
```
## Step3: 使用事件Event(將委派delegate加上event改成事件)
但會有些問題：
1. 如果把方法加入委派時，不小心把+=寫成=，就變成賦值，其他的方法就不會觸發了。
2. 如果不小心把直接調用工具人的委派方法，就變成工具人沒下樓，所有委派的方法都執行了。

```c#
toolMan.DownStairDelegate();//直接調用工具人的委派方法，就變成工具人沒下樓，所有委派的方法都執行了。
```
這變成，本來只有工具人才能觸發、發佈的消息，結果變成這種情況，即使工具人沒下樓，其他人也能調用執行。

正常情況，只能在工具人內部觸發，不讓它在外部觸發，不管是有意無意的，這樣的失誤去觸發，是不行的。

訂閱/發佈機制，這個消息只能由發佈者自身去發佈，其他人是無權去發佈的，其他人只能去訂閱我的消息，如果其他人可以發佈，那就是假消息了，就是這個事件並沒有真正發生，而你卻告訴大家工具人下樓了，結果大家都在等外賣、等快遞。

那麼，我們要怎麼解決這兩個問題呢？
那就是通過事件event。 在聲明委派delegate的時候，在前面加上event關鍵字。

事件Event本質上還是一個委派，那它跟委派delegate有什麼區別呢？
區別在於你加了event之前，你再去訪問它時，它是受限制的，這個事件event相當於受限制的委派，

這個限制就是：
1. 你在外界這個委派只能增加方法+=、減少方法=+，不能賦值=。
```c#
toolMan.DownStairDelegate = lazyMan2.TakeFood;
toolMan.DownStairDelegate();
```
2. 這個事件你只能在內部觸發，這個事件它在外部你是無法直接調用的。

這兩個限制，就是進行權限的限制，它是一個特殊的委派，是一個受限制的委派，它是一個進行權限的作用。

完整Code
```c#
internal class Program
{
    static void Main(string[] args)
    {
        ToolMan toolMan = new ToolMan("小明");

        LazyMan lazyMan1 = new LazyMan("張三");
        LazyMan lazyMan2 = new LazyMan("李四");
        LazyMan lazyMan3 = new LazyMan("王五");

        //每個懶人把要做的事情，都寫在清單上(委派加入多個方法，也叫做多播委派)
        toolMan.DownStairDelegate += lazyMan1.TakeFood;
        toolMan.DownStairDelegate += lazyMan2.TakePackage;
        toolMan.DownStairDelegate += lazyMan3.TakePackage;
        //toolMan.DownStairDelegate = lazyMan2.TakeFood; //使用event，只能+=或-=，不能賦值=

        toolMan.DownStair(); //工具人下樓
        //toolMan.DownStairDelegate(); //委派可以在外部調用方法，這樣會有問題的，所以要改成event, 讓它只能在工具人內部調用
    }
}
```
```c#
delegate void DownStairDelegate();//定義委派, 下樓時觸發
internal class ToolMan
{
    public string Name { get; private set; }

    //event事件-受限制的委派
    public event DownStairDelegate DownStairDelegate = null; //聲明委派，先設為空

    public ToolMan(string name)
    {
        this.Name = name;
    }
    public void DownStair()
    {
        Console.WriteLine($"工具人{Name}下樓了");
        if (DownStairDelegate != null) //先判斷是否為空，委派為空會出現異常
        {
            DownStairDelegate();//下樓的時候，調用委派方法
        }
    }
}
```
```c#
internal class LazyMan
{
    public string Name { get; private set; }
    public LazyMan(string name) {
        this.Name = name;
    }

    public void TakeFood() {
        Console.WriteLine($"給{Name}拿外賣");
    }
    public void TakePackage() {
        Console.WriteLine($"給{Name}拿快遞");
    }
}
```
## Notes
- 委派可以在外部觸發，但別這麼用。   
- 委派通常用來表達回調，事件表達外發接口(訂閱/發佈機制)  
- 委託：把小方法當作一個參數傳給大方法裡面，小方法還需要使用大方法裡面的變量。實現這種功能的辦法就叫委託。  
- 把函數（方法）當作一個變量、參數   
- 委派就是：這有一件事情，我不親自去做，而是交給別人來做。   
