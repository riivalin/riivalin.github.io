---
layout: post
title: "[C# 筆記] C# 中的委託是什麼？"
date: 2017-02-10 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,委託,Delegate]
---

委託（`Delegate`）是一種類型，它表示對一個或多個方法的引用。委託可以看作是函數指標的一種類型安全的封裝，它允許將方法作為參數傳遞給其他方法，或將方法賦值給委託變數。        

.net中有很多內建的委託類型，如`Action`和`Func`，它們分別用來表示無回傳值的方法和有回傳值的方法。這些內建委託類型在泛型和非泛型形式中都可用      

作用：提高方法的擴展性      


[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)  