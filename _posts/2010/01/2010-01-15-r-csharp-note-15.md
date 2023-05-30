---
layout: post
title: "[C# 筆記] 什麼是多態(多型)?"
date: 2010-01-15 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,override,virtual,多型]
---

## 什麼是多態(多型)?

[對象(物件)的向上轉型](https://riivalin.github.io/posts/2010/01/r-csharp-note-12/)      

`Circle` 繼承於 `Shape`，在完成`circle` 實體化之後，通過向上轉型，我們可以把 `circle` 類轉化為 `shape`類，其實這就是「多態(多型)」最簡單的例子。

```c#
Circle circle = new Circle();
Shape shape = circle;
```
- 內存一致、數據一樣，但表現型態多種多樣

不管是 `circle` 對象(物件)，選是 `shape` 對象(物件)，他們都是引用類型，指向同一塊內存地址，雖然他們所代表的數據是完全一樣的，但是表現形式卻是多種多樣的，這就是「多態(多型)」。

## 多態(多型)的應用場景
「多態(多型)」是面向對象(物件導向)編程重要的特徵之一，「多態(多型)」到底可以為我們解決什麼樣的實際問題呢？      


### 鍵盤
- 一個按鍵在不同輸入法中具有不同的表現，這就是多態(多型)」

請看一下自己的鍵盤，我們鍵盤上有一個貨幣符號(shift+4)，貨幣的輸出，是根據你目前使用的輸入法來決定的，每種輸入法都會對這個貨幣符號進行重新定義，一個按鍵在不同輸入法中具有不同的表現，這就是多態(多型)」典型的應用場景。

### 畫圖案
不同的圖案，繪製表現形式也各不相同，可以畫圓、畫方、畫三角形。      

而這種對派生類的集中處理，也是「多態(多型)」應用的經典場景。

```c#
var shapeList = new List<Shape>(); //創建圖案列表
shapeList.Add(new Cicle()); //加入圓形
shapeList.Add(new Rectangle()); //加入方形

var canvas = new Canvas(); //創建畫布物件
canvas.DrawShape(shapeList); //調用畫圖方法
```
```c#
public void DrawShape(List<Shape> shapes) {
    foreach (var shape in shapes) {
        shape.Draw();
    }
}
```

## 多態的好處

「多態(多型)」有哪些好處呢？        

- 可替換性
- 可擴展性
- 接口(介面)性
- 靈活性
- 簡化性

首先，「多態(多型)」對於已存在的程式碼，具有非常高的可替換性，例如上面畫圖案的例子，「多態(多型)」它可以對圓 circle 這個類發揮作用，同樣的，它也可以對任何其他的圖形發揮相同的作用。

第二點，「多態(多型)」對程式碼具有可擴展性，增加新的子類不會影響已存在的類任何特性，以及它的運行操作規則，而新加的子類同樣也具有「多態(多型)」的功能。      

還是上面的例子，在實現了方形和圓形的「多態(多型)」之後，再去添加三角形、橢圓形的多態，拓展是非常容易的。        

第三點，接口(介面)性，通過方法簽名向子類提供一個共同的接口(介面)，由子類來完善、或者覆蓋它而實現的，接口(介面)的概念示常重要。      

第四點，靈活性，它在應用中體現了靈活多樣的操作，提高了程式碼的使用效率。        

最後，簡化性，「多態(多型)」可以簡化程式碼的編寫和修改的過程，尤其可以集中處理大量對象(物件)的運算和操作。這個特點在多態的使用中非常重要、非常突出。        

但要注意的是，「多態(多型)」並不能提高執行的速度，

## 練習
添加畫三角形
- 只需要給這個項目添加一個新的class
- 繼承`Shape`
- 重寫 `Draw` 方法
- `shapeList` 添加第三個元素

```c#
public class Triangle : Shape {
    public override void Draw() {
        Console.WriteLine("畫三角形");
    }
}
```

```c#
shapeList.Add(new Triangle()); //添加三角形
```

Shape 對象的變化，第一個迴圈，shape進入圓形畫圓，第二個回圈，shape進入方形畫方，第三個迴圈畫三角形，於是我們的程序在調用同一個方法的時候，就表現出了不一樣的行為。      

當我們談到多態的時候，指的是一個過程，一個狀態。    

因為我們在 `Shape`中使用了虛方法`virtual`，這就讓它的派生類(子類)獲得了重新定義這個方法的機會。     

只有在程式跑起來、運行時，它的多態才會發揮功效。

## Code

```c#
public class Shape
{
    public virtual void Draw()
    {
        Console.WriteLine("繪製圖案");
    }
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

        var canvas = new Canvas(); //創建畫布物件
        canvas.DrawShape(shapeList); //調用畫圖方法

        Console.Read();
    }
}
```

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=34](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=34)