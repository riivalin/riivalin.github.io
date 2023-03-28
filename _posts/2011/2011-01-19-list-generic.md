---
layout: post
title: "[C# 筆記] List 泛型集合"
date: 2011-01-19 22:09:00 +0800
categories: [Notes, C#]
tags: [C#]
---

##  List 泛型集合
優點：長度任意

```c#
//建立整數類型的泛型集合
List<int> list = new List<int>();

list.Add(1); //加入一個元素
list.AddRange(new int[] { 1, 2, 3, 4, 5 }); //加入一個整數集合

//遍歷每個元素
for (int i = 0; i < list.Count; i++) {
    Console.WriteLine(list[i]); 
}

//list能轉換成什麼樣的陣列，取決於list的類型
int[] nums = list.ToArray(); //list 轉成陣列
List<int> numList = nums.ToList(); //int[]陣列轉List
```
### List 能轉換成什麼樣的陣列，取決於list的類型
```c#
List<string> listStr = new List<string>();
listStr.AddRange(new string[] { "a", "b", "c" });
string[] arrStr = listStr.ToArray(); //轉成陣列
```
### 陣列轉換成List
```c#
char[] chs = new char[] { 'a', 'b', 'c' };
chs.ToList<char>();
```
### ArrayList, Hashtable 很少在用，為什麼?
除了取數據不方便外，花費時間較多、效率低外，  
因為涉及到裝箱、拆箱的問題



[Boxing & Unboxing 裝箱 & 拆箱](https://riivalin.github.io/posts/boxing-unboxing/)