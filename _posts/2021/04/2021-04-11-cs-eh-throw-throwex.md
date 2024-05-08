---
layout: post
title: "[C# 筆記] throw 和 throw ex 區別"
date: 2021-04-11 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,例外處理(Exception Handling),throw]
---


C#中使用`throw`和`throw ex`拋出異常，但二者是有區別的。(主要區別在`堆疊（stack）`訊息的起始點不同)       

在C#中推薦使用`throw；`來拋出異常；`throw ex；` 會將到現在為止的所有資訊清空，認為你`catch`到的異常已經被處理了，只不過處理過程中又拋出新的異常，從而找不到真正的錯誤來源(破壞堆疊追蹤)。

> `throw` 時會保留較完整的堆疊追蹤(`stack trace`), 而 `throw ex `會重置堆疊追蹤, 造成行數顯示在 `throw ex `那一行。


- `throw`：使用`throw`拋出錯誤時，並不會重置呼叫堆疊，保有完整`StackTrace`資訊，方便獲得例外發生點來進行除錯。(獲得完整呼叫堆疊資訊)    
- `throw ex`：當我們使用 `throw ex` 時，會重置呼叫堆疊，造成確切例外發生位置的遺失。例如，錯誤發生點明明就在 B()方法中，而`StackTrace`提供的資料卻只有到 A()方法，因此單就此訊息無法得知實際錯誤到底是在哪個類別方法中發生的，實在是會造成很大的困擾。          



## throw

用於拋出當前異常，並保留原始的異常堆疊資訊。當使用 throw 關鍵字時，當前異常的堆疊資訊將被保留，這對於偵錯和追蹤異常非常有用。

```c#
try
{
    // 一些可能引發異常的程式碼
}
catch (Exception ex)
{
    // 處理異常
    throw; // 重新抛出當前異常，保留原始的異常堆疊 StackTrace 資訊
}
```


## throw ex

也用於拋出當前異常，但會重置異常的堆疊資訊。當使用 `throw ex` 關鍵字時，目前異常的堆疊資訊將被重設為目前位置，而不是保留原始的異常堆疊資訊。這可能會導致調試和追蹤異常變得困難。

```c#
try
{
    // 一些可能引發異常的程式碼
}
catch (Exception ex)
{
    // 處理例外
    throw ex; // 重新拋出當前異常，重置異常的堆疊訊息
}
```

## throw 的用法

`throw` 的用法主要有以下四種：
1. `throw ex;` (會吃掉原始異常點)
2. `throw;` (會保留原始的異常堆疊 StackTrace 資訊)
3. 不帶異常參數的 `catch { throw; }`
4.  經過對異常重新包裝 `throw new Exception("經過進一步包裝的異常", ex);`

### 第一種（不建議使用，可惜很多人都一直這麼用的，包括俺，嘻嘻），這樣適用會吃掉原始異常點，重置堆疊中的異常起始點

```c#
try
{
}
catch (Exception ex)
{
    throw ex;
}
```

### 第二種，可追溯到原始異常點，不過編譯器會警告，定義的ex未有使用：

```c#
try
{
}
catch (Exception ex)
{
    throw;
}
```

### 第三種，不帶異常參數的，這個同第二種其實一樣，可捕捉所有類型的異常，IDE不會警告：

```c#
try
{
}
catch 
{
    throw;
}
```

其實第二種和第三種用法，書上也是不建議使用的，一般要從小粒度的異常捕獲開始，採用多個catch語句，大家就見仁見智吧。 。 。

### 第四種：經過對異常重新包裝，但會保留原始異常點資訊。推薦使用。

```c#
try
{
}
catch (Exception ex)
{
    throw new Exception("經過進一步包裝的異常", ex);
}
```


[[Tips][C#] 正確重拋例外 (Exception) 的方式](https://www.dotblogs.com.tw/wasichris/2015/06/07/151505#google_vignette)       
[CSDN - C#: throw和throw ex的区别](https://blog.csdn.net/lidandan2016/article/details/78864798)