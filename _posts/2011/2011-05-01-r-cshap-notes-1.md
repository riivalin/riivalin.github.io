---
layout: post
title: "[C# 筆記] 註解&命名 -R"
date: 2011-05-01 00:02:21 +0800
categories: [Notes,C#]
tags: [C#,R,OO]
---

## 註解
`//`、`/**/`、`///`

單行註解 
: `//`註解單行代碼
多行註解
: `/* 要註解的內容 */`
文檔註解
: `///`註解類別和方法

HTML
: <!--要註解的內容-->
CSS
: /* 要註解的內容 */

## 命名規範
- Camel駱駝命名：變數/變量、Field(欄位/字段)
- Pascal帕斯卡命名：類別、方法、屬性名

### Camel駱駝命名
要求首單詞的或字母小寫，其餘單詞首字母大寫，變數/變量、欄位/字段
- `int age`、`string name`、`char gender`、`string highSchool`
- `int _chiness`

### Pascal帕斯卡命名
類別、方法、屬性名
- `GetMax`、`GetSum`定義的變量或方法，名字要有意義
- 方法名：動詞
`Write()`、`Open()`、`Close()`、`Dispose()`、`GetUserId()`...方法都是要做一件事情
- 變量名：按功能命名、按方法的返回值內容命名
`userName = GetUserName();`

## 物件導向/面向對象
### 需求
```
1. 在控制台提示用戶要進入的硬碟路徑
D:\
2. 提示用戶輸入要打開的文件名稱
1.txt
=> 不曉得用戶會入什麼類型的文件，按照父類別去處理(抽象方法)
```
### 寫成OO概念
- 父類：文件的父類
- 子類：只能打開`.txt`/`.wmv`/`.jpg`文件

#### 父類
文件的父類
- `OpenFile();`打開文件 寫一個抽象方法(因為不知道用戶會輸入什麼類型的文件)
- `public abstract void OpenFile(全路徑)`寫法
    - 方法1：傳參
    - 方法2：寫屬性

#### 子類
- .txt只能打開txt文件
- .wmv 只能打開wmv文件
- .jpg 只能打開jpg文件

### 簡單工廠
不知道用戶會輸入什麼類型的文件，所以給用戶返回一個父類，但是父類中裝的肯定是子類對象(子類物件)。



[[C# 筆記] 簡單工廠和抽象類別-複習](https://riivalin.github.io/posts/factory-and-abstract/)