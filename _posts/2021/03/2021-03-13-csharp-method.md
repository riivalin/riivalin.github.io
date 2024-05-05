---
layout: post
title: "[C# 筆記] 陣列常用的方法(Method)"
date: 2021-03-13 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,array]
---

# 常用方法

- `Clear`：清空陣列中的元素並設定成元素的預設值
- `Exist`：判定陣列中是否有滿足指定條件的元素
- `Find`：找尋陣列中滿足指定條件的元素
- `FindIndex`：返回陣列中滿足指定條件的元素索引值
- `GetLength`：返回指定維度的長度
- `GetValue`：範圍指定索引位置的值
- `Copy`：從一個陣列複製到另一個陣列

--- 
- `CopyTo()`: 複製陣列元素的內容。
- `GetLength()`: 取得陣列的長度。       
> `Length`是取所有的元素個數，`GetLength()`是取行數和列數。     

- `GetLowerBound()`: 取得陣列維度的下限索引值。(獲得下限，即最小)
- `GetUpperBound()`: 取得陣列維度的上限索引值。(獲得上限，即最大)
- `GetValue()`: 取得陣列元素值。
- `SetValue()`: 設定陣列元素值。


# CopyTo()
將目前一維陣列的所有項目複製到指定的一維陣列。      
(複製陣列元素的內容)

```c#
CopyTo(Array, Int32)
```

- Array：一維陣列，從目前陣列複製過來的項目之目的端。
- Int32：代表 array 中的索引，由此開始複製。


## 範例
複製 Array 另一個 Array 

```c#
public static void Main()
{
    string[] author = {"Rii", "JYY"};
    string[] copyAuthor = new string[2];

    //開始copy, 從索引0開始複製
    author.CopyTo(copyAuthor,0);
    
    //查看結果
    foreach(var e in copyAuthor) {
        Console.WriteLine(e);
    }
}
```

# GetLength()

取得陣列的長度

```c#
Array.GetLength(Int32)
```

- Int32：一維陣列就是`0`，維度2是`1`


## 範例

```c#
string[] author = {"Rii", "JYY"};
Console.WriteLine(author.GetLength(0)); //一維陣列就是0

//執行結果：2
```

# Length 和 GetLength() 的區別?

## 程式碼格式
首先`Length`直接用，`GetLength()`需要用括號       

`Length`是取所有的元素個數，`GetLength()`是取行數和列數     

## 說明

```
假設一個陣列：(兩行三列)
arry =
1,2,3
4,5,6
```

長度為`6`；     
`GetLength(0)`是`2`，(`GetLength(0)`表示取行數)        
`GetLength(1)`是`3`

## 實驗範例

```c#
int[,] nums = {
    {1,2,3},
    {1,2,3}
};

Console.WriteLine(nums.Length); //6,總元素有6
Console.WriteLine(nums.GetLength(0)); //第一維長度為2
Console.WriteLine(nums.GetLength(1)); //第二維長度為3
```

# GetLowerBound()

取得陣列維度的下限索引值。        
(獲得下限，即最小)

```c#
GetLowerBound(Int32) 
```
- Int32：一維陣列就是`0`，維度2是`1`

## 範例

取得第一維度的下限(最小索引值)。        


```c#
string[] author = {"Rii", "JYY"};
Console.WriteLine(author.GetLowerBound(0)); 
//執行結果：0
```


# GetUpperBound()

取得陣列維度的上限索引值。        
(獲得上限，即最大)

```c#
GetUpperBound(Int32) 
```
- Int32：一維陣列就是`0`，維度2是`1`

## 範例

取得第一維度的上限(最大索引值)。        


```c#
string[] author = {"Rii", "JYY"};
Console.WriteLine(author.GetUpperBound(0)); 
//執行結果：1
```

# GetValue()

```c#
GetValue(Int32)
```
- Int32: 要取得Array 元素的位置(index)。

## 範例

獲得陣列元素值

```c#
string[] author = {"Rii", "JYY"};
Console.WriteLine(author.GetValue(0)); //Rii
```

# SetValue()

```c#
SetValue(Object, Int32)
```
- Object: 指定項目的新值 (要設定的值)。
- Int32: 要取得Array元素的位置(index)。

## 範例
設定陣列元素值

```c#
string[] author = {"Rii", "JYY"};
author.SetValue("YYY",1); //設定索引位置為1 的值
Console.WriteLine(author[1]); //YYY
```

[MSDN - Array.CopyTo 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.copyto?view=net-8.0)      
[MSDN - Array.GetLength(Int32) Method](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.getlength?view=net-8.0)       
[MSDN - Array.GetLowerBound(Int32) Method](https://learn.microsoft.com/en-us/dotnet/api/system.array.getlowerbound?view=net-8.0)    
[MSDN - Array.GetValue 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.getvalue?view=net-7.0)       
[MSDN - Array.SetValue 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.array.setvalue?view=net-8.0)     
[c#中的Length和GetLength()的区别](https://blog.csdn.net/weixin_41529093/article/details/105346526)