---
layout: post
title: "[閱讀筆記][Design Pattern] Ch1. 簡單工廠模式"
date: 2009-03-02 05:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 簡單工廠模式(Simple Factory Pattern)

## 物件導向
1. 可維護
2. 可複用
3. 可擴充
4. 靈活性好
透過封裝、繼承、多型把程式的耦合性降低。

> 程式設計有一個原則：就是用盡可能的辦法去避免重複。

## 業務的封裝
讓「業務邏輯」與「介面邏輯」分開，讓它們之間的耦合度下降。

### 計算機為例
分一個類別出來，讓計算和顯示分開。(但這還談不上完全物件導向)

```c#
//用戶端
public static void Main() {
    decimal result = Operation.GetResult(1,0,"/");
    Console.WriteLine(result);
}

//運算類別
public class Operation {
    public static decimal GetResult(decimal numberA, decimal numberB, string operate) {      
        decimal result = 0;
        switch(operate) {
            case "+":
                result = numberA + numberB;
                break;
            case "-":
                result = numberA - numberB;
                break;
            case "*":
                result = numberA * numberB;
                break;
            case "/":
                if(numberB == 0) break;
                result = numberA / numberB;
                break;
        }
        return result;
    } 
}
```

## 緊耦合 vs. 鬆耦合

如果現在希望增加一個開根號(sqrt)運算，如何改？  

應該把加減乘除等運算分離，修改其中一個不影響另外的幾個，增加運算演算法也不會影響其他程式碼。

```c#
public static void Main()
{
    OperationDiv div = new OperationDiv();
    div.NumberA = 9;
    div.NumberB = 10;
    
    Console.WriteLine(div.GetResult());
}

//運算類別
public class Operation {
    //兩個屬性，用於計算機的前後數
    public decimal NumberA {get; set;} = 0;
    public decimal NumberB {get; set;} = 0;

    //虛方法，用於得到結果
    public virtual decimal GetResult() {
        return 0;
    }
}

//加減乘除類別，繼承Operation運算類別
public class OperationAdd:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        return NumberA + NumberB;
    }
}
public class OperationSub:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        return NumberA - NumberB;
    }
}
public class OperationMul:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        return NumberA * NumberB;
    }
}
public class OperationDiv:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        if(NumberB == 0) throw new Exception("除數不能為0。");
        return NumberA / NumberB;
    }
}
```

但，問題來了，我該如何讓計算機知道我是希望用哪一個演算法呢？    

## 簡單工廠模式

其實現在的問題就是：如何去實體化物件。也就是說，到底要實體化誰，將來會不會增加實體化的物件，比如：增加開根號運算。      

應該考慮用一個單獨的類別來做這個創造實體的過程，這就是工廠。

```c#
//用戶端程式碼
public static void Main()
{
    Operation oper; //宣告運算類別物件
    oper = OperationFactory.CreateOperation("+"); //使用工廠類別實體化相應的物件，並賦值給oper變數
    oper.NumberA = 10;
    oper.NumberB = 20;
    Console.WriteLine(oper.GetResult());
}

//簡單運算工廠類別
public class OperationFactory {
    //依據傳入的符號，實體化出合適的物件，並返回該物件
    public static Operation CreateOperation(string operate) {
        //宣告運算類別物件，初始值為空
        Operation oper = null;
        //判斷傳入的符號
        switch(operate) {
            case "+":
                oper = new OperationAdd(); //實體化加法運算物件
                break;						
            case "-":
                oper = new OperationSub(); //實體化減法運算物件
                break;
            case "*":
                oper = new OperationMul(); //實體化乘法運算物件
                break;
            case "/":
                oper = new OperationDiv(); //實體化除法運算物件
                break;
        }
        return oper;
    }
}

//運算類別
public class Operation {
    //兩個屬性，用於計算機的前後數
    public decimal NumberA {get; set;} = 0;
    public decimal NumberB {get; set;} = 0;

    //虛方法，用於得到結果
    public virtual decimal GetResult() {
        return 0;
    }
}

//加減乘除類別，繼承Operation運算類別
public class OperationAdd:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        return NumberA + NumberB;
    }
}
public class OperationSub:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        return NumberA - NumberB;
    }
}
public class OperationMul:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        return NumberA * NumberB;
    }
}
public class OperationDiv:Operation {
    //重寫父類別的GetResult方法
    public override decimal GetResult() {
        if(NumberB == 0) throw new Exception("除數不能為0。");
        return NumberA / NumberB;
    }
}
```

