---
layout: post
title: "[C# 筆記] class property method 練習"
date: 2011-01-14 23:19:00 +0800
categories: [Notes, C#]
tags: [C#]
---

寫一個ticket類，有一個距離屬性(該屬性唯讀，在構造方法中賦值)，不能為負數，有一個價格屬性，價格屬性唯讀，並且根據距離distance 計算價格Price (1元/公里)： 
0-100公里 票價不打折    
101-200公里 總額打9.5折 
201-100公里 總額打9折   
300 公里以上 總額打8折  

```c#
Ticket ticket = new Ticket(150);
ticket.ShowTicket();
Console.ReadKey();

public class Ticket
{
    //構造函式
    public Ticket(decimal distance)
    {
        //距離不能為負數
        if (distance < 0) _distance = 0;
        this._distance = distance;
    }

    //距離屬性(該屬性唯讀，在構造方法中賦值)
    private decimal _distance;
    public decimal Distance
    {
        get { return _distance; }
    }

    //價格屬性唯讀，並且根據距離distance 計算價格Price (1元/公里)
    private decimal _price;
    public decimal Price
    {
        get
        {
            //0 - 100公里 票價不打折
            //101-200公里 總額打9.5折
            //201-300公里 總額打9折
            //300 公里以上 總額打8折

            if (_distance > 0 && _distance <= 100)
            {
                return _distance * 1.0m;
            } else if (_distance > 100 && _distance <= 200)
            {
                return _distance * 0.95m;
            } else if (_distance > 200 && _distance <= 300)
            {
                return _distance * 0.9m;
            } else
            {
                return _distance * 0.8m;
            }
        }
    }

    public void ShowTicket()
    {
        //Distance, Price為屬性，這樣能進入get做處理
        Console.WriteLine($"{Distance}公里需要{Price}元");
    }
}
```