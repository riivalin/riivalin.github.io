---
layout: post
title: "[C# 筆記] for 迴圈 (for-loop)"
date: 2021-03-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,重複結構,for,99乘法表]
---

「重複結構」就是「當程式需要反覆執行時就會用到，通常會在不符合某些測試條件時才會離開迴圈」。

- `for`、`foreach`、`while`、`do while`

# for 迴圈

- `for`主要運作方式是：設定計數器(`counter`)的「起始值」、「判斷條件式」、「遞增/減值」三個部分，用來決定重複執行的規則與次數。
- 在`for`區塊內可以與`break`或`continue`來搭配使用，通常在不符合「判斷條件式」時，便會離開迴圈。     
- `for`通常會與陣列搭配使用，可以逐一取出陣列元性的內容值。

## 語法

```c#
for(初始值; 判斷條件式; 遞增值) {
    statement;
    [continue/break;]
}
```

## 範例：累加

透過 for迴圈，從0累加使用者輸入的數字n。

```c#
int number = int.Parse(Console.ReadLine()!); //接收用戶輸入

int sum = 0;
for (int i = 0; i <= number; i++)
{
    sum += i;
}

Console.WriteLine($"1+...+ {number} = {sum}");
```


## 範例：巢狀迴圈

99乘法表

```c#
for (int i = 1; i <= 9; i++)
{
    for (int j = 1; j <= 9; j++)
    {
        Console.WriteLine($"{i}*{j}={i*j}");
    }
}
```

> 巢狀迴圈：當遇到某個事情要做一遍，而另一個事情要做N遍的時候。     
> (一個迴圈內，使用另一個或多個迴圈。)