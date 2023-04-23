---
layout: post
title: "[C# 筆記][多型] Interface 介面複習"
date: 2011-02-12 00:01:31 +0800
categories: [Notes,C#]
tags: [C#,virtual,abstract,interface,多型,OO,物件導向]
---


## 回顧 虛方法 virtural、抽象方法 abstract
### 虛方法 virtural
關於虛方法需要注意的幾點：
1. 父類中如果有方法需要讓子類重寫，則可以將該方法標記為 `virtual`
2. 虛方法在父類中必須有實現，哪怕是空實現       
什麼是空實現？有大括號，沒有內容 `void T() { }`   
你有了大括號就叫做有方法體，你裡面什麼都不寫，叫空實現    
 
```c#
//如果我這個方法需要被子類重寫，可以標記為虛方法virtual
public virtual void SayHello()
{
    //虛方法，可以空實現
    //有方法體(大括號{})，沒有內容，什麼都不寫
}
```  
> 有沒有方法體？有，因為有大括號`{}`    
> 有沒有實現？沒有，因為沒有內容，什麼都不寫   
3. 虛方法子類可以重寫(`override`)，也可以不重寫     
子類可以重寫(override)父類方法   
```c#
//子類可以重寫(override)父類方法
public override void SayHello() {
    base.SayHello();
}
``` 
也可以不重寫父類方法   
``` c#
//也可以不重寫父類方法(沒有override標記)
public void SayHello() {
}
```
但是，如果此類是抽象類別，子類必須要重寫父類的抽象成員  
```c#
public abstract class Person { //抽象類別
    public abstract void Test(); //抽象函數 => 子類必須要重寫該方法
}
```

### 抽象方法 abstract
關於抽象方法需要注意的幾點：
1. 需要用 `abstract` 關鍵字標記 
2. 抽象方法不能有任何方法實現   
```c#
//抽象方法 加上abstract，沒有方法體
public abstract int Test(string name); 
``` 
3. 抽象成員必須在抽象類別中 
抽象成員必須在抽象類別中，但是抽象類別當中可以有非抽象成員，同樣可以寫字段、屬性、函數
```c#
//抽象類別
public abstract class Person {
    //抽象類別當中可以有非抽象成員，同樣可以寫構造函數、字段、屬性、函數
    //但，這些成員是給自己用的，純粹是給子類用的(給繼承用)
    public Person(string name) {
        this.Name = _name;
    }
    //會報錯是因為沒有無參的構造函數，再加上去就可以了
    public Person() {
    }
    private string _name;
    public string Name
    {
        get { return _name; }
        set { value = Name; }
    }
    //抽象方法
    public abstract int Test(string name);
    public virtual void SayHello() { }
}
``` 
4. 由於抽象成員沒有任何實現，所以子類必須將抽象成員重寫。   
```c#
//抽象成員只允許在抽象類別當中，不能在普通類別中有抽象成員
public abstract class Person {
    //抽象方法不能有任何方法實現(沒有方法體)
    public abstract int Test(string name);
}
//子類在重寫的時候，必須跟父類的這個抽象方法具有相的簽名(返回值和參數一樣)
public class Student : Person {
    public override int Test(string name) {
        return 123;
    }
}
``` 
5. 抽象類別不能實體化
6. 抽象類的作用，抽象類別的作用就是為了讓子類繼承
7. 抽象類別中可以包括抽象成員、可以包括有具體有代碼的成員
8. 抽象方法不能用 `static` 修飾     

Q：為什麼 抽象方法不能是靜態的`static`？    
我們調用函數的時候，是拿子類物件去調的，這樣搞的話，類名是不是也可以去調用了(因為靜態類是用類名去調用函數)，所以他不允許你這樣子，你必須拿對象(物件)去調這個函數。

## Interface 介面
1. 介面中只含方法(屬性、事件、索引器也都是方法)
2. 介面中的成員都不能有任何實現 
介面中的函數，跟抽象類的抽象函數是一樣的。四個字形容：光說不做。    
由誰做呀？子類去做。        
3. 介面不能被實體化 
現在我們己知的，不能被實體化的有：介面、抽象類、靜態類
4. 介面中的成員不能有任何訪問修飾符。(默認為 `public`)  
```c#
public interface IFlyable {
    //函數前面不能有修飾符
    //在介面當中，所有成員默認都是public，也不讓你加public
    void Fly();
}
``` 
5. 實現介面的子類必須將介面中的所有成員全都實現(與抽象類相同) 
跟抽象類一樣。抽象類裡的所有成員，必須全部都實現。      
6. 子類實現介面的方法時，不需要任何關鍵字，直接實現即可。 
而我們在實現virtual虛方法和abstract抽象函數的時候，都需要加上override，而實現介面的時候不需要。 
7. 介面存在的意義就是為了多態(多型)

Q：介面是什麼？什麼時候要使用介面 `interface`？ 使用介面的目的是什麼？  
## 介面是什麼？ 
介面是規範一個能力，尤其是側重於表現於那個能力。    
例如：鳥會飛、飛機會飛。「飛」這個能力就可以寫成介面

## 什麼時候要使用介面 `interface`？     
例如：鳥會飛、飛機會飛，他們是不是都會飛呀？你能提一個父類讓他們去繼承嗎？不能。
        
鳥會飛、飛機會飛。
他們是不是都有一個會飛的方法，對吧。但是你能不能把「飛」的方法單獨拿出來，封裝成一個父類？不能。有東西可以做為鳥跟飛機的父類嗎？沒有。

所以說，你只能把這個「飛」寫成一個「介面」

```c#
public interface IFlyable
{
    //函數前面不能有修飾符
    //在介面當中，所有成員默認都是public，也不讓你加public
    void Fly();
}
```

## 使用介面的目的是什麼？
為了多態(多型)，表現出多種類型。實現多態(多型)的三種方法：虛方法`virtual`、抽象`abstruce`、介面`interface`。我們在使用介面實現多態(多型)的時候，聲明的也是介面類型，指向的也是誰呀？子類對象。

表面上我調的是我介面裡面自己的Fly，實際上我調的是子類的Fly

```c#
namespace Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //介面是什麼？什麼時候要使用介面 `interface`？ 使用介面的目的是什麼？多態/多型
            //鳥會飛 飛機也會飛
            IFlyable fly = new Plane();//new Bird();
            fly.Fly();
            Console.ReadKey();
        }
    }
    public interface IFlyable
    {
        //函數前面不能有修飾符
        //在介面當中，所有成員默認都是public，也不讓你加public
        void Fly();
    }
    //當你繼承了介面，就必須要實現介面的成員
    public class Bird : IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("鳥會飛");
        }
    }
    public class Plane : IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("飛機會飛");
        }
    }
}
```
再一個例子，鳥會飛、飛機會飛，我再加上麻雀會飛，企鵝不會飛。    

寫一個鳥類的父類Bird，所有的鳥類都繼承父類，我能在父類裡面提供一個會飛的方法，讓兩個鳥類去繼承嗎？不能，因為不是所有的鳥都會飛，比如說，企鵝就不會飛。這時候，這個「飛」就只能寫成一個介面。       

現在這個麻雀應該是，既要繼承於Bird鳥類，也繼承於這個IFlyable介面，那麼在語法上，我們要求把誰寫在前面？把父類寫在前面。

```c#
//鳥類的父類
public class Bird
{
    //因為不是所有的鳥都會飛，所以裡面不能提供「飛的方法」
}
//當你繼承了介面，就必須要實現介面的成員
public class Maque : Bird, IFlyable
{
    public void Fly() {
        Console.WriteLine("麻雀會飛");
    }
}
public class QQ : Bird //企鵝
{
}
public interface IFlyable
{
    //函數前面不能有修飾符
    //在介面當中，所有成員默認都是public，也不讓你加public
    void Fly();
}
```
再來，下面這段程式碼：  
這個函數是父類的？還是子類的？還是實現介面的？        
```c#
public class Maque : Bird, IFlyable
{
    /// <summary>
    /// 這個函數是父類的？還是子類的？還是實現介面的？
    /// </summary>
    public void Fly() //我是介面的
    {
        Console.WriteLine("麻雀會飛");
    }
}
```
一個子類去繼承了一個介面，我們在語法上要求你要子類必須要實現介面中的成員，對吧？如果說，你這個方法是父類的，或者是自己的，但，就不是介面的，那語法上能不能通過？不能。所以說，現在語法上建置成功了，所以說，你這個函數肯定是誰的？介面的。

現在給你報一個警告，是因為你自己提供的這個函數跟父類的函數一樣了，從繼承角度來說，你會隱藏掉你繼承過來的這個函數，對吧。

---

承上，如果，我想要這個函數表示為我麻雀自己的函數，而不是實現介面的函數，這事我該怎麼去做？

實現介面的函數有兩種方式：  
1. 普通實現 (實作介面)
2. 顯示實現介面 (明確實作所有成員)：解決方法重名的問題

顯示實現介面 (明確實作所有成員)只有一個目的，就是解決方法重名的問題。
當你子類中的函數，跟介面中函數名稱一樣的時候，那麼，你在實現這個介面的時候，必須要「顯示實現介面 (明確實作所有成員)」去實現。

```c#
public class Maque : Bird, IFlyable
{
    /// <summary>
    /// 這個函數是父類的？還是子類的？還是實現介面的？
    /// </summary>
    public void Fly() //子類自己的
    {
        Console.WriteLine("麻雀會飛");
    }
    //介面的，使用顯示實現介面 (明確實作所有成員)」去實現
    void IFlyable.Fly()
    {
        Console.WriteLine("實現介面的飛方法");
    }
}
```