---
layout: post
title: "[閱讀筆記][Design Pattern] Ch27.解譯器模式(Interpreter)"
date: 2009-03-27 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---


# 解譯器模式(Interpreter)

解譯器模式(Interpreter)，給定一個語言，定義它的文法的一種表示，並定義一個解譯器，這個解譯器使用該表示來解譯語言中的句子。     

比如，我們常常會在字串中搜尋相符的字元或判斷一個字串是否符合我們規定的格式，此時一般我們會用「正則運算式」技術。    

> 因為比對字元的需求在軟體的很多地方都會使用，而且行為之間都非常類似，過去的做法是針對特定的需求，編寫特定的函數，比如判斷Email、比對電話號碼等等，與其為每一個特定需求都寫一個演算法函數，不如使用一種通用的搜尋演算法來解譯執行一個正則運算式，該正則運算式定義了待比對字串的集合。

而，解譯器模式(Interpreter)，「正則運算式」就是它的一種應用，解譯器為正則運算式定義了一個文法，如何表示一個特定的正則運算式，以及如何解譯這個正則運算式。

## 結構
- `Context` 包含解譯器之外的一些全域資訊
- `AbstractExpression` 抽象運算式，宣告一個抽象的解譯操作，這個介面為語法樹中所有的節點所共用
- `TerminalExpression` 終結符運算式，實現與文法中的終結符相關聯的解釋操作。實現抽象運算式中所要求的介面，主要是一個`Interpret()`方法。文法中每一個終結符都有一個具體終結運算式與之相對應。
- `NonterminalExpression` 非終結符運算式，為文法中的非終結符實現解釋操作。對文法中每一條規則R1, R2...Rn都需要一個具體的非終結符運算式類別。透過實現抽象運算式的`Interpret()`方法解譯操作。解譯操作以遞迴方式調用上面所提到的代表R1, R2...Rn中各個符號的實體變數。

```
Client
    Context 包含解譯器之外的一些全域資訊

    AbstractExpression 抽象運算式，宣告一個抽象的解譯操作，這個介面為語法樹中所有的節點所共用
    + Interpret(in context: Context)

        TerminalExpression 終結符運算式，實現與文法中的終結符相關聯的解譯操作
        + Interpret(in context: Context)

        NonterminalExpression 非終結符運算式，為文法中的非終結符實現解譯操作。對文法中每一條規則R1, R2...Rn都需要一個具體的非終結符運算式類別
        + Interpret(in context: Context)
```

## 程式碼
### AbstractExpression抽象運算式
`AbstractExpression` 抽象運算式，宣告一個抽象的解譯操作，這個介面為語法樹中所有的節點所共用。

```c#
//抽象運算式
abstract class AbstractExpression {
    //抽象解譯方法
    public abstract void Interpret(Context context);
}
```


### TerminalExpression終結符運算式
`TerminalExpression` 終結符運算式，實現與文法中的終結符相關聯的解釋操作。實現抽象運算式中所要求的介面，主要是一個`Interpret()`方法。文法中每一個終結符都有一個具體終結運算式與之相對應。

```c#
//終結符運算式
class TerminalExpression: AbstractExpression {
    public override void Interpret(Context context) {
        Console.WriteLine("終端解譯器");
    }
}
```

### NonterminalExpression
`NonterminalExpression` 非終結符運算式，為文法中的非終結符實現解釋操作。對文法中每一條規則R1, R2...Rn都需要一個具體的非終結符運算式類別。透過實現抽象運算式的`Interpret()`方法解譯操作。解譯操作以遞迴方式調用上面所提到的代表R1, R2...Rn中各個符號的實體變數。

```c#
//非終結符運算式
class NonterminalExpression: AbstractExpression {
    public override void Interpret(Context context) {
        Console.WriteLine("非終端解譯器");
    }
}
```

### Context
`Context` 包含解譯器之外的一些全域資訊。

```c#
class Context {
    public string Input { get; set;}
    public string Output { get; set;}
}
```

