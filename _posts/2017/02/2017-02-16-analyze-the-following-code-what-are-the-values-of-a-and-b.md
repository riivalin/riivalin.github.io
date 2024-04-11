---
layout: post
title: "[C# 筆記] 分析下面程式碼，a、b 的值是多少？"
date: 2017-02-16 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法]
---

```c#
string strTmp = "a1某某某";
int a = System.Text.Encoding.Default.GetBytes(strTmp).Length;
int b = strTmp.Length;

//a = 11(1+1+3*3)
//b = 5
```

分析：在`UTF-8`編碼下，每個中文字元通常佔用3個位元組，而每個英文字元和數字佔用1個位元組。

> `UTF-8`編碼一個中文字佔`3 bytes`，而`BIG5`編碼一個中文字佔`2 bytes`