---
layout: post
title: "[C# 筆記] Dictionary 字典集合"
date: 2011-01-19 22:39:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 字典集合

- `dic.Add(1, "AAA");` 用 Add 加入相同的key會拋異常
- `dic[1] = "我是新來的";` 用 index 索引的方式，不會拋異常，只會覆蓋掉
- `KeyValuePair<key,value>` 用 KeyValuePair 一對數據的方式來遍歷  

> KeyValuePair<key,value>    
這裡面key, value的類型要跟宣告的類型匹配   
Dictionary<int, string> => KeyValuePair<int, string>  

```c#
Dictionary<int, string> dic = new Dictionary<int, string>();

dic.Add(1, "張三");
dic.Add(2, "李四");
dic.Add(3, "老五");
//dic.Add(1, "AAA"); //用Add加入相同的key會拋異常

//使用索引方式就不會拋異常
dic[1] = "我是新來的"; //用index索引的方式，不會拋異常，只會覆蓋掉

//用 KeyValuePair 一對數據的方法來遍歷
foreach (KeyValuePair<int,string> kv in dic) {
    Console.WriteLine($"{kv.Key}---{kv.Value}"); //key:value
}

//不用這寫法，用 KeyValuePair
//foreach (var item in dic.Keys) {
//    Console.WriteLine($"{item}---{dic[item]}"); //key:value
//}

Console.ReadKey();
```
