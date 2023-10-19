---
layout: post
title: "[閱讀筆記][Design Pattern] Ch6.裝飾模式 Decorator"
date: 2009-03-06 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 裝飾模式 Decorator
裝飾模式 Decorator：動態地給一個物件加入一些額外的職責，就增加功能來說，裝飾模式比產生子類別更為靈活。      

## 裝飾模式 Decorator結構
```
Component 定義一個物件介面，可以給這些物件動態地加入職責
+ Operation()

    ConcreteComponent 定義了一個具體的物件，也可以給這個物件加入一些職責
    + Operation()

    Decorator 裝飾抽象類別，繼承了Component，從外類別來擴展Component類別的功能，但對Component來說，是無需知道Decorator的存在
    - component
    + Operation()

        ConcreteDecoratorA 具體的裝飾物件，發揮為Component加入職責的功能
        - addedState:string
        + Operation()

        ConcreteDecoratorB 具體的裝飾物件，負責為Component加入職責的功能
        + Operation()
        + AddedBehavior()
```

- `Component`定義一個物件介面，可以給這些物件動態地加入職責。   
- `ConcreteComponent` 定義了一個具體的物件，也可以給這個物件加入一些職責。  
- `Decorator` 裝飾抽象類別，繼承了`Component`，從外類別來擴展`Component`類別的功能，但對`Component`來說，是無需知道`Decorator`的存在。  
- `ConcreteDecorator` 具體的裝飾物件，負責為`Component`加入職責的功能。

## 基本程式碼實現
### Component 抽象類別
```c#
//Component 定義一個物件介面，可以給這些物件動態地加入職責
abstract class Component {
    public abstract void Operation();
}
```

### ConcreteComponent 類別
```c#
//定義了一個具體的物件，也可以給這個物件加入一些職責
class ConcreteComponent: Component {
    public override void Operation() {
        Console.WriteLine("具體物件的操作");
    }
}
```

### Decorator 裝飾抽象類別
```c#
//繼承了Component，從外類別來擴展Component類別的功能，但對Component來說，是無需知道Decorator的存在
abstract class Decorator: Component {
    protected Component component;
    //設定Component
    public void SetComponent(Component component) {
        this.component = component;
    }

    //重寫父類別的Operation方法，實際執行的是Component的Operation()
    public override void Operation() {
        if(component != null) {
            component.Operation();
        }
    }
}
```

### ConcreteDecorator 具體的裝飾物件
具體的裝飾物件，發揮為Component加入職責的功能

```c#
//具體裝飾A
class ConcreteDecoratorA: Decorator {
    //本類別的特有功能
    private string addedState;
    public override void Operation() {
        //首先執行原Component的Operation()，再執行本類別的功能
        //addedState相當於對原Component進行了裝飾
        base.Operation();
        addedState = "New State";
        Console.WriteLine("具體裝飾A的操作");
    }
}
//具體裝飾B
class ConcreteDecoratorB: Decorator {
    public override void Operation() {
        //首先執行原Component的Operation()，再執行本類別的功能
        // AddedBehavior()相當於對原Component進行了裝飾
        base.Operation();
        AddedBehavior();
        Console.WriteLine("具體裝飾B的操作");
    }
    private void AddedBehavior() {
    }
}
```

### 用戶端程式碼
```c#
ConcreteComponent cc = new ConcreteComponent();
ConcreteDecoratorA d1 = new ConcreteDecoratorA(); //裝飾A
ConcreteDecoratorB d2 = new ConcreteDecoratorB(); //裝飾B

//裝飾的方法：
//首先用ConcreteComponent實體化的物件
//然後用ConcreteDecoratorA實體化的物件 d1 來包裝cc
//再用ConcreteDecoratorB實體化的物件 d2 來包裝d1
//最終執行d2的Operation()
d1.SetComponent(cc); //d1包裝cc
d2.SetComponent(d1); //d2來包裝d1
d2.Operation(); //執行d2的Operation()
```

「裝飾模式」利用`SetComponent`來對物件進行包裝。        

這樣每個裝飾物件的實現就和「如何使用這個物件」分離開了，每個「裝飾物件」只關心自己的功能，不需要關心如何被添加到物件鏈當中。    

> 以穿搭來說，我們可以先穿外褲，再穿內褲，而不一定要先內後外。 

## 穿搭v1.0
功能是實現了，但是如果我現在需要增加超人的裝扮，就得改`Person`類別，這就違背了「開放-封閉原則」了。

```
Person
+ 穿大T恤()
+ 穿垮褲()
+ 穿破球鞋()
+ 打領帶()
+ 穿皮鞋()
+ 形象展示()
```

