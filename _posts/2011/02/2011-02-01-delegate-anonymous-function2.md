---
layout: post
title: "[C# 筆記] Delegate 匿名函數 2"
date: 2011-02-01 00:09:17 +0800
categories: [Notes, C#]
tags: [C#,Delegate]
---

## 匿名函數
### 寫法一：寫一個有委派參數的方法
把一個函數給一個委派    
`SayHi("張三", SayHiChinese);`

```c#
//聲明一個委派類型指向一個方法
public delegate void DelSayHi(string name); //沒有返回值，一個參數
internal class Program
{
    static void Main(string[] args)
    {
        //寫法一：把一個函數給一個委派
        SayHi("張三", SayHiChinese);
        Console.ReadKey();
    }

    //我只想要調用這個方法
    public static void SayHi(string name, DelSayHi del)
    {
        del(name);
    }

    //不想調用下面兩個方法(SayHiEnglish,SayHiChinese)
    public static void SayHiEnglish(string name) {
        Console.WriteLine($"Hi, {name}");
    }
    public static void SayHiChinese(string name) {
        Console.WriteLine($"您好啊, {name}");
    }
}
```

### 寫法二：把SayHiChinese給del
寫法二：把SayHiChinese給del    
用這個方式SayHi()函數就可以不要了     
`DelSayHi del = SayHiChinese;`  

```c#
//聲明一個委派類型指向一個方法
public delegate void DelSayHi(string name); //沒有返回值，一個參數
internal class Program
{
    static void Main(string[] args)
    {
        //寫法一：把一個函數給一個委派
        //SayHi("張三", SayHiChinese);

        //寫法二：把SayHiChinese給del
        //用這個方式SayHi()函數就可以不要了
        DelSayHi del = SayHiChinese;
        del("張三");

        Console.ReadKey();
    }

    //我只想要調用這個方法
    //不用調用下面兩個方法(SayHiEnglish,SayHiChinese)
    //public static void SayHi(string name, DelSayHi del) {
    //    del(name);
    //}

    public static void SayHiEnglish(string name) {
        Console.WriteLine($"Hi, {name}");
    }
    public static void SayHiChinese(string name) {
        Console.WriteLine($"您好啊, {name}");
    }
    ```

    ### 寫法三：直接寫匿名函數
    沒有名字的函數    

    寫法三：如果我打招呼方法只會用一次  
    那我就直接寫匿名函數就好了, SayHiChinese()函數就不要了  

    ```c#
    DelSayHi del = delegate (string name) { 
        Console.WriteLine($"您好啊, {name}"); 
    };
    del("張三");
    ```

    ```c#
    //聲明一個委派類型指向一個方法
    public delegate void DelSayHi(string name); //沒有返回值，一個參數
    internal class Program
    {
        static void Main(string[] args)
        {
            //寫法一：把一個函數給一個委派
            //SayHi("張三", SayHiChinese);

            //寫法二：把SayHiChinese給del
            //用這個方式SayHi()函數就可以不要了
            //DelSayHi del = SayHiChinese;
            //del("張三");

            //寫法三：如果我打招呼方法只會用一次
            //那我就直接寫匿名函數就好了, SayHiChinese()函數就不要了
            DelSayHi del = delegate (string name) { 
                Console.WriteLine($"您好啊, {name}"); 
            };
            del("張三");

            Console.ReadKey();
        }

        //我只想要調用這個方法
        //不用調用下面兩個方法(SayHiEnglish,SayHiChinese)
        public static void SayHi(string name, DelSayHi del)
        {
            del(name);
        }

        //public static void SayHiEnglish(string name) {
        //    Console.WriteLine($"Hi, {name}");
        //}
        //public static void SayHiChinese(string name) {
        //    Console.WriteLine($"您好啊, {name}");
        //}
    }
}
```

### Lamda表達式
寫法四：Lamda表達式    
`=>`代表什麼：goes to
```c#
DelSayHi del = (string name) => { Console.WriteLine($"您好啊, {name}"); };
del("張三");
```

```c#
namespace 匿名函數
{
    //聲明一個委派類型指向一個方法
    public delegate void DelSayHi(string name); //沒有返回值，一個參數
    internal class Program
    {
        static void Main(string[] args)
        {
            //寫法一：把一個函數給一個委派
            //SayHi("張三", SayHiChinese);

            //寫法二：把SayHiChinese給del
            //用這個方式SayHi()函數就可以不要了
            //DelSayHi del = SayHiChinese;
            //del("張三");

            //寫法三：如果我打招呼方法只會用一次
            //那我就直接寫匿名函數就好了, SayHiChinese()函數就不要了
            //DelSayHi del = delegate (string name) { 
            //    Console.WriteLine($"您好啊, {name}"); 
            //};
            //del("張三");

            //寫法四：Lamda表達式 => goes to
            DelSayHi del = (string name) => { Console.WriteLine($"您好啊, {name}"); };
            del("張三");

            Console.ReadKey();
        }

        //我只想要調用這個方法
        //不用調用下面兩個方法(SayHiEnglish,SayHiChinese)
        public static void SayHi(string name, DelSayHi del) {
            del(name);
        }

        public static void SayHiEnglish(string name) {
            Console.WriteLine($"Hi, {name}");
        }
        public static void SayHiChinese(string name) {
            Console.WriteLine($"您好啊, {name}");
        }
    }
}
```