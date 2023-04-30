---
layout: post
title: "[C# 筆記](int)、Convert.ToInt32、int.Parse、int.TryParse"
date: 2011-02-10 00:02:29 +0800
categories: [Notes,C#]
tags: [C#,R,(int),Convert.ToInt32,int.Parse,int.TryParse]
---

## Convert.ToInt32、(int) 和 int.Parse 三者的區别：

- `Convert.ToInt32`適合將 `object` 類型轉換成 `int`，例如 `Convert.ToInt32(session["rii"])`
- `(int)`適合簡單數據類型之間的轉換，有小數時會直接捨去
- `int.Parse`適合將 `string` 類型轉換成 `int`類型，例如 `int.Parse("999")`。如果有小數點，就會報錯 =>`int.Parse("4.5") //報錯`


## 對null的處理- Convert.ToInt32(null) & int.Parse(null)
`Convert.ToInt32(null)`和`int.Parse(null)`最大的不同是他們對 `null`值的處理方法：
- `Convert.ToInt32(null)`會返回0，不會產生任何異常
- `int.Parse(null)`會產生異常、產生Exception

## 小數點
- `(int)` 強制轉型，有小數時會直接捨去。
- `int.Parse` 有小數點會報錯，發生異常。
- `Convert.ToInt32` 有小數點比較特殊，.1~.4 的話會捨去，.6~.9 的話會進位，而 .5 的進位與否是取決於如果進位之後是該數是偶數的話就進位，是奇數的話就捨去

### (int)
`(int)` 強制轉型，有小數時會直接捨去

```c#
int x = (int)3.99; //結果為 3
int x = (int)3.1; // 結果為 3
```

### Convert.ToInt32
```c#
int x = Convert.ToInt32(7.1); //結果為 7  
int x = Convert.ToInt32(7.5); //結果為 8  
int x = Convert.ToInt32(7.9); //結果為 8  
int x = Convert.ToInt32(8.1); //結果為 8  
int x = Convert.ToInt32(8.5); //結果為 8  
int x = Convert.ToInt32(8.9); //結果為 9
```
使用這個函數時會有一個比較特別的現象，如果說 .1 ~ .4 的話會捨去，.6~.9 的話會進位，而 .5 的進位與否是取決於如果進位之後是該數是偶數的話就進位，是奇數的話就捨去。       

以上面的範例來看，7.5 進位的話為 8，所以就進位，8.5 進位的話為 9，所以就捨去，這個現象又叫做 四捨六入五成雙。

## int.TryParse()
使用時機，用以確認是否轉換成功，若回傳false，則需要有額外的邏輯去處理它。

int.tryParse 其實是一個方法。   
他是`out`參數的方法，需要宣告變數來接收返回的值，用來接收的變數，不必初始化給值
```
int.tryParse(參數1, out 參數2)
第一個參數：要轉換的參數 
第二個參數：返回結果的參數
```
轉換成功，返回對應的數字，失敗，返回0

### 轉換成功
```c#
int number; //用來接收out參數返回的值
bool result = int.tryParse("123", out number); //number是方法的參數返回值
//123轉換int成功：
//result = true 
//number = 123
```

### 轉換失敗
```c#
int number; //轉換失敗，返回0
bool b = int.tryParse("123abc", out number); //number是方法的參數返回值
//output: 
//result = false
//number = 0
```

## R Note
- `int.Parse`只能轉換 `string` 類型
- 有小數就不能用`int.Parse`會報錯
- `(int)`有小數會直接捨去
> 四括五入 => `Math.Round`加第二個參數`MidpointRounding.AwayFromZero`



- [Convert.ToInt32、(int)和int.Parse三者的区别2008-06-26 07:01 P.M.Convert.ToInt32、(int)和int.Parse三者的区别](https://www.cnblogs.com/flyker/archive/2009/03/04/1402673.html)
- [[C# 筆記] int.Parse & int.tryPase](https://riivalin.github.io/posts/int-parse/)
- [C# - 數字轉換和四捨五入](https://blog.cashwu.com/blog/csharp-number-convert)