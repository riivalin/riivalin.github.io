---
layout: post
title: "[C# 筆記] try throw 小技巧"
date: 2021-04-12 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,例外處理(Exception Handling),throw]
---

throw; preserves the original stack trace of the exception, which is stored in the Exception.StackTrace property.       
Opposite to that, throw e;      
updates the StackTrace property of e.       

`throw`, 保留異常的原始堆疊跟踪，該跟踪存儲在異常中。 `StackTrace`屬性。        
相反，`throw e`；更新`e`的`StackTrace`屬性。        


## 舉例

```c#
int a = 0; int b = 0;
 
//第一種: throw ex 
try
{
	int z = a / b;
}
catch (Exception ex)
{
	throw ex;
}
// 這種 throw ex；會將到現在為止的所有資訊清空，認為你catch到的異常已經被處理了，只不過處理過程中又拋出新的異常，從而找不到真正的錯誤源。

//第二種: throw
try
{
	int z = a / b;
}
catch (Exception ex)
{
	throw;
}

//第三種: throw (沒有寫異常變數)
try
{
	int z = a / b;
}
catch (Exception)
{
	throw;
}
 
//一般情況，正確的使用方法，是上面這兩種方式，這樣會保留原始的錯誤訊息，
//catch (Exception ex)和 catch (Exception ) 是一樣的, 直接 throw就可以了
```

總結：為了方便問題定位，上面三種`try catch` 寫法，沒有特殊邏輯處理推薦： 第二種或第三種，直接 `throw`就可以了。

[CSDN - C# try throw小技巧](https://blog.csdn.net/u012655702/article/details/135597716)