---
layout: post
title: "[C# 筆記] null 和 \"\""
date: 2011-01-08 05:10:00 +0800
categories: [Notes, C#]
tags: [C#]
---

`null` 跟 `""`不同，    
`null` 沒有佔存空間，沒有值，  
`""` 有佔存空間，存了一個空。  

```c#
string str = null; //沒有開空間
string str = "";  //有開空間
```
> 等於null，等於空，兩者不一樣... 
