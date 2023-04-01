---
layout: post
title: "[C# 筆記] 超市收銀系統-商品類"
date: 2011-01-23 23:39:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 超市收銀系統-商品類

```text
ProductFather   Price  Count  ID

商品 價格 數量 編號

Acer 價格 數量 編號(唯一的不重複)
三星手機
香蕉
醬油
```

```c#
//超市收銀系統

//產生一個不會重複的編號
using System.ComponentModel;
Console.WriteLine(Guid.NewGuid());
Console.WriteLine(Guid.NewGuid());


/// <summary>
/// 商品的父類
/// </summary>
public class ProductFather
{
    public ProductFather(string id, double price, double count)
    {
        this.ID = id;
        this.Price = price;
        this.Count = count;
    }
    public string ID { get; set; } //Guid.NewGuid()
    public double Price { get; set; }
    public double Count { get; set; }
}

/// <summary>
/// 三星
/// </summary>
public class SamSung : ProductFather
{
    public SamSung(string id, double price, double count)
        : base(id, price, count)
    {
    }
}
/// <summary>
/// Acer筆電
/// </summary>
public class Acer : ProductFather
{
    public Acer(string id, double price, double count)
        : base(id, price, count)
    {
    }
}
/// <summary>
/// 香蕉
/// </summary>
public class Banana : ProductFather
{
    public Banana(string id, double price, double count)
        : base(id, price, count)
    {
    }
}
/// <summary>
/// 醬油
/// </summary>
public class 醬油 : ProductFather
{
    public 醬油(string id, double price, double count)
        : base(id, price, count)
    { }
}
```