---
layout: post
title: "[C# 筆記][多型] Interface 明確介面實作"
date: 2011-01-23 21:59:00 +0800
categories: [Notes, C#]
tags: [C#,interface,多型]
---


## 明確介面實作
明確介面實作的目的：解決方法重名問題    

語法
```c#
public class Bird : IFlyable {
    public void Fly() {
        Console.WriteLine("鳥會飛");
    }
    /// <summary>
    /// 明確介面實作
    /// </summary>
    void IFlyable.Fly() {
        Console.WriteLine("我是介面的飛");
    }
}
```

什麼時候顯示的去實現介面？    
當繼承的介面中的方法和參數一模一樣的時候，就是用明確介面實作。    

當一個抽象類實現介面的時候，需要子類去實現介面。

## 範例：類別與介面方法 重名問題

直接顯示介面名就可以了 `void IFlyable.Fly()`

```c#
//明確介面實作：就是為了解決方法的重名問題

IFlyable fly = new Bird();
fly.Fly(); //調的是介面的fly，為什麼？因為fly是IFlyable介面類型

//如要調鳥的fly，要這宣告為Bird的類型↓↓↓
//不要跟override搞混了
Bird bird = new Bird();
bird.Fly(); //這調的才是鳥的fly

Console.ReadKey();

//類別
public class Bird : IFlyable
{
    public void Fly() {
        Console.WriteLine("鳥會飛");
    }
    /// <summary>
    /// 明確介面實作
    /// 這個強調的是介面的飛
    /// 且不能加修飾符(ex:public)
    /// 方法因為是在類別裡面，所以默認是 private，
    /// (方法在介面裡面，默認是 public)
    /// </summary>
    void IFlyable.Fly() {
        Console.WriteLine("我是介面的飛");
    }
}

//介面
public interface IFlyable {
    void Fly();
}
```  



[interfaces/explicit-interface-implementation](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation)