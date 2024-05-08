---
layout: post
title: "[C# 筆記] 例外處理 try catch finally "
date: 2021-04-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,例外處理(Exception Handling),try catch finally,throw]
---


# C# 常見的錯誤類型

- 語法錯誤(Syntax error)
- 編譯期錯誤(Complile time error)
- 邏輯錯誤(Logical error)
- 執行期錯誤(Runtime error)
- 數值性錯誤(Numerical error)


# 結構化例外處理 try...catch...finally

`try...catch...finally` 可以提供在`try`區域中擷取此程式碼區塊所有可能發生的錯誤，並使用`catch`抓取錯誤後，在`catch`區域中定義「例外處理」的程式式，最後`finally`區域所定義的程式碼，不管是否發生例外狀況，都一定會執行。通常`finally`區域是用來釋放資源的區域。     

這個 `try...catch` 的寫法可以這樣來理解：「嘗試執行一些程式碼，看看會不會出錯。萬一真的發生意外狀況，就中斷目前的正常流程，並且把捕捉到的例外交給 `catch` 區塊來繼續處理。」

```c#
try
{
    // 正常流程的程式碼
    // 擷取此程式碼區塊所有可能發生的錯誤
}
catch(Exception 例外處理變數名稱)
{
    // 意外狀況發生時，會執行此區塊的程式碼
    // 定義例外處理的程式碼
}
finally
{
    //最終必定會執行
    //釋放資源
    //例如:關閉檔案、關閉網路連線或資料庫連線等等。
}
```

> 在 C# 中，用來處理例外的關鍵字主要有四個：`try`、`catch`、`finally`、和 `throw`。     
> 還有例外篩選器（`exception filters`）、釋放資源的標準寫法（`using` 陳述句）、基礎類別 `System.Exception` 與其家族成員。


## 範例

```c#
Test(1, 2);

void Test(int x, int y)
{
    try
    {
        Console.WriteLine(x + y);
    } catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    } finally
    {
        Console.WriteLine("finally區域最後一定會被執行");
    }
}
```

執行結果：

```
3
finally區域最後一定會被執行
```


# try 區塊之後可以有多個 catch 區塊

實務上，我們還可以更細緻地針對不同類型的例外來個別處理。像這樣：

```c#
try
{
    ... // 正常流程的程式碼
}
catch (FileNotFoundException fileEx)
{
    ... // 處理「檔案找不到」的意外狀況。
}
catch (SqlException sqlEx)
{
    ... // 處理資料庫相關操作的意外狀況。
}
catch (Exception ex)
{
    ... // 前面幾張「網子」都沒抓到的漏網之魚，全都在此處理。
}
```

> `catch` 區塊之間的順序：愈特殊的例外類型要寫在愈上面，而愈是一般的例外類型應該寫在愈下面。

以上面的例子來說，如果把第一個和第三個 catch 區塊的位置交換，在語意上就會變成優先捕捉 Exception 類型的例外；由於 .NET 中的任何例外類型都是 Exception 的後代，所以任何例外狀況都會被 catch (Exception ex) 捕捉到，那麼寫在下方的其他 catch 區塊就等於完全沒作用了。不過你也不用太擔心自己會寫錯 catch 的順序，因為當編譯器發現這種情況時就會出現編譯失敗的錯誤訊息。


# finally 子句：最終必定會執行

try 區塊底下一定要跟著至少一個 catch 區塊，要不然就必須是 finally 區塊——其作用為：無論執行過程是否發生例外狀況，最終都要執行 finally 區塊中的程式碼。一種常見的寫法是在 finally 區塊中撰寫資源回收的工作，例如關閉檔案、關閉網路連線或資料庫連線等等。參考以下範例：

```c#
StreamReader reader = null;
try
{
    reader = new StreamReader("app.config");
    var content = reader.ReadToEnd();
    Console.WriteLine(content);
}
catch (FileIOException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    if (reader != null)
    {
        reader.Dispose();
    }    
}
```

程式碼說明：        

第 1～7 行：開啟一個文字檔，然後讀取檔案內容，再輸出至螢幕。如果一切順利，跳至第 3 步（finally 區塊）。如果讀取檔案的過程發生 FileIOException 類型的例外，則跳至相應的 catch 區塊。     
第 8～11 行：處理 FileIOException 類型的例外，並將錯誤訊息輸出至螢幕，然後進入 finally 區塊。       
第 12～18 行：關閉先前已經開啟的檔案，並釋放相關資源。      


