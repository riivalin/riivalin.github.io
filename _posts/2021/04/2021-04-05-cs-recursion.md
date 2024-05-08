---
layout: post
title: "[C# 筆記] 撰寫遞迴(Recursion)程式"
date: 2021-04-05 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,方法與參數,recursion,遞迴,遞迴(Recursion),"?:"]
---


## 何謂「遞迴程式」？      

遞迴程式的定義就是：「呼叫自己本身(call itself) 的函式(Function)或方法(Method)」，便可稱為遞迴(Recursion)。

> 方法的遞迴，就是「方法自己調用自己」。

常見的問題例如：求解N階層的問題。       

一個完整的遞迴程式須必備兩種要素：      
1. 遞迴條件 
2. 終止條件 

> 就算是遞迴，也是要有個條件跳出來，不然就會變成死循環。

所謂的「遞迴條件」就是：程式應該在什麼條件下呼叫自己。      
而「終止條件」就是：程式應該在什麼條件下終止遞迴的執行。(要有個條件跳出來)。        

所有遞迴程式都應該擁有這兩個要素才可以正常執行。        

## 語法

```c#
[存取修飾詞] 資料型別 方法名稱([資料型別 變數名稱...])
{
    if(終止條件) {
        return 值;
    } else {
        return 方法名稱(參數改變);
    }
}
```


## 遞迴(Recursion) vs 迴圈(Loop) 比較

遞迴(`Recursion`)：     
- 優點：有簡單思考邏輯、程式可讀性的提升、撰寫容易。    
- 缺點：較佔記憶體空間(因為運用到 Stack)、且執行速度較慢。  


|          |   Recursion  | Loop    | 　
|:---------|:-----------:|:---------------:|
| 中文       |遞迴       | 迴圈       |
| 程式碼大小| 較為精簡      | 較為繁雜 |
| 執行速度  | 較慢      | 較快|
| 佔用記憶體| 較多      | 較少 |
| 使用 Stack| 是        | 否 |
| 改寫特點  | 一定可以改成迴圈 | 不一定可以改寫遞迴 |



## 範例1：使用遞迴 求解N階層

n 階乘：連續正整數的乘積`1×2×3×⋯⋯×（n－1）×n＝n!`       
`N! = N(N-1)(N-2)......`   

階乘公式：`n!= n*(n-1)!`  

`n * (n-1)`


```c#
int n = 5;
Console.WriteLine($"{n} 的階層是：{NTier(n)}");

//計算階層
static int NTier(int n)
{
    // 階層 0! 和 1! 都為 1
    if (n <= 1) return 1;

    // 執行遞迴：n! = n * (n-1)!
    return n * NTier(n - 1);
}

//簡寫成一行
//return (n <= 1) ? 1 : n * NTier(n - 1);
```

> 在數學中，正整數的階乘（英語：factorial）是所有小於等於該數的正整數的積，記為 `n!`，例如5的階乘表示為 `5!`，其值為`120`：      
> `5! = 5 x 4 x 3 x 2 x 1 = 120`        
>
> 並定義，`1`的階乘 `1!` 和`0`的階乘 `0!` 都為`1`，其中`0`的階乘表示一個「空積」。      
> [Wiki - 階乘 ](https://zh.wikipedia.org/zh-tw/階乘) 


## 範例2：講故事

```c#
TellStory();

static int i = 0;
void TellStory()
{
    // 開始講故事
    Console.WriteLine("從前從前，有一座山，山裡有間廟，廟裡有兩個小和尚...");
    i++; //講一次就加1

    // 如果講超過10遍，就不講了，跳出方法
    if (i > 10) return;

    // 使用遞迴，自己呼叫自己
    TellStory();
}
```

錯誤的寫法：

```c#
TellStory();

//錯誤寫法，死循環
//就算是遞迴，也是要有個條件跳出來，不然就會變成死循環
void TellStory() 
{
    // 開始講故事
    Console.WriteLine("從前從前，有一座山，山裡有間廟，廟裡有兩個小和尚...");
    // 使用遞迴，自己呼叫自己
    TellStory();
}
```

## 範例3：求最大公因數

```c#
GCD(12, 8); //4

int GCD(int x, int y)
{
    //x除以y餘數為0，除數y就是最大公因數
    if (x % y == 0) return y;

    //使用遞迴，自己呼叫自己
    return GCD(y, x % y);
}
```

`GCD()`方法 可以使用三元運算子`?:`簡寫成 一行

```c#
GCD(12, 8); //4

int GCD(int x, int y)
{
    return (x % y == 0) ? y : GCD(y, x % y);
}
```

### 最大公因數

最大公因數（英語：highest common factor，hcf）也稱最大公約數（英語：greatest common divisor，gcd）是數學詞彙，指能夠整除多個整數的最大正整數。而多個整數不能都為零。例如8和12的最大公因數為4。      

輾轉相除法 相比質因數分解法，輾轉相除法的效率更高。     

計算`gcd(18,48)`時，先將`48`除以`18`得到商`2`、餘數`12`，然後再將`18`除以`12`得到商`1`、餘數`6`，再將`12`除以`6`得到商`2`、餘數`0`，即得到最大公因數`6`。**我們只關心每次除法的餘數是否為`0`，為`0`即表示得到答案**。       



[MSDN - 遞迴函式](https://learn.microsoft.com/zh-hk/cpp/c-language/recursive-functions?view=msvc-170) 
[Wiki - 階乘 ](https://zh.wikipedia.org/zh-tw/階乘)     
[Wiki -最大公因數](https://zh.wikipedia.org/zh-tw/最大公因數)       
[[C# 筆記] Recursion 方法的遞迴  by R](https://riivalin.github.io/posts/2011/01/recursion/)   
[字符串的最大公因子 by R](https://riivalin.github.io/posts/2000/01/leetcode-1071/)  
