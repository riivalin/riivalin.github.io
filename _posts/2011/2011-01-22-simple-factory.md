---
layout: post
title: "[C# 筆記] 簡單工廠設計模式"
date: 2011-01-22 22:19:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 簡單工廠設計模式
### 設計模式
設計這個專案的一種方式。

### 簡單工廠設計模式
我不知道你要什麼， 
給你一個父類  
你愛給誰就給誰  
我父類裡面可以裝子類的對象  
所以我可以返回對應的子類對象給你  
  

## 範例：用抽象類來實現多態
根據用戶的輸入返回一個父類  
但是這個父類裝的是子類的對象    

調的雖然是父類  
但我們實現了多態，實際執行的是子類的方法    

我們用最大化屏蔽了各個子類對象之間的最大差異  
```c#
Console.WriteLine("請輸入您想要的品牌");
string brand = Console.ReadLine();
//表面上調的是父類的，實際上調的是子類的
NoteBook nb = GetNoteBook(brand); //調的方法是誰的，取決於傳進去的
nb.SayHello();


/// <summary>
/// 簡單工廠核心，根據用戶的輸入建立物件賦值給父類
/// </summary>
/// <param name="brand"></param>
/// <returns></returns>
static NoteBook GetNoteBook(string brand)
{
    NoteBook nb = null;
    switch (brand)
    {
        case "Lenovo":
            nb = new Lenovo();
            break;
        case "Acer":
            nb = new Acer();
            break;
        case "IBM":
            nb = new IBM();
            break;
        case "Dell":
            nb = new Dell();
            break;
    }
    return nb;
}
 

//用抽象類來實現多態
public abstract class NoteBook
{
    public abstract void SayHello();
}

public class Lenovo : NoteBook
{
    public override void SayHello()
    {
        Console.WriteLine("我是聯想筆記電腦");
    }
}
public class Acer : NoteBook
{
    public override void SayHello()
    {
        Console.WriteLine("我是宏基");
    }
}
public class Dell : NoteBook
{
    public override void SayHello()
    {
        Console.WriteLine("我是戴爾");
    }
}
public class IBM : NoteBook
{
    public override void SayHello()
    {
        Console.WriteLine("我是IBM");
    }
}
```

https://www.bilibili.com/video/BV17G4y1b78i?p=147
