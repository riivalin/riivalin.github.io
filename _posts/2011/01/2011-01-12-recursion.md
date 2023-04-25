---
layout: post
title: "[C# 筆記] Recursion 方法的遞迴"
date: 2011-01-12 21:30:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 方法的遞迴
方法自己調用自己。

## 什麼時候會用到遞迴？
找出一個文件夾中所有的文件。

例如：需要一個方法，能夠找到一個指定的文件夾下所有的文件。

## 就算是遞迴，也是要有個條件跳出來，不然就會變成死循環
```c#
TellStory();

//錯誤寫法，死循環
void TellStory() {
    Console.WriteLine("從前從前");
    Console.WriteLine("有一座山");
    Console.WriteLine("山裡有間廟");
    Console.WriteLine("廟裡有兩個小和尚");
    TellStory();
}
```
## 就算是遞迴，也是要有個條件跳出來
```c#
TellStory();

public static int i = 0;//給它一個靜態變數，記錄次數
public static void TellStory()
{
    //開始講故事
    Console.WriteLine("從前從前");
    Console.WriteLine("有一座山");
    Console.WriteLine("山裡有間廟");
    Console.WriteLine("廟裡有兩個小和尚");
    i++; //講一次就加1

    //如果講超過10遍，就不講了，跳出方法
    if (i > 10) return;

    //使用遞迴，自己呼叫自己
    TellStory();
}
```

