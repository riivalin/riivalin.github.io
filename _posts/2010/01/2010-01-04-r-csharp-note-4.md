---
layout: post
title: "[C# 筆記] 什麼是面向對象(物件導向)"
date: 2010-01-04 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,OO]
---

## 什麼是面向對象(物件導向)
- 原則：使用對象來描述世界中客觀存在的一切事物
- 世界的運行過程，就是不同對象互相作用的結果
- 物件導向要求我們用程式碼來模擬真實世界的客觀運行規律
- 對象(物件)、類、實例(實體)
- 從宏觀上來認識，分解並且總結某個事物的運行規則

### 舉例：五子棋    
七個步驟，接下來循環234567，直到黑子勝出 或是白子勝出，然後遊戲結束。       

以下的每一個步驟都可以看作獨立的函數，或者獨立的方法來進行開發。 

這樣自下而下的過程，就是典型的物件導向的編程思想。  

```
第一步，開始遊戲
While
    第二步，黑方出子
    第三步，繪製棋盤
    Until 第四步，判斷黑子有沒有勝
    第五步，白方出子
    第六步，繪製棋盤
    Until 第七步，判斷白子有沒有勝
遊戲結束
```

如果把五子棋遊戲從面向過程改為面向對象的設計思路，我們可以把整個程式碼抽象為四個模塊系統：1.遊戲流程 2.黑白雙方 3.棋盤繪制 4.規則判定。 

- 遊戲流程：負責遊戲流程控制
- 棋盤繪制：繪制渲染遊戲畫面
- 黑白雙方：負責出子或是悔棋
- 規則判定：負責制定遊戲規則、判斷輸贏

根據這四個系統，於是我們就得到了遊戲的五個對象(物件)：
1. 遊戲控制器
2. 棋盤
3. 黑玩家
4. 白玩家
5. 規則系統

在面向對象的思路中，我們並不需要定義程式碼的過程，只需要定義不同的對象(物件)，或者說，不同的系統之間是如何互相影響、互相作用就可以了。

於是我們的程式碼就變成這樣了：  
首先初始化遊戲的四個系統    
- 初始化：1.遊戲控制器 2.棋盤 3.黑玩家 4.白玩家 5.規則系統
- 向「遊戲控制器」添加：棋盤、黑白玩家、規則系統
- 然後使用「遊戲控制器」開始遊戲
- 遊戲自動運行

「遊戲控制器」會自動運行整個遊戲流程，還會輸出結果。所以整個遊戲都將會被「遊戲控制器」所控制。

這個時候，我們不知道，也不需要知道這個遊戲是如何運行的，但是，我們知道的是，棋盤、黑白雙方、判定系統都會在一定程度內相互作用，並且通過「遊戲控制器」連接成為一個統一的整體。

所以當我們從宏觀上來認識，分解並且總結某個事物的運行規則，這就是面向對象(物件導向)。
    
## 對象(物件)
那麼，面向的對象到底是什麼呢？任何一個客觀存在的事物都是對象(物件)，類是對象、實例是對象、變量也是對象，甚至數據類型也可以視為是對象。

- C#中，一切皆是對象(物件)
- 類是對象、實例是對象、變量也是對象，甚至數據類型也是對象

### 舉例：一輛車
- 能夠執行行駛相關的動作，可以「開」、「停」、「剎車」。
能被執行的動作，是對象的「調用方法」。
- 不能執行，但可以被量化、可以被描述的信息，比如：重量、排量、軸距、油耗…等。
而可以被量化的描述，則是對象的「基本屬性」。

### 對象的特徵
「方法」和「屬性」是對象最基本的兩個特徵
- 能被執行的動作，是對象的調用「方法」。
- 而可以被量化的描述，則是對象的基本「屬性」。

## 類別 Class
那麼，類別 Class 是什麼呢？
- 類與面向對象息息相關
- 指的就是「一類東西」

比如：「車」是一類東西；   
細分為：汽車、卡車、火車、自行車；   
「汽車」繼續細分：寶馬、BMW、Benz   

### 所以，類class 就是
所以類的意思，就是表示一類事物的總和，相當於是在描述這一類事物的藍圖，
- 一類事物的「總和」
- 相當於描述這一類事物的「藍圖」

### 舉例：五菱宏光
```
class 五菱宏光 {
    外號：秋名山神車 --屬性
    馬力：5w匹  --屬性
    拉貨()  --方法
    載人()  --方法
    玩漂移() --方法
}
```
這個汽車使用`class` 來聲明它是個類，類的名稱：五菱宏光，這個class是由兩個部分組成的：屬性、方法。外號和馬力是可以被量化的描述，屬於類的基本屬性，而拉貨、載人、玩漂移則是可以被執行的動作，屬於類的調用方法。

而以上這個「五菱宏光」類，則是我們通過觀察真實世界中「五菱宏光」這個汽車而總結出的定義。

## 實例(實體)instance
###  何為實例(實體)instance?
我們說類可以描述一類事物，但是當我要表示這類事物中某一個具體的個體，應該怎麼處理呢？這時候我們就需要使用實例 instance。

- 類表示一類事物
- 「實體」表示這類事物中的某一個具體的個體

比如說，我有一個輛五菱宏光，老司機是我  
偽程式碼：  

```
五菱宏光 Riva的神車 = new 五菱宏光();

使用new初始化「五菱宏光」的物件，而這個物件的名稱就叫作「Riva的神車」
```
也可以簡單的理解為「初始化」

一般來說，我們會使用`new`這個關鍵字來實例化對象(實體化物件)，簡單解釋一下，我們使用了`new`這個關鍵字，初始化了一個「五菱宏光」的對象，而這個對象的名稱就叫作「Riva的神車」。

## 類、對象、實例(類、物件、實體)
套《Thinking in Java》一句話說明類、對象，以及實例的三者關係：「每個「物件」都是某個「類別(class)」的一個實體」

- 每個「對象(物件)」都是某個「類(class)」的一個實例(實體)。  
- 對象(物件)與實例(實體)可以劃上約等號，對象(物件)≈實例(實體)
- 創建一個物件的過程就叫作「實例化(實體化)」，而實例化(實體化)的產物就叫作「實例(實體)」、或者叫做「對象(物件)」

### 面向對象(物件導向)的意義
```
class 五菱宏光 {
    外號：秋名山神車
    馬力：5w匹
    拉貨()
    載人()
    玩漂移()
}
```
- 通過觀察真實的五菱宏光總結得出的定義
- 不一定能完全符合客觀規律
- 客觀事實某個片段在程式碼中的投影

王者的定義  

```
class 五菱宏光 {
    車長：4420
    車寬：1685
    車高：1770
    軸距：2720
    排量：1.2/1.5
    檔位：MT
    馬力：76~99
    拉貨()
    載人()
    遙控鑰匙啟動()
    駐車電達警報()
    一鍵升窗()
    電動調節後視創()
    ... ...
}
```

