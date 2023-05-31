---
layout: post
title: "[C# 筆記] Interface 接口(介面)實現"
date: 2010-01-20 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,interface,多型]
---

[[C# 筆記] Interface 什麼是接口(介面)?](https://riivalin.github.io/posts/2010/01/r-csharp-note-18/)     

```
OrderPressor --> IPriceCalculator
訂單處理系統  依賴於  價格計算接口(介面)
```
```
IPriceCalculator --> Price_1111 (雙11價格計算系統)
價格計算接口(介面)  --> Price_618  (618價格計算系統)
                 --> PriceNormal (正常價格計算系統)
```

## 範例
比如說，現在「雙11」大促銷，所有的訂單都免費，那麼我們應該怎麼改動呢？      

我們可以直接在 `ShippingCalculator`的 `CalculatesShipping`方法中，直接讓它的結果 `return 0` 嗎？        

如果直接在這裡修改了程式碼，那「雙11」結束之後，難道我們還要再改回來？      

```c#
//計算訂單的運費
public class ShippingCalculator
{
    public int CalculatesShipping(Order order)
    {
        //訂單超過$50免運，否則運費$10
        if (order.TotalPrice < 50) return 10;
        return 0;
    }
}
```

顯然，直接在`ShippingCalculator`中修改程式碼，是一種非常低級的操作，更好的選擇是：針對「雙11」的促銷，我們創建一個新的運費計算類，可以叫做「`DoubleElevenShippingCalculator`」。        

而這個「雙11」價格計算類中，也同樣需要一個運費計算的方法，而他的計算過程直接 `return 0` 就可以了。

```c#
//「雙11」計算運費系統
public class DoubleElevenShippingCalculator {
    public int CalculatesShipping(Order order) {
        return 0;
    }
}
```

回到「訂單處理系統」`OrderProcessor`，我們該如何使用新的「雙11運費計算系統」呢？

## 方案一：(這個方式感覺很蠢)
- 創建一個私有字段，不要使用 `ShippingCalculator`，而是使用 `DoubleElevenShippingCalculator`
- 同時在構造方法中，也為私有的「雙11運費計算系統」`_doubleElevenShippingCalculator`創建一個新實體
- 然後在 `Process()` 中，通過判斷時間來決定使用哪一個類

```c#
//訂單處理系統
public class OrderProcessor
{
    private readonly ShippingCalculator _shippingCalculator;
    private readonly DoubleElevenShippingCalculator _doubleElevenShippingCalculator;

    public OrderProcessor()
    {
        //在創建OrderProcessor的時候 實體化ShippingCalculator
        _shippingCalculator = new ShippingCalculator();
        _doubleElevenShippingCalculator = new DoubleElevenShippingCalculator();
    }
    .....
}
```

比如說，通過一個datetime 來確定今天是不是雙11，如果今天是雙11，那麼我們就使用雙11的價格計算器...，通過這樣的處理，我們也可以實現雙11價格計算、和普通價格計算處理。 

```c#
public void Process(Order order)
{
    //先判斷訂單是否已經被處理過
    if (order.IsShipped)
        throw new InvalidOperationException("訂單已出貨");
    //雙11
    DateTime doubleEvelen = new DateTime(2023, 11, 11);

    //如果訂單狀態正常，將會開始處理訂單，建立發貨信息
    order.Shipment = new Shipment {
        Cost =DateTime.Now == doubleEvelen? _doubleElevenShippingCalculator.CalculatesShipping(order): _shippingCalculator.CalculatesShipping(order),
        ShippingDate = DateTime.Today.AddDays(1)
    };
     Console.WriteLine($"訂單#{order.Id}完成，已出貨");
}
```

不過這個方式感覺很蠢，不僅多了很多程式碼，而且還把「雙11」的判斷機制交給了 `Order Process`，反而讓整個業務邏輯變得更加臃腫、更加繁瑣了，所以這種方法是肯定行不通的。

## 方案二：使用接口(介面)

使用接口來剝離「`order processor`」 vs 「`shipping calculator`」 之間的關係。       

比如說，「餐廳 vs 廚師」的例子，餐廳需要的是一個可以炒菜的人，我不在乎這個人是小明還是小張、我也不在乎他長得是高是矮、是胖還是瘦。      

我們把廚師看作是一個類，只要是這個廚師的對象(物件)，是能夠按照我們的需求執行炒菜這個方法就可以了。      

同樣的道理，計算運費，我不在乎你是「雙11計算」還是「普通計算」，只要能夠給訂單提供正確的方法就可以啦。      

所以對於 `order processor` 來說，只要它可以處理訂單的過程，調用`shipping calculator`這個方法就解決了。      

那麼為了達到個目的，我們可以把運費的計算過程全部抽象出來，放在一個接口(介面)中，這個接口(介面)就叫作「`IShippingCalculator`」。     

接下來，所有與運費相關的計算，都將會滿足這個 `shipping calculator`接口(介面)標準。      

而這個接口(介面)、這個標準，我們只需要包含 shipping calculate 這個方法就足夠了。

```c#
public interface IShippingCalculator
{
    int CalculateShipping(Order order);
}
```

那麼，接下來我們的「雙11 double eleven」 和 「普通的 shipping calculator」只要符合這個接口(介面)的定義就可以了。

#### 「雙11計算運費」實現接口(介面)
```c#
//「雙11」計算運費
public class DoubleElevenShippingCalculator : IShippingCalculator
{
    public int CalculateShipping(Order order)
    {
        return 0;
    }
}
```

#### 「普通計算運費」實現接口(介面)
```c#
//計算訂單的運費
public class ShippingCalculator : IShippingCalculator
{
    public int CalculateShipping(Order order)
    {
        //訂單超過$50免運，否則運費$10
        if (order.TotalPrice < 50) return 10;
        return 0;
    }
}
```
回到 `OrderProcessor`，現在我們就可以把系統依賴，從運費計算系統調整為面向運費計算標準了。        

接下來把 `ShippingCalculator` 改為接口(介面)，同時更改我們的構造方法，而我們真正的運費計算過程，則是通過構造方法的參數，傳遞到訂單處理系統中，而傳遞過程中依然使用 `interface` `IShippingCalculator`，讓外部傳入的`IShippingCalculator`等於私有成員變量`_shippingCalculator`。

```c#
//訂單處理系統
public class OrderProcessor
{
    private readonly IShippingCalculator _shippingCalculator;

    public OrderProcessor(IShippingCalculator shippingCalculator)
    {
        //在創建OrderProcessor的時候，讓外部傳入的IShippingCalculator 等於私有成員變量_shippingCalculator
        _shippingCalculator = shippingCalculator;
    }
    public void Process(Order order)
        {
            //先判斷訂單是否已經被處理過
            if (order.IsShipped)
                throw new InvalidOperationException("訂單已出貨");

            //如果訂單狀態正常，將會開始處理訂單，建立發貨信息
            order.Shipment = new Shipment {
                Cost = _shippingCalculator.CalculateShipping(order),
                ShippingDate = DateTime.Today.AddDays(1)
            };
            Console.WriteLine($"訂單#{order.Id}完成，已出貨");
        }
}
```
現在我們可以通過使用`IShippingCalculator`來同時指代「普通的ShippingCalculator」或是「雙11的ShippingCalculator」。       

通過使用接口(介面)，使用一定的標準，我們可以很好的把兩種不同的運費計算過程統一起來。        

而使用接口(介面)，不會對當前的訂單處理系統產生任何影響。而對於訂單處理系統來說，運費的計算跟它是沒有關係的，他不需要關心運費的計算過程，只需要把運費的計算結果放在訂單中就可以了。      

而整個運費的計算過程對於「訂單處理系統」來說，屬於黑箱操作，用這樣的方式，我們的運費和訂單的耦合就被解開了。        

不過我們還差最後一步，回到`Main`方法。      

我們還需要創建「運費計算系統」，並把「運費計算系統」傳遞到這個「訂單系統」中。      

首先，我們先來嘗試一下「雙11計算價格接口(介面)」

```c#
//「雙11」計算運費
IShippingCalculator doubleEleven = new DoubleElevenShippingCalculator();
var orderProcessor = new OrderProcessor(doubleEleven);
```
```c#
static void Main(string[] args)
{
    var order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100
    };

    //「雙11」計算運費
    IShippingCalculator doubleEleven = new DoubleElevenShippingCalculator();
    var orderProcessor = new OrderProcessor(doubleEleven);
    orderProcessor.Process(order);

    Console.Read();
}
```
這時候我們可能就會想：我們在實體化`orderProcessor`的時候，還不是得把「價格系統」傳遞進去嗎？所以「訂單處理系統 orderProcessor」和「價格系統」同樣還是會有依賴關係嘛，那麼這個答案「是」也「不是」。     

「訂單系統」的確與「價格系統」依然是有依賴關係的，但是「訂單系統」依賴的不再是「價格系統」，而是「價格系統」的接口(介面)。      

也就是說，誰可以給我「訂單系統」提供價格計算，我就依賴於誰。        

而這種依賴關係，僅僅存在於 `Main方法`這個邏輯操作的層面上，而在兩個系統的業務邏輯上，是不存在任何依賴關係的。       

比如說，現有「雙11」活動結束了，我們的價格系統要怎樣才能變回原本的價格呢？那其實這個就很簡單了，與「雙11價格」類似。        

我們在` Main方法` 中創建一個「普通的日常價格系統」，類型為接口(介面)，然後使一個`if`語句來判斷今天的日期。

```c#
static void Main(string[] args)
{
    var order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100
    };

    IShippingCalculator normal = new ShippingCalculator();//日常價格
    IShippingCalculator doubleEleven = new DoubleElevenShippingCalculator();//「雙11」計算運費
    var orderProcessor = new OrderProcessor(doubleEleven);

    //如果不是雙11，就使用普通計算價格
    if (DateTime.Now != new DateTime(2023, 11, 11)) {
        orderProcessor = new OrderProcessor(normal);
    }
    orderProcessor.Process(order);

    Console.Read();
}
```

## 完整Code

```c#

//計算運費介面
public interface IShippingCalculator
{
    int CalculateShipping(Order order);
}
//計算訂單的運費(日常)
public class ShippingCalculator : IShippingCalculator
{
    public int CalculateShipping(Order order)
    {
        //訂單超過$50免運，否則運費$10
        if (order.TotalPrice < 50) return 10;
        return 0;
    }
}
//「雙11」計算運費
public class DoubleElevenShippingCalculator : IShippingCalculator
{
    public int CalculateShipping(Order order)
    {
        return 0;
    }
}
//訂單
public class Order
{
    public int Id { get; set; }
    public DateTime DatePlaced { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsShipped { get; set; }
    public Shipment Shipment { get; set; }
}
//訂單處理系統
public class OrderProcessor
{
    private readonly IShippingCalculator _shippingCalculator;

    public OrderProcessor(IShippingCalculator shippingCalculator)
    {
        //在創建OrderProcessor的時候，讓外部傳入的IShippingCalculator 等於私有成員變量_shippingCalculator
        _shippingCalculator = shippingCalculator;
    }
    public void Process(Order order)
    {
        //先判斷訂單是否已經被處理過
        if (order.IsShipped)
            throw new InvalidOperationException("訂單已出貨");

        //如果訂單狀態正常，將會開始處理訂單，建立發貨信息
        order.Shipment = new Shipment {
            Cost = _shippingCalculator.CalculateShipping(order),
            ShippingDate = DateTime.Today.AddDays(1)
        };
        Console.WriteLine($"訂單#{order.Id}完成，已出貨");
    }
}
//發貨信息
public class Shipment
{
    public int Cost { get; set; } //運費
    public DateTime ShippingDate { get; set; }
}

internal class Program
{
    static void Main(string[] args)
    {
        var order = new Order {
            Id = 123,
            DatePlaced = DateTime.Now,
            TotalPrice = 100
        };

        IShippingCalculator normal = new ShippingCalculator();//日常價格
        IShippingCalculator doubleEleven = new DoubleElevenShippingCalculator();//「雙11」計算運費
        var orderProcessor = new OrderProcessor(doubleEleven);

        //如果不是雙11，就使用普通計算價格
        if (DateTime.Now != new DateTime(2023, 11, 11)) {
            orderProcessor = new OrderProcessor(normal);
        }
        orderProcessor.Process(order);

        Console.Read();
    }
}
```

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=38](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=38)