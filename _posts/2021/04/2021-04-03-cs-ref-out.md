---
layout: post
title: "[C# 筆記] 方法參數的使用(ref & out & params)"
date: 2021-04-03 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,方法與參數,ref,out,params,Call by Reference]
---

三個高級的參數      
`out`, `ref`, `params`

# ref 與 out 的差異

`ref` 與 `out` 均可以讓方法參數的傳遞以「傳參考呼叫(`Call by Reference`)」的方式來進行，二者主要差異在於 `ref` 在傳遞變數時，該實際參數變數必須要明確初始化；而`out`則不需要對要傳遞的變數進行初始化。

> 簡單一句話：使用`ref`必須先為變數初始化賦值，而使用`out`則不用。
>
> `ref` 參數傳遞前必須初始化；      
> `out` 參數不需初始化，但必須於方法內給值。        


#### 初始值:

- `ref` 在傳遞給方法之前，**必須**先為變數賦初值，也就是在方法呼叫前，必須對變數進行初始化。
- `out` 在傳遞給方法之前，**不用**先為變數賦初值，但在方法內部，必須在使用該參數之前先賦值。

```c#
// ref
int x = 10; //要初始化給值
void Test(ref int number) { }

// out
int x; //不用初始化
void Test(out int number) { }
```

#### 方法內部對參數的要求:

- `ref` 在方法內部，**不用**對變數重新賦值，但可以在方法內對其進行修改。
- `out` 在方法內部，**必須**對變數重新賦值，因為方法內部不能使用未賦值的 `out` 參數。
        
```c#
// ref 範例
int x = 10; //要初始化
void Test(ref int number) {
    number += 10;
}

// out 範例
int x; //不用初始化
void Test(out int number) {
    number = 10; // number 必須在方法內給值
}
```


## ref 參數傳遞

`ref` 在傳遞變數時，該實際參數變數必須要**明確初始化**。

```c#
// ref 的範例
static void Main(string[] args)
{
    int x = 10; //要初始化給值
    Test(ref x); //必須在呼叫前為 x 賦值
    Console.WriteLine(x);  // x=20
}

// ref參數方法
static void Test(ref int number) {
    number += 10;
}
```

## out 參數傳遞

`out` 不需要對要傳遞的變數進行初始化。      
必須在方法內部為 參數 賦值

```c#
// out 的範例
static void Main(string[] args)
{
    int x; //不用初始化
    Test(out x); // 不需要在呼叫前為 x 賦值
    Console.WriteLine(x); // x=10
}

// out參數方法
static void Test(out int number) {
    number = 10; // 必須在方法內部為 number 賦值
}
```

如果給 out 參數的 x 賦值 會怎樣呢？      

x 依然是方法內部的值。

```c#
int x = 100; //硬給 x 賦值 (這裡會出現提示「x 指派了不必要的值」)
Test(out x);
Console.WriteLine(x); //輸出結果 x=99

static void Test(out int number) {
    number = 99; 
}
```

## 使用時機

- `ref`的使用時機在於「傳入參數值需要加以處理時」。

> 如果方法需要從參數中取得值並可能對其進行修改，可以使用 `ref`。

- `out`的使用時機在於「直接獲取方法處理結果，而不需要加以處理原本傳入的參數」。

> 如果方法只需要傳回值，並且不關心參數的初始值，可以使用 `out`。


## Q & A
Q：若對同一的方法名稱執行多載宣告時，可以同時使用`ref` 與 `out`嗎？

```c#
void Add(ref int n) { }
void Add(out int n) { }
```

A：**不能**。「`ref`引數」與「`out`引數」只能存在一個，不能只以 `ref` 或 `out` 區分多載方法。

```c#
void Add(ref int n) { }
//不能只以 ref 或 out 區分多載方法，只能存在一個
//void Add(out int n) { }
```

# params

`params`以陣列方式來傳遞方法引數。

- `params`關鍵字後面資料型態，一定要是「一維陣列」型態(`int[]`, `striing[]`)。如：`params int[]`      
- `params`它是唯一性，所以在方法宣告中，只能有一個 `params` 關鍵字

此意即你不能宣告成：    

```c#
//錯誤的宣告，只能有一個 params
void Test(params int[] nums, params string[] names) { }
```
 
-  `params` 在方法宣告中，必須是最後一個參數。(一定要放在最後一個)

```c#
// params 必須是最後一個參數(一定要放在最後一個)
void Test(string name, params int[] score) { }
```

為什麼要最後一個，因為它區分不出來，所以要放最後一個。


### 能不能不要宣告一個陣列，方法中直接給陣列進去呢？        
能，在參數前面加上 `params`

```c#
Test("Rii",77, 88, 99); //不用宣告陣列，直接把數字放進去

// 在參數前面加上 params
void Test(string name, params int[] score) { }
```

### 當看到 praams 參數時，有兩個選擇

1. 填相同類型一致的元素帶進去
2. 填跟寫一樣的數組進去

```c#
//當看到praams 參數時，有兩個寫法選擇：

// 第一種：填相同 int[]類型 一致的元素 帶進去
Test("Rii", 88, 77, 56, 89, 100);  //不用宣告陣列，直接把數字放進去
 
// 第二種：填跟寫一樣的陣列進去
int[] score = { 88, 77, 56, 89, 100 }; //宣告陣列
Test("Rii", score); 

// params參數：在參數前面加上 params
void Test(string name, params int[] score) { }
```

## 範例

求任意長度陣列的總和(整數類型)

```c#
//調用params參數方法，有兩種選擇：

//第一種：宣告陣列，再放入方法
int[] nums = { 1, 2, 3, 4, 5 };
int sum1 = Sum(nums);

//第二種：直接放入元素
int sum2 = Sum(1, 2, 3, 4, 5);

//params參數方法
static int Sum(params int[] nums)
{
    int sum = 0;
    foreach (var e in nums) {
        sum += e;
    }
    return sum;
}
```


[[C# 筆記] 值傳參 & 引用傳參(ref) & 輸出傳參(out)  by R](https://riivalin.github.io/posts/2010/01/r-csharp-note-3/)     
[[C# 筆記] ref 和 out 有什麼區別？  by R](https://riivalin.github.io/posts/2017/02/what-is-the-difference-between-ref-and-out/)     
[[C# 筆記] params 可變參數  by R](https://riivalin.github.io/posts/2011/01/params_s/)     
[[C# 筆記] params   by R](https://riivalin.github.io/posts/2021/01/params/)