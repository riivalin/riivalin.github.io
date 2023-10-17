---
layout: post
title: "[閱讀筆記][Design Pattern] Ch2. 商場促銷-策略模式(Strategy)"
date: 2009-03-03 05:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---


> 物件導向的程式設計，並不是類別越多越好，類別的劃分是為了封裝，但分類的基礎是抽象，具有相同屬性和功能之物件的抽象集合才是類別。

# 商場收銀-簡單工廠實現

> 「簡單工廠模式」只是解決物件的建立問題，而且由於工廠本身包括了所有的收費方式，商場可以經常性地更改打折額度和紅利額，每次維護或增加收費方式，都要改動這個工廠，以致程式碼需要重新編譯部署，這真的是很糟糕的處理方式，所以用它不是最好的辦法。面對演算法的時常變動，應該考慮用策略模式(Strategy)。

## 商場收銀軟體
### 需求
做一個商場收銀軟體，營業員根據客戶所購買商品的單價和數量向客戶收費，且商場的商品可以決定打折額度(正常收費、八折、七折、五折、滿300送100、滿200送50……買幾送幾)。

### 思維(draft)
- 打折基本都是一樣的，只要有個初始化參數就可以了。
- 滿幾送幾，需要兩個參數才行。

> 打一折跟打九拆只是形式的不同，抽象分析出來，所有打拆算法都是一樣的，所以「打拆算法」應該是一個類別。

```
現金收費工廠類別 CashFactory
+ CreateCashAccept(): CashSuper

    現金收費抽象類別 CashSuper
    + AcceptCash(): decimal

        正常收費子類別 CashNormal
        + AcceptCash(): decimal
        打折收費子類別 CashRebate
        + AcceptCash(): decimal
        紅利收費子類別 CashReturn
        + AcceptCash(): decimal
```

```c#
//用戶端調用
public static void Main()
{
    //int unitPrice = 10; //單價
    //int quantity = 1; //數量
    //decimal total = unitPrice * quantity;
    
    //利用簡單工廠模式，產生相應的物件
    CashSuper cashSuper = CashFactory.CreateAcceptCash(DiscountType.CashNormal); //正常收費
    total = cashSuper.AcceptCash(100);
    Console.WriteLine(total);
    
    cashSuper = CashFactory.CreateAcceptCash(DiscountType.CashRebate);//打折收費
    total = cashSuper.AcceptCash(100);
    Console.WriteLine(total);
    
    cashSuper = CashFactory.CreateAcceptCash(DiscountType.CashReturn);//紅利收費(滿幾送幾)
    total = cashSuper.AcceptCash(620);
    Console.WriteLine(total);
}

//折扣方式
enum DiscountType {
	CashNormal, //正常收費
	CashRebate, //打折收費
	CashReturn //紅利收費(滿幾送幾)
}

//現金收費工廠類別 
class CashFactory {
    public static CashSuper CreateAcceptCash(DiscountType type) {
        CashSuper cashSuper = null;
        //根據條件返回相應的物件
        switch(type) {
            case DiscountType.CashNormal:
                cashSuper = new CashNormal(); //返回正常收費物件
                break;
            case DiscountType.CashRebate:
                cashSuper = new CashRebate(0.8m); //返回紅利收費(滿幾送幾)物件
                break;
            case DiscountType.CashReturn:
                cashSuper = new CashReturn(300,100);//返回打折收費物件
                break;
        }
        return cashSuper;		
    }
}

//父類別-現金收費抽象類別
abstract class CashSuper {
    //抽象方法，收取現金，參數為原價，返回現價
    public abstract decimal AcceptCash(decimal money); //抽象方法必須被子類別重寫
}

//正常收費-子類別
class CashNormal:CashSuper {
    public override decimal AcceptCash(decimal money) {
        return money;
    }
}
//打折收費-子類別
class CashRebate:CashSuper {
    private decimal moneyRebate = 1;
    public CashRebate(decimal moneyRebate) {
        //打折收費，初始化時，必須要輸入折扣率，8折，就是0.8
        this.moneyRebate = moneyRebate;
    }
    
    public override decimal AcceptCash(decimal money) {
        return money * moneyRebate;
    }
}
//紅利收費子類別 
class CashReturn:CashSuper {
    //紅利收費，初始化時必須要輸入：紅利條件、紅利值
    //比如:滿300送100，則moneyCondition = 300, moneyReturn = 100
    private decimal moneyCondition = 0;
    private decimal moneyReturn = 0;
    public CashReturn(decimal moneyCondition, decimal moneyReturn) {
        this.moneyCondition = moneyCondition;
        this.moneyReturn = moneyReturn;
    }

    public override decimal AcceptCash(decimal money) {
        decimal result = money;
        //如果大於紅利條件，需要減去紅利值
        if(money >= moneyCondition) {
            result = money - Math.Floor(money/moneyCondition) * moneyReturn;
        }
        return result;
    }
}
```

