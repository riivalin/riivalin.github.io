---
layout: post
title: "[C# 筆記] 抽象類別(Abstract Class)"
date: 2021-05-13 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,abstract,多型]
---


「抽象類別(`Abstract Class`)」只有抽象概念，代表的是，我們不需要在基類(父類)中實現這個方法。        
而具體的實現，只能在派生類(子類)中處理。


抽象類別(Abstract Class)就是「只能用來提供給其他類別繼承的基底類別」，它提供一種類似**樣版**的功能。

抽象類別中，可以實作完整的方法和屬性，也可以像 `interface 介面`一樣單純宣告方法和屬性的空殼，留待給繼承抽象類別的子類別來實作該方法和屬性。(不能夠對「抽象類別」進行實體化)        

- 抽象類別 不能進行實體化。
- 抽象方法 只能於抽象類別中定義。
(如果我們給一個類別`Class`的方法或者屬性加上了`abstract` 這個關鍵詞，整個類別也必須要使用 `abstract`)
- 抽象類別 不能使用`sealed`關鍵字修飾詞。
- 抽象方法 隱含 虛擬方法，所以當要實作時，需要搭配`override`關鍵字來實作。
- 在抽象方法中 不能使用 `static`或 `virtual`關鍵字。


> 當父類中的方法不知道如何去實現的時候，可以考慮將父類寫成 抽象類別，將方法寫成 抽象方法。      
> 既然不能實現，就不要實現，就用抽象類別，讓子類去實現。        
> [[C# 筆記][多型] Abstract 抽象類](https://riivalin.github.io/posts/2011/01/abstract/)


## 抽象類別 & 介面

介面可以被多重實現，抽象類別只能被單一繼承

---

什麼情況會用到介面interface？       
當你需要多繼承的時候，就要考慮介面interface了。

為什麼呀？我就不能用抽象類嗎？      
因為類別只能繼承一個，是不允許多繼承的，只能繼承一個父類(一個基底類別)    
(抽象類別 只能繼承一個類別)



## 實現多型的三種手段

- 虛方法 `virtual` (virtual, override)
- 抽象類 `abstract` (abstract, override)
- 介面 `interface`

## 什麼時候用虛方法、抽象類？

- 父類的函式有實現、有意義的時候，就用虛方法(virtual)
- 父類的函式不知道怎麼去實現，就用抽象類(abstract)

[MSDN - abstract (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/abstract)       
[c#中抽象类(abstract)和接口(interface)的相同点跟区别](https://blog.csdn.net/lidandan2016/article/details/78831865)   
[[C# 筆記][多型] Abstract 抽象類](https://riivalin.github.io/posts/2011/01/abstract/)      
[[C# 筆記] Abstract 抽象類與抽象成員](https://riivalin.github.io/posts/2010/01/r-csharp-note-16/)   
[[C# 筆記] 里氏轉換(LSP)  by R](https://riivalin.github.io/posts/2011/01/lsp/)    
[實現多態(多型)的三個方法 ([C# 筆記] .Net基礎-複習-R)  by R](https://riivalin.github.io/posts/2011/02/r-cshap-notes-3/#6多態多型)       
    


Book: Visual C# 2005 建構資訊系統實戰經典教本    