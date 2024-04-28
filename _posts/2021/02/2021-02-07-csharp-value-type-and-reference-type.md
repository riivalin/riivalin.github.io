---
layout: post
title: "[C# 筆記] 實值型別與參考型別(Value Type, Reference Type)"
date: 2021-02-07 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,Value Type,Reference Type]
---

C# 的型別系統(Type System)主要包含下列二種類別：
- 實值型別(`Value Type`)
- 參考型別(`Reference Type`)

# 實值型別 vs 參考型別

- 「實值型別」在複製的時候，傳遞的是這個「值的本身」。
- 「參考型別」在複製的時候，傳遞的是這個「物件的參考(記憶體的位置)」。

## 實值型別(Value Type)

- 宣告「實值型別」的變數會儲存資料。指派一個實值型別變數給其他實值型別變數，會複製所包含的值。
- 所有實值型別都是自`System.ValueType` 隱含衍生而來的。

> 「實值型別」在複製的時候，傳遞的是這個「值的本身」。

## 參考型別(Reference Type)

- 宣告「參考型別」的變數，是儲存實際資料的參考(儲存的是記憶體的位置)。
- 「參考型別」變數的指派會複製物件的參考，不會複製物件本身。
- 我們可以將「參考型別」視為`Object`。

> 「參考型別」在複製的時候，傳遞的是這個「物件的參考(記憶體的位置)」。

# 概念
## 實值型別(Value Type)的概念

建立兩個實值型別的變數 x, y，彼此之間互相獨立並無任何關連，也不會互相影響：

```c#
int x = 2;
int y = x; //把x的值本身複製給y
y = 10; //y再直接重新賦值10，沒有影響到x

Console.WriteLine(x);//2
Console.WriteLine(y);//10
```

它傳遞值的方式是直接把x的值本身複製給y，y再直接重新賦值10，沒有影響到x。


## 參考型別(Reference Type)的概念

傳遞的是「物件的參考(記憶體的位置)」。      

```c#
// p1, p2 都指向同一個記憶體位置、同一個空間、同一個物件
// 所以不管操作p1、p2，他們兩個都會改變
Person p1 = new Person();
p1.Name = "R";

Person p2 = p1; //將p1指定給p2，此時都存放相同記憶體參考位址(指向同一個物件)
p2.Name = "Rii";

Console.WriteLine(p1.Name); //Rii, p1的值也隨之改變
Console.WriteLine(p2.Name); //Rii


class Person {
    public string Name {get; set;}
}
```

p1、p2 都存放相同記憶體參考位址，只要變更Person任何屬性，p1、p2兩者都會跟著變更，這是因為它們都指向記憶體中的同一個物件。


# 比較表

|          | 實值型別                 | 參考型別 |
|:---------|:-------------------------|--------:|
| 英文名稱  | Value Type                | Reference Type |
| 儲存      | 資料本身                  | 變數記憶體位址的參考 |
| 衍生新型別 | 不可以                   | 可以   |
| 包含null值 |需透過 Nullable 宣告才可以  | 不用透過 Nullable 即可為 Null |
| 例如      | Int32, Double            | Object, Class   |


[值傳遞 & 引用傳遞 by R](https://riivalin.github.io/posts/2011/01/valuetype-referencetype/)