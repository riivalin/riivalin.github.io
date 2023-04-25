---
layout: post
title: "[C# 筆記] ++、-- 運算符"
date: 2021-01-02 23:42:00 +0800
categories: [Notes, C#]
tags: [C#]
---

- 自增`++`：變數自身加一
- 自減`--`：變數自身減一

```c#
int a = 10;
int b = 10;

//前++
int c = ++a; //a=a+1, c=a
Console.WriteLine($"c = ++a: {c}"); //11
Console.WriteLine($"a={a}"); //11

//後++
c = b++; //c=b, b=b+1
Console.WriteLine($"c = ++a: {c}"); //10
Console.WriteLine($"b={b}"); //11
```


## `++` `--` 使用方法(重要!!!)
自增自減有兩種使用方式：
### 1. 單獨使用
`++`與`--`無論放在變數的前面還是後面，結果都一樣的。

### 2. 參與運算式操作
- 如果是放在變數的後面，則：先使用變數值參與運算，再+1或-1：
```c#
int a = 10;
int b = a++; //b=a=10, a=a+1=11
```

- 如果是放在變數的前面，則：先使用變數+1或-1，再參與運算：
```c#
int a = 10;
int b = ++a; //a=a+1=11, b=a=11
```

## 練習 b=(a++)+(++b)

先拆解：以中間的加號，拆分a++, ++a，兩邊得到的結果，兩者再加在一起。

從左到右計算原則：
1. (a++)是後++，先將變數a的值參與運算，`b=a=1`。
2. 再將a進行自加操作 `a=a+1=2`，此時a已經變2。
3. (++a)是前++，要先進行本身操作，`a=2+1=3`，然後再將a加1的結果，參與運算。
4. 再將兩者相加，`b=1+3=4`

```c#
int a = 1;
int b = (a++) + (++a);
Console.WriteLine(b); //b=4
Console.WriteLine(a); //a=3
```
在實際工作當中，`b=(a++)+(++b)`這種運算式儘量不要去用，因為這樣的程式碼很難去理解。

## 結論
- `a++` `b--` 表示 `a=a+1`, `b=b-1`。
- 在單獨操作的時候，`++`與`--`可以放在變數的前面或後面，結果是一樣的。
- 在參與運算式計算的時候：
    - ++在前：
        先進行變數的自增+1，然後再參與到運算式中。
    - ++在後：
        先進行變數參與運算式的操作，再對變數進行自增+1。


[operators/arithmetic-operators](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/arithmetic-operators)