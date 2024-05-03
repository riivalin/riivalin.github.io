---
layout: post
title: "[C# 筆記] ?: 運算子"
date: 2021-03-05 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,流程控制,"?:"]
---

「選擇結構(Selection Structure)」會根據程式的「判斷條件」是否成立來決定程式最後要往哪一流程(程序)去跑。     

選擇結構包含：
- `if`陳述句
- `switch`陳述句
- `?:`運算子

# ?: 運算子

```c#
變數 = 運算式 ? ture的結果 : false的結果
```

# 範例

成績 60分以上「及格」，低於60「被當」。

```c#
string result = (score >= 60) ? "及格" : "被當";
```