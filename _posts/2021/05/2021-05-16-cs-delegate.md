---
layout: post
title: "[C# 筆記] 委派(Delegate)"
date: 2021-05-16 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,委派,Delegate,Event]
---

# 什麼是委派(Delegate)？        

「委派(`Delegate`)」可以看成一種**方法指標(Method Pointer)**。        

- 委派就是：這有一件事情，我不親自去做，而是交給別人來做。
- 把函數（方法）當作一個變量、參數。     

> 委派：把小方法當作一個參數 傳給大方法裡面，小方法還需要使用 大方法裡面的變量。        
> 實現這種功能的辦法就叫委派。


> 通過`+`增加「子委派」，通過`-`刪除某個「子委派」      
> 多重傳送委派 調用的返回值 是 最後一個執行委派的返回值       


使用「委派(`Delegate`)」可以間接叫用方法。      
此方法指標專門指向一個方法。        

可以將`Delegate`想像成電視搖控器一樣，不需要親自去轉台便能透過搖控器來選擇想要看的節目或調整音量大小。      

「委派(`Delegate`)」通常主要應用於：        
(1) 事件(`Event`)的驅動     
(2) 兩個處理程序(`Process`)間互相呼叫(`Call Back`)的狀況        

委派可以利用 `+` 號運算子來串連多個方法，最常的應用就是 利用單一事件`Event`引發多個事件。

> 什麼是委派？      
> 如果我們要把方法當參數來傳遞的話，就要用到委派，簡單來說，委派是一個類型，這個類型可以賦值一個方法的引用。
>       
> 一般我們變數都是儲存數據，像是string, int, float, double...       
> 委派變數可以把方法複製過來，通過變數去調用這個方法        

## 委派概念

- 聲明一個委派指向一個函數        
- 委派所指向的函數 必須跟委派具有相同的簽名     

> 跟建立執行緒很像，都是傳入一個方法    
> `Thread t = new Thread(SayHi);`


## 委派(Delegate)三步驟

1. 宣告委派 `delegate`  
2. 實體化委派型別 並指向方法 (用 `new` 實體化)  
3. 使用`Invoke`方法叫用委派 (`Invoke` 叫用函式)    
(也可以省略Invoke的寫法)   


```c#
public delegate void myDelegate(); //1.宣告委派
myDelegate say = new myDelegate(SayHi); //2.實體化委派型別，並指向一個方法SayHi()
say(); //say.Invoke(); 3.叫用委派
```

## 語法

```c#
public delegate 回傳資料類型 委派名稱([參數群]) //1.宣告委派
委派名稱 委派物件名稱 = 指向的方法名稱; //2.實體化委派型別，並指向一個方法
委派物件名稱.Invoke(); //3.叫用委派, 也可以省略Invoke的寫法
```

### 宣告

```c#
public delegate 回傳資料類型 委派名稱([參數群])
```

```c#
public delegate int PerformCalculation(int x, int y);
```

- 使用`delegate`關鍵字。
- 宣告完委派後，所有的**參數個數**、**資料型態**、**回傳資料類型**都必須要與你所要**指向的方法**完全相同，名稱不用相同。
- 宣告完委派後，可以把它當作一種資料型態，使用時需要**實體化**一個委派物件。
- 此物件被實作後，必須指向一個方法。

### 實體化委派並指向方法
實體化委派型別並指向相對應方法

```c#
委派名稱 委派物件名稱 = new 委派名稱(指向的方法名稱);
委派名稱 委派物件名稱 = 指向的方法名稱; //.net 2.0 之後可以簡化。
```

### 叫用委派

```c#
委派物件名稱.Invoke(); 
委派物件名稱(); //也可以省略Invoke的寫法,
```

調用委託方法(`invoke`或`()`)    

> `invoke()`調用委派方法與 `()`直接調用 相同


```c#
public delegate void Say(); //宣告一個委派 (無參無回傳值)
    
static void Main(string[] args)
{
    Say say = SayHi; //實體化委派 指向SayHi方法
    say(); //調用委派
}

//給委派用的方法
static void SayHi() {
    Console.WriteLine("Hi");
}
```


