---
layout: post
title: "[C# 筆記] 計算時間差距: 天換算周+天、秒換算天+時+分+秒"
date: 2017-01-05 23:09:00 +0800
categories: [Notes, C#]
tags: [C#]
---
## 練習：46天是幾周零幾天？
```c#
int days = 46;
int weeks = days / 7;
int day = days % 7;
Console.WriteLine($"{days}天是{weeks}周零{day}天");
```

## 練習：107653秒是幾天幾小時幾分鐘幾秒？
```c#
/*
 60*60= 3600 一小時有3600秒
 3600*24 = 86400 一天有86400秒
 */

int seconds = 107653;
int days = seconds / 86400; //求得天數
int remainSeconds = 107653 % 86400; //剩餘的秒數
int hours = remainSeconds / 3600; //求得時數
remainSeconds = remainSeconds % 3600; ////剩餘的秒數
int minutes = remainSeconds / 60; //求得分鐘數
seconds = remainSeconds % 60; //求得秒數

Console.WriteLine($"107653秒是{days}天{hours}小時{minutes}分鐘{seconds}秒");
Console.ReadKey();

//output: 107653秒是1天5小時54分鐘13秒
```