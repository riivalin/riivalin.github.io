---
layout: post
title: "[C# 筆記] new 關鍵字的作用?"
date: 2017-02-08 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,new]
---


## 實體化物件:

使用 `new` 關鍵字可以建立一個類別的實體，也就是物件。透過呼叫類別的建構子來初始化對象，並傳回對新建立對象的參考。

```c#
MyClass myObject = new MyClass();
```

> 對於已知類型的情況下，直接打上 `new()` 來使用建構式建立對象。     
> ```c#
> Test test = new() { str = "a" };
> ```

## 方法的重寫（Override）:

在衍生類別中，`new` 關鍵字可以用來隱藏基底類別中的成員，特別是在衍生類別中重新定義一個與基底類別中的成員同名的成員。這被稱為方法的重寫或隱藏。

```c#
class BaseClass
{
  public void Display()
  {
      Console.WriteLine("BaseClass Display");
  }
}
class DerivedClass : BaseClass
{
  public new void Display()
  {
      Console.WriteLine("DerivedClass Display");
  }
}
```

## 隱藏欄位或屬性:

在衍生類別中使用 `new` 關鍵字也可以隱藏基底類別中的欄位或屬性。這樣，在衍生類別中可以定義一個與基底類別中同名但不同類型的欄位或屬性。

```c#
class BaseClass
{
  public int Number = 12;
}
class DerivedClass : BaseClass
{
  public new string Number = "Hello";
}
```

## 在泛型類型中的實體化:

在泛型類型中，`new` 關鍵字用於建立具體類型的實例。在泛型類型參數需要具體類型時，可以使用 `new` 來建立實體。

```c#
public class MyGenericClass<T> where T : new()
{
  public T CreateInstance()
  {
      return new T();
  }
}
```

## 總結

總的來說，`new` 關鍵字在C#中用於建立物件、方法的重寫、欄位或屬性的隱藏以及在泛型類型中實例化。其具體行為取決於它在程式碼中的上下文。

[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)      
[[C# 筆記] new 關鍵字](https://riivalin.github.io/posts/2011/01/new/)