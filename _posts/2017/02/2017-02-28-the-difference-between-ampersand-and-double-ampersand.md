---
layout: post
title: "[C# 筆記] &和 && 的區別"
date: 2017-02-27 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,"&","&&"]
---

- `&` 是位元與運算符，同時也可用於邏輯與操作，但不會短路。
- `&&` 是邏輯與運算符，具有短路的特性。


## & 運算子:
`&` 是位元與運算符，用於對整數類型的對應位元執行位元與操作。      

在邏輯上，`&` 也可用於執行邏輯與操作，但與 `&&` 不同，`&` 會對兩側的操作數都進行求值，而不會短路。      

例如，`if (condition1 & condition2)`，無論 `condition1` 是否為 `false`，`condition2` 都會被求值。

## && 運算子:
`&&` 是邏輯與運算符，用於執行邏輯與運算。       

`&&` 具有短路的特性，即如果第一個條件為 `false`，則不會對第二個條件進行求值。       

例如，`if (condition1 && condition2)`，如果 `condition1` 為 `false`，則不會執行 `condition2` 的求值。

## 範例:

```c#
int a = 5;
int b = 10;

// 使用 & 進行位元與運算
int result1 = a & b; // 結果是 0b0000 (0)

// 使用 && 進行邏輯與運算
bool condition1 = (a > 0) && (b > 0); // 結果是 true

// 使用 & 進行邏輯與運算，不會短路
bool condition2 = (a > 0) & (b > 0); // 結果是 true，即使第一個條件為 false，仍會對第二個條件求值
```

[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)    