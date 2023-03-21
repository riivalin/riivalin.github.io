---
layout: post
title: "[C# 筆記] function 練習4"
date: 2011-01-12 23:19:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習：計算任意多個數間的最大值(params)
[params 參數](http://127.0.0.1:4000/posts/params_s/)能把元素當作陣列去處理
```c#
int max = GetMax(2, 1, 4, 6, 5, 7); //params能把元素當作陣列去處理
Console.WriteLine(max);
Console.ReadKey();

//params參數方法
public static int GetMax(params int[] nums) //params可變動參數
{
    int max = nums[0]; //先給一個假定值
    for (int i = 0; i < nums.Length; i++)
    {
        max = max > nums[i] ? max : nums[i];
    }
    return max;
}
```
## 練習：用冒泡排序做升幕排序(ref)
通過[冒泡排序](http://127.0.0.1:4000/posts/bubble-sort/)對整數陣列做升幕排序    
{ 1, 2, 3, 7, 9, 100, 2, 4, 6, 8, 10 }
```c#

int[] nums = { 1, 2, 3, 7, 9, 100, 2, 4, 6, 8, 10 };
R.ToSort(ref nums);

for (int i = 0; i < nums.Length; i++) {
    Console.WriteLine(nums[i]);
}
Console.ReadKey();

//ref參數方法
public static void ToSort(ref int[] nums) 
{
    for (int i = 0; i < nums.Length; i++)
    {
        for (int j = 0; j < nums.Length-1-i; j++)
        {
            if (nums[j] > nums[j + 1]) {
                int temp = nums[j];
                nums[j] = nums[j + 1];
                nums[j + 1] = temp;
            }
        }
    }
}
```
> 使用[ref 參數](https://riivalin.github.io/posts/ref/)，將字串陣列帶入方法中，在方法中改變後，再帶出來

## 練習：將一個字串陣列以"|"分割輸出
{小明|小王|小月|小六}
```c#
string[] names = { "小明", "小王", "小月", "小六" };
string s = ProcessString(names);
Console.WriteLine(s);
Console.ReadKey();

public static string ProcessString(string[] names)
{
    string str = null;
    for (int i = 0; i < names.Length - 1; i++)
    {
        str += names[i] + "|";
    }
    return str + names[names.Length - 1]; //加上最後一個
}
```

[params 參數](http://127.0.0.1:4000/posts/params_s/)    
[冒泡排序](http://127.0.0.1:4000/posts/bubble-sort/)    
[ref 參數](https://riivalin.github.io/posts/ref/)   