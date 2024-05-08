---
layout: post
title: "[C# 筆記] try catch 例外處理（Exception Handling）"
date: 2021-04-06 23:58:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,例外處理(Exception Handling),try catch,DivideByZeroException]
---


# C# 常見的錯誤類型

- 語法錯誤(Syntax error)
- 編譯期錯誤(Complile time error)
- 邏輯錯誤(Logical error)
- 執行期錯誤(Runtime error)
- 數值性錯誤(Numerical error)

## 範例1：引發例外

以下是一個會引發例外的程式，由於 x/b = 10/0 會導致嘗試以零除 (`System.DivideByZeroException`) 的例外，但這個例外又沒有被任何的 `try...catch` 段落所處理，因此整個程式會中斷並輸出錯誤訊息。

```c#
static void Main(string[] args)
{
    int x = 10, y = 0;
    Console.WriteLine($"x/y= {x/y}"); //會發生錯誤，因為 被除數不可為0 
}
```

執行結果：      

會發生錯誤「 未處理的例外狀況: System.DivideByZeroException: 嘗試以零除。
   於 Program.Main(String[] args)」

```
Unhandled exception. System.DivideByZeroException: Attempted to divide by zero.
   at ConsoleApp1.Program.Main(String[] args) in C:\Users\rivalin\source\ConsoleApp1\ConsoleApp1\Program.cs:line 12

```

## 範例2：處理例外 (用 try…catch 語句)

要處理例外可以用 `try...catch` 語句，以下範例就利用 `try { ... } catch (DivideByZeroException ex)` 捕捉了上述的除以零之例外，您可以在 `catch` 段落中進行例外處理後，再決定要如何繼續執行程式。(本範例中只單純的提示被除數不可為零)。

```c#
static void Main(string[] args)
{
    try
    {
        int x = 10, y = 0;
        Console.WriteLine($"x/y= {x / y}");

    } catch (DivideByZeroException ex)
    {
        Console.WriteLine($"被除數不可為 0 \r\n{ex}");
    }
}
```

> `DivideByZeroException`：用於嘗試除0所擲回的例外狀況。

執行結果：

```
被除數不可為 0
System.DivideByZeroException: Attempted to divide by zero.
   at ConsoleApp1.Program.Main(String[] args) in C:\Users\rivalin\source\ConsoleApp1\ConsoleApp1\Program.cs:line 14
```



# 常見的例外狀況

|例外狀況類型 	                  |描述                                                       |
|:--------------------------|:-----------------------------------------------------------|
|`ArgumentException`	    |當其中一個提供給方法的引數為無效時所擲回的例外狀況。|
|`ArithmeticException`	    |為算術、轉型 (`Casting`) 或轉換作業中的錯誤擲回例外狀況。|
|`DivideByZeroException`	|嘗試將整數或小數值除以零時所擲回的例外狀況。|
|`DllNotFoundException` 	|`DLL` 匯入中所指定的 `DLL` 找不到時所擲回的例外狀況。|
|`FormatException`	        |當引數的格式不符合叫用 (`Invoke`) 方法的參數規格時所擲回的例外狀況。|
|`MissingFieldException`	|當嘗試動態存取不存在的欄位時，所擲回的例外狀況。|
|`OutOfMemoryException`	    |當沒有足夠的記憶體繼續執行程式時，所擲回的例外狀況。|
|`OverflowException`	    |當檢查內容中的算數、轉型 (`Casting`) 或轉換作業發生溢位時所擲回的例外狀況。|
|`NullReferenceException`	|當嘗試解除 `Null` 物件的參考時，所擲回的例外狀況。|
|`IndexOutOfRangeException`	|嘗試使用陣列以外的索引來存取陣列的元素時所擲回的例外狀況。這個類別無法被繼承。|




[MSDN - 在 .NET 中處理和擲回例外狀況](https://learn.microsoft.com/zh-tw/dotnet/standard/exceptions/)        
[免費電子書：C# 程式設計 - C# 的例外處理範例 by 陳鍾誠](http://cs0.wikidot.com/exception1)      