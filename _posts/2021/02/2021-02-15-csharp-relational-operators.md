---
layout: post
title: "[C# 筆記] 關係運算子(Relational Operators)"
date: 2021-02-15 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,運算子,operator,關係運算子,比較運算子]
---

- 「關係運算子(Relational Operators)」又稱「比較運算子(Comparision Operators)」。
- 其作用是用來判斷「比較兩個值之間是否符合」的關係運算子。
- 所有關係運算子均會產生布林(`Boolean`)值。

例如：我們設定：`a = 5`、`b = 10`       


| 運算子| 說明     | 實例      |執行結果|
|:-----:|:---------:|:------------:|:----:|
| `==` | 等於 | bool c = (a == b) | c = false |
| `!=` | 不等於 | bool c = (a != b) | c = true |
| `<` | 小於 | bool c = (a < b) | c = true |
| `>` | 大於 | bool c = (a > b) | c = false |
| `<=` | 小於等於 | bool c = (a <= b) | c = true |
| `>=` | 大於等於 | bool c = (a >= b) | c = false |


        