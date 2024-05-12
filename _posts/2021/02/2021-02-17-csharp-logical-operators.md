---
layout: post
title: "[C# 筆記] 邏輯運算子(Logical Operators)"
date: 2021-02-17 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,運算子,operator,邏輯運算子]
---

「邏輯運算子(Logical Operators)」(`!`, `~`, `&&`, `||`)用來對運算式中的運算元進行邏輯運算，並傳回`Boolean`或位元結果。      

- `!` 反相運算
- `~`位元補數運算
- `&&` AND 運算
- `||` OR 運算

## !

`!` 反相運算

```c#
bool a = false;
bool b = !a; // true
```

## ~

`~`位元補數運算

```c#
int a = 15;
int b = ~a; // -16
```

## &&

`&&` AND運算

```c#
bool a = true;
bool b = false;
bool c = a && b; // false
```

## ||

`||` OR 運算

```c#
bool a = true;
bool b = false;
bool c = a && b; // true
```

Book: Visual C# 2005 建構資訊系統實戰經典教本 