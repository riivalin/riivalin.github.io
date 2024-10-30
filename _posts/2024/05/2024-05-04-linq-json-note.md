---
layout: post
title: "[C#] LINQ & JSON Note"
date: 2024-05-04 06:23:00 +0800
categories: [Notes,C#]
tags: [C#,json,Dictionary,DeserializeObject,JObject.Parse, GetProperty]
---

Take notes for myself...

```c#
// 將 JSON 字串反序列化為 StandingScore 類型的物件
var standings = JsonConvert.DeserializeObject<StandingScore>(datas.Standings);
```


```c#
// 使用反射根據指定語系（lang）動態取得比賽類型的屬性值
// 例如，如果 lang 是 "En"，則取得 "GroupNameEn" 屬性
GroupName = x.GetType().GetProperty($"GroupName{lang}").GetValue(x, null).ToString()
```

### 依指定語系lang 動態取得比賽類型



```c#
// 依指定語系lang 動態取得比賽類型
GroupName = (string)x.Score.FirstOrDefault()
                    .GetType()
                    .GetProperty($"GroupName{lang}")?
                    .GetValue(x.Score.FirstOrDefault())
```

# Dictionary<string, string>
使用 `JsonConvert.DeserializeObject<Dictionary<string, string>>` 進行 JSON 反序列化時，它會將 JSON 字串轉換為一個 Dictionary 物件，其中每個 JSON 屬性名稱（鍵）映射到一個字符串值。這是一種常用的方法來處理簡單的鍵值對(key-value)資料。

```c#
using Newtonsoft.Json;

// 假設有一個 JSON 字串
string jsonString = "{\"key1\":\"value1\", \"key2\":\"value2\"}";

// 反序列化 JSON 字串為 Dictionary
var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);

// 存取字典中的值
string value1 = dictionary["key1"]; // 存取 key1 的值
string value2 = dictionary["key2"]; // 存取 key2 的值
```

### 程式碼說明

引入命名空間：需要引入 Newtonsoft.Json 命名空間，以便使用 JsonConvert 類。      

定義 JSON 字串：這是待反序列化的 JSON 字串。它應符合 JSON 格式，通常以大括號 `{}` 包圍。

### 反序列化：

JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString) 方法將 JSON 字串轉換為 Dictionary<string, string>。       

每個 JSON 屬性名稱會成為字典的鍵，每個對應的值會成為字典的值。      

存取字典中的值：通過鍵名存取字典中的值，例如 `dictionary["key1"]` 將返回 "value1"。       


### 注意事項
- 鍵名的大小寫：字典中的鍵名是`區分大小寫`的，因此確保在存取時使用正確的鍵名。  

- JSON 格式：確保傳遞給 DeserializeObject 方法的字串是有效的 JSON 格式。如果 JSON 格式不正確，將引發異常。
- 空值處理：在存取字典中的值時，最好在使用之前檢查鍵是否存在，以避免引發 KeyNotFoundException。

### 適用場景
當您需要處理的 JSON 資料結構簡單且僅包含鍵值對時，使用 Dictionary<string, string> 是一個快速和靈活的選擇。
這種方法適合處理配置資料、簡單的表單資料等。對於更複雜的結構，建議定義相應的 C# 類進行反序列化。

## JObject.Parse

解析 JSON 字符串，獲取 "list" 中的第一個物件，並提取出來

```c#
var standings = JObject.Parse(datas.Standings)["list"]?.First;
```