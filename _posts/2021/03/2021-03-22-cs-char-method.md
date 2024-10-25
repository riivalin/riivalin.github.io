---
layout: post
title: "[C# 筆記] 字元(Char)常用方法"
date: 2021-03-22 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,char]
---

字元(Char)常用的方法：      

| 方法      | 說明             | 
|:----------|:-----------------|
| `IsControl()` | 判斷是否為控制字元。如：`\n`、`\t`、`\r`等。|
| `IsDigit()`     | 判斷是否為十進數字  |
| `IsLetter()`   | 判斷是否為英文字母 |
| `IsLower()`   | 判斷是否為小寫英文字母|
| `IsUpper()`   | 判斷是否為大寫英文字母|
| `IsNumber()`   | 判斷是否為數字 |
| `IsWhiteSpace()`| 判斷是否為「空白字元」|
| `IsLower()` | 將字元轉換成小寫英文字母| 
| `IsUpper()` |將字元轉換成大寫英文字母|
| `IsPunctuation()` | 判斷是否為標點符號。如：`-`,`\`,`/`,`@`,`#`,`%`,`&`,大小括弧等。|
| `IsSeparator()`   | 判斷是否為分隔符號字元，「空白字元」是分隔符號。|
| `IsSymbol()`   | 判斷是否為符號字元。如：`~`,`+`,`$`,`^`,`\|`,`=`|




[[C# 筆記] 特殊字元處理：反斜線/、@符號](https://riivalin.github.io/posts/2021/03/cs-char-escape-sequences/)       
Book: Visual C# 2005 建構資訊系統實戰經典教本