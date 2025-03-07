---
layout: post
title: "[C# 筆記] 猴子吃桃問題"
date: 2017-01-01 22:51:00 +0800
categories: [Notes,C#]
tags: [C#]
---
練習：
森林裡有一隻猴子和一堆桃子，猴子每天吃掉<span style="color: red;">桃子總數的一半</span>，把剩下一半中<span style="color: red;">扔掉一個壞的</span>，到**第七天**的時候，猴子睜開眼發現只剩下一個桃子，問森林裡剛開始有多少桃子？

思路：正的不行，反著來
```text
第7天剩一個桃子
第6天有 (1+1壞的) * 2 = 4
第5天有 (4+1壞的) * 2 = 10
第4天有 (10+1壞的) * 2 = 22
第3天有 (22+1壞的) * 2 = 46
第2天有 (46+1壞的) * 2 = 94
第1天有 (94+1壞的) * 2 = 190
```
分析：
- 宣告變數 peachNum=1，初始化為第7天的桃子數量
- 使用for循環，循環次數為6，對應第6天一直到第一天的計算
- 循環體中，使用總結的規律計算 peachNum = (peachNum + 1) * 2

```c#
int peachNum = 1; //第7天桃子數量
for (int i = 1; i <= 6; i++) {
    peachNum = (peachNum + 1) * 2;
}
Console.WriteLine(peachNum); //190
```

