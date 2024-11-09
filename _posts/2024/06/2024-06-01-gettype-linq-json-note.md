---
layout: post
title: "[C#] GetType: Language switching (dynamically obtained in three ways)"
date: 2024-06-01 06:23:00 +0800
categories: [Notes,C#]
tags: [C#,dynamic,動態,語系,GetType,TryGetValue,IDictionary,Dictionary,GetProperty]
---


Take notes for myself…

```c#
//lang switch 語系切換(三種方式動態取得)

var name = (string)((IDictionary<string, object>)teamInfo.FirstOrDefault())[$"name_{lang}"] ?? teamInfo.FirstOrDefault().name_en;

var name = x.GetType().GetProperty($"Name{lang}").GetValue(x, null).ToString();

var name = (((IDictionary<string, object>)item).TryGetValue($"league_{lang}", out var value)) ? value : item.name_en;
```


<https://riivalin.github.io/posts/2024/05/linq-json-note>