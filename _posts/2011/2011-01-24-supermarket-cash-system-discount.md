---
layout: post
title: "[C# 筆記] draft 超商收銀系統-折扣類"
date: 2011-01-24 22:19:00 +0800
categories: [Notes, C#]
tags: [C#]
---

父類
- CalFather 打折的父類 (抽象類)
 -  public abstract decimal GetTotalMoney(decimal realMoney);
    
子類
- CalNormal : CalFather 不打折的子類
- CalRate : CalFather 按折扣率打折的子類
 - public decimal Rate { get; set; } //折扣率
 - public CalRate(decimal rate){} //建立物件時，把折扣率傳進去，構造函數會幫我們賦值給屬性
- CalMN : CalFather  買M元 送N元
 - public decimal M { get; set; } //買多少
 - public decimal N { get; set; } //送多少
 - public CalMN(decimal m, decimal n){} //建立物件時，把買多少，送多少傳進去，構造函數會幫我們把值給屬性


```c#
/// <summary>
/// 打折的父類
/// </summary>
abstract class CalFather
{
    /// <summary>
    /// 計算打折後應付多少錢
    /// </summary>
    /// <param name="realMoney">打折前應付的價錢</param>
    /// <returns>打折後應付的價錢</returns>
    public abstract decimal GetTotalMoney(decimal realMoney);
}

/// <summary>
/// 不打折 該多少錢就多少錢
/// </summary>
internal class CalNormal : CalFather
{
    public override decimal GetTotalMoney(decimal realMoney)
    {
        return realMoney;
    }
}

/// <summary>
/// 按折扣率打折
/// </summary>
internal class CalRate : CalFather
{
    /// <summary>
    /// 折扣率
    /// </summary>
    public decimal Rate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rate">折扣率</param>
    public CalRate(decimal rate)
    {
        this.Rate = rate;
    }

    public override decimal GetTotalMoney(decimal realMoney)
    {
        return realMoney * this.Rate;
    }
}

/// <summary>
/// 買M元 送N元
/// </summary>
internal class CalMN : CalFather
{
    //買500送100
    /// <summary>
    /// 買多少
    /// </summary>
    public decimal M { get; set; }
    /// <summary>
    /// 送多少
    /// </summary>
    public decimal N { get; set; }

    /// <summary>
    /// 構造函式
    /// </summary>
    /// <param name="m">買多少</param>
    /// <param name="n">送多少</param>
    public CalMN(decimal m, decimal n)
    {
        this.M = m;
        this.N = n;
    }
    /// <summary>
    /// 計算買多少送多少的折扣
    /// </summary>
    /// <param name="realMoney"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override decimal GetTotalMoney(decimal realMoney)
    {
        //600-100
        //1000-200
        //1200-200

        if (realMoney >= this.M)
        {
            //如果買1000，有2個500，就要減200
            return realMoney - (int)(realMoney / this.M) * this.N;
        } else
        {
            return realMoney;
        }
    }
}
```