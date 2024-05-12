---
layout: post
title: "[C# 筆記] 隱含轉換 & 明確轉換 (Implicit conversion & Explicit conversion)"
date: 2021-02-09 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,隱含轉換,明確轉換,自動轉換,強制轉換,Implicit Conversion,Explicit Conversion]
---

數值資料型別轉換有二種方式：
- 隱含轉換(Implicit Conversion)：記憶口訣「小轉大」(自動轉換)
- 明確轉換(Explicit Conversion)：記憶口訣「大轉小」(強制轉換)


## 隱含轉換(Implicit Conversion)

「小轉大」(自動轉換)、資料不會失真。        

「隱含轉換」就是將值域範圍較小的資料型別轉換成值域範圍較大的資料型別，由於此種轉換方式是由系統自動處理，故又稱「自動轉換」，並且在轉型之後資料不會因此而失真。例如：`int`轉`double`. 

```c#
int i = 99;
double j = i; //進行隱含轉換，資料不會因此失真
```

反之，資料型別值域較大的`double`轉換成`int`需要透過「明確轉換」才能辦到。       
所以下面程式碼無法進行編譯：

```c#
double i = 99.5;
int j = i; //error: 型別double不能隱含轉換為int。已有明確轉換存在(您是否漏掉了轉型？)
```


## 明確轉換(Explicit Conversion)

「大轉小」(強制轉換)、資料可能會失真。        

「明確轉換」就是將值域範圍較大的資料型別強制轉換成值域範圍較小的資料型別，又稱為「強制轉換」，在轉型之後資料通常會有失真的問題。        

C# 的明確轉換方式有：
- `System.Convert`轉換
- 指定轉換(`Cast`)

例如：`double`轉`int`：

```c#
double i = 99.5;
int j = (int)i; //99, cast轉換，明確轉換資料會失真
int k = Convert.ToInt32(i); //100, convert轉換，明確轉換資料會失真

Console.WriteLine(j); //99
Console.WriteLine(k); //100
```

## Note

- `Cast` 明確轉換是「無條件捨去」小數點部分數字
- `Convert` 明確轉換是「四捨五入」來計算        

在執行「隱含轉換」或「明確轉換」時，需要注意：
- 從 `int`轉換至 `float`可能會遺失精確度，但是不會遺失範圍。
- `float`型別與`decimal`型別之間沒有「隱含轉換」。
- 當`double`或`float`轉換成整數類資料型別時，其值會被截斷。
- 明確數值轉換可能會遺失小數位數或造成擲回例外狀況。
- `char`型別沒有「隱含轉換」。      


[[C# 筆記] 明確轉換(顯式轉換) by R](https://riivalin.github.io/posts/2021/01/explicit-cast/)      
[[C# 筆記] 隱含轉換(隱式轉換) by R](https://riivalin.github.io/posts/2021/01/implicit-cast/)     
Book: Visual C# 2005 建構資訊系統實戰經典教本    