---
layout: post
title: "[C# 筆記] 現有一個整數 number，請寫一個方法來判斷這個整數是否是 2 的 N 次方"
date: 2017-02-13 23:29:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法]
---

## % (Mod) 運算
取`Mod`運算： 用`number%2==0`       

```c#
if (number % 2 == 0)
{
    //是 2 的 N 次方
}
```

## 位元運算

可以透過位元運算來判斷一個整數是否是2的N次方。如果一個整數是2的N次方，那麼它的二進位表示中只有一個1，例如，2、4、8、16等。

```c#
public class Solution
{
  public bool IsPowerOfTwo(int number)
  {
      // 判断是否为正数且只有一个1
      return number > 0 && (number & (number - 1)) == 0;
  }
}
```

[C# .NET面试系列一：基础语法](https://cloud.tencent.com/developer/article/2394466)  