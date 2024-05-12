---
layout: post
title: "[C# 筆記] 泛型 (Generics)"
date: 2021-02-01 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,泛型,generic,T]
---

以功能來說，泛型 (Generics) 的出現是為了 **「解決程式在執行時期(`Run time`)中，型別間互相轉換換所耗費的成本問題」**。       

如果兩個變數(也可以是物件)型別的不同在進行轉換時將會耗費太多時間，雖然說可以把所有的變數宣告成`Object`來解決型別轉換所耗費的時候，但是全部宣告成`Object`型別也會佔掉一大部分的記憶體空間，於是在時間與空間的競爭下，泛型 (Generics)便成為兩全其美的解決方案。       

泛型 (Generics)的作法是在程式 **編譯期間(`Compiler time`)** 先不決定變數的型別，而是在 **執行時期(`Run time`)**才決定變數的型別，只要在撰寫程式碼時把使用泛型 (Generics)的方法設計好即可。      

## 泛型的優點

- 避免執行時期型態檢查所需進行的`Boxing`/`Unboxiing`轉換，進而提升程式執行效率。
- 能指定類別或方法作用於特定資料型別。
- 較佳的型別安全管理。
- 最佳化程式碼的可再利用(Reuse)。
- 減少型別轉換與降低`Runtime`的錯誤機率。


## 泛型的限制

- 不支援內容繫結泛型型別。
- 不可以是輕量動態的方法。
- 泛型型別中的巢狀型別無法具現化(Instantiated)。

## 泛型類別

```c#
//宣告一個名稱為GenericClass的類別
public class GenericClass<T> 
{
    public T var1; //宣告一個名稱為var1、型別為T的成員
}
```
- `T`就是我們尚未定義的資料型別，等到最後要實體化`GenericClass`此類別時，再進行`T`資料型別的設定。


> 當 `GenericClass<T>` 具現化為具象類型時，例如 `GenericClass<int>`，每個出現的 都會 `T` 取代為 `int`。


例如現在我們想在實體化`GenericClass`類別時，給定整數資料型別(`Integer type`)，可以這樣寫：

```c#
//實體化GenericClass的物件，名稱為obj、資料型別為int
GenericClass<int> obj = new GenericClass<int>();  
```


## 泛型方法

除了類別(`Class`)可使用泛型外，方法(`Method`)也可以使用泛型。       

```c#
//宣告一個名稱為GenericMethod的方法
public void GenericMethod<T>()
{
    T var1; //宣告一個名稱為var1、資料型別為T的變數
}
```

現在想在使用`GenericMethod`方法時，給定布林資料型別(Boolen data type)，可以這樣寫：

```c#
//使用GenericMethod方法，資料型別指定Boolen
GenericMethod<Bool>();
```
            

[MSDN -  泛型型別和方法](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/types/generics)
[[C# 筆記] 泛型 by R](https://riivalin.github.io/posts/2010/03/89-generic/)     
Book: Visual C# 2005 建構資訊系統實戰經典教本    