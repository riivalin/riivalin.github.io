---
layout: post
title: json 序列化(Serialize) & 反序列化(Deserialize)
date: 2024-05-01 06:23:00 +0800
categories: [Notes,C#]
tags: [C#,json,序列化(Serialize),反序列化(Deserialize),Newtonsoft.Json,DeserializeObject,SerializeObject,JSON.parse(),JObject.Parse(),serialize,deserialize]
---


# 將 JSON 字串反序列化為 Object 物件

```c#
// 方法一：將 JSON 字串反序列化為 Person 類型的物件
var data = JsonConvert.DeserializeObject<Person>(jsonStr);  

// 方法二：將 JSON 字串解析為 JObject，並取得第一個屬性（直接存取內層的對應屬性）
var data = JObject.Parse(jsonStr).Properties().FirstOrDefault().Value.ToObject<IEnumerable<Person>>();  
```


# 範例
## 範例1：使用 Newtonsoft.Json 將 json 字串直接轉換成物件

`JsonConvert.DeserializeObject<Person>(jsonString)`

```c#
using System;
using Newtonsoft.Json;	

public class Program
{
	public class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}	

	public static void Main()
	{
		// JSON 字串轉換成物件
		string jsonString = "{\"Name\":\"John\",\"Age\":30}";
		Person person = JsonConvert.DeserializeObject<Person>(jsonString);

		// 檢查轉換結果
		Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
	}
}
```


## 範例2：將 JSON 字串轉換成物件，但忽略最外層的鍵(直接存取內層)

假設 json 結構：

```json
{
    "data": {
        "Name": "John",
        "Age": 30,
        "Address": {
            "Street": "123 Main St",
            "City": "New York"
        }
    }
}

```

將 JSON 字串轉換成物件，但忽略最外層的鍵，可以透過反序列化之後直接存取內層的對應屬性

```c#
// JSON 字串
string jsonString = @"{""data"":{""Name"":""John"",""Age"":30,""Address"":{""Street"":""123 Main St"",""City"":""New York""}}}";

//將 JSON 字串轉換成物件，但忽略最外層的鍵，可以透過反序列化之後直接存取內層data的對應屬性
var person = JObject.Parse(jsonString).Properties().FirstOrDefault().Value.ToObject<Person>(); //jsonObject["data"];

// 檢查結果
Console.WriteLine($"Name: {person.Name}, Age: {person.Age}");
Console.WriteLine($"Address: {person.Address.Street}, {person.Address.City}");

/*
Name: John, Age: 30
Address: 123 Main St, New York
*/

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Address Address { get; set; }
}
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
}
```

### 程式碼說明

如果想將 JSON 字串轉換成物件，但忽略最外層的鍵，可以透過反序列化之後直接存取內層的對應屬性。以下是具體做法。

```c#
// 將 JSON 字串解析為 JObject，並取得第一個屬性
var data = JObject.Parse(jsonStr)   // 將 JSON 字串轉換為 JObject
                 .Properties()      // 取得 JObject 中所有屬性
                 .FirstOrDefault()  // 取得第一個屬性（如果存在）
                 .Value             // 取得該屬性的值
                 .ToObject<IEnumerable<Person>>();  // 將該值反序列化為 IEnumerable<Person> 型別的物件
```

### 註解說明：

- `JObject.Parse(jsonStr)`：將 JSON 字串轉換成 `JObject`，這樣可以方便地操作 JSON 結構。
- `Properties()`：取得 JSON 中的所有屬性（鍵值對），例如：`{"key1": value1, "key2": value2}`，這些屬性可以透過 `Properties()` 來取得。
- `FirstOrDefault()`：從所有屬性中選取第一個屬性。如果 JSON 沒有屬性，它將返回 null。
- `Value`：取得該屬性的值（通常是內層的 JSON 對象）。
- `ToObject<IEnumerable<Person>>()`：將該值（JSON 結構）反序列化為 `IEnumerable<Person>` 型別的物件，也就是一個 Person 類型的集合。     

這段程式碼的目的是將 JSON 中第一個屬性對應的值轉換為 `IEnumerable<Person>` 物件。



# 將物件轉換成 JSON 字串

## 使用 `JsonConvert.SerializeObject`

```c#
// 檢查泛型 T 是否為 string 類型
if (typeof(T) == typeof(string))
{
    // 將物件轉換為 JSON 字串
    data = (T)Convert.ChangeType(JsonConvert.SerializeObject(jsonString), typeof(T));
}
```

### 程式碼說明：

- `JsonConvert.SerializeObject`：這個方法用於將物件轉換成 JSON 字串格式。
- `Convert.ChangeType`：仍然用來強制將結果轉換成泛型 T，這裡是針對 string 的轉換。
這樣就可以將物件轉換為 JSON 字串，並賦值給 data 變數。

## 使用 `Convert.ChangeType`

```c#
// 檢查泛型 T 是否為 string 類型
if (typeof(T) == typeof(string)) 
{
    // 如果 T 是 string 型別，將 jsonString 轉換為 T 型別 (也就是 string)
    data = (T)Convert.ChangeType(jsonString, typeof(T));
}
```

### 程式碼說明：
- `typeof(T) == typeof(string)`：檢查泛型 T 是否是 string 類型。如果 T 是 string，則執行後續的轉換。
- `Convert.ChangeType`：將 jsonString 轉換為指定的型別，這裡是將 jsonString 轉換為 T（即 string 型別）。
(T)：將轉換後的結果強制轉型為 T 型別，並賦值給 data。
此程式碼的主要目的是在泛型方法中，針對 string 類型進行特定處理，將 jsonString 轉換為 string 並賦值給 data。