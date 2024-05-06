---
layout: post
title: "[C# 筆記] 字串格式化 string.Format() & ToString()"
date: 2021-03-26 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,string,字串格式化]
---


格式化(Format) 對於「數字字串」或「日期字串」的顯示扮演相當重要的角色。     

最常遇到的問題：
1. 字串顯示對齊
2. 數值格式調整
3. 日期格式調整

```
1. 字串顯示對齊

姓名    數學   英文        姓名    數學   英文
張大三   99    80   ->>   張大三   99    80
李小四   88    90         李小四   88    90
小菜  69   89             小菜    69    89

2. 數值格式調整

88      088
9   ->> 009
100     100

3. 日期格式調整

1999年1月9日        1999年01月09日
2001年10月10日  ->> 2001年10月10日
2020年3月22日       2020年03月22日
```

## 數值格式化

| 格式| 說明            | Format  | Input  |執行結果 |      補充        |
|:---:|:---------------|:---------|:-------|:------|:---------------------|
| `D` | 十進位補零           | `{0:D3}` | 99      |099   |`D`後面數字表示指定的位數|
| `F` | 固定浮點數，且四捨五入 | `{0:F}` | 5.6789  | 5.68  | `F`後面數字表示指定的位數，`F`預設到小數第二位|
| `N` | 數值表示法     | `{0:N}` | 120000 |  120,000.00  | Number：每三位數用逗號 "," 隔開|
| `C` | 貨幣值，且四捨五入 | `{0:C}`| 20.5678 | NT$20.57 | Currency ：C預設到小數2位…C1取小數一位，C3取小數三位…|


```c#
// D: 十進位補零，D後面數字表示指定的位數
int i = 9;
Console.WriteLine(string.Format("{0:D2}",i)); //09
Console.WriteLine(i.ToString("D2")); //09
Console.WriteLine(string.Format("{0:D3}",i)); //009
Console.WriteLine(i.ToString("D3")); //009

// F: 固定浮點數，且四捨五入，F後面數字表示指定的位數，沒指定數字是到小數第二位
double i = 4.5678;
Console.WriteLine(string.Format("{0:F0}",i)); //5
Console.WriteLine(i.ToString("F0")); //5
Console.WriteLine(string.Format("{0:F}",i)); //4.57
Console.WriteLine(i.ToString("F")); //4.57
Console.WriteLine(string.Format("{0:F1}",i)); //4.6
Console.WriteLine(i.ToString("F1")); //4.6
Console.WriteLine(string.Format("{0:F3}",i)); //4.568
Console.WriteLine(i.ToString("F3")); //4.568

// N: 數值表示法，每三位數用逗號 "," 隔開
double i = 120000;
Console.WriteLine(string.Format("{0:N}",i)); //120,000.00
Console.WriteLine(i.ToString("N")); //120,000.00


// C: 貨幣值，且四捨五入，C預設到小數第二位
double i = 20.5678;
Console.WriteLine(string.Format("{0:C}", i)); //NT$20.57
Console.WriteLine(i.ToString("C")); //NT$20.57
```


[MSDN -  標準數值格式字串](https://learn.microsoft.com/zh-tw/dotnet/standard/base-types/standard-numeric-format-strings)