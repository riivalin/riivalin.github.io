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


[MSDN - Array.CopyTo 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.copyto?view=net-8.0)      
[MSDN - Array.GetLength(Int32) Method](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.getlength?view=net-8.0)       
[MSDN - Array.GetLowerBound(Int32) Method](https://learn.microsoft.com/en-us/dotnet/api/system.array.getlowerbound?view=net-8.0)    
[MSDN - Array.GetValue 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.getvalue?view=net-7.0)       
[MSDN - Array.SetValue 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.setvalue?view=net-8.0)     
[c#中的Length和GetLength()的区别](https://blog.csdn.net/weixin_41529093/article/details/105346526)      
[[C# 筆記] array 陣列  by R](https://riivalin.github.io/posts/2011/01/array/)       
[[C# 筆記] 陣列(Array)的宣告   by R](https://riivalin.github.io/posts/2021/03/csharp-array/)        
Book: Visual C# 2005 建構資訊系統實戰經典教本