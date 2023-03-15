---
layout: post
title: "[C# 筆記] 賦值運算符=、加號、佔符位{0}"
date: 2011-01-01 23:17:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 賦值運算符 =

`=` 表示賦值的意思，表示把等號右邊的值，賦值給等號左邊的變數。    
(不是數學意義上的相等)

```c#
int num = 10;
num = 50; //重新賦值50給num
```

## + 加號的使用
- 連接   
當`+`號兩邊，有一邊是字串的時候，`+`號就起連接的作用
```c#
Console.WriteLine(5+"5"); //output:55
```
- 相加   
兩邊是數字的時候
```c#
Console.WriteLine(5+5); //output:10
```

## 佔符位 `{0}`
先挖坑，再填坑
> 先佔住一個固定的位置，再往裡面添加內容的符號

```c#
int x = 10;
int y = 20;
int z = x + y;
Console.WriteLin("{0}+{1}={2}", x,y,z);
```
也可以
```c#
Console.WriteLin($"{x}+{y}={z}");
```