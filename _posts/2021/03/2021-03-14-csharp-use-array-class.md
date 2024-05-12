---
layout: post
title: "[C# 筆記] 使用 Array類別來對陣列進行處理"
date: 2021-03-14 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,array]
---

# 常用的一些陣列操作

- `BinarySearch()`：用來搜尋陣列元素是否符合傳入的參數值。
- `Clear()`：用來清除指定索引範圍的陣列的元素內容。
- `Resize()`：用來變更目前陣列的大小。
- `Reverse()`：用來反轉陣列元素。
- `Sort()`：用來排序陣列元素。

---

# LINQ (Language-Integrated Query)

陣列在C#裡繼承自不同的介面(Interface)，命名空間 `System.Linq`，最原始的出發點是為了更有效率地從資料庫拿回指定的陣列集合, 也可以使用在Array陣列的操作上。如下：

- `Sum`：加總
- `Average`：平均
- `Contain`：判定是否包含滿足條件的元素
- `Max`：最大值
- `Min`：最小值


# BinarySearch()
`BinarySearch()`用來搜尋陣列元素是否符合傳入的參數值。

```c#
BinarySearch(Array, Object)
```
- Array: 要搜尋的陣列名
- Object：搜尋的關鍵字

## 範列

```c#
string[] names = {"Kii", "Mii"};
int index = Array.BinarySearch(names,"Mii");

Console.WriteLine(index);//1, Mii在陣列中的索引值是1
```

# Clear()
`Clear()`用來清除指定索引範圍的陣列的元素內容。

```c#
Clear(Array)
```

- Array: 要清除的陣列

```c#
Clear(Array, Int32, Int32)
```
- array
Array項目需要加以清除的陣列。
- index
Int32 要清除之項目範圍的起始索引。
- length
Int32要清除的項目數目。


## 範列

```c#
string[] names = {"Kii", "Mii"};
Array.Clear(names); //要清除的陣列名稱

//或是：(陣列名稱,起始索引,清除的陣列長度)
//Array.Clear(names, 0, 2); 

Console.WriteLine($"{names[0]} {names[1]}"); //陣列內元素已被清除，無任何資料顯示
```


# Resize()
`Resize()`用來變更目前陣列的大小。

```c#
Array.Resize<T>(T[], Int32) 
```
- array
T[]: The one-dimensional, zero-based array to resize, or null to create a new array with the specified size.
- newSize
Int32: The size of the new array.


## 範列

```c#
string[] names = {"Kii", "Mii"};
Array.Resize(ref names, names.Length + 1); //加入ref來傳入陣列名稱，新陣列的大小(原本的長度+1)
names[2] = "Rii";

Console.WriteLine(names[2]); //Rii
```

# Reverse()

`Reverse()`用來反轉陣列元素。

```c#
string[] names = {"Kii", "Mii", "Rii"};
Array.Reverse(names); //反轉

//看結果
foreach(var e in names) {
    Console.WriteLine(e);
}

/* 執行結果:
Rii
Mii
Kii
*/
```

# Sort()

`Sort()`用來排序陣列元素。

```c#
int[] lottery = {9,7,2,23,6,8,1,5};
Array.Sort(lottery); //由小到大排序

//輸出看結果
foreach(var e in lottery) {
    Console.WriteLine(e);
}

/* 執行結果: 
1
2
5
6
7
8
9
23
*/
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