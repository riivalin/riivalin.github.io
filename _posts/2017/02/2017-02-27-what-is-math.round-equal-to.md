---
layout: post
title: "[C# 筆記] Math.Round(11.5) 等於多少? Math.Round(-11.5) 等於多少"
date: 2017-02-27 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,Math.Round]
---

Math.Round(11.5) 等於多少? Math.Round(-11.5) 等於多少


在C#中，`Math.Round` 方法用於將浮點數捨入到最接近的整數。對於包含 `.5` 的情況，它遵循一種特定的規則，稱為「銀行家捨入」規則。 

```c#      
Math.Round(11.5) = 12
Math.Round(-11.5) = -12
```


[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)    