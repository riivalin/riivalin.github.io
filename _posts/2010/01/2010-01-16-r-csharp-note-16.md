---
layout: post
title: "[C# 筆記] Abstract 抽象類與抽象成員"
date: 2010-01-16 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,abstract,多型]
---

只有抽象概念，就需要使用`abstract` 關鍵詞，代表的是，我們不需要在基類(父類)中實現這個方法。     

而具體的實現，只能在派生類(子類)中處理。

## abstract 關鍵詞
- 聲明修飾符
- 可以修飾類、方法、屬性
- 只有聲明、沒有邏輯，不會被實現

## 語法
注意：如果我們給一個類的方法或者屬性加上了 `abstract` 這個關鍵詞，整個類也必須要使用 `abstract`。   

如果我們使用了 `abstract` 這個關鍵詞，在做方法重寫的時候，`virtual` 這個關鍵詞就可以不需要使用了。

```c#
//基類/父類
public abstract class Shape {
    public abstract void Draw();
}
//派生類/子類
public class Circle:Shape {
    public override void Draw() {
        ......
    }
}
```

## 抽象規則
- 聲明為 `abstract`之後，這個屬性，或是方法是不可以有程式碼實現(沒有方法體)
- 當某個成員被聲明為 `abstract`，整個 `class` 都需要被聲明為抽象類
- 派生類(子類)必須實現抽象類中所聲明的所有抽象方法和抽象屬性
- 抽象類不可以被實例化(實體化)
(不可以使用 `new` 來創建對象(物件)的)

## 為什麼要使用抽象
- 只是一種概念，一種設計方法的集中體現
- 規範程式碼的開發設計思路
- 從業務層面上避免錯誤出現

例如，如果 `Shape` 的 `Draw()`不是一個抽象方法，而是一個虛方法 `virtual`，那麼在circle類中，我們是可以不對這個繪製方法進行重寫的，在程序執行過程中，必然會出現業務邏輯上的錯誤。        

所以，使用抽象類、抽象成員不僅可以提高程式碼的開從發效率，還可以規範程式碼的開發設計思路，從業務層面上避免錯誤的可能性。

## 練習
- 將 Shape 的`virtual` 方法改成抽象方法`abstract`、改成抽象類
- 再加一個 畫橢圓 class

```c#
public abstract class Shape
{
    public abstract void Draw();
}
public class Cicle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("畫圓");
    }
}
public class Rectangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("畫方");
    }
}

public class Triangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("畫三角形");
    }
}
public class Oval : Shape
{
    public override void Draw()
    {
        Console.WriteLine("畫橢圓");
    }
}
public class Canvas
{
    public void DrawShape(List<Shape> shapes)
    {
        foreach (var shape in shapes)
        {
            shape.Draw();
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        var shapeList = new List<Shape>(); //創建圖案列表
        shapeList.Add(new Cicle()); //加入圓形
        shapeList.Add(new Rectangle()); //加入方形
        shapeList.Add(new Triangle()); //添加三角形
        shapeList.Add(new Oval()); //添加橢圓形

        var canvas = new Canvas(); //創建畫布物件
        canvas.DrawShape(shapeList); //調用畫圖方法

        Console.Read();
    }
}
```

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=35](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=35)