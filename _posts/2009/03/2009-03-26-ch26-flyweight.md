---
layout: post
title: "[閱讀筆記][Design Pattern] Ch26.享元模式(Flyweight)"
date: 2009-03-26 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 享元模式(Flyweight)

享元模式(Flyweight)，運用共用技術有效地支援大量細粒度的物件。

## 結構
- `FlyweightFactory` 一個Flyweight工廠，用來建立並管理Flyweight物件。主要是用來確保合理地共用Flyweight，當用戶請求一個Flyweight時，FlyweightFactory物件提供一個已建立的實例或者建立一個(如果不存在的話)。
- `Flyweight` 所有具體Flyweight類別的超類別或介面，透過這個介面，Flyweight可以接受並作用於外部狀態。
- `ConcreteFlyweight` 繼承Flyweight超類別或實現Flyweight介面，並為內部狀態增加儲存空間。
- `UnsharedConcreteFlyweight` 指那些不需要共用的Flyweight子類別。因為Flyweight介面共用成為可能，但它並不強制共用。

```
Client
    
    FlyweightFactory 一個Flyweight工廠，用來建立並管理Flyweight物件。主要是用來確保合理地共用Flyweight，當用戶請求一個Flyweight時，FlyweightFactory物件提供一個已建立的實例或者建立一個(如果不存在的話)
    - flyweights
    + GetFlyweight(in key: int): Flyweight

        Flyweight 所有具體Flyweight類別的超類別或介面，透過這個介面，Flyweight可以接受並作用於外部狀態
        + Operataion(in extrinsicsstate: int)

            ConcreteFlyweight 繼承Flyweight超類別或實現Flyweight介面，並為內部狀態增加儲存空間
            + Operataion(in extrinsicsstate: int)

            UnsharedConcreteFlyweight 指那些不需要共用的Flyweight子類別。因為Flyweight介面共用成為可能，但它並不強制共用
            + Operataion(in extrinsicsstate: int)
```

## 程式碼
### Flyweight
`Flyweight` 所有具體Flyweight類別的超類別或介面，透過這個介面，Flyweight可以接受並作用於外部狀態。

```c#
abstract class Flyweight {
    public abstract void Operation(int extrinsicState);
}
```


### ConcreteFlyweight(共用的具體Flyweight)
`ConcreteFlyweight` 繼承Flyweight超類別或實現Flyweight介面，並為內部狀態增加儲存空間。

```c#
//共用的具體Flyweight
class ConcreteFlyweight: Flyweight {
    public override void Operation(int extrinsicState) {
        Console.WriteLine($"具體Flyweight：{extrinsicState}");
    }
}
```

### UnsharedConcreteFlyweight(不共用的具體Flyweight)
`UnsharedConcreteFlyweight` 指那些不需要共用的Flyweight子類別。因為Flyweight介面共用成為可能，但它並不強制共用。

```c#
//不共用的具體Flyweight
class UnsharedConcreteFlyweight: Flyweight {
    public override void Operation(int extrinsicState) {
        Console.WriteLine($"不共用的具體Flyweight：{extrinsicState}");
    }
}
```

### FlyweightFactory(享元工廠)
`FlyweightFactory` 一個Flyweight工廠，用來建立並管理Flyweight物件。主要是用來確保合理地共用Flyweight，當用戶請求一個Flyweight時，FlyweightFactory物件提供一個已建立的實例或者建立一個(如果不存在的話)。

```c#
//Flyweight工廠
class FlyweightFactory {
    private Hashtable flyweights = new Hashtable();

    public FlyweightFactory() {
        //初始化工廠時，先產生三個實體
        flyweights.Add("X", new ConcreteFlyweight());
        flyweights.Add("Y", new ConcreteFlyweight());
        flyweights.Add("Z", new ConcreteFlyweight());
    }

    public Flyweight GetFlyweight(string key) {
        //根據用戶端請求，獲得已產生的實體
        return ((Flyweight)flyweights[key]);
    }
}
```

### 用戶端調用

