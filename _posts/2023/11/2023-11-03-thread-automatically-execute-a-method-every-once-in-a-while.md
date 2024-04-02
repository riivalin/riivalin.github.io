---
layout: post
title: "[C# 筆記] 每隔一段時間自動執行某個方法(使用執行緒)"
date: 2023-11-03 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread,timer,PeriodicTimer]
---

## 方法：.Net6 新定时器PeriodicTimer

在.NET 6中引入了新`Timer`：`System.Threading.PeriodicTimer`，它和之前的`Timer`相比，最大的區別就是新的`PeriodicTimer`事件處理可以方便地使用異步，消除使用`callback`機制減少使用複雜度。

```c#
using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));
while (await timer.WaitForNextTickAsync())
{
    Console.WriteLine(DateTime.UtcNow);
}
```

#### 與Timer的區別

1. 消除了回呼,不再需要綁定事件

2. 不會發生重入，只允許有一個消費者，不允許同一個 `PeriodicTimer` 在不同的地方同時 `WaitForNextTickAsync` ，不需要自己做排他鎖來實現不能重入

3. 非同步化，之前的幾個 `timer` 的 `callback` 都是同步的，使用新的 `timer` 我們可以更好的使用非同步方法，避免寫 `Sync over Async` 之類的程式碼


## 方法一：使用 Timer (System.Threading.Timer)

如果`callback 方法`的執行時間很長，計時器可能(在上個`callback`還沒完成的時候)再次觸發。可以造成多個線程池 線程同時執行你的`callback 方法`。

```c#
public Timer(TimerCallback callback, object? state, int dueTime, int period)
```

參數：
- `Callback`：一個`Object`類型參數的委託，表示要執行的方法。
- `State`：`Callback`委託調用時的參數。也就是要傳入的值(可為空`null`)。
- `dueTime`：定時器延遲多久開始調用(單位：毫秒)。指定零`0`立即啟動計時器。
- `Period`：定時器每隔多久調用一次(單位：毫秒)。


```c#
static void Main()
{
    //建立一個計時器
    Timer timer = new Timer(Method1, null, 0, 2000); //0:立刻開始，2000毫秒(2秒)執行一次
    //Timer timer = new Timer(async (state) => await TimerCallback(), null, 0, 2000);

    //讓timer不能離開當前的作用域
    Console.ReadKey();//可以使窗口停留一下，直到點擊鍵盤任一鍵為止
}

static void Method1(object? obj)
{
    Console.WriteLine($"Method1: {DateTime.Now} - thread ID: {Environment.CurrentManagedThreadId}");
}
```

執行結果：

```
Method1: 2024/4/2 上午 01:19:10 - thread ID: 4
Method1: 2024/4/2 上午 01:19:12 - thread ID: 5
Method1: 2024/4/2 上午 01:19:14 - thread ID: 5
Method1: 2024/4/2 上午 01:19:16 - thread ID: 5
Method1: 2024/4/2 上午 01:19:18 - thread ID: 5
...
```


## 方法二：使用 Thread

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
        Thread.Sleep(2000); //主執行緒暫停2秒
    }
}
```
        
~~`Task.Delay()` 會先放 `Thread` 自由，讓它去處理其他工作，而 `Thread.Sleep()` 則會持續佔用該 `Thread` 直到等待時間結束。~~


# Thread.Join 方法 (System.Threading)

如果想要等待某執行緒的工作執行完畢才繼續處理其他工作，可以呼叫 `Thread` 物件的 `Join` 方法等待執行緒結束。

```c#
static void Main(string[] args)
{
    Thread t1 = new Thread(DoSomething);
    Thread t2 = new Thread(DoSomething);
    Thread t3 = new Thread(DoSomething);

    t1.Start("X");
    t2.Start("Y");
    t3.Start("Z");

    //分別呼叫三個執行緒物件的 Join 方法。
    //這會令主執行緒依序等待 t1、t2、t3 執行完畢之後才繼續跑底下的迴圈
    t1.Join();
    t2.Join();
    t3.Join();

    for (int i = 0; i < 500; i++)
    {
        Console.Write(".");
    }
}
```

[MSDN - Thread.Join 方法(System.Threading)](https://learn.microsoft.com/zh-tw/dotnet/api/system.threading.thread.join?view=net-8.0)     
[c#实现每隔一段时间执行代码（多线程）　３种定时器](https://blog.csdn.net/wuyuander/article/details/76870514)        
[C# 學習筆記：多執行緒 (2) - 分道揚鑣](https://www.huanlintalk.com/2013/05/csharp-notes-multithreading-2.html)      
[新時代 .NET ThreadPool 程式寫法以及為什麼你該用力 async/await](https://blog.darkthread.net/blog/tpl-threadpool-usage/)     
[C#中 System.Threading.Timer 的回收问题](https://www.cnblogs.com/Jeffrey-Chou/p/12366185.html)
[c# 线程定时器 System.Threading.Timer](https://blog.csdn.net/qq_27461747/article/details/105505420)     
[.Net6 新特性 - PeriodicTimer - 异步化的定时器](https://blog.hwj.im/index.php/archives/17/)     
[[C#] .Net6新定时器PeriodicTimer](https://cloud.tencent.com/developer/article/2182393)