### 如果日後需要更改需求：
1. 需要更改加法運算，怎麼做？     
只要改`OperationAdd`子類別就可以了。     

2. 需要增加各種複雜運算，比如：平方根、立方根、自然對數，怎麼做？     
只要增加相應的運算子類別，以及去工廠類別的`switch`中增加分支就可以了。

### 類別結構

```
簡單工廠類別
+ CreateOperation():運算類別

    運算類別
    + NumberA: decimal
    + NumberB: decimal
    + GetResult(): decimal

        加法類別
        + GetResult(): decimal
        減法類別
        + GetResult(): decimal
        乘法類別
        + GetResult(): decimal
        除法類別
        + GetResult(): decimal
```

# UML基本圖示

- 矩形框：它就代表一個類別`class`       

## 類別圖
- 類別圖分三層：
    - 第一層：顯示類別的名稱
    - 第二層：是類別的特性，通常就是欄位和屬性
    - 第三層：是類別的操作，通常是方法或行為
> 注意前面符號：    
> - `+`表示`public` 
> - `-`表示`private`    
> - `#`表示`protected`      

## 介面圖
- 介面圖有`<<interface>>`顯示
- 棒棒糖標記法：圓圈旁為介面名稱，介面方法在實現類別中出現

> 介面還有另一種表示方法，俗稱：棒棒糖表示法。        
> 比如：唐老鴨類別實現了「講人話」的介面。

## 關係符號
### 繼承關係
- 繼承關係：用「空心三角形+實線」來表示 
> 例如：動物 → 鳥之間的關係(鳥繼承動物類別)。

### 實現介面
- 實現介面：用「空心三角形+虛線」來表示
> 例如：大雁是最能飛的，我讓它實現了「飛翔介面`IFly`」。

### 關聯關係(知道)
- 關聯關係(知道)：用實線箭頭來表示  

比如：企鵝和氣候兩個類別，企鵝會游不會飛，更重要的是，牠與氣候有很大的關聯。企鵝需要「知道」氣候的變化，需要「瞭解」氣候規律。      

當一個類別「知道」另一個類別時，可以用關聯(`association`)。

```c#
class Penguin: Bird {
    //在企鵝Penguin中，參考到氣候Climate物件
    private Climate climate;
}
```

### 聚合關係
- 聚合關係：用「空心菱形+實線箭頭」來表示 

大雁和雁群這兩個類別，大雁是群居動物，每一隻大雁都是屬於一個雁群，一個雁群可以有多少隻大雁。所以牠們之間就滿足「聚合(`Aggregaion`)關係」。      

> 聚合表示一種弱的「擁有」關係，表現的是「A物件」可以包含「B物件」，但「B物件」不是「A物件」的一部分。

```c#
class WideGooseAggregate {
    //在雁群WideGooseAggregate類別中，有大雁陣列物件arrayWideGoose
    private WideGoose[] arrayWideGoose;
}
```

### 合成(組合)關係
- 合成(組合)關係：用「實心的菱形+實線箭頭」來表示
- 連線兩端的數字，稱為「基數」。表明這一端的類別可以有幾個實體。


合成(組合)是一種強的「擁有」關係，表現了嚴格的部分和整體的關係，部分和整體的生命週期一樣。      

鳥和翅膀就是合成(組合)關係，因為繚們是部分和整體的關係，並且翅膀和鳥的生命週期是相同的。
> 一隻鳥有兩隻翅膀，連線兩端有數字：`鳥1 ◆-> 翅膀2`。   
> 如果一個類別可能有無數個實體，則就用`n`來表示。關聯關係、聚合關係也可以有基數的。     

```c#
class Bird {
    private Wing wing;
    public Bird() {
        //在鳥bird類別中，初始化時，實體產生翅膀Wing，它們之間同時產生
        wing = new Wing;
    }
}
```

### 依賴關係(Dependency)
- 依賴關係(Dependency)：用「虛線箭頭」來表示

動物要有生命力，需要氧氣、水以及食物。也就是說，動物依賴於氧氣和水，他們之間是「依賴關係(Dependency)」。

```c#
abstract class Animal {
    //新陳代謝(in O2:氧氣, in water: 水)
    public abstract Metabolism(Oxyge oxyge,Water water);
}
```