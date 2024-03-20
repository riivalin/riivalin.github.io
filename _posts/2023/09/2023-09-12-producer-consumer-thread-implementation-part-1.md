---
layout: post
title: "[C# 筆記] Threading - C# Producer Consumer Thread Implementation Part 1"
date: 2023-09-12 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread,Queue<int>]
---



#
有一個產生隨機數的box，隨意吐出任意數，每個線程會都抓取吐出來的數字各自做相加，再把每個線程單獨所相加的值做加總，但不能讓這些線程抓到同一個數字，不然會發生最後加總結果是錯誤的。       

```c#
internal class Program
{
    static Queue<int> numbers = new Queue<int>(); //Queue<T>:表示物件的先進先出 (FIFO) 集合
    static Random random = new Random(); //產生隨機數的物件
    const int NumThreads = 3; //三個線程
    static int[] sums = new int[NumThreads]; //存放thread各自相加的值

    static void ProduceNumbers()
    {
        //因為線程速度很快，所以放慢速度來看, 做25個數字
        for (int i = 0; i < 25; i++)
        {
            numbers.Enqueue(random.Next(10)); //將數字加入
            Thread.Sleep(random.Next(1000));
        }
    }

    //數值相加
    static void SumNumbers(object NumThreads) 
    {
    }
    static void Main() 
    { 
    }
}
```

## 錯誤示範的寫法：數值相加

```c#
//數值相加
static void SumNumbers(object NumThreads)
{
    //錯誤示範：以下會存在很多的問題
    DateTime startTime = DateTime.Now;
    int sum = 0;
    while ((DateTime.Now - startTime).Seconds < 10) //讓它攪動10秒
    {
        //從陣列中取出數字
        if(numbers.Count != 0)) {
            sum += numbers.Dequeue(); //dqqueue取出數字
        }
    }
    sums[(int)NumThreads] = sum; //放入各自相加的陣列中
}
```

[C# Producer Consumer Thread Implementation Part 1](https://www.youtube.com/watch?v=6XV7o2VsMiI&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=12)       
[C# Producer Consumer Thread Implementation Part 2](https://www.youtube.com/watch?v=JXRSmlCFo1o&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=13)       
[MSDN - Queue<T> 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.generic.queue-1?view=net-7.0)