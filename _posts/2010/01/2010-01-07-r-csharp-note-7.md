---
layout: post
title: "[C# 筆記] 構造方法與方法重載"
date: 2010-01-07 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,Constructor,this,overload]
---


[Point class](https://riivalin.github.io/posts/2010/01/r-csharp-note-6/)   
`Point class`在進行實體化、初始化的時候，程式碼結構看起來非常分散，比較凌亂，現在就來解決這個問題。

實體化的同時，完成對坐標點a數據的初始化：

```c#
//類的實體化
Point a = new Point 
{
    x = 15, //數據的初始化
    y = 10
}; 
```
這種方法直接在 `new` 一個物件的時候，使用大括號來初始化數據的過程，叫做「物件初始化值 `Object Initializer`」，這種方法是C# 獨有的，更普遍的方法使用的是「構造函數」，或者叫做「構造方法 Constructor」。

## 構造方法 Constructor

構造方法語法：  
訪問修飾符 類別名稱() { }   
public Point() { }  

```c#
public class Point
{
    public Point()
    {
        x = 15;
        y = 10;
    }
    ... ...
}
```

這段程式碼是什麼意思呢？    
其實就是在告訴編譯器，在我們 `new` 一個新的 `Point` 物件的時候、實體化`Point` 物件同時對它進行「初始化賦值」。

賦值的數據：`x軸`默認為15，`y軸`默認為10，有了構造方法後，我們就不需要再給它的x和y軸賦值了。(`b.x = 20; b.y = 30;`就可以拿掉了)

```c#
Point b = new Point();
double result = a.GetDistance(b);
```
那麼，我們在初始化的時候，可不可以在構造函數中，自定義`x軸`和`y軸`的數據呢？當然是可以的，我們給構造函數追加兩個參數就解決了。

我們可以使用`this`來解決命名衝突，     
使用 `this` 關鍵字來引導內部的成員變量，`this`代表的就是這個類別本身。  
而沒有使用 this的則屬於外部輸入的數據，從參數過來的數據。

```c#
public class Point
{
    public Point(int x, int y)
    {
        this.x = x; //this代表的就是這個類別本身
        this.y = y;
    }
    ... ...
}
```

這樣我們就可以把想要初始化的數據，傳遞給小括號中的參數了

```c#
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    //dynamic var
    Point a = new Point(15, 10);
    a.DrawPoint();

    Point b = new Point(22, 10);
    double result = a.GetDistance(b);
    Console.WriteLine(result);

    Console.Read();
}
```

## 方法重載 Overload
既然我們可以創建默認的構造方法，也可以創建自定義的構造方法，那麼我們可不可以既保留默認構造方法，同時也保留自定義構造方法呢？當然可以，我們直接把沒有參數的構造方法搬回來就可以了。

```c#
public class Point
{
    public int x;
    public int y;

    public Point()
    {
        this.x = 15;
        this.y = 10;
    }
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
```

在C# 中，一個 class 可以支持多個不同的構造方法，不過要注意的是：不同構造方法之間，他們的參數數量、或者是參數的類型，必須有所區別。

這種名稱一致，但是參數有所區別的方法聲明，就叫做方法的重載。    

從原理來說，構造方法的底層設計依然是一個方法，只不過是在這個類實體化的同時，同時被調用而已。

所以基於方法設計的原理，構造方法同樣可以通過參數的數量和類型區別，來進行重載。

現在我們看到`Point class`中，有兩個不同的構造函數，這就是一個方法重載非常典型的例子。

所以根據方法重載的原理，我們還可以創建一個只包含一個參數的構造方法。    

```c#
public class Point
{
    public int x;
    public int y;

    public Point()
    {
        this.x = 15;
        this.y = 10;
    }
    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Point(int x)
    {
        this.x = x;
        this.y = 10;
    }
    ... ...
}
```

## Code

```c#
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    Point a = new Point(15, 10);
    a.DrawPoint();

    Point b = new Point(22, 10);
    double result = a.GetDistance(b);
    Console.WriteLine(result);

    Console.Read();
}
public class Point
{
    public int x;
    public int y;

    public Point()
    {
        this.x = 15;
        this.y = 10;
    }
    public Point(int x, int y)
    {
            this.x = x;
            this.y = y;
    }
    public Point(int x)
    {
        this.x = x;
        this.y = 10;
    }

    //輸出坐標點的數據
    public void DrawPoint() =>
        Console.WriteLine($"左邊點為 x: {x}, y: {y}");

    //兩點之間的距離
    public double GetDistance(Point p) =>
        Math.Pow(x - p.x, 2) + Math.Pow(y - p.y, 2);
}
```
        
[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=20](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=20)