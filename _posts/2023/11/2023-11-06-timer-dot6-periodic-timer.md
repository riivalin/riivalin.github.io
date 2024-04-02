---
layout: post
title: "[C# 筆記].Net6 新定时器 PeriodicTimer (異步化的定時器)"
date: 2023-11-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,timer,PeriodicTimer,async/await]
---


在.NET 6中引入了新`Timer`：`System.Threading.PeriodicTimer`，它和之前的`Timer`相比，最大的區別就是新的`PeriodicTimer`事件處理可以方便地使用異步，消除使用`callback`機制減少使用複雜度。

```c#
using PeriodicTimer timer = new(TimeSpan.FromSeconds(2));
while (await timer.WaitForNextTickAsync())
{
    Console.WriteLine(DateTime.UtcNow);
}
```

## 與Timer的區別

1. 消除了回呼,不再需要綁定事件

2. 不會發生重入，只允許有一個消費者，不允許同一個 `PeriodicTimer` 在不同的地方同時 `WaitForNextTickAsync` ，不需要自己做排他鎖來實現不能重入

3. 非同步化，之前的幾個 `timer` 的 `callback` 都是同步的，使用新的 `timer` 我們可以更好的使用非同步方法，避免寫 `Sync over Async` 之類的程式碼


[.Net6 新特性 - PeriodicTimer - 异步化的定时器](https://blog.hwj.im/index.php/archives/17/)     
[[C#] .Net6新定时器PeriodicTimer](https://cloud.tencent.com/developer/article/2182393)