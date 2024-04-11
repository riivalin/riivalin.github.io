---
layout: post
title: "[C# 筆記] C# 可否對記憶體直接操作？"
date: 2017-02-19 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法]
---


C# 在`unsafe`模式下可以使用指針對記憶體進行操作, 但在託管模式下不可以使用指針，C# NET預設不運行帶指針的，需要設定下，選擇專案右鍵 -> 屬性 -> 建置 -> 一般 ->「不安全程式碼-允許適應`unsafe`」關鍵字編譯的程式碼，打勾 -> 儲存。       

```c#
class Program
{
  static unsafe void Main()
  {
      int value = 42;
      // 使用 unsafe 关键字和指针来直接修改内存
      int* pointer = &value;
      *pointer = 99;
      Console.WriteLine(value); // 输出: 99
  }
}
```

需要注意的是，使用指標直接操作記憶體時存在一些潛在的風險和安全性問題。因此，除非在特殊情況下確實需要對記憶體進行底層操作，否則應避免使用 `unsafe` 關鍵字。對於普通的應用程式和開發任務，建議使用C#的高階特性(垃圾回收機制和類型安全)和標準函式庫來進行記憶體管理，以確保程式碼的安全性和可維護性。      


[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)  