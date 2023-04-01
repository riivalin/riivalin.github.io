---
layout: post
title: "[C# 筆記] interface 介面練習 1"
date: 2011-01-23 23:09:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習：多型-介面
麻雀會飛 鸚鵡會飛 鴕鳥不會飛 企鵝不會飛 直升飛機會飛  
用多型來實現  
~~虛方法、抽象類~~、介面 

R:(不確定是不是這樣理解)  
- 有共同行為、共同能力：「飛」，可以用介面interface
- 麻雀、鸚鵡、鴕鳥、企鵝是鳥類，可以抽出Bird父類

```c#
//麻雀會飛 鸚鵡會飛 鴕鳥不會飛 企鵝不會飛 直升飛機會飛
IFlyable fly = new 直升飛機(); //new 麻雀(); //new 鸚鵡();
fly.Fly();
Console.ReadKey();

//麻雀,鸚鵡,鴕鳥,企鵝是鳥類，可以抽出父類
//鳥的類別-父類
public class Bird
{
    //翅膀屬性
    public double Wings { get; set; }
    public void EatAndDrink() {
        Console.WriteLine("我會吃喝");
    }
}

public class 麻雀 : Bird, IFlyable
{
    //實作 IFlyable的飛方法
    public void Fly() {
        Console.WriteLine("麻雀會飛");
    }
}
public class 鸚鵡 : Bird, IFlyable, ISpeak
{
    //實作 IFlyable的飛方法
    public void Fly() {
        Console.WriteLine("鸚鵡會飛");
    }
    //實作ISpeak的說話方法
    public void Speak() {
        Console.WriteLine("鸚鵡會學人說話");
    }
}
public class 鴕鳥 : Bird { }
public class 企鵝 : Bird { }

public class 直升飛機 : IFlyable
{
    public void Fly() {
        Console.WriteLine("直升飛機轉動螺旋槳飛行~~~");
    }
}

//有共同行為、共同能力：「飛」，可以用介面interface
//飛的介面
public interface IFlyable
{
    void Fly();
}

//說話的介面
public interface ISpeak
{
    void Speak();
}
```