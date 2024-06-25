---
layout: post
title: "[C# 筆記] int? 和 int 有什麼差別?"
date: 2017-02-09 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法]
---


- `int?` 為可空型，預設值是`null`，它允許具有正常整數值或 `null`。在需要表示缺失或未知值的情況下，可以使用 `int?`

- `int` 是值類型，不允許為 `null`，預設值是`0`，它總是有一個具體的整數值

- `int?`是透過`int`裝箱為引用型別實現


[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)  