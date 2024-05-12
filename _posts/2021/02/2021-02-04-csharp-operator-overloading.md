---
layout: post
title: "[C# 筆記] 運算子多載 (Operator Overloading)"
date: 2021-02-04 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,運算子多載,Operator Overloading, operator]
---

# 什麼是運算子多載？

`1+1`等於...? 答案當然是2。那`一瓶牛奶+另一瓶牛奶`等於幾瓶牛奶？當然是2瓶牛奶。     

如果現在定義了一個叫做「牛奶瓶(`Milk`)」的類別，當實體化兩個名稱分別為`MilkA`、`MilkB`的「牛奶瓶」物件後，下面這行指令編譯器看得懂嗎？

```c#
MilkA = MilkA + MilkB;
```
編譯器絕對看不懂`MilkA = MilkA + MilkB`，因為「牛奶瓶」類別不是 VS C# 中預設的資料型別，所以舉凡跟「牛奶瓶」類別相關的運算，編譯器看到這些指令是絕對不會理我們的。      

那麼我們可以定義自定義型別的相關運算嗎？有的，這些問題的解決方法就是「運算子多載 (Operator Overloading)」。     

運算子多載 (Operator Overloading)，就是「針對程式設計師所自行定義的新型別中，定義其算術運算子與關係運算子的方法」。     

這裡算術運算是指`+`、`-`、`*`、`/`等，而關係運算是指`==`、`>=`、`!=`等。


# 語法

```c#
public static [類別名稱] operator [運算子符號](運算子左邊的運算元, 運算子右邊的運算元)
```

假設我們剛剛要多載`Milk`類別的 `+` 號運算子，可以這樣寫：

```c#
//多載牛奶瓶類別的[+]運算子
public static Milk operator + (MilkA, MilkB) 
{
    //在此撰寫多載牛奶瓶類別的[+]運算子的內容
}
```

# Q&A

**Q**：為什麼運算子多載語法中，在[類別名稱] 前要加上`static`靜態？

**A**：因為所有的運算子多載都必須是該類別的「靜態方法(static method)」，所謂**靜態方法**是指 **「不需要實體化該類別就可以直接使用的方法」**，由於運算子也是一稱方法，所以只要是運算子都應該符合此特性才合理。       


## 合法的運算子多載符號

並不是所有符號都可以透過「運算子多載」來定義或實現，只有下列符號在運算子多載時才會被接受，而且在多載的時候要注意是「一元(Unary)運算子」還是「二元(Binary)運算子」。

- 一元(Unary)運算子：是指運算子只有一邊有運算元，例如：`i++`、`~i`。
- 二元(Binary)運算子：是指運算子的左右兩邊都有運算元，例如：`x+y`、`x>=y`。     


| 多載符號屬性       | 可多載的運算符號|
|:-----------------|:-----------------|
| 一元(Unary)運算子  | +、-、!、~、++、--、true、false |
| 二元(Binary)運算子 | +、-、*、/、%、\|、^、<<、>>、==、!=、>、<、>=、<= |


# 範例

設計一個可以計牛奶瓶數量的程式，讓使用者輸入完兩箱牛奶瓶的數量後，最後我們會程式去自動相加，並將結果顯示在螢幕上。

```c#
internal class Program
{
    static void Main(string[] args)
    {
        //分別輸入牛奶的瓶數
        Console.WriteLine("請輸入第一箱牛奶瓶的數量：");
        int milkACount = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("請輸入第二箱牛奶瓶的數量：");
        int milkBCount = Convert.ToInt32(Console.ReadLine());

        //宣告兩個Milk物件，並初始化數量
        Milk milkA = new Milk(milkACount);
        Milk milkB = new Milk(milkBCount);

        //宣告Milk物件，並作相加動作
        Milk totalMilk = new Milk();
        totalMilk = milkA + milkB;
        
        //將相加結果顯示在畫面上
        Console.WriteLine(totalMilk.ToString());
    }
}

//牛奶瓶類別
public class Milk
{
    //private int milkCount;
    //public int MilkCount {
    //    get{
    //        return milkCount;
    //    }
    //}

    //建構函式(constructor)，用來初始化牛奶瓶數
    public Milk(int milkCount = 0)
    {
        //儲存牛奶瓶的瓶數
        this.MilkCount = milkCount;
    }

    //取得目前牛奶瓶數的屬性
    public int MilkCount { get; }

    //多載牛奶瓶類別 [+]運算子
    public static Milk operator +(Milk milkA, Milk milkB)
    {
        //將二個類別的內部屬性總值相加
        return new Milk(milkA.MilkCount + milkB.MilkCount);
    }

    //重寫(override)牛奶類別的ToString方法，以方便顯示用
    public override string ToString()
    {
        return $"現在有 {MilkCount} 瓶牛奶";
    }
}
```

[MSDN - 運算子多載：預先定義的一元、算術、相等和比較運算子](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/operator-overloading)      
[[C# 筆記] 運算符重載(Operator Overloading) 1 by R](https://riivalin.github.io/posts/2010/03/95-operator-overload-1/)       
[[C# 筆記] 運算符重載(Operator Overloading) 2 by R](https://riivalin.github.io/posts/2010/03/95-operator-overload-2/)     
Book: Visual C# 2005 建構資訊系統實戰經典教本    
