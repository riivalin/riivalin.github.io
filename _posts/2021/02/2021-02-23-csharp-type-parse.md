---
layout: post
title: "[C# 筆記] 型別.Parse 方法"
date: 2021-02-23 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,型別.Parse,Parse(),"null"]
---

# 資料型別的轉換方法

資料型別的三種轉換方法：
- `System.Convert`轉換
- `型別.Parse`方法
- 指定轉換(`Cast`)

由於C# 是強型別(Strongly-Typed)而非弱型別(Weakly-Typed)，所以在執行時期(Runtime)資料型別通常需要明確地宣告才能通過編譯器的嚴苛考驗。


# 型別.Parse 方法

**`Parse()`方法**主要將「指定字串」的內容轉換成指定的型別資料。     

這個意思就是表示`Parse()`方法主要處理對象是「字串(`String`)」。     

```c#
string s = "1999-09-09";
DateTime dt = DateTime.Parse(s);
```

# 常犯的錯誤

在使用`Parse()`方法最常犯的錯誤就是 **「轉換的內容不是字串(`String`)」**。

以下錯誤範例：

```c#
float score = 99.0f;
double newScore = double.Parse(score); //報錯
```

上面的錯誤為：score 是`float`型態並不是`string`型態，所以進行`double.Parse(score)`方法時就會無法進行編譯而產生「引數無效」的錯誤。      

將程式改成下面，就能順利執行編譯：

```c#
float score = 99.0f;
double newScore = double.Parse(score.ToString()); //score改成string型態
```

# int.Parse() 對 null 的處理
  
`int.Parse(null)`會產生異常、產生`Exception`，      
而`Convert.ToInt32(null)`會返回`0`不會產生任何異常。    

```c#
int.Parse(null); //會產生異常、產生Exception
Convert.ToInt32(null); //會返回0，不會產生任何異常。
```

# int.Parse() 對小數點的處理

`int.Parse` 有小數點會報錯，發生異常。

# 結論

- `int.Parse()`只能轉換 `string` 類型。
- `int.Parse(null)`會產生異常、產生`Exception`， 
- 有小數就不能用`int.Parse()`，會報錯。例如：`int.Parse("4.5"); //報錯`



[[C# 筆記](int)、Convert.ToInt32、int.Parse、int.TryParse   by R](https://riivalin.github.io/posts/2011/02/convert-parse/)      
Book: Visual C# 2005 建構資訊系統實戰經典教本