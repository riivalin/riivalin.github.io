---
layout: post
title: "[C# 筆記] 三元表達式 ? :"
date: 2011-01-06 00:01:00 +0800
categories: [Notes, C#]
tags: [C#]
---


表達式1 ? 表達式2: 表達式3  
判斷條件 ? 成立 : 不成立    

```c#
bool result = 5 > 3 ? true : false;
```

只要是可以用在 if-else 都可以用三元表達式

### 練習: 計算兩個數的大小，求出最大
```c#
int x = 10;
int y = 20;
int max = x > y ? x : y;
Console.WriteLine(max);
```

[conditional-operator](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/conditional-operator)