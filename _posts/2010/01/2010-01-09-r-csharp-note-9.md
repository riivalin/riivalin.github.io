---
layout: post
title: "[C# 筆記] 字段、屬性與對象(物件)封裝"
date: 2010-01-08 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,get-set]
---

## 字段、屬性的區別

[將 Point class的 x, y改成private](https://riivalin.github.io/posts/2010/01/r-csharp-note-8/)       

上節已經將x, y改成private，但我們需要從外部訪問這兩個字段，但還需要這兩個字段必須高度保持各自的私有隔離。

例如說，我們現在提出一個要求：經緯度都不可以是負數的。  
如果將`x`改成`public`，我們是可以定義的，但是如果我們一不小心給他賦值的時候，寫成了`point.x = -30`，那麼這個時候可能就會引起系統級別的錯誤。


所以我們在給橫坐標賦值前，應該做一些判斷和限制，而這些判斷和限制就需要借助 `get` 和 `set` 這兩個方法了。

從本質來說， `get` 和 `set`就是兩個公有的方法，

```c#
private int x;
public int X
{
    get { return this.x; }
    set
    {
        if (value < 0) throw new Exception("value不能小於0");
        this.x = value;
    }
}

//get set 簡寫(自動實現屬性)
 public int Y { get; set; }
```

## 對象(物件)封裝
以上的操作就是封裝，所謂的封裝，就是能夠隱藏對象的具體細節，


