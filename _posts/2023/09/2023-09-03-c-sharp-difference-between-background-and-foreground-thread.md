---
layout: post
title: "[C# 筆記] Threading - C# Difference Between Background and Foreground Thread"
date: 2023-09-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread,background-thread,foreground-thread]
---

# C# Difference Between Background and Foreground Thread
## Foreground Thread (前景執行緒)
若主程序已下達中止工作命令了，有任一前景執行緒尚未完成工作，程序不會立即中止，需待前景執行緒完成工作後才會終止。        

在預設的狀況下`Thread`是屬於「前景執行緒」也就是`Thread.IsBackground=false`。       

下面範例，當主執行緒(`Main()`)結束，控制台印出"Leaving Main"後，        
另一個執行緒還會繼續執行，這就是`Foreground Thread` (前景執行緒)。

```c#
internal class Program
{
    //Main是主線程的線程
    static void Main(string[] args)
    {
        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            var thread = new Thread(DifferentMethod); //建立另一個線程
            thread.Start(i); //開始線程
        }
        Console.WriteLine("Leaving Main");
    }

    //另一個線程的開始
    static void DifferentMethod(object? threadID) {
        while (true) {
            Console.WriteLine($"Hello from different method: {threadID}");
        }
    }
}
```

執行結果：      

主執行緒`Main()`已經結束了，另一個執行緒依然續繼執行，      
這就是`Foreground Thread` (前景執行緒)

```
Hello from different method: 1
Leaving Main --> 主執行緒Main()已經結束了，另一個線程依然續繼執行，這就是Foreground Thread (前景執行緒)
Hello from different method: 0
Hello from different method: 1
Hello from different method: 0
Hello from different method: 1
Hello from different method: 0
Hello from different method: 1
Hello from different method: 1
Hello from different method: 0
Hello from different method: 1
Hello from different method: 0
Hello from different method: 0
.... //另一個執行緒 還會繼續執行
```


## Background Thread (背景執行緒)
背景執行緒不管工作有沒有完成，一但收到中止命令，馬上就停下手邊的工作中止工作。      

下面範例，將新建立的執行緒設為「背景執行緒」`thread.IsBackground = true;`，     
按`F5`執行...可以看到        

當主執行緒(`Main()`)已經結束，在控制台印出離開主線程"Leaving Main"後， 
另一個執行緒立即結束。      

也就是說，一旦離開主執行緒`Main()`後，所有其他的執行緒都被關閉。        

```c#
internal class Program
{
    //Main是主線程的線程
    static void Main(string[] args)
    {
        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            var thread = new Thread(DifferentMethod); //建立另一個線程
            thread.IsBackground = true; //設為背景執行緒
            thread.Start(i); //開始線程
        }
        Console.WriteLine("Leaving Main");
    }

    //另一個線程的開始
    static void DifferentMethod(object? threadID) {
        while (true) {
            Console.WriteLine($"Hello from different method: {threadID}");
        }
    }
}
```


執行結果：      

主執行緒`Main()`已結束，另一個執行緒也會立即關閉結束，不再執行。        
這就是`Background Thread` (背景執行緒)      

```
Hello from different method: 0
Hello from different method: 0
Hello from different method: 1
Leaving Main ==> 主執行緒Main()已結束，另一個執行緒也會立即關閉結束
Hello from different method: 0
Hello from different method: 1
```

## Thread.CurrentThread.ManagedThreadId

可以使用 `Thread.CurrentThread.ManagedThreadId`，       
顯示出當時正在執行的受管理的執行緒ID代碼，讓你清楚現在的是使用哪個執行緒來執行

```c#
internal class Program
{
    //Main是主線程
    static void Main(string[] args)
    {
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            var thread = new Thread(DifferentMethod); //建立另一個線程
            thread.IsBackground = true; //設為背景執行緒
            thread.Start(i); //開始線程
        }
        Console.WriteLine("Leaving Main");
    }

    //另一個線程的開始
    static void DifferentMethod(object? threadID) {
        while (true) {
            Console.WriteLine($"Hello from different method: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
```

[C# Difference Between Background and Foreground Thread](https://www.youtube.com/watch?v=IVci1IvHThU&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=3&pp=iAQB)     
[[C#.NET][Thread] 背景執行緒與前景執行緒的差別](https://dotblogs.com.tw/yc421206/2011/01/04/20574)      
[[C# 筆記] Threading - C# Hello World Thread](https://riivalin.github.io/posts/2023/09/c-sharp-hello-world-thread/)     
[[C# 筆記] Threading - C# Multiple Threads](https://riivalin.github.io/posts/2023/09/c-sharp-multiple-threads/)     
