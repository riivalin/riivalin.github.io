---
layout: post
title: "[C# 筆記] 訪問修飾符 Access Modifier"
date: 2010-01-08 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R]
---

## 訪問修飾符 Access Modifier

像這種在物件外部直接訪問內部成員變量的方法，甚至是重新賦值的操作，在我們實際工作中都是非常危險的，所以一般來說，我們希望能夠儘量避免外界直接操作classn內部的成員變量、內部的字段，那麼這個時候我們就需要使用 Access Modifier訪問修飾符，來對訪問class內部成員變量、或者內部方法加以限制。

在C#中，我們最常見的訪問修飾符： `public`, `private`, `protected`, `internal`，其中 `public`和 `private`是最常見的。

現在將 `Point class`的 x, y改成`private`，可以防止我們的對象向外部環境暴露太多的內部信息。

```c#
public class Point
{
    private int x;
    private int y;

    public Point()
    {
        x = 15;
        y = 10;
    }
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Point(int x)
    {
        this.x = x;
        y = 10;
    }
    ... ...
}
```