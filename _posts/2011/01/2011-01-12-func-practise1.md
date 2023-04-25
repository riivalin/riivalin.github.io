---
layout: post
title: "[C# 筆記] function+ref 方法綜合練習"
date: 2011-01-12 22:59:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 提示用戶輸入兩個數字，計算這兩個數字之間所有的整數和。
1. 用戶只能輸入數字
2. 計算兩個數字之間的和
3. 要求第一數字必須比第二個數字小，失敗就重新輸入

## 1.用戶只能輸入數字
```c#
//限定只能輸入數字
static int GetNmuber(string s)
{
    while (true)
    {
        try
        {
            int num = Convert.ToInt32(s);
            return num;

        } catch
        {
            Console.WriteLine("不是數字，請重新輸入:");
            s = Console.ReadLine()!; //重新接收用戶輸入
        }
    }
}
```

## 2.要求第一數字必須比第二個數字小，失敗就重新輸入
```c#
//判斷第一個數字是否小於第二個數字，失敗就重新輸入
static void JudgeNumber(ref int n1, ref int n2) //使用ref參數，可以將帶入方法中的值，帶出來
{
    while (true)
    {
        if (n1 < n2)
        {
            return; //符合意題
        } else //>2
        {
            Console.WriteLine("第一數字必須比第二個數字小，請重新輸入");
            string s1 = Console.ReadLine()!; //重新接收用戶輸入
            n1 = GetNmuber(s1);
            Console.WriteLine("請重新輸入第二個數字");
            string s2 = Console.ReadLine()!; //重新接收用戶輸入
            n2 = GetNmuber(s2);
        }
    }
}
```

## 3.計算兩個數字之間的和
```c#
//計算兩個數字之間的和
static int GetSum(int n1, int n2)
{
    int sum = 0;
    for (int i = n1; i <= n2; i++) {
        sum += i;
    }
    return sum;
}
```
## 執行
```c#
Console.WriteLine("請輸入兩個數字");
Console.WriteLine("第一個數字");
string strNumberOne = Console.ReadLine()!; //接收用戶輸入
int num1 = GetNmuber(strNumberOne); //用戶只能輸入數字檢查
Console.WriteLine("第二個數字");
string strNumberTwo = Console.ReadLine()!; //接收用戶輸入
int num2 = GetNmuber(strNumberTwo); //用戶只能輸入數字檢查

//判斷第一個數字是否小於第二個數字
JudgeNumber(ref num1, ref num2); //使用ref參數，可以將值帶入方法處理，再從方法帶出來

//計算兩個數之間的和
int sum = GetSum(num1, num2);

Console.WriteLine(sum);
Console.ReadKey();
```

## 完整程式碼
```c#
Console.WriteLine("請輸入兩個數字");
Console.WriteLine("第一個數字");
string strNumberOne = Console.ReadLine()!; //接收用戶輸入
int num1 = GetNmuber(strNumberOne); //用戶只能輸入數字檢查
Console.WriteLine("第二個數字");
string strNumberTwo = Console.ReadLine()!; //接收用戶輸入
int num2 = GetNmuber(strNumberTwo); //用戶只能輸入數字檢查

//判斷第一個數字是否小於第二個數字
JudgeNumber(ref num1, ref num2); //使用ref參數，可以將值帶入方法處理，再從方法帶出來

//計算兩個數之間的和
int sum = GetSum(num1, num2);

Console.WriteLine(sum);
Console.ReadKey();


//計算兩個數字之間的和
static int GetSum(int n1, int n2)
{
    int sum = 0;
    for (int i = n1; i <= n2; i++) {
        sum += i;
    }
    return sum;
}

//判斷第一個數字是否小於第二個數字，失敗就重新輸入
static void JudgeNumber(ref int n1, ref int n2) //使用ref參數，可以將帶入方法中的值，帶出來
{
    while (true)
    {
        if (n1 < n2)
        {
            return; //符合意題
        } else //>2
        {
            Console.WriteLine("第一數字必須比第二個數字小，請重新輸入");
            string s1 = Console.ReadLine()!; //重新接收用戶輸入
            n1 = GetNmuber(s1);
            Console.WriteLine("請重新輸入第二個數字");
            string s2 = Console.ReadLine()!; //重新接收用戶輸入
            n2 = GetNmuber(s2);
        }
    }
}


//限定只能輸入數字
static int GetNmuber(string s)
{
    while (true)
    {
        try
        {
            int num = Convert.ToInt32(s);
            return num;

        } catch
        {
            Console.WriteLine("不是數字，請重新輸入:");
            s = Console.ReadLine()!; //重新接收用戶輸入
        }
    }
}
```