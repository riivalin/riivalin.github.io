---
layout: post
title: "[C# 筆記] 匿名方法(Anonymous methods)"
date: 2021-02-03 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,匿名方法,委派,Delegate,委託]
---


「匿名方法」主要是為了「簡化不必要的程式碼的撰寫」，讓程式開發人員更專心把心力放在程式的設計與邏輯上。      

其中比較常用的例子是當使用「委派(Delegate)」時，不需要建立額外的方法。      


## 什麼是「匿名方法」？

- 「匿名方法」就是沒有名字的方法。(匿名跟委派也有點關係...)
- 他的本質就是一個函數
- 當你方法只會用到一次的時候，就可以考慮用匿名函數


## 語法

```c#
delegate (arguments) { statements }

```
- delegate: 匿名函式的關鍵字
- arguments: 傳入的參數，可以多個參數(以逗號「`,`」隔開)
- statements: 此函式執行的程式碼片段

## 範例

`delegate` 這段就叫做「匿名函數」        
他的本質就是一個函數

```c#
delegate (string name) {
    return name.ToLower();
}
```


## 使用「匿名方法」的委派

使用匿名方法的委派，不需要明確定義出執行委派方法的名稱，而是在宣告委派後直接寫出程式碼。

```c#
//1.宣告一個委派類型的變數，它代表一個方法(沒有回傳值，需要傳入string類型的參數數)
delegate void Printer(string s); 
static void Main(string[] args)
{
    //2.宣告委派後直接寫出程式碼
    Printer p2 = delegate (string s) { 
        Console.WriteLine(s); 
    };
    //3.調用方法
    p2("匿名方法真好用!");
}
```

## 不使用「匿名方法」的委派

不使用匿名方法的委派，需要明確定義出執行委派方法的名稱之後，再進行委派。

```c#
//1.委派會調用的方法
static void Display(string s)
{
    Console.WriteLine(s);
}

//2.宣告一個委派類型的變數，它代表一個方法(沒有回傳值，需要傳入string類型的參數數)
delegate void Printer(string s); 
static void Main(string[] args)
{
    //3.宣告並實體化委派，並傳入相對應的方法
    Printer p = new Printer(Display);
    //4.調用方法
    p("匿名方法真好用，我是不使用匿名方法的委派!");

}
```

## 委派概念

- 宣告一個委派指向一個方法(把方法封裝進變數內)
- 委派所指向的方法，必須符合委派定義的方法
- 跟實體化「執行緒」很像，都是傳入一個方法

```c#
Thread t = new Thread(SayHi);
```

## 使用委派3個步驟

1. 宣告一個委派類型的變數，指向一個方法
2. 宣告並實體化委派，並傳入相對應的方法
3. 執行方法

        
```c#
// 1. 宣告一個委派類型的變數，指向一個方法(該方法沒有回傳值，需傳入一個string類型的參數)
delegate void Printer(string s);

static void Main(string[] args)
{
    // 2.宣告並實體化委派，並傳入相對應的方法
    Printer p = new Printer(Display);
    // 3.執行方法
    p("Hello");
}

//委派要調用的方法
static void Display(string s)
{
    Console.WriteLine(s);
}
```

[MSDN - 使用具名和匿名方法委派的比較 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/delegates/delegates-with-named-vs-anonymous-methods)       
[[C# 筆記] Delegate 匿名函數 by R](https://riivalin.github.io/posts/2011/01/delegate-anonymous-function/)