---
layout: post
title: "[C# 筆記] 顯式轉換"
date: 2021-01-05 00:58:00 +0800
categories: [Notes, C#]
tags: [C#]
---

把一個<span style="color: red;">取值範圍大</span>的數值或是變數，賦值給另一個<span style="color: red;">取值範圍小</span>的變數。
- 不可以直接賦值，需要<span style="color: red;">強制類型轉換</span>

就像是把特大杯咖啡，倒到中杯裡是不可以的，當然，倒到小杯也是有問題的。

## 顯式類型轉換 `(類型)變數`
錯誤範例
```c#
long a = 10;
int b = a; //報錯，這樣是不行的
``` 
顯式轉換 (int)
```c#
long a = 10;
int b = (int)a; //使用 (int) 轉換
```
```c#
float a = 10.6f;
int num = (int)a; //10
```
> 注意: 浮點數float 轉換為整數int，小數部分會直接拋棄(截斷)   

- 如果大整數類型給到小整數類型，數據有可能被截斷或是歧義   

### 範例：數據截斷(精度丟失)   
將257較大數據，賦值一個byte數據，得到1
```c#
int a = 257;
byte b = (byte)a; //output: 1
```

### 範例：數據歧義(含義改變)   
將130較大數據，賦值一個sbyte數據，得到-126
```c#
int a = 130;
sbyte b = (sbyte)a; //output: -126
```
## 顯式類型轉換 Convert.To
Convert是C#提供的類，裡面含有一系列的方法。
- 特點：任意數據類型的值，轉換成任意數據類型
```c#
int a = Convert.ToInt32('a'); //97
int b = Convert.ToInt32("123");
```
- 數據類型無法轉換時，會報錯。
```c#
double b = Convert.ToDouble("123.af1");
```

## 總結
1. 顯示轉換有哪兩種方式？
- (類型)變數; `(int)num;`
- 使用`Convert`類提供的功能
2. 什麼是轉換時候的截斷與歧義？
- 截斷：大空間給到小空間變數數據，只能給到一部分，從而導致丟失(精度丟失)。
- 歧義：大空間給到小空間變數數據，只能給到一部分，從而導致數據含義的改變。

