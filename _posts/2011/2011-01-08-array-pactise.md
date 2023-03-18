---
layout: post
title: "[C# 筆記] array 練習"
date: 2011-01-08 06:08:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習1：從一個整數數組中取出最大的整數、最小整數、總和，平均值
```c#
int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
int max = nums[0]; //儲存最大值, 並將一個元素賦值給max做參照比較, 不一定要給nums[0]
int min = nums[0]; //儲存最小值, 並將一個元素賦值給max做參照比較, 不一定要給nums[0]
int sum = 0; // //儲存總和

//for迴圈每個元素都跟max,min進行比較
for (int i = 0; i < nums.Length; i++)
{
    max = max > nums[i] ? max : nums[i]; //最大值：max與每個陣列元素比較
    min = min < nums[i] ? min : nums[i]; //最小值：min與每個陣列元素比較
    sum += nums[i]; //將每個元素加到總和中
}
Console.WriteLine($"最大整數: {max} \r\n最小整數: {min}\r\n總和: {sum}\r\n平均值: {sum / nums.Length}");
```