### 用戶端調用
構建表示該文法定義的語言中，一個特定之句子的抽象語法樹。調用釋譯操作。

```c#
using System.Collections.Generic;
public static void Main()
{
    Context context = new Context();

    IList<AbstractExpression> list = new List<AbstractExpression>();
    list.Add(new TerminalExpression());
    list.Add(new NonterminalExpression());
    list.Add(new TerminalExpression());
    list.Add(new TerminalExpression());

    foreach(AbstractExpression exp in list) {
        exp.Interpret(context);
    }
}

/* 執行結果:

終端解譯器
非終端解譯器
終端解譯器
終端解譯器
*/
```

# 解譯器模式好處
## 何時用

通常當一個語言需要解譯執行，並且你可將語言中的句子表示為一個抽象語法樹時，可使用解譯器模式。

> 用解譯器模式(Interpreter)，就如同開發了一個程設計語言或腳本給自己或別人用。       
> 解譯模式就是用「迷你語言」來表現程式要解決的問題，以迷你語言寫成「迷你程式」來表現具體的問題。

## 好處

用了解譯器模式，就意味著可以很容易地改變和擴展文法，因為該模式使用類別來表示文法規則，你可使用繼承來改變或擴展文法。也比較容易實現文法，因為定義抽象語法樹中各個節點的類別的實現大體類似，這些類別都易於直接編寫。

## 應用

除了像：正則運算式、瀏覽器等應用，只要是可以用語言來描述的，都可以應用。        

比如：機器人，可以直接對它說：「老兄，向前走10步，然後左轉90度，再向前走5步」，而不用去電腦面前調用向前走、左轉、右轉的方法。       

解譯器模式，就是將這樣的一句話，轉變成實際的命令程式執行而已。

# 音樂解譯器

## 規則：
- `O`表示音階：
    - `O1`低音階
    - `O2`中音階 
    - `O3`高音階
- `p`休止符
- `CDEFGAB`表示`Do-Re-Mi-Fa-So-La-Ti`
- 音符長度
    - 長度1為一拍
    - 長度2為二拍
    - 長度0.5為半拍
    - 長度0.25為1/4拍
- 所有的字母和數字都要用半形空格分開

---

- 表示如果獲得的key是C則演奏1(do)，如果獲得的key是D則演奏2(Re)
- 如果獲得的key是O並且value是1，則演奏低音，2則中音，3則是高音
- 當首欄位是O時，則運算式實體化為音階
- 當首字母為CDEFGAB，以及休止符P時，則實體化音符

# 音樂解譯器實現
## 結構

```
客戶端
    運算式
    + 解譯器(in context: 演奏內容)
    + 執行(in key: string, in value: double)

        音符
        + 執行(in key: string, in value: double)

        音階
        + 執行(in key: string, in value: double)

        音速
        + 執行(in key: string, in value: double)

    演奏內容
    + 演奏文字: string
```

## 程式碼
### 演奏內容類別(Context)

```c#
//演奏內容
class PlayContext {
    public string PlayText { get; set; }
}
```

### 運算式類別(AbstractExpression)

```c#
abstract class Expression {
    //解譯器
    public void Interpret(PlayContext context) {
        if(context.PlayText.Length == 0) return;

        // 目前播放文字的第一個指令字母和參數值
        // 例如 O 3 G 0.5 A 0.5 E 3
        // 則 playKey 為 O, playValue 為 3
        string playKey = context.PlayText.Substring(0,1);
        context.PlayText = context.PlayText.Substring(2);
        double playValue = Convert.ToDouble(context.PlayText.Substring(0, context.PlayText.IndexOf(" ")));

        //獲得playKey和playValue，將其從演奏文字中移除
        //例如：O 3 E 0.5 G 0.5 A 3
        //變成：E 0.5 G 0.5 A 3
        context.PlayText = context.PlayText.Substring(context.PlayText.IndexOf(" ")+1);
        Excute(playKey, playValue);
    }

    //抽象方法「執行」，不同的文法子類別，有不同的執行處理
    public abstract void Excute(string key, double value);
}
```

