---
layout: post
title: "[C# 筆記] 值傳參 vs 引用傳參ref vs 輸出傳參out"
date: 2010-01-03 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,ref,out]
---

C# 中有三種傳遞參數的方法：
1. 值傳參
2. 引用傳參`ref`
3. 輸出傳參`out`

## 值傳參
在這種方式下，當我們調用一個方法的時候，會為每一個值創建一個新的儲存位置，也就是說，實真的參數會賦值給方法中的形參，而實參和形參在程式執行的時候，使用的是兩個完全不同的內存，當形參改變的時候，不會影響實參的數據，從而保証了實參數據的安全。

- 參數傳遞的默認方式
- 為每個值參數創建一個新的儲存位置
- 當形參的值發生改變時，不會影響實參的值，實參數據安全

以下程式碼，進行x,y的值交換，形參的值改變了，a和b沒有發生變化
```c#
static void Swap(int x, int y) {
    int temp;
    temp = x;
    x = y;
    y = temp;
}

static void Main(string[] args)
{
    int a = 100;
    int b = 500;
    Swap(a, b);

    Console.WriteLine($"a: {a}");
    Console.WriteLine($"b: {b}");
    Console.Read();
}
```
輸出：a:100, b:500

## 引用傳參 ref

若我們把方法中的形參加上 `ref`(reference)(引用的意思)呢？   
參數的傳遞方式就從真實的數據複製，轉變為了內存地址的引用了。`ref`這詞，就有一點點類似`C++`中的 `pointer`的意思。

調用方法的時，同樣要在參數中標明 `ref`這個關鍵字，表示引用變量的內存地址。

```c#
static void Swap(ref int x, ref int y) {
    int temp;
    temp = x;
    x = y;
    y = temp;
}

static void Main(string[] args)
{
    int a = 100;
    int b = 500;
    Swap(ref a, ref b);

    Console.WriteLine($"a: {a}");
    Console.WriteLine($"b: {b}");
    Console.Read();
}
```
輸出：a:500, b:100

## 輸出傳參 out
我們還可以使用輸出參數，在不改變方法的範圍內，輸出更多的數據。比如說，我們現在創建一個沒有返回值的方法：
        
這方法什麼事都沒幹，只是把傳入的值改成5而己，調用它時也不會有任何改變
```c#
static void GetValue(int x) {
    x = 5;
}
static void Main(string[] args)
{
    int a = 100;
    int b = 500;
    Swap(ref a, ref b);

    Console.WriteLine($"a: {a}");
    Console.WriteLine($"b: {b}");

    GetValue(a);//a依然是100
    Console.WriteLine($"第二次 a: {a}");
    Console.Read();
}
```

如果我們在方法中的參數前加上`out`，在調用方法的時候，同時使用關鍵字`out`，那麼這個方法中的x值就會突破限制，直接向參數a進行輸出，a 的數據也會變成5，這個就是「輸出傳參」，就是可以把方法內部的變化，輸出反映在參數中，並且以return 以外的形式來輸出
```c#
static void GetValue(out int x) {
    x = 5;
}
static void Swap(ref int x, ref int y) {
    int temp;
    temp = x;
    x = y;
    y = temp;
}

static void Main(string[] args)
{
    int a = 100;
    int b = 500;
    Swap(ref a, ref b);

    Console.WriteLine($"a: {a}");
    Console.WriteLine($"b: {b}");

    GetValue(out a); //加上out，a=5
    Console.WriteLine($"第二次 a: {a}");
    Console.Read();
}
```

## 引用傳參 vs 輸出傳參
Q：「引用傳參`ref`」和「輸出傳參`out`」到底有什麼不同呢？

簡單一句話：使用`ref`需要提前定義、提前初始化、提前賦值，而使用`out`則沒有限制。

Q：什麼意思呢？

來改一下程式碼，把變量a初始化數據刪掉：

```c#
static void Main(string[] args)
{
    int a;
    int b = 500;
    Swap(ref a, ref b);//a報錯

    Console.WriteLine($"a: {a}");
    Console.WriteLine($"b: {b}");

    GetValue(out a);//a沒報錯
    Console.WriteLine($"第二次 a: {a}");
    Console.Read();
}
```
這時候發現問題沒，使用「引用傳參`ref`」的swap 方法報錯了，使用「輸出傳參`out`」的getValue則沒有任何問題。

所以使用「引用傳參`ref`」的前提就是：被引用的變量必須已經完成了初始化，也就是說，被引用變量必須得有數據。

而「輸出傳參`out`」則沒有這個要求，沒有初始化的變量，也可以通過輸出參數在引用的同時完成初始化。

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=16](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=16)