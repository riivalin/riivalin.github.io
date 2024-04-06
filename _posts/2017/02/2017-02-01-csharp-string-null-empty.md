---
layout: post
title: "[C# 筆記] 字串中 null、\"\"、string.Empty 的區別"
date: 2017-02-01 23:01:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,"null"]
---

在C#中，字串中 `string s = null`、`string s = ""`、`string s = string.Empty` 是用來表示字串「空值(`null`)」或「空字串(`""`、`string.Empty`)」的不同方式。       

# null、""、string.Empty 的區別

總結的來說，這三者的區別在於表示空字串的方式以及它們所處的上下文。      
- `null`：空值，「未分配記憶`」的引用
- `""`、`string.Empty`：空字串，「會分配儲存空間」，表示「`長度為零`」的字串，而`string.Empty`是`""`的更可讀的替代。

它們之間的差異如下： 

## string s = null (空值)

這表示字串變數 s 被初始化為 `null`，即它不引用任何對象，不會分配內存空間。這與空字串是不同的，因為空字串是一個具有「零長度的字串」。        

`null`表示引用未指向任何物件。在C#中，字串變數預設是`null`，除非明確地初始化為其他值或賦予一個字串物件。        
當字串變數為`null`時，它`沒有分配任何內存`，因此在嘗試存取其成員時會導致`NullReferenceException`異常。

## string s = string.Empty (空字串)

這表示字串變數 s 被初始化為「空字串」，即引用了一個具有「零長度的字串」對象。(這與`null`是不同的)       

`string.Empty`是一個靜態唯讀常數，用來表示一個「空字串」。它等同於`""`，都表示一個「長度為零」的字串。      
與`""`相比，使用`string.Empty`可能更具「可讀性」，因為它清晰地表明了意圖，即創建一個空字串。

## string s = "" (空字串)

`""`這也是將字串變數 s 被初始化為「空字串」，

`""`表示一個空字串，即「長度為零」的字串。它實際上是一個字串對象，但其包含的字元數量為零。      
使用`""`初始化的字串變數被認為是空字串。


> 將字串變數初始化為空字串。 若要測試字串的值是否為 `null` 或`String.Empty` ，請使用`IsNullOrEmpty` 方法
 

# string.Empty 和 string = "" 的區別

`string.Empty`是`string`類的一個靜態唯讀常數。      

`string.Empty`和`string=""`區別不大，因為 `string.Empty`內部實現是：

```c#
public static readonly string Empty;
//這就是String.Empty 唯讀的String類的成員，也是string的變數的默認值是什麼
 
//String的構造函數
static String(){
    Empty = ""; //Empty就是他""
    WhitespaceChars = new char[] {
        '\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',
        ' ', ' ', ' ', ' ', '', '\u2028', '\u2029', '　', ''
     };
}
```

# "" 和 String.Empty，為什麼推薦優先使用String.Empty ?

再看一段程式碼：

```c#
string s1 = "";
string s2 = string.Empty;
if (s1 == s2) {
    Console.WriteLine("一模一样！");
}   
// 结果都是True
Console.WriteLine("".Equals(string.Empty));
Console.WriteLine(object.ReferenceEquals(string.Empty, ""));
```

既然`string.Empty`和`string=""`一樣，同樣需要佔用記憶體空間，為什麼推薦優先使用`string.Empty` ?     

`string.Empty`只是讓程式碼好讀，防止程式碼產生歧義，比如說：        

`string s = ""`; `string s = " "`; 這個不細心看，很難看出是「空字串」還是「空格字元」。


如果判斷一個字串是否是空串，使用        
`if(s==String.Empty)`和`if(s=="")`的效率是一樣的，但是最有效率的寫法是`if(s.Length==0)`

`string.IsNullOrEmpty`的內部實作方式：
```c#

public static bool IsNullOrEmpty(string value) {
    if (value != null) {
        return (value.Length == 0);
    }
    return true;
}
``` 

而`string str=null`則是表示`str`未指向任何物件。        
將字串變數初始化為空字串。 若要測試字串的值是否為 `null` 或`String.Empty` ，請使用`IsNullOrEmpty` 方法。        



[MSDN - String.IsNullOrEmpty(String) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.string.isnullorempty?view=net-8.0)       
[C# .NET面试系列一：基础语法](https://blog.51cto.com/goodtimeggb/9869105?articleABtest=0)       
[String.Empty、string=”” 和null的区别](https://www.cnblogs.com/roboot/p/4783118.html)