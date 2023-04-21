---
layout: post
title: "[C# 筆記] Protected 存取修飾符"
date: 2011-02-10 00:09:31 +0800
categories: [Notes,C#]
tags: [C#,Protected,存取修飾符]
---


- public: 任何人都可以存取使用
- private: 只有自身類別才能存取使用
- protected: 只有自身類別與子類別才能存取使用
(只要是繼承關係，不管兩者是否在同一程序集中，子類都有訪問父類的權限)

```c#
class Program
{
    static void Main(string[] args)
    {
        //protected成員除了當前的類和子類外，是訪問不到的
        //拿不到_name，受保護的protected成員
        Person p = new Person();
    }
}
//父類
public class Person
{
    //protected受保護的
    protected string name;
}
//子類
public class Teacher : Person
{
    public void T()
    {
        //子類可以訪問到父類的protected成員
        age = 99;
    }
}
```
所以說 Protected 比 Private 權限高了一些，多了一個權限，Protected 在子類當中可以訪問的到(通過繼承)，

---

- public: 任何人都可以存取使用
- private: 只有自身類別才能存取使用
- protected: 只有自身類別與子類別才能存取使用
(只要是繼承關係，不管兩者是否在同一程序集中，子類都有訪問父類的權限)
- internal: 只有同一個namespace的才能存取
- protected internal: protected /|/| internal的概念

![](/assets/img/post/access-csharp.jpeg)
