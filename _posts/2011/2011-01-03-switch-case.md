---
layout: post
title: "[C# 筆記] switch-case"
date: 2011-01-03 10:10:00 +0800
categories: [Notes, C#]
tags: [C#]
---

Switch Case 
範圍為一個定值

範例一  
工資調薪考績評定ABCD
```c#
bool b = true;
int salary = 50000;
Console.WriteLine("請輸入張三考績評定 ABCD");
string level = Console.ReadLine()!;
switch (level)
{
    case "A":
        salary += 5000;
        break;
    case "B":
        salary += 2000;
        break;
    case "C": break;
    case "D":
        salary -= 2000;
        break;
    case "E":
        salary -= 5000;
        break;
    default:
        b = false;
        Console.WriteLine("輸入有誤，程式退出");
        break;
}

if (b) {
    Console.WriteLine($"張三明年的工資為:{salary}");
}
Console.ReadKey();
```

範例二  
成績評測ABCDE等級
```c#
Console.WriteLine("請輸入成績");
int score = Convert.ToInt32(Console.ReadLine()); //0-100

switch (score/10) //將範圍score 變成一個定值
{
    case 10: //case10,case9 要執行的程式是一樣的
    case 9:
        Console.WriteLine("A");
        break;
    case 8:
        Console.WriteLine("B");
        break;
    case 7:
        Console.WriteLine("C");
        break;
    case 6:
        Console.WriteLine("D");
        break;
    default:
        Console.WriteLine("E");
        break;
}
```
範例三  
輸入年份、月份，輸出該月份的天數。(要判斷閏年)
```c#
//輸入年份、月份，輸出該月份的天數。(要判斷閏年)
try
{
    Console.WriteLine("請輸入年份");
    int year = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("請輸入月份");
    try
    {
        int month = Convert.ToInt32(Console.ReadLine());
        if (month >= 1 && month <= 12)
        {
            int day;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    day = 31;
                    break;
                case 2:
                    //2月有閏月、平月，所以要判斷當年是不是閏年
                    if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0)) {
                        day = 29; //閏年
                    } else {
                        day = 28; //平年 
                    }
                    break;
                default:
                    day = 30;
                    break;
            }
            Console.WriteLine($"{year}年{month}有{day}天");
        } else //if月份不是1-12
        {
            Console.WriteLine("輸入的月份不符合要求，程式退出");
        }
    } catch //try月份
    {
        Console.WriteLine("輸入的月份有錯誤，程式退出");
    }
} catch //try年份
{
    Console.WriteLine("輸入的年份有錯誤，程式退出");
}
```