---
layout: post
title: "[C# 筆記] function 練習"
date: 2011-01-10 23:30:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 實參、形參
可以這樣理解「實參」,「形參」： 
- 形參：形式上的一個參數    
- 實參：實實在在的參數  

> 定義方法時，要求要給的值 `int GetMax(int x, int y)`   
> 調用方法時，實實在在給的值 `GetMax(10, 20);`  

<span style="color: red;">不管是「實參」還是「形參」，都是在內存開闢了空間。</span> 

- 方法的功能一定要單一。  
- 方法中最忌諱的就是，出現提示用戶輸入的字眼。

## 練習1：讀取輸入的整數，多次調用
讀取輸入的整數  
多次調用(如果用戶輸入的是數字，則返回，否則就提示用戶重新輸入)  

### 沒有寫成方法前，是這樣
```c#
while (true)
{
    Console.WriteLine("***請輸入一個整數**");
    try
    {
        //這段有可能會出異常，用try-catch包起來
        int number = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(number);
        break; //輸入的是數字，就不用再循環了，用break跳出循環
    } catch
    {
        Console.WriteLine("輸入有誤!!!");
    }
}
Console.ReadKey();
```

### 寫成方法
```c#
Console.WriteLine("***請輸入一個整數**");
string input = Console.ReadLine();
int number = GetNumber(input); //實參
Console.WriteLine(number);
Console.ReadKey();

/// <summary>
/// 這個方法需要判斷用戶的輸入是否是數字
/// 如果數字，則返回該數字
/// 如果不是數字，則提示用戶重新輸入
/// </summary>
/// <param name="s">用戶的輸入</param>
/// <returns>用戶輸入的數字</returns>
public static int GetNumber(string s) //形參
{
    while (true)
    {
        try
        {
            //這段可能會有異常，用try-catch包起來
            int number = Convert.ToInt32(s);
            return number;
        } catch
        {
            Console.WriteLine("輸入有誤!!!請重新輸入：");
            s = Console.ReadLine();
        }
    }
}
```
## 練習2：只允許用戶輸入y或n，改成方法
這個方法做了什麼事？
只能讓用戶輸入yes或no，只要不是就重新輸入
輸入yes 看 輸入no 不能看

```c#
Console.WriteLine("***請輸入yes或no");
string str = Console.ReadLine()!;
string result = IsYesOrNo(str);
Console.WriteLine(result);
Console.ReadKey();

/// <summary>
/// 限定用戶只能輸入yes或者no
/// </summary>
/// <param name="input">用戶輸入</param>
/// <returns>返回yes或no</returns>
public static string IsYesOrNo(string input)
{
    while (true)
    {
        if (input == "yes" || input == "no")
        {
            return input;
        } else
        {
            Console.WriteLine("**只能輸入yes或no, 請重新輸入**");
            input = Console.ReadLine()!; //重新接收用戶輸入
        }
    }
}
```

## 練習3：計算輸入的陣列總和
int[] nums = {1,2,3,4,5,6,7,8,9};

```c#
int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

int sum = R.GetSum(nums);
Console.WriteLine(sum);
Console.ReadKey()

/// <summary>
/// 計算一個整數類型陣列的總和
/// </summary>
/// <param name="nums">要求總和的陣列</param>
/// <returns>返回這個陣列的總和</returns>
public static int GetSum(int[] nums)
{
    int sum = 0;
    for (int i = 0; i < nums.Length; i++)
    {
        sum += nums[i];
    }
    return sum;
}
```
