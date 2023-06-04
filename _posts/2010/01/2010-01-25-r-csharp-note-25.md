---
layout: post
title: "[C# 筆記][UnitTest] 介面與單元測試"
date: 2010-01-25 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,interface,單元測試,MSTest,UnitTest,Assert,ExpectedException,typeof,InvalidOperationException]
---

單元測試是自動化測試的一個部分，基本原理就是我們寫程式碼來測試自己的程式碼。        

# 新建 MSTest 測試專案
- 方案 > 新增專案 > 測試 > `MSTest` 測試專案
- MSTest 測試專案：微軟開發的測試框架
- 專案命名規則：「`專案名稱.UnitTests`」 (Test是複數要加s，Tests)，`HelloWorld.UnitTests `  

# 類別與方法重新命名

因為我們要測試的對象是「訂單處理系統」，所以類別改名為`OrderProcessTest`
- 類別命名規則：`類別.Test`，`OrderProcessTest`     

接下來每一條測試用例，也要遵循一定的命名規範：「被測方法_條件_期望結果」這三個方面構成，所以第一個案例：訂單處理`Process`，訂單未出貨`OrderUnShipped`，而最終的期望結果就是計算運費價格 `SetShipment`。

- 「被測方法_條件_期望結果」：`Process_OrderUnshipped_SetShipment`

這個方法名稱的確很長，但是請不要害怕名字很長，好的程式碼讀起好像是在閱讀文章一樣，我們不需要給他添加註解，良好的變量、良好的方法的名稱是可以自我描述的，所以命名的長短並不重要，重要的是，一定要考慮到團隊，其他的成員能不能讀懂你的程式碼。

# 第一個測試案例 Assert
## 添加測試邏輯
接下來要考慮如何添加測試邏輯了。        

因為我們測試的是「訂單處理系統」，所以首先我們先實體化一個 `Order Proccessor`(記得要先引用專案，不然會報錯)。       

### 先創建一個「假的價格計算系統」
我們知道 `Order` 價格的計算是由「價格系統」所決定的，我們希望的系統是希望能全單加 $5 的運費，我們可以在測試案例中，通過介面來創建一個「假的價格計算系統」。     

所以在測試專案中，添加一個 `FakeShippingCalculator`，然後加上冒號 `:`，讓這個 `class` 實現  `IShippingCalculator` 介面，為了滿足測試需要，我們的返回值讓它返回5塊錢就可以了。

```c#
internal class FakeShippingCalculator : IShippingCalculator {
    public int CalculateShipping(Order order) {
        return 5;
    }
}
```

### 創建「訂單處理」+ 訂單
回到`OrderProccessorTest`測試案例，把「假的價格計算系統」放到`OrderProccessor`實體中。      

```c#
//被測方法_條件_期望結果
[TestMethod]
public void Process_OrderUnshipped_SetShipment()
{
    OrderProcessor orderProcessor = new OrderProcessor(new FakeShippingCalculator());
}
``` 

接下來，創建一個新的訂單：  

```c#
//被測方法_條件_期望結果
[TestMethod]
public void Process_OrderUnshipped_SetShipment()
{
    //實體化「訂單處理系統」，並把「價格計算系統」傳入
    OrderProcessor orderProcessor = new OrderProcessor(new FakeShippingCalculator());

    //創建訂單
    Order order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100
    };
}
```

並且使用 `orderProcessor`來處理這張訂單：

```c#
[TestClass]
public class OrderProcessorTest
{
    //被測方法_條件_期望結果
    [TestMethod]
    public void Process_OrderUnshipped_SetShipment()
    {
        //實體化「訂單處理系統」，並把「價格計算系統」傳入
        OrderProcessor orderProcessor = new OrderProcessor(new FakeShippingCalculator());

        //創建訂單
        Order order = new Order {
            Id = 123,
            DatePlaced = DateTime.Now,
            TotalPrice = 100
        };

        //處理訂單
        orderProcessor.Process(order);
    }
}
```
以上這些程式碼就結束了。    

### 檢驗測試結果

現在是最關鍵的最後一步了：檢驗測試結果。        

