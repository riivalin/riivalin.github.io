---
layout: post
title: "[C# 筆記] 匿名方法使用委派(Delegate)"
date: 2021-05-18 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,匿名方法,Lambda,委派,Delegate]
---


「匿名方法(Anonymous Method)」主要功能是：當執行委派(`delegate`)所指定的方法，其名稱不太重要的方法時，可以直接省略該方法的名稱。        


## 不使用匿名方法

```c#
//宣告一個委派，可以指向一個方法 (一個參數無回傳值)
delegate void Say(string name);
internal class Program
{
    static void Main(string[] args)
    {
        //實體化委派，並指向SayHi方法
        Say say = SayHi; 

        //執行委派
        say("Rii"); //輸出：Rii say hi.
    }

    //給委派用的方法
    static void SayHi(string name) {
        Console.WriteLine( $"{name} say hi.");
    }
}
```


## 使用匿名方法

```c#
delegate void Say(string name);
internal class Program
{
    static void Main(string[] args)
    {
        //實體化委派，並使用匿名方法(沒有名稱的方法)
        //實體化的同時 定義SayHi()方法相同的內容
        Say say = delegate (string name)
        {
            //直接定義SayHi()方法相同的內容
            //所以等於省略SayHi()方法
            Console.WriteLine($"{name} say hi.");
        };

        //執行委派
        say("Rii"); //輸出：Rii say hi.
    }
}
```

## 匿名函數 & Lamda表達式

- Lamda表達式： `() => { };`    
- 匿名函數：`delegate () { };`  

```c#
//宣告一個委派，可以指向一個方法(一個參數，沒有返回值)
public delegate void MyDelegate(string name);

//匿名函數 寫法
MyDelegate test1 = delegate (string name) { }; 

//lamda 寫法(把delegate拿掉)
MyDelegate test2 = (string name) => { };
```


## Lamda表達式 

`=>` 代表什麼：`goes to`

- `Lamda`表達式，本質上還是匿名函數

把匿名函數的 `delegate`拿掉

```c#
delegate void Say(string name);
internal class Program
{
    static void Main(string[] args)
    {
        Say say = (string name) => {
            Console.WriteLine($"{name} say hi.");
        };
        say("Rii");
    }
}
```

### 箭頭符號 =>

- 使用「箭頭`=>`」來簡化程式碼
- 主體使用「箭頭`=>`」來代替大括號。

```c#
//主體使用「箭頭=>」來代替大括號
int Add(int x, int y) => x + y;  //同等於: int Add(int x, int y) { x + y; }
```


[[C# 筆記] Lambda 表達式  by R](https://riivalin.github.io/posts/2011/01/lamda/)        
[[C# 筆記] 方法 Lambda表達式(=>)  by R](https://riivalin.github.io/posts/2010/01/r-csharp-note-2/#lambda表達式)     
[[C# 筆記] Delegate(委託)、Lambda、Event(事件)  by R](https://riivalin.github.io/posts/2012/01/delegate-lambda-event/#lambda表達式)