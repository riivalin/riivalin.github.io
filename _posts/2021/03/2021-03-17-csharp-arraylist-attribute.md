---
layout: post
title: "[C# 筆記] ArrayList 常用屬性"
date: 2021-03-17 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,arraylist]
---


# ArrayList 常用屬性
 
- `Capacity`：取得或設定目前 ArrayList 能夠包含的陣列元素個數。
- `Count`：取得在 ArrayList 中實際所包含的陣列元素個數。
- `Item[Int32]`：取得或設定在指定索引位置上的陣列元素。

> 注意：`Capacity` 始终大於或等於 `Count`。

## 範例

```c#
using System.Collections;	
public static void Main()
{
    ArrayList nums = new ArrayList() { 1, 2, 3, 4, 5 };
    Console.WriteLine($"集合 nums 能夠包含的元素數：{nums.Capacity}");
    Console.WriteLine($"集合 nums 實際包含的元素數：{nums.Count}");
    Console.WriteLine($"集合 nums 中第四個元素是：{nums[3]}");
}
```

> 注意：`Capacity` 始终大於或等於 `Count`。

執行結果：

```
集合 nums 能夠包含的元素數：8
集合 nums 實際包含的元素數：5
集合 nums 中第四個元素是：4
```

[[C# 筆記] ArrayList 集合 by R](https://riivalin.github.io/posts/2011/01/arraylist/)        
[C#中 数组、ArrayList、List＜T＞的区别](https://blog.csdn.net/Dust_Evc/article/details/114984023)       
[C#集合 ArrayList 的常用方法和属性](https://blog.csdn.net/qq_42007357/article/details/104278427)