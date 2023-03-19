---
layout: post
title: "[C# 筆記] Bubble Sort"
date: 2011-01-09 04:10:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 冒泡排序 Bubble Sort

冒泡排序：就是將一個陣列中的元素，按照從大到小, 或者, 從小到大的順序進行排列。

## 解析

`in[] nums={9,8,7,6,5,4,3,2,1,0};`  => 0 1 2 3 4 5 6 7 8 9  

|第一個元素，跟後面的每一個元素進行做比較，只要前面大於後面的，就交換。          ||||
|:--------------------------|:------|:----------------|:----------------|
|第一次比較：8765432109 交換9次|i=0 j=9|j=nums.Length-1-0|j=nums.Length-1-i|
|第二次比較：7654321089 交換8次|i=1 j=8|j=nums.Length-1-1|j=nums.Length-1-i|
|第三次比較：6543210789 交換7次|i=2 j=7|j=nums.Length-1-2|j=nums.Length-1-i|
|第四次比較：5432106789 交換6次|i=3 j=6|j=nums.Length-1-3|j=nums.Length-1-i|
|第五次比較：4321056789 交換5次|i=4 j=5|j=nums.Length-1-4|j=nums.Length-1-i|
|第六次比較：3210456789 交換4次|i=5 j=4|j=nums.Length-1-5|j=nums.Length-1-i|
|第七次比較：2103456789 交換3次|i=6 j=3|j=nums.Length-1-6|j=nums.Length-1-i|
|第八次比較：1023456789 交換2次|i=7 j=2|j=nums.Length-1-7|j=nums.Length-1-i|
|第九次比較：0123456789 交換1次|i=8 j=1|j=nums.Length-1-8|j=nums.Length-1-i|

>  |`nums[i]`跟 `nums[nums.Length-1-i]` 交換

## 找一下規律：    
外循環：控制走幾趙(共9趙)   
內循環：控制交換了幾次(nums.Length-1-i) 

## 程式碼

```c#
int[] nums = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

//外循環：控制總共要比較幾次，走幾趙(nums.Length - 1)
for (int i = 0; i < nums.Length - 1; i++)
{
    //內循環：每走一次，要跟後面的元素交換幾次(nums.Length-1-i)
    for (int j = 0; j < nums.Length - 1 - i; j++)
    {
        //如果比後面的元素大，就交換
        if (nums[j] > nums[j + 1]) //如果是降序，就將大於改成小於
        {
            int temp = nums[j]; //最簡單的交換，就是使用第三方變數暫存
            nums[j] = nums[j + 1];
            nums[j + 1] = temp;
        }
    }
}
// 輸出看結果
for (int i = 0; i < nums.Length; i++) {
    Console.WriteLine(nums[i]);
}
Console.ReadKey();
```
> 如果是降序，就將大於改成小於


什麼時候會用到冒泡排序? 
工作實務上不會用到，但面試的時候可能會考…XDD    

## 一行搞定冒泡排序 Array.Sort, Array.Reverse
升幕排序 Array.Sort(nums);
```c#
int[] nums = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
Array.Sort(nums); //升幕排序
```
降幕排序 Array.Reverse(nums);
```c#
Array.Reverse(nums);//降幕排序
```

雖然在學得時候，都是學最難的，  
但在用的時候，都是用最簡單的。  
要是不明白，那就是最難的…   
能理解了，才是學會了…   