測試結果的檢驗，需要使用 `MSTest` 框架自帶的 `Assert` 這個方法。        

而測試結果，我們有兩點需要注意：
1. 訂單的運費必須為5元 `AreEqual`
2. 訂單的狀態必須改為已出貨 `IsTrue`

### Assert.AreEqual
第一個測試結果，我們可以用 `AreEqual`，那測試的就是訂單的 `shipment cost` 是否等於 5元。

```c#
Assert.AreEqual(order.Shipment.Cost, 5);
```

### Assert.IsTrue

而第二個測試就是：檢測訂單出貨狀態是否變為 `true`，同樣使用 `Assert`，而這次將會使用`.IsTrue` 這個方法來測試，測試內容為 `order.IsShipped`有沒有被更改為 `true`。   

```c#
Assert.IsTrue(order.IsShipped);
```

說明一下，一個測試案例，同時寫了兩個`Assert`，這個方法則代表兩個測試結果必須同時通過，才算整個測試案例成功。

```c#
//被測方法_條件_期望結果
[TestMethod]
public void Process_OrderUnshipped_SetShipment()
{
    //實體化「訂單處理系統」，並把「價格計算系統」傳入
    OrderProcessor orderProcessor = new OrderProcessor(new FakeShippingCalculator());

    //創建訂單
    Order order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100
    };

    //處理訂單
    orderProcessor.Process(order);

    //驗証測試結果：
    //1.訂單的運費必須為5元 (AreEqual)
    //2.訂單的狀態必須改為已出貨 (IsTrue)
    Assert.AreEqual(order.Shipment.Cost, 5);
    Assert.IsTrue(order.IsShipped);
}
```
## 運行:失敗&成功

運行一下：測試 > 運行 所有的測試            

現在跑起來了，有報錯，「測試報告」告訴我們：`assert is true` 失敗了，失敗的問題就是 `order.IsShipped` 沒有被正確改為 `true`。       

這是上次故意留下的 `bug`，在訂單處理過程中，故意忘記在 `Processor()` 方法中修改訂單的狀態了，所以請現在在「訂單處理系統」的最後加上 `order.IsShipped = true;`

```c#
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

    //訂單的出貨狀態設為true
    order.IsShipped = true;
    Console.WriteLine($"訂單#{order.Id}完成，已出貨");
}
```

再運行一次就測試成功了。        

# 第二個測試案例 ExpectedException
「訂單處理」有兩條路徑：剛剛我們只是測試了第二條：
可以正常出貨的路徑，而為了達到100%的程式碼覆蓋率，
我們還需要測試第一條：也就是當`order` 已經出過貨，訂單處理方法將會拋出異常的這種情況。      

被測試的方法 `Process_OrderIsShippped_ThrowException`，方法上需要加上`[TestMethod]`，代表這是一個測試案例，同樣我們是需要創建「訂單處理系統」和「訂單」。       

這次不一樣的是，我們在創建訂單的時候，一上來就把這個訂單的 `IsShipping` 改為 `true`，再丟給 `porcess` 「訂單處理系統」去處理。      

正常情況下，對於一個已經出貨的訂單，我們的處理方式將會拋出異常，對於異常的檢查，我們就不可以再用之前用過的 `Assert`這個斷言的方法，因為程式碼根本就執行不到異常。       

所以 `MSTest` 框架，對於異常也有一個特殊的處理方法，就是要使用另外一個 `attribute`，另外一個特徵屬性來監異常了，這個屬性就叫做 `[ExpectedException]`，同時我們還要給它將要拋出異常的類型，它將要拋出異常的類型是什麼呢？        

就是 `InvalidOperationException`，我們可以使用`typeof`通過反射機制來獲取它的異常類型。      

```c#
[TestMethod]
[ExpectedException(typeof(InvalidOperationException))]
public void Process_OrderIsShippped_ThrowException()
{
    //實體化「訂單處理系統」，並把「價格計算系統」傳入
    OrderProcessor orderProcessor = new OrderProcessor(new FakeShippingCalculator());

    //創建訂單
    Order order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100,
        IsShipped = true
    };

    //處理訂單
    orderProcessor.Process(order);
}
```
現在我們第二個測試案例就完成了。

