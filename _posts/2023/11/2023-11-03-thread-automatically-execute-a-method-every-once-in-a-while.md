---
layout: post
title: "[C# 筆記] 每隔一段時間自動執行某個方法(使用執行緒)"
date: 2023-11-03 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---

調用線程執行方法，在方法中實現死循環，每個循環`Sleep`設定時間

```c#
static void Main()
{
    Thread thread = new Thread(Method1);
    thread.Start();

    // thread.IsBackground
    // false：若 thread 是前景執行緒，此應用程式不會結束，除非手動將它關閉; (預設)
    // true ：若 thread 是背景執行緒，此應用程式會立刻結束。
}

static void Method1()
{
    while (true)
    {
        Console.WriteLine($"Method1: {DateTime.Now} - thread ID: {Environment.CurrentManagedThreadId}");
        Thread.Sleep(2000); //睡2秒
    }
}
```
        

> `Task.Delay()` 會先放 `Thread` 自由，讓它去處理其他工作       
> 而 `Thread.Sleep()` 則會持續佔用該 `Thread` 直到等待時間結束
