---
layout: post
title: "[C# 筆記] 靜態成員和非靜態成員的區別"
date: 2017-02-18 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,static]
---

## 實體化:

- 靜態成員屬於類別，而不屬於類別的實體。可以透過類別名稱直接存取靜態成員，而不需要建立類別的實體。
- 非靜態成員屬於類別的實體。要存取非靜態成員，需要先建立類別的實體，然後透過實體來存取成員。


> 實體(實例、`instance`)

## 記憶體分配:

- 靜態成員在程式啟動時就分配內存，並在程式結束時釋放。它們的生命週期與應用程式的生命週期相同。
- 非靜態成員在創建類別的實體時分配內存，並在實體被銷毀時釋放。它們的生命週期與實體的生命週期相同。

## 訪問方式:

- 靜態成員可以透過類別名稱直接訪問，也可以透過實體訪問。但強烈建議使用類別名稱來存取靜態成員，以明確它們的靜態性。
- 非靜態成員只能透過實體存取。


## this 關鍵字:

- 靜態成員中不能使用 `this` 關鍵字，因為它們不屬於實體。
- 非靜態成員中可以使用 `this` 關鍵字來引用目前實體。
 

## 使用場景:

- 靜態成員通常用於表示與整個類別相關的資料或功能，例如共用的計數器、工廠方法等。
- 非靜態成員通常用來表示實體特有的資料或功能，每個實體都有獨立的值。


## 範例

```c#
public class Test
{
    public static int staticNumber = 10; //靜態成員
    public int InstanceNumber = 20; //非靜態成員

    //靜態方法
    public static void StaticMethod()
    { 
        // 靜態方法
        // 不能使用 this 關键字
        Console.WriteLine("Static Method:");
    }

    //非靜態方法
    public void InstanceMethod()
    {
        // 非靜態方法
        // 可以使用 this 關键字
        Console.WriteLine($"Instance Method: {this.InstanceNumber}");
    }
}
```

## 總結

總的來說，靜態成員與類別關聯，非靜態成員與類別的實體關聯。選擇使用靜態或非靜態成員取決於成員的用途和「資料的共享」需求。        



[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)     
[[C# 筆記] 靜態和非靜態的區別 By R](https://riivalin.github.io/posts/2011/03/static/)