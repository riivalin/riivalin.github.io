---
layout: post
title: "[C# 筆記] Hashtable (key:value) 字典集合"
date: 2011-01-17 23:19:00 +0800
categories: [Notes, C#]
tags: [C#,hashtable]
---

## Hashtable (索引鍵/值組) 集合

在Hashtable (key,value)集合中，是根據key去找值的  

用`foreach`來遍歷`Hashtable` (`foreach`效能比`for`高)  
```c#
using System.Collections;

Hashtable ht = new Hashtable();
ht.Add(1, "張三");
ht.Add(2, true);
ht.Add(3, "男");
ht.Add(false, "錯誤的");
ht[4] = "哈哈"; //這也是添加數據的方式

//用foreach來遍歷Hashtable
foreach (var item in ht.Keys) {
    Console.WriteLine($"{item}: {ht[item]}");
}
```
## 添加數據的兩個方式
- Add()方法  
如果加入相同的key，會拋異常。  
- ht[index] 索引方式  
如果加入相同的key，會替換掉原來的數據。  

```c#
ht.Add(4, "張三");  
ht[4] = "哈哈";
```

## 加入相同key值 
`ht.Add(1, "哇哈哈");`如果加入相同的key，會拋異常。   
  
可以通過index 索引的方式硬加進去  
`ht[1] = "把張三幹掉"`它的方式是，如果集合裡沒有就加入，有的話，就替換掉原來的數據。    
```c#
Hashtable ht = new Hashtable();
ht.Add(1, "張三");
ht[2] = "哈哈"; //這也是添加數據的方式
ht[1] = "把張三幹掉"; //不會報錯，原本的張三就不見了，被替換掉

//輸出：哈哈, 把張三幹掉
```

## 通常會加上判斷 ht.ContainsKey()
```c#
Hashtable ht = new Hashtable();
ht.Add(1, "張三");
ht.Add(2, true);
ht.Add(3, "男");
ht.Add(false, "錯誤的");
ht[4] = "哈哈"; //這也是添加數據的方式
ht[1] = "把張三幹掉"; //不會報錯，原本的張三就不見了
ht["abc"] = "cba"; 

//abc:cba
if (!ht.ContainsKey("abc"))
{
    ht["abc"] = "cba"; //己有"adc" key值，所以不會加入，會走else
} else {
    Console.WriteLine("己經有abc的key值了");
}
```
## 其他方法
```c#
ht.Remove(3); //依據key值 刪除某個元素
ht.Clear(); //移除集合中所有的元素
```


```text
var 推斷類型
c#  強類型語言
js  弱類型語言
GetType() 取得類型

`var`：根據值能夠推斷出來類型
C# 是一門強類型語言：在程式碼當中，必須對每一個變量的類型有一個明確的定義
```