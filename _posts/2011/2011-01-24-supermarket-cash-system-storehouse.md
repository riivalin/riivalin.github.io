---
layout: post
title: "[C# 筆記] draft 超商收銀系統-倉庫類"
date: 2011-01-23 21:19:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 超商收銀系統-倉庫類

```text
倉庫
1. 儲存貨物
2. 提貨
3. 進貨
```

```c#
/// <summary>
/// 倉庫-儲存貨物
/// </summary>
internal class Storehouse
{
    //我在一個集合(倉庫)裡面，再放一個集合(貨架)
    //這樣做跟剛的區別在：我是添加"集合"進去，不是添加"貨品"進去
    //是給這個倉庫添加"貨品的集合"進去。
    //倉庫就是貨架的集合，每個貨架都是商品的集合
    //現在有4種商品，我添加的就是4種貨架:(分門別類)
    //list[0]存放Acer筆電的貨架
    //list[1]存放SamSung手機的貨架
    //list[2]存放醬油的貨架
    //list[3]存放香蕉的貨架
    //實際拿到的是這個商品的貨架
    List<List<ProductFather>> list = new List<List<ProductFather>>();

    /// <summary>
    /// 向用戶展現貨物
    /// </summary>
    public void ShowProduct()
    {
        foreach (var item in list)
        {
            //item=貨架
            Console.WriteLine($"超市有：{item[0].Name},\t{item[0].Count}個，每個{item[0].Price}元\r\n");
        }
    }

    //list[0]存放Acer筆電的貨架
    //list[1]存放SamSung手機的貨架
    //list[2]存放醬油的貨架
    //list[3]存放香蕉的貨架

    /// <summary>
    /// 在建立倉庫物件的時候，向倉庫中加入貨架
    /// </summary>
    public Storehouse(int count = 4)
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(new List<ProductFather>());
        }
    }

    /// <summary>
    /// 進貨
    /// </summary>
    /// <param name="strType">貨物的類型</param>
    /// <param name="count">貨物的數量</param>
    public void JinProduct(string strType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            switch (strType)
            {
                case "Acer":
                    list[0].Add(new Acer(Guid.NewGuid().ToString(), "宏基筆電", 100000));
                    break;
                case "SamSung":
                    list[1].Add(new SamSung(Guid.NewGuid().ToString(), "三星手機", 5000));
                    break;
                case "Oil":
                    list[2].Add(new Oil(Guid.NewGuid().ToString(), "老抽醬油", 200));
                    break;
                case "Banana":
                    list[3].Add(new Banana(Guid.NewGuid().ToString(), "香蕉", 50));
                    break;
            }
        }
    }

    /// <summary>
    /// 從倉庫裡面提取貨物
    /// </summary>
    /// <param name="stryType"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public ProductFather[] QuProduct(string stryType, int count)
    {
        ProductFather[] prod = new ProductFather[count];
        for (int i = 0; i < count; i++)
        {
            switch (stryType)
            {
                case "Acer":
                    prod[i] = list[0][0];
                    list[0].RemoveAt(0);
                    break;
                case "Samsung":
                    prod[i] = list[1][0];
                    list[0].RemoveAt(0);
                    break;
                case "Oil":
                    prod[i] = list[2][0];
                    list[0].RemoveAt(0);
                    break;
                case "Banana":
                    prod[i] = list[3][0];
                    list[0].RemoveAt(0);
                    break;
            }
        }
        return prod;
    }


    //這樣寫也不好，添加進去只能是商品
    //沒有體現分門別類，這個倉庫就是亂七八糟的，
    //取值的時候會很麻煩，相當難取的
    //list[0]存放第一台Acer筆電的貨架
    //list[1]存放第一瓶醬油
    //list[2]存放第二台SamSung手機
    //List<ProductFather> list = new List<ProductFather>();

    //這樣寫，如果有上千萬個，就要寫上千萬次，不ok
    //List<SamSung> listSamSun = new List<SamSung>();
    //List<Acer> listAcer = new List<Acer>();
    //List<Oil> listOil = new List<Oil>();
    //List<Banana> listBanana = new List<Banana>();
}
```