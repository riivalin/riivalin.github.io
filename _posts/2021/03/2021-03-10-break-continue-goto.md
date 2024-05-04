---
layout: post
title: "[C# 筆記] break、contiune、goto (終止、繼續、跳躍)"
date: 2021-03-10 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,break,continue,goto]
---

# break、contiune, goto

- `break` 強制結束所在的迴圈語句(能夠結束離它最近的迴圈)，或是結束所在的`switch`語句。
- `continue` 強制結束當前迴圈的過程，開始下一次的迴圈。
- `goto`常見的用法可作用於 `switch case`標記，或者跳出複雜的巢狀迴圈。(不建議使用`goto`)


# break 終止

- `break` 用於迴圈(`for`, `while`, `do while`)與`switch`語句當中。
- `break` 只能夠結束當前所屬的語句範圍。(結束掉離它最近的迴圈)


> - 可以跳出`switch-case` 結構。
> - 可以跳出當前的迴圈(`for`, `while`, `do while`)。


`break` 一般不單獨使用，而是跟著 `if` 判斷一起使用，        
表示，當滿足某些條件的時候，就不再循環迴圈了。


## 範例1：質數判斷

實作 `n` 是否為質數。     
(質數是`大於1`，且只能被`1`或`自己本身`整除的數，不能被其他自然數整除)

```c#
static void Main(string[] args)
{
    Console.WriteLine("檢查是否為質數，請輸入一個數字:");
    int number = int.Parse(Console.ReadLine()!);

    int counter = 0; //存放被整除的次數
    for (int i = 1; i <= number; i++)
    {
        if (number % i == 0)
        {
            counter++;

            if (counter > 2)
            {
                Console.WriteLine($"{number}不是質數");
                break; //跳出迴圈
            }
        }
    }
    //質數是大於1，且只能被1或自己本身整除的數，不能被其他自然數整除
    //所以counter==2來判斷 (如果初始值i=2 開始，就要改成counter==1來判斷)
    if (counter == 2) Console.WriteLine($"{number}是質數");
}
```


## 範例2：求1~100的質數

求1~100的質數。     
(質數是`大於1`，且只能被`1`或`自己本身`整除的數，不能被其他自然數整除)

```c#
static void Main(string[] args)
{
    //質數是大於1，所以從2開始迴圈
    for (int i = 2; i <= 100; i++)
    {
        bool isPrime = true; //預設是質數
        for (int j = 2; j <= i - 1; j++)
        {
            if (i % j == 0) //可以被整除，就代表有一個因子，就不是質數
            {
                isPrime = false; //設定不是質數
                break; //不是質數，就跳出int j 這個迴圈
            }
        }
        if (isPrime) Console.WriteLine($"{i}是質數"); //是質數就輸出
    }
}
```

# continue 繼續

`continue`就是「忽略`continue`以下的程式，繼續下一個迴圈的執行」。

```c#
static void Main(string[] args)
{
    int[] score = { 89, 40, 60, 77, 99 };

    string temp = string.Empty;
    for (int i = 0; i < score.Length; i++)
    {
        //成績小於60就continue，會忽略以下程式，跳到i++開始下一個迴圈
        if (score[i] < 60) continue; 
        temp += score[i].ToString() + Environment.NewLine;
    }
    Console.WriteLine($"及格的分數如下所示：{Environment.NewLine}{temp}");
}

/* 執行結果：

及格的分數如下所示：
89
60
77
99

*/
```

> - `continue` 用於結束本次的迴圈內容，直開始下一次的迴圈。       
> (直接終結掉這次的迴圈，不執行下面的程式碼(下面的語句就不會執行了)，跳到 i++，開始下一個迴圈)


# goto 跳躍

- `goto`主要作用是將「程式控制權直接轉移到指定的標記位置陳述句中」。
- `goto`常見的用法可作用於 `switch case`標記，或者跳出複雜的巢狀迴圈。

> `goto`的便利性，違背了結構化程式設計「單一入口、一單出口」的設計理念。        
> `goto`破壞程式結果造成「單一入口，多個出口」的致命缺點。


## 範例1: goto 使用在for迴圈

```c#
static void Main(string[] args)
{
    Console.WriteLine("請輸入密碼：");
    string input = Console.ReadLine()!;


    string[] password = { "123", "abc", "xyz", "890" };

    for (int i = 0; i < password.Length; i++)
    {
        if (input == password[i])
        {
            goto Found;
        }
    }
    goto NotFound;

Found:
    Console.WriteLine("密碼正確");
    goto Finish;

NotFound:
    Console.WriteLine("密碼錯誤");

Finish:
    Console.WriteLine("執行完畢");

}

/* 執行結果:

請輸入密碼：
abc
密碼正確
執行完畢

請輸入密碼：
567
密碼錯誤
執行完畢

*/
```

## 範例2: goto 使用在 switch case 迴圈

```c#
static void Main(string[] args)
{
    Console.WriteLine("請輸入[男]或[女]：");
    while (true)
    {
        string sex = Console.ReadLine()!;

        switch (sex)
        {
            case "男":
                Console.WriteLine("男生標準體重 = (身高-80) x 0.7");
                break;
            case "女":
                Console.WriteLine("女生標準體重 = (身高-70) x 0.6");
                break;
            case "男女":
                Console.WriteLine("請輸入[男]或[女]：");
                break;
            default:
                goto case "男女"; //輸入值不為"男"或"女"，會執行 goto 跳到 case "男女" 去執行
        }
    }
}
```


[[C# 筆記] break、continue by R](https://riivalin.github.io/posts/2021/01/break-continue/)      
[[C# 筆記] break by R](https://riivalin.github.io/posts/2011/01/break/)