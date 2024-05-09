---
layout: post
title: "[C# 筆記] 自訂例外狀況 (Exception)"
date: 2021-04-14 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,例外處理(Exception Handling),throw]
---


C#是一種強型別語言，可以捕捉和處理各種異常，從而幫助我們發現程式中出現的錯誤。在程式開發過程中，如果需要找到特定的錯誤情況並處理，這時就需要建立自訂例外狀況。

> 雖然可以自行建立例外處理，但不應該藉由例外處理來變更既有的程式流程或用於偵錯。您應該用於處理、紀錄或回報錯誤情況。


## 1. 什麼是異常？

異常是指在程式執行期間​​發生的錯誤或異常情況，例如除法中`除以0`、檔案不存在、記憶體不足等。當發生異常時，程式會停止執行目前的操作，並拋出一個異常物件。異常物件包含有關異常情況的信息，例如異常類型、錯誤訊息、堆疊追蹤等。

C# 例外處理基於四個關鍵字建構：`try`、`catch`、`finally` 和 `throw`。

1. `try`： try 區塊標識為其啟動特定異常的程式碼區塊。它後面是一個或多個捕獲塊。        
2. `catch`：程式在程式中要處理問題的位置使用異常處理程序捕捉異常。 catch 關鍵字會捕捉發生的異常。     
3. `finally`：finally 區塊用於執行一組給定的語句，無論是否引發異常。例如，如果開啟一個文件，無論是否引發異常，都必須關閉該文件。     
4. `throw`：當出現問題時，程式會引發異常。這是使用 throw 關鍵字完成的。


## 2. 自訂例外狀況

接下來我們來看看如何建立C#異常類，在C#中，建立自訂異常很簡單。只需要建立一個類，並從`System.Exception`類或其子類派生即可。例如，以下程式碼建立了一個名為`CustomException`的自定義異常類

### 建立自訂例外狀況

```c#
public class CustomException: Exception
{
    public CustomException() 
        : base("預設的錯誤訊息") { }
    public CustomException(string message) //指定錯誤訊息
        : base(message) { throw new Exception(message); }
    public CustomException(string message, Exception innerException) //指定錯誤訊息 和 內部異常訊息
        : base(message, innerException) { }
}
```

這樣一個自訂例外狀況就建立好了。

- `throw new CustomException();`會呼叫第一個建構子
- `throw new CustomException("測試...");`會呼叫第二個建構子

## 使用自訂例外狀況

使用自定義例外處理與使用內建例外處理類別相同。只需要在程式中拋出異常物件，並使用try-catch塊捕獲異常即可。程式碼如下：

```c#
try
{
    int x = 10, y = 0;
    int z = y / x; // 0/10

    //如果是 除0問題(10/0) 會報 系統異常
    //int z = x / y; // error輸出: 系統異常：Attempted to divide by zero.

    throw new CustomException(); // 0/10，error輸出: 自定義異常：預設的錯誤訊息
    throw new CustomException("發生了自定義異常"); // 0/10 error輸出: 系統異常：發生了自定義異常

} catch (CustomException ex)
{
    Console.WriteLine($"自定義異常：{ex.Message}");
} catch (Exception ex)
{
    Console.WriteLine($"系統異常：{ex.Message}");
}
```

上面程式碼如果是**除0問題**會報系統異常，而自訂例外狀況在觸發的時候報自訂異常。


## 3.自訂例外狀況的使用場景

那麼自訂異常類別通常在哪些場景下使用呢？檢測業務規則

當我們需要檢查業務規則時，可以建立自訂異常類別。例如，當使用者嘗試建立已經存在的帳戶時，我們可以拋出一個名為`DuplicateAccountException`的自訂例外。

### 範例

當「使用者不存在」時所使用的例外狀況

```c#
class UserNotFoundException : Exception, ISerializable
{
    public UserNotFoundException()
        : base("使用者不存在") { }
    public UserNotFoundException(string message) 
        : base(message) { }
    public UserNotFoundException(string message, Exception inner) 
        : base(message, inner) { }
    protected UserNotFoundException(SerializationInfo info, StreamingContext context) 
        : base(info, context) { }
}
```

在第一個建構子帶入一個預設的錯誤訊息，也就是當執行到下列程式時，由呼叫者取得的例外資訊就會包含這個例外型別提供的預設錯誤訊息：

```c#
throw new UserNotFoundException();
```




[MSDN - 建立和擲回例外狀況](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions)       
[MSDN - Exception 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.exception?view=net-8.0&redirectedfrom=MSDN)     
[MSDN - 例外狀況和例外狀況處理](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/exceptions/)  
[如何利用「自訂例外狀況」處理無法繼續執行的錯誤 by The Will Will Web](https://blog.miniasp.com/post/2009/09/30/How-to-Designing-Custom-Exceptions)      
[CSDN - 如何在C#中创建和使用自定义异常](https://blog.csdn.net/lwf3115841/article/details/130643671)