```c#
public static void Main()
{
    int extrinsicState = 22; //程式碼外部狀態

    FlyweightFactory f = new FlyweightFactory();

    Flyweight fx = f.GetFlyweight("X");
    fx.Operation(--extrinsicState);

    Flyweight fy = f.GetFlyweight("Y");
    fy.Operation(--extrinsicState);

    Flyweight fz = f.GetFlyweight("Z");
    fz.Operation(--extrinsicState);

    Flyweight uf = new UnsharedConcreteFlyweight();
    uf.Operation(--extrinsicState);
}

/* 執行結果:

具體Flyweight：21
具體Flyweight：20
具體Flyweight：19
不共用的具體Flyweight：18

*/
```

- `FlyweightFactory`不一定要事先產生物件實體，完全可以初始化時什麼也不做，到需要時，再去判斷物件是否為`null`來決定是否實體化。
- `UnsharedConcreteFlyweight`的存在：儘管我們大部分時間都需要共用物件來降低記憶體的損耗，但個別時候也有可能不需要共用的。那麼此時的`UnsharedConcreteFlyweight`子類別就有存在的必要了，它可以解決那些不需要共用物件的問題。


# v1.0 尚未網站共用程式碼

(TODO：但這樣寫，只表現了它們共用的部分，沒有表現物件間的不同。)

- 類似商家的客戶，但需求也不太一樣，要求也就是：資訊發佈、產品展示、部落格留言、論壇等功能。
- 如果有100家企業，每個網站租用一個空間、用100個資料庫、類似的程式碼複製100遍去做？
- 如果有Bug或是新的需求改動，維護量就太可怕了。

如果是每個網站一個實體，程式碼應該是這樣

```c#
//網站類別
class WebSite {
    string name;
    public WebSite(stirng name) {
        this.name = name;
    }

    public void Use() {
        Console.WriteLine($"網站分類：{name}");
    }
}
//用戶端
WebSite w1 = new WebSite("產品展示");
w1.Use();

WebSite w2 = new WebSite("產品展示");
w2.Use();

WebSite w3 = new WebSite("部落格");
w3.Use();

WebSite w4 = new WebSite("部落格");
w4.Use();
```

本質上都是一樣的程式碼，如果網站增多，實體也就隨著增多，這對伺服器的資源而言是嚴重的浪費。      

> 利用用戶`ID`號的不同，來區分不同的用戶，具體資料和範本可以不同，但程式碼核心和資料庫卻是共用的。

# v2.0 網站共用程式碼

網站應該有一個抽象類別和一個具體網站類別，然後透過網站工廠來產生物件。

## 網站抽象類別

```c#
abstract class WebSite {
    public abstract void Use();
}
```

## 具體網站類別

```c#
class ConcreteWebSite: WebSite {
    private string name;
    public ConcreteWebSite(string name) {
        this.name = name;
    }

    public override void Use() {
        Console.WriteLine($"網站分類：{name}");
    }
}
```

## 網站工廠

```c#
using System.Collections;
class WebSiteFactory {
    private Hashtable flyweights = new Hashtable();

    //獲得網站分類
    public WebSite GetWebSiteCategory(string key) {
        //判斷是否存在這個物件，存在就直接返回，不存在則實體化它再返回
       	if(!flyweights.ContainsKey(key)) {
			flyweights.Add(key, new ConcreteWebSite(key));
		}
        //根據用戶端請求，獲得已產生的實體
        return ((WebSite)flyweights[key]);
    }

    //獲得網站分類總數
    public int GetWebSiteCount() {
        return flyweights.Count;
    }
}
```

## 用戶端調用

