---
layout: post
title: "[C# 筆記][IOC][DI][介面導向] 反轉控制 vs 依賴注入 -理論"
date: 2010-01-23 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,IOC 控制反轉,DI 依賴注入,interface 介面導向]
---

## 依賴注入 DI
- 依賴關係注入(DI - Dependencies Injection) 是一種軟體設計模式
- 在類別似其依賴項之實現控制反轉(Ioc)的技術
- 可以實現類似系統配置、日誌記錄和選項模式等功能的解耦

## 反轉控制Ioc vs 依賴注入DI

在軟體開發過程中，每個系統都是由若干個對象(物件)構成的，不同的子系統，不同的業務模塊互相組合，互相作用的結果。

- 每個系統都是由若干個對象(物件)構成的

### IoC 反轉控制
什麼是`IoC`反轉控制？       

把高度複雜、高度耦合的系統，分解為一個個獨立的接口(介面)，通過一個中間人，也就是`IoC`容器調用這些接口(介面)，把已經獨立出對象(物件)重新再串聯起來的過程。      

- Inversion of Control，控制反轉，控制倒置
- 通過獨立的接口(介面)串聯系統
- `IoC`容器

### `IoC`容器
通過使用`IoC`容器對對象(物件)進行封裝之後，我們系統內部就實現了對外部的透明，從而顯著地降低了整體的複雜性，而系統的各個組件還可以靈活地被重用和擴展。   

```
中間是「IOC容器」，如同中間人，物件ABCD之間沒有關聯，他們都是通過IOC容器連接起來的：

物件A       物件C
    IOC容器
物件B       物件D
``` 

如果現在我拿走這個 `IOC容器`，再來看這套系統，就會發現，ABCD這四個物件之間沒有任何耦合關係，彼此之間毫無關係。      

這樣的話，當你實現A的時候，就不需要去考慮BCD物件之間的依賴關係，物件之間的依賴關係減到了最底的程度。        

所以在引入了中間人，也就是 `IOC容器`的概念以後，ABCD這四個物件失去了耦合關係，而齒輪之間的傳動，完全靠這個中間人`IOC容器`，同時，ABCD這四個物件，把自己控制權也全權交給了這個 `IOC容器`進行負責。       

所以`IOC容器`就變成了整個系統最關鍵的核心，它起到了一種類似黏合劑的作用，把系統中所有對象(物件)黏點在一起發揮作用。

### 依賴注入 DI

既然`IOC` 是控制反轉，那麼到底是哪些方面的控制被反轉了呢？  
原來獲得依賴對象(物件)的過程被反轉了。      

當控制被反轉了，獲得依賴對象(物件)的過程，由自身管理變成了`IOC容器`的主動注入，於是他就給這個反轉控制取了一個更加合適的名字，叫做「`依賴注入 DI`」。        

所謂的「`依賴注入 DI`」就是由`IOC容器`在運行期間，動態地將某種依賴關係注入到某個對象(物件)中。

- 既然`IOC` 是控制反轉，那麼到底是哪些方面的控制被反轉了呢？
- 獲得依賴對象(物件)的過程被反轉了
- 控制被反轉之後，獲得依賴對象(物件)的過程，由自身管理變成了`IOC容器`的主動注入

## 舉例
比方說，USB介面 和 USB設備，通過 USB介面 我們可以從硬碟中讀取文件，電腦在讀取硬碟文件的時候，它不會關心USB介面上面連接的是什麼設備，它只要知道它的任務：讀取USB介面就可以了，所以，凡是符合USB介面標準的設備，都可以被電腦所使用。      

比如說，現在我把這個 硬碟 換成一個行動硬碟，文件照樣還是可以讀取的。        

而我把行動硬碟換成滑鼠、鍵盤，USB設備同樣也可以被兼容。     

插入什麼設備，電腦決定不了，它只能被動接受，需要使用到某個設備的時候，我作為電腦的主人，就會主動幫它插入USB設備，這就是我們生活中所常見的「`依賴注入DI`」的例子。       

而在這個過程中，我起了 `IOC容器`的作用。        

## IOC與依賴注入

所以，`依賴注入DI`和`控制反轉IOC`，實際上是從不同的角度描述同一件事情。就是通過引入`IOC容器`，利用依賴關係的注入，實現對象(物件)之間的解耦。 而它的思想核心則是面向接口編程(介面導向編程)。


- 從不同的角度描述同一件事情
- 通過引入`IOC容器`，實現對象(物件)之間的解耦
- 核心思想：面向接口(介面導向)

## IOC的好處
使用`IOC容器`有哪些好處呢？     

- 低耦合性
- 標準性
- 可重複性

### 低耦合性
以USB設備為例，首先它是低耦合性的USB設備，作為電腦的外部設備，在插入主機之前與電腦沒有任何關係，只有當我們插入USB連接上電腦之後，兩者才會發生關係。     

而兩者之中，無論任何一方出現任何問題，都不會影響到另一方的運行，這種特質體現在軟體工程中，就是可維護性非常好，容易進行單元測試，便於調試程序和診斷故障。        

這就是組件之間「低耦合性」和「無耦合性」帶來的好處。        

### 標準性
第二個好處：使用 `IOC`可以建立一個完整的標準。      

比如說，生產USB設備的廠商和生產主機的廠商，可以使用兩組完全不相干的人，
他們各司其職，唯一需要遵守的約定就是：USB介面標準。     

這種特性體現在軟體開發過程中，這就叫做「面向接口編程」(介面導向編程)，每個團隊只需要關係自己的業務邏輯實現，兩支團體只需要遵守同一個介面就可以了。      

所以一個中大型專案中，將一個大的任務劃分為小的任務，在遵守介面定義標準下，團隊成員可以明確分工、明晰責任，開發效率和產品質量可以得到大幅度的提高。      

### 可重複性
第三個好處就是「可重複性」。        

同一個USB可以接到支持任何USB設備上，可以接到電腦上，也可以接到智能電視上，所以同一個USB設備是可以重複利用的。       

在軟體工程中，這種特性就是可重複性。

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=41](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=41)