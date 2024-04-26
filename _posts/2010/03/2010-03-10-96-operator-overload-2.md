---
layout: post
title: "[C# 筆記] 運算符重載(Operator Overloading) 2"
date: 2010-03-10 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,Operator Overloadint,operator,implicit operator,explicit operator]
---

# 隱式轉換 & 顯示轉換(強制轉換)
## 隱式轉換

- 隱式類型轉換：將取值範圍外小的值，裝到一個取值範圍較大的變量裡面，叫做隱式。      

例如，`int`的值裝到`float`裡：`float a = 5;`、`float`的變量裝到`double`裡：`double b = 1.25f;`。

```c#
float a = 5; //int的值裝到float變量裡
double b = 1.25f; //float的值裝到doublel變量裡
```

## 顯示轉換(強制轉換)

- 顯式類式轉換(強制轉換)：將取值範圍較大的值，裝到取值範圍較小的變量裡。        

例如，將`float`的值裝到`int`裡肯定會報錯，所以咱們要明顯的告訴編譯器說，我這加個括號裡面放int`(int)`，我呢希望能夠把這個`1.5`給我強制轉換成一個`int`型的數字整數，放到變數`c`裡面去。

```c#
//int c = 1.5f; //會報錯
int c = (int)1.5f; //顯示轉換
```

# 類型轉換運算符
- `C#`允許將「顯式類型轉換」與「隱式類型轉換」看做運算符，並且得到「重載」功能

## 舉例：

```c#
class Person {
    public int age = 0;
}
...
Person person = 12; //可否將int類型的12轉換為Person 呢？
int a = (int)Person; //可否將person類型強制轉換為int呢？
```

## 格式

```c#
//隱式類型轉換
public static implicit operator 目標類型(類型 待轉換對象);
//顯式類型轉換
public static explicit operator 目標類型(類型 待轉換對象);
```

## 舉例

```c#
class Person {
    public int age = 0;
    //隱式類型轉換
    public static implicit operator Person(int age) {
        Console.Write("implicit Person");
        return new Person { age = age; }
    }
    //顯示類型轉換
    public static explicit operator int(Person person) {
        Console.Write("explicit Person");
        return person.age;
    }
}
//使用
Person person = 12; //隱式類型轉換
int a =(int)person; //顯示類型轉換
```

# 練習
## 目標 1：Person p = 12, 隱式轉換
數值轉換為物件

```c#
class Person {
    public int age = 0;
    public string name = "default";

    //目標1：Person p = 12, 隱式轉換
    public static implicit operator Person(int age) {
        //Person p = new Person();
        //p.age = age;
        //return p;
        return new Person { age = age };
    }
}

Person p = 12;
Console.WriteLine(p.age); //12
Console.WriteLine(p.name);//default
```

## 目標 2: Person p = "Rii";
字串轉換為物件

```c#
class Person {
    public int age = 0;
    public string name = "default";

    //目標2: Person p = "Rii";
    public static implicit operator Person(string name) {
        return new Person { name = name };
    }
}
Person p = "Rii";
Console.WriteLine(p.age); //0
Console.WriteLine(p.name);//Rii

```
## 目標 3: Person p = "12"; 數字字串
年齡為字串

```c#
class Person {
    public int age = 0;
    public string name = "default";

    //目標1：Person p = 12, 隱式轉換
    public static implicit operator Person(string age) {
        return new Person { age =  Convert.ToInt32(age) };
    }
}

Person p = 12;
Console.WriteLine(p.age); //12
Console.WriteLine(p.name);//default
```

## 目標 4: int age = (int)p; //p 是person類型的對象

將物件「顯示轉換」放到int中

```c#
class Person {
    public int age = 0;
    public string name = "default";

    //目標4: int age = (int)p; //p 是person類型的對象
    public static explicit operator int(Person person) {
        return person.age;
    }
}

Person p = new Person();
p.age = 100;
int age = (int)p; //顯示轉換(強制轉換)
Console.WriteLine(age); //100
```

## 目標 5: int age = p;//p 是person類型的對象

將物件「隱式轉換」放到int中
```c#
class Person {
    public int age = 0;
    public string name = "default";

    //目標5: int age = p; //p 是person類型的對象
    public static implicit operator int(Person person) {
        return person.age;
    }
}

Person p = new Person();
p.age = 100;
int age = p; //隱式轉換
Console.WriteLine(age); //100
```

# 完整程式碼

```c#
class Person {
    public int age = 0;
    public string name = "default";

    //目標1：Person p = 12, 隱式轉換
    public static implicit operator Person(int age) {
        //Person p = new Person();
        //p.age = age;
        //return p;
        return new Person { age = age };
    }

    //目標2: Person p = "Rii";
    //public static implicit operator Person(string name)
    //{            
    //    //Person p = new Person();
    //    //p.name = name;
    //    //return p;
    //    return new Person { name = name };
    //}

    //目標3: Person p = "12"; 數字字串
    public static implicit operator Person(string age) {
        int numAge = Convert.ToInt32(age);
        return new Person { age = numAge };
    }

    //目標4: int age = (int)p; //p 是person類型的對象
    //int age = p;
    //public static explicit operator int(Person person) {
    //    return person.age;
    //}

    //目標5: int age = p;//p 是person類型的對象
    public static implicit operator int(Person person) {
        return person.age;
    }
}
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
        //顯式類型轉換實驗
        Person p = new Person();
        p.age = 100;
        int age = p;
        Console.WriteLine(age);
        //int age = (int)p;
        //Console.WriteLine(age);
            
        //隱式類型轉換實驗
        //Person p1 = 12;
        ////Person p2 = "Rii";
        //Person p3 = "12";
        //Console.WriteLine(p1.age); //12
        //Console.WriteLine(p1.name);//default
        ////Console.WriteLine(p2.age);//0
        ////Console.WriteLine(p2.name);//Rii
        //Console.WriteLine(p3.age);//0
        //Console.WriteLine(p3.name);//Rii



        ////前++
        //Vector vplus = new Vector();
        //vplus.x = 8;
        //vplus.y = 9;
        //++vplus;
        //Console.WriteLine(vplus.x);
        //Console.WriteLine(vplus.y);

        //Vector v = new Vector();
        //v.x = 5;
        //v.y = 4;

        //Vector v1 = new Vector();
        //v1.x = 2;
        //v1.y = 2;

        //Vector nv = -v;//取反
        //Vector minusV = v - v1; //相減
        //Console.WriteLine($"x ={minusV.x}");
        //Console.WriteLine($"y ={minusV.y}");

        //Box box1 = new Box();
        //Box box2 = new Box();
        //box1.width = 10;
        //box1.height = 10;
        //box1.depth = 10;

        //box2.width = 20;
        //box2.height = 20;
        //box2.depth = 20;

        ////Box b =  Box operator+(box1, box2);
        ////Box b = box1 + box2;
        //Box b = box1 - box2;
        //Console.WriteLine($"width: {b.width} height: {b.height} depth: {b.depth}");
    }
}
```


[https://www.bilibili.com/video/BV1Bm4y1a7wb](https://www.bilibili.com/video/BV1Bm4y1a7wb)