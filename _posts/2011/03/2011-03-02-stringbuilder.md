---
layout: post
title: "[C# 筆記] 高效的 StringBuilder"
date: 2011-03-02 22:13:00 +0800
categories: [Notes, C#]
tags: [C#,string,StringBuilder]
---

- StringBuilder高效的字串操作
    - 當大量進行字串操作的時候，比如，很多次的字串拼接操作。
    - String對象是不可變的。每次使用 System String類中的酪方法時，都要在內存中創建一個新的字串對象，就需要為該新對象分配新的空間。在需要對字串執行重複修改的情況下，與創建新的String 對象相關的系統開銷可能會非常大。如果要修改字串二不創建新的對象，則可以使用 System.Text StringBuilder類，例如，當在一個循環中將許多字串連接在一起時，使用StringBuilder類可以提升性能。
- StringBuilder!=String，將StringBuilder轉換為String用ToString().
- StringBuilder僅僅只是拼接字串的工具，大多數情況下還需要把StringBuilder轉換為String
    - StringBuilder sb = new StringBuilder()
    - sb.Append(); //追加字串
    - sb.Insert();
    - sb.Replace();

```c#
StringBuilder sb = new StringBuilder();
sb.Append("張三");
sb.Append("李四");
sb.Insert(1, 123);
sb.Replace("李四", "王五");
Console.WriteLine(sb.ToString());
Console.ReadKey();
```
