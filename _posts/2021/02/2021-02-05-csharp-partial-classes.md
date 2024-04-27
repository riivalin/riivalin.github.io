---
layout: post
title: "[C# 筆記] 部分類別(Partial Classes)"
date: 2021-02-05 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,partial,Partial Type]
---

部分類別(Partial Classes)的精神是 **「把一個類別的內容，分別拆解成多個位於一個「命名空間」和「組件」下的類別檔案來撰寫」**，這可以讓撰寫同一類別時程式設計人員可以各司其職，在相同或不同的檔案中撰寫`partial class`，以增進工作效率。在編譯時期，編譯器會自動將分散於各處的`partial classes`組今成所屬的單一類別。      

# 舉例

將「人類(Human)」的類別分成兩個部分類別，分別位於`Class1.cs`和`Class2.cs`檔案，僅定義姓名和年齡的屬性。

### Class1.cs

```c#
namespace ConsoleApp1
{
    partial class Human
    {
        public string Name { get; set; }
    }
}
```

### Class2.cs

```c#
namespace ConsoleApp1
{
    partial class Human
    {
        public int Age { get; set; }
    }
}
```

### Form1.cs

定義好上述類別後，可以於`Form1.cs`檔案中實體化 `Human`類別，這時候就會發現，不等是`Class1.cs` 或 `Class2.cs`內所定義的所有公開成員都可以存取。

```c#
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Human human = new Human();
            human.Name = "Rii";
            human.Age = 99;
        }
    }
}
```

> 這裡必須要注意一點，一個大類別所有要拆解的部分類別，它的「類別名稱(非檔案名稱)」必須要一致，且所在「命名空間」和「組件」也必須一致，也就是說，其實拆解後的 `partial classes`對編譯器而言，最後要編譯時還是視為同一個`class`。


# Tips

> `partial`這個關鍵字不只可以使用在類別`class`中。        

`partial` 除了可以應用在類別`class`中外，結構(`structure`)和介面(`interface`)一樣都可以使用`partial`關鍵字來部分定義。      

也就是我們可以實現「部分結構(`partial structure`)」與「部分介面(`partial interface`)，而這三種應用加起來我們統稱為「部分型態(`Partial Type`)。      

        

[[C# 筆記] Partial 部分類別 by R](https://riivalin.github.io/posts/2011/01/partial-class/)      
[[C# 筆記] 常用關鍵字 by R](https://riivalin.github.io/posts/2011/02/keyword-1/)