---
layout: post
title: "[C# 筆記] Override 重寫父類的ToString()"
date: 2011-01-22 23:09:00 +0800
categories: [Notes, C#]
tags: [C#,override,virtual]
---
## 重寫父類的ToString()

物件、陣列、集合直接ToString()，輸出的是命名空間


為什麼誰都可以ToString()？
因為ToString()是Object
Object 是父類
父類的方法通過繼承，子類都可以使用

既然可以直接調用這方法，又可以重寫這方法，代表他是虛方法virtual    

為什麼不是抽象方法(abstract)？因為抽象方法一定要重寫方法，不能直接調用  

## 範例：物件、陣列、集合直接ToString()，輸出的是命名空間

```c#
Person p = new Person();
Console.WriteLine(p.ToString()); //輸出:Person
Console.ReadKey();

public class Person {
    public string Name { get; set; }
}
```
輸出這個類的所在空間Person

## 範例：重寫父類的ToString()
```c#
Person p = new Person();
Console.WriteLine(p.ToString()); //Hello, World
Console.ReadKey();

public class Person {
    public override string ToString() {
        return "Hello, World";
    }
}
```
輸出：Hello, World