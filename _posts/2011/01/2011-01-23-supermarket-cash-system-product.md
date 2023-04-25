---
layout: post
title: "[C# 筆記] draft 超市收銀系統-商品類"
date: 2011-01-23 23:39:00 +0800
categories: [Notes, C#]
tags: [C#,base,this]
---

## 超市收銀系統-商品類
- 商品

```text
ProductFather  Price  Count  ID

商品 價格 數量 編號
Acer 價格 數量 編號(唯一的不重複)
三星手機
香蕉
醬油
```

- 倉庫 1. 儲存貨物 2. 提貨 3. 進貨  
- 收銀 超市

## Guid.NewGuid()產生不重複的編號
```c#
Guid.NewGuid(); 
```

## 程式碼
```c#
//超市收銀系統

//產生一個不會重複的編號
Console.WriteLine(Guid.NewGuid());


/// <summary>
/// 商品的父類
/// </summary>
internal class ProductFather
{
    public string ID { get; set; } //Guid.NewGuid()
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }

    public ProductFather(string id, string name, decimal price, int count)
    {
        this.ID = id;
        this.Name = name;
        this.Price = price;
        this.Count = count;
    }

    public ProductFather(string id, string name, decimal price)
        : this(id, name, price, count: 0) //調用自己的構造函數
    {
    }
}
/// <summary>
/// Acer筆電
/// </summary>
internal class Acer : ProductFather
{
    public Acer(string id, string name, decimal price)
        : base(id, name, price) //調用父類的構造函數
    {
    }
}
/// <summary>
/// 三星
/// </summary>
internal class SamSung : ProductFather
{
    public SamSung(string id, string name, decimal price)
        : base(id, name, price)
    {
    }
}
/// <summary>
/// 醬油
/// </summary>
internal class Oil : ProductFather
{
    public Oil(string id, string name, decimal price)
        : base(id, name, price)
    {
    }
}
/// <summary>
/// 香蕉
/// </summary>
internal class Banana : ProductFather
{
    public Banana(string id, string name, decimal price)
        : base(id, name, price)
    {
    }
}
```

