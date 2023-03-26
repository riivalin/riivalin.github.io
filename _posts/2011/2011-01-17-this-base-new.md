---
layout: post
title: "[C# 筆記] this base new 關鍵字"
date: 2011-01-17 22:29:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## this base new 關鍵字
this 兩個作用
- 指當前的類別的物件
- 可以調用自己的構造函數

base 作用
- 調用父類的構造函數

new 兩個作用
- 建立物件
- 隱藏父類的成員

## this 兩個作用
1. 指當前的類別的物件
2. 可以調用自己的構造函數

```c#
public class Student
{
    //構造函式
    public Student(string name, int age, char gender, int english, int math)
    {
        this.Name = name; //1.指當前的類別的屬性
        this.Age = age;
        this.Gender = gender;
        this.English = english;
        this.Math = math;
    }

    public Student(string name, int english, int math) 
    : this(name, 18, 'C', english, math) //2.調用自己的全參構造函數
    { }
}
```

## base 作用
- 調用父類的構造函數

```c#
//子類 派生類
public class Student : Person
{
    public Student(string name, int age, char gender, int score)
        : base(name, age, gender) //調用父類的構造函式，使用關鍵字 ":base()"
    {
        //不用寫，因為有調用Person的有參構造函式
        //this.Name = name;
        //this.Age = age;
        //this.Gender = gender;
        this.Score = score;
    }

    private int _score;
    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
}

//父類 基類
public class Person
{
    public Person(string name, int age, char gender)
    {
        this.Name = name;
        this.Age = age;
        this.Gender = gender;
    }
}
```

## new 兩個作用
1. 建立物件 `Student s = new Student();`  
2. 隱藏父類的成員

```c#
//子類 派生類
public class Student {
    //2. 沒有加上new 關鍵字，程式會警告提示：....若本意即為要隱藏，請使用 new 關鍵字
    public new void SayHello() { ... }
}

//父類 基類
public class Person {
    public void SayHello() { ... }
}
```
