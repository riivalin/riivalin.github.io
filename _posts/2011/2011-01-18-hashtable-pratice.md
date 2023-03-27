---
layout: post
title: "[C# 筆記] Hashtable 集合練習"
date: 2011-01-18 21:29:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習：將用戶輸入的繁體字轉換成簡體字
```c#
using System.Collections;

private const string traditional = "您好啊很高興認識你"; //繁體
private const string simplified = "您好啊很高兴认识你"; //簡體

Console.WriteLine("請輸入您想要轉換的繁體字:");
string input = Console.ReadLine()!; //接收用戶輸入

//建立hashtable物件
Hashtable ht = new Hashtable();
//將每一個字添加到hashtable裡
for (int i = 0; i < traditional.Length; i++)
{
    ht.Add(traditional[i], simplified[i]); //key:value (繁:簡)
}

//遍歷每個輸入的字
for (int i = 0; i < input.Length; i++)
{
    //檢查hashtable裡有沒有input[i]的key值
    if (ht.ContainsKey(input[i]))
    {   //有就輸出對應的值
        Console.Write(ht[input[i]]);
    } else
    {   //沒有就輸出初值
        Console.Write(input[i]);
    }
}
```