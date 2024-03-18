---
layout: post
title: "[C# 筆記] Threading - C# lock to Monitor Method Calls"
date: 2023-09-07 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread,lock,monitor,monitor.enter,monitor.exit]
---

# C# lock to Monitor Method Calls
直接結論：      
`lock` 是一個語法糖，它將 `Monitor`物件進行包裝。       

`lock`最終產生的是：`Monitor.Enter`和`Monitor.Exit`方法，並且包含了`try`和`catch`。保證在崩潰的時候執行到`finally`中的`Monitor:Exit`方法，進行鎖的釋放。        

所以`lock`鎖定的方式，要比`Monitor.Enter`和`Monitor.Exit`安全一些。


## Monitor.Enter 方法

- `Monitor.Enter(object)`獲取鎖
- `Monitor.Exit(object)`釋放鎖

將 `lock` 改成`Monitor.Enter` 方法       

```c#
static object baton = new object(); //接力棒：獲得使用權的變數

static void Main(string[] args)
{
    //lock (baton) 
    Monitor.Enter(baton); //獲取鎖
    try
    {
        Console.WriteLine("Got to baton");
    } finally //finally無論如何都會執行
    {
        Monitor.Exit(baton); //釋放鎖
    }
}
```


[C# lock to Monitor Method Calls](https://www.youtube.com/watch?v=cQadZLH8NkI&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=7&pp=iAQB)      
[C# Monitor和Lock的定义及区别](https://blog.csdn.net/qq_30725967/article/details/126242520?spm=1001.2101.3001.6661.1&utm_medium=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7EPayColumn-1-126242520-blog-119170818.235%5Ev43%5Epc_blog_bottom_relevance_base8&depth_1-utm_source=distribute.pc_relevant_t0.none-task-blog-2%7Edefault%7ECTRLIST%7EPayColumn-1-126242520-blog-119170818.235%5Ev43%5Epc_blog_bottom_relevance_base8&utm_relevant_index=1)      
[C#中的lock和Monitor.Enter和Monitor.Exit](https://blog.csdn.net/wodownload2/article/details/119170818)      