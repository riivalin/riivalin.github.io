---
layout: post
title: "[C# 筆記] 字串格式化 string.Format() & ToString()"
date: 2021-03-26 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,string,字串格式化]
---


格式化(Format) 對於「數字字串」或「日期字串」的顯示扮演相當重要的角色。     

- 數值格式化
- 自訂數值格式化
- 標準DateTime格式化
- 自訂DateTime格式化


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

| 格式| 功能            | Format  | Input  |執行結果 |      補充       |
|:---:|:---------------|:---------|:-------|:------|:---------------------|
| `D` | 十進位補零           | `{0:D3}` | 99      |099   |`D`後面數字表示指定的位數|
| `F` | 固定浮點數，且四捨五入 | `{0:F}` | 5.6789  | 5.68  | `F`後面數字表示指定的位數，`F`預設到小數第二位|
| `N` | 數值表示法           | `{0:N}` | 120000 |  120,000.00  | Number：每三位數用逗號 "," 隔開|
| `C` | 貨幣值，且四捨五入   | `{0:C}`| 20.5678 | NT$20.57 | Currency ：C預設到小數2位…C1取小數一位，C3取小數三位…|
| `P` | 百分比，且四捨五入 |`{0:P}`|  0.567 | 56.70% | Percent：輸入數值＊100。預設取小數2位，P0可取小數|

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

// P: 百分比，且四捨五入，Percent：輸入數值＊100 ; 預設取小數2位，P0可取小數
double i = 0.567;
Console.WriteLine(string.Format("{0:P}",i)); //56.70%
Console.WriteLine(i.ToString("P"));//56.70%
Console.WriteLine(string.Format("{0:P0}",i)); //57%
Console.WriteLine(i.ToString("P0"));//57%
```

## 自訂數值格式化

| 格式| 功能            | Format  | Input  |執行結果 |      補充        |
|:---:|:---------------|:---------|:-------|:------|:---------------------|
| `0` | 預留零值位置  |`{0:000.000}` | 10.5679| 010.568|  會四捨五入 |
| `#` | 預留數值位置  | `{0:#.#}`    | 5.67   | 5.7     | 會四捨五入 |
| `.` |	小數點	| `{0:0.0}` | 5.75 | 5.8 | 會四捨五入 |
| `,` |	千位分隔符號 |	`{0:0,0}`|	1200|	1,200 | |
| `%` |	百分比預留位置 |	`{0:0%}`|	0.256 |	26% | 會四捨五入 |


```c#
// 0: 預留零值位置，會四捨五入
double i = 10.5679;
Console.WriteLine(string.Format("{0:0.00}", i)); //10.57
Console.WriteLine(i.ToString("0.000")); //10.568
Console.WriteLine(i.ToString("000.00000")); //010.56790

// #: 預留數值位置，會四捨五入
double i = 5.67;
Console.WriteLine(string.Format("{0:#.#}", i)); //5.7
Console.WriteLine(i.ToString("#.#")); //5.7

// .: 小數點，會四捨五入
double i = 5.75;
Console.WriteLine(string.Format("{0:#.#}", i)); //5.8
Console.WriteLine(i.ToString("#.#")); //5.8

// ,: 千位分隔符號
double i = 1200;
Console.WriteLine(string.Format("{0:0,0}", i)); //1,200
Console.WriteLine(i.ToString("0,0")); //1,200

// %: 百分比預留位置，，會四捨五入 {0:0%}	0.25	25%
double i = 0.256;
Console.WriteLine(string.Format("{0:0%}", i)); //26%
Console.WriteLine(i.ToString("0%")); //26%
```

電話號碼格式化：

```c#
int number = 0222309988;
Console.WriteLine(number.ToString("(0#)####-####")); //(02)2230-9988

int number = 0900666555;
Console.WriteLine(number.ToString("0###-###-###")); //0900-666-555
```

## 標準 DateTime 格式化

2010/8/11 下午 01:02:03