```c#
public static void Main()
{
    WebSiteFactory f = new WebSiteFactory();

    //實體化「產品展示」的物件
    WebSite fx = f.GetWebSiteCategory("產品展示");
    fx.Use();

    //共用上方產生的物件，不再實體化
    WebSite fy = f.GetWebSiteCategory("產品展示");
    fy.Use();

    WebSite fz = f.GetWebSiteCategory("產品展示");
    fz.Use();

    WebSite f1 = f.GetWebSiteCategory("部落格");
    f1.Use();

    WebSite f2 = f.GetWebSiteCategory("部落格");
    f2.Use();

    WebSite f3 = f.GetWebSiteCategory("部落格");
    f3.Use();

    Console.WriteLine($"網站分類總數為：{f.GetWebSiteCount()}");
}

/* 顯示結果:

網站分類：產品展示
網站分類：產品展示
網站分類：產品展示
網站分類：部落格
網站分類：部落格
網站分類：部落格
網站分類總數為：2
*/
```

這樣寫算是基本實現了享元模式的共用物件的目的，也就是說，不管建幾個網站，只要是「產品展示」，都是一樣的，只要是「部落格」也是完全相同的。      

但是這樣是有個問題的，你給企業建的網站不是一家企業的，他們的資料不會相同，所以至少他們都應該有不同的帳號。這樣寫，只表現了它們共用的部分，沒有表現物件間的不同。


# v3.0 網站共用程式碼(內部外部狀態)

## 內部狀態 vs 外部狀態
- 內部狀態：在享元物件內部並且不會隨環境改變而改變的共用部分，可以稱為是享元物件的內部狀態。
- 外部狀態：而隨環境改變而改變的、不可以共用的狀態就是外部狀態。

> 享元模式可以避免大量非常相似類別的消耗

在程式設計中，有時需要產生大量細粒度的類別實體來表示資料。如果能發現這些實體除了幾個參數外基本上都是相同的，有時就能夠受大幅度地減少需要實體化類別的數量。如果能把那些參數移到類別實體的外面，在方法調用時將它們傳遞進來，就可以透過共用大幅度地減少單個實體的數目。        

也就是說，享元模式`Flyweight`執行時所需的狀態是有內部的，也有可能有外部的。     

- 內部狀態儲存於`ConcreteFlyweight`物件之中
- 而外部物件則應該考慮由用戶端物件儲存或計算，當調用`Flyweight`物件的操作時，將該狀態傳遞給它。

> 客戶的帳號是外部狀態，應該由專門的物件來處理


## 結構

- 用戶類別：用於網站的客戶帳號，是「網站」類別的外部狀態

```
用戶
+ 帳號: string

    網站
    + 使用(in user: 用戶)

        具體網站
        + 使用(in user: 用戶)

    網站工廠
    + 取得網站分類(in key:string): 網站
    + 取得網站分類總數(): int
```

## 用戶類別
用戶類別：用於網站的客戶帳號，是「網站」類別的外部狀態

```c#
//用戶
public class User {
    public string Name { get; private set;}
    
    public User(string name) {
        this.Name = name;
    }
}
```

## 網站抽象類別

```c#
abstract class WebSite {
    //Use方法，需要傳遞用戶物件
    public abstract void Use(User user);
}
```

## 具體網站類別

```c#
class ConcreteWebSite: WebSite {
    private string name;
    public ConcreteWebSite(string name) {
        this.name = name;
    }
    //實現「Use方法」
    public override void Use(User user) {
        Console.WriteLine($"網站分類：{name} | 用戶：{user.Name}");
    }
}
```

## 網站工廠類別

```c#
//網站工廠
using System.Collections;
class WebSiteFactory {
    private Hashtable flyweights = new Hashtable();

    //取得網站分類
    public WebSite GetWebSiteCategory(string key) {
        //判斷是否存在這個物件，存在就直接返回，不存在則實體化它再返回
        if(!flyweights.ContainsKey(key)) {
            flyweights.Add(key, new ConcreteWebSite(key));
        }
        //根據用戶端請求，獲得已產生的實體
        return (WebSite)flyweights[key];
    }

    //取得網站分類總數
    public int GetWebSiteCount() {
        return flyweights.Count;
    }
}
```

## 用戶端調用

