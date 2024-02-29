---
layout: post
title: "[C# 筆記] IsNullOrEmpty、IsNullOrWhiteSpace"
date: 2023-06-01 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,"null"]
---

## String.IsNullOrEmpty
關於 `String.IsNullOrEmpty`     
表示指定的字串是否為 `null` 或`空字串 ("")`。

- 參數：欲檢驗的字串
- 回傳值：Boolean
- 從 .NET Framework 2.0 開始加入
- 用來檢驗傳入參數是否為 `null` 或 `空字串` 的方法

## String.IsNullOrWhiteSpace
關於 `String.IsNullOrWhiteSpace`        
表示指定的字串是否為 `null`、`空白`，或`只由空白字元組成的字串`。

- 參數：欲檢驗的字串
- 回傳值：Boolean
- 從 .NET Framework 4.0 開始加入
- 用來檢驗傳入參數是否為 `null`、`空字串` 或 `只有空白符號` 的方法

[MSDN - String.IsNullOrWhiteSpace(String) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.string.isnullorwhitespace?view=net-8.0)     
[MSDN - String.IsNullOrEmpty(String) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.string.isnullorempty?view=net-8.0#system-string-isnullorempty(system-string))        
[https://blog.yowko.com/string-isnullorempty-isnullorwhitespace/](https://blog.yowko.com/string-isnullorempty-isnullorwhitespace/)      