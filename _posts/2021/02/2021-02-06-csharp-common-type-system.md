---
layout: post
title: "[C# 筆記] 共通型別系統(Common Type System)"
date: 2021-02-06 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,CLR,CTS,CLS]
---

[![](https://dotnettrickscloud.blob.core.windows.net/img/netframework/cts-cls.png)](https://dotnettrickscloud.blob.core.windows.net/img/netframework/cts-cls.png)       

- 通用語言執行平台（`Common Language Runtime`, `CLR`）：用來管理執行中的`.NET`程序
- 共通型別規範（`Common Type System`, `CTS`）：定義了所有「資料型別(Data Types)」
- 共通語言規範（`Common Language Specification`, `CLS`）：用來描述`.Net`平台上各種不同語言(包含：`C#`、`VB`、`C++`、`J#`...)

# CTS（Common Type System）

「共通型別系統(Common Type System)」定義了所有資料型別，主要分為二大類包括：
- 實值型別(`Value Type`)
- 參考型別(`Reference Type`)        

在`CTS`中的所有資料型別都是繼承於`System.Object`基底(`Base Type`)所衍生出來的。          


## CTS 架構圖

```
物件 Object
    - 實值型別 Value Type
        - 內建實值型別：Int16, Int32, Double, Boolean, Byte, Char...
        - 使用者自訂實型別
        - 列舉型別 Enumeration
    - 參考型別 Reference Type
        - 類別 Class
        - 介面 Interface
        - 字串 String
        - 陣列 Array
        - 委派 Delegate
        - 指標 Pointer
```


# Q&A

Q：為什麼我們需要瞭解共通型別系統(Common Type System,`CTS`)？     

A：在.Net Framework 底下許多程式語言如：C#、VB.Net 所宣告的資料型別都會對應到`CTS`的基底資料型別，因此達成語言間底層資料型別的互通性。      

而對於`CTS`主要二大分類：「實值型別」和「參考型別」其特性如同參數傳遞方式`Call by value`與`Call by reference`一樣。瞭解了底層運作的概念有助於我們日後程式除錯的能力。       

`System`命名空間是 .Net Framework 中基礎型別的根(`Root`)命名空間，我們可以把`Object`看成是應用程式使用的基底資料型別的根節點，尚包含`Array`、`Byte`、`Char`、`Int32`、`String`等等，這些型別將對應到程式語言中的原始資料型別。      

所以當使用 .Net 平台上某種語言的型別宣告來撰寫程式碼時，都會對應到 .Net Framework 的基底資料型別。


# CLR、CTS與CLS三者關係

```
通用語言執行平台 Common Language Runtime, CLR
    - 共通型別規範 Common Type System, CTS
        - 共通語言規範 Common Language Specification, CLS
            C#, VB, C++, J#
```

## 通用語言執行平台 CLR（Common Language Runtime）

`CLR` 負責執行和管理`.NET`程式。 

- CLR 是.NET平台的執行環境，負責管理和執行.NET程式。
- 它提供了許多關鍵的服務，包括記憶體管理、垃圾回收、執行緒管理、安全性、程式碼存取安全性等。 
- CLR 也負責將中間語言（IL，Intermediate Language）編譯成本地機器程式碼，並執行程式集中的方法。 
- CLR 是 .NET 平台的核心元件，為不同語言提供了一個公共的執行環境。

## 共通型別規範 CTS（Common Type System）

`CTS`是定義關於「資料型別(`Data Types`)」的規則，為`CLS`的父集


## 共通語言規範 CLS（Common Language Specification）

共通語言規範`CLS`，為`CTS`的子集，用來描述.Net平台上各種不同語言(包含：`C#`、`VB`、`C++`、`J#`...)的共同特色，`CLS`是一組目的為提升語言互通性的規則(`Rule`)，確保程式語言之間的互通性。       

`CLR`使得設計其物件可跨語言互動的元件和應用程式更為容易，讓不同語言所撰寫的物件可以彼些通訊，而且它們的行為可以緊密整合。




[[C# 筆記] CTS、CLS、CLR 分別作何解釋？ by R](https://riivalin.github.io/posts/2017/02/what-are-the-explanations-for-cts-cls-and-clr/)      
[[C# 筆記] 一切的祖宗object類 by R](https://riivalin.github.io/posts/2010/03/88-object/)     
Book: Visual C# 2005 建構資訊系統實戰經典教本    