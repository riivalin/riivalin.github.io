---
layout: post
title: "[C# 筆記] 屬性(get/set)對非法值進行限定的三種方法"
date: 2021-05-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,屬性,get-set]
---


# 属性對非法值進行限定

為了防止使用者輸入或傳入非法值，我們往往需要對類別中的屬性進行限定。例如在登入系統中，限定密碼為數字則必須輸入數字，年齡的限定必須為0~120，對於性別的設定必須為男或女，現在介紹三種限定方法。        

屬性是用來保護和限定欄位的，屬性的限定一般是`set`和`get`方法，其中`set`方法是在給屬性賦值的時候執行的，而`get`則是在屬性輸出呼叫的時候使用的。   
1. 在類別中，對属性中的`get`方法進行限定
2. 在類別中，對属性中的`set`方法進行限定
3. 在類別中，在建構函式`ctor`中進行限定

#### 先建立 Person 類別

首先我們先建立一個person類，和類別下的欄位和屬性

```c#
public class Person //非靜態類別(實體類別)
{
    private string _name; //定義欄位
    public string Name //定義屬性
    {
        get { return _name; } //取得屬性值
        set { _name = value; } //給屬性賦值
    }
}
```

## 1.對属性中的 get 方法進行限定

在 `get` 中對欄位進行判斷

```c#
plublc class Person
{
    private int _age;
    public int Age
    {
        get
        {
            //限定Age屬性值，設定值必須在 0~99 之間
            if (_age < 0 || _age > 100) {
                return 0;
            }
            return _age;

            //return (_age < 0 || _age > 100) ? 0 : _age;
        }
        set { _age = value; }
    }
}
```

## 2.對属性中的 set 方法進行限定

在 `set` 中對欄位進行判斷

```c#
public class Person
{
    public string Name
    {
        get { return _name; }
        set
        {
            if (value != "admin") {
                //在此限定了Name 無輸入什麼都是 admin
                value = "admin";
            }
            _name = value; //给物件的属性赋值时使用set方法


            //_name = (value == "admin") ? value : "admin"; 
        }
    }
}
```

## 3.在建構函式中進行限定

```c#
public class Person
{
    public Person(string name, int age, char gender)
    {
        this.Name = name;
        this.Age = age;

        //此時當呼叫函數的時候，輸入的值若不是男或女，則會賦值為男
        if (gender != '男' && gender != '女') gender = '男';
        this.Gender = gender;
    }
    public string Name { get; set; }
    public int Age { get; set; }
    public char Gender { get; set; }
}
```

[MSDN - 使用屬性 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/using-properties)     
[MSDN - 自動實作的屬性 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties)        
[[C# 筆記] get set 自動屬性 & 普通屬性  by R](https://riivalin.github.io/posts/2011/01/auto-and-normal-properties/)     
[CSDN - C#在类的属性对非法值的进行限定的三种方法](https://blog.csdn.net/weixin_46096032/article/details/121537152)