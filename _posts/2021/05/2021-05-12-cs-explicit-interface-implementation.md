---
layout: post
title: "[C# 筆記] 實作介面 vs 明確實作介面"
date: 2021-05-12 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,介面,interface]
---


實現介面的函數有兩種方式：      

1. 實作介面
2. 明確實作所有成員：解決方法重名的問題

[MSDN say:](https://learn.microsoft.com/zh-tw/previous-versions/dotnet/netframework-4.0/ms229034(v=vs.100)?redirectedfrom=MSDN)
- 如果沒有強力的理由，要避免明確實作介面成員。
- 如果成員只要透過介面來呼叫，請考慮明確實作介面成員。


## 實作介面

如果類別實作兩個具有相同名稱成員的介面，則在類別上實作該成員 會造成這兩個介面都使用該成員進行實作。     
所有對 Paint() 的呼叫都會叫用相同的方法。

```c#
//介面：IControl和ISurface 具有相同名稱的 Paint()方法
public interface IControl
{
    void Paint();
}
public interface ISurface
{
    void Paint();
}

//使用 實作介面
public class SampleClass : IControl, ISurface
{
    // Both ISurface.Paint and IControl.Paint call this method.
    // ISurface.Paint 和 IControl.Paint 都會呼叫此方法。
    public void Paint()
    {
        Console.WriteLine("Paint method in SampleClass");
    }
}
```

呼叫方法 & 執行結果：       

所有對 Paint() 的呼叫都會叫用相同的方法。
(全部都會呼叫同一個方法。)

```c#
SampleClass sample = new SampleClass();
IControl control = sample;
ISurface surface = sample;

// The following lines all call the same method.
sample.Paint();
control.Paint();
surface.Paint();

// Output:
// Paint method in SampleClass
// Paint method in SampleClass
// Paint method in SampleClass
```

## 明確實作介面

但您可能不想要針對這兩個介面 呼叫相同的實作。       
若要根據使用的介面呼叫不同的實作，您可以明確地實作介面成員。


```c#
//介面：IControl和ISurface 具有相同名稱的 Paint()方法
interface IControl {
    void Paint();
}
interface ISurface {
    void Paint();
}

//使用 明確實作介面
public class SampleClass : IControl, ISurface
{
    void IControl.Paint()
    {
        System.Console.WriteLine("IControl.Paint");
    }
    void ISurface.Paint()
    {
        System.Console.WriteLine("ISurface.Paint");
    }
}
```

類別成員    
`IControl.Paint` 只能透過 `IControl` 介面取得，        
`ISurface.Paint` 只能透過 `ISurface` 取得。         
這兩種方法實作是分開的，都無法直接在類別上使用。        

```c#
SampleClass sample = new SampleClass();
IControl control = sample;
ISurface surface = sample;

// The following lines all call the same method.
//sample.Paint(); // Compiler error.
control.Paint();  // Calls IControl.Paint on SampleClass.
surface.Paint();  // Calls ISurface.Paint on SampleClass.

// Output:
// IControl.Paint
// ISurface.Paint
```

> **明確實作介面**沒有 存取修飾詞，因為它無法當做其定義類型的成員來存取。       
> 相反地，它只有在透過介面的執行個體呼叫時才能存取。


### 明確介面宣告上不允許 public 關鍵字

- 明確介面宣告上不允許 `public` 關鍵字。 在此情況下[編譯器錯誤 CS0106](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/compiler-messages/cs0106)，請從明確介面宣告移除 public 關鍵字。

```c#
//汽車介面
interface ICar { 
    string Name { get; set; } //設定車款名稱的屬性
}

//跑車類別
class SportsCar : ICar //繼承汽車介面
{
    //錯誤寫法：
    //錯誤CS0106:明確實作介面 不允許 有 public
    public string ICar.Name { get; set; } = "Audi R8"; //錯誤CS0106

    //正確寫法：把 public 移除就可以了
    string ICar.Name { get; set; } = "Audi R8";
}
```





[MSDN - 明確介面實作 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation)     
[MSDN - 編譯器錯誤 CS0106](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/compiler-messages/cs0106)     
[[C# 筆記] 里氏轉換(LSP)  by R](https://riivalin.github.io/posts/2011/01/lsp/)        
[[C# 筆記] 介面與實作  by R](https://riivalin.github.io/posts/2021/05/interface/)       
[[C# 筆記][多型] Interface 介面複習  by R](https://riivalin.github.io/posts/2011/02/interface-review/#interface-介面)       
[實現多態(多型)的三個方法 ([C# 筆記] .Net基礎-複習-R)  by R](https://riivalin.github.io/posts/2011/02/r-cshap-notes-3/#6多態多型) 