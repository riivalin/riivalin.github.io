---
layout: post
title: "[C# 筆記] ArrayList 集合練習"
date: 2011-01-17 23:07:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習1：建立一個集合，裡面添加一些數字，求平均值與和

```c#
using System.Collections;

ArrayList list = new ArrayList(); //建立arraylist物件
list.AddRange(new int[] { 1, 2, 3, 4, 5 }); //集合裡加一些數字

int sum = 0; //記錄總和
for (int i = 0; i < list.Count; i++) {
    sum += (int)list[i]!; //累加到總和中
}

Console.WriteLine(sum); //總和
Console.WriteLine(sum / list.Count); //平均
```
## 練習：承上，延伸 求最大值，最小值
```c#
using System.Collections;

ArrayList list = new ArrayList(); //建立arraylist物件
list.AddRange(new int[] { 1, 2, 3, 4, 5 }); //集合裡加一些數字

int sum = 0; //記錄總和
int max = (int)list[0]!; //最大值，先給一個假定值
int min = (int)list[0]!; //最小值，先給一個假定值

for (int i = 0; i < list.Count; i++)
{
    max = max > (int)list[i]! ? max : (int)list[i]!; //比較最大值
    min = min < (int)list[i]! ? min : (int)list[i]!; //比較最小值
    sum += (int)list[i]!; //累加到總和中
}

Console.WriteLine(sum); //總和
Console.WriteLine(sum / list.Count); //平均
Console.WriteLine(max); //最大值
Console.WriteLine(min); //最小值
```

## 練習2：寫一個長度為10的集合，要求在裡面隨機地存放10數字(0-9)，但是要求所有的數字不重複
```c#
using System.Collections;

ArrayList list = new ArrayList(); //建立集合物件
Random rnd = new Random(); //建立產生隨機數的物件

for (int i = 0; i < 10; i++) 
{
    int num = rnd.Next(0, 10); //產生0-9隨機數

    //集合中沒有這個隨機數
    if (!list.Contains(num))
    {
        list.Add(num); //加入隨機數
    } 
    else //集合中有這個隨機數
    {
        //一旦產生了重複的隨機數，這次的循環就不算數
        i--;
    }
}

//輸出看結果
for (int i = 0; i < list.Count; i++) {
    Console.WriteLine(list[i]);
}
Console.ReadKey();
```