---
layout: post
title: "[C# 筆記] a.Equals(b) 和 a == b 一樣嗎？"
date: 2017-02-24 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,Equals]
---

## a.Equals(b)

- `Equals` 方法是從 `System.Object` 類別繼承而來的，因此對於所有類型都是可用的。
- 預設情況下，`Equals` 方法執行的是引用比較，即檢查兩個物件是否**引用「同一個記憶體位置」**。
- 子類別可以重寫 `Equals` 方法以提供自訂的相等性比較。

```c#
object a = new object();
object b = a;
bool result = a.Equals(b); // 引用比較
```

## a == b 

- `== `操作符的行為取決於特定的類型。對於引用類型，`==` 執行的是引用比較，與 `Object.ReferenceEquals`方法的行為相同。
- 對於值類型，`==` 運算子通常會執行值比較，即比較兩個物件的值是否相等。但某些值類型可以透過重寫 `==` 操作符來改變這種行為。

```c#
object a = new object();
object b = a;
bool result = (a == b); // 引用比較
```

## 總結

需要注意的是，對於自訂類型，如果沒有重寫 `Equals` 方法和 `==`操作符，它們將預設執行引用比較，即比較物件的參考是否相同。     

在一些常見的值類型（如 `int`、`double` 等）和字串類型上，`==` 運算子通常會執行值比較，而不是引用比較。但對於自訂類型，特別是引用類型，最好重寫 `Equals `方法以提供有意義的相等性比較。      
            

[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)  