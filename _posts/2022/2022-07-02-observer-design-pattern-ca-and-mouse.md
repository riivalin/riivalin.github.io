---
layout: post
title: "[C# 筆記] 觀察者設計模式-貓捉老鼠"
date: 2022-07-02 00:01:10 +0800
categories: [Notes, C#]
tags: [C#, Delegate, Event]
---

## 第一步:使用委託
被觀察者:貓(只有一個)
觀察者:老鼠(有很多個)

> 如果應用在程式開發中，例如：在遊戲中，會有個「開始」按鈕，當按下按鈕後，會有很多的場景、音樂、去進行一個資源的加載、其他加載，「開始按鈕」就是「被觀察者」，開始按鈕的狀態變化就是「是否被點擊」，當這個狀態被改變了，它被點擊了，就要通知「觀察者」(資源管理器、音樂理理器、場景管理器…)去做一些事情，像是場景切換，音樂開始... 

第一步:利用委託
先建立Cat、Mouse類別
```c#
/// <summary>
/// 被觀察者類別：貓
/// </summary>
public class Cat
{
    private string name;
    private string color;
    public Cat(string name, string color)
    {
        this.name = name;
        this.color = color;
    }
    //貓進屋(貓的狀態改變了)(被觀察者的狀態改變)
    public void CatComing() {
        Console.WriteLine($"{color}的貓{name}過來了，喵喵喵...");
    }
}
```
```c#
/// <summary>
/// 觀察者類別：老鼠
/// </summary>
public class Mouse
{
    private string name;
    private string color;
    public Mouse(string name, string color) {
        this.name = name;
        this.color = color;
    }
    /// <summary>
    /// 老鼠跑
    /// </summary>
    public void RunAway() {
        Console.WriteLine($"{color}的老鼠{name}說: 老貓來了，趕緊跑，我使勁跑…");
    }
}
```
TODO: 當貓的狀態改變了，老鼠就要跑
```c#
Cat cat = new Cat("加非貓", "黃色");
Mouse mouse1 = new Mouse("米奇", "黑色");
Mouse mouse2 = new Mouse("小花", "灰色");
Mouse mouse3 = new Mouse("XX", "灰色");
//TODO: 當貓的狀態改變了，老鼠就要跑
cat.CatComing(); //貓的狀態改變了
Console.ReadKey();
```
思路：使用委託(多播委託，引用多個方法)
TODO: 當貓的狀態改變了，老鼠就要跑
Step1.在貓的類別中，聲明公開的貓跑委託，在貓跑的方法中調用委託。
Step2.在Program場景中，貓跑委託累加每個老鼠跑的方法

Step1.在貓的類別中，聲明公開的貓跑委託，在貓跑的方法中調用委託。
```c#
//貓進屋(貓的狀態改變了)(被觀察者的狀態改變)
public void CatComing() {
    Console.WriteLine($"{color}的貓{name}過來了，喵喵喵...");
    if (catCome != null) catCome();//調用委託
}
public Action catCome; //聲明委託
```
Step2.在Program場景中，貓跑委託累加每個老鼠跑的方法
```c#
cat.catCome += mouse1.RunAway;
cat.catCome += mouse2.RunAway;
cat.catCome += mouse3.RunAway; //catCome這個委託就引用到三個方法
```

完整Code
```c#
/// <summary>
/// 被觀察者類別：貓
/// </summary>
public class Cat
{
    private string name;
    private string color;
    public Cat(string name, string color)
    {
        this.name = name;
        this.color = color;
    }
    //貓進屋(貓的狀態改變了)(被觀察者的狀態改變)
    public void CatComing() {
        Console.WriteLine($"{color}的貓{name}過來了，喵喵喵...");
        if (catCome != null) catCome();//調用委託
    }
    public Action catCome; //聲明委託
}
```
```c#
/// <summary>
/// 觀察者類別：老鼠
/// </summary>
public class Mouse
{
    private string name;
    private string color;
    public Mouse(string name, string color) {
        this.name = name;
        this.color = color;
    }
    /// <summary>
    /// 老鼠跑
    /// </summary>
    public void RunAway() {
        Console.WriteLine($"{color}的老鼠{name}說: 老貓來了，趕緊跑，我使勁跑…");
    }
}
```
貓捉老鼠場景(Program)
```c#
Cat cat = new Cat("加非貓", "黃色");
Mouse mouse1 = new Mouse("米奇", "黑色");
cat.catCome += mouse1.RunAway;
Mouse mouse2 = new Mouse("小花", "灰色");
cat.catCome += mouse2.RunAway;
Mouse mouse3 = new Mouse("XX", "灰色");
cat.catCome += mouse3.RunAway; //catCome這個委託就引用到三個方法
cat.CatComing(); //貓的狀態改變了
Console.ReadKey();
```
## 第二步: 優化

**進一步優化：**

每次創建老鼠的時候，都要把老鼠逃跑的方法加到貓的委託裡，很麻煩(如下)
```c#
Mouse mouse1 = new Mouse("米奇", "黑色");
//cat.catCome += mouse1.RunAway; //把老鼠自身逃跑的方法，註冊到貓裡面，就不用這段了
```
我們可以在每次創建老鼠的時候，把貓類別傳進去，就可以在老鼠的建構函式裡去註冊貓的事件

在mouse構造的時候，把貓傳過來，就可以去註冊貓裡面的事件
```c#
public Mouse(string name, string color, Cat cat) {
    this.name = name;
    this.color = color;
    cat.catCome += this.RunAway; //把自身逃跑的方法，註冊到貓裡面
}
```
把老鼠自身逃跑的方法，註冊到貓裡面，就不用這段了```cat.catCome += mouse1.RunAway;```
```c#
Cat cat = new Cat("加非貓", "黃色");
Mouse mouse1 = new Mouse("米奇", "黑色", cat);
Mouse mouse2 = new Mouse("小花", "灰色", cat);
Mouse mouse3 = new Mouse("XX", "灰色", cat);
cat.CatComing(); //貓的狀態改變了
```
完整Code:
貓捉老鼠場景:
```c#
Cat cat = new Cat("加非貓", "黃色");
Mouse mouse1 = new Mouse("米奇", "黑色", cat);
//cat.catCome += mouse1.RunAway; ////把老鼠自身逃跑的方法，註冊到貓裡面，就不用這段了
Mouse mouse2 = new Mouse("小花", "灰色", cat);
//cat.catCome += mouse2.RunAway;
Mouse mouse3 = new Mouse("XX", "灰色", cat);
//cat.catCome += mouse3.RunAway; //catCome這個委託就引用到三個方法
cat.CatComing(); //貓的狀態改變了
Console.ReadKey();
```
貓類別:
```c#
/// <summary>
/// 被觀察者類別：貓
/// </summary>
public class Cat
{
    private string name;
    private string color;
    public Cat(string name, string color)
    {
        this.name = name;
        this.color = color;
    }
    //貓進屋(貓的狀態改變了)(被觀察者的狀態改變)
    public void CatComing() {
        Console.WriteLine($"{color}的貓{name}過來了，喵喵喵...");
        if (catCome != null) catCome();//調用委託
    }
    public Action catCome; //聲明委託
}
```
老鼠類別:
```c#
/// <summary>
/// 觀察者類別：老鼠
/// </summary>
public class Mouse
{
    private string name;
    private string color;
    public Mouse(string name, string color, Cat cat) {
        this.name = name;
        this.color = color;
        cat.catCome += this.RunAway; //把自身逃跑的方法，註冊到貓裡面
    }
    /// <summary>
    /// 老鼠逃跑功能
    /// </summary>
    public void RunAway() {
        Console.WriteLine($"{color}的老鼠{name}說: 老貓來了，趕緊跑，我使勁跑…");
    }
}
```
## 第三步: 使用事件Event (把委託改成事件)

聲明一個事件(把委託改成事件，聲明委託前面加上event)
```C#
public event Action catCome; //聲明一個事件
```
改完之後完全沒有影響

**那麼委託與事件的區別在哪裡呢？**
- 委託可以在外部調用，會比較危險```cat.catCome();```，別這麼用。
- 事件只能在類別的內部觸發，不能在類別的外部觸發 ```catCome();```。

> 使用中，委託常用來表達回調，事件表達觸發的接口。

### 完整Code
```c#
//觀察者設計模式_貓捉老鼠
Cat cat = new Cat("加非貓", "黃色");
Mouse mouse1 = new Mouse("米奇", "黑色", cat);
//cat.catCome += mouse1.RunAway; ////把老鼠自身逃跑的方法，註冊到貓裡面，就不用這段了
Mouse mouse2 = new Mouse("小花", "灰色", cat);
//cat.catCome += mouse2.RunAway;
Mouse mouse3 = new Mouse("XX", "灰色", cat);
//cat.catCome += mouse3.RunAway; //catCome這個委託就引用到三個方法
cat.CatComing(); //貓的狀態改變了
//cat.catCome(); //事件只能在類別的內部觸發，不能在類別的外部觸發
Console.ReadKey();

/// <summary>
/// 被觀察者類別：貓
/// </summary>
public class Cat
{
    private string name;
    private string color;
    public Cat(string name, string color)
    {
        this.name = name;
        this.color = color;
    }
    //貓進屋(貓的狀態改變了)(被觀察者的狀態改變)
    public void CatComing() {
        Console.WriteLine($"{color}的貓{name}過來了，喵喵喵...");
        if (catCome != null) catCome();//調用事件。事件只能在類別的內部觸發，不能在類別的外部觸發
    }
    public event Action catCome; //聲明一個事件
}

/// <summary>
/// 觀察者類別：老鼠
/// </summary>
public class Mouse
{
    private string name;
    private string color;
    public Mouse(string name, string color, Cat cat) {
        this.name = name;
        this.color = color;
        cat.catCome += this.RunAway; //把自身逃跑的方法，註冊到貓裡面
    }
    /// <summary>
    /// 老鼠逃跑功能
    /// </summary>
    public void RunAway() {
        Console.WriteLine($"{color}的老鼠{name}說: 老貓來了，趕緊跑，我使勁跑…");
    }
}
```
## 第四步：事件Event「發佈/訂閱機制」

事件(Event)基於委託，為委託提供一個「發佈/訂閱機制」

發佈消息：聲明事件，相當於發佈了一個消息
```c#
public event Action catCome; //聲明一個事件，發佈一個消息
```

訂閱消息：在老鼠mouse的建造函式中，```cat.catCome += this.RunAway; ```加入方法的時候，相當於訂閱消息
```c#
public Mouse(string name, string color, Cat cat) {
    this.name = name;
    this.color = color;
    cat.catCome += this.RunAway; //把自身逃跑的方法，註冊到貓裡面(訂閱消息)
}
```
觸發消息：貓來的方法裡，catCome()相當於觸發消息
```c#
//貓進屋(貓的狀態改變了)(被觀察者的狀態改變)
public void CatComing() {
    Console.WriteLine($"{color}的貓{name}過來了，喵喵喵...");
    if (catCome != null) catCome();//觸發事件。事件只能在類別的內部觸發，不能在類別的外部觸發(觸發消息)
}
```
### 完整Code: 觀察者設計模式_貓捉老鼠
```c#
Cat cat = new Cat("加非貓", "黃色");
Mouse mouse1 = new Mouse("米奇", "黑色", cat);
//cat.catCome += mouse1.RunAway; ////把老鼠自身逃跑的方法，註冊到貓裡面，就不用這段了
Mouse mouse2 = new Mouse("小花", "灰色", cat);
//cat.catCome += mouse2.RunAway;
Mouse mouse3 = new Mouse("XX", "灰色", cat);
//cat.catCome += mouse3.RunAway; //catCome這個委託就引用到三個方法
cat.CatComing(); //貓的狀態改變了
//cat.catCome(); //事件只能在類別的內部觸發，不能在類別的外部觸發
Console.ReadKey();

/// <summary>
/// 被觀察者類別：貓
/// </summary>
public class Cat
{
    private string name;
    private string color;
    public Cat(string name, string color)
    {
        this.name = name;
        this.color = color;
    }
    //貓進屋(貓的狀態改變了)(被觀察者的狀態改變)
    public void CatComing() {
        Console.WriteLine($"{color}的貓{name}過來了，喵喵喵...");
        if (catCome != null) catCome();//觸發事件。事件只能在類別的內部觸發，不能在類別的外部觸發(觸發消息)
    }
    public event Action catCome; //聲明一個事件，發佈一個消息
}
    /// <summary>
/// 觀察者類別：老鼠
/// </summary>
public class Mouse
{
    private string name;
    private string color;
    public Mouse(string name, string color, Cat cat) {
        this.name = name;
        this.color = color;
        cat.catCome += this.RunAway; //把自身逃跑的方法，註冊到貓裡面(訂閱消息)
    }
    /// <summary>
    /// 老鼠逃跑功能
    /// </summary>
    public void RunAway() {
        Console.WriteLine($"{color}的老鼠{name}說: 老貓來了，趕緊跑，我使勁跑…");
    }
}
```