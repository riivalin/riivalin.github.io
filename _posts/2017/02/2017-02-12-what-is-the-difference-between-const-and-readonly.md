---
layout: post
title: "[C# 筆記] const 和 readonly 有什麼不同？"
date: 2017-02-12 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法]
---

都用於定義常數。主要有以下區別：


1. 初始化位置不同。 `const`必須在宣告的同時賦值；`readonly`既可以在宣告處賦值，也可以在靜態建構方法（必須是靜態建構方法，普通建構方法不行）裡賦值。

2. 修飾對象不同。 `const`即可以修飾類別的欄位，也可以修飾局部變數；`readonly`只能修飾類別的欄位

3. `const`是編譯時常數，在編譯時決定該值；`readonly`是執行時間常數，在執行時決定該值。

4. `const`預設是靜態的；而`readonly`如果設定成靜態需要顯示聲明

5. 修飾引用型別時不同，`const`只能修飾`string`或值為`null`的其他引用型別；`readonly`可以是任何型別。        


[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)  