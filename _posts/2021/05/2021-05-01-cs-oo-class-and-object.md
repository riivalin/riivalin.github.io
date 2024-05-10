---
layout: post
title: "[C# 筆記] 物件導向(Object-Oriented, OO)基本概念"
date: 2021-05-01 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO]
---

# 物件導向的基本概念

何謂「物件導向(Object-Oriented, OO)」？     
物件導向是程式實作的方法之一，將程式所要處理的功能與資料歸類於多個物件中的設計方法。(設計方式以「物件」為主)        


## 主要核心

物件導向的核心共有三種：
1. `Class`(類別)
2. `Object`(物件)
3. `Inheritance`(繼承)

## 重要名詞

物件導向的幾個重要名詞：

- 類別和物件(Class and Object)
- 繼承性(Inheritance)
- 多型/同質性/同名異式(Polymorphism)
- 多載(Overloading)
- 重寫(Overriding)
- 封裝性(Encapsulation)
- 抽象化(Abstraction)

物件導向最基本的特性就是上述所提到的**封裝**與**繼承性**。    

「封裝性」可以讓程式變得更安全，並且達到所謂的資訊隱藏。        
「繼承性」可以讓程式碼達到可再利用性(Reusable)的目的，減少往後繼續開發的成本，以及增加維護的方便性。        

### 類別和物件(Class and Object)

- 類別(`Class`)是用來描述某物件(`Object`)的結構。
- 而物件(`Object`)是把：變數(`Variable`)、屬性(`Property`)和方法(`Method`)包在一起的一種軟體技術，為某類別的「執行個體」。
- 例如，我們將`Class`視為「車體設計圖」，那麼`Object`就是依據車體設計圖所打造出來的汽車。

### 繼承性(Inheritance)

- 繼承就是子類別可以繼承某一個父類別的所擁有的方法和屬性。
- 例如：男人若是父類別，而帥哥就是男人的子類別，帥哥繼承了男人的一些基本特色，包含：有鬍子、喉結、聲音低沉、粗獷等。
- 繼承性(Inheritance)主要的優點就是：容易達成軟體再利用(Reuse)，並減少相同功能方法重複開發。

#### 範例

```c#
class Person { } //父類
class Man : Person { } //子類(繼承Person)
```

### 多型/同質性/同名異式(Polymorphism)

多型主要有二個核心概念：
1. 多載(`Overloading`)：是屬於「靜態多型」。
2. 重寫(`Overriding`)：是屬於「動態多型」。

### 多載(Overloading)

- 多載(`Overloading`)就是可「**可重複定義方法**」，相同運算子或方法可以有不同定義。
- 在同一個命名空間中，可以有兩個以上相同名稱的方法，但具有不同的參數個數。

> 概念：方法的重載，指的就是方法的名稱相同，但是參數不同。

#### 範例

```c#
 void Test(int x) {  }
 void Test(double x) { } // 方法名相同，參數類型不同
 void Test(int x, int y) { } // 方法名相同，參數個數不同

//錯誤寫法：沒有構成方法的重載，因為方法的重載跟 返回值 沒有關係
//void int Test(int x) { }
```

### 重寫(Overriding)

- 重寫(`Overriding`)就是可「**可重新定義方法**」。
- 是指「子類別(child class)」可將「父類別(parent class)」中所定義的方法(Method)於子類別中透過`override`關鍵字來**重新定義**，此法使父類別的方法相對失效的技術稱之。

> 重寫的作用：實現多型(多態) 。       
> (調用同一類，卻產生不同表現形式的過程，就稱它為「多型(多態)」。)

#### 範例

使用 `Override` 重寫父類的`ToString()`

```c#
Person p = new Person();
Console.WriteLine(p.ToString()); //輸出：Hello World

//Override 重寫父類的ToString()
class Person {
    public override string ToString() {
        return "Hello World";
    }
}
```

### 封裝(Encapsulation)

- 封裝(Encapsulation)是將資料與操作此資料的方法包在一起而成一個`Object`的特性，故又稱「資訊隱藏(Information Hiding)」。
- 封裝將物件的資料與執行的程序隱藏，只提供屬性和方法供外界參考，其概念是：使用者只需要知道介面的引用方法，不需要知道實作的細節。

### 抽象化(Abstraction)
- 抽象化(Abstraction)又稱「抽象化資料型態(Abstract Data Type), ADT」，主要目的是使用一些有意義的「參數名稱」來取代「無意義的文數字」，這樣的方式是為了讓程式的可讀性變得更好。
- 抽象化(Abstraction)對象主要以「資料型別」為主，就是把物件和運算的設計規格(Specification)和實作(Implementation)分開的資料型態。
- 常見的抽象資料型別有：`Enum`、`Struct`、`Stack`和`Queue`。

#### 範例

- 使用`enum`來設計汽車類型(`CarsType`)
- 使用`struct`來設計汽車的基本資料(`CarProfile`)

```c#
//宣告一個名為CarsType的列舉
public enum CarsType : int
{
    SportsCar = 0, //跑車
    Covertibles = 1, //敞篷車
    RV = 2 //休旅車
}

//宣告一個名為CarProfile結構(struct)
public struct CarProfile
{
    public string Name;
    public string Company;
}
static void Main(string[] args)
{
    Console.WriteLine("請選擇車種：0-跑車，1-敞篷車，2-休旅車");
    int input = Convert.ToInt32(Console.ReadLine()!); //接收使用者的輸入

    CarProfile CP; //stuct的叫用方式
    CP.Name = "";
    CP.Company = "";

    string carType = "";
    switch (input)
    {
        case (int)CarsType.SportsCar:
            carType = "跑車";
            CP.Company = "Audi";
            CP.Name = "Audi R8";

            break;
        case (int)CarsType.Covertibles:
            carType = "敞篷車";
            CP.Company = "Audi";
            CP.Name = "Audi A3 Cabriolet";
            break;
        case (int)CarsType.RV:
            carType = "休旅車";
            CP.Company = "Audi";
            CP.Name = "Audi Q5 Sportback SUV";
            break;
        default:
            break;
    }
    Console.WriteLine($"車名：{CP.Name}\r\n公司：{CP.Company}\r\n汽車類型：{carType}");
}
```

> 抽象類別(Abstract Class)：        
> 只有抽象概念，就需要使用`abstract`關鍵詞，代表的是，我們不需要在基類(父類)中實現這個方法。       
> 而具體的實現，只能在派生類(子類)中處理。



[[C# 筆記] Overload 方法的重載  by R](https://riivalin.github.io/posts/2011/01/overload/)       
[[C# 筆記] 虛方法(Virtual) vs 方法重寫(Override)  by R](https://riivalin.github.io/posts/2010/01/r-csharp-note-14/)     
[[C# 筆記] Override 重寫父類的ToString()  by R](https://riivalin.github.io/posts/2011/01/override-tostring/)