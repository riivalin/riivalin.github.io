---
layout: post
title: "[C# 筆記][IOC][DI][介面導向] 反轉控制 vs 依賴注入 -操作"
date: 2010-01-24 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,IOC 控制反轉,DI 依賴注入,interface 介面導向,ServiceCollection,反轉控制容器 ServiceCollection,]
---

## 前言
使用之前的程式碼：[[C# 筆記] Interface 接口(介面)實現](https://riivalin.github.io/posts/2010/01/r-csharp-note-20/#完整code)     

通過程式碼實戰，創建一個「反轉控制容器」，來進一步降低「訂單系統」和「價格系統」的耦合關係，直到把它們之間的關係降至為零。        

先來觀察目前的程式碼結構：
- 首先我們會手動使用「介面」來創建「價格計算器」
- 然後「訂單處理器」初始化的時候，把「價格計算器」作為參數傳遞給這個「訂單處理系統」
- 最後在「訂單處理器」的構造方法中，綁定「價格計算器」

請想一想，這樣的「訂單處理器」和「價格處理器」是什麼關係呢？它們之間的依賴是什麼呢？    

簡單來說，「訂單處理器」就是依賴於「價格處理器」所存在的，也就是說，我們得先有一個「價格處理器」，然後才能創建「訂單處理器」。      

從這個角度上來說，「訂單處理器」和「價格處理器」屬於高度耦合的關係，那麼怎麼樣，我們才可以剝離「訂單處理器」和「價格處理器」呢？        

那麼，這個時候就需要「反轉控制`IOC`」的幫助了。     

## MSDN

我們先來看看微軟官方是如何定義的：  
[MSDN - .NET Core 中的相依性插入](https://learn.microsoft.com/zh-tw/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0)，在這坨程式碼中，有兩個地方值得我們注意的：      

1. 引用`DI`
它引用了`Microsoft.Extensions.DependencyInjection;`命名空間

```c#
using Microsoft.Extensions.DependencyInjection;
```
2. 加入服務
接下來我們還需要注意到這個綠色框框，框起來的部分

```c#
services.AddScoped<IMessageWriter, MessageWriter>();
```
在這裡我們會把所有的服務，比方說這個`MessageWriter`，通過介面 `IMessageWriter` 添加到「反轉控制容器`IOC`」中，讓「反轉控制容器`IOC`」在程序運行的過程中，自動幫我們實現實體化，創建相應的對象(物件)，而這整個過程都是全程委託給「反轉控制容器`IOC`」自動處理的，我們不需要手動參與。

以上，要講的就這麼多。

## 開始程式碼
## 引入 DI
首先，正如剛剛我們看到的微軟官方案例程式碼，我們需要引入`Microsoft.Extensions.DependencyInjection`，

- 引用「反轉控制`IOC`」：Nuget > Microsoft.Extensions.DependencyInjection;

```c#
using Microsoft.Extensions.DependencyInjection;
```

## 配置IOC

到 Main 方法中，刪除除了創建訂單以外的所有程式碼，如下：

```c#
static void Main(string[] args)
{
    var order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100
    };

    Console.Read();
}
```

接下來我們來配置 `IOC 反轉控制容器`。 

- 配置 `IOC`
在 .Net中，「反轉控制容器」叫做 `Service Collection`，同樣也是對象(物件)，所以需要使用 `new`這個關鍵詞來實體化。

```c#
//配置IOC
ServiceCollection services = new ServiceCollection();
```

### 介面化服務

一般來說，添加「進入IOC容器」的程式碼，我們稱之為「服務」。     

所以接下來，我就要把「訂單服務」和「價格計算服務」一起添加到「`IOC容器`」中了。     

原則上，「`IOC容器`」只會使用「介面」來對服務進行匹配和解耦   

- IOC容器通過介面來識別服務

### 「訂單處理」介面化處理

所以如果我們希望向「反轉控制容器」添加「訂單服務」，那麼第一件事情就是需要把 `Order Processer `介面化處理。

```c#
public interface IOrderProcessor {
    void Process(Order ordrer);
}
```
介面完成。      

我們回到 `OrderProcessor`，我們讓`OrderProcessor`實現`IOrderProcessor`介面

```c#
public class OrderProcessor:IOrderProcessor {
    ......
}
```
通過這樣的方法，我們的「訂單處理器」就完成了介面化的處理了。        

## 向「IOC容器」中註冊服務
向「反轉控制容器」(`IOC容器`)加入服務: `AddScoped<介面,類別>`       

回到 Main方法

`AddScoped<介面,類別>`通過在泛型類型中定義「訂單處理介面」，來指定對應的「訂單處理服務」的具體業務實現程式碼。

```c#
services.AddScoped<IOrderProcessor, OrderProcessor>();
```

除了「訂單服務」，我們還需要添加「價格計算服務」。  

「價格計算服務」，它具體的實現，我們有兩個：
- 第一個是「普通價格計算 `ShippingCalculator`
- 另一個是「雙11價格計算」`DoubleElevenShippingCalculator`

這邊我們使用「雙11價格」

```c#
services.AddScoped<IShippingCalculator, DoubleElevenShippingCalculator>();//雙11價格計算服務
```

那麼現在兩個服務的介面和實現都確定下來了：  
我們分別添加了「訂單處理服務」，以及「價格計算服務」

```c#
//配置IOC
ServiceCollection services = new ServiceCollection();

//向「反轉控制容器」(IOC容器)加入服務
services.AddScoped<IOrderProcessor, OrderProcessor>(); //訂單處理服務
services.AddScoped<IShippingCalculator, DoubleElevenShippingCalculator>();//雙11價格計算服務
```

當我們在「`IOC容器`」中註冊好服務之後，那麼這兩個服務的生命周期就全權委託`IOC`來負責了。        

所以在向「`IOC容器`」分別註冊了兩個服務之後，我們就不需要再使用 `new` 這個關鍵詞來手動的創建服務的實體了，而創建服務新實體的過程，全部都被封裝在「`IOC容器`」中了。     

他自己會分析兩個服務的依賴關係，而且自己來決定這兩個服務分別實體化的處理時機。

## 向IOC容器添加服務的方式
向`IOC容器`添加服務的方式有三種：
- `singleton` 單例模式
- `scoped` 作用域模式
- `tansient` 瞬時模式

### singleton 單例模式
singleton 單例模式，`IOC容器`在管理單例服務的時候，全局，也就是整一個程序的運行生命周期內，只會創建唯一的一個實體。

```c#
services.AddSingleton<IOrderProcessor, OrderProcessor>(); //訂單處理服務
```

### scoped 作用域模式
`scoped` 也叫做「作用域模式」，`IOC容器`會在一個作用域的範圍內，創建唯一的一個實體。        

比如說，在後端API 程序中處理一次Http請求，整個過程就相當於一個 `scope`、一個作用域。

```c#
services.AddScoped<IOrderProcessor, OrderProcessor>(); //訂單處理服務
```

### tansient 瞬時模式
`tansient` 瞬時模式，指的是每一次調用服務的時候，`IOC容器`都會創建一個的新的實例對象(實體物件)，而服務操作結束之後，`IOC`也會自動收回內存，刪除這個實體。     

所以基本上，用 `tansient`定義的服務，都是一次性的用完就丟的這種服務。

```c#
services.AddTransient<IOrderProcessor, OrderProcessor>(); //訂單處理服務
```

## 從IOC提取服務 BuildServiceProvider

提取服務需要使用`IOC容器`中的 `BuildServiceProvider`。      

使用變量`services`調用 `BuildServiceProvider`，而他的返回值就是  `ServiceProvider`。

```c#
//從IOC提取服務
ServiceProvider serviceProvider = services.BuildServiceProvider();
```

那麼，接下來我們將會使用 `ServiceProvider`來提取 `OrderProccessor` 服務。       

首先咱們先聲明一個變量，訂單服務 `orderProcessor` 等於通過使用  `ServiceProvider`調用 `GetService`這個方法來調用相應的服務，但是我們需要通過「介面」來調用它，也就是 `IOrderProcessor`。

```c#
//從IOC提取服務
ServiceProvider serviceProvider = services.BuildServiceProvider();
//使用 ServiceProvider 來提取 訂單處理 服務
var orderProcessor = serviceProvider.GetService<IOrderProcessor>();
```

### AddScoped 單例模式

```c#
static void Main(string[] args)
{
    var order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100
    };

    //配置IOC
    ServiceCollection services = new ServiceCollection();
    //向 IOC容器 加入服務
    //向 IOC容器 添加服務的方式有三種：
    //singleton 單例模式
    //scoped 作用域模式
    //tansient 瞬時模式
    services.AddScoped<IOrderProcessor, OrderProcessor>(); //訂單處理服務
    services.AddScoped<IShippingCalculator, DoubleElevenShippingCalculator>();//雙11價格計算服務

    //從IOC提取服務
    ServiceProvider serviceProvider = services.BuildServiceProvider();
    //使用 ServiceProvider 來提取 訂單處理 服務
    var orderProcessor = serviceProvider.GetService<IOrderProcessor>();
    var orderProcessor2 = serviceProvider.GetService<IOrderProcessor>();

    //TODO: 處理訂單

    Console.Read();
}
//輸出:
//DoubleElevenShippingCalculator 被創建了
//OrderProcessor 被創建了
```

### 使用 singleton 單例模式
singleton 單例模式，`IOC容器`在管理單例服務的時候，全局，也就是整一個程序的運行生命周期內，只會創建唯一的一個實體。

```c#
services.AddSingleton<IOrderProcessor, OrderProcessor>(); //訂單處理服務
......

//輸出:
//DoubleElevenShippingCalculator 被創建了
//OrderProcessor 被創建了
```

現在來運行一下...   
結果發現命令行中，兩個服務都被創建了，而且只會被創建一次。      

注意：根據添加方式的不同，創建的機制和創建以後的持續時間也不一樣。      

比如說，我們現在使用的 `AddSingleton`，它只允許服務在整個程序的運行生命周期內創建一個。     

所以在接下來的程式碼中，無論我調用多少次 `serviceProvider`, `OrderProcessor` 的構造方法，都只會執行一次。      


比如說，我現在再加一個調用`orderProcessor2` 

```c#
var orderProcessor = serviceProvider.GetService<IOrderProcessor>();
var orderProcessor2 = serviceProvider.GetService<IOrderProcessor>();

//輸出:
//DoubleElevenShippingCalculator 被創建了
//OrderProcessor 被創建了
```
在命命行中同樣也只會打印一次，我們可以看到 `OrderProcessor`依然還是只會被創建一次。


### 使用 AddTransient 作用域模式

現在我們把 `AddSingleton`改為 `AddTransient`，運行..

```c#
services.AddTransient<IOrderProcessor, OrderProcessor>(); //訂單處理服務

//從IOC提取服務
ServiceProvider serviceProvider = services.BuildServiceProvider();
//使用 ServiceProvider 來提取 訂單處理 服務
var orderProcessor = serviceProvider.GetService<IOrderProcessor>();
var orderProcessor2 = serviceProvider.GetService<IOrderProcessor>();

//輸出：
//OrderProcessor 被創建了
//OrderProcessor 被創建了
```

這一次我們就會發現 `OrderProcessor`被創建了兩次。       

每次我們在調用 `serviceProvider`，就會為「訂單處理系統」創建一個新的服務實體。

## Singleton 和 Scoped
我們再把`AddTransient`改為 `AddScoped`...   
結果有點費解，構造方法只是出了一次，也就是說，服務只實體化了一次，  
那是不是說明 `AddScoped`跟`AddSingleton` 是一樣的呢？答案是肯定不一樣       

`AddScoped` 更加強調 `Scoped`，也就是作用域的概念，同一個服務在不同的作用域中，將會產生不同的實體。     
 
而 `Singleton`則是在整個程序運行的生命周期內都有。  

## 處理訂單
然後就到了我們最後一步，處理訂單了。        

那麼處理訂單使用`orderProcessor`，調用 `Process()`方法，傳入 訂單變量 `order`

```c#
orderProcessor.Process(order);
```

## 總結

通過`IOC容器`，我們「訂單系統」和「價格計算系統」之間已經沒有任何關係了，他們沒有依賴，互相之間僅剩下引用關係。     

而且這個引用是通過「介面」連接在一起的。        

現在「訂單系統」和「價格計算系統」的耦合，就成功複餓們降為零了，這就是「反轉控制」的強大之處。      

通過`IOC`，我們可以把系統中各種服務，從「依賴關係」轉變為「引用關係」，讓系統的價格關係更加清晰，開發和維護的效率都能夠得到明顯的提升。     

最後總結一下，不過是哪個語言，「`反轉控制IOC`」和「`依賴注入DI`」絕對都是核心。     

而「`反轉控制IOC`」背後的實現原理，則是面向接口編程(介面導向編程)，理解介面、掌握介面，能夠熟練使用介面，是應該熟悉掌握的基本技能。

## 完整程式碼

```c#
//訂單處理介面
public interface IOrderProcessor
{
    void Process(Order ordrer);
}
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
    public DoubleElevenShippingCalculator() {
        Console.WriteLine("DoubleElevenShippingCalculator 被創建了");
    }
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
public class OrderProcessor:IOrderProcessor
{
    private readonly IShippingCalculator _shippingCalculator;

    public OrderProcessor(IShippingCalculator shippingCalculator)
    {
        Console.WriteLine("OrderProcessor 被創建了");
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

        //配置IOC
        ServiceCollection services = new ServiceCollection();
        //向 IOC容器 加入服務
        //向 IOC容器 添加服務的方式有三種：
        //singleton 單例模式
        //scoped 作用域模式
        //tansient 瞬時模式
        services.AddScoped<IOrderProcessor, OrderProcessor>(); //訂單處理服務
        services.AddScoped<IShippingCalculator, DoubleElevenShippingCalculator>();//雙11價格計算服務

        //從IOC提取服務
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        //使用 ServiceProvider 來提取 訂單處理 服務
        var orderProcessor = serviceProvider.GetService<IOrderProcessor>();

        //處理訂單
        orderProcessor.Process(order);

        Console.Read();
    }
}
```
        
[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=42](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=42)