---
layout: post
title: "[C# 筆記] using 關鍵字的作用"
date: 2017-02-22 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,using]
---

- 命名空間引用
- 別名引用
- 資源管理（IDisposable 介面）  


## 命名空間引用:

```c#
// using 用於引入命名空間，以便在程式碼中使用其中定義的類型而不需要使用完全限定的類型名稱。
using System;
namespace MyNamespace
{
  class MyClass
  {
      static void Main()
      {
          Console.WriteLine("Hello, World!");
      }
  }
}
```



## 別名引用:

```c#
// using 也可以用於為類型或命名空間建立別名，以解決命名衝突或簡化類型名稱的使用。
using MyAlias = MyNamespace.MyClass;
namespace AnotherNamespace
{
  class AnotherClass
  {
      MyAlias myObject = new MyAlias();
  }
}
```

## 資源管理（IDisposable 介面）:

```c#
// using 語句也用於資源管理，特別是實作了 IDisposable 介面的類型。
// 在 using 區塊中建立的物件會在區塊結束時自動呼叫 Dispose 方法，以確保資源被正確釋放。
using (MyDisposableObject myObject = new MyDisposableObject())
{
    // 使用 myObject
} // 在這裡，myObject 的 Dispose 方法被調用
```

## 總結

總的來說，`using` 關鍵字用於：**引入命名空間、建立別名和資源管理**。在不同的上下文中，它提供了一種方便和簡潔的方式來管理程式碼中的命名空間、類型和資源。        


[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)  