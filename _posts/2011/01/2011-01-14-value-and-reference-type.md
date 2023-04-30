---
layout: post
title: "[C# 筆記] Value type, Reference type"
date: 2011-01-14 23:33:00 +0800
categories: [Notes, C#]
tags: [C#,stack,heap]
---

## 值類型和引用類型
區別：
1. 值類型和引用類型在內存上儲存的地方不一樣。
2. 在傳遞值類型和傳遞引用類型的時候，傳遞的方式不一樣。

值類型我們稱之為值傳遞，引用類型我們稱之為引用傳遞

## 值類型 Value Type
int, double, bool, char, decimal, struct, enum

## 引用類型 Reference Type
string, 自定義類, 陣列

## 儲存
棧內存(`Stack`) ＆ 堆內存(`Heap`)   
- 值類型的值是儲存在內存的棧當中。棧內存(Stack)
- 引用類型的值是儲存在內存的堆當中。堆內存(Heap)    

## R Note
簡單來說, 從記憶體配置的角度, 用一個二分法
- stack 用於靜態記憶體配置, 大陸翻譯為棧
- heap 用於動態記憶體配置, 大陸翻譯為堆

[值類型 Value Type, 引用類型 Reference Type](https://riivalin.github.io/posts/value-and-reference-type/)  
[[探索 5 分鐘] stack 與 heap 的底層概念](https://nwpie.blogspot.com/2017/05/5-stack-heap.html)