如果我現在需要增加一種商場促銷手段，滿100積分10點，積分到一定程度可以領取獎品，如何做？

「簡單工廠模式」只是解決物件的建立問題，而且由於工廠本身包括了所有的收費方式，商場可以經常性地更改打折額度和紅利額，每次維護或增加收費方式，都要改動這個工廠，以致程式碼需要重新編譯部署，這真的是很糟糕的處理方式，所以用它不是最好的辦法。面對演算法的時常變動，應該考慮用策略模式(Strategy)。

# 策略模式 Strategy

「策略模式 Strategy」它定義演算法家族，分別封裝起來，讓它們之間可以互相替換，此模式讓演算法的變化，不會影響到使用演算法的客戶。

> 策略模式封裝了變化

## 策略模式 Strategy 結構
```
Context 上下文(策略模式)
- Strategy()
+ ContextFunc()

    Strategy 策略
    + Algorithm() 演算法方法

        ConcreteStrategyA 具體策略A
        + Algorithm() 演算法A 實現方法
        ConcreteStrategyB 具體策略B
        + Algorithm() 演算法B 實現方法
        ConcreteStrategyB 具體策略B
        + Algorithm() 演算法C 實現方法

```

- `Strategy`策略類別：定義所有支援的演算法的公共介面
- `ConcreteStrategy`具體策略類別：封裝了具體的演算法或行為，繼承`Strategy`策略類別
- `Context`上下文類別：用一個 `ConcreteStrategy`具體策略類別來配置，維護一個對`Strategy`物件的參考

```c#
//用戶端調用
Context context;

//由於實體化不同的策略，所以最終在調用context.ContextFunc()時，所獲得的結果就不盡相同
context = new Context(new ConcreteStrategyA());
context.ContextFunc();

context = new Context(new ConcreteStrategyB());
context.ContextFunc();

context = new Context(new ConcreteStrategyC());
context.ContextFunc();

//Context 上下文(策略模式)
class Context {
    Strategy strategy;
    //初將化時，傳入具體的策略物件
    public Context(Strategy strategy) {
        this.strategy = strategy
    }
    //根據具體的策略物件，調用其演算法的方法
    public void ContextFunc() {
        strategy.AlgorithmFunc();
    }
}

//Strategy 策略 抽象類別
abstract class Strategy {
    public abstract AlgorithmFunc();
}

//具體演算法A 子類別
class ConcreteStrategyA: Strategy {
    public override void AlgorithmFunc() {
        Console.WriteLine("演算法A 實現方法");
    }
}
//具體演算法B 子類別
class ConcreteStrategyB: Strategy {
    public override void AlgorithmFunc() {
        Console.WriteLine("演算法B 實現方法");
    }
}
//具體演算法C 子類別
class ConcreteStrategyC: Strategy {
    public override void AlgorithmFunc() {
        Console.WriteLine("演算法C 實現方法");
    }
}
```

## 策略模式實現

只要加一個`CashContext`類別，`CashSuper`、`CashNormal`、`CashRebate`、`CashReturn`都不用更改了。    

- 策略模式：具體實現的職責由`CashContext`來承擔
- 父類別：`CashSuper`：策略抽象類別
- 子類別：三個具體策略：`CashNormal`正常收費、`CashRebate`打折收費、`CashReturn`紅利收費(滿幾送幾)，也就是策略模式中說的具體演算法。
- 並將用戶端的判斷移到`CashContext`中

```
CashContext 上下文(策略模式)
- CashSuper
+ GetResult(): decimal

    CashSuper 抽象策略-父類別
    + AcceptCash(): decimal

        CashNormal 正常收費-子類別
        + AcceptCash(): decimal
        CashRebate 打折收費-子類別
        + AcceptCash(): decimal
        CashReturn 紅利收費(滿幾送幾)-子類別
        + AcceptCash(): decimal
```

> 簡單工廠模式：需要讓用戶端認識兩個類別：`CashSuper`、`CashFactory`。      
> 而「策略模式」結合「簡單工廠模式」的用法，用戶端就只需要認識一個類別：`CashContext`，耦合更降底。     
>
> 我們在用戶端實體是`CashContext`物件，調用的是`CashContext`的方法`GetResult()`，這使得具體的收費演算法徹底地與用戶端分離，連演算法的父類別`CashSuper`都不讓用戶端認識了。


