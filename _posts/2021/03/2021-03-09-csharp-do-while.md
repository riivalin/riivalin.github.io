---
layout: post
title: "[C# 筆記] do while 迴圈"
date: 2021-03-09 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,重複結構,do while]
---


「重複結構」就是「當程式需要反覆執行時就會用到，通常會在不符合某些測試條件時才會離開迴圈」。

- `for`、`foreach`、`while`、`do while`

# do while

先執行迴圈，再檢查。

- `do while`當條件式(condition)成立時，才會進入迴圈執行敘述區塊(statement)。
- `do while`的特性是：不管條件是否成立，迴圈一定至少會被執行一次。

> `do while`：先執行，再判斷。最少執行一遍迴圈。

## 語法

```c#
do
{
    statement; //敘述區塊
    [continue/break;]
} while(條件式 condition); //true: 續繼迴圈, false: 結束迴圈
```

- 條件式 condition：`true`續繼迴圈。`false`結束迴圈。
- `break`：可以在迴圈中使用break終止循環。(徹底終止)。
- `continue`：可以在迴圈中使用continue，只跳過本次循環，同時進行下一次循環。

## 範例

要求用戶輸入帳號密碼，只要不是admin, 8888，就一直重新輸入。

```c#
static void Main(string[] args)
{
    string id, pw;
    do
    {
        Console.WriteLine("請輸入帳號：");
        id = Console.ReadLine()!;
        Console.WriteLine("請輸入密碼：");
        pw = Console.ReadLine()!;
    } while (id != "admin" || pw != "8888");
}
```

# while vs do-while

`while`     
先判斷，再執行。有可能一遍迴圈都不執行。

`do while`      
先執行，再判斷。最少執行一遍迴圈。


# for、while、do while 的使用時機？ 

- `for`：知道迴圈所需執行的次數，或有「起始值」、「絡止條件」、「遞增/減值」。
- `while`：只知道結束條件，而無法確定執行次數時。
- `do while`：迴圈內至少要執行一次時。



[[C# 筆記] do-while by R](https://riivalin.github.io/posts/2011/01/do-while/)        
[[C# 筆記] 變數、決策、迴圈 by R](https://riivalin.github.io/posts/2010/01/r-csharp-note-1/)          
Book: Visual C# 2005 建構資訊系統實戰經典教本