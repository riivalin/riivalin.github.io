---
layout: post
title: "[C# 筆記] Threading - C# Producer Consumer Thread Implementation Part 2"
date: 2023-09-13 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread,Queue<int>]
---

```c#
internal class Program
{
    static Queue<int> numbers = new Queue<int>(); //Queue<T>:表示物件的先進先出 (FIFO) 集合
    static Random random = new Random(); //產生隨機數的物件
    const int NumThreads = 3; //三個線程
    static int[] sums = new int[NumThreads]; //存放thread各自相加的值

    //產生隨機數器
    static void ProduceNumbers()
    {
        //做10個數字
        for (int i = 0; i < 10; i++)
        {
            int numToEnqueue = random.Next(10); //產生隨機數
            Console.WriteLine($"Prducing thread add {numToEnqueue} to the queue.");
            numbers.Enqueue(numToEnqueue); //將數字加入
            Thread.Sleep(random.Next(1000));
        }
    }

    //數值相加
    static void SumNumbers(object NumThreads)
    {
        //錯誤示範：以下會存在很多的問題
        DateTime startTime = DateTime.Now;
        int sum = 0;

        while ((DateTime.Now - startTime).Seconds < 11) //讓它攪動10秒
        {
            //從陣列中取出數字
            if(numbers.Count != 0) {
                int numToSum = numbers.Dequeue(); //dqqueue取出數字
                sum += numToSum;
                Console.WriteLine($"Consuming thread adding {numToSum} to its total sum making {numToSum} for the thread total.");
            }
        }
        sums[(int)NumThreads] = sum; //放入各自相加的陣列中
    }
    static void Main()
    {
        //建立執行緒去產生隨機數器
        var producingThread = new Thread(ProduceNumbers);
        producingThread.Start();

        //建立多個執行緒去各自加總
        Thread[] threads = new Thread[NumThreads];
        for (int i = 0; i < NumThreads; i++)
        {
            threads[i] = new Thread(SumNumbers);
            threads[i].Start(NumThreads);
        }

        //
        for (int i = 0; i < NumThreads; i++) {
            threads[i].Join();
        }

        //各自相加的總和
        int totalSum = 0;
        for (int i = 0; i < NumThreads; i++) {
            totalSum += sums[i];
        }
        Console.WriteLine($"Done adding. Total is {totalSum}");
    }
}
```

[C# Producer Consumer Thread Implementation Part 1](https://www.youtube.com/watch?v=6XV7o2VsMiI&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=12)       
[C# Producer Consumer Thread Implementation Part 2](https://www.youtube.com/watch?v=JXRSmlCFo1o&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=13) 