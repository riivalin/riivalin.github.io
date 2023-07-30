---
layout: post
title: "[C# 筆記] 運算符重載(Operater Overload) 1"
date: 2010-03-09 23:59:00 +0800
categories: [Notes,C#]
tags: [C#]
---

# 運算符重載(Operater Overload)

## 需求
如有下`Box`的類型，我們希望使用`+`來讓兩個`Box`做相加，結果返回一個新的`Box`，長寬厚是二者的加和。

```c#
class Box {
    public int width = 0;
    public int height = 0;
    public int depth = 0;
}
Box box1 = new Box();
Box box2 = new Box();
Box b = box1 + box2;
```

## 格式

- 運算符重載是將`+``-``*``/`等運算符看做方法，在類中重新定義其方法功能。

```c#
//雙目運算符
public static 返回類型 operator符號 (類型 對象1，類型 對象2);

//單目運算符
public static 返回類型 operator符號 (類型 對象); 
```

### 案例-雙目運算符

```c#
class Box {
    public int width = 0;
    public int height = 0;
    public int depth = 0;

    public static Box operator+(Box left, Box right) {
        Box result = new Box();
        result.depth = left.depth + rigth.depth;
        result.width = left.width + rigth.width;
        result.height = left.height + rigth.height;

        return result;
    }
}

Box box1 = new Box();
Box box2 = new Box();
Box b = box1 + box2;
```

## 練習1:雙目運算符

```c#
class Box {
    public int width = 0;
    public int height = 0;
    public int depth = 0;

    //重載+運算符
    public static Box operator+(Box left, Box right) {
        Box box = new Box();
        box.width = left.width + right.width;
        box.height = left.height + right.height;
        box.depth = left.depth + right.depth;
        return box;
    }

    //重載-運算符
    public static Box operator-(Box left, Box right)
    {
        Box box = new Box();
        box.width = left.width - right.width;
        box.height = left.height - right.height;
        box.depth = left.depth - right.depth;
        return box;
    }
}


internal class Program
{
    static void Main(string[] args)
    {
        Box box1 = new Box();
        Box box2 = new Box();
        box1.width = 10;
        box1.height = 10;
        box1.depth = 10;

        box2.width = 20;
        box2.height = 20;
        box2.depth = 20;

        //Box b =  Box operator+(box1, box2);
        //Box b = box1 + box2;
        Box b = box1 - box2;
        Console.WriteLine($"width: {b.width} height: {b.height} depth: {b.depth}");
    }
}
```

### 案例-單目運算符
二維向量(或三維、四維、多維向量)

```c#
class Vector {
    public int x = 0;
    public int y = 0;

    //這裡的-，代表的是負號(取反)，不是減號
    public static Vector operator-(Vector v1) {
        Vector v = new Vector();
        v.x = -v1.x;
        v.y = -v1.y;

        return v;
    }
}
Vector v = new Vector();
v.x = 10;
v.y = 20;
Vector nv = -v;
```

## 練習2:單目運算符

```c#
class Vector {
    public int x = 0;
    public int y = 0;

    //實現單目運算符取反-
    public static Vector operator -(Vector v) {
        Vector nv = new Vector();
        nv.x = -v.x;
        nv.y = -v.y;
        return nv;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Vector v = new Vector();
        v.x = 5;
        v.y = 4;

        Vector nv = new Vector();
        nv = -v;//取反
        Console.WriteLine($"x: {nv.x}");
        Console.WriteLine($"y: {nv.y}");
    }
}
```

## 練習3: 向量-取反、相減

```c#
class Vector {
    public int x = 0;
    public int y = 0;

    //實現雙目運算符-減法
    public static Vector operator -(Vector v1, Vector v2) {
        Vector v = new Vector();
        v.x = v1.x - v2.x;
        v.y = v1.y - v2.y;

        return v;
    }
    //實現單目運算符取反-
    public static Vector operator -(Vector v) {
        Vector nv = new Vector();
        nv.x = -v.x;
        nv.y = -v.y;
        return nv;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Vector v = new Vector();
        v.x = 5;
        v.y = 4;

        Vector v1 = new Vector();
        v1.x = 2;
        v1.y = 2;
        
        Vector nv = -v;//負號取反
        Vector minusV = v - v1; //減號相減
        Console.WriteLine($"x ={minusV.x}");
        Console.WriteLine($"y ={minusV.y}");
    }
}
```

## 練習4: 單目運算符中的 ++/--
- `a++`: 後++，先參與表達式運算，再加1
- `++a`: 前++，先加1，再參與運算

```c#
class Vector {
    public int x = 0;
    public int y = 0;

    //實現雙目運算符-減法
    public static Vector operator -(Vector v1, Vector v2) {
        Vector v = new Vector();
        v.x = v1.x - v2.x;
        v.y = v1.y - v2.y;

        return v;
    }
    //實現單目運算符取反-
    public static Vector operator -(Vector v) {
        Vector nv = new Vector();
        nv.x = -v.x;
        nv.y = -v.y;
        return nv;
    }

    //單目運算符中的 ++/--(只能二擇一)
    //a++: 後++，先參與表達式運算，再加1
    //++a: 前++，先加1，再參與運算

    //前++
    public static Vector operator ++(Vector v) {
        //傳進來的v 跟回傳回去給vplus的，是指向同一塊內存
        v.x += 1;
        v.y += 1;
        return v;
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        //前++
        Vector vplus = new Vector();
        vplus.x = 8;
        vplus.y = 9;
        ++vplus;
        Console.WriteLine(vplus.x);
        Console.WriteLine(vplus.y);
    }
}
```
[https://www.bilibili.com/video/BV1FP411B7cV/](https://www.bilibili.com/video/BV1FP411B7cV/)