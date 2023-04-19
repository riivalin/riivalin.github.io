---
layout: post
title: "[C# 筆記] Delegate 匿名函數"
date: 2011-02-01 00:09:12 +0800
categories: [Notes, C#]
tags: [C#,Delegate]
---

## delegate 委派
三個需求：  
1.將一個字串數組中每一個元素都轉換成小寫  
2.將一個字串數組中每一個元素都轉換成大寫  
3.將一個字串數組中每一個元素兩邊都加上雙引號    

將一個方法做為參數傳給另一個方法  
那傳的方法，是什麼類型？委派類型    

```c#
//聲明委派
//聲明一個委派指向一個函數
//委派所指向的函數必須跟委派具有相同的簽名
public delegate string DelProstr(string name);

internal class Program
{
    static void Main(string[] args)
    {
        string[] names = { "abCDefG", "HIjgLm", "QxdeTXd", "WxyZ" };

        //調用
        ProStr(names, StrToLower); //傳入方法
        //看結果
        for (int i = 0; i < names.Length; i++) {
            Console.WriteLine(names[i]);
        }

        Console.ReadKey();
    }
    //能把方法傳回來，只有委派類型
    public static void ProStr(string[] names, DelProstr del) {
        for (int i = 0; i < names.Length; i++) {
            names[i] = del(names[i]);
        }
    }

    public static string StrToUpper(string name) {
        return name.ToUpper();
    }
    public static string StrToLower(string name) {
        return name.ToLower();
    }
    public static string StrToSYH(string name) {
        return "\"" + name + "\""; //\轉義符
    }
}
```
這樣寫，程式碼還是一樣多啊，沒有變少…    

好，改一下，我下面三個函數都不要了

```c#
//聲明一個委派指向一個函數
//委派所指向的函數必須跟委派具有相同的簽名
public delegate string DelProstr(string name);

internal class Program
{
    static void Main(string[] args)
    {
        string[] names = { "abCDefG", "HIjgLm", "QxdeTXd", "WxyZ" };

        //轉小寫
        ProStr(names, delegate (string name) {
            return name.ToLower();
        });

        //看結果
        for (int i = 0; i < names.Length; i++) {
            Console.WriteLine(names[i]);
        }
        Console.ReadKey();
    }

    //能把方法傳回來，只有委派類型
    public static void ProStr(string[] names, DelProstr del) {
        for (int i = 0; i < names.Length; i++) {
            names[i] = del(names[i]);
        }
    }
}
```

> 轉大寫  
```c#
ProStr(names, delegate (string name) {
    return name.ToUpper();
});
```
> 加上雙引號  
```c#
ProStr(names, delegate (string name) {
    return "\"" + name + "\""; //\轉義符
});
```

## 匿名函數

```c#
//ProStr(names, StrToLower);
ProStr(names, delegate (string name) {
    return name.ToLower();
});
```

delegate 這段就叫做匿名函數    
他的本質就是一個函數
```c#
delegate (string name) {
    return name.ToLower();
}
```
### 什麼時候用匿名函數
當你方法只會用到一次的時候，就可以考慮用匿名函數



