---
layout: post
title: "[C# 筆記] ArrayList 的長度問題"
date: 2011-01-17 23:00:00 +0800
categories: [Notes, C#]
tags: [C#,arraylist]
---

## ArrayList 的長度問題
每次集合中實際包含的元素個數(count)超過了可以包含的元素容量(capacity)的時候，集合就會向內存中申請多開闢一倍的空間，來保証集合的長度一直夠用。  

- count 個數：表示這個集合中實際包含的元素的個數。  
- capacity 容量：表示這個集合中可以包含的元素的個素。  

## 程式驗証
### 第一次：沒有加任何元素時
Count：0  
Capactity：0  
```c#
using System.Collections;

ArrayList list = new ArrayList(); //建立 arraylist 物件
Console.WriteLine(list.Count); //0
Console.WriteLine(list.Capacity); //0
```

### 第二次：Add(1)
Count：1  
Capacity：4  
```c#
using System.Collections;

ArrayList list = new ArrayList(); //建立 arraylist 物件
list.Add(1);
Console.WriteLine(list.Count); //1
Console.WriteLine(list.Capacity); //4
```

### 第三次：再加一個 Add(1)
Count：3  
Capacity：4  
```c#
using System.Collections;

ArrayList list = new ArrayList(); //建立 arraylist 物件
list.Add(1);
list.Add(1);
Console.WriteLine(list.Count); //3
Console.WriteLine(list.Capacity); //4
```
### 第四次：共三個 Add(1)
Count：6  
Capacity：48  
```c#
using System.Collections;

ArrayList list = new ArrayList(); //建立 arraylist 物件
list.Add(1);
list.Add(1);
list.Add(1);
Console.WriteLine(list.Count); //6
Console.WriteLine(list.Capacity); //8
```
