---
layout: post
title: "[C# 筆記] Constructor 構造函式"
date: 2011-01-14 23:00:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 構造函式
作用：幫助我們初始化物件(給物件的每個屬性依次賦值)

構造函式是一個特殊的方法：
1. 構造函式沒有返回值，連void也不能寫。
2. 構造函式的名稱，必須跟類別名一樣。

- 創建物件的時候，會調用構造函式  
- 構造函數是可以有重載的。

類別當中會有一個默認的無參釋的構造函數，當你寫一個新的構造函數後，不管是有參數的、還是無參數的，那個默認的無參數構造函數就會被取代掉。

## 使用構造函式
沒使用構造函式，要寫這麼多程式去賦值(給每一個屬性賦值)
```c#
Student student = new Student();

student.Name = "小明";
student.Age = 100;
student.Gender = 'M';
student.English = 77;
student.Math = 90;
```
使用構造函式，只要一行，在初始化物件時去賦值
```c#
Student student = new Student("小明", 0, '中', 89, 60);
```

## new 關鍵字
`Person person = new Person();` 
`new` 幫助我們做了三件事：
1. 在內存中開辟一塊空間
2. 在開辟的空間中創建對象(物件)
3. 調用物件的構造函數進行初始化物件

> 所以構造函式一定是 public

> 物件 = 對象

## Student Class
```c#
public class Student
{
    //構造函式
    public Student(string name, int age, char gender, int english, int math)
    {
        this.Name = name;
        this.Age = age;
        this.Gender = gender;
        this.English = english;
        this.Math = math;
    }

    private string _name; //字段
    public string Name //屬性。用來保護字段
    {
        get { return _name; }
        set { _name = value; }
    }

    private int _age;
    public int Age
    {
        get { return _age; }
        set
        {
            //在賦值前，先進行限定
            if (value < 0 || value > 100)
            {
                _age = 0;
            }
            _age = value;
        }
    }

    private char _gender;
    public char Gender
    {
        get
        {
            //在取值前，先進行限定
            if (_gender != '男' || _gender != '女')
            {
                return _gender = '男';
            }
            return _gender;
        }
        set { _gender = value; }
    }

    private int _english;
    public int English
    {
        get { return _english; }
        set { _english = value; }
    }

    private int _math;
    public int Math
    {
        get { return _math; }
        set { _math = value; }
    }

    public void SayHello()
    {
        Console.WriteLine($"Hi,我是{this.Name}，今年{this.Age}歲，我是{this.Gender}生");
    }

    public void ShowScore()
    {
        Console.WriteLine($"我是{this.Name}, 平均成續是：{(this.English + this.Math) / 2}");
    }
}
```
[classes-and-structs/using-constructors](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/using-constructors)
[classes-and-structs/constructors](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/constructors)