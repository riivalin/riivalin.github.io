---
layout: post
title: "[C# 筆記] 介面與實作"
date: 2021-05-11 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,介面,interface,多型]
---

# 什麼是介面？

所謂的「介面(`Interface`)」如同「契約」一般，於介面中所定義的屬性、方法、和事件等，就是契約的大綱。     

若要得知這份契約的詳盡內容，則需要透過類別來繼承介面才能完成實作(`Implementation`)。        

而介面本身是不提供實作的。      
也就是說，你可以在介面宣告一個個方法名稱`MyMethod()`，但是此方法的**執行內容**不能於介面中進行程式碼編寫。      

```c#
public interface IFlyable {
 //函數前面不能有修飾符
 //在介面當中，所有成員默認都是public，也不讓你加public
 void Fly();
}
```

介面的性質就像是一個「空殼」，需透過類別繼承介面才能將「果實」放入「空殼」中。      


> 透過介面可以達到多型效果      
> - 「介面」存在的意義就是為了「多型」。    
> - 「多型」就是讓一個物件能夠表現出多種的狀態(類型)。  


## 需要注意的地方

- 介面就像「類別」一樣，可以定義屬性、方法、事件，但不提供「實作」。    
- 無法對介面進行**實體化**動作。(介面不能被`new`)   
- 「類別」和「結構」均能繼承一個以上的介面，而介面本身亦可繼承介面。    
- 宣告介面時，其介面內容不允許明確定義「存取修飾詞」，例如：`public`,`private`等等，而宣告成`static`亦不行。        
- 介面類似`abstract class`(抽象類別)一樣，一旦繼承介面，除了抽象型別成員之外，其他一律必須完整實作。    
- 透過介面的設計與實作，可達成「多型」的效果。


## 語法
### 宣告

```c#
interface 介面名稱
{
    [資料型別] 屬性名稱 { get; set; } //屬性
    [資料型別|void] 方法名稱(); //方法
}
```

> 介面只能宣告，不能實作，且只能為公開(`public`)。      
> 預設就是公開，所以不用特別加上`public`。     

### 實作

在完成介面宣告之後，接下來就是要實作它。        
而實作的目的在於：要實際完成介面宣告所定義的屬性或方法。        
(簡言之，「實作」就是要真正去撰寫屬性和方法運作的程式碼。)

```c#
class 類別名稱 : 介面名稱
{
    //屬性
    private [資料型別] 欄位名稱 = 預設值; 
    public [資料型別] [介面名稱].屬性名稱 
    { 
        get { return 欄位名稱; } 
        set { 欄位名稱 = value; }
    }

    //方法
    [資料型別|void] [介面名稱].方法名稱()
    {
        //撰寫方法所要執每的程式碼
    }
}
```

> 實現介面的函數有兩種方式：
>       
> 1. 實作介面
> 2. 明確實作所有成員：解決方法重名的問題

## 範例

透過**介面**來達到**多型**效果：        
- 先宣告一個 `ICar`介面。
- 建立三個汽車類別：`SportsCar`、`Convertibles`、`RV`，均繼承`ICar`介面。

當我們需要使用哪一種車型時，只要**實體化**該類別即可，由此來達成「多型」的效果。

### 介面 & 類別 

```c#
//宣告一個汽車介面
interface ICar
{
    string Name { get; set; } //設定車款名稱的屬性
    int GetHP(bool isTurbo); //取得汽車馬力數的方法
}

//跑車類別
class SportsCar : ICar
{
    string ICar.Name { get; set; } = "Audi R8";

    int ICar.GetHP(bool isTurbo)
    {
        return (isTurbo) ? 200 : 140;
    }
}

//敞蓬車類別
class Convertibles : ICar
{
    string ICar.Name { get; set; } = "MX-5";

    int ICar.GetHP(bool isTurbo)
    {
        return (isTurbo) ? 250 : 110;
    }
}

//休旅車
class RV : ICar
{
    string ICar.Name { get; set; } = "Tuson";

    int ICar.GetHP(bool isTurbo)
    {
        return (isTurbo) ? 200 : 140;
    }
}
```

###  實作介面 & 執行結果

使用 明確實作介面

```c#
Console.WriteLine("請選擇車種：1:跑車 2:敞蓬車 3:休旅車");
int input = Convert.ToInt32(Console.ReadLine()); //接收使用者輸入

//存放顯示訊息的 相關變數
string name = "";
int hp = 0;
string type = "";

//依據輸入的數字 取得該汽車的資訊
switch (input)
{
    case 1: //跑車
        ICar audi = new SportsCar(); //子類 賦值給 父類
        name = audi.Name;
        hp = audi.GetHP(true);
        type = "跑車";
        break;
    case 2: //敞蓬車
        ICar mx5 = new Convertibles();
        name = mx5.Name;
        hp = mx5.GetHP(false);
        type = "敞蓬車";
        break;
    case 3: //休旅車
        ICar tucson = new RV();
        name = tucson.Name;
        hp = tucson.GetHP(true);
        type = "休旅車";
        break;
    default:
        Console.WriteLine($"請輸入正確的數值{Environment.NewLine}"); 
        continue;
}
Console.WriteLine($"汽車資訊：\r\n車名：{name}\r\n馬力：{hp}\r\n汽車種類：{type}");
```

執行結果：

```
請選擇車種：1:跑車 2:敞蓬車 3:休旅車
1
汽車資訊：
車名：Audi R8
馬力：200
汽車種類：跑車
```

# 里氏轉換(LSP)

- 子類可以賦值給父類
- 如果父類中裝的是子類物件，那麼可以將這個父類強轉為子類物件。


## 子類可以賦值給父類

```c#
//寫法一：
Student s = new Student();
Person p = s;

//寫法二：
Person s = new Student();
```

`string.Join` 也是 子類 賦值給父類的

```c#
string str = string.Join("|", new string[] { "1", "2", "3", "4", "5" });
```

## 如果父類中裝的是子類物件，那麼可以將這個父類強轉為子類物件。

```c#
Person p = new Student(); //父類中 裝的是 子類
Student s = (Student)p; //父類 強轉為 子類 物件
//Teacher t = (Teacher)p; //執行會報錯，因為父類裝的是student
```


[MSDN - 介面 - 定義多個類型的行為](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/types/interfaces)        
[MSDN - 明確介面實作 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation)   
[MSDN - 明確實作介面成員](https://learn.microsoft.com/zh-tw/previous-versions/dotnet/netframework-4.0/ms229034(v=vs.100)?redirectedfrom=MSDN)     
[[C# 筆記][多型] Interface 介面簡介  by R](https://riivalin.github.io/posts/2011/01/interface/)     
[[C# 筆記][多型] Interface 介面複習  by R](https://riivalin.github.io/posts/2011/02/interface-review/#interface-介面)       
[實現多態(多型)的三個方法 ([C# 筆記] .Net基礎-複習-R)  by R](https://riivalin.github.io/posts/2011/02/r-cshap-notes-3/#6多態多型)     
[[C# 筆記] 里氏轉換(LSP)](https://riivalin.github.io/posts/2011/01/lsp/)        
[[C# 筆記] 實作介面 vs 明確實作介面   by R](https://riivalin.github.io/posts/2021/05/cs-interface/)     
[[C# 筆記][多型] Interface 介面複習  by R](https://riivalin.github.io/posts/2011/02/interface-review/#interface-介面)
Book: Visual C# 2005 建構資訊系統實戰經典教本       