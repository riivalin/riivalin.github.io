---
layout: post
title: "[C# 筆記] Multicast Delegate 多點傳送委派(多播委托)"
date: 2011-02-01 01:29:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 多點傳送委派(多播委托)
多播委托可以指向多個函數  
除了+=，還可以減掉函數 -=

```c#
namespace 多播委托
{
    public delegate void DelTest();
    internal class Program
    {
        static void Main(string[] args)
        {
            //多播委托可以指向多個函數
            DelTest del = T1;
            del += T2; //T1,T2
            del += T3; //T1,T2,T3
            del -= T1; //除了+ 還可以-減掉函數
            del = T4; //注意，這樣就只有T4
            del += T5;
            del -= T5;
            del();
            Console.ReadKey();
        }

        public static void T1() {
            Console.WriteLine("我是T1");
        }
        public static void T2() {
            Console.WriteLine("我是T2");
        }
        public static void T3() {
            Console.WriteLine("我是T3");
        }
        public static void T4() {
            Console.WriteLine("我是T4");
        }
        public static void T5() {
            Console.WriteLine("我是T5");
        }
    }
}
```