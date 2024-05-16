---
layout: post
title: "[C# 筆記] 多重傳送委派(Multicast Delegate)"
date: 2021-05-19 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,多重傳送委派,多播委託,委派,Delegate,Multicast-Delegate]
---

「多重傳送委派(`Multicast Delegate`)」是**單一事件引發多個事件**。      
(多重傳送委派 可以引發 多重事件)

也就是說，一個主委派物件 可以容納多個 其他的子委派物件，當調用主委派物件，會將所有的子委派全部按序運行。        

- 通過`+`增加「子委派」，通過`-`刪除某個「子委派」
- 多播調用的返回值 是最後一個執行委派的返回值

> 「多重傳送委派(Multicast-Delegate)」說穿了，你把實體化好的委派看成是一個隊列或集合，`+=`看成是執行Add方法，`-=`看成是執行Remove方法，但如果你直接用 `=` 號那就相當於把這個方法 直接賦值給了委派物件，會造成前面的加入進委派物件的方法被覆寫。
>       
> 但是，委派有一個弊端，它可以使用「`=`」將所有已經訂閱的取消，只保留`=`後的這一個訂閱。

## 建立多播委派

### 方法一

```c#
//方法一：
//實體化委派 並指向一個方法 Method1()
MyDelegate myDelegate = Method1; 

//利用 多重傳送委派 將 Method2 和Method3 加入
myDelegate += Method2;
myDelegate += Method3;
```

### 方法二

```c#
//方法二：
//實體化委派 初始化為null
MyDelegate? myDelegate = null; 

//利用 多重傳送委派 將Method1(),Method2 和Method3 加入
myDelegate += Method1;
myDelegate += Method2;
myDelegate += Method3;
```
        
## 範例

使用「多重傳送委派(`Multicast Delegate`)」可以引發多重事件的特性，寫一個地震警報系統，當社區有地震時，系統會先告知此訊息，然後再觸發其他社區的住戶。

```c#
internal class Program
{
    //宣告一個委派
    public delegate void EarthquakeDelegate();
    static void Main(string[] args)
    {
        //顯示警告訊息
        Console.WriteLine("地震系統：地震發生請社區住戶小心。");

        //實體化委派 初始化為 null
        EarthquakeDelegate? earthquakeDelegate = null;

        //利用 多重傳送委派 將HouseholdB和HouseholdC 加入
        earthquakeDelegate += HouseholdA; //驚動住戶A
        earthquakeDelegate += HouseholdB; //驚動住戶B
        earthquakeDelegate += HouseholdC; //驚動住戶C

        //叫用委派
        earthquakeDelegate();

        //也可以這樣寫:
        //實體化委派 並指向一個方法HouseholdA()
        //EarthquakeDelegate earthquakeDelegate = HouseholdA; 

        //利用 多重傳送委派 將HouseholdB和HouseholdC 加入
        //earthquakeDelegate += HouseholdB;
        //earthquakeDelegate += HouseholdC;
    }

    static void HouseholdA() { //住戶A
        Console.WriteLine("Jack: 地震來了! Vic!");
    }

    static void HouseholdB() { //住戶B
        Console.WriteLine("Vic: 天啊，地震真的來了! 搖好大啊~~~ Jack!");
    }

    static void HouseholdC() { //住戶C
        Console.WriteLine("Tim: 別再聊天啊，快逃命!!!");
    }
}
```

執行結果：

```
地震系統：地震發生請社區住戶小心。
Jack: 地震來了! Vic!
Vic: 天啊，地震真的來了! 搖好大啊~~~ Jack!
Tim: 別再聊天啊，快逃命!!!
```

[[C# 筆記] 委派(Delegate) & 事件(Event)   by R](https://riivalin.github.io/posts/2021/05/cs-event/)        
[[C# 筆記] 委派(Delegate)   by R](https://riivalin.github.io/posts/2021/05/cs-delegate/)       
[[C# 筆記] 多播委託 Multicast-Delegate  by R](https://riivalin.github.io/posts/2010/03/92-multicast-delegate/)     
[[C# 筆記] 匿名方法使用委派(Delegate)](https://riivalin.github.io/posts/2021/05/cs-anonymous-method-use-delegates/)     
