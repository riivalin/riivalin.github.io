---
layout: post
title: "[C# 筆記][Serializable] 序列化 & 反序列化 1"
date: 2011-01-22 22:39:00 +0800
categories: [Notes, C#]
tags: [C#,Serializable]
---

BinaryFormatter、Formatter 和IFormatter 上的序列化和還原序列化方法已過時。vs2022

## 序列化 & 反序列化

- 序列化：就是將對象轉換為二進制
- 反序列化，就是將二進制轉換為對象
目的：傳輸數據

我們在網路傳輸數據的時候，只有二進制這個型態是可以被傳輸的。       

步驟：將這個類標記為序列化    

Q：怎樣才可能將一個類別標記為這個類可以被序列化呢？  
在類別上加上`[Serializable]`

## 範例：序列化- 將物件傳輸給對方電腦
要將p這個對象，傳輸給對方電腦
```c#
using System.Runtime.Serialization.Formatters.Binary;

//要將p這個對象 傳輸給對方電腦
Person p = new Person();
p.Name = "KIKI";
p.Age = 18;
p.Gender = 'F';

//處理文件流
using (FileStream fsWrite = new FileStream(@"C:\Users\rivalin\Desktop\new.txt", FileMode.OpenOrCreate, FileAccess.Write))
{
    //開始序列化對象
    BinaryFormatter bf = new BinaryFormatter(); //創建序列化對象

    //需要傳入stream，FileStream是繼承Stream，
    //根據里氏轉換原則，可以將子類賦值給父類，
    //所以就把fsWrite傳入
    bf.Serialize(fsWrite, p); //開始序列化對象
}
Console.WriteLine("序列化成功");
Console.ReadKey();

[Serializable]
public class Person
{
    private string _name;
    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    private int _age;
    public int Age {
        get { return _age; }
        set { _age = value; }
    }
    private char _gender;
    public char Gender {
        get { return _gender; }
        set { _gender = value; }
    }
}
```
## 範例：反序化-接收對方發過來的二進制，反序列化成對象
接收對方發過來的二進制 反序列化成對象
```c#
//接收對方發過來的二進制 反序列化成對象
using System.Runtime.Serialization.Formatters.Binary;

Person p;
using (FileStream fsRead = new FileStream(@"C:\Users\rivalin\Desktop\new.txt", FileMode.OpenOrCreate, FileAccess.Read))
{
    BinaryFormatter bf = new BinaryFormatter(); //創建序列化對象
    p = (Person)bf.Deserialize(fsRead); //反序列化後並轉換為person類型
}
Console.WriteLine(p.Name);
Console.WriteLine(p.Age);
Console.WriteLine(p.Gender);
Console.ReadKey();
```