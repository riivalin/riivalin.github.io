---
layout: post
title: "[C# 筆記] 計算棋盤放芝麻的重量"
date: 2017-01-01 23:01:00 +0800
categories: [Notes, C#]
tags: [C#]
---

有一個棋盤，有16個方格，在第一個方格裡放1粒芝麻，芝麻重量是0.00001kg，第二個放2粒，第三個放4粒，第四個放8粒，依此類推，計算整個棋盤上所有芝麻的重量。

分析：
- 初始化
  - 定義變數 sum=0，表示芝麻總數計數器。
  - 定義變數 gridNum=1，表示當前格子內有多少芝麻，初始化為第一個方格中的芝麻數量。
- 使用for循環，循環每一個方格，計算其中芝麻數量(gridNum)，並累加到總數(sum)上
- 數量乘以每個芝麻的重量，得到所有芝麻重量

```c#
int sum = 0; //記錄芝麻累加的數量
int gridNum = 1; //記錄當前格子的芝麻數量

for (int i = 1; i <= 16; i++) {
    sum += gridNum; //將當前格子的芝麻 累加到sum
    gridNum *= 2; //計算下一個格子的芝麻數量
}
Console.WriteLine(sum * 0.00001);
```