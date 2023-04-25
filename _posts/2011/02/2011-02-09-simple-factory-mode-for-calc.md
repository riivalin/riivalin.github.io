---
layout: post
title: "[C# 筆記][多型] 物件導向計算機 -複習"
date: 2011-02-09 00:15:28 +0800
categories: [Notes,C#]
tags: [C#,LSP,OO,多型]
---

## 多態(多型)的語法和用法

實現多態(多型)的三個方法
1. 虛方法`virtual`
2. 抽象類`abstract`
3. 接口(介面)`interface`

Q：什麼時候用虛方法`virtual`？  
如果這個父類有意義，能夠創建對象(物件)，並且這個方法可以調用的話，可以寫成虛方法。

Q：什麼時候用抽象類`abstract`？  
如果說父類不知道如何實現，方法也不知道怎麼寫，可以用抽象類

---

## 案例：物件導向計算機
### 用物件導向的思想來構思

先想類，用物件導向的思想來想
- 父類：運算符
- 子類：+-*/

在父類、子類裡面寫什麼？    
反正我們要的這幾個子類，目的就是要把把結果算出來    
那是不是應該有個計算的方法呀    
那麼，你父類知道子類怎麼去計算嗎？不知道    
那就寫成一個抽象函數。   

```c#
//運算符 父類
public abstract decimal GetResult();//計算結果
```
根據去算呀？得根據用戶輸入兩個數字來算啊。       
而且子類都需要這兩個數字，對吧。    
要傳參的話，每個子類都要傳，很麻煩呀，    
乾脆寫成屬性，兩個屬性。 

```c#
//所有運算符的父類
NumberOne, NumberTwo //兩個屬性
public abstract decimal GetResult(); //計算結果
```
那麼，在創建對象的時候，我是不是還需要一個構造函數，把兩個數據可以傳過去啊？對吧。

```c#
//所有運算符的父類
//3構造函數
NumberOne, NumberTwo //2兩個屬性
public abstract decimal GetResult(); //1計算結果
```
這個兩個屬性和構造函數，父類自己能用嗎？不能用了，純粹給子類用的
> 抽象類無法創建對象(物件)，為什麼? 因為它沒方法，創建沒有意義
    
子類就好辦了，重寫父類的抽象方法

### 父類&子類
```
父類：
  1. 計算結果 public abstract decimal GetResult(); 
  2. 兩個屬性 NumberOne, NumberTwo
  3. 構造函數
子類：
  重關父類的抽象方法
```

### 模擬用戶輸入
```
請輸入第一個數字
numberOne
請輸入第二個數字
numberTwo
請輸入運算符
string operate
```

### 簡單工廠
現在拿到了一個運算符，要開始計算了，但有一個問題，你知道人家要的是哪個運算符嗎？不知道咩。    
這時候又需要我們用什麼呀？    
簡單工廠來返回一個什麼樣的計算對象出來？
由於咱們不知道你要的是哪個運算符，
所以說，我們返回一個父類對象回去

```c#
//簡單工廠：返回一個父類對象
return Father(); //裡面裝的一定是某一個子類
```
##### Q：什麼要用簡單工廠呀？      
用戶在控制台輸入了一個運算符，我們應該要根據運算符，創建一個對應的計算對象，你是加的，我是創建加的，你是減的，我創建減的，對吧!但是你知道人家的是什麼嗎？不知道咩，這個時候我們就通過簡單工廠，返回一個父類，我返回一個父類，是不是屏蔽了所有子類的一個差異，對吧!因為我們己經用抽象類來實現多態了，最終我調父類的函數，這個抽象函數其實最終調的是什麼？是被子類重寫的這個函數。
    
### 處理返回的父類對象Father()
    
`return Father();` 這樣一個父類對象回去，但這個裡面裝的一定是其中的某一個子類對象

我們拿到這個返回的`Father()`，我們去調用他的計算方法
```c#
father.計算; //計算=>抽象類裡的抽象方法
```
表面上雖然我調的是我自己父類裡面的抽象函數，但是，都被我每一個子類重寫了。
    
具體我會調哪一個類的函數，取決於你裡面裝的是誰的對象(物件)

## 實現計算機-多型
### 父類(抽象class)

寫一個父類的抽象class，這裡面提供兩個屬性，用來儲存用戶輸入的兩個數字，再提供一個抽象的計算方法，再提供一個構造函數。

```c#
//父類:所有運算符的父類
public abstract class CalcFather
{
    //提供兩個屬性，用來儲存用戶輸入的兩個數字(純粹提供給子類用)
    public decimal NumberOne { get; set; }
    public decimal NumberTwo { get; set; }

    //構造函數(純粹提供給子類用)
    public CalcFather(decimal n1, decimal n2)
    {
        this.NumberOne = n1;
        this.NumberTwo = n2;
    }

    //抽象的計算方法
    public abstract decimal GetResult();
}
```
### 子類 加法 +
```c#
public class Add : CalcFather //繼承父類抽象類
{
    //構造函數
    public Add(decimal n1, decimal n2) 
        : base(n1, n2) //繼承父類的構造函數
    {
    }

    //重寫父類抽象方法
    public override decimal GetResult()
    {
        return this.NumberOne + this.NumberTwo;
    }
}
```

### 子類 減法 - 
```c#
public class Sub : CalcFather //繼承父類抽象類
{
    //構造函數
    public Sub(decimal n1, decimal n2)
        : base(n1, n2) //繼承父類的構造函數
    {
    }

    //重寫父類抽象方法
    public override decimal GetResult()
    {
        return this.NumberOne - this.NumberTwo;
    }
}
```

### 子類 乘法 * 
```c#
```

### 子類 除法 /
```c#
public class Div : CalcFather //繼承父類抽象類
{
    //構造函數
    public Div(decimal n1, decimal n2)
        : base(n1, n2) //繼承父類的構造函數
    {
    }

    //重寫父類抽象方法
    public override decimal GetResult()
    {
        return this.NumberOne / this.NumberTwo;
    }
}
```

### 內函式實現案例

```c#
static void Main(string[] args)
{
    //虛方法、抽象類、介面
    //物件導向計算機 案例
    while (true)
    {
        Console.WriteLine("請輸入第一個數字");
        decimal n1 = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("請輸入第二個數字");
        decimal n2 = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine("請輸入要運算符");
        string operate = Console.ReadLine();

        CalcFather calc = GetCal(operate, n1, n2);
        decimal res = calc.GetResult();
        Console.WriteLine(res);
        Console.ReadKey();
    }
}
//簡單工廠
public static CalcFather GetCal(string operate, decimal n1, decimal n2)
{
    CalcFather cal = null;

    switch (operate)
    {
        case "+":
            cal = new Add(n1, n2);
            break;
        case "-":
            cal = new Sub(n1, n2);
            break;
        case "*":
            cal = new Mul(n1, n2);
            break;
        case "/":
            cal = new Div(n1, n2);
            break;
    }
    return cal;
}
```

## 完整Code

```c#
namespace 多態複習
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //虛方法、抽象類、介面
            //物件導向計算機 案例
            while (true)
            {
                Console.WriteLine("請輸入第一個數字");
                decimal n1 = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("請輸入第二個數字");
                decimal n2 = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("請輸入要運算符");
                string operate = Console.ReadLine();

                CalcFather calc = GetCal(operate, n1, n2);
                decimal res = calc.GetResult();
                Console.WriteLine(res);
                Console.ReadKey();
            }
        }
        //簡單工廠
        public static CalcFather GetCal(string operate, decimal n1, decimal n2)
        {
            CalcFather cal = null;

            switch (operate)
            {
                case "+":
                    cal = new Add(n1, n2);
                    break;
                case "-":
                    cal = new Sub(n1, n2);
                    break;
                case "*":
                    cal = new Mul(n1, n2);
                    break;
                case "/":
                    cal = new Div(n1, n2);
                    break;
            }
            return cal;
        }
    }
    //父類
    public abstract class CalcFather
    {
        //提供兩個屬性，用來儲存用戶輸入的兩個數字(純粹提供給子類用)
        public decimal NumberOne { get; set; }
        public decimal NumberTwo { get; set; }

        //構造函數(純粹提供給子類用)
        public CalcFather(decimal n1, decimal n2)
        {
            this.NumberOne = n1;
            this.NumberTwo = n2;
        }

        //抽象的計算方法
        public abstract decimal GetResult();
    }
    //子類 +-*/
    public class Add : CalcFather //繼承父類抽象類
    {
        //構造函數
        public Add(decimal n1, decimal n2)
            : base(n1, n2) //繼承父類的構造函數
        {
        }

        //重寫父類抽象方法
        public override decimal GetResult()
        {
            return this.NumberOne + this.NumberTwo;
        }
    }
    public class Sub : CalcFather //繼承父類抽象類
    {
        //構造函數
        public Sub(decimal n1, decimal n2)
            : base(n1, n2) //繼承父類的構造函數
        {
        }

        //重寫父類抽象方法
        public override decimal GetResult()
        {
            return this.NumberOne - this.NumberTwo;
        }
    }
    public class Mul : CalcFather //繼承父類抽象類
    {
        //構造函數
        public Mul(decimal n1, decimal n2)
            : base(n1, n2) //繼承父類的構造函數
        {
        }

        //重寫父類抽象方法
        public override decimal GetResult()
        {
            return this.NumberOne * this.NumberTwo;
        }
    }
    public class Div : CalcFather //繼承父類抽象類
    {
        //構造函數
        public Div(decimal n1, decimal n2)
            : base(n1, n2) //繼承父類的構造函數
        {
        }

        //重寫父類抽象方法
        public override decimal GetResult()
        {
            return this.NumberOne / this.NumberTwo;
        }
    }
}
```

- [[C# 筆記] 多型(Polymorphism)-抽象類 Abstract](https://riivalin.github.io/posts/abstract/)
- [[C# 筆記] 多型(Polymorphism)-虛方法 Virtual](https://riivalin.github.io/posts/polymorphism/)
- [[C# 筆記] 多型(Polymorphism)-抽象類 Abstract 練習](https://riivalin.github.io/posts/abstract-practice/)