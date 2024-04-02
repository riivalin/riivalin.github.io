---
layout: post
title: "[C# 筆記] 練習：串接API + Telegram Bot 發送訊息 + 每隔一段時間自動執行(PeriodicTimer定時器)"
date: 2023-11-05 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,api,Telegram Bot,tg,timer,PeriodicTimer,async/await,random]
---

練習：
- 串接 `API` - 以 `URL` 取得 `JSON` 資料並反序列化(`Deserialize`) 為物件
- 透過 `Telegram Bot` 發送訊息
- 每隔一段時間自動執行某個方法(使用`PeriodicTimer`異步化定時器)

---

# 串接 API - 以 URL 取得 JSON 資料並反序列化(Deserialize) 為物件

[[C# 筆記] 練習串接 API - 以 URL 取得 JSON 資料並反序列化(Deserialize) 為物件](https://riivalin.github.io/posts/2023/11/practice-concatenation-api-json/) 


## 把 JSON 資料轉換為對應的 Class
## 利用網站 [JSON Editor Online](https://jsoneditoronline.org/#left=local.xiboba) 顯示查看了 `JSON` 資料樹狀圖。
![](/assets/img/post/json-editor-online.png)

## 建立對應的類別(class)

1. 用一個偷懶的方式去建立類別，複製 JSON 字串裡的一個物件的資料        
使用「編輯 > 選擇性貼上 > 貼上 JSON 做為類別」的方式來建立。        

2. 新增一個類別 Vocabulary.cs 的檔案 
3. 將游標移至你要產生類別的位置上，使用「編輯 > 選擇性貼上 > 貼上 JSON 做為類別」的方式來建立。

再稍微修改一下：把 Rootobject 改成 Vocabulary

```c#
public class Vocabulary
{
    public int letterCount { get; set; }
    public string word { get; set; } = string.Empty;
    public Definition[] definitions { get; set; } = new Definition[2];
}

public class Definition
{
    public string text { get; set; } = string.Empty;
    public string partOfSpeech { get; set; } = string.Empty;
}
```

## 以 URL 取得 JSON 資料

使用` HttpClient.GetStringAsync()` 取得指定 `URI` 所回傳的資料

```c#
static async Task Main(string[] args)
{
    //取得api數據
    var content = await FetchApiData("https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/6級.json");

    //將JSON 資料反序列化(Deserialize)為物件(自定義的類別Class)
    var data = JsonSerializer.Deserialize<List<Vocabulary>>(content)!;
}

//取得api數據
static async Task<string> FetchApiData(string url)
{
    //使用HttpClient發出Get請求，並接收回傳的內容
    HttpClient client = new HttpClient(); //建立HttpClient物件
    return await client.GetStringAsync(url); //接收回傳的內容
}
```

# 透過 Telegram Bot 發送訊息

[[C# 筆記] 透過 Telegram Bot 發送訊息](https://riivalin.github.io/posts/2023/11/how_to_send_a-telegram_message_in_csharp/) 


```c#
//發送TG訊息
static async Task SendMessage(string msg)
{
    var botClient = new Telegram.Bot.TelegramBotClient("999999:AAEqPs3--n5jAk2qhjg2YnPH0MflxbkWsoo");
    await botClient.SendTextMessageAsync(
        chatId: "-123456789",
        text: msg);
}

static async Task Main(string[] args)
{
    //取得api數據
    var content = await FetchApiData("https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/6級.json");

    //將JSON 資料反序列化(Deserialize)為物件(自定義的類別Class)
    var data = JsonSerializer.Deserialize<List<Vocabulary>>(content)!;

    //發送TG訊息
    await SendMessage("test send message"); 
}
```

組字串 - 使用`Random`隨機數取得某一單字

```c#
//使用隨機數取得某一單字
Random random = new Random(); //建立隨機數物件
int rndNum = random.Next(data.Count); //產生0~單字量之間的數字

//組字串
var s = $"{data[rndNum].word}\r\n{data[rndNum].definitions[0].text}\r\n{data[rndNum].definitions[0].partOfSpeech}"; 
```


# 每隔一段時間自動執行某個方法(使用 PeriodicTimer 異步化定時器)
## 什麼是 PeriodicTimer 異步化定時器

[[C# 筆記].Net6 新定时器 PeriodicTimer (異步化的定時器)](https://riivalin.github.io/posts/2023/11/timer-dot6-periodic-timer/)       

在.NET 6中引入了新`Timer`：`System.Threading.PeriodicTimer`，它和之前的`Timer`相比，最大的區別就是新的`PeriodicTimer`事件處理可以方便地使用異步，消除使用`callback`機制減少使用複雜度。

```c#
using PeriodicTimer timer = new(TimeSpan.FromSeconds(2));
while (await timer.WaitForNextTickAsync())
{
    Console.WriteLine(DateTime.UtcNow);
}
```

與Timer的區別

1. 消除了回呼,不再需要綁定事件

2. 不會發生重入，只允許有一個消費者，不允許同一個 `PeriodicTimer` 在不同的地方同時 `WaitForNextTickAsync` ，不需要自己做排他鎖來實現不能重入

3. 非同步化，之前的幾個 `timer` 的 `callback` 都是同步的，使用新的 `timer` 我們可以更好的使用非同步方法，避免寫 `Sync over Async` 之類的程式碼

## 加上`PeriodicTimer`異步化定時器，透過Telegram Bot定時發送單字

```c#
static async Task Main(string[] args)
{
    //取得api數據
    var content = await FetchApiData("https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/6級.json");

    //將JSON 資料反序列化(Deserialize)為物件(自定義的類別Class)
    var data = JsonSerializer.Deserialize<List<Vocabulary>>(content)!;

    //定時發送TG訊息
    using PeriodicTimer timer = new(TimeSpan.FromSeconds(2)); //每小時
    while (await timer.WaitForNextTickAsync())
    {
        
        //使用隨機數取得某一單字
        Random random = new Random(); //建立隨機數物件
        int rndNum = random.Next(data.Count); //產生0~單字量之間的數字

        //組字串(單字)
        var s = $"{data[rndNum].word}\r\n{data[rndNum].definitions[0].text}\r\n{data[rndNum].definitions[0].partOfSpeech}"; 

        //發送TG訊息
        await SendMessage(s); 
    } 
}
```

# Code

## Program.cs

```c#
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace _0402
{
    internal class Program
    {
        static void Log(string s)
        {
            Console.WriteLine(s);
        }
        static async Task Main(string[] args)
        {
            Log($"{DateTime.Now} - 開始取得api資料.");

            //取得api數據
            var content = await FetchApiData("https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/6級.json");

            Log($"{DateTime.Now} - 完成取得api資料.");

            Log($"{DateTime.Now} - 開始轉換 JSON 為 Object.");

            //將JSON 資料反序列化(Deserialize)為物件(自定義的類別Class)
            var data = JsonSerializer.Deserialize<List<Vocabulary>>(content)!;


            Log($"{DateTime.Now} - JSON 為 Object，並開始組字串(單字).");


            //定時發送TG訊息
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(2)); //每小時
            while (await timer.WaitForNextTickAsync())
            {
                
                //使用隨機數取得某一單字
                Random random = new Random(); //建立隨機數物件
                int rndNum = random.Next(data.Count); //產生0~單字量之間的數字
                var s = $"{data[rndNum].word}\r\n{data[rndNum].definitions[0].text}\r\n{data[rndNum].definitions[0].partOfSpeech}"; //組字串

                Log($"{DateTime.Now} - 要傳送的單字為：\r\n{s}\r\n");

                Log($"{DateTime.Now} - 開始發送TG訊息.");

                await SendMessage(s); //發送TG訊息

                Log($"{DateTime.Now} - 完成發送TG訊息.");

            }                
            //Console.ReadKey();//可以使窗口停留一下，直到點擊鍵盤任一鍵為止
        }

        //取得api數據
        static async Task<string> FetchApiData(string url)
        {
            //使用HttpClient發出Get請求，並接收回傳的內容
            HttpClient client = new HttpClient(); //建立HttpClient物件
            return await client.GetStringAsync(url); //接收回傳的內容
        }

        //發送TG訊息
        static async Task SendMessage(string msg)
        {
            var botClient = new Telegram.Bot.TelegramBotClient("6460822229:AAEqPs3--n5jAk2qhjg2YnPH0MflxbkWsoo");
            await botClient.SendTextMessageAsync(
                chatId: "-1002101862034",
                text: msg);
        }
    }
}
```

## Vocabulary.cs

```c#
public class Vocabulary
{
    public int letterCount { get; set; }
    public string word { get; set; } = string.Empty;
    public Definition[] definitions { get; set; } = new Definition[2];
}

public class Definition
{
    public string text { get; set; } = string.Empty;
    public string partOfSpeech { get; set; } = string.Empty;
}
```

執行結果：

```c#
2024/4/3 上午 03:25:26 - 開始取得api資料.
2024/4/3 上午 03:25:27 - 完成取得api資料.
2024/4/3 上午 03:25:27 - 開始轉換 JSON 為 Object.
2024/4/3 上午 03:25:27 - JSON 為 Object，並開始組字串(單字).
2024/4/3 上午 03:25:29 - 要傳送的單字為：
trek
(長途而辛苦的)旅行或移居
n

2024/4/3 上午 03:25:29 - 開始發送TG訊息.
2024/4/3 上午 03:25:31 - 完成發送TG訊息.
```


[不要寫「假的」非同步方法 - by Huanlin學習筆記](https://www.huanlintalk.com/2016/01/dont-write-fake-async-methods.html#google_vignette)     
[閱讀筆記 - 使用 .NET Async/Await 的常見錯誤 - by 黑暗執行緒](https://blog.darkthread.net/blog/common-async-await-mistakes/)        
[async 與 await - by Huanlin學習筆記](https://www.huanlintalk.com/2016/01/async-and-await.html#google_vignette)     
[.Net6 新特性 - PeriodicTimer - 异步化的定时器](https://blog.hwj.im/index.php/archives/17/)     
[[C# 筆記] 每隔一段時間自動執行某個方法(使用執行緒)](https://riivalin.github.io/posts/2023/11/thread-automatically-execute-a-method-every-once-in-a-while/)     
[[C# 筆記] 練習串接 API - 以 URL 取得 JSON 資料並反序列化(Deserialize) 為物件](https://riivalin.github.io/posts/2023/11/practice-concatenation-api-json/)       
[[C# 筆記] 透過 Telegram Bot 發送訊息](https://riivalin.github.io/posts/2023/11/how_to_send_a-telegram_message_in_csharp/)      
[[C# 筆記].Net6 新定时器 PeriodicTimer (異步化的定時器)](https://riivalin.github.io/posts/2023/11/timer-dot6-periodic-timer/)      
[[C# 筆記] 將主程式進入點 Main()方法改成 async 非同步](https://riivalin.github.io/posts/2023/10/async-main/)