---
layout: post
title: "[C# 筆記] 虛方法(Virtual) vs 方法重寫(Override)"
date: 2010-01-14 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,override,virtual,多型]
---

```
父類/基類
子類/派生類/衍生類
```

## 方法重寫 Override
在 `class` 繼承過程中，有時候我們需要修改基類的某些方法的執行邏輯，這個時候我們就需要使用方法重寫 `Override` 這個關鍵詞了，使`Override` 以後，可以對基類的某些指定的方法進行重新實現，重寫這些方法的內部程式碼邏輯。

- 修改基類的某些方法的執行邏輯
- 使用 `Override` 關鍵詞

## 範例
例如：Shape class 定義了圖像的大小以及位置，通過Area 方法可以計算出圖案的面積，一般來說，面積就是「長*寬」。

```c#
public class Shape {
    Area() = () => 長*寬
}
```

基於 Shape 類，我們還有兩個派生類(子類)：圓形、平行四邊形，很明顯，這兩個類是沒有辦法直接繼承使用基類(父類) Shape的面積計算公式的。     

計算圓面積應該是：`pi*r^2`，而平行四邊形的面積計算公式則是：`底*高`。       

```c#
public class Circle:Shape {
    Area() = () => pi*r^2
}
public class Parallelogram:Shape {
    Area() = () => 底*高
}
```

對於這種情況，我們在繼承的時候，就需要按照實真的需要來重新定義：圓形和平行四邊形的面積公式了。      

## 虛方法 Virtual

這個時候，我們首先需要在基類(父類)中使用 `virtual`來聲明方法，  

```c#
public class Shape {
    virtual Area() = () => 長*寬
}
```
## 方法重寫 Override

在派生類中(子類)加上 override 關鍵詞來修飾需要重寫的方法。  

```c#
public class Circle:Shape {
    override Area() = () => pi*r^2
}
public class Parallelogram:Shape {
    override Area() = () => 底*高
}
```

## 重寫的作用(實現多態/多型)

重新定義可以給我們很大的便利，其中最重要的就是，可以在C#程式碼中實現「多態/多型」

- 實現多態(多型)


## 學習「多態/多型」重要的概念

```c#
//基類/父類
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("繪製圖案");
    }
}
//派生類/子類:圓形
public class Cicle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("畫圓");
    }
}
//派生類/子類:方形
public class Rectangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("畫方");
    }
}
//畫布
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

        var canvas = new Canvas(); //創建畫布物件
        canvas.DrawShape(shapeList); //調用畫圖方法

        Console.Read();
    }
}
```

儘管我們調用的是Shape基類(父類)中的繪圖方法，可是在程式運行過程中，編譯器卻能夠準確地識別出當前派生類(子類)的類型，並且執行派生類(子類)中的方法，而不是基類(父類)中的方法。     

第一個圓形執行畫圖，第二個方形執行畫方。        

對於這種，調用同一類，卻產生不同表現形式的過程，就稱它為「多態(多型)」。        

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=33](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=33)
