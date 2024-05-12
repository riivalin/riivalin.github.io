---
layout: post
title: "[C# 筆記] Nullable 類別"
date: 2021-02-21 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,資料型別,nullable,"null"]
---

## 資料型別 - enum、struct、Nullable
- 列舉(`enum`)與結構(`struct`)可以提高程式可讀性。
- `Nullable`類別的宣告讓實值變數可以存放`null`值。


## Nullable 類別

- `Nullable` 類別其功能是用來支援「實值型別」存放`null`值。
- 所以`Nullable` 類別的作用就是「讓實值型別變數可以存放虛值」。
- `Nullable`結構只支援使用「實值型別」來做為可`null`的型別。
- 因為「參考型別」原本就支援存放`null`值，並不需要特別去宣告為`Nullable`。

只要在型別後面加上「問號(`?`)」就可以存放`null`值。

```c#
short? score1;
int? score2;
```

## 範例

以下程式，`Score`未宣告成 `Nullable`，所以無法存放`null`值。

```c#
public struct Student
{
    public string ID;
    public string Name;
    public int Score;
}
internal class Program
{
    static void Main(string[] args)
    {
        Student[] stu = new Student[2];
        stu[0].ID = "S001";
        stu[0].Name = "Rii";
        stu[0].Score = null; //報錯，因為Score未宣告成Nullable，所以不能存放null值
    }
}
```

只要在型別後面加上一個「問號`?`」就變成可存放`null`的資料型別。

```c#
public struct Student
{
    public string ID;
    public string Name;
    public int? Score;
}
internal class Program
{
    static void Main(string[] args)
    {
        Student[] stu = new Student[2];
        stu[0].ID = "S001";
        stu[0].Name = "Rii";
        stu[0].Score = null;

        stu[1].ID = "S002";
        stu[1].Name = "Riva..";
        stu[1].Score = 99;
    }
}
```

## 為什麼變數需要存放 null 值？

因為在實務上我們常用`null`來表示「未知的值」。      
例如：學生也許因為有事缺考，所以成績分數需要用`null`來代表，而暫時不存入任何成績分數。


[[C# 筆記] ?: 運算子  by R](https://riivalin.github.io/posts/2021/03/csharp-expression/)            
Book: Visual C# 2005 建構資訊系統實戰經典教本 