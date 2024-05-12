---
layout: post
title: "[C# 筆記] System.Convert 轉換"
date: 2021-02-22 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,System.Convert,"null"]
---

# 資料型別的轉換方法

資料型別的三種轉換方法：
- `System.Convert`轉換
- `型別.Parse`方法
- 指定轉換(`Cast`)

由於C# 是強型別(Strongly-Typed)而非弱型別(Weakly-Typed)，所以在執行時期(Runtime)資料型別通常需要明確地宣告才能通過編譯器的嚴苛考驗。


# System.Convert 轉換

`System.Convert`類別主要作用在於將基底資料型別轉換為其他的型別資料。

```c#
string s = "1234";
int number = Convert.ToInt32(s); //將字串變數轉換成32位元整數型態
```


# Convert.ToInt32 對 null 的處理

`Convert.ToInt32(null)`會返回`0`不會產生任何異常。      
而`int.Parse(null)`會產生異常、產生Exception

```c#
Convert.ToInt32(null); //會返回0，不會產生任何異常。
int.Parse(null); //會產生異常、產生Exception
```


# Convert.ToInt32 對小數點的處理(四捨六入五成雙)

`Convert.ToInt32 `有小數點比較特殊：
- `.1~.4` 的話會「捨去」
- `.6~.9` 的話會「進位」
- `.5` 的進位與否是取決於：如果進位之後是該數是「偶數」的話就「進位」，是「奇數」的話就「捨去」

```c#
int x = Convert.ToInt32(7.1); //結果為 7  
int x = Convert.ToInt32(7.5); //結果為 8  
int x = Convert.ToInt32(7.9); //結果為 8  
int x = Convert.ToInt32(8.1); //結果為 8  
int x = Convert.ToInt32(8.5); //結果為 8  
int x = Convert.ToInt32(8.9); //結果為 9
```

使用這個函數時會有一個比較特別的現象，如果說 .1 ~ .4 的話會捨去，.6~.9 的話會進位，而 .5 的進位與否是取決於如果進位之後是該數是偶數的話就進位，是奇數的話就捨去。       
        
以上面的範例來看，7.5 進位的話為 8，所以就進位，8.5 進位的話為 9，所以就捨去，這個現象又叫做「四捨六入五成雙」。



[[C# 筆記](int)、Convert.ToInt32、int.Parse、int.TryParse   by R](https://riivalin.github.io/posts/2011/02/convert-parse/)      
Book: Visual C# 2005 建構資訊系統實戰經典教本 