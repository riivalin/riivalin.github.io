---
layout: post
title: "[C# 筆記] String.CompareTo 方法"
date: 2021-03-25 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,string,CompareTo]
---


`String.CompareTo()`方法，比較這個執行個體與指定的物件或 String，並傳回一個整數，指出這個執行個體在排序次序中，位於指定物件或 String 之前、之後或相同位置。


- `CompareTo(String)`	
比較這個執行個體與指定的 String 物件，並且表示這個執行個體在排序次序中，位於所指定字串之前、之後或相同位置。
- `CompareTo(Object)`	
比較這個執行個體與指定的 Object，並且指出這個執行個體在排序次序中，位於所指定 Object 之前、之後或相同位置。

```c#
public int CompareTo (string? strB);
```
參數
- strB
    - String: 要和這個執行個體比較的字串。      

傳回
- Int32: 32 位元帶正負號的整數，指出這個執行個體在排序次序中，位於 strB 參數之前、之後或相同位置。


| 值 | 條件            | 
|:----------|:-----------------|
| `小於 0`|這個執行個體位於 strB 之前。|
| `0`   |這個執行個體在排序次序中的位置與 strB 相同。|
| `大於 0`|這個執行個體位於 strB 之後。
|| -或-|
||strB 為 null。|


## 用法

```c#
string s1 = "c";
string s2 = "b";
if(s1.CompareTo(s2)==1)
{
}
```

它有三個返回值：``
- 當`s1>s2`時，`s1.CompareTo(s2) == 1`
- 當`s1=s2`時，`s1.CompareTo(s2) == 0`
- 當`s1<s2`時，`s1.CompareTo(s2) == -1`

以上为例，`c`的asc大於`b`的asc，所以返回`1`


[MSDN - String.CompareTo 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.string.compareto?view=net-8.0)       
[C#中CompareTo（）语法的用法](https://blog.csdn.net/weixin_42006872/article/details/88541160)