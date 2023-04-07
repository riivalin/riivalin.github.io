---
layout: post
title: "[C# 筆記] 為什麼要用 Delegate 委派？"
date: 2011-02-01 00:09:01 +0800
categories: [Notes, C#]
tags: [C#]
---

## 為什麼要使用委派？
假設今天有三個需求：  
1.將一個字串數組中每一個元素都轉換成小寫  
2.將一個字串數組中每一個元素都轉換成大寫  
3.將一個字串數組中每一個元素兩邊都加上雙引號    

一般我們會寫三個方法：

```c#
string[] names = { "abCDefG", "HIjgLm", "QxdeTXd", "WxyZ" };

//ProStrToLower(names);
//ProStrToLower(names);
ProStrSYH(names);
for (int i = 0; i < names.Length; i++) {
    Console.WriteLine(names[i]);
}
Console.ReadKey();
//換成大寫
static void ProStrToUpper(string[] name) {
    for (int i = 0; i < name.Length; i++) {
        name[i] = name[i].ToUpper();
    }
}
//換成小寫
static void ProStrToLower(string[] name) {
    for (int i = 0; i < name.Length; i++) {
        name[i] = name[i].ToLower();
    }
}
//兩邊都加上雙引號
static void ProStrSYH(string[] name) {
    for (int i = 0; i < name.Length; i++) {
        name[i] = "\"" + name[i] + "\"";//加\轉義符
    }
}
```
但他們長得很像，是不是寫法可以寫成一個呢？  
這時候我們就可以用到使用委派delegate，怎麼用？    
將一個方法做為參數傳給另一個方法    
那傳的方法是什麼類型？委派類型`delegate`    
