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

- `foreach`在一個集合中針對每一個元素反覆執行的一組敘述。     
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


## 陣列的宣告方式

```c#
//宣告的同時，就確定了初始值(用大括弧)
int[] arr = { 1, 2, 3, 4, 5 }; //宣告陣列，並直接給初始值(用大括弧)
Console.WriteLine($"共 {arr.Length} 個數量。"); //或 arr1.Count()

//宣告的同時，就確定了初始值(用方括弧)
int[] arr = [1, 2, 3, 4, 5];

//宣告的同時，就確定了類型、長度
int[] arr = new int[5];
arr[0] = 1;
arr[1] = 2;
arr[2] = 3;
arr[3] = 4;
arr[4] = 5;


//以下不推 ==========================
//不推，寫了幾個長度，就要給幾個值  
int[] arr = new int[5] { 1, 2, 3, 4, 5 };

//不推，還要多寫new int[]，意思跟直接給值一樣: int[] arr = {1,2,3,4,5}
int[] arr = new int[] { 1, 2, 3, 4, 5 };
```