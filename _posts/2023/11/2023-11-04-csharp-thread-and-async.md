---
layout: post
title: "[C# 筆記] 多執行緒(Thread)與非同步(async/await)"
date: 2023-11-04 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,task,thread,ThreadPool,async/await]
---


# 基本概念
## 多執行緒
執行緒可以分工加速，但多建立一條執行緒，就會消耗約 1 MB 的記憶體來配置

### 前景執行緒(前台線程)
- 程式必須執行完前景執行緒才會結束退出
- `Thread` 預設建立前景執行緒

> ~~比如：「資料的拷貝」可以使用前景執行緒~~

### 背景執行緒 (後台線程)
- 背景執行緒在程式退出時就會結束
- 背景執行緒結束時，程式並不會結束退出
- `ThreadPool`、`Task`、`Parallel` 預設建立背景執行緒

## 非同步

- 非同步目的在於增加產能而非提高效能
    - 當程式在等待時，可以先去執行另一項程式，不浪費等待時間

- 非同步不等於多執行緒
    - 多執行緒的精神為分工加速，建立多個執行緒個別處理
    - 非同步的重點在於允許執行緒在等待時間先處理其他工作

- 非同步對 I/O 相關工作較有效
    - I/O 相關工作例如 : 呼叫 API、存取資料庫
    - 要等待 I/O 回應的等待時間，可以先處理其他工作
    - 而大量消耗 CPU 的重度運算，並無等待時間，使用多執行緒較有效率


# 四種建立執行緒的方法

歷史演變由上而下
- Thread
- ThreadPool
- Task
- Parallel

> TPL ( Task Parallel Library ) 是 .NET 4.0 中增加的平行運算函式庫，其中包含了 Task 類別與 Parallel 類別

## Thread

使用 `System.Threading.Thread` 類別

### 執行不帶參數的方法

開啟執行緒

```c#
Thread thread = new Thread(new ThreadStart(Method)); //建立一條執行緒去執行 Method方法
thread.Start(); //並不是馬上執行，而是準備好被 CPU 執行，甚麼時候執行視情況而定
```

Method 方法

```c#
public void Method()
{
    Thread.Sleep(1000); //睡一秒。單位為毫秒
    Console.WriteLine("thread done");
}
```

### 執行帶參數的方法

開啟執行緒

```c#
Thread thread = new Thread(new ParameterizedThreadStart(Method)); //建立一條執行緒去執行 Method方法
thread.Start("thread done"); //將字串帶入Method方法
```

Method 方法

```c#
public void Method(string str)
{
    Thread.Sleep(1000);
    Console.WriteLine(str);
}
```

## ThreadPool
- 使用 `System.Threading.ThreadPool` 類別     
- 為 `Thread` 的升級版

### 執行不帶參數的方法

```c#
ThreadPool.QueueUserWorkItem(new WaitCallback(x => Console.WriteLine("thread done")));
```

### 執行帶參數的方法

```c#
ThreadPool.QueueUserWorkItem(new WaitCallback(x => Console.WriteLine(x)), "thread done");
```

### 與 Thread 的差別 :
- 建立執行緒
    - `Thread` : 每次都建立一個新的執行緒
    - `ThreadPool` : 會查看執行緒池，若無空閒的執行緒才建立，剛開始執行緒池是沒有執行緒的，當 `ThreadPool` 建立一個執行緒後，此執行緒才會加入執行緒池

- 操控執行緒的狀態
    - `ThreadPool` 可以操控執行緒的狀態，例如 : 等待執行緒完成、中止超時的執行緒等，`Thread` 則不行

## Task

- 使用 `System.Threading.Tasks.Task` 類別
- 與 `ThreadPool` 是一樣使用執行緒池的

### 兩種建立方式
#### 使用 Task 的 Run 方法

```c#
Task.Run(()=> {
    Console.WriteLine("thread done");
});
```
#### 使用 TaskFactory 物件的 StartNew 方法

```c#
(new TaskFactory()).StartNew(() =>
{
    Console.WriteLine("thread done");
});
```

### 方法有返回值

```c#
Task<string> task = Task.Run<string>(() => {
    return "thread done";
});

Console.WriteLine(task.Result);
```

### 取消超時執行緒

```c#
//1秒後自動取消執行緒
CancellationTokenSource cts = new CancellationTokenSource(1000);

cts.Token.Register(()=> {
    Console.WriteLine("thread cancle");
});

Task.Run(()=> {
    Console.WriteLine("thread start");
    Thread.Sleep(2000);

    if (cts.Token.IsCancellationRequested) {
        Console.WriteLine("thread stop");
        return;
    }

    Console.WriteLine("thread done");
}, cts.Token);
```

### 等待所有執行緒執行完畢

```c#
Task.WaitAll(Task1, Task2, Task3);
```

### 等待任意一個執行緒執行完畢

```c#
Task.WaitAny(Task1, Task2, Task3);
```

- 與 `ThreadPool` 差別在於 `Task` 在多核 `CPU` 時效能較優，因為 `ThreadPool` 使用的執行緒池是全域，會造成資源共享的競爭，且 `Task` 提供較豐富的 `API` 方法



## Parallel

- 使用 `System.Threading.Tasks.Parallel` 類別

### Parallel.Invoke()

```c#
Action[] action = new Action[] {
    ()=>Console.WriteLine($"thread：{Thread.CurrentThread.ManagedThreadId}"),
    ()=>Console.WriteLine($"thread：{Thread.CurrentThread.ManagedThreadId}"),
    ()=>Console.WriteLine($"thread：{Thread.CurrentThread.ManagedThreadId}"),
};
Parallel.Invoke(action);
```

### Parallel.For()

```c#
ParallelLoopResult plr = Parallel.For(1, 10, (i) =>
{
    Console.WriteLine($"thread：{Thread.CurrentThread.ManagedThreadId}");
});
Console.WriteLine(plr.IsCompleted);
```

### Parallel.ForEach()

```c#
Parallel.ForEach<String>(new List<String>() {
    "a","b","c","d","e","f","g","h","i"
}, (str) =>
{
    Console.WriteLine(str);
});
```

與 Task 差別在於執行緒的數量控制較為簡單

# 使用非同步的方法
## async/await
### 為非同步的修飾詞
- `async` `用來修飾方法，await` 用來呼叫方法
- `await` 必須出現在有 `async` 修飾的方法中
- `await` 呼叫的方法可以不用 `async` 修飾，但返回值必須為 `Task<T>` 型別

### async 像病毒一樣會傳染
- 當方法前加上 `async` 後，裡面若呼叫外部方法必須加上 `await`
- 此設計是為了避免同步與非同步寫法混用

### 將同步程式重構為非同步
- 建議採由下而上 ( Bottom-Up ) 的策略
- 若由上開始，所有呼叫到的方法都必須更改

[筆記 - C# 多執行緒與非同步](https://kenny88881234.gitlab.io/2020/12/05/Csharp-thread-and-Async/#筆記-C-多執行緒與非同步)     
[C#：前台线程后台线程](https://blog.51cto.com/u_15057841/3532516)  
