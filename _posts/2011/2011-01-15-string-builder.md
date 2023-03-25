---
layout: post
title: "[C# 筆記] String Builder"
date: 2011-01-15 22:23:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## string 和 StringBuilder 運行時間比較

用`Stopwatch`看一下兩者的運行時間：
- str += i; 等20秒還沒好，時間非常久啊!!
- sb.Append(i); 執行 00:00:00.0001952  

```c#
using System.Diagnostics;

string str = null; //為空
Stopwatch sw = new Stopwatch(); //建立一個計時器，來記錄程式運行的時間
StringBuilder sb = new StringBuilder();
sw.Start(); //開始計時
for (int i = 0; i < 1000; i++)
{
    //str += i; //時間非常久啊!!等20秒還沒好
    sb.Append(i); //00:00:00.0001890
}
sw.Stop(); //結束計時
Console.WriteLine(sb.ToString()); //StringBuilder輸出字串
Console.WriteLine(sw.Elapsed());
Console.ReadKey();
```

## string 和 StringBuilder 差別
string 為什麼這麼慢？  
因為他要在內存開空間    

StringBuilder因為沒有開空間，所以特別快