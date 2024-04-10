---
layout: post
title: "[C# 筆記] CTS、CLS、CLR 分別作何解釋？"
date: 2017-02-14 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法]
---

![](https://dotnettrickscloud.blob.core.windows.net/img/netframework/cts-cls.png)

## CTS（Common Type System）

`CTS` 是.NET平台中所有程式語言都必須遵循的規範，它定義了一組公共的資料類型和規則，以確保不同語言之間的互通性。這意味著不同的程式語言可以使用相同的資料類型，從而實現相互溝通和互動。      

## CLS（Common Language Specification）

`CLS` 是定義在CTS上的一組規範，目的是確保.NET程式語言之間的互通性。 CLS規範包含一組規則，要求支援CLS的程式語言應該使用CTS定義的類型，並且必須遵循一些規則，以便其他語言也能夠使用這些類型。如果一個程式集符合CLS，那麼它可以被任何CLS相容的語言使用。     

## CLR（Common Language Runtime）

`CLR` 是.NET平台的執行環境，負責管理和執行.NET程式。它提供了許多關鍵的服務，包括記憶體管理、垃圾回收、執行緒管理、安全性、程式碼存取安全性等。 CLR也負責將中間語言（IL，Intermediate Language）編譯成本地機器程式碼，並執行程式集中的方法。 CLR是.NET平台的核心元件，為不同語言提供了一個公共的執行環境。     

## 總結

總的來說，`CTS`定義了.NET平台中的資料類型和規則，`CLS`確保程式語言之間的互通性，而`CLR`則負責執行和管理.NET程式。這三者共同構成​​了.NET平台的基礎，使得不同語言能夠在相同的執行時間環境中協同工作。       


[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)  