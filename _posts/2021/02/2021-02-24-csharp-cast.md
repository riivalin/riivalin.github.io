---
layout: post
title: "[C# 筆記] 指定轉換(Cast)"
date: 2021-02-24 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,指定轉換(Cast),(int),強制轉換]
---

# 資料型別的轉換方法

資料型別的三種轉換方法：
- `System.Convert`轉換
- `型別.Parse`方法
- 指定轉換(`Cast`)

由於C# 是強型別(Strongly-Typed)而非弱型別(Weakly-Typed)，所以在執行時期(Runtime)資料型別通常需要明確地宣告才能通過編譯器的嚴苛考驗。


# 指定轉換(Cast)

指定轉換(Cast)就是將資料型別，強制轉換成指定的資料型別。

此種轉換的能力最為強大，所以這種強制轉換的方式又稱為「鑄型(`Cast`)」。      

## 語法

```c#
變數名稱1 = (變數名稱1的型別) 變數名稱2;
```

```c#
double i = 9.9;
int j = (int)i; //cast轉換
```

`(int)`適合「簡單數據類型」之間的轉換，有小數時會直接捨去。

# 常犯的錯誤

在使用指定轉換(`Cast`)最常犯的錯誤就是 **「轉換的內容是字串型態」**。       

```c#
string s = "99.8";
int number = (int)s; //報錯，Cannot convert type 'string' to 'int'
```

上面程式的錯誤為，用指定轉換強制要將 變數s 轉換成 int 型態時，就會發生「string型別無法轉換」的錯誤訊息。


# (int) 對小數點的處理

- `(int)` 強制轉型，有小數時會直接捨去。

```c#
int x = (int)99.5; //結果為 99
int x = (int)99.1; //結果為 99
```



[[C# 筆記](int)、Convert.ToInt32、int.Parse、int.TryParse by R](https://riivalin.github.io/posts/2011/02/convert-parse/)