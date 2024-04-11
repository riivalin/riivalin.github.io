---
layout: post
title: "[C# 筆記] short s1 = 1; s1 = s1 + 1; 有什麼錯? short s1 = 1; s1 += 1; 有什麼錯？"
date: 2017-02-20 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法]
---


```c#
s1 = s1 + 1; //會造成編譯錯誤，需要明確型別轉換。
s1 += 1; //不會造成編譯錯誤，編譯器會自動進行型別轉換
```



[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)  