---
layout: post
title: "[C# 筆記] 訪問修飾符(存取修飾詞)-複習"
date: 2011-02-10 00:05:31 +0800
categories: [Notes,C#]
tags: [C#,R,存取修飾詞]
---

## 存取修飾詞
- `public` 公開的、公共的
- `private` 私有的，只能在當前類的內部訪問，類中成員們，如果不加訪問修飾符，默認就是 private
- `procteced` 受保護的，可以在當前類的內部訪問，也可以在該類的子類中訪問
- `internal` 在當前專案中都可以訪問。跟 public 一樣，在當前專案中都可以訪問。不同的是，出了這個專案，被修飾public的成員可以訪問，但是 internal 不可以。internal 的權限就限制在這個專案當中。我新增一個專案，就訪問不到被 internal 修飾的成員了
- `protected internal`他的權限就是`protected`+`internal`

> 如果 class 前面不加修飾符，默認是 `internal`。    
> 能夠修飾 類別 的訪問只有兩個：`public`、`internal`

![](/assets/img/post/access-csharp.jpeg)


## internal、protected 誰的權限大？   
- 在同一個專案裡，internal的權限比 protected 大，因為internal它在當前專案中哪都可以訪問，protected只能被繼承他的子類訪問。
- 在不同的專案裡，protected權限大，internal它在當前專案中哪都可以訪問，但是，它出了這個專案就訪問不到internal的成員。 protected 雖然只能在當前的類別的內部，和繼承它的子類中訪問的到。但是我們出了這專案，在另一個專案裡面，internal這個成員絕對是訪問不到，但是我們卻可以通過繼承關係，訪問到 protected的成員， 

---
    
## 舉例：子類的權限不可以高於父類的權限

子類的權限可以高於父類的權限嗎？不能  
為什麼不能比父類高？因為有可能會暴露父類的成員。

```c#
//父類internal只能在當前專案中訪問
internal class Person {
    public void T() {
    }
}

//子類public，意味著到別的專案，同樣可以訪問到這個子類
//由於繼承關係，可以拿到父類的成員
//這下，你在別的專案裡面，就暴露這個父類的成員
public class Teacher : Person { }
```
程式說明：
假如把父類加上internal，我的想法肯定是什麼呀？只能讓這個類別在當前的程序集(組件Assembly)(exe或dll)中進行訪問，出了這個專案還能訪問嗎？不能，但你個子類是什麼呀？Public，他意味著到別的專案，同樣可以訪問到這個子類。但是由於繼承關係，你是不是能拿到我這邊父類的成員呀？這下，你是不是在別的專案裡面，暴露這個父類的成員了，所以子類的權限不能高於父類。

## 承上，怎麼排除問題
把子類改成跟父類一樣 internal 就可以了

```c#
internal class Person {
    public void T() {
    }
}
//把子類public 改成跟父類一樣 internal
internal class Teacher : Person {
}
```

## 舉例：protected 兩個權限
protected 有兩個權限：
1. 只有自身類別與子類別才能存取使用
我在子類中可以訪問到父類的protected成員，但是在外面就拿不到了
2. 只要是繼承關係，不管兩者是否在同一程序集中，子類都有訪問父類的權限
跨專案，我可透過繼承關係，訪問到父類的protected成員


### 舉例：只有自身類別與子類別才能存取使用
我在子類中可以訪問到父類的protected成員，但是在外面就拿不到了(`internal class Program`)。   
所以，protected除了Person類和Teacher{ void T{ ... }}之外，就再也訪問不到protected

```c#
internal class Program
{
    static void Main(string[] args)
    {
        //訪問不到protected成員
        Person p = new Person(); //就算你有創建Person物件，也是訪問不到protected成員
        //拿不到_name，受保護的protected成員，要有繼承關係才拿得到
    }
}
//父類
internal class Person
{
    protected string _name;
    public void T()
    {
        //自身類別可以拿到自己所有的成員
    }
}
//子類
internal class Teacher : Person
{
    public void T1()
    {
        //protected在子類當中可以拿得到
        //通過繼承關係，我可以拿到父類的protected
        _name = "Rii";
    }
}
```

### 舉例：只要是繼承關係，不管兩者是否在同一程序集中，子類都有訪問父類的權限
跨專案，我可透過繼承關係，訪問到父類的protected成員

```c#
//專案A
namespace ProjectA
{
    public class Person
    {
        protected string _name; //受保護的
        internal int _age; //只有同一個namespace的才能存取
        public void T() { }
    }
}

//專案B(Test)
using ProjectA;
namespace Test
{
    //由於繼承關係，我在別的專案可以訪問到父類的protected成員
    //但訪問不到父類的internal成員
    public class Student : Person
    {
        public void T() {
            _name = "Rii"; //專案A的 父類的protected成員
            //訪問不到_age，因為它是被修飾internal，只能被他自己的專案訪問
        }
    }
}
```
結論：
protected兩個權限： 
- 只有自身類別與子類別才能存取使用
- 只要是繼承關係，不管兩者是否在同一程序集中，子類都有訪問父類的權限

---
## R Note:
- public: 任何人都可以存取使用
- private: 只有自身類別才能存取使用
- protected: 只有自身類別與子類別才能存取使用
(只要是繼承關係，不管兩者是否在同一程序集中，子類都有訪問父類的權限)
- internal: 只有同一個namespace的才能存取
- protected internal: protected || internal的概念

![](/assets/img/post/access-csharp.jpeg)

---

## 知識點總結

### 欄位、屬性、構造函數、this、new、base
##### Q：Field(字段/欄位)是幹什麼的？    
儲存數據用的
##### Q：屬性是幹什麼的？
保護Field(字段/欄位)用的
##### Q：構造函數是幹什麼的？   
初始化對象(物件)    
初始化對象(物件)說白了，就是給對象(物件)的每一個屬性去賦值。    
##### Q：什麼時候會調用構造函數？
當我們`new`的時候會調用構造函數，

--- 

##### this 關鍵字
`this` 有兩個作用
- 當前類的對象(物件)
- 調用自己的構造函數

##### new 關鍵字
`new` 有什麼作用？
- 創建對象。我們創建對象(物件)的時候要用`new`。
- 隱藏父類成員。隱藏從父類那邊繼承過來的成員。

子類的成員 跟父類成員的名稱一樣，會將父類的成員隱藏，隱藏帶來的效果是什麼？子類調不到父類的成員。如果你是故意這麼做的話，就要在返回值前面加個 `new`
```c#
public class Person
{
    public void T()
    {
    }
}
public class Teacher : Person
{
    public new void T() //如果故意要跟父類同名，要加new
    {
    }
}
```
> 警告CS0108 'Teacher.T()' 會隱藏繼承的成員 'Person.T()'
若本意即為要隱藏，請使用 new 關鍵字。

##### base 關鍵字
`base`調用父類的構造函數，在子類中調用父類重名的方法

##### 繼承：
繼承是指類別與類別之間的關係

#### 多型
`virtual`虛方法、`abstract` 抽象類、`interface` 介面

#### interface 介面 
- 介面存在的意義：多型。多型的意義：程序可擴展性
- 介面解決了類別多繼承的問題
- 介面之間可以多繼承。

--- 
## 物件導向
### 什麼是面向對象(物件導向)
- 一種分析問題的方式(增強了程序的可擴展性)
- 面向對象(物件導向)三大特性：封裝、繼承、多態(多型)

### 類和對象(類別和物件)
- 類別：類是一個模子，確定對象將會挪有的特徵(屬性)和行為(方法)
- 物件：對象(物件)是一個你能夠看得到、摸得著的具體實體—--萬物皆物件

### 什麼是類(類別)？什麼是對象(物件)？類和對象的區別？
物件 & 類別 = 大樓 & 藍圖 
這個大樓是根據設計藍圖構造出來的，圖紙上有什麼東西，咱們這個大樓裡面是不是也得有什麼東西，對吧。    

- 類別算是一個藍圖、一個範本、一個可參考的文件，他沒有 實體 (Instance) 的概念，屬靜態的。
- 物件是一個看的到、摸的到的實體，屬於動態的，狀態會隨時改變，但架構與行為不會改變。
        
* 類是模具、創建對象的模具、抽象的
    * 類是一種數據類型，用戶自定義的數據類型
    * 類的組成：字段、屬性、方法、構造函數等
* 對象是具體的，是類的具體實例。對象具有屬性(特徵)和方法(行為)
* 類中包含了數據(用字段表示)與行為(用方法/函數/功能)表示
    * 方法為一塊具有名稱的代碼
- this 當前對象，顯示的調用自己的構造函數
- base 調用父類的構造函數，在子類中調用父類的重名方法


### 知識點總結
- 類別對物件本身就是封裝的體現
- 1.屬性封裝了Field(字段/欄位)、2.方法的多個參數封裝成了一個對象、3.將一堆代碼封裝到了一個小程序集中(dll、exe)。將一坨程序封裝起來到一個程序集當中
- 繼承是指類別與類別之間的關係
- 為什麼要繼承？繼承帶給我們的好處？代碼重用
- LSP里氏轉換原則(聲明父類類型變量，指向子類類型對象，以及調用方法時的一些問題)、多態(多型)、類的單根繼承性、傳遞性、繼承時構造函數的問題
- 所有的類都直接或間接的繼承自object類
- 繼承中的訪問修飾符問題




- [[C# 筆記] C#中的訪問修飾符(存取修飾詞)](https://riivalin.github.io/posts/access-modifiers/)
- [存取修飾詞 (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/access-modifiers)