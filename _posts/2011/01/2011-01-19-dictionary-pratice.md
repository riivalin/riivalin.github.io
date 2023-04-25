---
layout: post
title: "[C# 筆記] Dictionary 字典集合 練習"
date: 2011-01-19 22:49:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習1：將陣列中的奇數偶數各放在一個集合，再合併輸出顯示奇數左邊，偶數在右邊  
將一個陣列中的奇數放到一個集合中，再將偶數放到另一集合中  
最終將兩個集合合併為一個集合，並且奇數顯示在左邊，偶數顯示在右邊  
13572468  

```c#
int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8 };
List<int> oddList = new List<int>(); //奇數泛型集合
List<int> evenList = new List<int>(); //偶數泛型集合

//遍歷每一個陣列元素
for (int i = 0; i < nums.Length; i++)
{
    //判斷是否偶數
    if (nums[i] % 2 == 0) {    
        //是偶數
        evenList.Add(nums[i]); //加入偶數集合
    } else
    {   //是奇數
        oddList.Add(nums[i]);  //加入奇數集合
    }
}

//奇數加入偶數
oddList.AddRange(evenList); //奇數顯示在左邊，偶數顯示在右邊

//輸出看結果
foreach (var item in oddList) {
    Console.Write(item);
}
```
如果要偶數顯示在左邊，奇數顯示在右邊，則用偶數加入奇數`evenList.AddRange(oddList)`。

## 練習2：將用戶輸入一個字串，通過`foreach`循環將用戶輸入的字串賦值給一個字串陣列

```c#
string input = Console.ReadLine();
char[] chs = new char[input.Length]; //字串可以看作是唯讀char[]陣列，因為字串賦值給陣列，所以陣列長度可以為字串長度

int i = 0; //用來記錄index
foreach (var item in input) { //遍歷字串中的每一個字元
    chs[i] = item; //加入char[]，不用擔心會超出index，因為陣列長度=字串長度
    i++; //每賦值一次就加1
}

//輸出看結果
foreach (var item in chs) {
    Console.WriteLine(item);
}
```
- 字串可以看作是唯讀char[]陣列，因為字串賦值給陣列，所以陣列長度可以為字串長度。  
- 題目要求用`foreach`，所以需要宣告變數i來記錄index，每循環一次就i++。
- 字元加入char[]陣列，不用擔心會超出index，因為陣列長度設定是字串長度。


## 練習3：
統計 Welcome to China 中每個字元出現的次數，不考慮大小寫。  

```c#
//統計 Welcome to China 中每個字元出現的次數，不考慮大小寫。  
//字元---> 出現的次數  
//key ---> value
//w ---> 1
string str = " Welcome to China".ToLower(); //不分大小寫，就統一轉大寫或小寫
Dictionary<char, int> dic = new Dictionary<char, int>(); //建立字典集合物件
for (int i = 0; i < str.Length; i++)
{
    //dic裡不要空格
    if (str[i] == ' ') continue; //continue會強制結束當前的循環(以下程式碼不會執行)，強制開始下一次新的循環

    //如果dic已經包含了當前循環到的這個key
    if (dic.ContainsKey(str[i]))
    {  
        //值再加1
        dic[str[i]]++;
    } else
    {
        //這個字元在集合當中是第一次出現
        dic[str[i]] = 1;
    }
}
//輸出看結果
foreach (KeyValuePair<char,int> kv in dic)
{
    Console.WriteLine($"字母{kv.Key}出現了{kv.Value}次");
}
Console.ReadKey();
```
> `continue`會強制結束當前的循環(以下程式碼不會執行)，強制開始下一次新的循環