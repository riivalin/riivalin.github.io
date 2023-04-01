---
layout: post
title: "[C# 筆記] 多型(Polymorphism)-介面 Interface 簡介"
date: 2011-01-23 21:29:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 多型(Polymorphism)
概念：讓一個物件能夠表現出多種的狀態(類型)    

## 實現多型的三種手段：  
1. 虛方法 virtual 
2. 抽象類 abstract
3. 介面 interface

## 介面 interface 
介面就是一個規範、能力  
就像是筆記電腦，不同廠家，但我可以插入相同的滑鼠、鍵盤、隨身碟…

```c#
[public] interface I...able
{
    成員;
}
```
以`I`開頭`…able`，比抽象更省事

## 介面的成員可以有：方法、自動屬性、索引器
介面的成員可以有方法、自動屬性、索引器，  
他們三個本質上就是方法，  
所以說介面中就只能用方法。  

## 介面 interface的語法特徵
### 繼承觀念
繼承有一個很重要的特性：單根性   
「單根性」指的是：一個子類只能有一個父類

### 錯誤範例：子類繼承兩個父類(錯誤)
學生繼承Person，但我也想要學生可以扣籃，我就繼承 Person、NBAPlayer
```c#
//父類
public class Person {
    public void SayHello() {
        Console.WriteLine("我是人類");
    }
}
public class NBAPlayer {
    public void KouLan() {
        Console.WriteLine("我可以扣籃");
    }
}

//子類
//繼承有一個很重要的特性：單根性
//「單根性」指的是：一個子類只能有一個父類
//Student不可以有多重基底類別
public class Student : Person, NBAPlayer //=>報錯
{
}
```

### 正確範例：多繼承時，使用interface
把"扣籃"寫成介面interface，讓學生去繼承它。    
這個時候，我們就可以考慮把"扣籃"寫成介面，這樣學生既可以吃喝拉撤睡，也可扣籃。

```c#
//父類
public class Person {
    public void SayHello() {
        Console.WriteLine("我是人類，可以吃喝拉撤睡");
    }
}
public class NBAPlayer {
    public void KouLan() {
        Console.WriteLine("我可以扣籃");
    }
}

//子類
//繼承有一個很重要的特性：單根性
//「單根性」指的是：一個子類只能有一個父類
public class Student : Person, IKouLanale //繼承父類、介面interface
{
    public void KouLan() { //實作介面
        Console.WriteLine("我也可以扣籃");
    }
}

//介面interface
//這個時候，我們就可以考慮把"扣籃"寫成介面
interface IKouLanable {
    void KouLan();
}
```
#### 所以什麼情況會用到介面interface？  
當你需要多繼承的時候，就要考慮介面interface了。  

####　為什麼呀？
因為類別是不允許多繼承的，只能繼承一個父類(一個基底類別)

### 介面中的成員不允許添加修飾符
介面中的成員不允許添加修飾符，因為成員不允許有定義。    
介面默認是public，但不讓你自己加public。  
(介面不添加默認是public，類別不添加默認是private) 

```c#
//以i開頭…able，比抽象更省事
public interface IFlyable
{
    //介面默認是public，但不讓你自己加public。
    public void Fly(); //報錯，不能加public
    string Test(); //正確
}
```

### 介面中能有方法(不能有方法體)
for VS2011：介面中能不能有方法體(大括號)？能寫嗎？不能

```c#
public interface IFlyable
{
    //這樣又區別於抽象類，
    //抽象類當中可不可以有方法體呀(普通的方法)？可以
    //但介面可以嗎？
    //不可以，介面可以有方法，但不能有方法體

    //不允許寫具有方法體的函式 (vs2022可以)
    void Test2() { } //vs2011報錯(vs2022可以)
    void Test2(); //正確
}
```
### 介面不能寫欄位field,普通屬性
Q：為什麼介面不能寫欄位field,普通屬性？為什麼？  
欄位field是幹嘛的呀？是儲存數據的，  
因為介面並不是存數據用的，介面是用來規範的，所以也不需要你寫field(欄位/字段)。

