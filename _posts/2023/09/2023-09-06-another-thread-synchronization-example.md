---
layout: post
title: "[C# 筆記] Threading - C# Another Thread Synchronization Example"
date: 2023-09-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread,lock]
---

# C# Another Thread Synchronization Example

[繼上一個範例](https://riivalin.github.io/posts/2023/09/basic-thread-synchronization/)...       

模擬上wc的情境...

```c#
internal class Program
{
    static object baton = new object(); //為lock建立一個接力棒的變數，用它來協調誰可以有使用權
    static Random random = new Random(); //讓線程共享的數據
    static void Main(string[] args)
    {
        //建立5個執行緒來執行UseRestroomStall()方法
        for (int i = 0; i < 5; i++)
        {
            new Thread(UseRestroomStall).Start();
        }
    }

    static void UseRestroomStall()
    {
        //排隊上wc
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Trying to obtain the bathroom stall...");

        //使用lock來協調 誰可以有使用權
        lock (baton) 
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} obtained the lock. Doing my business...");
            Thread.Sleep(random.Next(2000)); //讓主執行緒睡，睡的最大隨機數時間為2秒
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} leaving the stall.");
        }
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} is leaving the restaurant.");
    }
}
```

執行結果：

```
9 Trying to obtain the bathroom stall...
9 obtained the lock. Doing my business...
11 Trying to obtain the bathroom stall...
12 Trying to obtain the bathroom stall...
10 Trying to obtain the bathroom stall...
13 Trying to obtain the bathroom stall...
9 leaving the stall.
10 obtained the lock. Doing my business...
9 is leaving the restaurant.
10 leaving the stall.
11 obtained the lock. Doing my business...
10 is leaving the restaurant.
11 leaving the stall.
12 obtained the lock. Doing my business...
11 is leaving the restaurant.
12 leaving the stall.
12 is leaving the restaurant.
13 obtained the lock. Doing my business...
13 leaving the stall.
13 is leaving the restaurant.
```