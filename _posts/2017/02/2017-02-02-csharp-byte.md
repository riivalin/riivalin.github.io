---
layout: post
title: "[C# 筆記] byte b = 'a'; byte c = 1; byte d = 'ab'; byte e = '好'; byte g = 256; 這些變數錯在哪？"
date: 2017-02-02 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,byte]
---


## byte b = 'a';

```c#
byte b = 'a';//編譯錯誤
byte b = (byte)'a'; //97, a的ascii碼的值為97
```

這行程式碼會引發編譯錯誤。雖然字元`'a'`可以隱式地轉換為整數，但是將其賦值給`byte`類型時，需要確保其值在`byte`類型的範圍內（`0` 到 `255`）。字元`'a'`的`ASCII`碼值為`97`，這是一個在`byte`範圍內的值，所以可以修改為 `byte b = (byte)'a';` 來解決問題。

## byte c = 1;

```c#
byte c = 1; //1
```
這是合法的，因為 `1` 在 `byte` 範圍內（`0` 到 `255`）。

## byte d = 'ab';

這是不合法的。因為 `'ab'` 是一個字串，不能直接賦值給一個 `byte` 變數。

## byte e = '好';
這是不合法的。'好' 是一個中文字元，它的 `Unicode` 編碼超出了 `byte` 的範圍。

## byte g = 256;

```c#
byte g = 256; //錯誤。超出byte的範圍。0-255
```

這是不合法的。因為 256 超出了 byte 的範圍。byte 的範圍是 `0` 到 `255`。         
因此不能將 256 賦值給 byte 型別。需要將值修改為在 byte 範圍內的值。


```c#
byte g = 255 + 1; //錯誤。256超出byte的範圍。0-255
byte g = 254 + 1; //255。在 byte 範圍內
```

[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)