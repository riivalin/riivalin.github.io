---
layout: post
title: "[C# 筆記] System.Exception 類別"
date: 2021-04-09 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,例外處理(Exception Handling),System.Exception]
---


`System.Exception` 類別是所有例外的共同祖先（基礎類別 `Base Class`），其常用的屬性有：

- StackTrace：是一個字串，裡面包含了發生例外當下的函式呼叫堆疊（call stack）中的所有方法名稱。
- Message：描述錯誤訊息的字串。       
- InnerException：內部例外。如果不是 null 的話，則是引發當前例外的上一個例外。此屬性的型別也是 Exception，故每個內部例外都可能還有另一個內部例外。      


這裡無法完整呈現 System.Exception 的所有子類別與繼承關係，但是有個概略的認識還是有幫助的，如下圖（命名空間 System 已省略）：


[![](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEgu-XkZ55-SqjChbuYW3wzYaYJduE-yOHrrzk_g8bkVwajs-zRaRL-EptZrTCQ9ZtwXSwn1m0PEBxz-nLA5InY8EPW7yMKTO712oRyvCvlRmSJTeckYi9Rf-xibrxI9ewKv8mWih39KQNEaK2b-qHKbl25XT1vStw-tVqTTSwnfwM3OaI1aarLK_u54/w640-h542/exception-classdiagram.png)](https://blogger.googleusercontent.com/img/b/R29vZ2xl/AVvXsEgu-XkZ55-SqjChbuYW3wzYaYJduE-yOHrrzk_g8bkVwajs-zRaRL-EptZrTCQ9ZtwXSwn1m0PEBxz-nLA5InY8EPW7yMKTO712oRyvCvlRmSJTeckYi9Rf-xibrxI9ewKv8mWih39KQNEaK2b-qHKbl25XT1vStw-tVqTTSwnfwM3OaI1aarLK_u54/w640-h542/exception-classdiagram.png)



由上而下依序來看，`Exception` 繼承自所有 .NET 類別的共同祖先，也就是 `Object`，並且有兩個子類別，分別代表例外類型的兩個大分類：`SystemException` 和 `ApplicationException`。        

SystemException 繼承自 Exception，代表「系統層級」的例外，像是 ArgumentException、NullReferenceException、IOException 等等。這些由 .NET 平台所拋出的例外，都是所謂的「系統例外」，它們通常代表無法回復的嚴重錯誤。      

ApplicationException 也是繼承自 Exception，但是它沒有增加任何功能，只是單純用來「分類」，作為應用程式自訂例外的基礎類別。換言之，應用程式如果需要定義自己的例外型別，便可以優先考慮繼承自 ApplicationException。不過，由於 Exception 即代表所有執行時期發生的錯誤，所以實務上在設計自訂例外類別的時候，許多人也會直接繼承自 Exception。


[C# 例外處理（Exception Handling） by huanlintalk](https://www.huanlintalk.com/2022/09/csharp-exception-handling.html)     