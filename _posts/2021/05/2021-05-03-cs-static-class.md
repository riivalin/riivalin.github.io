---
layout: post
title: "[C# 筆記] 建立不需要實體化的靜態類別(static class)"
date: 2021-05-03 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,class,static]
---

# 什麼是「靜態類別」？

所謂「靜態類別(`static class`)是指「**被宣告成靜態的類別，不用透過`new`關鍵字來將類別實體化(`Instantiated`)，就能直接取用靜態類別所屬成員的屬性和方法**」。     

(不用實體化，就可以直接取用。)     

`static` 類別基本上與非靜態類別相同，但有一項差異︰無法實體化靜態類別。      
換句話說，不能使用 `new` 來建立類別型別的變數。         
因為沒有任何執行個體變數，所以是使用「類別名稱」本身來存取「靜態類別的成員」。      


例如，`Person`靜態類別有 `MethodA` 公開的靜態方法，則呼叫方法，如下列範例所示︰

```c#
Person.MethodA();
```

只有在完全完全確定一個方法不會有結構調整，與系統中其他部分幾乎沒有關聯時，才可以考慮把它寫成靜態方法。

否則，不要用靜態方法！

> - `static`方法是類別中的一個成員方法，屬於整個類別，不用創建任何物件也可以直接呼叫!         
> - 靜態方法效率上要比實體化高，但是靜態方法的缺點是：不自動進行銷毀，而實體化的則可以做銷毀。        
> - 靜態方法和靜態變數創建後始終使用同一塊記憶體空間，而使用實體的方式會創建多個憶體空間。  

## 特色

靜態類別特色：      

- 不能執行實體化。(不能`new`) 
- 「靜態類別」所屬成員，包含：屬性、方法、事件，都必須宣告成靜態才能被取用，否則會發生錯誤而無法進行編譯。
(也就是說，靜態類別中，只允許有靜態成員，不允許出現實體成員。)  
- 「靜態類別」不能被繼承，相當於密封類別(`Sealed Class`)。
- 「靜態類別」雖然不能透過`new`來實體化，但仍可宣告「靜態建構函式」來初始化靜態成員。

> 有一點要特別注意：在「靜態類別」內是不能宣告「解構函式」。


## 靜態類別的宣告

加上 `static`

```c#
public static class Person
{
    //不能在靜態類別中宣告實體成員
    //非靜態成員(實體成員)
    //private int num; //報錯
    //public void M1() {..}//報錯

    public static int count;
    public static void Add() {...}
}
```

> 不能在「靜態類別」中宣告「實體成員」。


## 取用靜態類別內的成員

使用靜態成員：不需要`new`，直接使用。

```c#
類別名稱.靜態成員名稱
```

```c#
//在調用靜態成員的時候，需要使用：類別名稱.靜態成員名稱
Person.M2(); //靜態方法
Person.Age; //靜態屬性
```

> 使用實體成員：需要`new`實體化。`物件名.實體成員`      
> 使用靜態成員：不需要`new`，直接使用。`類別名.靜態成員名`


## 範例

假設我們要宣告一個汽車的靜態類別：      

屬性：鋁圈、車門、馬力、最高時速        
方法：供油方式、引擎技術        

Car 靜態類別的宣告(加上`static`)

```c#
//取用靜態類別內的成員
Car.MaxSpeed; //直接取用屬性
Car1.FuelSystem(); //直接取用方法

//靜態類別的宣告
public static class Car
{
    //「靜態類別」雖然不能透過 new 來實體化，但仍可宣告「靜態建構函式」來初始化靜態成員
    static Car() //靜態類別 初始化
    {
        Console.WriteLine("執行靜態類別的建構函式");
    }

    public static int AluminumWheel { get { return 18; } } //鋁圈
    public static int CarDoor { get { return 2; } } //車門
    public static int Horsepower { get { return 400; } } //馬力
    public static int MaxSpeed { get { return 295; } } //極速

    //供油方式
    public static string FuelSystem()
    {
        return "Multi-Port Fuel Injected"; //多當噴射
    }

    //引擎技術
    public static string EngineTechnology(bool engineType)
    {
        return (engineType) ? "Natural Aspirate" : "Turbo"; //自然進氣/渦輪增壓
    }
}
```

[MSDN - static (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/static)      
[MSDN - 靜態類別和靜態類別成員 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members)
[[C# 筆記] static 靜態與非靜態  by R](https://riivalin.github.io/posts/2011/01/static/)     
[[C# 筆記] 靜態成員 vs 非靜態成員 的區別  by R](https://riivalin.github.io/posts/2017/02/the-difference-between-static-members-and-non-static-members/)     
[[C# 筆記] class property method field review](https://riivalin.github.io/posts/2011/01/review3/)   
[CSDN - 为什么应该少用静态（static）方法：静态方法的三大问题](https://blog.csdn.net/VoisSurTonChemin/article/details/125729755)         
[Imooc(慕课) - tatic 静态方法 有什么优缺点?](https://www.imooc.com/wenda/detail/515705)     
Book: Visual C# 2005 建構資訊系統實戰經典教本     
