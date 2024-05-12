---
layout: post
title: "[C# 筆記] static靜態成員 & 實體成員"
date: 2021-05-10 24:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,static]
---


心得：只有在完全完全確定一個方法不會有結構調整，與系統中其他部分幾乎沒有關聯時，才可以考慮把它寫成靜態方法。
否則，不要用靜態方法！      

[靜態方法的三大問題：](https://blog.csdn.net/VoisSurTonChemin/article/details/125729755)         
- 問題 1：測試困難        
- 問題 2：不靈活
- 問題 3：靜態傳染        


> - 不必要的記憶體浪費      
> 因為 靜態並非 沒有實體，而是只有一個實體，在程式執行之初就建立，並佔用記憶體位置，而且一直存在        
> 當程式用上一堆靜態成員的時候，就造成不必要的記憶體浪費。      
>
> - 牽一髮而動全身      
> 會牽一髮而動全身，靜態成員是唯一的，只要是宣告成 `static` 且同名的成員，都是共用一區記憶體位置。


# static 優缺點
## static 的優點

- 不需要建立物件即可以直接使用。(不用`new` 就可以使用了)
- 常駐在記憶體中，在程式碼任何區域都可以直接存取。(在整個專案中「資源共享」)
(程式一開始就載入記憶體空間，不會自動進行消毀，直到程式關閉。)

## static 的缺點
- 會長時間佔用記憶體，直到程式關閉。
- 散佈於各使用區域共享記憶體，可以被隨意修改和使用，難以偵錯和測試。
- 由於「共享記憶體」，在資料取存時，「多執行緒」下有可能會造成 `Race Condition`，需再額外處理`Lock`。

[【C#】static 使用心得](https://clarklin.gitlab.io/2020/12/10/c-sharp-static/)


# 靜態成員 vs 非靜態成員(實體成員)

- 靜態的成員 不需要實體(`Instance`) 就能進行訪問。
- 非靜態的成員 必須 `new` 一個 實體(`Instance`) 才能進行訪問。

- 靜態方法 屬於 類別(`Class`)所有。
- 非靜態方法 屬於 實體(`Instance`)所有。

- 靜態成員使用「類別名.靜態成員」去調用。
- 實體成員使用「物件名.實體成員」去調用。

- 實體函數中，可以使用靜態成員，也可以使用實體成員。(實體成員+靜態成員)
- 靜態函數中，只能訪問靜態成員，不允許訪問實體成員。(only 靜態成員)

> - 靜態成員必須使用「類別名」去調用，而實體成員使用「物件名」調用。        
> - 靜態函數中，只能訪問靜態成員，不允許訪問實體成員。      
> - 實體函數中，既可以使用靜態成員，也可以使用實體成員。        
> - 靜態類別中，只允許有靜態成員，不允許出現實體成員。(不能在靜態類別中宣告實體成員)    


[[C# 筆記] static 靜態與非靜態 by R](https://riivalin.github.io/posts/2011/01/static/)      


## 實體化

- 靜態成員屬於類別`Class`，而不屬於類別的實體`Instance`。可以透過類別名稱直接存取靜態成員，而不需要建立類別的實體。
- 非靜態成員屬於類別的實體。要存取非靜態成員，需要先建立類別的實體，然後透過實體來存取成員。

> - 靜態方法 屬於 類別(`Class`)所有。(使用「類別名.靜態成員」去調用)
> - 非靜態方法 屬於 實體(`Instance`)所有。(使用「物件名.實體成員」去調用)

## 記憶體分配

- 靜態成員在程式啟動時 就分配記憶體，並在程式結束時釋放。它們的生命週期 與應用程式的生命週期相同。      
- 非靜態成員在建立類別的實體時 才分配記憶體，並在實體被銷毀時釋放。它們的生命週期 與實體的生命週期相同。

## 訪問方式

- 靜態成員可以透過類別名稱直接訪問，也可以透過實體訪問。但強烈建議使用類別名稱來存取靜態成員，以明確它們的靜態性。      
- 非靜態成員只能透過實體存取。

> - 實體函數中，可以使用靜態成員，也可以使用實體成員。(實體成員+靜態成員)
> - 靜態函數中，只能訪問靜態成員，不允許訪問實體成員。(only 靜態成員)

## this 關鍵字

- 靜態成員中 不能使用 `this` 關鍵字，因為它們不屬於實體。
- 非靜態成員中 可以使用 `this` 關鍵字來引用目前實體。

```c#
public class Test
{
    public static int staticNumber = 10; //靜態成員
    public int InstanceNumber = 20; //非靜態成員

    //靜態方法
    public static void StaticMethod()
    { 
        // 靜態方法: 不能使用 this 關键字
        Console.WriteLine("Static Method:");
    }

    //非靜態方法
    public void InstanceMethod()
    {
        // 非靜態方法: 可以使用 this 關键字
        Console.WriteLine($"Instance Method: {this.InstanceNumber}");
    }
}
```

## 使用場景

- 靜態成員通常用於表示 與整個類別相關的資料或功能，例如共用的計數器、工廠方法等。
- 非靜態成員通常用來表示 實體特有的資料或功能，每個實體都有獨立的值。


[[C# 筆記] 靜態成員和非靜態成員的區別  by R](https://riivalin.github.io/posts/2017/02/the-difference-between-static-members-and-non-static-members/)


# 什麼時候用靜態類別?
## 使用時機

- 如果你想要你的類別(`Class`)當做一個「工具」去使用(經常使用)，就可以考慮使用靜態類別。
- 靜態類別在整個專案中「資源共享」。(共享記憶體)
- 靜態類別在專案中儘量不要太多，因為它會消耗你的資源。(它一開始載入時就存在，所以就會佔據記憶體空間)


> - 「靜態類別」是佔記憶體。(程式一開始就載入記憶體)
> - 「`Class`類別」是不佔記憶體，「實體」是佔記憶體。(`new`實體化後才會記憶體)

## 物件實體方法與靜態方法的疑問

在設計類別時，如果會有需要讓用戶端（呼叫它的程式）直接使用而不必產生物件實體的話，可以把它設計為 static. static 也可以用來提供全域性（Global）的成員，包含變數，常數或函式 . 但它的缺點是在程式一開始就載進記憶體，直到程式終止，過多的靜態成員會佔住很多的記憶體 .     

如果你要讓類別必須在做一些事情（初始化，載入資料）才能給用戶端取用時，就不必設計為 `static`. 這樣它就只會在載入時才會佔用記憶體，並且會在類別被終止（設定物件變數為 `null` 或是呼叫自己的摧毀物件方法，例如 `Dispose()`）時會釋放出記憶體，缺點就是要呼叫前都要建立物件實體 .



[MSDN - 靜態類別和靜態類別成員 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members)
[一秒看破 static](http://weisnote.blogspot.com/2012/08/static.html)     
[【C#】static 使用心得](https://clarklin.gitlab.io/2020/12/10/c-sharp-static/)
[为什么应该少用静态（static）方法：静态方法的三大问题](https://blog.csdn.net/VoisSurTonChemin/article/details/125729755)        
[[C# 筆記] static 靜態與非靜態   by R](https://riivalin.github.io/posts/2011/01/static/)    
[[C# 筆記] class property method field review   by R](https://riivalin.github.io/posts/2011/01/review3/#調用實體方法靜態方法)    
[[C# 筆記] 靜態和非靜態的區別  by R](https://riivalin.github.io/posts/2011/03/static/)      
[[C# 筆記] 靜態成員和非靜態成員的區別  by R](https://riivalin.github.io/posts/2017/02/the-difference-between-static-members-and-non-static-members/)