有兩個細節值得提出來說一下。        

首先，try 區塊中的任何一行程式碼都有可能發生我們意想不到的錯誤，而當某一行程式碼出錯時，try 區塊中剩下的程式碼就不會被執行到，因為此時會立刻跳到某個 catch 區塊（如果有的話）；待某 catch 區塊執行完畢，便會執行 finally 區塊的程式碼。

其次，如果 try 區塊中的程式碼在執行過程中發生例外，而那個例外並沒有被任何 catch 區塊捕捉並處理，則程式會跳到 finally 區塊，等到此區塊的程式碼執行完畢，那個尚未被處理的例外依然存在，所以會被拋到上一層程式碼區塊。如果上一層程式碼也沒有捕捉並處理那個例外，則又會繼續往上拋，直到它被處理為止。要是某個例外狀況一路往上拋，直到應用程式最外層的主程式區塊都沒有被處理，此時應用程式可能就會意外中止，而使用者可能會看到程式底層 API 拋出的錯誤訊息。

簡言之，只要例外沒有被目前的程式區塊所捕捉並處理，就一定會往外層拋。因此，如果在 catch 區塊中的程式碼又引發了例外，那個新產生的例外自然也會往外層拋。實務上，在 catch 區塊中處理例外時，應注意避免再度引發新的例外（除非是刻意為之）。


# throw：拋出例外

我們已經看過例外處理的三個主要關鍵字：try、catch、finally。現在要介紹最後一個：throw。

當你在程式的某處需要引發例外來中斷正常流程時，便可以使用 throw 來拋出一個例外。範例：


```c#
void Print(string name)
{
    if (name == null)
    {
        throw new ArgumentNullException(nameof(name));
    }
    Console.WriteLine(name);
}
```


Print 方法會先檢查參數 name 是否為 null，如果是的話，便拋出一個 ArgumentNullException 類型的例外，讓呼叫端知道此函式堅決不接受傳入的參數為 null。

第 3～6 行程式碼也可以用一行解決：ArgumentNullException.ThrowIfNull(name);。

順便提及，當你需要拋出 NullReferenceException，除了用 throw new NullReferenceException()，也可以這樣寫：


```c#
throw null;
```

## 再度拋出例外

你也可以在 catch 區塊裡面使用 throw 來將目前的例外再度拋出：

```c#
try
{
    DoSomething();
}
catch (Exception ex)
{
    Log(ex); // 把當前的例外資訊寫入 log。
    throw;   // 再次拋出同一個例外。
}
```

這裡有個細節值得一提：在上述範例中，如果把倒數第二行寫成 throw ex 也可以通過編譯，但其作用稍微不同：

- 單單寫 throw 會保留原本的堆疊追蹤資訊（stack trace）。也就是說，透過堆疊追蹤資訊，便能抓到原始的例外。        
- 如果寫 throw ex，則會重設堆疊追蹤資訊，這將導致呼叫端無法透過例外物件的 StackTrace 屬性得知引發例外的源頭是哪一行程式碼。基於此緣故，通常不建議採用此寫法。       


於 catch 區塊中再次拋出例外時，你也可以拋出一個新的、不同類型的例外：

```c#
DateTime StringToDate(string input)
{
    try
    {
        return Convert.ToDateTime(input);
    }
    catch (FormatException ex)
    {
        throw new ArgumentException($"無效的引數: {nameof(input)}", ex);
    }
}
```

請注意上述範例在拋出一個新的 ArgumentException 時，有把當前的例外 ex 傳入建構子的第二個參數，這會把當前的例外保存於新例外的 InnerException 屬性。也就是說，在拋出新例外的同時，依然保存原始的例外資訊（也許呼叫端在診斷錯誤的時候會用到）。

一般來說，以下幾種場合會在 catch 區塊中拋出不同的例外：     

- 在處理當前的例外時，又發生了其他意外狀況。        
- 讓外層接收到更一般的例外類型，以便隱藏底層的細節（不想讓外界知道太多、避免駭客攻擊）。        
- 讓外層接收到更特殊的例外類型，以便呼叫端更明確知道發生錯誤的原因。        


[C# 例外處理（Exception Handling） by huanlintalk](https://www.huanlintalk.com/2022/09/csharp-exception-handling.html)      