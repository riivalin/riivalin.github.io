---
layout: post
title: "[C# 筆記] Strings = new String(“xyz”); 建立了幾個 String Object？"
date: 2017-02-17 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,string]
---


這段程式碼實際上會創建一個字串對象，其中每個字元都是從提供的字串中複製的，但是因為 `string` 對象本身是不可變的，所以這樣的使用方式並不常見。        

通常，我們直接使用字串字面量或透過其他方法建立字串，而不需要使用 `new string `建構函數。例如：

```c#
string s = "xyz"; //使用字串
```

或者如果你有字元數組，可以使用：

```c#
char[] arr = {'x','y','z'};
string s = new String(arr);
```

[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)