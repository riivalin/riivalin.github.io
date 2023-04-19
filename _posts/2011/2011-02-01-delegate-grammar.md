---
layout: post
title: "[C# 筆記] Delegate 委派語法"
date: 2011-02-01 00:09:07 +0800
categories: [Notes, C#]
tags: [C#,Delegate,Thread]
---

## 委派概念
- 聲明一個委派指向一個函數  
- 委派所指向的函數必須跟委派具有相同的簽名

> 跟創建執行緒很像，都是傳入一個方法  
`Thread t = new Thread(SayHiChinese);`


## 委派基本語法

```c#
namespace 委派概念
{
    //聲明一個委派指向一個函數
    //委派所指向的函數必須跟委派具有相同的簽名
    public delegate void DelegateSayHi(string name);//沒有返回值，但有傳入一個參數

    internal class Program
    {
        static void Main(string[] args)
        {
            //Test("張三",)

            //用中文打招呼，若要用英文打招呼改成SayHiEnglish
            DelegateSayHi del = new DelegateSayHi(SayHiChinese); //用英文打招呼
            //跟創建執行緒很像，都是傳入一個方法
            //Thread t = new Thread(SayHiChinese); 

            del("張三");
            Console.ReadKey();
        }

        //我只調這個函數，如果我想用中文打招呼，他就去調用中文打招呼方法，反之，用英文，就調英文
        public static void Test(string name, DelegateSayHi delegateSayHi)
        {
            //調用
            delegateSayHi(name);
        }

        //這兩個方法都做差不多的事
        public static void SayHiChinese(string name) { //中文打招呼
            Console.WriteLine($"吃了什麼?{name}");
        }
        public static void SayHiEnglish(string name) { //英文打招呼
            Console.WriteLine($"Nice to meet you{name}");
        }
    }
}
```
還沒完…

仔細看，這樣有意義嗎？    
我還要創建對象new委派，怎不直接調用打招呼方法？   
```c#
DelegateSayHi del = SayHiChinese; //new DelegateSayHi(SayHiEnglish);
```
既然我可以將方法賦值給委派類型，那我就可以不用寫這段，我直接在Test傳入啦，  
因為Test裡有一個delete類型 可以傳入方法  

```c#
static void Main(string[] args) {
    //DelegateSayHi del = SayHiChinese;//new DelegateSayHi(SayHiEnglish);

    //我最終目的是只調用Test函數，不調用下面那兩個函數
    //那既然我可以直接將方法傳給委派類型
    //那我函數中有委派類型的參數，我就直接調用傳入方法就可以啦
    Test("張三", SayHiChinese);
    Test("張三", SayHiEnglish);
    Console.ReadKey();
}
```
現在成功的將函數當做參數傳給了Test()  
- 函數可以賦值給一個委派
    
但是，還是沒什差異~~~  
跟我直接調用兩個打招呼方式一樣啊???  
SayHiChinese("張三");  
SayHiEnglish("張三");  

這時候還是看不出委派delegate的好處     

(待續…)
---


```c#
namespace 委派概念
{
    //聲明一個委派指向一個函數
    //委派所指向的函數必須跟委派具有相同的簽名
    public delegate void DelegateSayHi(string name);//沒有返回值，但有傳入一個參數

    internal class Program
    {
        static void Main(string[] args)
        {
            //Test("張三",)

            //仔細看，這樣有意義嗎？
            //我還要創建對象new委派，怎不直接調用打招呼方法？
            //既然我可以將方法賦值給委派類型，
            //那我就可以不用寫這段，我直接在Test傳入啦
            //因為Test裡有一個delete類型 可以傳入方法
            //DelegateSayHi del = SayHiChinese;//new DelegateSayHi(SayHiEnglish);
            //跟創建執行緒很像，都是傳入一個方法
            //Thread t = new Thread(SayHiChinese); 

            //del("張三");

            //我最終目的是只調用Test函數，不調用下面那兩個函數
            //那既然我可以直接將方法傳給委派類型
            //那我函數中有委派類型的參數，我就直接調用傳入方法就可以啦
            Test("張三", SayHiChinese);
            Test("張三", SayHiEnglish);

            //但是，還是沒什差異~~~
            //跟我直接調用兩個打招呼方式一樣啊 ???
            //SayHiChinese("張三");
            //SayHiEnglish("張三");

            Console.ReadKey();
        }

        //我最終目的是只調用Test函數，不調用下面那兩個函數
        //我只調這個函數，如果我想用中文打招呼，他就去調用中文打招呼方法，反之，用英文，就調英文
        public static void Test(string name, DelegateSayHi delegateSayHi)
        {
            //調用
            delegateSayHi(name);

        }

        //這兩個方法都做差不多的事
        public static void SayHiChinese(string name) {
            Console.WriteLine($"吃了什麼{name}");
        }
        public static void SayHiEnglish(string name) {
            Console.WriteLine($"Nice to meet you{name}");
        }
    }
}
```
