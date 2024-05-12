---
layout: post
title: "[C# 筆記] 疊代器 (Iterators) - yield return 語法糖"
date: 2021-02-02 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,疊代器,iterators,yield return]
---

疊代器 (Iterators) 的出現是為了「讓`foreach` 語法更加靈活與簡單」。     

- `yield`關鍵字用於遍歷循環中
- `yield return`用於返回`IEnumerable<T>`
- `yield break`用於終止循環遍歷

當我們編寫 C# 程式碼時，經常需要處理大量的資料集合。在傳統的方式中，我們往往需要先將整個資料集合載入記憶體中，然後再進行操作。但是如果資料集合非常大，這種方式就會導致記憶體佔用過高，甚至可能導致程式崩潰。        

C# 中的`yield return`機制可以幫助我們解決這個問題。透過使用`yield return`，我們可以將資料集合按需生成，而不是一次性生成整個資料集合。這樣可以大大減少記憶體佔用，並且提高程式的效能。     

## Iterators 區塊

```c#
  public class MyList 
  {
      public IEnumerable<string> GetNames() 
      {
          yield return "Qoo";
          yield return "77";
          yield return "Rii";
      }
  }
```

## foreach 區塊

```c#
static void Main(string[] args)
{
    MyList list = new MyList();
    foreach (var item in list.GetNames()) {
        Console.WriteLine(item);
    }
}
```

## yield return 的工作方式

上面提到了`yield return`將資料集合按需生成，而不是一次生成整個資料集合。接下來透過一個簡單的範例，我們來看看它的工作方式是什麼樣的，以便加深對它的理解。        

```c#
static void Main(string[] args)
{
    foreach (var num in GetNumbers())
    {
        Console.WriteLine($"外部輸出：{num}");
    }
}

static IEnumerable<int> GetNumbers() {
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"內部遍歷了{i}");
        yield return i;
    }
}
```

### 輸出結果

首先，在 GetNumbers() 方法中，我們使用`yield return`關鍵字來定義一個疊代器。這個疊代器可以按需產生整數序列。在每次循環時，使用`yield return`傳回目前的整數。透過`foreach`循環來遍歷 GetNumbers() 方法傳回的整數序列。在疊代時 GetNumbers() 方法會被執行，但是不會將整個序列載入到記憶體中。而是在需要時，按需產生序列中的每個元素。在每次迭代時，會輸出目前疊代的整數對應的資訊。所以輸出的結果為：
```
內部遍歷了0
外部輸出：0
內部遍歷了1
外部輸出：1
內部遍歷了2
外部輸出：2
內部遍歷了3
外部輸出：3
內部遍歷了4
外部輸出：4
```

可以看到，整數序列是按需生成的，並且在每次生成時都會輸出相應的資訊。這種方式可以大大減少記憶體佔用，並且提高程式的效能。


## 範例

使用`yield return` 拿到所有的偶數，有結果立即回傳，提供更好的即時性，而不用一次性產生整個資料集合。

```c#
static void Main(string[] args)
{
    foreach (var item in GetAllEvenNumber())
    {
        Console.WriteLine(item);//輸出偶數
    }
}

//求1-100的偶數
//使用yield return 拿到所有的偶數
//有結果立即回傳，提供更好的即時性
//而不用一次性產生整個資料集合
static IEnumerable<int> GetAllEvenNumber() 
{
    for (int i = 0; i <= 100; i++)
    {
        if (i % 2 == 0) {
            yield return i;
        }
    }
}
```

## 結論

`Yield Return`關鍵字的作用就是退出目前函數，並且會保存目前函數執行到什麼地方，也就上下文。你會發現下次執行這個函數跟上次跑來的程式碼是不會重複執行的。      

但是一般的`return result` 假如你在循環體提前`return`，下面調這個函數是會從第一步開始重新執行的。不會記錄上次執行的地方。


## yield return 的優勢
善用 yield return 省時省 CPU 省 RAM，打造高效率程式

- 有結果立即回傳，提供更好的即時性 (這是要串接成生產線模式的必要條件)
- 只需部分結果時，省去處理無用資料的成本
- 不需耗用記憶體儲存全部結果

遇到結果筆數龐大或產生資料成本偏高的情境，善用這些優勢，將能打造出更有效率的系統。



[MSDN - Yield 陳述式 - 提供下一個元素](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/statements/yield)     
[善用 yield return 省時省 CPU 省 RAM，打造高效率程式](https://blog.darkthread.net/blog/yield-return/)      
[由C# yield return引发的思考](https://www.cnblogs.com/wucy/p/17443749.html)     
Book: Visual C# 2005 建構資訊系統實戰經典教本    