```c#
public interface IFlyable
{
    //不能寫欄位field,普通屬性
    //為什麼？
    //欄位field是幹嘛的呀？是儲存數據的
    //因為介面並不是存數據用的，介面是用來規範的，所以也不需要你寫欄位field
    string name; //報錯
}
```

### 介面可以寫「自動屬性」
Q：介面可以寫屬性嗎？
介面可以寫自動屬性，但不能寫普通屬性。    

Q：為什麼可以寫[「自動屬性」](https://riivalin.github.io/posts/auto-and-normal-properties/)？  
介面裡面是什麼？全是方法，可以讓繼承的類別去重寫，  
而`get`、`set`本質上就是兩個函數，所以有什麼區別？沒有區別都一樣咩。    

```c#
//以i開頭…able，比抽象更省事
public interface IFlyable
{
    //那介面可以寫屬性嗎？
    //介面可以寫自動屬性，但不能寫普通屬性
    public string Name { get; set; }
}
```

## 完整Code+Comment解說
```c#
/*
介面就是一個規範、能力  
就像是筆記電腦，不同廠家，但我可以插入相同的滑鼠、鍵盤、隨身碟…

介面的成員可以有方法、自動屬性、索引器，
他們三個本質上就是方法
所以說介面中就只能用方法。
 */

//以i開頭…able，比抽象更省事
public interface IFlyable
{
    //VS2022沒報錯: 加方法體、修飾符沒報錯
    public void Fly(){ } 
    public string Age { get; set; }

    //介面中的成員不允許添加修飾符(因為成員不充許有定義)
    //介面默認是public，但不讓你自己加public 
    //(介面不添加是public,類別不添加是private)
    //不能有方法體，能寫嗎？不能

    //public void Fly(); //報錯
    void Fly(); 
    string Test();

    //這樣又區別於抽象類，
    //抽象類當中可不可以有方法體呀(普通的方法)？可以
    //但介面可以嗎？
    //不可以，介面可以有方法，但不能有方法體

    //不允許寫具有方法體的函式 vs2022可以
    void Test2() { } //vs2011報錯

    //不能寫欄位field,屬性
    //為什麼？
    //欄位field是幹嘛的呀？是儲存數據的
    //因為介面並不是存數據用的，介面是用來規範的，所以也不需要你寫欄位field
    //string name; //報錯

    //那介面可以寫屬性嗎？
    //介面可以寫自動屬性，但不能寫普通屬性
    //為什麼可以寫「自動屬性？
    //介面裡面是什麼？全是方法，可以讓繼承的類別去重寫，
    //而get,set本質上就是兩個函數，所以有什麼區別？沒有區別都一樣咩
    string Name { get; set; }
}

//父類
public class Person
{
    //普通屬性
    private string _name; //field(欄位/字段)
    /// <summary>
    /// 普通屬性
    /// </summary>
    public string Name //屬性
    {
        get { return _name; }
        set { _name = value;}
    }

    /// <summary>
    /// 自動屬性
    /// 為什麼稱它為自動屬性呢？
    /// 雖然我不寫field欄位/字段，但編譯後依然會自動給我們生成field(欄位/字段)
    /// </summary>
    public int Age { get; set; }

    public void SayHello() {
        Console.WriteLine("我是人類，可以吃喝拉撤睡");
    }
}
public class NBAPlayer {
    public void KouLan() {
        Console.WriteLine("我可以扣籃");
    }
}

//子類
//繼承有一個很重要的特性：單根性
//「單根性」指的是：一個子類只能有一個父類
public class Student : Person, IKouLanable {
    public void KouLan() {
        Console.WriteLine("我也可以扣籃");
    }
}

//介面interface
//這個時候，我們就可以考慮把"扣籃"寫成介面
interface IKouLanable {
    void KouLan();
}
```



VS2022沒報錯: 加方法體、修飾符沒報錯
```c#
public interface IFlyable
{
    public void Fly8() { }
    public string Age { get; set; }
}
```

[R Note - 自動屬性 & 普通屬性](https://riivalin.github.io/posts/auto-and-normal-properties/)
