---
layout: post
title: "[C# 筆記][多型] Abstract 抽象類-練習"
date: 2011-01-21 22:10:00 +0800
categories: [Notes, C#]
tags: [C#,多型,abstract]
---

## 什麼時候用虛方法、抽象類？
- 父類的函式有實現、有意義的時候，就用虛方法(virtual)     
- 父類的函式不知道怎麼去實現，就用抽象類(abstract)  

## 練習：使用多態求矩形的面積和周長以及圓形的面積和周長

```c#
//抽象類不能創建物件，只能聲明父類指向子類
Shape circle = new Cicle(5); //聲明父類Shape指向子類Cicle
double area = circle.GetArea();
double perimeter = circle.GetPerimeter();
Console.WriteLine($"圓形的面積是 {area} 周長是 {perimeter}");

Shape squqre = new Squqre(5, 6); //聲明父類Shape指向子類Squqre
area = squqre.GetArea();
perimeter = squqre.GetPerimeter();
Console.WriteLine($"矩形的面積是 {area} 周長是 {perimeter}");

//父類:形狀
public abstract class Shape
{
    //因為矩形、圓形的面積和周長公式不一樣
    //父類不能寫死，也不知道怎麼寫，就寫成抽象，這個類就必須是抽象類
    public abstract double GetArea(); //圓形
    public abstract double GetPerimeter(); //矩形
}

//子類：圓形
public class Cicle : Shape //繼承抽象類別
{
    //半徑
    private double _r; //字段(欄位):用來保護屬性的
    public double R //屬性
    {
        get { return _r; }
        set { _r = value; }
    }

    //面積:重寫抽象方法
    public override double GetArea() {
        return Math.PI * this.R * this.R;
    }

    //構造函式
    //希望在創建物件的時候，能把半徑放進去
    public Cicle(double r) {
        this.R = r;
    }

    //周長:重寫抽象方法
    public override double GetPerimeter() {
        return 2 * Math.PI * this.R;
    }
}

//子類:矩形
public class Squqre : Shape
{
    //長
    private double _height; //欄位(field): 用來保護屬性的
    public double Height //屬性
    {
        get { return _height; }
        set { _height = value; }
    }

    //寬
    private double _width;
    public double Width //屬性
    {
        get { return _width; }
        set { _width = value; }
    }

    //構造函式
    //創建物件的時候，可以將長、寬傳進去
    public Squqre(double height, double width)
    {
        this.Height = height;
        this.Width = width;
    }

    //面積
    public override double GetArea() {
        return this.Height * this.Width;
    }
    //周長
    public override double GetPerimeter() {
        return (this.Height + this.Width) / 2;
    }
}
```

---

1. 抽象成員必須標記為`abstract`並且不能有任何實現。
2. 抽象成員必須在抽象類中。
3. 抽象類不能被實體化。
4. 子類繼承抽象類後，必須把父類中的所有抽象成員都重寫(`override`)。    
(除非子類也是一個抽象類，則可以不重寫)
5. 抽象成員的訪問修飾符不能是`private`。
6. 在抽象類中可以包含實體成員。    
並且抽象類的實體成員可以不被子類實現
7. 抽象類是有構造函式的。雖然不能被實體化。
8. 如果父類的抽象方法中有參數，那麼，繼承這個抽象父類的子類在重寫父類的方法的時候，必須傳入對應的參數。    
如果抽象父類的抽象方法中有返回值，那麼子類在重寫這個抽象方法的時候，也必須要傳入返回值。

   

---
    
- 如果父類中的方法有默認的實現，並且父類需要被實體化，這時可以考慮將父類定義成一個普通類，用虛方法
- 如果父類的方法沒有默認實現，父類也不需要被實體化，則可以將類別定義為抽象類    

多型(Polymorphism) /多態  


[csharp/language-reference/keywords/abstract](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/abstract)    
[老趙老師-抽象類](https://www.bilibili.com/video/BV17G4y1b78i?p=142)

