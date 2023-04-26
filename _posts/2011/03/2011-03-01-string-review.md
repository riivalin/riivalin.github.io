---
layout: post
title: "[C# 筆記] string 字串複習"
date: 2011-03-01 22:13:00 +0800
categories: [Notes, C#]
tags: [C#,string,Split,StringComparison.OrdinalIgnoreCase,StringSplitOptions.RemoveEmptyEntries,Substring,new string()]
---

## 字串
### 字串的不可變性

字串 s1，表面上是給s1重新賦值，實際上，是重新開了一塊空間 存著"李四"，"張三"還在不在？ 在。那還有沒有人去指向這個"張三"？ 沒有。最終他被誰幹掉了？ GC。

```c#
string s1 = "張三"; //沒有人去指向它，GC處理掉
s1 = "李四";
```
> 什麼時候會用到GC？當我們的程序結束之後，GC會掃描著整個內存，發有像"張三"這種沒有被任何人所指向的空間，馬上消毀掉。  
> 但有些資源GC是沒有辦法幫我們釋放資源的，例如文件流`FileStream``，FileStream` GC是沒有辦法幫我們回收的，所以我們要寫在 `using() { }`裡面。


### 字串可以看做是char類型的唯讀陣列
字串可以看做是char類型的唯讀陣列，既然可以看做是陣列，那就可以透過index去訪問其中一個元素。

```c#
string s = "abc";
Console.WriteLine(s[0]); //輸出：a
Console.ReadKey();
```
因為是唯讀陣列，所以不能直接改變他`s[0]='A'`，如果真想要改變的話，要先把這個字串轉成真正的char類型的陣列，`s.ToCharArray()`改完後，還不會真正改變，還得將這個陣列轉成字串`new string(chs)`，重新賦值給他。

```c#
string s = "abc";
char[] chs = s.ToCharArray(); //將string轉換為char[]
chs[0] = 'b'; //改完後，還不會真正改變
s = new string(chs); //將這個陣列轉成字串,重新賦值給他
Console.WriteLine(s); //輸出：bbc
Console.ReadKey();
```

## 字串的方法
### IsNullOrEmpty
靜態方法，判斷為`null`或者為`""`

```c#
string s1 = "AA"; 
if (string.IsNullOrEmpty(s1))
{
    Console.WriteLine("Yes");
} else {
    Console.WriteLine("No");
}
Console.ReadKey();
```

### Equals 比較兩個字串是否相同
Equals 比較的時候，可以使用第二個參數 `OrdinalIgnoreCase` 忽略大小寫。

```c#
string s1 = "c#";
string s2 = "C#";
if (s1.Equals(s2, StringComparison.OrdinalIgnoreCase)) {
    Console.WriteLine("相同");
} else {
    Console.WriteLine("不相同");
}

Console.ReadKey();
```

### Contain 是否包含
### IndexOf 第一次出現的位置，如果沒有找到對應的數據，返回-1
### LastIndexOf 最後一次出現的位置，如果沒有找到對應的數據，返回-1

### Substring 截取字串
`s.Substring(3);` 從第index為3開始截取字串

```c#
string s = "abcde";
string sNew = s.Substring(3); //從第index為3開始截取字串
Console.WriteLine(sNew); //de
Console.ReadKey();
```


`s.Substring(3,1);` 從第index為3開始截取字串，只取1個字元

```c#
string s = "abcde";
string sNew = s.Substring(3,1); //從第index為3開始截取字串，只取1個字元
Console.WriteLine(sNew); //d
Console.ReadKey();
```

### Split 分割字串

```c#
string s = "abc as,d [ ]t- t d/";
//不要字元都打進去，RemoveEmptyEntries把結果空的元素移除掉
string[] sNew = s.Split(new char[] { ',', '[', ']', '-', '/' },StringSplitOptions.RemoveEmptyEntries);
```

### Join (靜態方法)
`string.Join`將指定的連接符/分隔符放到我們陣列的每個元素後面，返回一個字串。

```c#
string[] names = { "Ken", "Joe", "Qi", "Kenny" };
string str = string.Join("|", names);
Console.WriteLine(str); //輸出：Ken|Joe|Qi|Kenny
```
- 第二個參數是`object`類型，意味著什麼類型都可以。
- 承上，前面加了`params`任意個數，`params`意味著：
    - 第一，你可以放陣列進去
    - 第二，你可以放元素進去。這個元素是什麼類型呀？`object`什麼都可以。

```c#
//他會把你後面的元素，都當作陣列的元素
string str = string.Join("|", 'c', true, 2.34, 100, 100m, "張三");
Console.WriteLine(str); //輸出：c|True|2.34|100|100|張三
```

### Replace 舊字串替換成新字串

```c#
string str = "太陽好大";
string strNew = str.Replace("太陽", "雨");
Console.WriteLine(strNew); //輸出：雨好大
```

## 練習1:接收用戶輸入的字符串，將其中的字元以與輸入相反的順序輸出。 
"abc"->"cba"    

### 使用 Array.Reverse
```c#
string s = "abcde";
char[] chs = s.ToCharArray();
Array.Reverse(chs);
s = new string(chs);
Console.WriteLine(s);
Console.ReadKey();
```

### 兩元素進行交換
實現一個陣列當中兩元素全部交換循環多少次？陣列的長度/2
```c#
string s = "abcdefg";
char[] chs = s.ToCharArray();
for (int i = 0; i < chs.Length / 2; i++)
{
    char temp = chs[i]; //第三方變量
    chs[i] = chs[chs.Length - 1 - i];
    chs[chs.Length - 1 - i] = temp;
}
s = new string(chs);
Console.WriteLine(s);
```

### 使用for倒循環，只是內部印出來，不真正的交換
使用倒循環，本質沒變，只是簡單的輸出。

```c#
string s = "abcdefg";
for (int i = s.Length - 1; i >= 0; i--)
{
    Console.WriteLine(s[i]);
}
Console.ReadKey();
```

### 使用foreach
```c#
string s = "abcdefg";
char[] chs = new char[s.Length];
int i = s.Length - 1;
foreach (var item in s)
{
    chs[i] = item;
    i--;
}

//看結果
foreach (var item in chs) {
    Console.WriteLine(item);
}
```
## 練習2:一句英文，將其中的單調以反序輸出
"I love you" => "I evol uoy"

```c#
string str = "I love you";
string[] strNew = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
for (int i = 0; i < strNew.Length; i++)
{
    strNew[i] = ProcessStr(strNew[i]);
}
str = string.Join(" ", strNew);
Console.WriteLine(str);
Console.ReadKey();

public static string ProcessStr(string s)
{
    char[] chs = s.ToCharArray();
    for (int i = 0; i < chs.Length / 2; i++)
    {
        char temp = chs[i];
        chs[i] = chs[chs.Length - 1 - i];
        chs[chs.Length - 1 - i] = temp;
    }
    return new string(chs);
}
```

## 練習3:2012年1月12日，從日期字串中把年月日分別取出來

```c#
string date = "2012年1月12日";
string[] dateNew = date.Split(new char[] { '年', '月', '日' }, StringSplitOptions.RemoveEmptyEntries);
Console.WriteLine($"{dateNew[0]}-{dateNew[1]}-{dateNew[2]}");

Console.ReadKey();
```