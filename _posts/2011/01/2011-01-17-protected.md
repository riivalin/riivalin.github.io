---
layout: post
title: "[C# 筆記] Protected 受保護的"
date: 2011-01-17 22:49:00 +0800
categories: [Notes, C#]
tags: [C#,Protected]
---

## protected 受保護的
子類也可以訪問

```text
public 公開：子類可以訪問
private 私有：子類不可以訪問
protected 受保護的：子類可以訪問
```

```c#
//父類
public class Person
{
    public int n1; //子類可以訪問
    private int n2;//子類不可以訪問
    protected int n3; //子類可以訪問

    public void SayHello() {
        Console.WriteLine("我是人類");
    }
}
//子類
public class Student : Person
{
    public Student() {
        this.n1 = 20; //父類的 public 變數
        this.n3 = 10; //父類的 protected 變數
    }
    public void StudnetSayHello() {
        Console.WriteLine("我是學生");
    }
}
```