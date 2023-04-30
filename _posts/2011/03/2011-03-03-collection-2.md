---
layout: post
title: "[C# 筆記] 泛型集合練習 2"
date: 2011-03-02 23:23:00 +0800
categories: [Notes,C#]
tags: [C#,List<T>,Dictionary<TKey,TValue>,泛型,泛型集合,KeyValuePair,Split,StringSplitOptions.RemoveEmptyEntries]
---

## 練習1：奇偶數的程用泛型實現，奇數在左，偶數在右
`int[] nums = {1,2,3,4,5,6,7,8,9};`

- 聲明兩個集合，一個存放奇數，一個存放偶數
- 遍歷每個元素，用2取餘數，整除為偶數
- 奇數在左，所以用奇數集合加偶數集合

```c#
int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
List<int> listJi = new List<int>();
List<int> listOu = new List<int>();
for (int i = 0; i < nums.Length; i++) {
    if (nums[i] % 2 == 0) {
        listOu.Add(nums[i]);
    } else {
        listJi.Add(nums[i]);
    }
}
listJi.AddRange(listOu);

//看結果
foreach (var item in listJi) {
    Console.WriteLine(item);
}
Console.ReadKey();
```

## 練習2：將int陣列中的奇數放到一個新的int陣列中返回
將陣列中的奇數取出來放到一個集合中，最終將集合轉換成陣列

```c#
int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
List<int> listJi = new List<int>();
for (int i = 0; i < nums.Length; i++) {
    if (nums[i] % 2 != 0) {
        listJi.Add(nums[i]);
    }
}
//轉換成陣列
int[] numsNew = listJi.ToArray();
foreach (var item in numsNew) {
    Console.WriteLine(item);
}
Console.ReadKey();
```

## 練習3：從一個整數的List<int>中取出最大數
聲明一個集合，並初始化賦值(加入整數)

### Max方法

```c#
List<int> list = new List<int>() { 1, 2, 3, 4, 85, 6 };
int max = list.Max();
Console.WriteLine(max);
Console.ReadKey();
```

### for迴圈

```c#
List<int> list = new List<int>() { 1, 2, 3, 4, 85, 6 };
int max = list[0];
for (int i = 0; i < list.Count-1; i++) {
    if (list[i] > max) max = list[i];
}
Console.WriteLine(max);
Console.ReadKey();
```

## 集合初始化
```c#
List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
```

## 物件初始化
```c#
Person p = new Person() { Name = "Rii", Age = 99 };
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

## 練習4：把123轉換為:壹貳參。Dictionary
- 字串中的空格先拿掉 `Split()`
- 再把陣列中每一個元素劈開

```c#
string str = "1一 2二 3三 4四 5五 6六 7七 8八 9九";
string[] strNew = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
Dictionary<char, char> dic = new Dictionary<char, char>();

for (int i = 0; i < strNew.Length; i++)
{
    //strNew[0] strNew[0][0]=1, strNew[0][1]=一
    dic.Add(strNew[i][0], strNew[i][1]);
}
//看結果
foreach (KeyValuePair<char, char> kv in dic)
{
    Console.WriteLine($"{kv.Key}-{kv.Value}");
}
//實作測試
Console.WriteLine("請輸入一個阿拉伯數字");
string input = Console.ReadLine();
for (int i = 0; i < input.Length; i++)
{
    if (dic.ContainsKey(input[i]))
    {
        Console.Write(dic[input[i]]); //依key找值
    } else {
        Console.Write(input[i]); //沒有的話，就輸出原值
    }
}
Console.ReadKey();
```

[https://www.bilibili.com/video/BV1vG411A7my?p=55](https://www.bilibili.com/video/BV1vG411A7my?p=55)