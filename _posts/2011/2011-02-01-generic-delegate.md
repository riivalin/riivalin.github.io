---
layout: post
title: "[C# 筆記] Generic Delegates 泛型委派"
date: 2011-02-01 00:39:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## Generic Delegates 泛型委派

### Delegate 委派
- 為什麼要使用委派？
將一個方法作為參數傳遞給另一個方法
- 委派概念
聲明一個委派類型  
委派所指向的函數必須跟委具有相同的簽名  
- 匿名函數
沒有名字的函數

## 一般求數組的最大值寫法
```c#
public static int GetMax(int[] nums) { //整數陣列求最大值
    int max = nums[0];
    for (int i = 0; i < nums.Length; i++) {
        if (max < nums[i]) max = nums[i];
    }
    return max;
}

public static string GetMax(string[] nums) { //字串陣列求最大值
    string max = nums[0];
    for (int i = 0; i < nums.Length; i++) {
        if (max.Length < nums[i.Length max = nums[i];
    }
    return max;
}
```
## 練習：使用委派求數組的最大值
這兩個函數有什麼差別？  
是不是都可以把類型改成`object`  

```c#
namespace 求數組的最大值
{
    public delegate int DelCompare(object o1, object o2);

    internal class Program
    {
        static void Main(string[] args)
        {
            object[] o = { "ass", "wesfd", "dfasfsdfds", "fsafdsd" };//{ 1, 2, 3, 4, 5 };
            object result = GetMax(o, Compare2); //Compare);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static object GetMax(object[] nums, DelCompare del)
        {
            object max = nums[0];
            for (int i = 0; i < nums.Length; i++)
            {
                //要傳一個比較的方法 
                //max-nums[i]<0,說明max是小於nums[i]，就要交換
                if (del(max, nums[i]) < 0)
                {
                    max = nums[i];
                }
            }
            return max;
        }
        //o1指的是max, o2指的是nums[i]
        public static int Compare(object o1, object o2) //整數陣列
        {
            int n1 = (int)o1;
            int n2 = (int)o2;
            return n1 - n2;
        }
        public static int Compare2(object o1, object o2) //字串陣列
        {
            string s1 = (string)o1;
            string s2 = (string)o2;
            return s1.Length - s2.Length;
        }

        //public static object GetMax(string[] nums)
        //{
        //    string max = nums[0];
        //    for (int i = 0; i < nums.Length; i++)
        //    {
        //        //if (max.Length < nums[i.Length max = nums[i];
        //        max = max.Length > nums[i].Length ? max : nums[i];
        //    }
        //    return max;
        //}
    }
}
```

## 寫成匿名函數

```c#
//寫成匿名函數
object result = GetMax(o, delegate (object o1, object o2) {
    string s1 = (string)o1;
    string s2 = (string)o2;
    return s1.Length - s2.Length;
});
```

```c#
namespace 求數組的最大值
{
    public delegate int DelCompare(object o1, object o2);

    internal class Program
    {
        static void Main(string[] args)
        {
            object[] o = { "ass", "wesfd", "Adfasfsdfds", "fsafdsd" };//{ 1, 2, 3, 4, 5 };
            //object result = GetMax(o, Compare2);

            //寫成匿名函數
            object result = GetMax(o, delegate (object o1, object o2) {
                string s1 = (string)o1;
                string s2 = (string)o2;
                return s1.Length - s2.Length;
            });

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static object GetMax(object[] nums, DelCompare del)
        {
            object max = nums[0];
            for (int i = 0; i < nums.Length; i++)
            {
                //要傳一個比較的方法 
                //max-nums[i]<0,說明max是小於nums[i]，就要交換
                if (del(max, nums[i]) < 0)
                {
                    max = nums[i];
                }
            }
            return max;
        }
        //o1指的是max, o2指的是nums[i]
        //public static int Compare(object o1, object o2) {
        //    int n1 = (int)o1;
        //    int n2 = (int)o2;
        //    return n1 - n2;
        //}
        //public static int Compare2(object o1, object o2) {
        //    string s1 = (string)o1;
        //    string s2 = (string)o2;
        //    return s1.Length - s2.Length;
        //}

        //public static object GetMax(string[] nums)
        //{
        //    string max = nums[0];
        //    for (int i = 0; i < nums.Length; i++)
        //    {
        //        //if (max.Length < nums[i.Length max = nums[i];
        //        max = max.Length > nums[i].Length ? max : nums[i];
        //    }
        //    return max;
        //}
    }
}
```

