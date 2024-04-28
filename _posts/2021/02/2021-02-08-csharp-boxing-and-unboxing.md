---
layout: post
title: "[C# 筆記] 裝箱與拆箱(Boxing & Unboxing)"
date: 2021-02-08 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,Boxing Unboxing,裝箱 拆箱,Value Type,Reference Type]
---

「實值型別」與「參考型別」其間的轉換動作，便構成了`Boxing`與`Unboxing`。

## 裝箱(Boxing) 

將「實值型別」轉換成「參考型別」的過程。        

「裝箱」是將「值類型（例如 `int`, `char`, `double` 等）」轉換為對應的「參考類型（通常是 `object` 類型或介面類型）」的過程。

```c#
//裝箱：將「值類型」轉換為「Object類型」的過程
int i = 10;
object o = i; //裝箱Boxing發生在這裡
```

`Boxing`處理「實值型別(Value Type)」時，會將型別封裝到`Object`參考型別的執行個體(`Instance`)內。


## 折箱(Unboxing)

將「參考型別」轉換成「實值型別」的過程。        

「拆箱」是將裝箱過的「參考型別」轉換回「原始值型別」的過程。

```c#
//拆箱：從 Object 中提取「值類型」的過程
object o = 10;
int i = (int)o; //折箱Unboxing發生在這裡, 就是把基本數據類型，從物件中打開提取出來，
```

`Unboxing`在處理「參考型別(Reference Type)」時，會從物件中擷取「實值型別」。        
(就是把基本數據類型，從物件中打開提取出來，)



## 注意

不管是「裝箱」還是「拆箱」，都會有比較明顯的性能問題，因為這些操作，都涉及額外的物件創建和銷毀的工作。

- 不管是「裝箱」還是「拆箱」，都會有比較明顯的性能問題
- 因為這些操作，都涉及額外的物件創建和銷毀



[[C# 筆記] 裝箱(Boxing) vs 拆箱(Unboxing) by R](https://riivalin.github.io/posts/2010/01/r-csharp-note-13/)     
[[C# 筆記] 什麼是装箱和拆箱？ by R](https://riivalin.github.io/posts/2017/02/what-is-boxing-and-unboxing/)