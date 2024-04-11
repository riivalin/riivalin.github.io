---
layout: post
title: "[C# 筆記] 下面這段程式碼求值"
date: 2017-02-25 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,Constructor,構造函數,靜態建構函數]
---

## 下面這段程式碼求值

```c#
class Class1
{
    internal static int count = 0;
    static Class1()
    {
        count++;
    }
    public Class1()
    {
        count++;
    }
}
Class1 o1 = new Class1();
Class1 o2 = new Class1();

// o1.count 的值是 2
// o2.count 的值是 3
```

## o1.count 的值是多少？     

在這個範例中，`count` 是一個靜態欄位，它被所有類別實體共用。在靜態建構子 `static Class1()` 中，`count` 被增加了一次。此外，在每個物件的建構子 `public Class1()` 中，`count` 又被增加了一次。        

因為你建立了兩個`Class1` 物件`o1` 和`o2`，所以靜態建構子`static Class1()` 會在類別的第一個實體被建立時調用，而普通建構子`public Class1() ` 在每個物件建立時都會呼叫。       

因此，在創建 `o1` 的時候，`count` 增加了兩次（一次來自靜態建構函數，一次來自普通建構函數）。而在建立 `o2` 的時候，靜態建構子不會再被調用，只有普通建構子會增加 `count`，所以        

`o1.count` 的值是 `2`, `o2.count` 的值是`3`。     


[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)  