# 完整程式碼
## 被測試的專案

```c#
//訂單處理介面
public interface IOrderProcessor {
    void Process(Order ordrer);
}
//計算運費介面
public interface IShippingCalculator {
    int CalculateShipping(Order order);
}

//計算訂單的運費(日常/普通的)
public class ShippingCalculator : IShippingCalculator {
    public int CalculateShipping(Order order) {
        //訂單超過$50免運，否則運費$10
        if (order.TotalPrice < 50) return 10;
        return 0;
    }
}
//「雙11」計算運費
public class DoubleElevenShippingCalculator : IShippingCalculator {
    public DoubleElevenShippingCalculator() {
        Console.WriteLine("DoubleElevenShippingCalculator 被創建了");
    }

    public int CalculateShipping(Order order) {
        return 0;
    }
}

//出貨信息
public class Shipment {
    public int Cost { get; set; } //運費
    public DateTime ShippingDate { get; set; }
}

//訂單
public class Order {
    public int Id { get; set; }
    public DateTime DatePlaced { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsShipped { get; set; }
    public Shipment Shipment { get; set; }
}

//訂單處理系統
public class OrderProcessor:IOrderProcessor {
    private readonly IShippingCalculator _shippingCalculator;

    public OrderProcessor(IShippingCalculator shippingCalculator) {
        Console.WriteLine("OrderProcessor 被創建了");
        //在創建OrderProcessor的時候，讓外部傳入的IShippingCalculator 等於私有成員變量_shippingCalculator
        _shippingCalculator = shippingCalculator;
    }

    public void Process(Order order) {
        //先判斷訂單是否已經被處理過
        if (order.IsShipped)
            throw new InvalidOperationException("訂單已出貨");

        //如果訂單狀態正常，將會開始處理訂單，建立發貨信息
        order.Shipment = new Shipment {
            Cost = _shippingCalculator.CalculateShipping(order),
            ShippingDate = DateTime.Today.AddDays(1)
        };

        //訂單的出貨狀態設為true
        order.IsShipped = true;
        Console.WriteLine($"訂單#{order.Id}完成，已出貨");
    }
}

//Main方法
using Microsoft.Extensions.DependencyInjection;
namespace IOCDemo
{
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
}
```

## 單元測試(MSTest)

```c#
//假的價格計算系統
namespace IOCDemo.UnitTests {
    internal class FakeShippingCalculator : IShippingCalculator {
        public int CalculateShipping(Order order) {
            return 5;
        }
    }
}

//兩個測試案例
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IOCDemo;
namespace IOCDemo.UnitTests
{
    //第一個測試案例：可以正常出貨
    [TestClass]
    public class OrderProcessorTest
    {
        //被測方法_條件_期望結果
        [TestMethod]
        public void Process_OrderUnshipped_SetShipment()
        {
            //實體化「訂單處理系統」，並把「價格計算系統」傳入
            OrderProcessor orderProcessor = new OrderProcessor(new FakeShippingCalculator());

            //創建訂單
            Order order = new Order {
                Id = 123,
                DatePlaced = DateTime.Now,
                TotalPrice = 100
            };

            //處理訂單
            orderProcessor.Process(order);

            //驗証測試結果：
            //1.訂單的運費必須為5元 (AreEqual)
            //2.訂單的狀態必須改為已出貨 (IsTrue)
            Assert.AreEqual(order.Shipment.Cost, 5);
            Assert.IsTrue(order.IsShipped);
        }

        //第二個測試案例：已經出過貨
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_OrderIsShippped_ThrowException()
        {
            //實體化「訂單處理系統」，並把「價格計算系統」傳入
            OrderProcessor orderProcessor = new OrderProcessor(new FakeShippingCalculator());

            //創建訂單
            Order order = new Order {
                Id = 123,
                DatePlaced = DateTime.Now,
                TotalPrice = 100,
                IsShipped = true //這裡要設置true，處理方式將會拋出異常
            };

            //處理訂單
            orderProcessor.Process(order);
        }
    }
}
```


[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=40](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=40)