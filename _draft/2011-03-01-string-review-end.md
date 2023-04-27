---
layout: post
title: "[C# 筆記] string 字串複習-End"
date: 2011-03-01 23:53:00 +0800
categories: [Notes,C#]
tags: [C#,string,string.Join,Split]
---

## 練習1: 把類似的字串中重複的符號去掉
"123-456-789---123-2 " => "123-456-789-123-2" `.split()`

思路
- `split()`把所有的橫線全都幹掉
- 再用 `join`把他們連起來

```c#
//123 - 456 - 789-- - 123 - 2 把類似的字串中重複的符號去掉
//123-456-789-123-2 .split()

string str = "123 - 456 - 789-- - 123 - 2";
string[] chs = str.Split(new char[] { '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
string strNew = string.Join("-", chs);
Console.WriteLine(strNew);
Console.ReadKey();
```

## 練習2:一txt文件，求員工中最高、最低、平均的工資

- 要拿到每一行的的內容`File.ReadAllLines`   
- `int max = int.MinValue;` 宣告最大值，給一個假定值(假定值為最小值)    
- i`nt min = int.MaxValue;` 宣告最小值，給一個假定值(假定值為最大值)    

```c#
string[] str = File.ReadAllLines(@"C:\Users\rivalin\Desktop\工資.txt");
int max = int.MinValue; //最大值給一個假定值(假定值為最小值)
int min = int.MaxValue; //最小值給一個假定值(假定值為最大值)
int sum = 0;
for (int i = 0; i < str.Length; i++)
{
    string[] s = str[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    if (int.Parse(s[1]) > max) max = int.Parse(s[1]);
    if (int.Parse(s[1]) < min) min = int.Parse(s[1]);
    sum += int.Parse(s[1]);
}

Console.WriteLine($"工資最高:{max}, 最低:{min}, 平均:{sum / str.Length}");
Console.ReadKey();
```