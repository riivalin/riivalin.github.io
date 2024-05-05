---
layout: post
title: "[C# 筆記] 陣列常用的屬性(Attribute)"
date: 2021-03-12 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,array,rank,length]
---

「陣列」所代表就是一個「物件」，既然陣列是一個物件，那麼也表示它應該還提供了一些操作陣列的屬性和方法。

## 常用的屬性

- `Length`：陣列裡所有維度的元素總和(不規則陣列僅顯示第一維度的長度)。
- `Count`：和`Length`功能相同。
- `Rank`：顯示陣列的維度。



## Length 長度

`Length`用來取得陣列的長度(陣列的元素總數)。

```c#
static void Main(string[] args)
{
    int[,] lottery = {
        { 02,33,06,02,04,11},
        { 12,23,26,35,07,01}
    };

    Console.WriteLine($"陣列長度為：{lottery.Length}");
}

//執行結果：
//陣列長度為：12
```

## Rank 維度

`Rank`用來取得陣列的「維度」。

```c#
static void Main(string[] args)
{
    int[,,] arr3D = new int[2, 4, 8];
    Console.WriteLine($"這是 {arr3D.Rank} 維陣列");
}

//執行結果：
//這是 3 維陣列
```