```c#
public static void Main()
{
    WebSiteFactory f = new WebSiteFactory();

    //實體化「產品展示」的物件
    WebSite w1 = f.GetWebSiteCategory("產品展示");
    w1.Use(new User("Ken"));

    //共用上方產生的物件，不再實體化
    WebSite w2 = f.GetWebSiteCategory("產品展示");
    w2.Use(new User("Rii"));

    WebSite w3 = f.GetWebSiteCategory("產品展示");
    w3.Use(new User("Qoo"));

    WebSite w4 = f.GetWebSiteCategory("部落格");
    w4.Use(new User("Wei"));

    WebSite w5 = f.GetWebSiteCategory("部落格");
    w5.Use(new User("Nini"));

    WebSite w6 = f.GetWebSiteCategory("部落格");
    w6.Use(new User("Goo"));

    Console.WriteLine($"得到網站分類總數為 {f.GetWebSiteCount()}");	
}

/* 執行結果:

網站分類：產品展示 | 用戶：Ken
網站分類：產品展示 | 用戶：Rii
網站分類：產品展示 | 用戶：Qoo
網站分類：部落格 | 用戶：Wei
網站分類：部落格 | 用戶：Nini
網站分類：部落格 | 用戶：Goo
得到網站分類總數為 2
*/
```

結果顯示，儘管給六個不同用戶使用網站，但實際上只有兩個網站實體。


# 享元模式應用

- 這些物件造成了大量記憶體消耗時，就應該考慮使用。
- 物件的大多數狀態是外部狀態，如果刪除物件的外部狀態，那麼可以用相對較少的共用物取代很多組物件，此時可以考慮使用享元模式。

## 效果

因為用了享元模式，所以有了共用物件，實體總數就大大減，如果共用的物越多，可以省下的記憶體使用量也就越多，節約量隨著共用狀態的增多而增大。

## .NET中的String

事實上在.NET中，字串`String`就是運用了`Flyweight`模式。     

`Object.ReferenceEquals(object objA, object objB)`方法是用來確定objA與objB是否相同的實體，返回值為bool值：

```c#
string s1 = "Rii";
string s2 = "Rii";
Console.WriteLine(Object.ReferenceEquals(s1,s2)); //true
```
結果返回值為true，這兩個字串是相同實體。        

試想一下，如果每次建立字串物件時，都需要建立一個新的字串物件的話，記憶體的消耗會很大。所以如果第一次建立了字串物件 s1，下次再建立相同的字串 s2時，只是把它的參到指向了「Rii」，這樣就實現了「Rii」在記憶體中的共用。


## 遊戲

比如遊戲開發中，五子棋、圍棋、跳棋等，它們都有大量的棋子物件。      

五子棋、圍棋只有黑白兩色，顏色就是棋子的內部狀態，而各棋子之間的差別主要就是位置的不同，所以方位座標是棋子的外部狀態。

> 像圍棋，一盤棋理論上有361個空位可以放棋子，如果用一般物件導向方式程式設計，每盤棋都有可能有兩三百個棋子物件產生，一台伺服器就很難支援太多玩家玩圍棋遊戲了，畢竟記憶體空間還是有限的。     
> 如果用了享元模式來處理棋子，那麼棋子物件可以減少到只有兩個實體。


## 思考

在某些情況下，物件的數量可能會太多，從而導致了執行時的資源與性能損耗。那麼我們如何去避免大量粒度的物件，同時又不影響客戶程式，是一個值得去思考的問題。      

享元模式，可以運用共用技術有效地支援大量細粒度的物件。不過，使用享元模式需要維護一個記錄了系統已有的所有享元的列表，而這也需要耗費資源。        

另外，享元模式使得系統更加複雜。為了使物件可以共用，需要將一些狀態外部化，這會讓程式的邏輯複雜化。因此，應當在有足夠多物件實體可供共用時得使用享元模式。

> 比如，給人家做網站，如果只有兩三個人的個人部落格，其實是沒有必要考慮太多的。但如果是要開發一個可供多人註冊的部落格網站，那麼用共用程式碼的方式是一個非常好的選擇。