---
layout: post
title: "[C# 筆記] Threading - C# Multiple Threads"
date: 2023-09-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---

再把[程式修改一下](https://riivalin.github.io/posts/2023/09/c-sharp-hello-world-thread/)

# C# Multiple Threads
## thread.Start()

按下 `F5`執行，可以看到 Console控制台輸出是：
兩個同時執行的線程混合交叉的顯示    
`Hello from Main()`和`Hello from DifferentMethod()`。

```c#
internal class Program
{
    //Main是主線程的線程
    static void Main(string[] args)
    {
        var thread = new Thread(DifferentMethod); //建立另一個線程
        thread.Start(); //開始線程

        while (true) {
            Console.WriteLine("Hello from Main()");
        }
    }

    //另一個線程的開始
    static void DifferentMethod( ) {
        while (true) {
            Console.WriteLine("Hello from DifferentMethod()");
        }
    }
}
```

執行結果：(按`Ctrl+C`結束)

```console
Hello from Main()
Hello from DifferentMethod()
Hello from Main()
Hello from Main()
Hello from DifferentMethod()
Hello from DifferentMethod()
Hello from Main()
Hello from DifferentMethod()

```

## 參數化線程 thread.Start(object? parameter)

讓輸出變得更有趣，使用參數化線程...        

按下 `F5`執行，可以看到 Console控制台內容：     
輸出的數字在變化，事實上，這些線程是同時在執行的，只是它們在爭奪控制台視窗以將其輸出印出。

```c#
internal class Program
{
    //Main是主線程的線程
    static void Main(string[] args)
    {
        for (int i = 0; i < 8; i++)
        {
            var thread = new Thread(DifferentMethod); //建立另一個線程
            thread.Start(i); //開始線程
        }
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

```console
Hello from different method: 4
Hello from different method: 2
Hello from different method: 0
Hello from different method: 5
Hello from different method: 3
Hello from different method: 3
Hello from different method: 3
Hello from different method: 7
Hello from different method: 7
Hello from different method: 2
Hello from different method: 2
Hello from different method: 5
Hello from different method: 5
Hello from different method: 6
Hello from different method: 4
Hello from different method: 4
Hello from different method: 4
Hello from different method: 0
Hello from different method: 2
```

## 結論

所以`Thread`執行緒(線程)主要的優點是：      
如果你可以將工作分解成邏輯塊，在其後立即發送一堆線程，你會發現可以更快得完成更多的工作。


[C# Multiple Threads](https://youtu.be/_HO86JjtB2c)     
[[C# 筆記] Threading - C# Hello World Thread](https://riivalin.github.io/posts/2023/09/c-sharp-hello-world-thread/)