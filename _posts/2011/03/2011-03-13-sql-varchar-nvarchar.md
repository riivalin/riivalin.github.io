---
layout: post
title: "[SQL筆記] char, varchar, nchar, nvarchar"
date: 2011-03-13 00:37:00 +0800
categories: [Notes,SQL]
tags: [R,char,varchar,nvarchar]
---

- `char` 固定大小。英數字。儲存單位：1 Byte 
- `varchar` 變動大小。英數字。儲存單位：1 Byte  
- `nchar` 固定大小。中英數字。儲存單位：2 Bytes(Unicode)    
- `nvarchar` 變動大小。中英數字。儲存單位：2 Bytes(Unicode) 

> `char`、`varchar` ，前面加上`n`，儲存`unicode`字串，對中文友好。

## char 和 varchar
儲存單位 : 1 Byte

- `char`固定大小：一建立就占據了固定的字元長度。
- `varchar`變動大小：只要在範圍內，存多占多少。

`char`、`varchar` 前面加上 `n`，儲存`unicode`字串，對中文友好。

## nchar 和 nvarchar
儲存單位 : 2 Bytes(Unicode)

## 使用時機
- `char` 資料有固定長度，並且都為英文數字。
- `nchar` 資料有固定長度，但不確定是否皆為英文數字。
- `varchar`	資料沒有固定長度，並且都為英文數字。
- `nvarchar` 資料沒有固定長度，且不確定是否皆為英文數字。


- [char 和 varchar ](https://learn.microsoft.com/zh-tw/sql/t-sql/data-types/char-and-varchar-transact-sql?view=sql-server-ver16)
- [nchar 和 nvarchar](https://learn.microsoft.com/zh-tw/sql/t-sql/data-types/nchar-and-nvarchar-transact-sql?redirectedfrom=MSDN&view=sql-server-ver16)