| 格式       | 功能               | Format   | 執行結果                     |      補充   |
|:--------|:--------------------|:---------:|:---------------------------|:-----------|
| `d`       |	簡短日期            | `{0:d}`|	2010/8/11                   |  MM/dd/yyyy|
| `D`       |	完整日期            | `{0:D}`| 2010年8月11日                 | |	
| `f`       |	完整可排序日期/時間 | `{0:f}`| 2010年8月11日 下午 01:02       | |
| `F`      |	完整可排序日期/時間	| `{0:F}`   | 2010年8月11日 下午 01:02:03   | |
| `g`       |	一般可排序日期/時間	| `{0:g}`   | 2010/8/11 下午 01:02	        ||
| `G`       |	一般可排序日期/時間	| `{0:G}`   | 2010/8/11 下午 01:02:03	    ||
| `M`、`m`  |	月日               | `{0:m}` | 8月11日                       ||
| `Y`、`y` |	年月	           | `{0:y}` |	2010年8月                     ||
| `t`       |	簡短時間            | `{0:t}` |	下午 01:02	                 | HH:mm|
| `T`       |	完整時間           | `{0:T}` |  下午 01:02:03	            | HH:mm:ss|



```c#
//2024/5/6 下午 11:23:38
Console.WriteLine(DateTime.Now.ToString("d")); //2024/5/6
Console.WriteLine(DateTime.Now.ToString("D")); //2024年5月6日
Console.WriteLine(DateTime.Now.ToString("t")); //下午 11:21
Console.WriteLine(DateTime.Now.ToString("T")); //下午 11:21:59
Console.WriteLine(DateTime.Now.ToString("F")); //2024年5月6日 下午 11:23:38
Console.WriteLine(DateTime.Now.ToString("f")); //2024年5月6日 下午 11:23
Console.WriteLine(DateTime.Now.ToString("G")); //2024/5/6 下午 11:23:38
Console.WriteLine(DateTime.Now.ToString("M")); //5月6日
Console.WriteLine(DateTime.Now.ToString("Y")); //2024年5月
```


## 自訂 DateTime 格式化

2024/5/6 下午 11:23:38

| 格式  | 功能          | Format            | 執行結果                     |      補充   |
|:------|:--------------|:---------------:|:------------------|:-----------|
| `yy`  | 西元年後2位  	| `{0:yy}`  	    | 24     ||
| `yyyy`| 顯示西元年	| `{0:yyyy}`        | 2024 ||
| `MM`	| 月份          | `{0:MM}`      | 05 | 個位數月份前面會補0|
| `MMM` | 月份的縮寫名稱   | `{0:MMM}`	| 三月	|Mar ||
| `MMMM`| 月份的完整名稱   | `{0:MMMM}`	| 三月	|March||
| `dd`  | 日期           | `{0:dd}`     | 06 | 個位數日期前面會補0|
| `ddd` | 星期幾的縮寫   |	`{0:ddd}`   | 星期日	|Sun|
| `dddd`|星期幾的完整名稱 | `{0:dddd}`	| 星期日         |	Sunday|
| `hh`  | 小時（12 小時制）| `{0:hh}`   |	11      ||	
| `HH`  | 小時（24 小時制）| `{0:HH}`    |	23      ||
| `mm`  | 分鐘             | `{0:mm}`   |  38           ||
| `ss`  | 秒數           | `{0:ss}`      |	49          ||
| `tt`  | `A.M.`/`P.M.`   |	`{0:tt}`      |	下午        ||
| `:`   | 時間分隔符號 	    |`{0:hh:mm:ss}`	| 02:29:06	||
|`/`    | 日期分隔符號	    |`{0:yyyy/MM/dd}`	| 2024/05/06 ||
|       |               | `"yyyy/MM/dd tt hh:mm:ss"` | 2024/05/06 PM 04:01:57||


```c#
Console.WriteLine(DateTime.Now.ToString("yy")); //24
Console.WriteLine(DateTime.Now.ToString("yyyy")); //2024
Console.WriteLine(DateTime.Now.ToString("MM")); //05
Console.WriteLine(DateTime.Now.ToString("dd")); //06
Console.WriteLine(DateTime.Now.ToString("ddd")); // May
Console.WriteLine(DateTime.Now.ToString("dddd")); // Monday
Console.WriteLine(DateTime.Now.ToString("tt")); //PM
Console.WriteLine(DateTime.Now.ToString("hh:mm:ss")); //04:01:18
Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd tt hh:mm:ss")); //2024/05/06 PM 04:01:57
```

[MSDN -  標準數值格式字串](https://learn.microsoft.com/zh-tw/dotnet/standard/base-types/standard-numeric-format-strings)        
[[C#] string.Format 格式整理](https://marcus116.blogspot.com/2018/10/c-stringformat.html)