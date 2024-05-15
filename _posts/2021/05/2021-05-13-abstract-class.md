---
layout: post
title: "[C# 筆記] 抽象類別(Abstract Class)"
date: 2021-05-13 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,abstract,多型]
---


「抽象類別(`Abstract Class`)」只有抽象概念，代表的是，我們不需要在基類(父類)中實現這個方法。        
而具體的實現，只能在派生類(子類)中處理。


抽象類別(Abstract Class)就是「只能用來提供給其他類別繼承的基底類別」，它提供一種類似**樣版**的功能。

抽象類別中，可以實作完整的方法和屬性，也可以像 `interface 介面`一樣單純宣告方法和屬性的空殼，留待給繼承抽象類別的子類別來實作該方法和屬性。(不能夠對「抽象類別」進行實體化)        

- 抽象類別 不能進行實體化。
- 抽象方法 只能於抽象類別中定義。
(如果我們給一個類別`Class`的方法或者屬性加上了`abstract` 這個關鍵詞，整個類別也必須要使用 `abstract`)
- 抽象類別 不能使用`sealed`關鍵字修飾詞。
- 抽象方法 隱含 虛擬方法，所以當要實作時，需要搭配`override`關鍵字來實作。
- 在抽象方法中 不能使用 `static`或 `virtual`關鍵字。


> 當父類中的方法不知道如何去實現的時候，可以考慮將父類寫成 抽象類別，將方法寫成 抽象方法。      
> 既然不能實現，就不要實現，就用抽象類別，讓子類去實現。        
> [[C# 筆記][多型] Abstract 抽象類](https://riivalin.github.io/posts/2011/01/abstract/)


## 語法

### 宣告抽象類別
```c#
abstract class 抽象類別名稱
{
    //抽象屬性、方法，不實作程式碼宣告
    public abstract 資料型別 屬性名稱 { get; set; }
    public abstract [資料型別|void] 方法名稱([參數群]);

    //在抽象類別裡，可以定義普通方法(實作程式碼)
    public [資料型別|void] 方法名稱([參數群]) { ... }
}
```

```c#
abstract class Person {
    public abstract string Name { get; set;}
    public abstract string SayHi(string name, int age);
}
```

#### 範例

抽象方法不能有任何方法實現      
抽象成員必須在抽象類別中，但是抽象類別當中可以有非抽象成員      

```c#
//抽象類別
public abstract class Person 
{
    //抽象類別當中可以有非抽象成員，同樣可以寫構造函數、字段、屬性、函數
    //但，這些成員是給自己用的，純粹是給子類用的(給繼承用)
    
    public Person(string name) { //有參的建構函式
        this.Name = _name;
    }
    //會報錯 是因為沒有無參的構造函數，再加上去就可以了
    public Person() {
    }
    
    public string Name {get; set;}
     
    //抽象方法: 加上abstract，沒有方法體
    public abstract int Test(string name);

    public virtual void SayHello() { }
}
```

### 繼承與實作抽象類別

```c#
class 類別名稱 : 抽象類別名稱
{
    //實作抽象類別的屬性和方法, abstract改成override
    public override 資料型別 屬性名稱 { get; set; }
    public override [資料型別|void] 方法名稱([參數群])
    {
        //實作程式碼
    }
}
```

#### 範例
由於抽象成員沒有任何實現，所以子類必須將抽象成員重寫。

```c#
//抽象成員只允許在抽象類別當中，不能在普通類別中有抽象成員
public abstract class Person {
    //抽象方法不能有任何方法實現(沒有方法體)
    public abstract int Test(string name);
}

//子類在重寫的時候，必須跟父類的這個抽象方法具有相同的簽名(返回值和參數一樣)
public class Student : Person {
    public override int Test(string name) {
        return 123;
    }
}
```

## 範例

宣告一個抽象類別，並透過繼承抽象類別的子類別來實作抽象類別內所定義的抽象屬性和抽象方法。

```c#
//使用調用
SportCar sportCar = new SportCar();
Console.WriteLine($"車名：{sportCar.Name}\r\n車門數：{sportCar.Door}\r\n引擎技術：{sportCar.EngineTechnology(true)}\r\n馬力：{sportCar.HP}\r\n供油方式：{sportCar.FuelSystem}");

/* 執行結果:
車名：Audi R8
車門數：2
引擎技術：渦輪增壓
馬力：205
供油方式：多點噴射

*/

//抽象類別
abstract class Car
{
	public abstract string Name { get; set; } //車名
	public abstract int Door { get; set; } //車門
	public abstract int HP { get; set; } //馬力
	
	//引擎技術(抽象方法)
	public abstract string EngineTechnology(bool isTurbo); 
	
	//供油方式(普通方法)
	public string FuelSystem()
	{
		return "多點噴射";
	}
}

//宣告跑車類別，並繼承抽象類別Car
class SportCar : Car
{
    //用override來實作抽象屬性
    public override string Name { get; set; } = "Audi R8"; //車名
    public override int Door { get; set; } = 2; //車門
    public override int HP { get; set; } = 140;//馬力
    
    //用override來實作抽象方法
    public override string EngineTechnology(bool isTurbo)
    {
        if (!isTurbo)
        {
            this.HP = 140;
            return "自然進氣";
        }

        this.HP = 205;
        return "渦輪增壓";
    }
}
```

---

什麼情況會用到介面interface？       
當你需要多繼承的時候，就要考慮介面interface了。

為什麼呀？我就不能用抽象類嗎？      
因為一個類別只能繼承一個抽象類別，是不允許多繼承的，只能繼承一個父類(一個基底類別)    

> 介面可以被多重實現，抽象類別只能被單一繼承        
> 一個類別只能繼承一個抽象類別，一個類別可以實現多個介面。  


## 實現多型的三種手段

- 虛方法 `virtual` (virtual, override)
- 抽象類 `abstract` (abstract, override)
- 介面 `interface`

## 什麼時候用虛方法、抽象類？

- 父類的函式有實現、有意義的時候，就用虛方法(virtual)
- 父類的函式不知道怎麼去實現，就用抽象類(abstract)



[MSDN - abstract (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/abstract)       
[MSDN - 如何定義抽象屬性 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/how-to-define-abstract-properties)     
[c#中抽象类(abstract)和接口(interface)的相同点跟区别](https://blog.csdn.net/lidandan2016/article/details/78831865)   
[【总结】abstract class抽象类与interface之间的区别](https://blog.csdn.net/u012556994/article/details/81563255)      
[[C# 筆記][多型] Abstract 抽象類](https://riivalin.github.io/posts/2011/01/abstract/)      
[[C# 筆記] Abstract 抽象類與抽象成員](https://riivalin.github.io/posts/2010/01/r-csharp-note-16/)   
[[C# 筆記] 里氏轉換(LSP)  by R](https://riivalin.github.io/posts/2011/01/lsp/)    
[實現多態(多型)的三個方法 ([C# 筆記] .Net基礎-複習-R)  by R](https://riivalin.github.io/posts/2011/02/r-cshap-notes-3/#6多態多型)       
    


Book: Visual C# 2005 建構資訊系統實戰經典教本    