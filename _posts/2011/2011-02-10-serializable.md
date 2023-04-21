---
layout: post
title: "[C# 筆記][Serializable] 序列化 & 反序列化"
date: 2011-02-10 00:03:31 +0800
categories: [Notes,C#]
tags: [C#,Serializable]
---

.NET 5 個重大變更，其中BinaryFormatter、Formatter 和IFormatter 上的序列化和還原序列化方法已過時。       


為什麼要序列化？因為要傳輸數據。    

## 序列化
#### 1. 要將序列化對象(物件)的類，標記為可以被序列化的

```c#
[Serializable]
public class Person { 
}
```

#### 2. 把這個對象(物件)序列化成二進制，用一個流來搞定這件事情
#### 3. 我們要把序列化後的二進制用 FileStream 寫到桌面上

```c#
Person person = new Person();
person.Name = "Ken";
person.Age = 18;
person.Gender = '男';

//需要一个stream，直接寫入檔案
using (FileStream fsWrite = new FileStream(@"C:\Users\rivalin\Desktop\new.txt", FileMode.OpenOrCreate, FileAccess.Write))
{
    BinaryFormatter bf = new BinaryFormatter(); //創建二進位序列化器
    bf.Serialize(fsWrite, person); //序列化
    Console.WriteLine("序列化成功");
}
Console.ReadKey();

//要將序列化對象(物件)的類，標記為可以被序列化的
[Serializable]
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public char Gender { get; set; }
}
```

## 反序列化 

```c#
Person p; //宣告Personm類型的變數，用來存放反序列化後的object對象
using (FileStream fsRead = new FileStream(@"C:\Users\rivalin\Desktop\new.txt", FileMode.OpenOrCreate, FileAccess.Read))
{
    BinaryFormatter bf = new BinaryFormatter();//創建二進位序列化物件
    p =(Person)bf.Deserialize(fsRead); //返回一個object類型，我知道裡面裝的一定是Person，所以強制轉型(Person)
}
Console.WriteLine(p.Name);
Console.WriteLine(p.Age);
Console.WriteLine(p.Gender);
Console.ReadKey();
```

## 完整程式碼   

```c#
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace 序列化
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.Name = "Ken";
            person.Age = 18;
            person.Gender = '男';

            //需要一个stream，直接寫入檔案
            //using (FileStream fsWrite = new FileStream(@"C:\Users\rivalin\Desktop\new.txt", FileMode.OpenOrCreate, FileAccess.Write))
            //{
            //    BinaryFormatter bf = new BinaryFormatter(); //創建二進位序列化物件
            //    bf.Serialize(fsWrite, person); //序列化
            //    Console.WriteLine("序列化成功");
            //}

            Person p; //宣告Personm類型的變數，用來存放反序列化後的object
            using (FileStream fsRead = new FileStream(@"C:\Users\rivalin\Desktop\new.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();//創建二進位序列化物件
                p =(Person)bf.Deserialize(fsRead); //返回一個object類型，我知道裡面裝的一定是Person，所以強制轉型(Person)
            }
            Console.WriteLine(p.Name);
            Console.WriteLine(p.Age);
            Console.WriteLine(p.Gender);
            Console.ReadKey();
        }
    }
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public char Gender { get; set; }
    }
}
```