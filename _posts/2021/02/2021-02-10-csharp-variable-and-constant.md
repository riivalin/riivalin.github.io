---
layout: post
title: "[C# 筆記] 變數(Variable)與常數(Constant)"
date: 2021-02-10 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,變數(Variable),常數(Constant),const,keyword]
---


## 變數(Variable)

- 是指「程式中資料最基本的儲存單位，是記憶體中用來存放資料的一塊儲存區域」。
- 變數的功能主要用來處理資料，用來**接收輸入與傳遞運算結果**，通常需要透過變數宣告才能使用。

### 語法

```c#
[存取修飾詞][資料型別][變數名稱];
```

### 範例

```c#
//宣告score、name變數(沒有直接給定初始值)(也可以直接給定初始值)
public int score; //宣告score為整數變數用來存放成績
public string name; //宣告name為字串變數用來存放名字

//針對變數直接給初值
score = 99;
name = "Rii";
```

### 變數命名

- 變數所命名的名稱不可與C# 關鍵字(Keyword)相同。
- C# 識別字有字母大小寫之分，例如：good 和 Good 是不一樣的變數。
- 變數第一個字元不能是「數字」，例如：`string 7-11_Employee;` 是錯誤的宣告。
- 有意義的變數名稱命名可以提高程式的可讀性，例如：存放員工姓名的變數名稱可為 `EmployeeName`。


## 常數(Constant)
不能被重新賦值、不能被改變。        
(唯讀常數使用`Pascal`規則命名，即開頭字母大寫。)

- 用來存放固定不變的數值。
- 例如：圓周率`π`為 `3.14`此值是不會變動的。
- 一旦宣告成常數之後，就不能再指定任何值給該常數。(若對常數重新給值則會發生錯誤)

### 語法

```c#
[存取修飾詞] const [資料型別][變數名稱] = [預設值];
```

### 範例

```c#
public const double CircleRatio = 3.14; //宣告圓周率π為3.14

double circle; //圓周長
int ratius = 10; //半徑

circle = ratius * 2 * CircleRatio; //計算圓周長
```

## 關鍵字 Keyword

- 「關鍵字`Keyword`」就是「對編譯器具有特殊意義的文字所組成的保留識別項」。
- 這些保留識別項不能當成「變數」來使用。
- 「關鍵字`Keyword`」主要是留給程式語言指領所使用的，故又稱「保留字(`Reserved Word`)」。
- 如果您希望關鍵字可以成為程式中的識別項，於關鍵字前面必須加上一個前置的`@`符號。
- 例如：`@string`是合法的識別項，但若是`string`就不能當成變數來使用。



[MSDN - C# 識別碼命名規則和慣例](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/coding-style/identifier-names)        
[MSDN - const (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/const)     
[MSDN - C# 關鍵字](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/)