---
layout: post
title: "[C# 筆記] 交換變數"
date: 2017-01-03 00:51:00 +0800
categories: [Notes, C#]
tags: [C#]
---

交換變數

## 使用第三方變數
```c#
int x = 10;
int y = 20;

// 開始交換
int temp;
temp = x;
x = y;
y = temp;
Console.WriteLine($"x:{x}, y:{y}");
```
## 不使用第三方的變數
``` c#
int x = 10;
int y = 20;

//開始交換
x = x - y;
y = x + y;
x = y - x;
Console.WriteLine($"x:{x}, y:{y}");
```