---
layout: post
title: "[C# 筆記] Stack (堆疊/棧), Heap (堆積/堆)"
date: 2011-01-15 22:01:50 +0800
categories: [Notes, C#]
tags: [C#]
---

## 記憶體配置
Stack(堆疊)和 Heap(堆積) 是存放資料的記憶體分成兩種不同的管理機制。  

簡單來說, 從記憶體配置的角度, 用一個二分法  
- Stack 堆疊：用於「靜態」記憶體配置, 大陸翻譯為棧
- Heap 堆積：用於「動態」記憶體配置, 大陸翻譯為堆

## 儲存
棧內存(Stack) ＆ 堆內存(Heap)   
- 值類型的值是儲存在內存的棧當中。棧內存(Stack)
- 引用類型的值是儲存在內存的堆當中。堆內存(Heap)    

## Stack(堆疊), Heap(堆積) 
- 堆疊可以想像成一個一個疊起來的盒子，數值型別的變數就一個一個放在盒子內。  
當變數生命周期結束時，盒子就會被移走。  

- 堆積就像一個空地內亂七八糟的擺了一堆盒子，  
然後盒子上有標明這個盒子目前是屬於誰在使用的(可以很多人使用同一個盒子)。  


[堆疊(Stack)和堆積(Heap) 還有Boxing與Unboxing觀念釐清](https://dotblogs.com.tw/lastsecret/2010/02/25/13757)  
[[探索 5 分鐘] stack 與 heap 的底層概念](https://nwpie.blogspot.com/2017/05/5-stack-heap.html)  

