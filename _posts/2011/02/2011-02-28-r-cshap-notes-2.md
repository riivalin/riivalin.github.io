---
layout: post
title: "[C# 筆記] 物件導向 -R "
date: 2011-02-28 00:03:41 +0800
categories: [Notes,C#]
tags: [C#,R,new,this,base,return,as,is,里氏轉換,繼承,OO,物件導向]
---

TODO: 單例模式，只能創建一個物件(對象)

## 物件導向(面向對象)的複習
封裝、繼承、多型(多態)

## Class類別的成員
- Class類別的成員：屬性、欄位/字段、構造函數、方法、介面/接口
- Field(欄位/字段)：儲存數據，訪問修飾符應該設置為private私有的。
- 屬性：保護Field(欄位/字段)，對Field的取值和賦值進行限定。

## new關鍵字
`new`關鍵字：
1. 在heap(堆積/堆)當中開闢空間
2. 在開闢的空間中創建物件(對象)
3. 調用物件(對象)構造函數
> 引用類型的值都是在heap(堆積/堆)當中

## this
- 代表當前類的物件(對象)
- 調用自己類的構造函數

## base
- 一切類型的父類
- 繼承父類的構造函數

## 構造函數
構造函數就是一個特殊的方法
- 沒有返回值
- 他是public公有的
- 名稱跟類名一樣

## 構造函數的作用是什麼？
初始化物件(對象)，當創建物件(對象)的時候，會調用構造函數

## 創建物件&初始化物件
- 創建物件
寫好一個類，要使用它必須先創建它的物件(對象)，除了靜態類、抽象類
- 初始化物件
給物件(對象)的每個屬性賦值的過程，稱之為物件(對象)的初始化

## 靜態成員&實體成員
- 靜態成員
有`static`關鍵字代表靜態成員，靜態方法怎麼調用？直接類名.方法
- 非靜態成員(實體成員)
沒有`static`關鍵字稱之為實體成員、非靜態成員

## 對Field(欄位/字段)的保護方法
1. get()
2. set()
3. 構造函數

## return
- 立即結束本次方法(立即離開方法)
- 在方法中返回要返回的值`return age = 0;`

## 繼承
解決代碼的冗餘，實現多形(多態)，增加了代碼的擴展素，便於維護。

## 繼承有兩個很重要的特性：
1. 單根性
2. 傳遞性
  
### 單根性：
單根性指的是，一個類只能有一個父類，所以我們說，類是單繼承的，誰是多繼承的？介面(接口)，介面才能多繼承。
> 類別：單繼承；介面：多繼承

### 傳遞性：
子類可以使用父類的成員，一個類繼承了一個父類，繼承了屬性和方法，沒有繼承Field(欄位/字段)。
- 子類並沒有繼承父類的構造函數，而是會默認調用父類的那個無參數的構造函數。
- 如果一個子類繼承了一個父類，那麼這個子類除了可以使用自己的成員外，還可以使用從父類那邊繼承過來的成員。但是父類永都只能使用自己的成員，而不能使用子類的成員。子類之間也不能互相使用對方的成員。

### 子類為什麼調用父類的構造函數？
因為子類最終要使用父類的成員，父類沒有物件(對象)的話，你能訪問他的成員嗎？不能，所以子類會先在內部會先創建父類的物件(對象)，當無參的構造函數沒有了，還能創建嗎？當然不能。

### 解決這個問題有兩個辦法：
- 父類再加一個無參的構造函數
- 在子類中調用父類的構造函數，使用關鍵字`base`


## 里氏轉換(LSP)
里氏轉換(LSP)就兩條定義：
- 第一條定義：子類可以賦值給父類
- 第二條定義：父類強轉為子類。(前提是這個父類裡面裝的一定是子類對象)    
如果父類中裝的是子類對象(物件)，那麼可以將個父類轉換為子類對象(物件)    
- 用`as`、`is`兩個關鍵字做轉換
    - `as`：轉換成功返回對應的對象(物件)，轉換失敗返回`null`
    - `is`：is會用在判斷，轉換成功回傳true，失敗回傳false

        
- [[C# 筆記] 里氏轉換(LSP)-複習](https://riivalin.github.io/posts/lsp-1/)
- [[C# 筆記] 里氏轉換(LSP)](https://riivalin.github.io/posts/lsp/)
- [[C# 筆記] 物件導向(面向對象)語法和繼承-複習](https://riivalin.github.io/posts/oo/)
- [[C# 筆記] Inherit 繼承](https://riivalin.github.io/posts/inherit/)