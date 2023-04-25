---
layout: post
title: "[C# 筆記] draft 超商收銀系統-超市類"
date: 2011-01-23 21:49:00 +0800
categories: [Notes, C#]
tags: [C#]
---

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
}
```