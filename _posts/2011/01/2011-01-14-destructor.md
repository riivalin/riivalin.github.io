---
layout: post
title: "[C# 筆記] destructor 解構函式"
date: 2011-01-14 23:10:00 +0800
categories: [Notes, C#]
tags: [C#]
---
## 解構函式
作用：幫助我們釋放資源
```c#
~Student()
{
    ....
}
```
如果你希望程式結束後，能馬上釋放資源就可以用解構函式

[classes-and-structs/finalizers](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/finalizers)