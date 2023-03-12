---
layout: post
title: "C# Notes - Part Ⅲ"
date: 2018-01-03 00:00:10 +0800
categories: [Notes, C#]
tags: [C#]
---

## 字串 string
string 可以利用類似索引器`[index]`的語法，去訪問字串中的某個字符
```c#
//可以利用類似索引器的語法，去訪問字串中的某個字符
string s = "www.google.com";
Console.WriteLine(s[3]); //.
for (int i = 0; i < s.Length; i++) {
    Console.WriteLine(s[i]);
}
Console.ReadKey();
```
> 字串是引用類型(Reference Type)
> - 值類型(Value Type)：int, double, float, char, byte...(數字, 字元char)
> - 引用類型(Reference Type)：object, dynamic, string...(對象, 動態, 字串)

## CompareTo()比較字串的內容/大小
CompareTo()是按字符的ASCII值進行大小比較
會有三個返回值: 0, 1, -1 (相等, 大於, 小於)
```text
string s1,s2;
當 s1.CompareTo(s2)
s1=s2,返回0
s1>s2,返回1
s1<s2,返回-1
```
```c#
string s = "zbc";
int res1 = s.CompareTo("zbc"); 
Console.WriteLine(res1);
//0，"zbc"="zbc"，返回0

int res2 = s.CompareTo("zac"); 
Console.WriteLine(res2);
//1，"zbc">"zac"，返回1，第一個字符相同，比較第二個字符，b的ASCII大於a，返回1

int res3 = s.CompareTo("zcc"); 
Console.WriteLine(res3);
//-1，"zbc"<"zcc"，返回-1，第一個字符相同，比較第二個字符，b的ASCII小於c，返回-1

Console.ReadKey();
```
## Replace()指定字符換成另一個字符
```c#
s.Replace('.', '-'); //Replace(char,char)，單引號
s.Replace(".", "="); //Replace(string,string)，雙引號
```
```c#
string s = "www.google.com";
string newStr = s.Replace('.', '-'); //www-google-com
Console.WriteLine(s);
Console.WriteLine(newStr); 
Console.ReadKey();
```
## Split()依據指定的字符拆分字串
```c#
Split('.');
```
```c#
string[] strArray = s.Split('.'); //根據點(.)拆分字串
foreach (var item in strArray) {
    Console.WriteLine(item);
}
Console.ReadKey();
```
## Substring()從字串中截取某段字串
```c#
s.Substring(4, 6); //從第4個索引，取6個字符
s.Substring(4); //從第4個索引開始，到字串的結尾
```
```c#
string s = "www.google.com";
Console.WriteLine(s.Substring(4, 6)); //google
Console.WriteLine(s.Substring(4)); //google.tw
Console.ReadKey();
```
## IndexOf()用來判斷字串中是否包含某字串
可以使用這個方法來判斷當前的字符串，是否包含一個子字符串，如果不包含就返回-1，如果包含就返回第一個字符的索引值。
```c#
string s = "www.google.com";
int index = s.IndexOf("google"); //返回4，第一個字符的索引值
Console.WriteLine(index);

int index = s.IndexOf("googleQQQ"); //返回-1，不包含這個字串
Console.WriteLine(index);
```
## StringBuilder()可變動的字符串 (與string不同)
- StringBuilder()，用來處理動態字串(想要串連許多字串時)，效能比string高。
- string 是不可變的，string每次執行操作字符，實際上是建立新的字串對象, 用來處理固定字串。
> string s = "www.google.tw"; //這裡的string是System.String的別名，所以是小寫的

StringBuilder 三種創建初始化的方式：(要using System.Text)
```c#
//1.利用構造函數建構stringbuilder
StringBuilder sb = new StringBuilder("www.google.tw"); //要 using System.Text;
//2.初始一個空的stringbuilder對象，占有20個字符的大小
StringBuilder sb = new StringBuilder(20);
//3.1+2的結合
StringBuilder sb = new StringBuilder("www.google.com", 100);
```
**結論：處理動態字串用StuingBuilder(), 處理固定字串用string**
## StringBuilder()和string的區別

例如: 組字串時，如下，結果會一樣，但是：
```c#
StringBuilder sb = new StringBuilder("www.google.com", 100);
sb.Append("/xxx.html");
```
```c#
string s = "www.google.tw";
s = s + "/xxx.html";
```
- string 會新增一個內存，把原來字串內容複製過來，重新指向新的內存，舊的內存就會回收掉。
- StringBuilder() 直接在原本的內存中操作，不用再新增新的內存，再去移動舊的字串，效率比較高。

所以，當我們對一個字串進行頻繁的刪除添加操作的時候，使用StringBuilder效率比較高。

## StringBuilder()的 insert()新增方法
```c#
StringBuilder sb = new StringBuilder("www.google.com", 100);
sb.Insert(0, "http://"); //從索引第0的位置插入"http://"
Console.WriteLine(sb);
```

## StringBuilder()的 Remove()移除方法
```c#
StringBuilder sb = new StringBuilder("www.google.com", 100);
sb.Remove(0, 3); //從索引第0的位置移除3個字元，移除3w，".google.com"
Console.WriteLine(sb)
```
## StringBuilder()的 Replace()取代方法
```text
Replace(char,char), Replace(string,string)
```
```c#
StringBuilder sb = new StringBuilder("www.google.com", 100);
sb.Replace(".", "");//移除點，wwwgooglecom
sb.Replace('.', '-');//點.替換成-，www-google-com
Console.WriteLine(sb)
```
## 正則表達式-常用的 ^(開始)$(結束)
可以幫我們:
1. 檢索(搜索)：通過正則表達式，獲取我們要的字串。
2. 匹配：判斷給定的字串，是否符合正則表達式的過濾邏輯

常用的：
```text
^ 匹配開始
$ 匹配結束
```
**示例一: ^ 匹配開頭**
```c#
using System.Text.RegularExpressions;

string s = "I am blue cat.";
string res = Regex.Replace(s, "^", "開始：");//^定位開始位置, 替換成後面的字串
Console.WriteLine(res);
//輸出: 開始：I am blue cat.
```
**示例二: $ 匹配開頭**
```c#
string s = "I am blue cat.";
string res = Regex.Replace(s, "$", "結束");//$定位結尾位置, 替換成後面的字串
Console.WriteLine(res);
//輸出: I am blue cat.結束
```
## 正則表達式(^開始，\d數字,*全部, $結尾, \w, \W)

*0~多個
```text
\d數字
\w 大小寫字母、0 - 9的數字、底線_
\W 除「大小寫字母、0 - 9的數字、底線_」之外
```
**示例一：校驗只允許輸入數字**

如果沒有正則表達式，一般會用for迴圈來進行檢查，比較麻煩
```c#
string s = Console.ReadLine()!;
bool isMatch = true;
for (int i = 0; i < s.Length; i++) {
    if (s[i] < '0' || s[i] > '9') { //如果當前字元不是數字
        isMatch = false; break;
    }
}
if (isMatch)
    Console.WriteLine("合法字元");
else
    Console.WriteLine("不合法字元");
Console.ReadKey();
```
使用正則表達式 (^開始，\d數字,*全部, $結尾)
```c#
string s = Console.ReadLine()!;
string pattern = @"^\d*$"; //^開始，\d數字,*全部, $結尾
bool isMatch = Regex.IsMatch(s, pattern);
Console.WriteLine(isMatch);
Console.ReadKey();
```
> string pattern2 = @"a*";
> *星號代表，可以出現0~多個
> 也就是說，a可以不出現，或出現一次，或出現多次。

**示例二：校驗只允許輸入除了大小寫字母、0-9的數字、底線_以外的任何字**
\w 大小寫字母、0 - 9的數字、底線_
\W 除「大小寫字母、0 - 9的數字、底線_」之外
```c#
string pattern = @"^\W*$";
```
## 正則表達式-正字符组[ ]、[^]負字符(反義字符)
[ab]匹配括弧中的字符
[a-b]匹配括弧中的字符a到b
[^x]匹配除了x以外的任意字符
[^abwz]匹配除了abwz以外的任意字符

**示例：找到除了ahou以外的任意字，替換成*星號**
[abc] 匹配abc的字符
[^abc] 匹配除了abc的字符 (加上^變成反義)

```c#
string str = "I am a cat.";
string pattern = @"[^ahou]"; //加上^為反義，除了ahou以外的任意字
string s = Regex.Replace(str, pattern, "*"); //除了ahou以外的任意字取代成*
Console.WriteLine(s); 
//輸出：**a**a**a**
```
## 正則表達式-重複描述字符 {n}, {n,}, {n,m}
{n} 匹配前面字符n次
{n,} 匹配前面字符n次，或多於n次
{n,m} 匹配前面字符n到m次

**示例：校驗內容是否為合法的QQ號(QQ號為5-12數字)**
```
\d 匹配數字(0-9數字)
{5,12} 匹配前面字符5到12次
一定要用開頭^和結尾$做限制
```
```c#
string qq1 = "234322";
string qq2 = "234322234322";
string qq3 = "d234322";
string pattern = @"^\d{5,12}$"; //用開頭^和結尾$做限制，\d 0-9數字，{5,12}只能5-12個數字
Console.WriteLine(Regex.IsMatch(qq1, pattern));//true
Console.WriteLine(Regex.IsMatch(qq2, pattern));//true
Console.WriteLine(Regex.IsMatch(qq3, pattern));//true
```
## 正則表達式-擇一匹配、|OR邏輯運算

**示例: 查找數字或字母**
```text
| ：OR邏輯
\d：數字0-9
[a-z]：小寫字母a到z
```
```c#
string s = "12873!@$#%$%^的高獵高武";
string pattern = @"\d|[a-z]"; //\d數字0-9，|or邏輯，[a-z]字母a到z
MatchCollection col = Regex.Matches(s, pattern);
foreach (var item in col) {
    Console.WriteLine(item); //輸出所有match對象
}
//輸出：1283asdfaa
```
**示例: 將人名輸出(Rii;Lisa,JOJO.Ken)**
```text
[;,.]匹配這三個;,.字符
[;]|[,]|[.] 或是這樣寫,用擇一匹配，加上OR|邏輯運算
Regex.Split() 使用Split分割
```
```c#
string s = "Rii;Lisa,JOJO.Ken";
string pattern = @"[;,.]"; //匹配這三個;,.字符
//string pattern = @"[;]|[,]|[.]"; //或是這樣寫,用擇一匹配，加上OR|邏輯運算
string[] array = Regex.Split(s, pattern);
foreach (var item in array) {
    Console.WriteLine(item);
}
Console.ReadKey();
```
## 對正則表達式進行分組
**示例1：重複多個字符 使用(abcd){n}進行分組限定**
```text
"(ab\w{2}){2}"=="ab\w{2}ab\w{2}"  代表ab\w{2}重複2次
```
```c#
string s = Console.ReadLine()!;
string strGroup = @"(ab\w{2}){2}"; //=="ab\w{2}ab\w{2}"  代表ab\w{2}重複2次
Console.WriteLine($"分組字符重複2次取代為555，結果為：{Regex.Replace(s, strGroup, "555")}");
Console.ReadKey();
//輸入:12!@ABab__ab23aDSFWERW32eaabababab
//輸出:12!@AB555aDSFWERW32ea555
```
```text
\w 比對一個英文、數字或底線，相等於 /[A-Za-z0-9_]/
```