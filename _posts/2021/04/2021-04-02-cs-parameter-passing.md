---
layout: post
title: "[C# 筆記] 參數傳遞 (Call by Value & Call by Reference)"
date: 2021-04-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,方法與參數,stack,heap,Value Type,Reference Type,ref]
---

參數傳遞(Parameter Passing)方式有兩種：        

1. 傳值呼叫(`Call by Value`)
2. 傳參考呼叫(`Call by Reference`)


# 傳值呼叫(Call by value)

「傳值呼叫(Call by value)」就是主程式將參數值傳給方法，執行方法後，所傳入的參數值為無論怎麼修改它，都不會因此而更動到主程式裡的參數值。        

故在傳遞參數時，其實是**重新複製**一個參數，再將此「複製參數」傳遞給方法。       

此「複製參數」稱為「虛擬參數」，方法內的「虛擬參數」與主程式中的「實際參數」位於**不同**的記憶體位址。      

由於`Call by value`需要額外記憶體，故`binding time`最晚。

- 優點：沒有副作用(`side effect`)
- 缺點：較佔記憶空間。
- 使用時機：傳送小量資料。

## 範例

將 n 傳入 Test()方法中，執行 +10 動作後，n仍然為10

```c#
//主程式
int n = 10;
Test(n); //將n傳入方法中，執行+10動作後
Console.WriteLine(n); //n仍然為10

//方法
void Test(int number) {
    number += 10; //20
}
```
number、n 兩塊不同的空間(不同記憶體位址)        


# 傳參考呼叫(Call by reference)

「傳參考呼叫(Call by reference)」就是主程式將參數值傳遞給方法，執行後，只要方法有修改到所傳入的參數值，就會更動影響到主程式裡的參數值，

故在傳遞參數時，方法內的「虛擬參數」與主程式中的「實際參數」位於「相同」的記憶體位址，故稱「Call by reference」。      

- 優點：節省記憶空間、傳遞速度快。
- 缺點：有副作用(`side effect`)
- 使用時機：適合傳送大量資料。

## 範例：參數加上 ref

寫一方法，參數加上`ref`，     
將 n 傳入 Test() 方法中，執行 +10 動作後，n 變成為20。

```c#
//主程式
int n = 10;
Add(ref n);
Console.WriteLine(n); //20

//方法
void Add(ref int number) { //參數加上ref
    number += 10;
}
```
原本 number、n 是兩塊不同的空間(不同記憶體位址)，      
執行後，number、n 指向相同位址。        

> number、n 他們本來不是同一塊空間，加了`ref`，他們在`Stack`(堆疊/棧)地址是一樣的，變成同一塊空間。


### 為什麼 number = 20？

因為`ref`的作用能把一個變量以參數的形式帶到一個方法中進行改變。     
再將改變後的值從方法中帶出來。

# 結論

由此可知，「call by value(傳值呼叫)」 當從主程式傳入參數時，經方法運算之後，並不會影響主程式所傳入的參數值，      

而「call by reference(傳參考呼叫)」當主程式傳入參數時，經方法運算之後，會指向同一個記憶體位址，     

所以也影響了主程式當時所傳入的參數值。      

最簡單的概念講法就是：call by value 沒有副作用，而call by reference有副作用


# ref 參數
`ref` 的好處就是，不用再寫返回值了

- 能夠將一個變量帶入一個方法中進行改變，改變完成後，再將改變後的值帶出方法。
- `ref` 參數要求在方法外必須為其賦值，而方法可以不賦值。

> 它能夠把一個變量以參數的型式傳遞給一個方法， 在一個方法中進行改變，       
> 改變完成後，再把這個值自動的，就是改變後的值自動的給我帶出來。        
>
> 在一個方法裡面改變這個變數，方法外面這個變數也跟著改變。      


## 範例

在方法的參數前加`ref`即可 `void Test( ref int i) { }`

```c#
public static void Main()
{
    int salary = 20000; //salary的值會帶入ref參數方法裡面
    SalaryIncrease(ref salary); //調薪。Call ref參數的調薪方法
    Console.WriteLine(salary); //25000
}

// ref參數 的 調薪方法
static void SalaryIncrease(ref int salary) 
{
    salary += 5000;
}
```

[[C# 筆記] 值傳遞 & 引用傳遞  by R](https://riivalin.github.io/posts/2011/01/valuetype-referencetype/)      
[[C# 筆記] ref 參數  by R](https://riivalin.github.io/posts/2011/01/ref/)