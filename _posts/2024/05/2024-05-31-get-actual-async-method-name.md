---
layout: post
title: 取得實際方法名稱（適用於async非同步方法）
date: 2024-05-31 06:23:00 +0800
categories: [Notes,C#]
tags: [C#,async,method-name]
---


MethodBase.GetMethodInfo() returns MoveNext

在 Visual Studio 2022 中，我編寫了以下 C# (.NET 6) 程式碼（如果重要的話，它位於從通用基底類別派生的非通用類別中）：

```c#
private async Task<bool> MyMethod()
{
    var method = MethodBase.GetCurrentMethod()!;
    string methodName = method.Name;
    Logger.LogInformation("Entering method {method}", methodName);
    // ... etc. ...
}
```

運行應用程式後，我檢查了日誌，發現它的方法名稱不是“MyMethod”，而是“MoveNext”。

        
### 方法實作為:

```c#
static string GetActualAsyncMethodName([CallerMemberName]string name = null) => name;
```

關於c# - 使用反射在非同步方法中取得方法名稱不會傳回預期結果，我們在Stack Overflow上找到一個類似的問題： https://stackoverflow.com/questions/41153628/


[c# - 使用反射在异步方法中获取方法名称不会返回预期结果](http://123.56.139.157:8082/article/23/8337662/detail.html)