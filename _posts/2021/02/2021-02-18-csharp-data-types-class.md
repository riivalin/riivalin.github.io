---
layout: post
title: "[C# 筆記] 資料型別種類(Data Types Class)"
date: 2021-02-18 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別]
---

資料型別的分類有兩種：      
- 內建資料型別：常見的有`int`, `string`, `bool`, `char`, `double`等
- 使用者定義資料型別：常見的有`interface`, `class`      

若從**儲存記憶體的觀點**來分類，亦可將資料型別分為二種：        
- 實值型別
- 參考型別

    
# 值域範圍和後置字元的重要

下面範例，會發生錯誤。      
「檢查模式下，作業於編譯時期溢位。」的錯誤訊息

```c#
double TB;
TB = 1024 * 1024 * 1024 * 1024; //發生溢位
```

錯誤在於每一個 `1024`均以`int`型態為預設形態，當連續乘上四次 1024便發生溢位情況(`1,099,511,627,776` > `2,147,483,647`)。        

> `int` 範圍 `-2,147,483,648` 至 `2,147,483,647`

## 值域範圍

要如何移除上述的錯誤呢？        
最簡單的方式就是將`1024`改成`1024.0`來做相乘，因為`1024.0`會被自動轉成 `double`資料型態。       

```c#
double TB;
TB = 1024.0 * 1024.0 * 1024.0 * 1024.0; //1024改成1024.0 就會自動轉成double來做相乘
Console.WriteLine($"TB = {TB} Bytes"); //TB = 1099511627776 Bytes
```

## 後置字元

除了上述方法之外，也可以使用「後置字元」方式來轉型成`double`資料型態。      
(數值後面加上`d`轉型成`double`)

```c#
double TB;
TB = 1024d * 1024d * 1024d * 1024d; //1024加上d轉型成 double來做相乘
Console.WriteLine($"TB = {TB} Bytes");
```

## 強制轉型 Convert.To
    
也可以用「強制轉型」方法`Convert.ToDouble()`。

```c#
double TB;
TB = Convert.ToDouble(1024) * Convert.ToDouble(1024) * Convert.ToDouble(1024) * Convert.ToDouble(1024);
Console.WriteLine($"TB = {TB} Bytes");
```

## 指定轉換 (Cast) 

或是「指定轉換」

```c#
double TB;
TB = (double)1024 * (double)1024 * (double)1024 * (double)1024;
Console.WriteLine($"TB = {TB} Bytes");
```

# Tips
## 程式執行效能調校 (Performance Tuning)？

程式設計時，可依照實際情況來宣告資料型別，例如：我們宣告`YearNum`來代表西元，正常之下我們宣告成`ushort`(0~65535)資料型別已經相當足夠了，但是，若我們宣當成`int`或`long`則會造成多餘記憶體的浪費，以宣告成`long`而言，這一差就是四倍(`16 bits` vs `64 bits`)的儲存剛空間，再加上先天電腦硬體設備不能無限擴充的限制，使得適當的資料型別宣告變得更為重要，這對於程式執行效能有著生死存已的利害關係，這些細節多留意。
