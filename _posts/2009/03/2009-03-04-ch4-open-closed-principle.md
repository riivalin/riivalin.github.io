---
layout: post
title: "[閱讀筆記][Design Pattern] Ch4. 開放封閉原則 Open-Closed Principle (OCP)"
date: 2009-03-04 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 開放-封閉原則 The Open-Closed Principle (OCP)
「開放-封閉原則」：是說軟體實體(類別、模組、函數……)應該可以擴展，但是不可以修改。       

> 即面對需求，對程式的改動是透過增加新程式碼進行的，而不是更改現有的程式碼。這就是「開放-封閉原則」的精神所在。

[計算機程式範例](https://riivalin.github.io/posts/2009/03/ch1-simple-factory-pattern/#簡單工廠模式)

```
Operation 運算類別
+ NumberA:decimal
+ NumberB:decimal
+ GetResult():decimal

    加法類別
    + GetResult():decimal
    減法類別
    + GetResult():decimal
    乘法類別
    + GetResult():decimal
    除法類別
    + GetResult():decimal
```

開發人員應該僅對程式中呈現出頻繁變化的那些部分做出抽象，然後，對於應用程式中的每個部分都刻意地進行抽象，同樣不是一個好主意。        

拒絕不成熟的抽象，和抽象本身一樣重要。
