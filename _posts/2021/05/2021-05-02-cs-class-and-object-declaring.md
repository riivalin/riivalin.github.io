---
layout: post
title: "[C# 筆記] 宣告類別(Declaring Classes)"
date: 2021-05-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,class]
---

# 什麼是類別？

類別(`Class`)是用來描述某物件的結構，我們可以將`Class`視為某 `Object` 的藍圖，由一群具有相同資料結構與相同物件描述，所形成的集合。
(類別是定義物件的藍圖)      

舉例：      
若每個「跑車」都是一個物件，那麼什麼是這個物件的類別？      
此問題的正解，當然是「汽車」囉，汽車就是跑車的類別。        

我們對跑車的物件的了解，汽車的類別裡應該包含哪些東西呢？        
汽車應該有：鋁圈、車門、馬力、最高時速等屬性，      
也可能包含：供油方式、引擎技術等方法。      


# 類別關係圖

汽車為例的類別關係圖

```
類別    車名
       屬性：鋁圈、車門、馬力、最高時速
       方法：供油方式、引擎技術、其他作用
------------------------------------------------
物件 -> 名貴跑車  法拉利360
                屬性：18吋鋁圈、雙門、400HP、極速295km/h
                方法：多點噴射、自然進氣、把妹泡妞

    -> 平民跑車  日蝕GST
                屬性：17吋鋁圈、雙門、205HP、極速220km/h
                方法：多點噴射、渦輪增壓、耍帥甩尾
```

由上得知，汽車是一個類別，他定義了類別本身含有哪些屬性和方法，根據汽車類別的定義，我們可以實作出「名貴跑車-法拉利360」和「平民跑車-日蝕GST」，這二台跑車是屬於汽車的實作，這二台拉風的跑車在自己的屬性與方法都有明確的定義。        

# 類別的宣告

類別是定義物件的藍圖，那麼類別的語法應該如何宣告？

## 語法

```c#
[public | protected | internal | private] [static] class 類別名稱
{
    //定義方法與屬性
}
```

```c#
class TestClass
{
    // Methods, properties, fields, events, delegates
    // and nested classes go here.
}
```

## 範例

假設我們要宣告一個汽車的類別：      
- 屬性：鋁圈、車門、馬力、最高時速
- 方法：供油方式、引擎技術

Car 類別的宣告

```c#
public class Car
{
    public int AluminumWheel { get { return 18; } } //鋁圈
    public int CarDoor { get { return 2; } } //車門
    public int Horsepower { get { return 400; } } //馬力
    public int MaxSpeed { get { return 295; } } //極速

    //供油方式
    public string FuelSystem()
    {
        return "Multi-Port Fuel Injected"; //多當噴射
    }
    //引擎技術
    public string EngineTechnology(bool engineType)
    {
        return (engineType) ? "Natural Aspirate" : "Turbo"; //自然進氣/渦輪增壓
    }
}
```

# 物件的宣告

寫好一個類別Class之後，我們需要創建這個類別的物件，     
創建這個類別的物件過程，稱之為類別的「實體化」。

```c#
//方法一：
Car car = new Car(); //有開空間，有佔內存

//方法二：
Car car; //沒有開空間，不佔內存
car = new Car(); //有開空間，有佔內存
```

> - 使用關鍵字`new`
> - `this`：表示當前這個類別的物件(`Object`)。
> - 類別`Class`是不佔內存的，而物件`Object`是佔內存的。


[MSDN -  類別 (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/class)
[[C# 筆記] Class 類別  by R](https://riivalin.github.io/posts/2011/01/class/)       
Book: Visual C# 2005 建構資訊系統實戰經典教本    