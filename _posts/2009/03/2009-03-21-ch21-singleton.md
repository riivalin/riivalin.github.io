---
layout: post
title: "[閱讀筆記][Design Pattern] Ch21.獨體模式(Singleton)"
date: 2009-03-21 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 獨體模式(Singleton)

獨體模式(Singleton)，保證一個類別僅有一個實體，並提供一個存取它的全域訪問點。       

通常我們可以讓一個總體變數使得一個物件被存取，但它不能防止你實體化多個物件。一個最好的方法就是，讓類別自身負責保存它唯一的實體。這個類別可以保證沒有其他實體可以被建立，並且它可以提供一個存取該實體的方法。


> 實體化其實就是`new`的過程，如果不對建構式做改動的話，是不可能阻止他人不去用`new`的。        
> 所以我們完全可以直接把這個類別的建構函式改成私有的(`private`)，所有的類別都有建構函式，不寫程式則系統預定產生空的建構函式，若有顯示定義的建構函式，預定的建構式就會失效。       

## 這個類別該如何實體化？
私有的方法外界不能存取，我們的目的是「讓這個類別只能實體化一次」。      

對於外部程式，不能用`new`來實體化它，但是我們可以再寫一個`public`方法，叫做`GetInstance()`，這個方法的目的就是返回一個類別實體，而此方法中，去做是否有實體化判斷。如果沒有實體化過，就調用`private`的建構式`new`出這個實體，之所以它可以調用是因為它們在同一個類別中，`private`方法可以被調用。

## 結構圖
`Singleton`類別，定義一個`GetInstance`操作，允許客戶使用它的唯一實例。`GetInstance()`是一個靜態方法，主要負責建立自己的唯一實例。

```
Singleton
- instance: Singleton
- Singleton()
+ GetInstance()
```

## 程式碼

```c#
//Singleton類別，定義一個GetInstance操作，允許客戶使用它的唯一實例。GetInstance()是一個靜態方法，主要負責建立自己的唯一實例。
class Singleton {
    //宣告一個靜態的類別變數
    private static Singleton instance;

    //建構式讓其private，這就堵死了外界利用new 建立此類別實體的可能
    private Singleton() { }

    //此方法是獲得本類別實體的唯一全域存取點
    public static Singleton GetInstance() {
        //如果實體不存在，則new 一個新實體，否則返回已有的實體
        if(instance == null) {
            instance = new Singleton();
        }
        return instance;
    }
}

//用戶端
public static void Main()
{
    Singleton s1= Singleton.GetInstance();
    Singleton s2= Singleton.GetInstance();

    //比較兩次實體化後，物件結果是實體相同
    if(s1==s2) {
        Console.WriteLine("兩個物件是相同的實體。");
    }
}
```

## WinForm

```c#
//工具箱FormToolbox是否實體化，由工具箱自己來判斷
public partial class FormToolbox: Form {
    //宣告一個靜態的類別變數
    public static FormToolbox instance = null;

    //改為私有的建構函式，外界不能直接new來實體化它
    private FormToolbox() { 
        InitializeComponent();
    }

    //得到類別實體的方法，返回值就是本身類別物件，也是靜態的
    public static FormToolbox GetInstance() {
        //當內部的FormToolbox是null或是被Disposed，則new 它
        if(instance == null || instance.IsDisposed) {
            instance = new FormToolbox();
            instance.MdiParent = Form1.ActiveForm;
        }
        return instance;
    }
}

//用戶端不再考慮是否需要去實體化的問題，而把責任都給了應該負責的類別去處理。
private void ToolStripMenuItemToolbox_Click(object sender, EventArgs e) {
    FormToolbox.GetInstance().Show();
}
private void ToolStripButtton1_Click(object sender, EventArgs e) {
    FormToolbox.GetInstance().Show();
}
```

## 好處？
獨體模式除了保證唯一的實體外，還有，比如，獨體模式因為`Singleton`類別封裝它的唯一實體，這樣它可以嚴格地控制客戶怎樣存取它以及何時存取它。簡單地說就是對唯一實體的受控存取。


