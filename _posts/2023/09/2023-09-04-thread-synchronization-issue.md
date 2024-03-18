---
layout: post
title: "[C# 筆記] Threading - C# Thread Synchronization Issue"
date: 2023-09-04 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---


# C# Thread Synchronization Issue
`Thread` 看起來是同步，但事實上是在相互搶資源...

## 前置準備：讓三個執行緒可以很乾淨的遞增

寫一個範例，讓三個執行緒可以很乾淨的遞增...     

現在總共有三個執行緒，一個是主執行緒`Main()`，另外兩個是新建立的`thread1`、`thread2`執行緒。        

```c#
internal class Program
{
    static int count = 0;
    static void Main(string[] args)
    {
        //建立兩個執行緒，讓它們都進入IncrementCount方法
        var thread1 = new Thread(IncrementCount);
        var thread2 = new Thread(IncrementCount);

        thread1.Start();
        Thread.Sleep(500); //讓主執行緒不同步半秒
        thread2.Start();
    }

    static void IncrementCount()
    {
        while (true)
        {
            count++;
            //做一個有趣的輸出
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} incremented count to: {count}");
            Thread.Sleep(1000); //讓執行緒睡眠1000毫秒(讓程式暫停一秒)
        }
    }
}
```

執行結果：      

控制台輸出 託管的 Thread ID 和很乾淨的 Count 遞增。     

```console
Thread ID: 10 incremented count to: 1
Thread ID: 11 incremented count to: 2
Thread ID: 11 incremented count to: 3
Thread ID: 10 incremented count to: 4
Thread ID: 10 incremented count to: 5
Thread ID: 11 incremented count to: 6
Thread ID: 11 incremented count to: 7
Thread ID: 10 incremented count to: 8
Thread ID: 11 incremented count to: 9
Thread ID: 10 incremented count to: 10
Thread ID: 11 incremented count to: 11
Thread ID: 10 incremented count to: 12
Thread ID: 11 incremented count to: 13
Thread ID: 10 incremented count to: 14
Thread ID: 11 incremented count to: 15
Thread ID: 10 incremented count to: 16
Thread ID: 11 incremented count to: 17
```


## 模擬互相爭奪搶資源的情境

再改一下程式，可以看出他們互相爭奪搶資源的情境...    

`count++`看起來像是一個操作，但事實上不是，其實它背後做了很多事，會執行很多步驟。     
count++....count=count+1        

修改一下，再讓主執行緒大量的睡一下，把差距拉大...      
(一秒和半秒對計算機來說，是很長的時間...)

```c#
internal class Program
{
    static int count = 0;
    static void Main(string[] args)
    {
        //建立兩個執行緒，讓它們都進入IncrementCount方法
        var thread1 = new Thread(IncrementCount);
        var thread2 = new Thread(IncrementCount);

        thread1.Start();
        Thread.Sleep(500); //讓主執行緒不同步半秒
        thread2.Start();
    }

    static void IncrementCount()
    {
        while (true)
        {
            int temp = count;
            Thread.Sleep(500); //讓主執行緒睡半秒
            count = temp + 1;

            //做一個有趣的輸出
            Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} incremented count to: {count}");
            Thread.Sleep(1000); //讓執行緒睡眠1000毫秒(讓程式暫停一秒)
        }
    }
}
```


執行結果：      

會發現控制台有非常糟糕的輸出，count不再井然有序，會出現重複的 count值。

```
Thread ID: 9 incremented count to: 1
Thread ID: 10 incremented count to: 2
Thread ID: 9 incremented count to: 3
Thread ID: 10 incremented count to: 4
Thread ID: 9 incremented count to: 5
Thread ID: 10 incremented count to: 5
Thread ID: 9 incremented count to: 6
Thread ID: 10 incremented count to: 6
Thread ID: 9 incremented count to: 7
Thread ID: 10 incremented count to: 8
Thread ID: 9 incremented count to: 9
Thread ID: 10 incremented count to: 9
Thread ID: 9 incremented count to: 10
Thread ID: 10 incremented count to: 10
Thread ID: 9 incremented count to: 11
Thread ID: 10 incremented count to: 11
```


[C# Thread Synchronization Issue](https://www.youtube.com/watch?v=DsHV2BY1lgQ&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=4&pp=iAQB)