### 音符類別(TerminalExpression)

```c#
class Note: Expression {
    public override void Excute(string key, double value) {
        string note = "";
        switch(key) {
            case "C": //表示如果獲得的key是C則演奏1(do)
                note = "1";
                break;
            case "D": //如果獲得的key是D則演奏2(Re)
                note = "2";
                break;			
            case "E":
                note = "3";
                break;			
            case "F":
                note = "4";
                break;			
            case "G":
                note = "5";
                break;			
            case "A":
                note = "6";
                break;			
            case "B":
                note = "7";
                break;
        }
        Console.Write($"{note}");
    }
}
```

### 音階類別

```c#
class Scale: Expression {
    public override void Excute(string key, double value) {
        string scale = "";
        switch(Convert.ToInt32(value)) {
            case 1: //如果獲得的key是O並且value是1，則演奏低音，2則中音，3則是高音
                scale = "低音";
                break;
            case 2:
                scale = "中音";
                break;
            case 3:
                scale = "高音";
                break;
        }
        Console.Write($"{scale}");
    }	
}
```

### 用戶端程式碼

```c#
public static void Main()
{
    PlayContext context = new PlayContext();
    //音樂-上海灘
    Console.WriteLine("上海灘!");

    context.PlayText = "O 2 E 0.5 G 0.5 A 3 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3 ";
    Expression expression = null;

    try {
        while(context.PlayText.Length > 0) {
        string str = context.PlayText.Substring(0,1);

        switch(str) {
            case "O":		
                //當首欄位是O時，則運算式實體化為音階
                expression = new Scale();//實體化音階
            break;
            case "C":
            case "D":
            case "E":
            case "F":
            case "G":
            case "A":
            case "B":
            case "P":
                //當首字母為CDEFGAB，以及休止符P時，則實體化音符
                expression = new Note();
            break;
        }
        expression.Interpret(context);
    }
    } catch(Exception ex) {
    Console.WriteLine(ex.Message);
    }
}

/* 執行結果:

上海灘!
中音356352356高音1中音65132
*/
```

## 增加一個文法：演奏速度
- T代表速度，以毫秒為單位，
- `T 1000`表示每節拍一秒，`T 500`表示每節拍半秒

### 音速類別
加一個運算式子類別：音速

```c#
class Speed: Expression {
    public override void Excute(string key, double value) {
        string speed = "";
        switch(value) {
            case 1000:
                speed = "快速";
            break;
            case 500:
                speed = "中速";
            break;
            default:
                speed = "慢速";
            break;
        }
        Console.WriteLine(speed);
    }
}
```

### 用戶端程式碼

- 增加速度設定`T 500`
- `switch`增加對`T`的判斷

```c#
public static void Main()
{
    PlayContext context = new PlayContext();
    //音樂-上海灘
    Console.WriteLine("上海灘!");

    //增加速度設定 T500
    context.PlayText = "T 500 O 2 E 0.5 G 0.5 A 3 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3 ";
    Expression expression = null;

    try {
        while(context.PlayText.Length > 0) {
        string str = context.PlayText.Substring(0,1);

        switch(str) {
            case "O":		
                //當首欄位是O時，則運算式實體化為音階
                expression = new Scale();//實體化音階
            break;
            case "T":
                expression = new Speed(); //音速
                break;
            case "C":
            case "D":
            case "E":
            case "F":
            case "G":
            case "A":
            case "B":
            case "P":
                //當首字母為CDEFGAB，以及休止符P時，則實體化音符
                expression = new Note();
            break;
        }
        expression.Interpret(context);
    }
    } catch(Exception ex) {
    Console.WriteLine(ex.Message);
    }
}

/* 執行結果:

上海灘!
中速 中音 356352356高音1中音65132
*/
```