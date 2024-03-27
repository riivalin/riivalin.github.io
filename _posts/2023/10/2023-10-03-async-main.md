---
layout: post
title: "[C# 筆記] 將主程式進入點 Main()方法改成 async 非同步"
date: 2023-10-03 23:59:00 +0800
categories: [Notes,C#]
tags: [async,main]
---

## 預設的 Console 應用程式

通常剛建立好一個 Console 應用程式專案，其主程式 Program.cs 的內容如下：

```c#
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Your code here
        }
    }
}
```

## 改成 async 非同步 Main() 方法

加上`async`，`void` 改成 `Task`

```c#
namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Your code here
        }
    }
}
```


[深入理解 C# 7.1 提供的 async 非同步 Main() 方法](https://blog.miniasp.com/post/2019/04/03/Deep-Dive-CSharp-71-async-Main-method)