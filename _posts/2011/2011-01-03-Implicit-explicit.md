---
layout: post
title: "[C# 筆記] 隱式類型轉換 vs. 顯示類型轉換"
date: 2011-01-03 10:00:00 +0800
categories: [Notes, C#]
tags: [C#]
---

- 自動類型轉換(隱式類型轉換)   
小轉大： int-> double 
- 強制類型轉換(顯示類型轉換)   
大轉小： double -> int

兩個都是整數類型，結果就是整數int類型
```c#
int x = 10;
int y = 3;
double result = x / y;
Console.WriteLine(result); //輸出:3
```

將一個操作值提升為double類型，整個結果就是double類型
```c#
result = x * 1.0 / y; //x乘上1.0，提升為double類型
Console.WriteLine(result); //輸出:3.3333333333333335
```

使用佔符位{0:0.00}調整顯示的小數位
```c#
Console.WriteLine("{0:0.00}", result); //輸出:3.33
```