## 範例

假設現在有兩個顧客要讓宅配公司替他們送貨。      
考慮一種情況，當呼叫送貨方法時，我們想知道客戶是誰？        
也就是想知道 到底是誰呼叫送貨方法時該怎麼辦？       

這時最佳的解決方法就是使用委派：        
我們可以利用委派將把客戶類別中`GetInfo()`當作一個參數「傳過去」`Delive()`方法       
讓`Delive()`方法 可以在它的方法內直接執行`GetInfo()`方法        
這樣一來就可以利用客戶所過來的方法來取得資料了。

### 顧客與宅配公司的關係

```c#
//客戶A
class CustomerA
{
    //由建構方法 呼叫宅配公司類別的送貨方法Delive()
    public CustomerA() {
        //在此呼叫 送貨方法
    }

    string name = "客戶A";
    string goods = "PDA";
    public string GetInfo() {
        return $"{name}, {goods}";
    }
}

//客戶B
class CustomerB
{
    // 與CustomerA類似
}

//宅配公司
class Deliver
{
    //送貨方法
    public void Delive() { }
}
```

### 實作委派

在兩客戶類別的建構函式中 實體化`Deliver物件`(宅配公司)，並將呼叫`Deliver物件`中的`Delive()`送貨方法的同時 將自身的`GetInfo()`方法當參數傳過去，以供 `Delive()`方法內使用

```c#
//宣告委派，可以指向 "無參數 返回值string" 的方法
public delegate string Who();

//客戶A
class CustomerA
{
    //由建構方法 呼叫宅配公司類別的送貨方法Delive()
    public CustomerA()
    {
        //在此呼叫 送貨方法
        Deliver deliver = new Deliver();
        Who w = GetInfo; //實體化委派型別為 w，並指向方法 GetInfo()
        deliver.Delive(w); //將GetInfo()方法 當作參數 w 傳進去
    }

    string name = "客戶A";
    string goods = "PDA";
    public string GetInfo()
    {
        return $"{name}, {goods}";
    }
}

//客戶B
class CustomerB
{
    //由建構方法 呼叫宅配公司類別的送貨方法Delive()
    public CustomerB()
    {
        //在此呼叫 送貨方法
        Deliver deliver = new Deliver();
        Who w = GetInfo; //實體化委派型別為 w，並指向方法 GetInfo()
        deliver.Delive(w); //將GetInfo()方法 當作參數 w 傳進去
    }

    string name = "客戶B";
    string goods = "Laptop";
    public string GetInfo()
    {
        return $"{name}, {goods}";
    }
}

//宅配公司
class Deliver
{
    //送貨方法
    public void Delive(Who customer) 
    {
        //也可以省略Invoke的寫法: 
        Console.WriteLine($"感謝您使用本公司的服務\r\n{customer()}");
    }
}

//當建立兩客戶實體後，就會顯示客戶資訊
CustomerA custA = new CustomerA();
CustomerB custB = new CustomerB();

/* 執行結果:

感謝您使用本公司的服務
客戶A, PDA
感謝您使用本公司的服務
客戶B, Laptop

*/
```




[MSDN -  委派 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/delegates/)      
[MSDN - 使用委派 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/delegates/using-delegates)        
[MSDN - Delegate 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.delegate?view=net-8.0)       
[[C# 筆記] Delegate(委託)、Lambda、Event(事件)  by R](https://riivalin.github.io/posts/2012/01/delegate-lambda-event/)      
[[C# 筆記] 工具人下樓問題(Delegate+Event)  by R](https://riivalin.github.io/posts/2012/01/delegate-tool-man/#notes)    
[[C# 筆記] 多重傳送委派 Multicast-Delegate  by R](https://riivalin.github.io/posts/2010/03/92-multicast-delegate/)      
[runoob - C# 委托（Delegate） ](https://www.runoob.com/csharp/csharp-delegate.html)
Book: Visual C# 2005 建構資訊系統實戰經典教本   
