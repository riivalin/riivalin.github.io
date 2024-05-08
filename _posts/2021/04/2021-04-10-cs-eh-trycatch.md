---
layout: post
title: "[C# 筆記] try catch 語句"
date: 2021-04-10 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,例外處理(Exception Handling),try catch finally,throw]
---


`try-catch`語句區塊是C#中用於異常處理的例外處理機制。異常是程式執行過程中可能出現的錯誤或意外情況，而`try-catch`語句區塊可讓您在執行程式碼時捕獲並處理這些異常。

# 1. try-catch語句區塊的結構

一個`try-catch`語句區塊通常包含以下部分：       

- `try`關鍵字：用於標識需要進行異常處理的程式碼區塊。在這個程式碼區塊內，您可以放置​​可能會引發異常的程式碼。   
- `catch`關鍵字：用於捕獲並處理異常。可以在`catch`區塊中定義**一個**或**多個**異常類型，以及對應的處理程式碼。  
- `finally`關鍵字（可選）：用於定義在try區塊中的程式碼執行後，無論是否發生異常，都會執行的程式碼區塊。  

以下是一個基本的`try-catch`語句區塊的結構：

```c#
try
{
    // 可能引發異常的程式碼
}
catch (ExceptionType1 ex1)
{
    // 處理 ExceptionType1 類型的異常
}
catch (ExceptionType2 ex2)
{
    // 處理 ExceptionType2 類型的異常
}
finally
{
    // 可選，執行清理操作
}
```


# 2. try-catch語句區塊的用法
## 2.1 捕獲特定類型的異常

您可以在`catch`區塊中指定特定類型的異常，以便只捕獲和處理特定類型的異常。這樣可以根據不同的異常類型提供不同的處理邏輯。

```c#
try
{
    // 可能引發異常的程式碼
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("除以零錯誤：" + ex.Message);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine("文件不存在：" + ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("其他異常：" + ex.Message);
}
```

在上述範例中，`DivideByZeroException`和`FileNotFoundException`是特定的異常類型，分別用於處理**除以零錯誤**和**檔案不存在錯誤**。

## 2.2 使用通用的異常基類

如果您想要在一個`catch`區塊中擷取多種類型的異常，可以使用`Exception`作為通用的異常基底類別。

```c#
try
{
    // 可能引發異常的程式碼
}
catch (Exception ex)
{
    Console.WriteLine("發生異常：" + ex.Message);
}
```

在這種情況下，`catch (Exception ex)`會捕獲所有類型的異常，包括系統異常和自訂異常。

## 2.3 處理多個異常

您可以在一個`try-catch`語句區塊中處理多個不同類型的例外。每個`catch`區塊會根據引發的異常類型，選擇執行對應的處理程式碼。

```c#
try
{
    // 可能引發異常的程式碼
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("除以零錯誤：" + ex.Message);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine("文件不存在：" + ex.Message);
}
catch (IOException ex)
{
    Console.WriteLine("IO錯誤：" + ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("其他異常：" + ex.Message);
}
```

## 2.4 使用 `finally` 區塊進行資源清理

`finally`區塊用於包含無論是否發生異常都必須執行的程式碼，通常用於進行資源的釋放和清理操作。

```c#
try
{
    // 可能引發異常的程式碼
}
catch (Exception ex)
{
    Console.WriteLine("發生異常：" + ex.Message);
}
finally
{
    Console.WriteLine("執行清理操作。");
}
```

`finally`區塊中的程式碼會在`try`區塊中的程式碼執行後執行，無論是否發生異常。

# 3. try-catc h語句區塊的最佳實踐
## 3.1 不要過度使用異常

異常處理是用來處理真正的異常情況的，而不應該被用於控制程式流程。過多的異常處理會影響效能和程式碼可讀性。

## 3.2 使用特定的異常類型

盡量使用特定的異常類型來捕捉和處理異常，這樣可以更準確地針對不同類型的錯誤提供不同的處理邏輯。

## 3.3 不要捕獲所有異常

避免在一個大的`catch`區塊中捕捉所有異常，這會導致難以定位問題。根據異常類型提供適當的處理。

## 3.4 使用 `finally` 進行資源釋放

在使用資源（如檔案、資料庫連線等）時，使用`finally`區塊確保資源在程式碼區塊執行後釋放，以避免資源外洩。

## 3.5 記錄異常訊息

捕獲到的異常至少應該記錄錯誤訊息，以便於調試和故障排除。可以使用日誌記錄庫或輸出到控制台。

## 3.6 自訂異常類

在需要時，您可以建立自訂異常類，以提供更有意義的異常資訊和處理方式。這有助於調試和錯誤處理。

# 4. 異常處理實例

以下是一個簡單的範例，示範了`try-catch`語句區塊的用法。我們將嘗試除以零，然後捕獲並處理引發的異常。

```c#
try
{
    int x = 10, y = 0;
    Console.WriteLine($"結果：{x / y}");  // 除以零

} catch (DivideByZeroException ex)
{
    Console.WriteLine($"除以0錯誤：{ex}");
} catch (Exception ex)
{
    Console.WriteLine($"發生異常：{ex}");
} finally
{
    Console.WriteLine("異常處理結束。");
}
```

在上述範例中，由於我們嘗試**除以零**，會引發`DivideByZeroException`異常。在`catch`區塊中，我們針對不同類型的異常提供了不同的處理邏輯，以及一個通用的異常處理區塊。最後，在`finally`區塊中執行了清理操作。

# 5. 總結

`try-catch`語句區塊是C#中用於異常處理的關鍵機制，可讓您在程式碼中捕獲並處理執行時間可能發生的例外狀況。透過正確使用`try-catch`語句區塊，您可以增強程式的穩定性和健壯性，從而避免程式在遇到錯誤時崩潰或產生不受控制的行為。在使用`try-catch`語句區塊時，應考慮使用特定的異常類型、避免過多的異常處理、使用`finally`區塊進行資源清理、記錄異常資訊以及建立自訂異常類別等最佳實踐，以確保您的程式碼具有良好的可讀性、可維護性和可靠性。


[CSDN - 【C# 基础精讲】try-catch语句块](https://blog.csdn.net/qq_21484461/article/details/132316349?ops_request_misc=%257B%2522request%255Fid%2522%253A%2522171518155616800184163052%2522%252C%2522scm%2522%253A%252220140713.130102334..%2522%257D&request_id=171518155616800184163052&biz_id=0&utm_medium=distribute.pc_search_result.none-task-blog-2~all~top_click~default-2-132316349-null-null.142^v100^pc_search_result_base5&utm_term=c%23%20try%20catch&spm=1018.2226.3001.4187)