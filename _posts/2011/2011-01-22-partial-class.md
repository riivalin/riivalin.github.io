---
layout: post
title: "[C# 筆記] Partial 部分類別"
date: 2011-01-22 22:49:00 +0800
categories: [Notes, C#]
tags: [C#]
---
## Partial 部分類

在同一個命名空間下，可以寫兩個相同的類名嗎？  
不行，但是還是得需要，怎麼辦？    

## 在類別前面加上 `partial`  
```c#
public partial class Person { }
public partial class Person { }
```
這樣代表什麼？
這兩個類都是Person的一部份 
這兩個部分類，就共同組成Person類別  

好處是什麼？  
兩邊寫的成員，都可以共用  
但不能重複方法，方法重載可以    

## Partial 部分類別的成員都可以共用
```c#
public partial class Person {
    private string name;  //private也可以共用
    //public void Test() { } //但不能重複方法 
    public void Test(string name) { }  //方法重載可以
}
public partial class Person {
    public void Test() {
        name = "Riii";
    }
}
```

