---
layout: post
title: "[C# 筆記] 自動屬性 & 普通屬性"
date: 2011-01-23 21:29:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 自動屬性 & 普通屬性

「自動屬性」有什麼不一樣啊？  
什麼稱它為自動屬性呢？  
用反編譯看，雖然我不寫field(欄位/字段)，但編譯後依然會自動給我們生成field(欄位/字段)。    

所以「自動屬性」和「普通屬性」基本上沒什麼區別，本質上就是同一個東西。  
只是體驗、寫法不同而己。一個有field(欄位/字段)、有field(欄位/字段)的方法體，另一個只有get,set。
    
「自動屬性」寫起來比較省事點，以後就用「自動屬性」來寫就可以了。    

如果要對「屬性」進行限定，可以在「構造函數」裡面做就可以了。

## 自動屬性
```c#
public int Age { get; set; }
```
`get`、`set`本質就是兩個函數。

## 普通屬性
```c#
private string _name; //field欄位/字段。用來保護屬性的
public string Name //屬性
{
    get { return _name; }
    set { _name = value;}
}
```
## 範例
```c#
public class Person
{
    //普通屬性
    private string _name; //field欄位/字段。用來保護屬性的
    public string Name //屬性
    {
        get { return _name; }
        set { _name = value;}
    }

    //自動屬性
    public int Age { get; set; }
}
```  
「自動屬性」寫起來比較省事點，以後就用「自動屬性」來寫就可以了。    

如果要對「屬性」進行限定，可以在「構造函數」裡面做就可以了。