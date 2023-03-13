---
layout: post
title: "[C# 筆記] 雞兔同籠問題"
date: 2017-01-04 23:51:00 +0800
categories: [Notes, C#]
tags: [C#]
---

已知雞兔一共30隻，腳共有90根，計算雞兔各有多少隻

分析-巢狀迴圈
- 迴圈遍歷兔子與雞數量所有可能
- 判斷兔子與雞數量相加是否為30隻，且同時滿足腳數量為90根

```c#
//數量一共30隻，腳數量一共90根，雞兔各自有幾隻
for (int chicken = 0; chicken <= 30; chicken++) //雞的可能數0~30隻
{
    for (int robbit = 0; robbit <= 30; robbit++)//兔的可能數0~30隻
    {
        if (chicken + robbit == 30 && chicken * 2 + robbit * 4 == 90) //滿足「數量一共30隻，腳數量一共90根」條件
        {
            Console.WriteLine($"雞有:{chicken}隻，兔子有:{robbit}隻");
        }
    }
}
Console.Read();
```