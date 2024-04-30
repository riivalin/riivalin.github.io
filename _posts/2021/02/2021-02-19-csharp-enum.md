---
layout: post
title: "[C# 筆記] 列舉(enum)資料型別"
date: 2021-02-19 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,列舉,enum]
---

## 資料型別 - enum、struct、Nullable
- 列舉(`enum`)與結構(`struct`)可以提高程式可讀性。
- `Nullable`類別的宣告讓實值變數可以存放`null`值。

## 什麼是列舉(enum)資料型別?

- `enum`是一種用來宣告**列舉型別(Enumeration Type)**的關鍵字。
- 是一組列舉清單項目的具名常數所構成的特殊型別。
- `enum`主要作用在於提高程式的可讀性與易維護性。

> 列舉(enum) 可以規範我們的開發。

## 注意事項

使用`enum`需要注意：
- `enum`不能於方法(Method)內宣告。
- 資料型別必須是：`byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long` 或`ulong`型別
- 預設的資料型別為`int`。
- 若列舉項目沒有給定任何整數值，則預設第一個列舉值為`0`、第二個列舉值為`1`，以此類推。
- `float`與`double`不可為`enum`的資料型別。

## 宣告語法

```c#
[存取修飾詞] enum [列舉名稱]:[資料型別]
{
    成員1,
    成員2,
    ...
    成員n,
}
```

## 範例

一共有四季，分別為「春、夏、秋、冬」，寫一程式，透過`enum`來宣告一個可以代表四季`Seasons`的列舉成員，當使用者選擇數字(1~4)時，便會顯示所對應的季節為何。

```c#
//宣告四季Seasons的列舉項目：春夏秋冬
enum Seasons : short
{
    Spring = 1,
    Summer = 2,
    Fall = 3,
    Winter = 4
}
static void Main(string[] args)
{
    while (true)
    {
        Console.WriteLine("請輸入數字1~4:");
        short number = short.Parse(Console.ReadLine()!); //接收用戶輸入

        //根據數值來顯示對應的四季訊息
        switch (number)
        {
            case (short)Seasons.Spring:
                Console.WriteLine("春天");
                break;
            case (short)Seasons.Summer:
                Console.WriteLine("夏天");
                break;
            case (short)Seasons.Fall:
                Console.WriteLine("秋天");
                break;
            case (short)Seasons.Winter:
                Console.WriteLine("冬天");
                break;
            default:
                Console.WriteLine("不正確的數值，請輸入數字1~4");
                break;
        }
    }
}
```

[[C# 筆記] enum 列舉 by R](https://riivalin.github.io/posts/2011/01/enum/)      
[[C# 筆記] 枚舉(列舉) Enum by R](https://riivalin.github.io/posts/2010/02/048/)