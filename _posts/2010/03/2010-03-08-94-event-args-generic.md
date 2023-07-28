---
layout: post
title: "[C# 筆記] 事件參數泛型與練習"
date: 2010-03-08 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,事件,event,event handler,泛型,generic,T]
---

# 前情提要 - 事件(Event關鍵字)

[事件(Event關鍵字)](https://riivalin.github.io/posts/2010/03/93-event/#程式碼練習-event-eventhandler)

## 事件(Event關鍵字)
`event`修飾了一個委託，其實是給這個委託升級了，給他加了兩個規則：

Event(事件)
1. event修飾的委託，只能在類內調用執行，類外不可調用的。(只能被類內調用執行)
2. event修飾的委託，不能直接賦值，只能通過+、-增減其中蘊含的方法。(只能通過`+=`、`-+`方式去加減所蘊含的方法)

![](/assets/img/post/event.png)

## 事件中的角色
![](/assets/img/post/event-role.png)

## TODO: EventHandler參數包...

[事件(Event關鍵字)](https://riivalin.github.io/posts/2010/03/93-event/#程式碼練習-event-eventhandler)       

上節提到，使用C#內置的 `EventHandler`這樣委託來聲明委託對象，然後有一個問題，就是說這樣一個委託對象，它需要傳入兩個參數，一個是`Object`類型，一個是`EvnetArgs`。      

`Object`類型就代表著：到底是誰在發出這樣的事件，那後面的`EvnetArgs`就是發出這樣的事件需要攜帶哪些參數，但是`EvnetArgs`裡面沒有任何參數，它是一個空的。      

所以呢，現在就得把這個問題解決一下了：那些攻擊力、是否眩暈、是否中毒，這些參數咱還得給它，逐步的給它加回來才行，不能說是一個空白的參數包就扔過去了吧。

### 內置委託類型 event EventHandler

```c#
//內置委託類型 event EventHandler
public event EventHandler OnAttack;
```

### 移至定義查看EventHandler: 需傳入兩個參數object,EventArgs

```c#
//移至定義查看EventHandler: 需傳入兩個參數object,EventArgs
 public delegate void EventHandler(object sender, EventArgs e);
```

### 移至定義查看EventArgs: 什麼都沒有，它是空的
問題就出在這裡

```c#
//移至定義查看EventArgs: 什麼都沒有，它是空的，問題就出在這裡
public class EventArgs
{
    [__DynamicallyInvokable]
    public static readonly EventArgs Empty = new EventArgs();
    [__DynamicallyInvokable]
    public EventArgs() {
    }
}
```

## 前情提要Code 
TODO: `EventArgs`參數包...      

如何自定義`EventArgs`參數包，把原本的`EventArgs`替換掉呢？這就需到咱們的參數泛型了。

```c#
//玩家
class Player {
    public event EventHandler OnAttack;

    //AOE技能
    public void DoAOE() {
        if (OnAttack != null) { 
            OnAttack(this, EventArgs.Empty);
        }
    }
}
//敵人
class Enemy {
    //攻擊我。TODO: EventArgs參數包
    public void AttackMe(object sender, EventArgs args) {
        Console.WriteLine("好疼啊，敵人被攻擊了!");
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        Player player = new Player();
        Enemy enemy = new Enemy();

        //OnAttack加進去實踐一個方法
        player.OnAttack += enemy.AttackMe;

        //調用AOE技能，會觸發OnAttack事件，OnAttack會調用enemy.AttackMe方法
        player.DoAOE();
    }
}
```


# 事件參數EventArgs (參數泛型)
- 事件被觸發的時候，可以傳送自定義的事件參數

![](/assets/img/post/event-args.png)

## 步驟
### 1. 定義事件類
定義自己的事件參數包`class`

```c#
class MyEventArgs {
    public string text = "";
    public int number = 0;
    public bool flag = false;
}
```
- 這裡是自定義的參數表：攻擊力、是否眩暈、是否中毒...

### 2. 定義事件源(使用泛型)
將`EventHandler`特化成傳輸咱們自己的參數包類別的委託類型

```c#
class Player {
    public event EventHandler<MyEventArgs> AttackEvent;
    //TODO 一堆屬性
}
```
- 泛型可以使用在`class`類別、`method`方法上，也可以使用在「委託」

### 3. 定義事件處理方法
響應方法中，將事件參數包類型替換成我們自己的參數包類型

```c#
public void Test<object? sender, MyEventArgs e> {...}
```


# 程式碼：自定義事件參數EventArgs

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 事件參數泛型
 *  1. 定義自己的事件參數包class
 *  2. 將EventHandler特化成傳輸咱們自己的參數包類別的委託類型
 *  3. 響應方法中，將事件參數包類型替換成我們自己的參數包類型
 */
namespace EventArgsTest
{
    //1. 定義自己的事件參數包class
    class MyArgs
    {
        public int Attack = 0; //攻擊力(減血)
        public bool IsPoison = false; //是否中毒
        public bool IsHeadache = false; //是眩暈
    }

    //玩家
    class Player
    {
        //2. 將EventHandler特化成傳輸咱們自己的參數包類別的委託類型
        // 加上<MyArgs>後，它就變成這樣的委託：
        // public delegate void EventHandler(object o, MyArgs e)
        public event EventHandler<MyArgs> OnAttack;

        //AOE技能
        public void DoAOE()
        {
            if (OnAttack != null)
            {
                //2.1 改成自己的參數包傳進去
                MyArgs args = new MyArgs();
                args.Attack = 10;
                args.IsHeadache = true;
                args.IsPoison = true;

                OnAttack(this, args);
            }
        }
    }
    //敵人
    class Enemy
    {
        //攻擊我
        //3. 響應方法中，將事件參數包類型替換成我們自己的參數包類型MyArgs
        public void AttackMe(object sender, MyArgs args)
        {
            Console.WriteLine("好疼啊，敵人被攻擊了!");
            Console.WriteLine($"攻擊力: {args.Attack}");
            Console.WriteLine($"是否中毒: {args.IsPoison}");
            Console.WriteLine($"是否眩暈: {args.IsHeadache}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(); //做一個玩家物件
            Enemy enemy = new Enemy(); //做一個敵人物件

            //player裡面有一個OnAttack類型的事件，
            //這個事件將敵人的AttackMe這樣的一個方法 加入到這個事件包裡面
            player.OnAttack += enemy.AttackMe;

            //隨後調用AOE技能，AOE會觸發OnAttack的實踐，
            //在觸發的時候，會構建一個我們自己的事件參數包MyArgs傳入OnAttack，
            //然後敵人的AttackMe()就會收到這樣的一個事件參數包MyArgs，
            //然後把事件參數包MyArgs每一個參數打印出來
            player.DoAOE();
        }
    }
}
```

# 事件練習一

請編寫鍵盤輸入管理類：`InputManager`，並對外暴露`OnKeyInput`事件，表示用戶輸入了一個字符
- 要求：使用自定義事件，將鍵盤的字符給到訂閱者

```c#
/*
 * Input Manager 練習
 *      編寫一個Input Manager的class，用來接收用戶輸入的一個字符，之後調用OnInput事件，該事件會傳遞給監聽者兩個東西(Object sender, InputArgs args)，args裡面攜帶著用戶輸入的字符，
 */
namespace EventTestInputManager
{
    //事件參數包
    class InputArgs
    {
        public char input;
    }
    class InputManager
    {
        //public delegate void EventHandle(object sender, InputArgs e);
        public event EventHandler<InputArgs> OnInput;

        public void WaitForInput() 
        {
            while (true) //做一個死循環
            {
                //1. 讀取用戶輸入的一個字符
                char input = Convert.ToChar(Console.Read());
                //Console.WriteLine(input);

                //2. 調用OnInput事件，將這個字符傳導到監聽方法/監聽者
                if (OnInput != null) {
                    InputArgs args = new InputArgs();
                    args.input = input;

                    OnInput(this, args); //事件的調用
                }

            }
        }
    }
    internal class Program
    {
        //監聽事件
        public static void OnKeyInput(object sender, InputArgs args) {
            Console.WriteLine($"收到了OnInput事件，拿到了字符：{args.input}");
        }

        static void Main(string[] args)
        {
            InputManager im = new InputManager();
            im.OnInput += OnKeyInput; //方法加入事件中

            im.WaitForInput();
        }
    }
}
```

# 事件練習二

請編寫人類餵食寵物的過程：
- 事件源：人類，人可以發出餵食的事件
- 事件參數：餵什麼食物
- 事件訂閱者：狗/貓/熊貓
- 事件處理方法：動物們的`OnFeed`方法判斷當前食物是否愛吃

```c#
/*
 * 餵食寵物
 *      模擬主人餵食寵物的過程，三個寵物：狗/貓/熊貓
 *      事件源：主人(OnFeed)
 *      監聽者：狗 貓 熊貓，Eat()
 *      事件參數包：食物 string food
 */
namespace EventTestFeedAnimals
{
    //事件參數包
    class FeedArgs {
        public string food = "";
    }
    //主人
    class Master 
    {
        public event EventHandler<FeedArgs> OnFeed; //餵食事件

        public void FeedAnimals(string food) 
        {
            if (OnFeed != null) 
            {
                //發出事件所攜帶的參數包
                FeedArgs args = new FeedArgs();
                args.food = food;

                //調用事件，並將發起人和它相關的參數包都丟進去
                //OnFeed已經有三個方法，狗 貓 熊貓他們三個各自的Eat方法
                //當OnFeed被觸發的時候，就會調用這三者的Eat方法
                //然後將FeedArgs傳給這三個方法裡面
                //由這三個方法裡面各自判斷他們愛不愛吃
                OnFeed(this, args);
            }
        }
    }

    class Dog {
        public void Eat(object sender, FeedArgs args) {
            if (args.food != "肉肉") {
                Console.WriteLine("狗狗：不愛吃!不愛吃!");
            } else {
                Console.WriteLine("狗狗：愛吃愛吃!");
            }
        }
    }

    class Cat {
        public void Eat(object sender, FeedArgs args) {
            if (args.food != "魚魚") 
                Console.WriteLine("貓貓：不愛吃!不愛吃!");
            } else {
                Console.WriteLine("貓貓：愛吃愛吃!");
            }
        }
    }

    class Panda {
        public void Eat(object sender, FeedArgs args) {
            if (args.food != "竹子") {
                Console.WriteLine("熊貓：不愛吃!不愛吃!");
            } else {
                Console.WriteLine("熊貓：愛吃愛吃!");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //事件發起者
            
            Master master = new Master();
            //事件的監聽者
            Dog dog = new Dog();
            Cat cat = new Cat();
            Panda panda = new Panda();

            //將狗/貓/熊貓的Eat方法加到事件中
            master.OnFeed += dog.Eat;
            master.OnFeed += cat.Eat;
            master.OnFeed += panda.Eat;

            //主人餵食
            master.FeedAnimals("竹子");
        }
    }
}
```





[https://www.bilibili.com/video/BV13X4y1479i/](https://www.bilibili.com/video/BV13X4y1479i/)