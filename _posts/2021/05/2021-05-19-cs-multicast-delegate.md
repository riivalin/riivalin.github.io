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


## 範例

```c#
internal class Program
{
    //宣告一個委派
    public delegate void EarthquakeDelegate();
    static void Main(string[] args)
    {
        //實體化委派 並指向一個方法HouseholdA()
        EarthquakeDelegate earthquakeDelegate = HouseholdA;

        //利用 多重傳送委派 將HouseholdB和HouseholdC 加入
        earthquakeDelegate += HouseholdB;
        earthquakeDelegate += HouseholdC;

        //叫用委派
        earthquakeDelegate();
    }

    static void HouseholdA() {
        Console.WriteLine("Jack: 地震來了! Vic!");
    }

    static void HouseholdB() {
        Console.WriteLine("Vic: 天啊，地震真的來了! 搖好大啊~~~ Jack!");
    }

    static void HouseholdC() {
        Console.WriteLine("Tim: 別再聊天啊，快逃命!!!");
    }
}
```

執行結果：

```
Jack: 地震來了! Vic!
Vic: 天啊，地震真的來了! 搖好大啊~~~ Jack!
Tim: 別再聊天啊，快逃命!!!
```

[[C# 筆記] 多播委託 Multicast-Delegate  by R](https://riivalin.github.io/posts/2010/03/92-multicast-delegate/#總結)