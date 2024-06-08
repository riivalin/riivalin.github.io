---
layout: post
title: "[C# 練習] 洗牌程式"
date: 1999-02-01 06:30:00 +0800
categories: [Notes,C#]
tags: [洗牌程式]
---

![](/assets/img/post/shuffle-cards-code.png)



```c#
//宣告撲克牌一維陣列
int[] card = new int[52];
//初始化陣列值
for(int i = 0; i < card.Length; i++) {
    card[i] = i+1;
}

int temp, rndTemp;
Random rnd = new Random(); //宣告一個產生隨機數
for(int i = 0; i < card.Length; i++) {
    //產生0~51之間的亂數
    rndTemp = rnd.Next(0, card.Length);
    
    //將i位置上的牌，和產生的隨機數rndTemp位置的牌 交換
    temp = card[i];
    card[i] = card[rndTemp];
    card[rndTemp] = card[i];
}

for(int i = 0; i < card.Length; i++) {
    Console.WriteLine($"第{i+1}張牌是{card[i]}");
}
```