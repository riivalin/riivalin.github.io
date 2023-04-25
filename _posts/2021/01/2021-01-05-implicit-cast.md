---
layout: post
title: "[C# 筆記] 隱式轉換"
date: 2021-01-05 00:18:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 隱式類型轉換
把一個「取值範圍小」的數值或是變數，賦值給另一個「取值範圍大」的變數。

以下這樣是可以沒問題的
```c#
int a = 4; //4 bytes
long b = a; // 8bytes
double c = a; // 8bytes
```
就像是把小杯咖啡，倒到中杯裡是可以的，當然，倒到特大杯也是沒問題的。
 
int a 到 double c 轉換的細節，它中間發生了什麼樣的過程？
```text
int a => 複製一個10 => 轉換為10.0 => 給到 double 變數裡
```
也就是說，在轉換的過程中，在程式碼上沒有特別做什麼樣的處理，是隱藏起來做這件事，所以叫「隱式轉換」，像這樣合理的事，可以交給「隱式轉換」來自動完成。

##  隱式轉換的規則
- 無符號數據不能接納有符號數據
```c#
sbyte b = 10; //有符號類型數據
uint u = b; //無符號類型數據 => 這裡會報錯
```
- 數據之間的隱式轉換關係如下：
```text
byte => short =>
                int => long => float => double
         char =>
```

```c#
byte b = 10;
short s = b;
char c = 'a';
int i = s;
i = c;
long l = i;
float f = l;
double d = f;
```
為什麼long(8bytes)數據可以放入float(4bytes)類型裡呢？

因為float 取值範圍 大於 long。   
> 隱式轉換，指的是「取值範圍」不是指「占用內存」。

## 運算過程中隱式轉換
- 取值範圍小的數據，與取值範圍大的數據進行運算，小數據數值提升為大類型後，再進行運算。

小的數據類型會先轉化成大數據類型，再進行計算：結果會是大數據類型
```c#
int x = 10;
double y = 10.0;
doublc result = x + y;
```
- `byte` `short` `char`三種數據在運算的時候，都會先轉化為`int`類型。

錯誤範例：
```c#
byte x = 10;
byte y = 20;
byte z = x + y; // 會報錯
```

`byte` `short` `char`變數進行計算的時候，都會預先轉化為int類型參與計算
```c#
byte x = 10;
byte y = 20;
int z = x + y; //byte+byte結果是int
```
```c#
short x = 10;
short y = 20;
int z = x + y; //short+short結果是int
```
```c#
char x = 'a';
char y = 'b';
int z = x + y;//char+char結果是int
```

## 練習
```text
___ a = 10 + 10.f;
```
`int + float` 所以是`float`類型

```text
char c = 'a';
short s = 9;
___ num = c + s
```
`char` `short`會被編譯器轉化為`int`，再作計算，所以是`int`類型

```text
___ b = 10 + `a` + 12.5f + 10.0
```
`int + char + float + double`，所以是`double`類型

## 總結
1. 基本隱式轉換遵從怎樣的法則？
- 把一個取值範圍小的數值或者變數，賦值給另一個取值範圍大的變數。   
> 是「取值範圍」而不是「內存範圍」

2. 運算中的隱式轉換遵從怎樣的法則？
- 取值範圍小的數據，與取值圍大的數據進行運算，小數據數值提升為大類型後，再進行運算。  
> 得到的結果，就是大數據的結果
- `byte` `short` `char`三種數據在運算的時候，都會先轉化成`int`類型。

[implicit-numeric-conversions](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/language-specification/conversions#1023-implicit-numeric-conversions)