```c#
//用戶端調用
public static void Main()
{
    CashContext cc;
    cc = new CashContext(DiscountType.CashNormal);//正常收費
    Console.WriteLine(cc.GetResult(50));

    cc = new CashContext(DiscountType.CashRebate);//打折收費
    Console.WriteLine(cc.GetResult(100));
        
    cc = new CashContext(DiscountType.CashReturn);//紅利收費(滿幾送幾)
    Console.WriteLine(cc.GetResult(300));
}

//折扣方式
enum DiscountType {
    CashNormal, //正常收費
    CashRebate, //打折收費
    CashReturn //紅利收費(滿幾送幾)
}

//收費上下文(策略模式)
//具體實現的職責由CashContext來承擔
class CashContext {
    //宣告CashSuper物件
    CashSuper cs;

    //初始化時，傳入收費類型
    public CashContext(DiscountType type) {
        //依據收費類型，實體化不同的策略
        switch(type) {
            case DiscountType.CashNormal://正常收費
                cs = new CashNormal();
                break;
            case DiscountType.CashRebate://打折收費
                cs = new CashRebate(0.8m);
                break;
            case DiscountType.CashReturn://紅利收費(滿幾送幾)
                cs = new CashReturn(300,100);
                break;
        }
    }
    //取得收取費用的結果
    public decimal GetResult(decimal money) {
        //根據收費策略的不同，獲得計算結果
        return cs.AcceptCash(money);
    }
}

//父類別-現金收費抽象類別
abstract class CashSuper {
    //抽象方法，收取現金，參數為原價，返回現價
    public abstract decimal AcceptCash(decimal money); //抽象方法必須被子類別重寫
}

//正常收費-子類別
class CashNormal:CashSuper {
    public override decimal AcceptCash(decimal money) {
        return money;
    }
}
//打折收費-子類別
class CashRebate:CashSuper {
    private decimal moneyRebate = 1;
    public CashRebate(decimal moneyRebate) {
        //打折收費，初始化時，必須要輸入折扣率，8折，就是0.8
        this.moneyRebate = moneyRebate;
    }
    
    public override decimal AcceptCash(decimal money) {
        return money * moneyRebate;
    }
}
//紅利收費子類別 
class CashReturn:CashSuper {
    //紅利收費，初始化時必須要輸入：紅利條件、紅利值
    //比如:滿300送100，則moneyCondition = 300, moneyReturn = 100
    private decimal moneyCondition = 0;
    private decimal moneyReturn = 0;
    public CashReturn(decimal moneyCondition, decimal moneyReturn) {
        this.moneyCondition = moneyCondition;
        this.moneyReturn = moneyReturn;
    }

    public override decimal AcceptCash(decimal money) {
        decimal result = money;
        //如果大於紅利條件，需要減去紅利值
        if(money >= moneyCondition) {
            result = money - Math.Floor(money/moneyCondition) * moneyReturn;
        }
        return result;
    }
}
```

## 策略模式解析

策略模式是一種定義一系列演算法的方法，從概念上來看，所有這些演算法完成的都是相同的工作，只是實現不同，它可以以相同的方式調用所有的演算法，減少了各種演算法類別與使用演算法類別之間的耦合。      

策略模式的`Strategy`類別層次為`Context`定義了一系列的可供複用的演算法或行為。       

繼承有助於析取出這些演算法中的公共功能。對於打折、紅利或者其他的演算法，其實都是對實際商品收費的一種計算方式，透過繼承，可以得到他們的公共功能。        

公共功能指的是什麼？就是獲得計算費用的結果`GetResult()`，這使得演算法間有了抽象的父類別`CashSuper`。        

> 另外一個，策略模式的優點是：簡化了單元測試，因為每個演算法都有自己的類別，可以透過自己的介面單獨測試。   
> 每個演算法可保證它沒有錯誤，修改其中任一個時，也不會影響其他的演算法。

還有，在最開始程式設計時，為了判斷用哪一個演算法計算，只好在用戶端的程式碼中使用`switch`條件分支，這也是正常的。因為當不同的行為堆砌在一個類別中時，就很難避免使用條件敘述來選擇合適的行為。將這些行為封裝在一個個獨立的`Strategy`類別中，可以在使用這些行為的類別中消除條件敘述。      

> 在商場收銀系統的例子而言，在用戶端的程式碼中就消除了條件敘述，避免了大量的判斷。  
> 這是非常重要的進展，一句話總結這個優點「策略模式封裝了變化」。        

但，還有不足，因為在`CashContext`裡還是用到了`switch`，也就是說，如果我們需要增加一種演算法，比如「買200送50」，你就必須要更改`CashContext`中的`switch`程式碼，還是很不方便，任何需求的變更都是需要成本的。     

成本的高低還是有差異的，花同樣的代價獲得最大的收益，或者說，做同樣的事花最小的代價。面對同樣的需求，當然是改動越小越好。    

這個辦法就是用到了「反射技術」。
