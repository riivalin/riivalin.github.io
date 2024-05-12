---
layout: post
title: "[C# 筆記] 特殊字元處理：反斜線/、@符號"
date: 2021-03-20 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,char,特殊字元,跳脫字元,反斜線 /,"@ 符號",$]
---


## 特殊字元處理(\或@)

如果存放的文字剛好有「逸出字元((Escape Sequences))」要怎麼辦？      
兩個方法：

- `\`：加上「`\`」(單一反斜線)。`"C:\\windows\\system32"`
- `@`：前面加上「`@`」，會視為字串來處理。`@"C:\windows\system32"`

```c#
//C:\windows\system32
Console.WriteLine(@"C:\windows\system32"); //加上反斜線 \
Console.WriteLine("C:\\windows\\system32"); //加上 @
```

- `\r\n` 作用相當於 C# 的`Environment.NewLine`。

- `\n` 換行
- `\t` 類似 tab 鍵
- `\\` 反斜線
- `\'` 單引號
- `\"` 雙引號


## 字串中放入變數($)

- `$`：前面加上「`$`」，字串中加上大括弧`{ }`可以放入變數。

```c#
int i = 10;
int j = 9;
Console.WriteLine($@"C:\ {i}*{j}={i*j}"); //加上 $@
Console.WriteLine(@$"C:\ {i}*{j}={i*j}"); //加上 @$，同上，位置相反沒差
Console.WriteLine($"C:\\ {i}*{j}={i*j}"); //只加上$，\符號必須再加一個反斜線來跳脫字元，不然就要同上再加@

//以上三種，輸出結果都為:
//C:\ 10*9=90
```



[MSDN - Escape Sequences(逸出序列)](https://learn.microsoft.com/en-us/cpp/c-language/escape-sequences?view=msvc-170&redirectedfrom=MSDN)        
[MSDN - 使用 $ 的字串內插補點](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/tokens/interpolated)      
[[C# 筆記] 轉義符(反斜線)、@符號 by R](https://riivalin.github.io/posts/2011/01/escapes/)       
Book: Visual C# 2005 建構資訊系統實戰經典教本