---
layout: post
title: "[C# 筆記] C#中的訪問修飾符"
date: 2011-01-22 22:09:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## C# 中的訪問修飾符

- public 公開公共的
- private 私有的，只能在當前的類別內部訪問
- protected 受保護的，子類也可以訪問。只能在當前類別的內部、子類中訪問。
- internal 只能在當前這個專案內部中訪問，在同一個專案中，internal, public的權限是一樣的。
- protected internal：protected+internal權限，存取只限於這個專案或是子類別


### 1.能夠修飾類別的修飾符只有兩個：public、internal，默認的情況下是internal

類別的修飾符：public、internal
```c#
public class Person {  }
internal class Person {  } //沒有手動加，默認是internal
```
內部成員的修飾符：public, private, protected, internal, protected internal.
```c#
//沒有手動加，默認是 internal
public class Student
{
    //這些修飾符都可以修飾類別內部的成員
    public string name;
    private int count;
    protected int age;
    internal char gender;
    protected internal int math;
}
```

### 2.訪問性不一致
什麼叫訪問性不一致？    
子類別的權限不能高於父類別的權限，會暴露父類的成員。   

為什麼？    
因為既然寫成 internal, 就代表我只想要在這個專案能被訪問到，不希望被其他專案訪問。
但是在其他的專案，能訪問到 Public，由繼承傳遞得知，我可以訪問到internal的成員，
所以子類別的權限大於父類別的權限，會暴露父類的成員。


錯誤的寫法：
不一致的存取範圍: 基底類別 'Person' 比類別 'Student' 的存取範圍小
```c#
internal class Person {
}
//報錯：不一致的存取範圍: 基底類別 'Person' 比類別 'Student' 的存取範圍小
public class Student: Person { 
}
```

## Public、internal 的區別在哪？
- internal只能在當前的專案中訪問
- public 別的專案也可以訪問，需要加入這個專案的引用(加入參考)及命名空間using它

```c#
//專案A
internal class Person {} // 受保護的
public class Student{} //公開的

//專案B
using 專案A;
Student s = new Student(); //可以訪問
Person p = new Person(); //不可訪問，因為它受保護級別的限制，只能在當前專案A訪問
```
在同一個專案中，internal, public 的權限是一樣的。


## protected & internal
internal、protected 誰的權限大？    
當然是internal權限大，因為它在當前專案中哪都可以訪問。
protected 只能在當前的類別的內部，和繼承它的子類中訪問。






[keywords/protected-internal](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/protected-internal)