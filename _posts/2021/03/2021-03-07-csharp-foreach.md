---
layout: post
title: "[C# 筆記] foreach 迴圈"
date: 2021-03-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,重複結構,foreach]
---

「重複結構」就是「當程式需要反覆執行時就會用到，通常會在不符合某些測試條件時才會離開迴圈」。

- `for`、`foreach`、`while`、`do while`


# foreach 陳述句

- 它通常處理對象是針對陣列、物件的集合，透過`foreach`來取出集合中的個別元素(物件)。       
- 當集合中每一個物件都被取出時，才會離開迴圈。

## 語法

```c#
foreach(資料型別 物件變數 in 群體集合) 
{
    statement; //敘述區塊
    [continue/break;]
}
```

- 群體集合：主要有共二種：陣列、物件的集合。

## 範例

```c#
//宣告一個名為 student 的字串陣列，裡面存放學生的英文姓名
string[] student = ["Tim", "Hugo", "Rii", "Leo", "Mini"];

//使用foreach將字串陣列中的元素逐一取出
foreach (var item in student)
{
    Console.WriteLine(item);
}
//顯示共多少元素
Console.WriteLine($"共有 {student.Count()} 個學生。");
```

## 陣列宣告

```c#
//宣告陣列，並直接給初始值(用大括弧)
int[] arr1 = { 1, 2, 3, 4, 5 };
Console.WriteLine($"共 {arr1.Length} 個數量。"); //或 arr1.Count()

//宣告陣列，並直接給初始值(用方括弧)
int[] arr2 = [1, 2, 3, 4, 5];

//宣告陣列，並new初始化給初始值(用大括弧)
int[] arr3 = new int[] { 1, 2, 3, 4, 5 };

//宣告陣列，並new初始化指定長度5
int[] arr4 = new int[5];
arr4[0] = 1;
arr4[1] = 2;
arr4[2] = 3;
arr4[3] = 4;
arr4[4] = 5;
```