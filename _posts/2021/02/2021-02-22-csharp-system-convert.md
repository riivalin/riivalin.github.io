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


[[C# 筆記](int)、Convert.ToInt32、int.Parse、int.TryParse by R](https://riivalin.github.io/posts/2011/02/convert-parse/)