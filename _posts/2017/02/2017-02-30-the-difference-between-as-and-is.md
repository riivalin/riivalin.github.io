---
layout: post
title: "[C# 筆記] as 和 is 的差別？"
date: 2017-02-30 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,as,is]
---


在C#中，`as` 和 `is` 是用於處理類型轉換和類型檢查的兩個不同的運算子。

- `as` 用於嘗試進行類型轉換
- `is` 用於檢查物件是否是指定類型的實體，而不進行實際的類型轉換


## as 運算子:

- `as` 運算子用於將物件轉換為指定類型，如果轉換失敗則傳回 `null`，而不會引發異常。
- 通常用於在不確定物件類型時進行類型轉換，如果轉換成功，得到一個非空值，否則得到 `null`。


```c#
object obj = "Hello";
string str = obj as string;
if (str != null)
{
    // 轉換成功
    Console.WriteLine("Conversion successful: " + str);
}
else
{
    // 轉換失敗
    Console.WriteLine("Conversion failed");
}
```

## is 運算子:

- `is` 運算子用於檢查物件是否為指定類型的實體，傳回一個布林值。
- 不執行實際的類型轉換，只是檢查物件的類型。


```c#
object obj = "Hello";
if (obj is string)
{
    // 是字串型
    Console.WriteLine("Object is a string");
}
else
{
    // 不是字串型
    Console.WriteLine("Object is not a string");
}
```

## 總結

總的來說，`as` 用於嘗試進行類型轉換，而 `is` 用於檢查物件是否是指定類型的實體，而不進行實際的類型轉換。使用它們時需要根據特定的需求來選擇。

[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)    