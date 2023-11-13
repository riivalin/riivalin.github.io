---
layout: post
title: "[閱讀筆記][Design Pattern] Ch24.職責鏈模式(Chain of Responsibility)"
date: 2009-03-24 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 職責鏈模式(Chain of Responsibility)
職責鏈模式(Chain of Responsibility)，使多個物件都有機會處理請求，從而避免請良的發送者和接收者之間的耦合關係。將這物件連成一條鏈，並沿著這條鏈傳遞該請求，直到有一個物件處理它為止。     

> 類別有太多的責任，這違背了「[單一職責原則(Single Responsibility Principle)](https://riivalin.github.io/posts/2009/03/ch3-single-responsibility-principle/)」，增加新的管理類別，需要修改這個類別，違背了「[開放封閉原則 Open-Closed Principle (OCP)](https://riivalin.github.io/posts/2009/03/ch4-open-closed-principle/)」。


## 結構

- `Handler` 定義一個處理請示的介面
- `ConcreteHandler` 具體處理者類別，處理它所負責的請求，可使用它的後繼者，如果可處理該請求，就處理之，否則就將該請求轉發給它的後繼者

```
Client

    Handler 定義一個處理請示的介面
    + SetSuccessor(in successor: Handler)
    + HandleRequest(in request: int)

        ConcreteHandler1 具體處理者類別，處理它所負責的請求，可使用它的後繼者，如果可處理該請求，就處理之，否則就將該請求轉發給它的後繼者
        + HandleRequest(in request: int)

        ConcreteHandler2
        + HandleRequest(in request: int)
```

## 程式碼
### Handler

`Handler` 定義一個處理請示的介面。

```c#
abstract class Handler {
    protected Handler successor;

    //設定繼任者
    public void SetSuccessor(Handler successor) {
        this.successor = successor;
    }

    //處理請求的抽象方法
    public abstract void HandleRequest(int resquest);
}
```

### ConcreteHandler
`ConcreteHandler` 具體處理者類別，處理它所負責的請求，可使用它的後繼者，如果可處理該請求，就處理之，否則就將該請求轉發給它的後繼者。

#### ConcreteHandler1
ConcreteHandler1 當請求數在0到10之間則有權處理，否則轉到下一位。

```c#
class ConcreteHandler1: Handler {
    public override void HandleRequest(int resquest) {
        //0-10處理此請求
        if(resquest >= 0 && resquest <= 10) {
            Console.WriteLine($"{this.GetType().Name} 處理請求 {resquest}");
        } //轉移到下一位 
        else if(successor != null) {
            successor.HandleRequest(resquest);
        }
    }
}
```

#### ConcreteHandler2
ConcreteHandler2 當請求數在10到20之間則有權處理，否則轉到下一位。

```c#
class ConcreteHandler2: Handler {
    public override void HandleRequest(int resquest) {
        //10-20處理此請求
        if(resquest > 10 && resquest <= 20) {
            Console.WriteLine($"{this.GetType().Name} 處理請求 {resquest}");
        } //轉移到下一位 
        else if(successor != null) {
            successor.HandleRequest(resquest);
        }
    }
}
```

#### ConcreteHandler3
ConcreteHandler3 當請求數在20到30之間則有權處理，否則轉到下一位。

```c#
class ConcreteHandler3: Handler {
    public override void HandleRequest(int resquest) {
        //20-30處理此請求
        if(resquest > 20 && resquest <= 30) {
            Console.WriteLine($"{this.GetType().Name} 處理請求 {resquest}");
        } //轉移到下一位 
        else if(successor != null) {
            successor.HandleRequest(resquest);
        }
    }
}
```

#### 用戶端程式碼
用戶端程式碼，向鏈上的具體處理者物件提交請求。

```c#
public static void Main()
{
    Handler h1 = new ConcreteHandler1();
    Handler h2 = new ConcreteHandler2();
    Handler h3 = new ConcreteHandler3();

    //設定職責鏈上家與下家
    h1.SetSuccessor(h2);
    h2.SetSuccessor(h3);

    int[] requests = {2,9,14,22,18,3,27,20};

    //迴圈給最小處理者提交請求，不同的數額，由不同許可權處理者處理
    foreach(int r in requests) {
        h1.HandleRequest(r);
    }
}

/* 執行結果
ConcreteHandler1 處理請求 2
ConcreteHandler1 處理請求 9
ConcreteHandler2 處理請求 14
ConcreteHandler3 處理請求 22
ConcreteHandler2 處理請求 18
ConcreteHandler1 處理請求 3
ConcreteHandler3 處理請求 27
ConcreteHandler2 處理請求 20
*/
```

## 職責鏈的好處
這當中最關鍵的是：當客戶提交一個請求時，請求是沿鍵傳遞直到有一個`ConcreteHandler`物件負責處理它。       

> 這樣的好處是說：請求者不用管哪個物件來處理，反正該請求會被處理就對了。      

這就使得接收者和發送者都沒有對方的明確資訊，且鏈中的物件自己也不知道鏈的結構。結果是職責鏈可簡化物件的相互連接，它們僅需保持一個指向其後繼者的參考，而不需保持它所有的候選接受者的參考，也就大大降低了耦合度了。  

> 由於是用戶端來定義鏈的結構，我們可以隨時地增加或修改處理一個請求的結構。增強了給物件指派職責的靈活性。        

### 注意
雖然很靈活，不過也要當心，一個請求極有可能到了鏈的末端都得不到處理，或者因為沒有正確配置而得不到處理，這就很糟糕了。需要事先考慮全面。

> 這就跟現實中郵件一封信，因地址不對，最終無法送達一樣。

### 重點
- 你需要事先給每個具體管理者設定他的上司是哪個類別，也就是設定後繼者。
- 你需要在每個具體管理者處理請求時，做出判斷，是可以處理這個請求，還是必須要「推卸責任」，轉移給後繼者去處理。

# v1.0 加薪程式-初步

- 類別有太多的責任，這違背了「[單一職責原則(Single Responsibility Principle)](https://riivalin.github.io/posts/2009/03/ch3-single-responsibility-principle/)」，增加新的管理類別，需要修改這個類別，違背了「[開放封閉原則 Open-Closed Principle (OCP)](https://riivalin.github.io/posts/2009/03/ch4-open-closed-principle/)」。
- 比較長的方法，多條的分支，這些其實都是程式壞味道。
- 需解決：經理無權上報總監，總監無權再上報總經理。它們之間有一定的關聯，把用戶的請求傳遞，直到可以解決這個請求為止。

## 申請

```c#
class Request {
    public string RequestType { get; set; } //申請類別
    public string Content { get; set; } //申請內容
    public int Number { get; set; } //數量
}
```

## 管理者

比較長的方法，多條的分支，這些其實都是程式壞味道。

```c#
class Manager {
    string name;
    public Manager(string name) {
        this.name = name;
    }

    //得到結果
    //比較長的方法，多條的分支，這些其實都是程式壞味道。
    public void GetResult(string managerLevel, Request request) {
        switch (managerLevel) {
            case "經理":
                if(request.RequestType == "請假" && request.Number <= 2) {
                    Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 被批准。");
                } else {
                    Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 我無權處理");
                }
            break;
            case "總監":
                if(request.RequestType == "請假" && request.Number <= 5) {
                    Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 被批准。");
                } else {
                    Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 我無權處理");
                }
            break;
            case "總經理":
                if(request.RequestType == "請假") {
                    Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 被批准。");
                } else if(request.RequestType == "加薪" && request.Number <= 500) {
                    Console.WriteLine($"{name}: {request.Content} 加薪 ${request.Number} 被批准。");
                } else if(request.RequestType == "加薪" && request.Number > 500) {
                    Console.WriteLine($"{name}: {request.Content} 加薪 ${request.Number} 再說吧。");
                }
            break;
        }
    }
}
```

## 用戶端程式

寫得不好：(TODO)
- 比較長的方法，多條的分支，這些其實都是程式壞味道。
- 需解決：經理無權上報總監，總監無權再上報總經理
- 它們之間有一定的關聯，把用戶的請求傳遞，直到可以解決這個請求為止。
- 類別有太多的責任，這違背了「[單一職責原則(Single Responsibility Principle)](https://riivalin.github.io/posts/2009/03/ch3-single-responsibility-principle/)」，增加新的管理類別，需要修改這個類別，違背了「[開放封閉原則 Open-Closed Principle (OCP)](https://riivalin.github.io/posts/2009/03/ch4-open-closed-principle/)」。

```c#
//三個管理者
Manager ken = new Manager("Ken"); //經理
Manager leo = new Manager("Leo"); //總監
Manager jim = new Manager("Jim"); //總經理

//小菜請求加薪
Request request = new Request();
request.RequestType = "加薪";
request.Content = "小菜請求加薪";
request.Number = 1000;

//不同級別對此請求做判斷和處理
ken.GetResult("經理",request);
leo.GetResult("總監",request);
jim.GetResult("總經理",request);

//小菜請假3天
Request request = new Request();
request.RequestType = "請假";
request.Content = "小菜請假";
request.Number = 3;
//不同級別對此請求做判斷和處理
ken.GetResult("經理",request);
leo.GetResult("總監",request);
jim.GetResult("總經理",request);
```

# v2.0 加薪程式-重構
把管理者類別當中的那些分支 分解到每一個具體的管理者類別當中，然後利用事先設定的後繼者來實現請求處理的許可權問題。

## 結構
```
管理者
+ 設定管理者上級()
+ 申請請求(in request: 申請)

    經理
    + 申請請求(in request: 申請)

    總監
    + 申請請求(in request: 申請)

    總經理
    + 申請請求(in request: 申請)

申請
+ 申請類別: string
+ 申請內容: string
+ 數量: int
```

## 程式碼
### 申請
```c#
class Request {
    public string RequestType { get; set; } //申請類別
    public string Content { get; set; } //申請內容
    public int Number { get; set; } //數量
}
```

### 管理者

```c#
abstract class Manager {
    protected string name;
    protected Manager superior; //管理者的上級
    public Manager(string name) {
        this.name = name;
    }

    //設定管理者的上級(關鍵的方法)
    public void SetSuperior(Manager superior) {
        this.superior = superior;
    }

    //申請請求
    public abstract void RequestApplications(Request request);
}
```

### 經理類別

```c#
class CommonManager: Manager {
    public CommonManager(string name): base(name) { }

    public override void RequestApplications(Request request) {
        //經理的權限：可准許下屬兩天內的假期
        if(request.RequestType == "請假" && request.Number <= 2) {
            Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 被批准。");
            return;
        }  
        //其餘的申請都需轉到上級
        if(superior != null) {
            superior.RequestApplications(request);
        }
    }
}
```

### 總監類別

```c#
class Majordomo: Manager {
    public Majordomo(string name): base(name) { }

    public override void RequestApplications(Request request) {
        //總監的權限：可准許下屬5天內的假期
        if(request.RequestType == "請假" && request.Number <= 5) {
            Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 被批准。");
            return;
        }  
        //其餘的申請都需轉到上級
        if(superior != null) {
            superior.RequestApplications(request);
        }
    }
}
```

### 總經理類別

```c#
class GeneralManager: Manager {
    public GeneralManager(string name): base(name) { }

    public override void RequestApplications(Request request) {
        //總經理的權限：可准許下屬任意天的假期
        if(request.RequestType == "請假") {
            Console.WriteLine($"{name}: {request.Content} 休假數 {request.Number} 被批准。");
        } else if(request.RequestType == "加薪" && request.Number <= 500) {
            Console.WriteLine($"{name}: {request.Content} 加薪 ${request.Number} 被批准。");
        } else if(request.RequestType == "加薪" && request.Number > 500) {
            Console.WriteLine($"{name}: {request.Content} 加薪 ${request.Number} 再說吧。");
        }
    }
}
```

### 用戶端程式碼

```c#
public static void Main()
{
    //三個管理者
    CommonManager ken = new CommonManager("Ken"); //經理
    Majordomo leo = new Majordomo("Leo"); //總監
    GeneralManager jim = new GeneralManager("Jim"); //總經理

    //設定上級，完全可以根據實際需求來更改設定
    ken.SetSuperior(leo);
    leo.SetSuperior(jim);

    //用戶端的申請都是由「經理」發起，但實際誰來決策由具體管理類別來處理，用戶端不知道
    Request request = new Request();
    request.RequestType = "請假";
    request.Content = "小菜請假";
    request.Number = 1;
    ken.RequestApplications(request);
            
    Request request2 = new Request();
    request2.RequestType = "請假";
    request2.Content = "小菜請假";
    request2.Number = 5;
    ken.RequestApplications(request2);

    Request request3 = new Request();
    request3.RequestType = "加薪";
    request3.Content = "小菜請求加薪";
    request3.Number = 500;
    ken.RequestApplications(request3);

    Request request4 = new Request();
    request4.RequestType = "加薪";
    request4.Content = "小菜請求加薪";
    request4.Number = 1000;
    ken.RequestApplications(request4);	
}

/*
Ken: 小菜請假 休假數 1 被批准。
Leo: 小菜請假 休假數 5 被批准。
Jim: 小菜請求加薪 加薪 $500 被批准。
Jim: 小菜請求加薪 加薪 $1000 再說吧。
*/
```

解決原來大量的分支判斷造成難維護、靈活性差的問題。