---
layout: post
title: "[C# 筆記] 關於構造函數說法正確的是哪個？"
date: 2017-02-26 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,Constructor,構造函數,靜態建構函數]
---

a) 建構子可以宣告回傳型別。     
b) 建構子不可以用`private`修飾      
c) 建構子必須與類別名稱相同     
d) 建構子不能帶參數         


答案：c     
建構函數的名稱必須與包含它的類別的名稱完全相同。這是建構函式的標準命名規則。其他選項是不正確的      

a) 建構子不可以宣告回傳型別。建構函式沒有傳回類型，甚至不能宣告 `void`。      
b) 建構子可以使用 `private` 修飾符。例如，私有建構函式常用於[實作單例模式](https://riivalin.github.io/posts/2010/04/76-singleton-1/)或工廠模式。        
d) 建構子可以帶參數。帶有參數的建構函式允許在建立物件時傳遞初始值，以便對物件進行初始化。       


        
[C# .NET面试系列一：基础语法](https://bbs.huaweicloud.com/blogs/423092)    
[[C# 筆記][WinForm] 單例模式 by R](https://riivalin.github.io/posts/2011/03/singleton/)     
[[C# 筆記] static應用-單例類別 (單例模式(Singleton)) by R](https://riivalin.github.io/posts/2010/04/76-singleton-1/)
