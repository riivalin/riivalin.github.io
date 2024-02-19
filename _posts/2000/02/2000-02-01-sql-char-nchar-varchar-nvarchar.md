---
layout: post
title: "[SQL] SQL欄位 nvarchar, varchar, nchar, char"
date: 2000-02-01 06:30:00 +0800
categories: [Notes,SQL]
tags: [nvarchar,varchar,nchar,char]
---


SQL在欄位設定中，儲存文字的`char`就分成這四種：

- `nvarchar`
- `varchar`
- `nchar`
- `char`

第一時間真的是不明白差距在哪！但仔細看其實還是有其規律性

|char前的參數|說明|
|:----|:---|
|`var`|可變動。須額外花`2bytes`來儲存地址|
|`n`|萬國碼。每一字會花2倍的空間|

## 實際儲存空間

|char|長度|
|:----|:---|
|char(n)|n Bytes|
|varchar(n)|(n + 2) Bytes ，其中2 Bytes用來記錄地址|
|nchar(n)|(2 × n) Bytes|
|nvarchar(n)|(2 × n + 2) Bytes|

## 使用情境

從效能來看，因`char`、`nchar`不必再確認長度，速度較快些     
佔據空間上，`n`則會花費2倍空間        
實務上，還是得根據需求選擇怎樣的格式較佳！      

簡單茲分如下        

|需求|參數|
|:----|:---|
|只有英數字、長度固定|char|
|只有英數字、長度不定|varchar|
|會含英數以外的字元、長度固定|nchar|
|會含英數以外的字元、長度不定|nvarchar|


[https://blog.typeart.cc/SQL欄位nvarchar,%20varchar,%20nchar,%20char比較/](https://blog.typeart.cc/SQL欄位nvarchar,%20varchar,%20nchar,%20char比較/)