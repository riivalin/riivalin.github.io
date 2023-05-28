---
layout: post
title: "[C# 筆記] 類別class和實體instance"
date: 2010-01-05 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,dynamic,class]
---

## 實體instance

寫一個方法來輸出`2d 坐標點：{x:15, y:10}`這個坐標點的數據：

```c#
//2d 坐標點：{x:15, y:10}
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    DrawPoint(15, 10);
    Console.Read();
    return;
}

public static void DrawPoint(int x, int y) =>
    Console.WriteLine($"左邊點為 x: {x}, y: {y}");
```

以上範例，經度x和緯度y應該被看作一個統一的整體，所以我們在`DrawPoint`中，應該將經度x和緯度y以整體的形式傳遞進來，而不是拆開分別處理。

所以我們要從面向對象(物件導向)的思想出發，把傳遞給`DrawPoint`方法參數中的 x軸和 y軸的數據統一起來，轉化為一個整體的坐標點對象(物件)`point`，而這個參數`point`將會包含`x軸`和`y軸`的信息。

不過現在這個坐標點`point`的數據類型我們還沒有定義，所以對於程序來說，它是一個不確定的數據類型，對於一個不能確定類型的對象(物件)，可以使用 `dynamic`作為它的類型。

不過對於`point`對象(物件)有一點是可以確定的，接下來我們將會使用 `x`和 `y` 分別代表橫縱坐標的信息，所以這個時候，`x軸`和`y軸`都將會是參數坐標點換成的內部數據，我們可以使用對象(物件)的鍵式結構來進行訪問，所以簡單的使用`point.x` 和`point.y`就可以輸出數據了。

```c#
public static void DrawPoint(dynamic point) =>
    Console.WriteLine($"左邊點為 x: {point.x}, y: {point.y}");
```

這時候我們在`main方法`中調用`DrawPoint`的時候，同樣也可使用動態數據類型dynamic。

動態數據類型的創建需要使用`new`這個關鍵詞，而它的數據結構非常簡單，使用大括號來處理所有的成員變量`new { x = 15, y = 10 };`，這時候我們還需要用`dynamic`或是 `var`來引導一下變量的類型，坐標點名稱`a`等於`new`一個動態數據`dynamic a = new { x = 15, y = 10 };`，最後方法調用的時候，就可以使用這個`a`作為一個整體來替換x和y 的數據了。

```c#
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    //dynamic var
    dynamic a = new { x = 15, y = 10 };
    DrawPoint(a);
    Console.Read();
    return;
}

public static void DrawPoint(dynamic point) =>
    Console.WriteLine($"左邊點為 x: {point.x}, y: {point.y}");
```

一般來說，坐標點我們都會使用數字來進行表示，可以為整數或者是浮點數，但問題是，在 `DrawPoint`方法中，參數 `point`我們沒有進行任何類型的限制，所以在參數 `point`中，即使使用了非數字的坐標點，也是可以正常運行的。

比如說， `DrawPoint`參數傳入一個新的對象，` DrawPoint(new { x = "阿來克斯", y = "liu" });`，甚至我們可以使用一個完全不相干的動態對象，x改成天氣，y改成溫度`DrawPoint(new { weather = "晴天", temperature = "26℃" });`，那麼這個時候雖然visual studio不會報錯，但是，不管是我們的程式碼邏輯、還是業務邏輯，都徹底的錯誤了。

```c#
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    //dynamic var
    dynamic a = new { x = 15, y = 10 };
    DrawPoint(a);

    //那麼這個時候雖然visual studio不會報錯，但是，不管是我們的程式碼邏輯、還是業務邏輯，都徹底的錯誤了。
    DrawPoint(new { x = "阿來克斯", y = "liu" });
    DrawPoint(new { weather = "晴天", temperature = "26℃" });

    Console.Read();
}

public static void DrawPoint(dynamic point) =>
    Console.WriteLine($"左邊點為 x: {point.x}, y: {point.y}");
```

這時候如果運行程式，就會發現在運行過程中會彈出錯誤，這時候我們該如何處理呢？現在就轉到我們的`class`類別出場了。

## 類別class
使用 `class`可以聲明一個對象(物件)的具體類型，也可以對這個對象(物件)的行為加以限制，實際上，C#中所有的對象(物件)都可以使用類來進行處理。

現在我們來針對這個坐標點`point`創建它的類`class`：

聲明一個類，需要用一個關鍵字`class`，接著為類的名稱`Point`，兩個成員變量`x`、`y`，給它們整數類型 `int`，因為需要被外部訪問，所以加上 `public`

```c#
public class Point {
    public int x;
    public int y;
}
```

接著我們就可以在main 方法中使用了
- 類的實體化 `Point a = new Point();`
- 數據的初始化 `a.x = 15;`

```c#
static void Main(string[] args)
{
    //2d 坐標點：{x:15, y:10}
    //dynamic var
    Point a = new Point(); //類的實體化
    a.x = 15; //數據的初始化
    a.y = 10;
    DrawPoint(a);

    Console.Read();
}

public static void DrawPoint(Point point) =>
    Console.WriteLine($"左邊點為 x: {point.x}, y: {point.y}");

public class Point {
public int x;
public int y;
}
```

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=18](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=18)