---
layout: post
title: "[C# 筆記] Threading - C# Hello World Thread"
date: 2023-09-01 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---

# C# Hello World Thread

`Main()`是主線程的線程，但目前它只是單線程

```c#
internal class Program
{
    //Main是主線程的線程，但目前來看它只是單線程
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}
```

再寫一個線程...     

(這是一個最基本的線程世界)     

現在就有兩個線程執行的入口點：
1. `Main()`主線程：主入口點
2. `DifferentMethod()`：剛在不同的線程(`Main()`)上建立另一個線程，它的入口點會在 `DifferentMethod()`開始執行

```c#
//第一個線程的入口點：Main是主線程的線程
static void Main(string[] args)
{
    //建立另一個線程
    var thread = new Thread(DifferentMethod);
    thread.Start(); //開始線程
}

//第二個線程的入口點：剛建立的thread，該thread線程的入口點會從這裡開始執行
static void DifferentMethod() {
    Console.WriteLine("Hello, World!");
}
```


[C# Hello World Thread](https://youtu.be/-s8dOv5G8WA?list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5)
