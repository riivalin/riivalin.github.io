---
layout: post
title: "[C# 筆記] 三元條件運算子？："
date: 2021-01-03 00:12:00 +0800
categories: [Notes, C#]
tags: [C#]
---

作用：從二者當中選擇其一，作為運算式的結果  
變數 = 判斷條件? 值1: 值2  

如果條件為真，變數等於值1  
如果條件為假，變數等於值2  

```c#
int x = 2, y = 9;
int max = (x > y) ? x : y;
Console.WriteLine(max); //output: 9
```
簡單來說，就一句話「二者之中，選擇一個」。

練習：給三個數字，輸出最大的數字
```c#
int x = 10, y = 20, z = 30;
int max = (x > y) ? x : y;
max = max > z ? max : z;

Console.WriteLine(max); //output: 30
```

[operators/conditional-operator](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/conditional-operator)