## 寫成 Lamda表達式

```c#
//寫成Lamda表達式
object result = GetMax(o, (object o1,object o2) => {
    string s1 = (string)o1;
    string s2 = (string)o2;
    return s1.Length - s2.Length;
});
```

```c#
namespace 求數組的最大值
{
    public delegate int DelCompare(object o1, object o2);

    internal class Program
    {
        static void Main(string[] args)
        {
            object[] o = { "ass", "wesfd", "Adfasfsdfds", "fsafdsd" };//{ 1, 2, 3, 4, 5 };
            //object result = GetMax(o, Compare2);

            //寫成匿名函數
            //object result = GetMax(o, delegate (object o1, object o2) {
            //    string s1 = (string)o1;
            //    string s2 = (string)o2;
            //    return s1.Length - s2.Length;
            //});

            //寫成Lamda表達式
            object result = GetMax(o, (object o1,object o2) => {
                string s1 = (string)o1;
                string s2 = (string)o2;
                return s1.Length - s2.Length;
            });

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static object GetMax(object[] nums, DelCompare del)
        {
            object max = nums[0];
            for (int i = 0; i < nums.Length; i++)
            {
                //要傳一個比較的方法 
                //max-nums[i]<0,說明max是小於nums[i]，就要交換
                if (del(max, nums[i]) < 0)
                {
                    max = nums[i];
                }
            }
            return max;
        }
        //o1指的是max, o2指的是nums[i]
        //public static int Compare(object o1, object o2) {
        //    int n1 = (int)o1;
        //    int n2 = (int)o2;
        //    return n1 - n2;
        //}
        //public static int Compare2(object o1, object o2) {
        //    string s1 = (string)o1;
        //    string s2 = (string)o2;
        //    return s1.Length - s2.Length;
        //}

        //public static object GetMax(string[] nums)
        //{
        //    string max = nums[0];
        //    for (int i = 0; i < nums.Length; i++)
        //    {
        //        //if (max.Length < nums[i.Length max = nums[i];
        //        max = max.Length > nums[i].Length ? max : nums[i];
        //    }
        //    return max;
        //}
    }
}
```

這樣寫也很囉嗦，而且有用到`object`就會牽扯到拆箱&裝箱

## 練習：使用委派求任意數組的最大值
泛型委派 寫法  
任意類型數組

```c#
namespace 泛型委派
{
    public delegate int DelCompare<T>(T t1, T t2); //T type
    //public delegate int DelCompare(object o1, object o2);
    internal class Program
    {
        static void Main(string[] args)
        {
            //泛型委派 寫法 
            //求整數陣列最大值
            int[] nums = { 1, 2, 3, 4, 5 };
            int max = GetMax<int>(nums, Compare1);
            Console.WriteLine(max);

            //求字串陣列最大值(泛型委派+Lamda表達式)
            string[] names = { "asdf", "sdf", "WERWxx" };
            string max1 = GetMax<string>(names, (string s1, string s2) => {
                return s1.Length - s2.Length;
            });
            Console.WriteLine(max1);
            Console.ReadKey();
        }

        public static T GetMax<T>(T[] nums, DelCompare<T> del)
        {
            T max = nums[0];
            for (int i = 0; i < nums.Length; i++)
            {
                //要傳一個比較的方法 
                //max-nums[i]<0,說明max是小於nums[i]，就要交換
                if (del(max, nums[i]) < 0)
                {
                    max = nums[i];
                }
            }
            return max;
        }
        public static int Compare1(int n1, int n2)
        {
            return n1 - n2;
        }
    }
}
```