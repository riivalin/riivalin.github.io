---
layout: post
title: "[C# 筆記] 循序結構(Sequence Structure)"
date: 2021-03-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,流程控制]
---

「 循序結構(Sequence Structure)」顧名思義就是指「在程式流程中，依照先後順序由上而下，一行一行執行下來逐一完成」。

## 範例

設計一個簡單的計算95無鉛汽油油價程式，當使用者輸入公升數時，計算出所需支付的金額。

```c#
double litre, oilPrice; //宣告公升、所需支付油價的變數(double型別)
litre = double.Parse(Console.ReadLine()!); //將使用者輸入轉換為double
oilPrice = litre * 28.6; //計算所需支付的金額

Console.WriteLine($"共需 ${oilPrice}元"); //輸出結果
```