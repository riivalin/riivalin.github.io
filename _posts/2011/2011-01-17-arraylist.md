---
layout: post
title: "[C# 筆記]  ArrayList 集合"
date: 2011-01-17 22:59:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## ArrayList 集合

集合：很多數據的一個集合  
```c#
ArrayList list = new ArrayList(); //創建集合物件
```
而`array`：長度不可變，類型單一   

集合的好處：長度可以任意改變，類型隨便  

## 範例：ArrayList集合加入不同的類型的元素、集合，並輸出
```c#
using System.Collections;

//創建集合物件
ArrayList list = new ArrayList();
//集合的好處：長度可以任意改變，類型隨便
list.Add(2);
list.Add("aaa");
list.Add(true);
list.Add("張三");
list.Add(new int[] { 1, 2, 3, 4, 5 });
list.Add(new Person());
list.Add(list);

for (int i = 0; i < list.Count; i++)
{
    //把一個物件輸出到控制台，默認情況下，輸的就是這個物件的命名空間
    if (list[i] is Person) //判斷是否可以轉型
    { 
        ((Person)list[i]).SayHello(); //強制轉型
    } else if (list[i] is int[])  //使用AddRange添加集合，就不用轉型
    {
        for (int j = 0; j < ((int[])list[i]).Length; j++) {
            Console.WriteLine(((int[])list[i])[j]);
        }
    } else {
        Console.WriteLine(list[i]);
    }
}
Console.ReadKey();

class Person {
    public void SayHello() {
        Console.WriteLine("Hello");
    }
}
```
> 把一個物件輸出到控制台，默認情況下，輸的就是這個物件的命名空間

## 範例：使用AddRange 添加集合並輸出
使用`AddRange`添加集合，輸出就不用轉型
```c#
using System.Collections;

//建立ArrayList集合物件
ArrayList list = new ArrayList();

//加入單個元素
list.Add(2);
list.Add("aaa");
list.Add(true);
list.Add("張三");
//加入集合元素(使用AddRange)
list.AddRange(new int[] { 1, 2, 3, 4, 5, 6 });
list.AddRange(list);

//輸出看結果
for (int i = 0; i < list.Count; i++) {
    Console.WriteLine(list[i]);
}
```

## .Clear(); 清空所有的元素
```c#
list.Clear(); //清空所有的元素
```
## 其他方法
```c#
list.Remove(true); //刪除某個元素，寫誰就刪誰
list.RemoveAt(0); //依據index去刪除元素
list.RemoveRange(0, 3);//依據index去移除一定範圍的元素
list.Sort(); //升序排序元
list.Reverse(); //反轉
list.Reverse(); //反轉
list.Insert(0, "好"); //依據index插入元素
list.InsertRange(0, new string[] { "AAA", "BBB", "CCC" }); //在指定的位置插入一個集合  
```

list.Contains() 判斷是否包含某個元素  
`if(list.Contains(2)) {...}`  
```c#
//集合中不包含"李四"，就在第一個位置添加"李四"
if (!list.Contains("李四")) {
    list.Add("李四");
}
```