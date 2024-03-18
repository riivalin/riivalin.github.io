---
layout: post
title: "[C# 筆記] Threading - C# Basic Thread Synchronization (Lock)"
date: 2023-09-05 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread,lock]
---

# C# Basic Thread Synchronization

[上一個範例可以看出](https://riivalin.github.io/posts/2023/09/thread-synchronization-issue/)，當主執行緒睡半秒啟動後，thread2 正在處理舊的數據，那麼該如何解決這個問題呢？讓它們可以同步一點呢？        

可以想想賽跑時，會有接力棒，一個傳一個賽跑者...     

我們可以宣告一個類似接力棒的變數`baton`，當執行緒進入危險區域時，它必須要有接力棒，     
程式碼中，什麼是「危險區域」？ 「危險區域」是當我有「讀」、「寫」，特別是對這些「共享數據」...      

像[上一個範例中](https://riivalin.github.io/posts/2023/09/thread-synchronization-issue/)的「共享數據」，就是靜態變數 `count`。      

## 使用 Lock

解決執行緒同步搶資源的最基本同步的方法，就是使用`lock`。

```c#
lock (x) 
{
    // Your code...
}
```

> lock 陳述式- 同步處理執行緒對共用資源的存取

```c#
internal class Program
{
    static int count = 0; //變數是靜態static，代表執行緒共享同一塊記憶體
    static object baton = new object(); //接力棒

    static void Main(string[] args)
    {
        //建立兩個執行緒，讓它們都進入IncrementCount方法
        var thread1 = new Thread(IncrementCount);
        var thread2 = new Thread(IncrementCount);

        thread1.Start();
        Thread.Sleep(500); //讓主執行緒不同步半秒
        thread2.Start(); //當主執行緒睡半秒啟動後，thread2正在處理舊的數據
    }

    static void IncrementCount()
    {
        while (true)
        {
            //使用lock，在搶資源的同時，讓執行緒儘可能的同步
            lock (baton) 
            {
                int temp = count;
                Thread.Sleep(500); //讓主執行緒睡半秒
                count = temp + 1;

                //做一個有趣的輸出
                Console.WriteLine($"Thread ID: {Thread.CurrentThread.ManagedThreadId} incremented count to: {count}");
            }

            Thread.Sleep(1000); //讓執行緒睡眠1000毫秒(讓程式暫停一秒)
        }
    }
}
```

執行結果：      

會發現控制台輸出的結果會變很慢，但是它的count 是井然有序的。

```
Thread ID: 10 incremented count to: 1
Thread ID: 11 incremented count to: 2
Thread ID: 10 incremented count to: 3
Thread ID: 11 incremented count to: 4
Thread ID: 10 incremented count to: 5
Thread ID: 11 incremented count to: 6
Thread ID: 10 incremented count to: 7
Thread ID: 11 incremented count to: 8
Thread ID: 10 incremented count to: 9
Thread ID: 11 incremented count to: 10
Thread ID: 10 incremented count to: 11
Thread ID: 11 incremented count to: 12
```

##

像買票：看電影、飛機票、車票...等，如果是特殊假日，一定會有很多人競爭這個門票、位置，       
不會在已經進入結帳時，進行下一步，卻發生該票已經被買走了...     
它背後有可能的機制，可能會為這張票保留個10分鐘，你可以成功購買這張票，如果你沒有買，票就會供其他人購買了。      

在爭奪這個共享數據，我們需要進行一些協調，確保沒有兩個人同時購買到同一張票，        


[C# Basic Thread Synchronization](https://www.youtube.com/watch?v=Q1sRKlzsXTE&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=5&pp=iAQB)