---
layout: post
title: "[C# 筆記] 結構(struct)資料型別"
date: 2021-02-20 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,結構,struct]
---

## 資料型別 - enum、struct、Nullable
- 列舉(`enum`)與結構(`struct`)可以提高程式可讀性。
- `Nullable`類別的宣告讓實值變數可以存放`null`值。

## 什麼是結構(struct)資料型別?

- `struct`是一種用來宣告結構的關鍵字，在結構中包含相關變數。

## 宣告方式

```c#
[存取修飾詞] struct [結構名稱]
{
    [存取修飾詞][資料型別][變數名稱]; 
    [存取修飾詞][資料型別][變數名稱];
    ...
}
```

```c#
public struct Employee
{
    public int ID;
    public string Name;
}
```

## 叫用方式

```c#
[結構名稱][變數名稱];
```

```c#
Employee emp;
```

## 範例

透過`struct`來宣告一個員工結構，其結構成員包含：員工編號(ID)、中文姓名(CName)、年齡(Age)等，當使用者輸入員工資料時，會存放於對應的結構變數中。

```c#
//宣告Employee結構
public struct Employee
{
    public string ID;
    public string CName;
    public int Age;
}

internal class Program
{
    static void Main(string[] args)
    {
        Employee employee; //宣告結構變數，以供叫用
        employee.ID = "E001";
        employee.CName = "Rii";
        employee.Age = 99;
        Console.WriteLine($"員工ID: {employee.ID}\r\n姓名: {employee.CName}\r\n年齡: {employee.Age}");
    }
}
```

## C#中的結構體(struct)要使用new來實體化嗎?

`struct`屬於值類型，可以不用`new`，如果不`new`，結構體內的值就都是未賦值狀態，需要在使用之前賦值，不然編譯器會報錯。若`new`了，結構體會呼叫無參構造函數，會初始化內部的值，例如`int`就會初始化為`0`，現在使用編譯器就不會報錯了。       


### 結構無需進行 new，就可以直接使用 (可new，也可不new)
比如:

```c#
MyStruct myStruct;
myStruct.Method();
```
對於類別(`class`)的話，這是錯誤的。

原因如下：      
結構(`stuct`)為值類型，而`new`用於為引用類型（類別、物件、介面等）分配參考（記憶體位址），值類型儲存於`Stack`(堆疊)中，無需使用`new`。      

比如:       
`int x;`和`MyStruct myStruct;`      
是一樣的道理        
這的`x`、`myStruct`都是值型的       


最後結構(`stuct`)可以用`new` 也可以不用`new`

        
[C#中的结构体要使用new来实例化吗?](https://www.cnblogs.com/fps2tao/p/14692302.html)     
[C#中的结构体要使用new来实例化吗？还是直接声明后直接使用？](https://zhidao.baidu.com/question/62698662.html)        
Book: Visual C# 2005 建構資訊系統實戰經典教本 

