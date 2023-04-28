---
layout: post
title: "[C# 筆記] 泛型集合複習 1"
date: 2011-03-02 23:13:00 +0800
categories: [Notes, C#]
tags: [C#,heap,stack,ref,List<T>,Dictionary<TKey,TValue>,泛型,泛型集合,Boxing Unboxing,裝箱 拆箱]
---

ArrayList & Hashtable 不常用了，用泛型

- List<T>
- Dictionary<TKey,TValue>

#### Q：為什麼不再使用ArrayList和Hashtable？
會發生裝箱和拆箱。
ArrayList和Hashtable 很少在用，為什麼?      
除了取數據不方便外，花費時間較多、效率低外，因為涉及到裝箱、拆箱的問題。
        
[[C# 筆記] Boxing & Unboxing 裝箱&拆箱](https://riivalin.github.io/posts/2011/01/boxing-unboxing/)


## 集合類(增、刪、查、改、遍歷)
- 集合常用操作：添加、遍歷、移除
- 命名空間`System.Collections`
- `ArrayList`可變度陣列，使用類似於陣列
    - 屬性
        - `Capacity`：集合中可以容納元素的個數，翻倍增長；
        - `Count`：集合中實際存放的元素的個數
    - 方法
        - `Add()`、`AddRange(ICollection c)`、`Remove()`、`RemoveAt()`、`Clear()`
        - `Contains()`、`ToArray()`、`Sort()`排序、`Reverse()`反轉
- `Hashtable`鍵值對的集合，類似於字典，`Hashtable`在查找元素的時候，速度很快。
    - `Add(object key, object value);`
    - `hash["key"]`
    - `hase["key"] = "修改" `
    - `.ContainsKey("Key");`
    - `Remove("key")`
    - 遍歷
        - `hash.Keys`
        - `hash.Values/DecitionaryEntry`


## 集合
- `ArrayList`（不常用了，用泛型）
- `Hashtable`（不常用了，用泛型）
- `List<T>`
-` Dictionary<TKey,TValue>`

## Q：為什麼不再使用ArrayList和Hashtable？
會發生裝箱和拆箱

## 裝箱 & 拆箱
- 裝箱：就是將值類型轉換為引用類型
- 拆箱：就是將引用類型轉換為值類型
不管裝箱或拆箱，都會影響軟體的效能

拆箱或者裝箱的兩種類型，必須具有繼承關係。
比如：`string`轉成`int`，`int`轉成`string`，雖然他們是值類型和引用類型，但是他們之間轉換會發生拆裝箱嗎？**不會**

## 值類型 & 引用類型
- 值類型：`bool`, `int`, `double`, `char`, `struct`, `enum`, `decimal`
- 引用類型：`string`、陣列、集合、`interface`、`object`、自定義類

- 值類型的值，是分配在內存的`stack`(堆疊/棧)。
- 引用類型的值，是皆配在內存的`heap`(堆積/堆)

- 值類型在賦值的時候，傳遞的是「值的本身」
- 引用類型在賦值的時候，傳遞的是「引用」

```
int n1=10;      Person p1 = new Person();
int n2=n1;      p1.name = "張三";
                Person p2 = p1;
                p2.name = "李四";
--------------| |--------------------------
stack         | |heap
--------------| |--------------------------
10 n1         | | new Person(); Name="張四"
10 n2         | |
0x001001 p1   | |
0x001001 p2   | |
```
`ref`關鍵字，他可以把一個變量帶到方法裡面進行改變，改變完成之後，再帶出來，那其實他內部本質的原理是什麼？將值傳遞變成引用傳遞

## List<T>

```c#
List<int> list = new List<int>();
list.Add(); //加入一個元素
list.AddRange(); //加入一個集合
list.Insert(); //插入一個元素
list.InsertRange(); //插入一個集合
list.Remove(); //移除
list.RemoveAt(); //根據index刪除
list.RemoveRange(); //刪除一定範圍的元素
list.Contains();//判斷是否包含

list.RemoveAll(); //放移除條件
```

## Dictionary<int, string> 

- 遍歷`KeyValuePair` 
- `dic.ContainsKey();`判斷是否包含這個key值
- `dic[3]` 相同的key值，不報錯的添加

```c#
Dictionary<int, string> dic = new Dictionary<int, string>();
dic.Add(1,"張三"); //添加
dic.Add(2, "李四");
dic.Add(3, "王五");
dic[3] = "Ken"; //相同的key值，不報錯的添加
foreach (KeyValuePair<int,string> kv in dic)
{
    Console.WriteLine($"key: {kv.Key} - value: {kv.Value}");
}
Console.ReadKey();

dic.ContainsKey();//判斷是否包含這個key值
```

[[C# 筆記] Boxing & Unboxing 裝箱&拆箱](https://riivalin.github.io/posts/2011/01/boxing-unboxing/)
