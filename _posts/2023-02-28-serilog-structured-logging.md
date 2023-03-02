---
layout: post
title: "[C#] Serilog 結構化日誌-初識"
date: 2023-02-28 10:00:00 +0800
categories: [Notes, NET, C#]
tags: [C#, Log, Serilog]
---

Text logging (本文日誌記錄):
```text
[21:49:12 INF] Processed {"Latitude": 25, "Longitude": 134} in 034 ms.
```
Structured logging (結構化日誌記錄): 
```json
{"@t":"2023-03-02T13:49:12.7714869Z","@mt":"Processed {@Position} in {Elapsed:000} ms.","@r":["034"],"Position":{"Latitude":25,"Longitude":134},"Elapsed":34}
```

***

### 練習[官方](https://serilog.net)提供的範例

```c#
var position = new { Latitude = 25, Longitude = 134 };
var elapsedMs = 34;

Log.Information("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);
```
- Position 前面的 @ 運算符，是告訴 Serilog 需要將傳入的對象序列化，而不是調用 ToString() 轉換它。
- Elapsed 後面的 :000 是一個標準的 .NET 格式字符串，它會影響屬性的呈現方式(034)。 

此範例記錄兩個屬性：Position 和 Elapsed，以及一個日誌事件：
```json
{
  "@t": "2023-03-02T13:49:12.7714869Z",
  "@mt": "Processed {@Position} in {Elapsed:000} ms.",
  "@r": ["034"],
  "Position": {
    "Latitude": 25,
    "Longitude": 134
  },
  "Elapsed": 34
}
```
- @t：Timestamp 事件時間
- @mt：message template 輸出原始定義的模板
- @r：Renderings 渲染後的效果
> Elapsed:000 => 使用<span style="color: red"> :000</span> (.NET 格式字符串格式) 後呈現 <span style="color: red">034</span>

範例中捕獲的 JSON 格式的屬性如下所示：
```text
"Position":{"Latitude":25,"Longitude":134},"Elapsed":34}
```
在Console控制台上顯示如下：
```text
[21:49:12 INF] Processed {"Latitude": 25, "Longitude": 134} in 034 ms.
```
***

### 練習一
```c#
var user = new { Id = 1001, Name = "Qoo" };
Log.Information("Created {@User} on {Datetmie}", user, DateTime.Now);
```

Text logging 顯示:
```text
[03:41:57 INF] Created {"Id": 1001, "Name": "Qoo"} on 03/03/2023 03:41:57
```
Structured logging 顯示: 
```json
{
  "@t": "2023-03-02T19:41:57.0306949Z",
  "@mt": "Created {@User} on {Datetmie}",
  "User": {
    "Id": 1001,
    "Name": "Qoo"
  },
  "Datetmie": "2023-03-03T03:41:57.0270496+08:00"
}
```
### 練習二
```c#
var fruit = new[] { "Apple", "Cherry", "Grape" };
Log.Information("I have {@Fruit}, total count {counts}", fruit, fruit.Count());
```
Text logging 顯示:
```text
[04:03:15 INF] I have ["Apple", "Cherry", "Grape"], total count 3
```
Structured logging 顯示: 
```json
{
  "@t": "2023-03-02T20:03:15.0121340Z",
  "@mt": "I have {@Fruit}, total count {counts}",
  "Fruit": [
    "Apple",
    "Cherry",
    "Grape"
  ],
  "counts": 3
}
```

### 完整程式碼

```c#
using Serilog;
using Serilog.Formatting.Compact;

// Create the Serilog logger, and configure the sinks
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(new CompactJsonFormatter(), "logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var count = 99;
var position = new { x = 123, y = 987 };
Log.Information("At {@Position} there are {Count} puppy.", position, count);

Log.CloseAndFlush();
```
Console 輸出顯示：
```text
[04:20:42 INF] At {"x": 123, "y": 987} there are 99 puppy.
```
Log檔寫入Json格式：
```json
{
  "@t": "2023-03-02T20:20:42.7987978Z",
  "@mt": "At {@Position} there are {Count} puppy.",
  "Position": {
    "x": 123,
    "y": 987
  },
  "Count": 99
}
```

### 養好習慣[將主程式用 try/catch/finally 包起來](https://riivalin.github.io/posts/serilog/)

```c#
using Serilog;

// Create the Serilog logger, and configure the sinks
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("Hey serilog, let's debgug division.");

int a = 10, b = 0;
// Wrap creating and running the host in a try-catch block
try 
{
    Log.Debug("Dividing {A} by {B}", a, b);
    Console.WriteLine(a / b);
} 
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
} 
finally
{   // Finally, once just before the application exits…
    Log.CloseAndFlush(); //程式結束前一定要呼叫它 (非常重要!!!)
}
```


Reference:
- [Serilog 官方文件](https://serilog.net)
- [Serialized data – structured logging concepts in .NET (6)](https://nblumhardt.com/2016/08/serialized-data-structured-logging-concepts-in-net-6/)
- [Message Templates](https://messagetemplates.org)
