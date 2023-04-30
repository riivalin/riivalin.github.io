---
layout: post
title: "[C# 筆記] 泛型集合練習結束 3"
date: 2011-03-02 23:43:00 +0800
categories: [Notes, C#]
tags: [C#,List,Dictionary,泛型,泛型集合]
---

## 練習1:統計字串中每個字出現的次數(不區分大小寫)
"Welcome to China world"

- `Dictionary<char, int>`把字元當作key，次數當作值
- `continue`如果是空格，就開始下一個新的循環(下面的程式碼就不會執行)。
- `dic.ContainsKey`判斷是否包含這個key值，有就這個key值就加1，沒有就添加。

```c#
string s = "Welcome to China world";
Dictionary<char, int> dic = new Dictionary<char, int>();
for (int i = 0; i < s.Length; i++)
{
    //如果是空格，就開始下一個新的循環(下面的程式碼就不會執行)
    if (s[i]== ' ') continue;

    //判斷是否包含這個key值
    if (dic.ContainsKey(s[i]))
    {
        //有這個key值就加1
        dic[s[i]]++;
    } else
    {
        //沒有的話，就添加
        dic[s[i]] = 1;
    }
}
//看結果
foreach (KeyValuePair<char,int> kv in dic)
{
    Console.WriteLine($"字母 {kv.Key} 出現 {kv.Value} 次");
}
Console.ReadKey();
```

## 練習2:兩個List集合，合併為一個集合，去除重複
`{ "a","b","c","d","e"}`、`{ "d","e","f","g","h"}`

- 遍歷每個元素，判斷是否包含，包含就不加

```c#
List<string> listOne = new List<string>() { "a","b","c","d","e"};
List<string> listTwo = new List<string>() { "d","e","f","g","h"};

//遍歷每個元素
for (int i = 0; i < listTwo.Count; i++) {
    //判斷是否包含，包含就不加，不包含就加入第一個集合listOne
    if (!listOne.Contains(listTwo[i])) {
        listOne.Add(listTwo[i]);
    }
}
//看結果
foreach (var item in listOne) {
    Console.Write(item);
}
Console.ReadKey();
```