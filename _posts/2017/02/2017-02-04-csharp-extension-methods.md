---
layout: post
title: "[C# 筆記] 什麼是擴展方法？"
date: 2017-02-04 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,string,StringExtensions,this,擴展方法]
---

擴充方法（`Extension Methods`）是C#中一種特殊的靜態方法，它允許你為現有的類別新增新的方法，而無需修改原始類別的定義。擴充方法通常用於為.NET框架中的類型添加功能，甚至是無法修改的封閉原始碼的類別。

要建立擴充方法，需要滿足以下條件：

1）擴展方法必須是靜態方法。     
2）擴展方法必須包含一個關鍵字 `this` 作為其第一個參數，該參數指定了該方法應用於的類型。這個參數是要擴展的類型的實例。

以下是一個簡單的擴展方法的例子，假設我們想為 `string` 類型添加一個反轉字串的方法：

```c#
public static class StringExtensions
{
    public static string Reverse(this string input)
    {
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
```

在上面的例子中，`StringExtensions` 類別是靜態類，而 `Reverse` 方法是擴充方法。透過使用 `this` 關鍵字，我們將 `Reverse` 方法關聯到 `string` 類型上。

使用擴充方法的範例：

```c#
string original = "Hello";
string reversed = original.Reverse();
Console.WriteLine(reversed); // 輸出：olleH
```

這裡 `Reverse` 方法就好像是 `string` 類別的一個原生方法。需要注意，擴展方法只是語法上的擴展，它並沒有真正修改原始類別的定義。       


[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)