```c#
Person p = new Person("小白");
//第一種裝扮
p.WearTShirts();
p.WearSneakers();
p.Show();
//第二種裝扮
p.WearLeatherShoes();
p.WearSneakers();
p.WearSuit();
p.Show();

class Person {
    string name;
    public Person(string name) {
        this.name = name;
    }

    public void WearTShirts() {
        Console.WriteLine("大T恤");
    }
    public void WearBigTrouser() {
        Console.WriteLine("垮褲");
    }
    public void WearSneakers() {
        Console.WriteLine("破球鞋");
    }
    public void WearSuit() {
        Console.WriteLine("西裝");
    }
    public void WearTie() {
        Console.WriteLine("領帶");
    }
    public void WearLeatherShoes() {
        Console.WriteLine("皮鞋");
    }
    public void Show() {
        Console.WriteLine($"裝扮的{name}");
    }
}
```
## 穿搭v2.0

將這些服飾都改成子類別，的確做到了「服飾」和「人」類別的分離，雖然不違背「開放-封閉原則」，但服飾的每一個詞都顯示出來了，這樣寫好比穿衣都是在眾目睽睽下穿的。

```c#
Person p = new Person("小白");
Finery f1 = new WearTShirts();
Finery f2 = new WearLeatherShoes();
f1.Show();
f2.Show();

//Person
class Person {
    string name;
    public Person(string name) {
        this.name = name;
    }
    public void Show() {
        Console.WriteLine($"裝扮的{name}");
    }
}
//父類別:服飾抽象類別
abstract class Finery {
    public abstract void Show();
}
//子類別:各個服飾
class WearTShirts: Finery {
    public override void Show(){
        Console.WriteLine("大T恤");
    }
}
//以下省略
```

## 穿搭v3.0(裝飾模式)

把所需的功能按正確的順序串聯起來進行控制。

「人」類別是 `Component`還是`ConcreteComponent`？       
這裡我們沒有必要有`Component`類別了，直接讓服飾類別`Decorator`繼承人類別`ConcreteComponent`就可。

```c#
//用戶端程式碼
public static void Main()
{
    Person p = new Person("Rii");
    Console.WriteLine("第一種裝扮:");

    TShirts tShirts = new TShirts();
    BigTrouser bigTrouser = new BigTrouser();

    //裝飾過程
    tShirts.Decorate(p);
    bigTrouser.Decorate(tShirts);
    bigTrouser.Show();
        
    Console.WriteLine("第二種裝扮:");
    Suit suit = new Suit();
    Tie tie = new Tie();
    LeatherShoes leatherShoes = new LeatherShoes();

    //裝飾過程
    suit.Decorate(p);
    tie.Decorate(suit);
    leatherShoes.Decorate(tie);
    leatherShoes.Show();

    /* 結果顯示
    第一種裝扮:
    垮褲
    大T恤
    裝扮的Rii

    第二種裝扮:
    皮鞋
    領帶
    西裝
    裝扮的Rii
    */
}

//Person類別 (ConcreteComponent)
public class Person {
    public Person(){}
    protected string name;
    public Person(string name) {
        this.name = name;
    }

    public virtual void Show() {
        Console.WriteLine($"裝扮的{name}");
    }
}

//Finery服飾類別 (Decorator)
public class Finery: Person {
    protected Person component; 
    //打扮
    public void Decorate(Person component) {
        this.component = component;
    }

    //重寫Show()，實際上執行的是component的Show()
    public override void Show() {
        if(component != null) {
            component.Show();
        }
    }
}

//具體服飾類別 (ConcreteDecorator)
public class TShirts:Finery {
    public override void Show() {
        Console.WriteLine("大T恤");
        base.Show();
    }
}
public class BigTrouser:Finery {
    public override void Show() {
        Console.WriteLine("垮褲");
        base.Show();
    }
}
public class Sneakers:Finery {
    public override void Show() {
        Console.WriteLine("球鞋");
        base.Show();
    }
}
public class Suit:Finery {
    public override void Show() {
        Console.WriteLine("西裝");
        base.Show();
    }
}
public class Tie:Finery {
    public override void Show() {
        Console.WriteLine("領帶");
        base.Show();
    }
}
public class LeatherShoes:Finery {
    public override void Show() {
        Console.WriteLine("皮鞋");
        base.Show();
    }
}
```

## 何時用「裝飾模式」？
「裝飾模式」是為既有功能動態地增加更多功能的一種方式。      

### 究竟何時用「裝飾模式」呢？
當系統需要新功能時，是在舊的類別中添加新的程式碼。這些新增的程式碼通常裝飾了原有類別的核心職責或主要行為。

就像起初的那個「`Person`人」的類別，新加入的東西只為了滿足一些只在某種特定情況下才會執行的特殊行為的需要。(比如用西裝或嘻哈才來裝飾Rii。)       

### 「裝飾模式」優點
- 把類別中的裝飾功能從類別中搬移去除，這樣可以簡化原有的類別。        
- 更大的好處是：有效地把類別的核心職責和裝飾功能區分開了。而且可以去除相關類別中重複的裝飾邏輯。        

> 要注意的是：「裝飾模式」的裝飾順序很重要。    
> 比如：加密資料和過濾辭彙都可以是資料持久化前的裝飾功能，如果先加密資料再用過濾功能就會出問題了。      
> 最理想的情況是：保證裝飾類別之間彼此獨立，這樣它們就能以任意的順序進行組合了。

