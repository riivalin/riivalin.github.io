---
layout: post
title: "[C# 筆記] 取出陣列元素的方法"
date: 2021-03-15 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,array,for,foreach]
---

常見取出陣列元素的方法有二種：
- `for`
- `foreach`

## for

用`for`迴圈取出陣列元素

```c#
int[] nums = { 9, 7, 2, 23, 6, 35 };
for(int i = 0; i < nums.Length; i++) {
    Console.WriteLine(nums[i]);
}
```

## foreach

用`foreach`迴圈取出陣列元素

```c#
int[] nums = { 9, 7, 2, 23, 6, 35 };
foreach(var e in nums) {
    Console.WriteLine(e);
}
```