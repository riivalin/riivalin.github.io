---
layout: post
title: "[C# 筆記] 接口(介面) vs 多態(多型)"
date: 2010-01-21 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,interface,多型,ocp,泛型,list]
---

一句話總結：「接口(介面) 」與繼承沒有關係。     

不過，通過「接口(介面) 」我們就可以實現一些「接口(介面) 」的現象，比如說「多態(多型)」。        

在程序中，一個技能取代的就是一個「接口(介面) 」。

## 舉例

- 訂單處理系統 OrderProcessor
- 郵件通知系統 MailService

下面例子，問題出在這裡，「訂單處理系統」跟「郵件通知系統」是直接的依賴關係，這將會極大地限制系統的可擴展性，同時也增加了系統單元測試的難度。

```c#
public class OrderProcessor
{
    private readonly MailService _mailService;
    public OrderProcessor()
    {
        //問題出在這裡，訂單處理系統跟郵件通知系統是直接的依賴關係
        _mailService = new MailService();
    }

    public void Process(Order order)
    {
        //處理訂單…處理出貨

        //通知用戶收貨
        _mailService.Send("訂單已出貨");
    }
}
```
## 使用接口(介面)
所以首先我們需要對這兩個系統進行剝離，使用一個郵件服接口(介面)，來取代現有的「郵件服務類別」。      

同時把這個類別，從構造方法的外部注入「訂單處理系統」。  

不過，這只是第一個問題，而我們面臨的第二個問題則更為關鍵。      

如果明天我們調整了需求，要求我們保持在發送郵件的同時，增加一個「發送手機短信服的通知」，那麼，我們必須回到 `order processor類`裡找到 `proccess方法`，然後添加，並且修改新用的方法，每當有類似的需求變化的時候，我們都不得不回到這個「訂單處理器」中更新程式碼、重新編譯、最後打包上線。     

而任何依賴於這個「訂單處理系統」的其他系統，也要同步更新，全部重新打包上線。        

那麼，怎樣用最少的程式碼，對應隨時可能發生的需求變化呢？        

這時候就需要我們借用設計模式「OCP開閉原則」，而實現「OCP開閉原則」的最關鍵的核心就在於「抽象」，我們可以通過使用接口(介面)來獲得抽象的能力，通過抽象來產生「多態(多型)」的效果。

### 開始重構
還記得餐館的例子嗎？一家餐館不應該依賴於一個廚師，而應該依賴於這個廚師所掌握的技巧，或者他的技能，招聘廚師的時候，我們需要的是某一個人能滿足技能的要求，那麼我們就可以雇用這個人。      

而在程序中，某一個技能指代的就是一個接口(介面)。        

而同樣的概念，我們可以使用到「訂單處理系統」中，而考慮到未來通知消息服務可能是多種多樣的，所以，請先徹底忘記這個「郵件服務」這個東西。      

### 消息通知介面 INotification 
我們使用一個「`INotification`」來指代所有消息通道類型，那麼這個「消息通道」是一個純粹抽象的，我們只需要考慮「消息通道」的行為就可以了。

```c#
public interface INotification
{
    void Send(string message);
}
```

### 發送Email服務 MailService
回到 `MailService類別`，加上`:INotification`，讓它實現 `INotification` 

```c#
public class MailService : INotification
{
    public void Send(string message)
    {
        Console.WriteLine(message);
    }
}
```
為了區分不同類型的服務，我們可以在Console.WriteLine中加上發送mail這個string

```c#
public class MailService : INotification {
    public void Send(string message) {
        Console.WriteLine($"發送Email: {message}");
    }
}
```

### 手機短信發送服務 SmsMessageService
接下來我們來創建一個「手機短信發送服務」`SmsMessageService`，與 `MailService`大同小異。

```c#
public class SmsMessageService : INotification {
    public void Send(string message) {
        Console.WriteLine($"手機發送短信：{message}");
    }
}
```
### 訂單處理系統 OrderProcessor
接下來回到「訂單處理系統」，現在`OrderProcessor`，我們看到只能處理 `MailService`這一種信息通道。        

但是根據我們的需求，`OrderProcessor`可能會時發送各種各樣的信息，可能是短信，可能是 Mail，甚至可能是傳真，所以通道信息可能不止一個，那麼它就應該是一個列表。     

我們可以通過列表 `List`，加上泛型來定義這個信息通道。

```c#
private readonly List<INotification> messageServices; //複數，所以單字字尾加s
```
既然 `messageServices`是一個列表，所以我們必須在「訂單處理系統」初始化的時候，創建這個列表。

```c#
public OrderProcessor() {
    messageServices = new List<INotification>();
}
```
不過，為了可以更好的增加系統的靈活性，「信息通道」應該是從「訂單處理系統」的外部被註冊，並且添加進來的。

所以我們應該要給「訂單處理系統」再加上一個公有的「消息服註冊方法」，而這個方法名稱就叫做「`RegisterNotificaion`」。     

