---
layout: post
title: "[C# 筆記] namespace 命名空間"
date: 2011-01-14 23:39:00 +0800
categories: [Notes, C#]
tags: [C#,namespace]
---

## namespace 命名空間
用於解決類別重名問題，可以看做「類別的文件夾」

```
A—> ProjectA —> 顧客類別
B—> ProjectB —> 顧客類別
C—> ProjectC —> 顧客類別
```
如果當前項目中沒有這個類的命名空間，需要我們手動的導入這個類所在的命名空間`using`

## 在一個專案中，引用另一個專案 
方案總管 > [相依性] > 滑鼠右鍵 > [新增專案參考]     
也可以  
專案 > 滑鼠右鍵 > 選取 [新增>專案參考]  

[ide/managing-references-in-a-project?view=vs-2022](https://learn.microsoft.com/zh-tw/visualstudio/ide/managing-references-in-a-project?view=vs-2022)