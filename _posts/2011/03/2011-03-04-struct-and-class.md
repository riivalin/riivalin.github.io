---
layout: post
title: "[C# 筆記] Struct & Class 結構和類別的區別"
date: 2011-03-04 23:53:00 +0800
categories: [Notes, C#]
tags: [C#,struct,class,stack,heap]
---

## 從類型來看
- Struct 結構：值類型。它的值是分配在內存的`stack`(堆疊/棧)上面
- Class 類別：引用類型。它的值是分配在內存的`heap`(堆積/堆)上面

- [[C# 筆記] 值傳遞 & 引用傳遞](https://riivalin.github.io/posts/2011/01/valuetype-referencetype/)
- [[C# 筆記] Stack (堆疊/棧) & Heap (堆積/堆)](https://riivalin.github.io/posts/2011/01/stack-heap/)

## 從聲明的語法來看
- 聲明的語法：`class` & `struct`
- 在`class`類別中，構造函數裡，既可以給字段賦值，也可以給屬性賦值。構造函數是可以重載的(也就是說，可以在類當中寫多個參數不同的構造函數)。   
- 但是，在`struct`結造的構造函數當中，必須只能給字段賦值。  
- 在`struct`結構的構造函數當中，我們需要**全部**的字段賦值，而不能去選擇的給字段賦值。  

### 在`struct`構造的構造函數當中，必須只能給字段賦值

```c#
public struct PersonStruct
{
    //字段、屬性、方法、構造函數
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private int _age;
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    private char _gender;
    public char Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }

    public PersonStruct(string name, int age, char gender)
    {
        ////ERROR:在struct構造的構造函數當中，必須只能給字段賦值，不能給屬性賦值。
        //this.Name = name;
        //this.Age = age;
        //this.Gender = gender;

        //在`struct`構造的構造函數當中，必須只能給字段賦值。
        _name = name;
        _age = age;
        _gender = gender;
    }
}
```
### 在`struct`結構的構造函數當中，我們需要全部的字段賦值，不能去選擇的給字段賦值
在`struct`結構的構造函數當中，我們需要全部的字段賦值，不能去選擇的給字段賦值，所以`struct`結構也只能寫一個構造函數。但是new會有兩個構結函數，一個是自己寫的「全參構造函數」，另外一個是默認的「無參構造函數」。

```c#
//ERROR:在struct構造的構造函數當中，我們需要**全部**的字段賦值，而不能去選擇的給字段賦值
//public PersonStruct(string name)
//{
//    _name = name;
//}
```

## 從調用來看
### new
class類會`new`對象       
`PersonClass p = new PersonClass();`        

new 會做三件事：
1. 在`heap`(堆積/堆)中開闢一塊空間
2. 在開闢的`heap`空間中，創建對象(物件)
3. 調用這個對象(物件)的構造函數，進行初始化

### Q：結構是否可以`new`？
可以。   

```c#
PersonClass pc = new PersonClass(); //類
PersonStruct ps = new PersonStruct(); //結構
```

### Q：struct結構的new和class類的new，他們的區別在哪裡？

結構的`new`是在`stack`(堆疊/棧)中開闢空間。    

簡單來說，結構的這個`new`只做了一件事，幹嘛呢？ 
就是調用結構的構造函數。

類class本身會有一個默認的「無參的構造函數」，當你寫了一個新的構造函數，那麼默認的無參的構造函數，就新的取而代之了，那個默認無參的就沒有了。

## 結構和類的構造函數
對於結構和類的構造函數，他們的相同點：
- 相同點：不管是結構還是類別，本身都會有一個默認的無參數的構造函數。

不同點：
- 不同點：當你在類中寫了一個新的構造函數之後，那個默認的無參的構造函數就被幹掉了(沒有了)。
- 但是，在結構當中，如果你寫了一個新的構造函數，那麼默認的無參數的構造函數依然存在。
- 在`class`類別中，構造函數裡，既可以給字段賦值，也可以給屬性賦值。構造函數是可以重載的(也就是說，可以在類當中寫多個參數不同的構造函數)。 
- 但是，在`struct`結造的構造函數當中，只能夠給字段賦值。並且是給全部的字段賦值，而不能去選擇的給字段賦值。


## 什麼時候去用類？什麼時候去用結構？

類是一個引用類型，結構是一個值類型。    
如果我們只是單純的去儲存數據，我們推薦使用結構。    
因為結構他的值是分配在我們stack(堆疊/棧)上，比較節省空間。  
注意，是「單純的儲存數據」。不涉及到面向對象(物件導向)。    

如果我們想要使用面向對象(物件導向)的思想來開發程式，我們推薦使用`class`，因為結構不具備面向對象(物件導向)的特徵。

## Code
```c#
internal class Program
{
    static void Main(string[] args)
    {
        //從類型來看
        //Struct 結構：值類型。它的值是分配在內存的`stack`(堆疊 / 棧)上面
        //Class 類別：引用類型。它的值是分配在內存的`heap`(堆積 / 堆)上面

        //調用
        PersonClass pc = new PersonClass();

        //結構是否可以new？可以
        //結構new  在stack(堆疊/棧)中開闢空間  調用結構的構造函數
        PersonStruct ps = new PersonStruct();

        ps.M1();//結構的非靜態方法
        PersonStruct.M2();// 類名.方法 結構的靜態方法

        Console.ReadKey();

        //結構和類的構造函數：
        //相同點：不管是結構還是類別，本身都會有一個默認的無參數的構造函數。
        //不同點：當你在類中寫了一個新的構造函數之後，那個默認的無參的構造函數就被幹掉了(沒有了)。
        //但是，在結構當中，如果你寫了一個新的構造函數，那麼默認的無參數的構造函數依然存在。

        //如果我們只是單純的去儲存數據，我們推薦使用結構。  

        //如果我們想要使用面向對象(物件導向)的思想來開發程式，我們推薦使用`class`，因為結構不具備面向對象(物件導向)的特徵。
    }
}
public class PersonClass
{
    //字段、屬性、方法、構造函數
}

public struct PersonStruct
{
    //字段、屬性、方法、構造函數
    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private int _age;
    public int Age
    {
        get { return _age; }
        set { _age = value; }
    }

    private char _gender;
    public char Gender
    {
        get { return _gender; }
        set { _gender = value; }
    }
    public void M1()
    {
        Console.WriteLine("我是結構中的非靜態方法");
    }
    public static void M2()
    {
        Console.WriteLine("我是結構中的靜態方法");
    }

    public PersonStruct(string name, int age, char gender)
    {
        //ERROR:在struct構造的構造函數當中，必須只能給字段賦值，不能給屬性賦值。
        //this.Name = name;
        //this.Age = age;
        //this.Gender = gender;

        //在`struct`構造的構造函數當中，必須只能給字段賦值。
        _name = name;
        _age = age;
        _gender = gender;
    }

    //ERROR:在`struct`構造的構造函數當中，我們需要**全部**的字段賦值，而不能去選擇的給字段賦值
    //public PersonStruct(string name)
    //{
    //    _name = name;
    //}
}
```


- [[C# 筆記] 值傳遞 & 引用傳遞](https://riivalin.github.io/posts/2011/01/valuetype-referencetype/)
- [[C# 筆記] Stack (堆疊/棧) & Heap (堆積/堆)](https://riivalin.github.io/posts/2011/01/stack-heap/)
