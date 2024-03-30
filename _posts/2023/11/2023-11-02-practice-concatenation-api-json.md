---
layout: post
title: "[C# 筆記] 練習串接 API - 以 URL 取得 JSON 資料並反序列化(Deserialize) 為物件"
date: 2023-11-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,json,HttpClient,GetStringAsync,Deserialize]
---


以 URL 取得 JSON 資料後，將 JSON 資料反序列化(Deserialize)為自定義的物件(Object)。

# 以 URL 取得 JSON 資料
以 URL 取得 [JSON 資料](https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/國一.json)   


使用 `HttpClient.GetStringAsync()` 取得指定 `URI` 所回傳的資料

- [MSDN - HttpClient 類別 (System.Net.Http)](https://learn.microsoft.com/zh-tw/dotnet/api/system.net.http.httpclient?view=net-8.0&WT.mc_id=DT-MVP-4015686)    
- [MSDN - HttpClient.GetStringAsync 方法 (String) (System.Net.Http)](https://learn.microsoft.com/zh-tw/dotnet/api/system.net.http.httpclient.getstringasync?view=net-8.0&redirectedfrom=MSDN)


```c#
static async Task Main(string[] args)
{
    string jsonUrl = "https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/國一.json";
    
    HttpClient httpClient = new HttpClient(); //建立HttpClient物件(用來傳送 HTTP 要求)
    var content = await httpClient.GetStringAsync(jsonUrl); //回傳的內容
    Console.WriteLine(content); //輸出內容
}
```

執行結果：    
查看回應的JSON內容

![](/assets/img/post/json-cw-out.png)


# 把JSON資料轉換為對應的Class

## 利用網站 JSON Editor Online 顯示
從 [JSON Editor Online](https://jsoneditoronline.org/#left=local.xiboba) 網站上先查看了 JSON 資料樹狀圖：

![](/assets/img/post/json-editor-online.png)


## 建立對應的類別(class)

用一個偷懶的方式去建立類別，可以使用「編輯 > 選擇性貼上 > 貼上 JSON 做為類別」的方式來建立。        

1.複製 JSON 字串裡的一個物件的資料，如下:

```json
{
  "letterCount": 6,
  "word": "action",
  "definitions": [
    {
      "text": "動作，行動",
      "partOfSpeech": "n"
    }
  ]
}
```

2.新增一個類別 `Vocabulary.cs` 的檔案
3.將游標移至你要產生類別的位置上，使用「編輯 > 選擇性貼上 > 貼上 JSON 做為類別」的方式來建立。  

再稍微修改一下：把 Rootobject 改成 Vocabulary

```c#
namespace TgBotDemo
{
    public class Vocabulary //把 Rootobject 改成 Vocabulary
    {
        public int letterCount { get; set; }
        public string word { get; set; }
        public Definition[] definitions { get; set; }
    }

    public class Definition
    {
        public string text { get; set; }
        public string partOfSpeech { get; set; }
    }
}
```

# 將 JSON 資料反序列化(Deserialize)為物件(Object)

使用`JsonSerializer.Deserialize<T>`來反序列化`JSON` 字串，並轉為自己所定義的物件(Object)

[MSDN - JsonSerializer.Deserialize 方法(System.Text.Json): 如何將 JSON 讀取為 .NET 物件 (還原序列化)](https://learn.microsoft.com/zh-tw/dotnet/standard/serialization/system-text-json/deserialization)

```c#
//將JSON資料反序列化後，儲存在自己的變數
var data = JsonSerializer.Deserialize<List<Vocabulary>>(content);
```

```c#
static async Task Main(string[] args)
{
    //JSON資料的Url
    string jsonUrl = "https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/國一.json";

    //使用HttpClient發出Get請求，並接收回應的內容
    HttpClient httpClient = new HttpClient(); //建立HttpClient物件
    var content = await httpClient.GetStringAsync(jsonUrl); //接收回應的內容

    //將JSON資料反序列化後，儲存在自己的變數
    var data = JsonSerializer.Deserialize<List<Vocabulary>>(content);

    //輸出看結果
    foreach (var item in data) {
        Console.WriteLine(item.word);

        foreach (var e in item.definitions) {
            Console.WriteLine(e.text);
            Console.WriteLine(e.partOfSpeech);
        }
        Console.WriteLine();
    }
}
```

執行結果：

![](/assets/img/post/json-cw-deserialize-to-object.png)


# 英文單字的 JSON
## 國中1200 英文單字的 JSON
- 國一 JSON
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/國一.json>

- 國二 JSON
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/國二.json>

- 國三 JSON
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/國三.json>

## 高中7000 英文單字的 JSON
- 高中 1 級
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/1級.json>

- 高中 2 級
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/2級.json>

- 高中 3 級
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/3級.json>

- 高中 4 級
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/4級.json>

- 高中 5 級
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/5級.json>

- 高中 6 級
<https://raw.githubusercontent.com/AppPeterPan/TaiwanSchoolEnglishVocabulary/main/6級.json>

    
[MSDN - HttpClient 類別 (System.Net.Http)](https://learn.microsoft.com/zh-tw/dotnet/api/system.net.http.httpclient?view=net-8.0&WT.mc_id=DT-MVP-4015686)    
[MSDN - HttpClient.GetStringAsync 方法 (String) (System.Net.Http)](https://learn.microsoft.com/zh-tw/dotnet/api/system.net.http.httpclient.getstringasync?view=net-8.0&redirectedfrom=MSDN)   
[MSDN - JsonSerializer.Deserialize 方法(System.Text.Json): 如何將 JSON 讀取為 .NET 物件 (還原序列化)](https://learn.microsoft.com/zh-tw/dotnet/standard/serialization/system-text-json/deserialization)   
[#192 利用國中1200 和高中7000 的英文單字 JSON 開發單字 App](https://medium.com/彼得潘的試煉-勇者的-100-道-swift-ios-app-謎題/利用國中1200-和高中7000-的英文單字-json-開發單字-app-bdeb3c87c087)   
[ASP.NET MVC 使用政府公開資料 Part.1](https://kevintsengtw.blogspot.com/2013/11/aspnet-mvc-part1.html)    
[[C#]使用 HttpClient 的正確姿勢](https://jo-jo.medium.com/c-使用-httpclient-的正確姿勢-54c499ff3950)    