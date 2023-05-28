---
layout: post
title: "[C# 筆記] 對象(物件)聚合Cohesion-高內聚、低耦合"
date: 2010-01-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class]
---

[寫一個方法來輸出`2d 坐標點：{x:15, y:10}`這個坐標點的數據](https://riivalin.github.io/posts/2010/01/r-csharp-note-5/)  

經過class的處理，我們完美的規避了程序中可能會出現的風險，同時也對數據做出了準確的限制。

根據面向對象(物件導向)的原則，我們添加了對象(物件)、添加了類以後，也就引起了另外一個問題，就是對象(物件)的聚合問題，也就是我們經常聽到的「高內聚、低耦合」的說法。

什麼是「高內聚、低耦合」呢？簡單來說，就是功能相關的事物，應該放在同一個集合中形成一個模塊，這就叫做「高內聚」，而這些模塊之間又應該是相互獨立的，不同的模塊之間，應該保持一個「低耦合」的狀態。

所以在我們的`Point class` 這個例子中，我們定義它的兩個坐標點，然後有一個獨立的函數`DrawPoint()`來輸出這個坐標點，從面向對象(物件導向)的角度來說，坐標點作為一個獨立的對象(物件)，與輸出坐標應該是聚合的關係。

所以，當我們把`DrawPoint()`方法與坐標點`Point`分割的時候，就違背了聚合的原則。

比如說，我們的需求發生了變化，不僅需要我們可以輸出當前的坐標點
，甚至需要我們輸出兩點之間的距離，那麼我們是不是又需要在不知道什麼地方，添加一個新的函數、一個新的方法。

```c#
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    //dynamic var
    Point a = new Point(); //類的實體化
    a.x = 15; //數據的初始化
    a.y = 10;
    DrawPoint(a);

    Point b = new Point();
    b.x = 20;
    b.y = 30;
    GetDistance(a, b);

    Console.Read();
}
public class Point
{
    public int x;
    public int y;
}
//輸出坐標點的數據
public static void DrawPoint(Point point) =>
    Console.WriteLine($"左邊點為 x: {point.x}, y: {point.y}");

//兩點之間的距離
public static double GetDistance(Point a, Point b) =>
    Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2);
```

那麼，這個能夠統一管理所有程式碼的集合，就是`class`，對於`DrawPoint`、`GetDistance`這兩個方法，它們都與坐標點 `Point`直接相關，所以我們應該把他們這兩個方法也集中放到`Point`類中。

`DrawPoint`在`Point`類中，它將會是一個成員方法，依然不用返回數據，所以方法類型為`void`，因為它需要外部訪問，所以加上 `public`。

接下來要注意了，因為這個方法輸出的是當前坐標點，也就是我自己這個坐標點的信息，所以不需要傳入整合的參數`public void DrawPoint()`，而在成員方法中，又是可以隨意訪問它的成員變量的，所以在 `Console.WriteLine`中不需要`Point.x`，而直接使用 `x` 和 `y`就足夠了。

```c#
//輸出坐標點的數據
public void DrawPoint() =>
    Console.WriteLine($"左邊點為 x: {x}, y: {y}");
```

而 `GetDistance`是計算兩點之間的距離，我們需要傳入的是「對方的坐標點信息」`GetDistance(Point p)`，計算過程，我們需要計算自己的 x點和 p.x點 之間的距離，同時也需要計算自己的 y點和 對方y點 之間的距離。

```c#
//兩點之間的距離
public double GetDistance(Point p) =>
        Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2);
```

現在可以把多餘的程式碼刪除了

```c#
public class Point
{
    public int x;
    public int y;

    //輸出坐標點的數據
    public void DrawPoint() =>
        Console.WriteLine($"左邊點為 x: {x}, y: {y}");

    //兩點之間的距離
    public double GetDistance(Point p) =>
            Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2);
}
```

回到內函數，調用

```c#
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    //dynamic var
    Point a = new Point(); //類的實體化
    a.x = 15; //數據的初始化
    a.y = 10;
    a.DrawPoint();//輸出坐標點的數據

    Point b = new Point();
    b.x = 20;
    b.y = 30;
    double result = a.GetDistance(b);//計算兩點之間的距離
    Console.WriteLine(result);

    Console.Read();
}
public class Point
{
    public int x;
    public int y;

    //輸出坐標點的數據
    public void DrawPoint() =>
        Console.WriteLine($"左邊點為 x: {x}, y: {y}");

    //計算兩點之間的距離
    public double GetDistance(Point p) =>
        Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2);
}
```
輸出：  
左邊點為 x: 15, y: 10   
425 

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=19](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=19)