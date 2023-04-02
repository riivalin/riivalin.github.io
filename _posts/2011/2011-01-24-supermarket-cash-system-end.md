---
layout: post
title: "[C# 筆記] draft 超商收銀系統-結束"
date: 2011-01-24 22:49:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## program.cs
```c#
//超市收銀系統
SupperMarket sm = new SupperMarket();//創建超市對象
sm.ShowProduct();//展現貨物
sm.AskBuying();//跟用戶交互
Console.ReadKey();
```

## SupperMarket.cs
```c#
/// <summary>
/// 超市
/// </summary>
internal class SupperMarket
{
    //建立倉庫物件(會直接建立4個貨架)
    Storehouse storehouse = new Storehouse();

    /// <summary>
    /// 建立超市物件的時候，給倉庫的貨架上導入貨物
    /// </summary>
    public SupperMarket()
    {
        storehouse.JinProduct("Acer", 1000);
        storehouse.JinProduct("SamSung", 1000);
        storehouse.JinProduct("Oil", 1000);
        storehouse.JinProduct("Banana", 1000);
    }
    /// <summary>
    /// 跟用戶交互的過程
    /// </summary>
    public void AskBuying()
    {
        Console.WriteLine("歡迎觀臨，請問您需要些什麼？");
        Console.WriteLine("我們有Acer,SamSun,Oil,Banana");
        string strType = Console.ReadLine()!;
        Console.WriteLine("您需要多少？");
        int count = Convert.ToInt32(Console.ReadLine());

        //去倉庫取貨物
        ProductFather[] prod = storehouse.QuProduct(strType, count);

        //計算價錢
        decimal realMoney = GetMoney(prod);
        Console.WriteLine($"您總共應付{realMoney}元");
        Console.WriteLine("請選擇您的打折方式 1--不打折 2--打九折 3--打85折 4--買300送50 5--買500送100");
        string input = Console.ReadLine();

        //通過簡單工廠的設計模式 根據用戶的輸入獲得一個打折對象
        CalFather cal = GetCal(input);
        decimal totalMoney = cal.GetTotalMoney(realMoney);
        Console.WriteLine($"打完折後，你應付{totalMoney}元");

        Console.WriteLine("以下是你的購物明細");
        foreach (var item in prod)
        {
            Console.WriteLine($"貨物名稱：{item.Name}, 單價{item.Price}, 貨品編號：{item.ID}");
        }
    }
    
    /// <summary>
    /// 根據用戶選擇的打折方式返回一個打折對象
    /// </summary>
    /// <param name="input">用戶的選擇</param>
    /// <returns>返回的是父類對象 但是裡面裝的是子類對象</returns>
    public CalFather GetCal(string input)
    {
        CalFather cal = null;
        switch (input)
        {
            case "1":
                cal = new CalNormal();
                break;
            case "2":
                cal = new CalRate(0.9m);
                break;
            case "3":
                cal = new CalRate(0.85m);
                break;
            case "4":
                cal = new CalMN(300m, 50m);
                break;
            case "5":
                cal = new CalMN(500m, 100m);
                break;
        }
        return cal;
    }

    /// <summary>
    /// 根據用戶買的貨物計算總價錢
    /// </summary>
    /// <param name="prod">用戶買的貨物</param>
    /// <returns>總價錢</returns>
    public decimal GetMoney(ProductFather[] prod)
    {
        decimal realMoney = 0;
        for (int i = 0; i < prod.Length; i++)
        {
            realMoney += prod[i].Price; //累加金額
        }
        return realMoney;
    }

    public void ShowProduct()
    {
        storehouse.ShowProduct();
    }
}

```