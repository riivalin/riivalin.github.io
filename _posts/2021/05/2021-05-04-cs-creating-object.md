---
layout: post
title: "[C# 筆記] 建立物件(Creating Object)"
date: 2021-05-04 24:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,class,object,new]
---

# 什麼是物件？
所謂的「物件」就是指一個「實際存在」的東西。也就是「可使用的類別執行個體」。        

而每個物件都有自己的變數、屬性和方法。      

例如：跑車是耍帥必備的行頭，在世界上每一台跑車都是實際的物件，以保時捷911來說，它雖然是跑車，但是它還是屬於「汽車」的一種，擁有一般「汽車」共同的特性，如：四個輪子、方向盤、車大燈、擋風玻璃等等，只是材質和價格與一般汽車不同而已。      

因此，我們可以將「汽車」視為`Class`，也就是車體設計的基本藍圖，而保時捷911跑車這個`object`就是根據汽車類別的藍圖所加以打造的超級跑車。      

- 類別(`Class`)：定義程式的方法、屬性    
- 物件(`Object`)：可執行的東西    

## 跑車的物件範例

兩個跑車的物件範例

```
法拉利360
    屬性：18吋鋁圈、雙門、400HP、極速295km/h
    方法：多點噴射、自然進氣、把妹泡妞

保時捷911
    屬性：17吋鋁圈、雙門、420HP、極速313km/h
    方法：多點噴射、渦輪增壓、耍帥甩尾
```

由上可知，不同的物件所擁有的屬性和方法，彼此之間會有差異。

## 物件的宣告

```c#
類別名稱 物件名稱 = new 類別名稱([參數群]);
```

或是

```c#
類別名稱 物件名稱;
物件名稱 = new 類別名稱([參數群]);
```

對於已知類型的情況下，可以直接打上 `new()` 來使用建構式建立物件。

```c#
類別名稱 物件名稱 = new();
```
```c#
Test test = new() { str = "a" };
```


## 範例

宣告一個汽車的類別，並實體化物件(`new`)：      

- 屬性：鋁圈、車門、馬力、最高時速
- 方法：供油方式、引擎技術

```c#
//宣告物件
//方法一：
Car porsche = new Car();
Console.WriteLine(car.MaxSpeed); //295

//方法二：
Car ferrari;
car = new Car();
Console.WriteLine(car.MaxSpeed); //295

//方法三：對於已知的類型，直接打上 new()
Car audi = new();
Console.WriteLine(car.MaxSpeed); //295


// Car汽車類別
public class Car
{
    public int AluminumWheel { get { return 18; } } //鋁圈
    public int CarDoor { get { return 2; } } //車門
    public int Horsepower { get { return 400; } } //馬力
    public int MaxSpeed { get { return 295; } } //極速

    //供油方式
    public string FuelSystem()
    {
        return "Multi-Port Fuel Injected"; //多當噴射
    }
    //引擎技術
    public string EngineTechnology(bool engineType)
    {
        return (engineType) ? "Natural Aspirate" : "Turbo"; //自然進氣/渦輪增壓
    }
}
```

## Q&A

Q：我們常聽到的「物件(`Object`)」和「實體(`Instance`)」這二個名詞，它們是相同的東西？       

A：An instance of a class is an object. (一個`object`就是某個`class`的`instance`)
簡單的一句話就是「物件(`Object`)是類別(`Class`)的實體(`Instance`)


> - Class是用來定義object的一種東西，class的內容包含了動作（operations）與資料（data）。
> - 動作（operations）、方法（methods）和行為（behaviors）可以看作同義詞。理想上一個object的狀態只能透過動作去改變它。
> - 一個object就是某個class的instance，換句話說可以把object和instance看作是同樣的東西。只是在某些場合大家比較習慣用object這個說法，其他場合則是會用instance。       



# 類別(Class) 與 物件(Object)

類別(Class) 與 物件(Object) 是個一體兩面的東西，以下我用幾個不同的方式說明這兩著的差別：

簡單解釋：      

類別算是一個藍圖、一個範本、一個可參考的文件，他沒有 實體 (Instance) 的概念，屬靜態的。     
物件是一個看的到、摸的到的實體，屬於動態的，狀態會隨時改變，但架構與行為不會改變。      

## 比喻一：建築物

建築物  
- 類別：設計藍圖
- 物件：實際蓋好的房子  

兩者關係：設計藍圖(類別)決定房子應該怎麼蓋，決定幾台電梯、幾間房間、走道如何設計。實際蓋好的房子(物件)是照著設計藍圖所蓋出來的房子，人只能照設計藍圖的設計使用這間房子。

## 比喻二：蓋世武功

蓋世武功
- 類別：武林密笈
- 物件：修練武林密笈而成的武林高手

兩者關係：武林密笈(類別)記載許多各種攻擊與回應的方式，讓武林高手(物件)知道遭遇到什麼攻擊時要用什麼招式回應。
程式設計：每執行到我們用 new 運算子時，等同於將物件產生，也等同於成功得到武林密笈可以開始練功，或是在「建構子」的時候就已經賦予你基本功力。

基本上，類別只用來決定物件形成時的樣子，當物件形成時，物件就變成一個記憶體中的空間，記載著物件活動時暫存的資料與狀態，並且當有類別存在時有能力透過方法(Method)執行一些動作。



[物件導向基礎：何謂類別(Class)？何謂物件(Object)？  by The Will Will Web](https://blog.miniasp.com/post/2009/08/27/OOP-Basis-What-is-class-and-object)    
[什麼是物件導向（2）：Object, Class, Instance](https://teddy-chen-tw.blogspot.com/2012/01/2object-class-instance.html)  
[[C# 筆記] new 關鍵字的作用?](https://riivalin.github.io/posts/2017/02/what-does-the-new-keyword-do/)