```c#
public void RegisterNotificaion() { }
```

方法可以從外部調用，外部傳入的是什麼對象(物件)都無所謂，但是，它有一個要求，就是要實現 `INotification` 的定義。     

注意，它的參數類型需要為 `INotification`

```c#
public void RegisterNotificaion(INotification notification) {  }
```

接下來，我們就可以向這個「消息服務列表」添加這個新的消息通道了，

```c#
public void RegisterNotificaion(INotification notification) {
    //向「消息服務列表」添加新的消息通道
    messageServices.Add(notification);
}
```

最後一步也是最簡單的一步，接下來，我們需要一個`for`循環，來循環消息列表服務

```c#
//通知用戶收貨
foreach (INotification n in messageServices)
{
    n.Send("訂單已出貨");
}
```
好啦，程式碼重構完成了

```c#
public class OrderProcessor
{
    private readonly List<INotification> messageServices; //複數，所以單字字尾加s
    public OrderProcessor()
    {
        messageServices = new List<INotification>();
    }

    public void RegisterNotificaion(INotification notification)
    {
        //向「消息服務列表」添加新的消息通道
        messageServices.Add(notification);
    }

    public void Process(Order order)
    {
        //處理訂單…處理出貨

        //通知用戶收貨
        foreach (INotification n in messageServices)
        {
            n.Send("訂單已出貨");
        }
    }
}
```

### Main方法
回到 Main方法，目前「訂單處理系統」的消息列表是空的，它是沒有辦法處理消息發送的。       

#### 添加 MailService 郵件服務
所以接下來，讓我們添加第一種消息通道：      
- 創建一個基於 `INotification` 的 `MailService`郵件服務
- 註冊到「訂單處理系統」的列表中

```c#
//創建一個基於INotification的MailService郵件服務
INotification mailService = new MailService();
//註冊到「訂單處理系統」的列表中
orderProcessor.RegisterNotificaion(mailService);
```

#### 添加 手機短信服務

```c#
INotification smsService = new SmsMessageService();
orderProcessor.RegisterNotificaion(smsService);
```

### 程式碼 (Main 方法)

```c#
static void Main(string[] args)
{
    //創建訂單
    Order order = new Order {
        Id = 123,
        DatePlaced = DateTime.Now,
        TotalPrice = 100
    };

    //創建訂單處理系統
    OrderProcessor orderProcessor = new OrderProcessor();

    //發送mail通知
    INotification mailService = new MailService(); //創建一個基於INotification的MailService郵件服務
    orderProcessor.RegisterNotificaion(mailService);//註冊到「訂單處理系統」的列表中

    //發送手機短信
    INotification smsService = new SmsMessageService();
    orderProcessor.RegisterNotificaion(smsService);

    //處理訂單
    orderProcessor.Process(order);

    Console.Read();
}
```

## OCP開閉原則
- Open for extension, and Closed for modification
- Open for extension 對於系統擴展是開發的
- Closed for modification 對於修改是關閉的

而實現「OCP開閉原則」的最關鍵的核心就在於「抽象」，可以通過使用接口(介面)來獲得抽象的能力，通過抽象來產生「多態(多型)」的效果。


## 完整程式碼

```c#
//信息通知管道介面
public interface INotification
{
    void Send(string message);
}
//發送mail服務
public class MailService : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"發送Email: {message}");
    }
}
//發送手機短信服務
public class SmsMessageService : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"手機發送短信：{message}");
    }
}

//訂單處理系統
public class OrderProcessor
{
    private readonly List<INotification> messageServices; //複數，所以單字字尾加s
    public OrderProcessor()
    {
        messageServices = new List<INotification>();
    }

    public void RegisterNotificaion(INotification notification)
    {
        //向「消息服務列表」添加新的消息通道
        messageServices.Add(notification);
    }

    public void Process(Order order)
    {
        //處理訂單…處理出貨

        //通知用戶收貨
        foreach (INotification n in messageServices)
        {
            n.Send("訂單已出貨");
        }
    }
}

//訂單
public class Order
{
    public int Id { get; set; }
    public DateTime DatePlaced { get; set; }
    public int TotalPrice { get; set; }
}

//主程式
internal class Program
{
    static void Main(string[] args)
    {
        //創建訂單
        Order order = new Order {
            Id = 123,
            DatePlaced = DateTime.Now,
            TotalPrice = 100
        };

        //創建訂單處理系統
        OrderProcessor orderProcessor = new OrderProcessor();

        //發送mail通知
        INotification mailService = new MailService(); //創建一個基於INotification的MailService郵件服務
        orderProcessor.RegisterNotificaion(mailService);//註冊到「訂單處理系統」的列表中

        //發送手機短信
        INotification smsService = new SmsMessageService();
        orderProcessor.RegisterNotificaion(smsService);

        //處理訂單
        orderProcessor.Process(order);

        Console.Read();
    }
}
```



[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=45](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=45)