# 多執行緒時的獨體

多執行緒的程式中，多個執行緒同時，注意是同時存取`Singleton`類別，調用`GetInstance()`方法，會有可能造成建立多個實體的。      

該怎麼辦？可以給行程一個鎖(`lock`)來處理。      

`lock`是確保當一個執行緒位於程式碼的臨界區時，另一個執行緒不進入臨界區。如果其他執行緒試圖進入鎖定的程式碼，則它將一直等待(即被阻止)，直到該物件被釋放。


## 鎖定 lock

`lock(syncRoot){ }`這段程式碼使得物件實體由最先進入的那個執行緒建立，以後的執行緒在進入時不會再去建立物件實體了。由於有了`lock`，就保證了多執行緒環境下的同時存取也不會造成多個實體的產生。

```c#
class Singleton {
    //宣告一個靜態的類別變數
    private static Singleton instance;

    //程式執行時建立一個靜態唯讀的行程輔助物件
    private static readonly object syncRoot = new object();

    //建構式改為私有，讓外界無法 new實體
    private Singleton() { }

    //負責建立自己的唯一實體
    public static Singleton GetInstance() {
        //在同一時刻加了鎖的那部分程式，只有一個執行緒可以進入
        lock(syncRoot) {
            //如果實體不存在，就new 一個新的實體
            if(instance == null) {
                instance = new Singleton();
            }
        }
        return instance;
    }
}
```

### 為什麼不直接`lock(instance)`，而是再建立一個`syncRoot`來`lock`呢？
加鎖時，`instance`實體有沒有被建立過實體都還不知道，怎麼對它加鎖呢？        

但，這樣就得每次調用`GetInstance()`方法時都需要`lock`，這種做法是會影響性能的。     


## 雙重鎖定 Double-Check Locking

```c#
class Singleton {
    //宣告一個靜態的類別變數
    private static Singleton instance;

    //程式執行時建立一個靜態唯讀的行程輔助物件
    private static readonly object syncRoot = new object();

    //建構式改為私有，讓外界無法 new實體
    private Singleton() { }

    //負責建立自己的唯一實體
    public static Singleton GetInstance() {
        //先判斷實體是否存在，不存在再加鎖lock處理
        if(instance == null) {
            //在同一時刻加了鎖的那部分程式，只有一個執行緒可以進入
            lock(syncRoot) {
                //如果實體不存在，就new 一個新的實體
                if(instance == null) {
                    instance = new Singleton();
                }
            }
        }
        return instance;
    }
}
```

### 為什麼外面已經判斷了`instance`實體是否存在，為什麼在`lock`裡面還需要做一次`instance`實體是否存在的判斷？   

對於`instance`存在的情況，就直接返回，這沒有問題。當`instance`為`null`並且同時有兩個執行緒調用`GetInstance()`方法時，它們都可以透過第一重`instance==null`的判斷。然後由於`lock`機制，這兩個執行緒則只有一個進入，另一個在外排隊等候，必須要其中一個進入並出來後，另一個才能進入。       

而此時如果沒有了第二重的`instance`是否為`null`的判斷，則第一個執行緒建立了實體，而第二個執行緒還是可以繼續再建立新的實體，這就沒有達到獨體的目的。      

# 靜態初始化

類別加上`sealed`防止被繼承。        

```c#
//加上 sealed，阻止被繼承，因為繼承後可能會增加實體
sealed class Singleton {
    //在第一次參考類別的任何成員時建立實體。CLR負責處理變數初化
    public static readonly Singleton instance = new Singleton();
    private Singleton() { }

    public static Singleton GetInstance() {
        return instance;
    }
}
```

- 「靜態初始化」的方式，是在自己被載入時，就將自己實體化。它是類別一載入就實體化的物件，所以要提前佔用系統資源。
- 原先的獨體模式處理方式，是要在第一次被參考時，才會將自己實體化。



- [[MSDN] lock 陳述式-確保共用資源的獨佔存取權](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/statements/lock)
- [[C# 筆記][WinForm] 單例模式](https://riivalin.github.io/posts/2011/03/singleton/)