---
layout: post
title: "[C# 筆記] 為屬性(get/set)設定初始值"
date: 2021-05-07 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,屬性,get-set]
---


## 使用建構函式

透過建構子來設定

```c#
public class Person
{
    public Person() //建構子 ctor
    {
        this.Name = "Initial Name";
    }
    public string Name { get; set; }
}
```
在構造函數中。構造函數的目的是初始化它的資料成員。      
使用建構函數，因為「當建構函數完成時，構造應該完成」。      
屬性就像你的類別所持有的狀態，如果你必須初始化一個預設狀態，你會在你的建構函式中這樣做。      

## 使用普通屬性(帶有初始值)

在 屬性背後的實際欄位（backing field）直接設定。

```c#
public class Person
{
    private string _name = "Initial Name"; //屬性背後的實際欄位（backing field）直接設定
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}
```

## 使用自動屬性(指定初始值) C# 6

在宣告屬性的時候就設定初始值。      
(在定義自動屬性的時候用 `= `運算子來加上賦值敘述，以設定該屬性的初始值)

```c#
public class Person
{
    public string Name { get; set; } = "Initial Name"; //直接用=，指定初始值
}
```

## 唯讀的自動屬性 C# 6

唯讀的自動屬性可以使用 Lambda 運算子`=>`設定初始值。

```c#
public class Person
{
    public string Name => "Initial Name";
}
```

## C# (7.0)

Lambda 運算子`=>` + 屬性背後的實際欄位(backing field)

```c#
public class Person
{
    private string _name = "Default Value";
    public string Name
    {
        get => _name;
        set => _name = value;
    }
}
```

        
[MSDN - 使用屬性 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/using-properties)     
[MSDN - 自動實作的屬性 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties)        
[C# 的唯讀自動屬性是怎樣煉成的  by huanlintalk](https://www.huanlintalk.com/2018/02/c-readonly-auto-property-from-beginning.html)       
[CSDN - 为 C# 自动属性赋予初始值的最佳方法是什么？](https://blog.csdn.net/kalman2019/article/details/128624090)     
[HuntsBot - 为 C# 自动属性赋予初始值的最佳方法是什么？](https://www.huntsbot.com/qa/Zv4Y/what-is-the-best-way-to-give-a-c-sharp-auto-property-an-initial-value?lang=zh_CN&from=csdn)        
[[C# 筆記] get set 自動屬性 & 普通屬性  by R](https://riivalin.github.io/posts/2011/01/auto-and-normal-properties/)    
Book: Visual C# 2005 建構資訊系統實戰經典教本 