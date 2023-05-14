---
layout: post
title: "[SQL筆記] char, varchar, nchar, nvarchar"
date: 2011-03-13 00:37:00 +0800
categories: [Notes,SQL]
tags: [R,sql char,sql varchar,sql nvarchar]
---


```
bit 可以代表0,1 
1 英數字 = 1 byte = 8 bits 
1 中文字 = 1 word = 2 bytes = 16 bits 
```

```
bit 位元 0,1  
8 bits(位元) = 1 byte(位元組) 
英文字母、0~9數字、符號 = 1 byte(位元組) = 1字元(character)   
1個中文字 = 2 bytes(2位元組) = 16 Bits(16位元) = 1字組(word)  
```

- `char` 固定大小。 
- `varchar` 變動大小。  
- `char`、`varchar` ，前面加上`n`，儲存`unicode`字串，對中文友好。

一般在進行中文字插入時，會在中文字串前面加上一個大寫字母`N`

```sql
insert into Student(StuName) values (N'張三')
```

## char 和 varchar
儲存單位 : 1 Byte

- `char`固定大小：一建立就佔據了固定的字元長度。
- `varchar`變動大小：只要在範圍內，存多少就佔多少。
一個字母佔1byte，一個中文字佔2bytes。

- `char(10)`：不管裡面存多數據，都佔10個字元。
- `varchar(10)`：只要在範圍內，儲存幾個就佔用幾個，最多佔用10個字元。

- `char(10)`：存「AB」，佔用10bytes，因為長度固定，不足的會自動用空格填滿。(真正存的內容是AB加上8個空格)
- `varchar(10)`：存「AB」，佔用2bytes，因為長度變動，儲存幾個就佔用幾個。
    
- char 固定大小，浪費空間，所需計算時間少。
- varchar 不固定長度，必須要花費較多的CPU計算時間。

- `char`、`varchar` 前面加上 `n`，儲存`unicode`字串，對中文友好。
 

## nchar 和 nvarchar
儲存單位 : 2 Bytes(Unicode)
- `char`、`varchar` 前面加上 `n`，儲存`unicode`字串，對中文友好。

## varchar 和 nvarchar
- `varchar(100)`：儲存100個英數字，或是50個中文字
- `nvarchar(100)`：儲存100個英數字，或是100個中文字

> `varchar`比`nvarchar`更加節省空間

## 使用時機

- 有中文用 `nchar`、`nvarchar`
- 純英數用 `char`、`varchar`


- `char` 資料是固定長度，並且都為英文數字。
- `varchar`	資料沒有固定長度，並且都為英文數字。
- `nchar` 資料是固定長度，可能有中文。(不確定是否皆為英文數字)
- `nvarchar` 資料沒有固定長度，可能有中文。(不確定是否皆為英文數字)
- `varchar(max)` 字串長度可能超出 8,000 位元組時。
- `nvarchar(max)` 字串長度可能超出 4,000 位元組配對時。


- [MSDN char 和 varchar](https://learn.microsoft.com/zh-tw/sql/t-sql/data-types/char-and-varchar-transact-sql?view=sql-server-ver16)
- [MSDN nchar 和 nvarchar](https://learn.microsoft.com/zh-tw/sql/t-sql/data-types/nchar-and-nvarchar-transact-sql?redirectedfrom=MSDN&view=sql-server-ver16)

