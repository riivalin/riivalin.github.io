---
layout: post
title: "[C# 筆記] string 練習 2"
date: 2011-01-16 23:09:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習1：將"abc" 反轉 "cba" 輸出
### 方法一：倒循環
投機，只輸出，原值沒改變  

```c#
string s = "abcefg";
for (int i = s.Length-1; i >= 0; i--) {
    Console.Write(s[i]);
}
```

### 方法二： 兩兩元素交換(冒泡排序)

```c#
string s = "abcefg";
char[] chs = s.ToCharArray(); //先將字串轉成char陣列
for (int i = 0; i < chs.Length/2; i++) //循環Length/2次
{
    //最前跟最後元素，兩兩元素交換
    char temp = chs[i]; //第三方變數
    chs[i] = chs[chs.Length - 1-i]; 
    chs[chs.Length - 1 - i] = temp;
}
s = new string(chs);//轉回字串
Console.WriteLine(s);
Console.ReadKey();
```

### 方法二：ToCharArray + Array.Reverse + new string

```c#
string s = "abcefg";
char[] chs = s.ToCharArray(); //轉成char陣列
Array.Reverse(chs); //反轉
s = new string(chs); //轉回string
Console.WriteLine(s);
```

## 練習2：反轉單字順序
反轉單字順序"hello c sharp" -> "sharp c hello"

### 方法一： 兩兩元素交換(冒泡排序)
```c#
string s = "hello   c  sharp";
string[] strNew = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //空白分割，分割後的結果去掉空格

for (int i = 0; i < strNew.Length / 2; i++) //循環strNew.Length/2次
{
    //最前最後的兩兩元素交換
    string temp = strNew[i]; //第三方變數
    strNew[i] = strNew[strNew.Length - 1 - i];
    strNew[strNew.Length - 1 - i] = temp;
}
s = string.Join(" ", strNew); //將以空格連接將陣列轉回字串

Console.WriteLine(s);
Console.ReadKey();
```

## 練習3：從Email 中提取出用戶名首域名：abc@tt.cc
### 方法一：substring + indexOf

```c#
string email = "abc@tt.cc";
int index = email.IndexOf('@'); //@所在的位置index=3
string username = email.Substring(0, index); //從0開始取3個字元
string domainName = email.Substring(index + 1); //從4開始取

Console.WriteLine(username);
Console.WriteLine(domainName);
```

### 方法二：split
```c#
string email = "abc@tt.cc";
string[] str= email.Split('@');

string username = str[0]; //用戶名
string  domainName = str[1]; //域名

Console.WriteLine(username);
Console.WriteLine(domainName);
Console.ReadKey();
```
  
## 練習4：找出所有e的位置
"abcdeefgefefefgefefefefe"  

### 方法一：while 判斷
循環體：從上一前出現e的位置加1的位置，找下一次e出現的位置  
循環條件：index!=-1  

這種方式是需要掌握的，因為它適合多個單字、字串的搜尋  
```c#
string s = "abcedeefgefefefgefefefefe";
int index = s.IndexOf('e');
Console.WriteLine("第1次出現e的位置是{0}", index);

int count = 1; //記錄e出現的次數
while (index != -1) //index找不到會回傳-1
{
    count++;
    index = s.IndexOf('e', index + 1); //從上一次index+1 的位置開始找
    if (index == -1) break;

    Console.WriteLine("第{0}次出現e的位置是{1}",count, index);
}
Console.ReadKey();
```

### 舉例：找出所有abc字串的位置

這種方式是需要掌握的，因為它適合多個單字、字串的搜尋  
```c#
string s = "abcedeefgabcefefabccee";
int index = s.IndexOf("abc");
Console.WriteLine("第1次出現abc的位置是{0}", index);

int count = 1; //記錄e出現的次數
while (index != -1) //index找不到會回傳-1
{
    count++;
    index = s.IndexOf("abc", index + 1); //從上一次index+1 的位置開始找
    if (index == -1) break;

    Console.WriteLine("第{0}次出現abc的位置是{1}", count, index);
}
Console.ReadKey();
```

### 方法二： for 迴圈

```c#
string s = "abcedeefgefefefgefefefefe";
for (int i = 0; i < s.Length; i++) {
    if (s[i] == 'e') Console.WriteLine($"第{i+1}次出現e的位置是{i}");
}
```

## 練習5：判斷句中是否有「邪惡」，有的話就用「**」輸出
"老牛很邪惡"   

Contains + Replace
```c#
string s = "老牛很邪惡";
if (s.Contains("邪惡")) {
    s = s.Replace("邪惡", "**");
}
Console.WriteLine(s);
Console.ReadKey();
```

## 練習6：陣列用|連接，再分割
{ "AAA", "BBB", "CCC", "DDD", "EEE" }

```c#
string[] names = { "AAA", "BBB", "CCC", "DDD", "EEE" };
string str = string.Join('|', names);
string[] strNew = str.Split(new char[] {'|'},StringSplitOptions.RemoveEmptyEntries);
```