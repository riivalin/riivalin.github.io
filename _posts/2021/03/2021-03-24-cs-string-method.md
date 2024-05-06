---
layout: post
title: "[C# 筆記] 字串(String)常用方法"
date: 2021-03-24 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,string,string.Join,Split]
---


| 方法      | 說明             | 
|:----------|:-----------------|
| `Contains()`| `.Contains(要找的字)` \r\n 傳回 bool 值，這個值表示指定的子字串是否會出現在這個字串內。|
| `IndexOf()`| 指定字串或字元，有找到，返回第一個索引位置，沒找到返回 -1。\r\n  建議改 `Contains` 用 來改善可讀性。|
| `string.Join()`| 將陣列按照指定的字元串接，返回一個字串|
| `Replace()` |`.Replace(要換的字, 替換的字)` 替換 |
| `Split()`|分割字串|
| `Substring()` |`s.Substring(3,1);` 從第index為3開始截取字串，只取1個字元|
| `ToLower()` | 英文字母轉小寫|
| `ToUpper()`| 英文字母轉大寫|
| `Trim()`| 去除字串中的前後「空白字元」|


> [MSDN - CA2249：請考慮使用 String.Contains 而非 String.IndexOf](https://learn.microsoft.com/zh-tw/dotnet/fundamentals/code-analysis/quality-rules/ca2249)。此規則會找出用來檢查子字串是否存在的呼叫 `IndexOf` ，並建議改 `Contains` 用 來改善可讀性。

## 1、String.Contains

`.Contains(要找的字)`       

返回一个值，该值指示指定的字符是否出现在此字符串中。        
(傳回 bool 值，這個值表示指定的子字串是否會出現在這個字串內。)

```c#
string s1 = "The quick brown fox jumps over the lazy dog";
string s2 = "fox";
Console.WriteLine(s1.Contains(s2)); //true
```

## 2、String.IndexOf

指定 Unicode 字符或字符串在此实例中的第一个匹配项的从零开始的索引。 如果未在此实例中找到该字符或字符串，则此方法返回 -1

`.IndexOf(要找的字)`：要找的字，是在第一次出現的位置        
如果找不到，會回傳 -1       

```c#
string s = "abc123efg456";
Console.WriteLine(s.IndexOf('e')); //6，有找到，返回索引位置
Console.WriteLine(s.IndexOf('x')); //-1，沒找到，返回-1
Console.WriteLine(s.IndexOf("456")); //9，有找到，返回第一個索引位置
Console.WriteLine(s.IndexOf("789")); //-1
```

[MSDN - CA2249：請考慮使用 String.Contains 而非 String.IndexOf](https://learn.microsoft.com/zh-tw/dotnet/fundamentals/code-analysis/quality-rules/ca2249)       


## 3、String.Join

將陣列按照指定的字元串接，返回一個字串

```c#
string[] s = ["aa","bb", "cc"];
string newS = string.Join(',',s);
Console.WriteLine(newS); //aa,bb,cc
```

也可以這樣寫：

```c#
string s = string.Join('-',"123","456","789");
Console.WriteLine(s); //123-456-789
```

因為它是`params`參數

## 4、String.Replace
`.Replace(要換的字, 替換的字)` 替換       

返回一个新字符串，其中已将当前字符串中的指定 Unicode 字符或 String 的所有匹配项替换为其他指定的 Unicode 字符或 String。

```c#
string s = "abcd efa abc";
Console.WriteLine(s.Replace('a','A')); //Abcd efA Abc
Console.WriteLine(s.Replace("abc","XX")); //XXd efa XX
```

舉一反三：Contains() + Replace()

```c#
string s = "小六在哪裡？";
if(s.Contains("小六")) {
    s = s.Replace("小六","張三");
}
Console.WriteLine(s); //張三在哪裡？
```

## 5、String.Split
分割字串。      

返回的字符串数组包含此实例中的子字符串（由指定字符串或 Unicode 字符数组的元素分隔）。

```c#
string s = "aa,bb,cc,dd";
string[] ss = s.Split(','); //[aa,bb,cc,dd]
```

舉一反三：Split() + string.Join()       

將重複的符號拿掉 `a--b -c--23 --xx-yz` 改成 `a-b-c-23-xx-yz`      

```c#
string s = "a--b -c--23 --xx-yz";

//split()把所有的橫線全都拿掉，RemoveEmptyEntries把結果空的元素移除掉
string[] ss = s.Split(new char[] { '-', ' ' },StringSplitOptions.RemoveEmptyEntries);

//再用 join把他們連起來
s = string.Join('-', ss);

Console.WriteLine(s); //a-b-c-23-xx-yz
```

## 6、String.Substring

该方法用于截取字符串，有两个重载:       
- Substring(Int32) 
从此实例检索子字符串。 子字符串在指定的字符位置开始并一直到该字符串的末尾。
- Substring(Int32, Int32) 
从此实例检索子字符串。 子字符串从指定的字符位置开始且具有指定的长度。


例如：`s.Substring(3,1); `從第index為3開始截取字串，只取1個字元

## 範例

```c#
string s = "abcdefg";
Console.WriteLine(s.Substring(1)); //bcdefg
Console.WriteLine(s.Substring(2,3)); //cde
```

## 7、String.ToLower
返回此字符串转换为小写形式

```c#
string s = "abCdEFg";
Console.WriteLine(s.ToLower()); //abcdefg
```

## 8、String.ToUpper

返回此字符串转换为大写形式

```c#
string s = "abCdEFg";
Console.WriteLine(s.ToUpper()); //ABCDEFG
```

## 9、String.Trim

返回一个新字符串，删除了字符串前後的空白符。        
(去除字串中的前後「空白字元」)

```c#
string s = "   abCdEFg  ";
Console.WriteLine(s.Trim()); 
```

[MSDN - CA2249：請考慮使用 String.Contains 而非 String.IndexOf](https://learn.microsoft.com/zh-tw/dotnet/fundamentals/code-analysis/quality-rules/ca2249)       
[[C# 筆記] string 字串提供的各種方法 1 by R](https://riivalin.github.io/posts/2011/01/string3/)     
[[C# 筆記] string 字串提供的各種方法 2 by R](https://riivalin.github.io/posts/2011/01/string4/)     
[C# String 类在开发中常用到的方法汇总【详细版】](https://www.cnblogs.com/qingheshiguang/p/17965543)     
[.split() + string.Join()   by R](https://riivalin.github.io/posts/2011/03/string-review-end/)