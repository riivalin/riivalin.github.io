---
layout: post
title: "[C# 筆記] function 練習2"
date: 2011-01-12 23:00:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習：求一個字串陣列中最長的元素
用方法實現：有一個字串陣列
{ "李奧納多", "馬龍", "泰勒·斯威夫特", "湯姆·希德勒斯頓", "本尼迪克特·康伯巴奇" }
輸出最長的字串

```c#
string[] names = { "李奧納多", "馬龍", "泰勒·斯威夫特", "湯姆·希德勒斯頓", "本尼迪克特·康伯巴奇" };

string name = GetLongest(names);
Console.WriteLine(name);

/// <summary>
/// 求一個字串陣列中最長的元素
/// </summary>
/// <param name="s">字串陣列</param>
/// <returns>最長的元素</returns>
public static string GetLongest(string[] s)
{
    string max = s[0]; //先給一個假定元素
    for (int i = 1; i < s.Length; i++)
    {
        max = max.Length > s[i].Length ? max : s[i]; //Length是字串長度
    }
    return max;
}
```

## 練習：計算出一個整數陣列的平均值
用方法來實現：  
請計算出一個整數陣列的平均值，保留兩位小數  

兩位小數可以用`ToString("0.00")`,或是佔符位`{0:0.00}`
```c#
int[] nums = { 1, 2, 7};
string s = GetAvg(nums).ToString("0.00"); //保留兩位小數
double avg = Convert.ToDouble(s);
Console.WriteLine(avg);
Console.ReadKey();

/// <summary>
/// 求整數陣列的平均值
/// </summary>
/// <param name="nums">陣列</param>
/// <returns>平均值</returns>
public static double GetAvg(int[] nums) {
    double sum = 0;
    for (int i = 0; i < nums.Length; i++)
    {
        sum += nums[i];
    }
    return sum / nums.Length;
}
```
> 保留兩位小數可以用： `ToString("0.00")`或是`{0:0.00}`
```c#
double d = 1.348;
Console.WriteLine(d.ToString("0.00"));
Console.WriteLine("{0:0.00}",d);
```

## 練習：判斷用戶輸入的數字是不是質數
寫一個方法，用來判斷用戶輸入的數字是不是質數    
再寫一個方法，要求用戶只能輸入數字，輸入有誤，就一直讓用戶輸入  
```c#
while (true)
{
    Console.WriteLine("請輸入一個數字，我們將判斷您輸入的是不是質數");
    string s = Console.ReadLine()!;//接收用戶的輸入
    int num = GetNumber(s);//判斷是不是數字
    bool b = IsPrime(num);//判斷是不是質數
    Console.WriteLine(b);
}

/// <summary>
/// 判斷是不是質數
/// </summary>
/// <param name="num">要判斷的數字</param>
/// <returns>是質數:true, 不是質數:false</returns>
public static bool IsPrime(int num)
{
    //小於2不是質數
    if (num < 2) {
        return false;
    }

    //讓這個數從2開始除，除到自身的前一位
    for (int i = 2; i < num; i++) {
        if (num % i == 0) { //除盡了，不是質數
            return false; //給非質數準備的
        }
    }
    return true; //給質數準備的
}
```