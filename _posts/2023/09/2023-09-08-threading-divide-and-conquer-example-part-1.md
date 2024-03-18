---
layout: post
title: "[C# 筆記] Threading - Divide and Conquer Example Part 1"
date: 2023-09-08 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---

##

先寫一個可以產生很多整數的方法

```c#
internal class Program
{
    static byte[] values = new byte[500000000]; //因為需要很多整數，會造成內存不足的異常，所以用byte
    static void GenerateInts() 
    {
        var rnd = new Random(); //使用隨機數
        for (int i = 0; i < values.Length ; i++) {
            values[i] = (byte)rnd.Next(10); //隨機產生的數字0-9放到陣列中
        }
    }
    static void Main(string[] args)
    {
    }
}
```

對這些值進行相加(求和)

```c#
internal class Program
{
    static byte[] values = new byte[500000000];
    static void GenerateInts() 
    {
        var rnd = new Random();
        for (int i = 0; i < values.Length ; i++) {
            values[i] = (byte)rnd.Next(10);
        }
    }
    static void Main(string[] args)
    {
        GenerateInts(); //產生很多整數
        Console.WriteLine("Summing...");

        //進行相加
        long total = 0;
        for (int i = 0; i < values.Length; i++) {
            total += values[i];
        }

        Console.WriteLine($"Total value is: {total}");
    }
} 
```

執行結果：(執行需要一點時間...)

```
Summing...
Total value is: 2250066839
```

## 使用 Stopwatch 取得效能數值(花了多少時間)

想知道它實際上花了多少時間來做加總      
使用`Stopwatch`取得效能數值

> `TotalMilliseconds` 是總共用了幾毫秒      
> `TotalMilliseconds / 1000` 是 = 幾秒

```c#
internal class Program
{
    static byte[] values = new byte[500000000];
    static void GenerateInts() 
    {
        var rnd = new Random();
        for (int i = 0; i < values.Length ; i++) {
            values[i] = (byte)rnd.Next(10);
        }
    }
    static void Main(string[] args)
    {
        GenerateInts(); //產生很多整數
        Console.WriteLine("Summing...");

        //使用Stopwatch來看花了多少時間加總
        Stopwatch watch = new Stopwatch();
        watch.Start();//開始計時

        long total = 0;
        for (int i = 0; i < values.Length; i++) {
            total += values[i];
        }
        watch.Stop(); //結束計時
        Console.WriteLine($"Total value is: {total}");
        Console.WriteLine($"Time to sum: {watch.ElapsedMilliseconds/1000} 秒");
        //Console.WriteLine($"Time to sum. {watch.Elapsed}");
    }
}
```

執行結果：      

花了將近17秒

```
Summing...
Total value is: 2250013690
Time to sum: 17 秒
```

##

~~試想，如果我把這些所有數字都放在RAM中、我挪有所有的CUP、把這些工作放在一個執行緒上...~~       
~~如果我聰明的話，應該要將這些數據分開，分成多個執行緒，將數據加起來...~~       
~~天真的假設我有8個CPU，將這些很長的數據分成8部分，再給每個執行緒添加相同數量的陣列，允許多個執行緒在自己一小部分中工作。~~     


[Threading - Divide and Conquer Example Part 1](https://www.youtube.com/watch?v=PmURwqfNTko&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=8&pp=iAQB)