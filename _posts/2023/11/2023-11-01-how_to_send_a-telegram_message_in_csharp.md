---
layout: post
title: "[C# 筆記] 透過 Telegram Bot 發送訊息"
date: 2023-11-01 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,tg,telegram bot]
---

建立 Telegram Bot，並以 C# 透過 Private Channel 來發送訊息

## Preparation

- 一個 Telegram 帳戶

## New a Bot

1. 私訊 `@BotFather` 並輸入 `/newbot`，接著依序輸入 Bot 的名稱、ID，需要注意的是 ID 必須以 bot 結尾，如：`example_bot`
2. 若建立成功，`@BotFather` 會給予我們一個 `Token`，往後我們便可利用此 Token 透過 HTTPS 發送 Request

## Create Channel

由於 `Telegram Bot` 無法直接私訊使用者，因此必須透過頻道（`Channel`）來廣播。

1. 至 Telegram Chats 畫面，點擊右上角的 New Message 圖示
點擊 New Channel    
2. 輸入 Channel Name，Description 為選填    
3. 這裡要選擇 Channel Type，因為等等會需要找 Channel Numeric ID，所以暫時先設定為 Public，並給予該 Channel 一個 ID（也就是網址的後綴）  
4. 接著我們可以選擇是否要將其他使用者加入 Channel，如果不需要則直接 Next    
5. Channel 建立完成 

## Add the Bot to Channel

1. 進入 Channel 聊天室窗並點擊右上角的圖示
2. 點擊 Admin，之後點擊 Add Admin
3. 在搜尋欄位輸入 Bot ID，記得要加 @ 符號，如：`@example_bot`
4. 加入 Bot 並設定權限，以本例來說只需要給予 Post Messages 權限即可
5. 完成加入

## Get the Channel Numeric ID

如果是 Public Channel，我們可以直接透過 Channel ID 來發送訊息。但若是 Private Channel，因為沒有 Channel ID，所以我們必須取得 Channel 的 Numeric ID。        

我們先在瀏覽器上輸入這段網址：https://api.telegram.org/bot{token}/sendMessage?chat_id={@channelId}&text=Hello+World，若前面操作步驟都正確，則應該會回傳一 JSON 如：     


```json
{
    "ok":true,
    "result":{
        "message_id":3,
        "sender_chat":{
            "id":-123456789,
            "title":"Exmaple Channel",
            "username":"exmapleChannel",
            "type":"channel",
        },
        "date":1711527849,
        "text":"Hello World",
    }
}
```

其中的 result->chat->id 便是 Channel 的 Numeric ID。在取得這 ID 之後便可以把 Channel 改為 Private。改為 Private 的方式很簡單，把 Share Link 刪掉就可以了！


## Quickstart

先建立一個 ConsoleApplication 專案，並從 `Nuget` 將 `Telegram.Bot` 加入至專案，此程式碼透過呼叫 Bot API `getMe()` 方法，根據其存取 `Token` 來獲取 Bot 資訊。程式碼如下：

```c#
internal class Program
{
    static async Task Main(string[] args)
    {
        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
    }
}
```

執行結果輸出：

```console
Hello, World! I am user 6460822229 and my name is rivademo.
```

## Send Message From Bot with C#

建立一個 ConsoleApplication 專案，並從 `Nuget` 將 `Telegram.Bot` 加入至專案。利用 Bot 發送訊息，程式碼如下：

```c#
namespace ConsoleApplication1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var botClient = new Telegram.Bot.TelegramBotClient("{Bot Token}");
            Message message = await botClient.SendTextMessageAsync(
                chatId: "{Channel Numeric ID}",
                text: "C# Test message");
        }
    }
}
```

## Code

```c#
using Telegram.Bot;
using Telegram.Bot.Types;

internal class Program
{
    static async Task Main(string[] args)
    {
        var botClient = new Telegram.Bot.TelegramBotClient("6460822229:AAEqPs3--n5jAk2qhjg2YnPH0MflxbkWsoo");
        Message message = await botClient.SendTextMessageAsync(
            chatId: "-1002101862034",
            text: "C# Test message");


        //var me = await botClient.GetMeAsync();
        //Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");
    }
}
```

[Telegram Bots Book](https://telegrambots.github.io)        
[A guide to Telegram.Bot library](https://telegrambots.github.io/book/1/quickstart.html)        
[C# 透過 Telegram Bot 發送訊息](https://blog.holey.cc/2017/08/30/csharp-send-messages-by-telegram-bot/)