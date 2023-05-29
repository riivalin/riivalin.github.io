---
layout: post
title: "[C# 筆記] 構造方法繼承的要點"
date: 2010-01-11 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,Constructor,base]
---

```
父類/基類
子類/派生類
```

- 在初始化時，基類構造方法總是會首先運行
- 基類的構造方法不會被繼承，在派生類需要重新定義

## 舉例1：在初始化時，基類構造方法總是會首先運行
- 先創建一個`Staff`類，構造函數什麼都不做，只輸出一句話「員工類初始化」
- 再創建一個`Manager`類，繼承`Staff`類，構造函數什麼都不做，只輸出一句話「經理類初始化」

```c#
//父類/基類
public class Staff
{
    public Staff()
    {
        Console.WriteLine("員工類初始化");
    }
}
//子類/派生類
public class Manager : Staff
{
    public Manager()
    {
        Console.WriteLine("經理類初始化");
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        var manager = new Manager();
        Console.Read();
    }
}
//輸出:
//員工類初始化
//經理類初始化
```
令人費解的是，我們只是創建了`Manager`這一個類，但是在命令行中，我們得到了兩行輸出，首先是「員工類初始化」，然後「經理類初始化」。       

這個輸出表明，當我們在創建`Manager`物件的時候，作為派生類，它不僅調用了自己的構造方法，甚至還會在調用自己之前，先調用基類，也就是`Staff`類的構造方法。      

所以從物件初始化的角度來說，任何一個類的初始化，都會首先調用基類的構造方法，然後再一層一層的調用派生類的構造方法。      

## 舉例2：基類的構造方法不會被繼承，在派生類需要重新定義

再加一點難度，如果每個員工都有一個共有的屬性：員工編號，我們在初始化員工的時候，除了默認的構造方法外，我們還會有一個帶參數的構造方法，需要在初始化員工的時候，同時傳入員工的編號，並且把這個編號保存在屬性`Number`中。      

然後在經理類操作，創建一個帶參數的構造方法，這個參數就是員工編號，不過按照剛剛的理論，在初始化派生類物件的時候，會首先執行基類的構造方法，既然會先執行`Staff`類的構造方法，也就是說，我們似乎沒有必要在經理類的構造方法中，重複給員工的號碼賦值了，那麼現在我們就把經理類的構造方法留空，只是加一句`Console.WriteLine`來提示一下吧！        

接著回到`Main`方法中，在創建`Manager`物件的同時，我們傳入員工編號123，然後`Console.WriteLine(manager.Number);`輸出一下結果。   

```c#
public class Staff
{
    public Staff()
    {
        Console.WriteLine("員工類初始化");
    }

    public int Number { get; set; }
    public Staff(int number)
    {
        this.Number = number;
    }
}
public class Manager : Staff
{
    public Manager()
    {
        Console.WriteLine("經理類初始化");
    }
    public Manager(int number) {
        Console.WriteLine($"{number}經理類初始化");
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        var manager = new Manager(123);
        Console.WriteLine(manager.Number);
        Console.Read();
    }
}
//輸出
//員工類初始化 -->調用的是默認的「無參構造函數」
//123經理類初始化
//0
```

運行一下，結果`Manager`物件編號為`0`，一般來說，整數對象如果在沒有賦值的時候，它就是`0`，所以這就說明我們的構造方法，根本就沒有對員工編號進行賦值。     

如果再仔細觀察命令行的輸出，第一行是「員工類初始化」，代表它調用的是`Staff`的「無參構造函數」，並不是按照我們預期的那樣，調用`Staff`類中有參數的構造方法來實體化，而是調用了一個無參的默認的構造方法。         

實真上，如果一個`class`中存在多個構造方法，那麼它在處理類別繼承的時候，就面臨著多個構造方法選擇的一個問題，默認情況下，無論這個基類有多少個構造方法，它的衍生類，它的派生類在創建對象的時候，都會默認使用無參數構造方法來進行繼承工作，所以這就是為什麼我們的`Manager`物件在賦值的時候會失敗。      

那麼對於一個類挪有多個構造方法，這個時候我們該如何選擇，才能在繼承過程中順利處理我們所需要的初始化方法呢？      

那麼，這個時候就需要使用到`base`這個關鍵詞了。

## 使用 Base 繼承構造方法
現在試試使用`base` 關鍵詞來選擇「繼承構造方法」吧。     

在派生類中，使用`base` 來調用基類構造方法的語法比較特殊，我們需要在聲明衍生類構造方法的時候，同時在進入方法體之前使用「`冒號:`」，加上 `base`關鍵詞來對基類構造方法進行訪問`:base`，從邏輯上來說，這就相當於執行了一個方法調用。        

所以，我們還可以在調用方法的時候，添加參數的傳遞，通過使用不同的參數組合，我們就實現了對基類構造方法的選擇。        

比如說，沒有傳參的時候`:base()`，將會使用默認的無參數構造方法，如果傳遞了參數 `number`=>`:base(number)`，那麼這個時候將會啟用`Staff類`中，有參數的構造方法來參與實體化。    

```c#
public Manager(int number) : base(number)
{
    Console.WriteLine($"{number}經理類初始化");
}
```

現在讓我們再運行一下程式碼：    

```c#
public class Staff
{
    public Staff()
    {
        Console.WriteLine("員工類初始化");
    }

    public int Number { get; set; }
    public Staff(int number)
    {
        this.Number = number;
    }
}
public class Manager : Staff
{
    public Manager()
    {
        Console.WriteLine("經理類初始化");
    }
    public Manager(int number) : base(number)
    {
        Console.WriteLine($"{number}經理類初始化");
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        var manager = new Manager(123);
        Console.WriteLine(manager.Number);
        Console.Read();
    }
}
//輸出
//123經理類初始化
//123
```
這一次數據正常了。      


[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=30](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=30)