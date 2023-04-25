---
layout: post
title: "[C# 筆記] function 練習3"
date: 2011-01-12 23:09:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習：輸入成績判斷其等級
接收輸入後，判斷其等級並顯示出來    
判斷依據如下：  
等級={優 90-100分 良 80-89分}   
```c#
while (true)
{
    Console.WriteLine("請輸入成績"); //提示用戶輸入
    int score = Convert.ToInt32(Console.ReadLine()!); //接收用戶輸入
    string level = GetLevel(score); //取得成績等級
    Console.WriteLine(level); //輸出
}

public static string GetLevel(int score)
{
    string level;
    switch (score / 10)
    {
        case 10:
        case 9:
            level = "優";
            break;
        case 8:
            level = "良";
            break;
        case 7:
            level = "中";
            break;
        case 6:
            level = "差";
            break;
        default:
            level = "不級格";
            break;
    }
    return level;
}
```
## 練習：字串陣列反轉
請將字串陣列反轉 { "日本", "中國", "巴西", "美國" }

### 使用Array.Reverse
```c#
string[] names = { "日本", "中國", "巴西", "美國" };
Array.Reverse(names);
```

### 自己寫
```c#
string[] names = { "日本", "加拿大", "巴西", "美國" };
for (int i = 0; i < names.Length / 2; i++) //會交換(names.Length/2)次
{
    string temp = names[i];
    names[i] = names[names.Length - 1 - i];
    names[names.Length - 1 - i] = temp;
}

//輸出看結果
for (int i = 0; i < names.Length; i++) {
    Console.WriteLine(names[i]);
}
```

### 寫成方法(有回傳值)
```c#
string[] names = { "日本", "加拿大", "巴西", "美國", "巴西" };
names = ToReverse(names);

public static string[] ToReverse(string[] names)
{
    for (int i = 0; i < names.Length / 2; i++) //會交換(names.Length/2)次
    {
        string temp = names[i];
        names[i] = names[names.Length - 1 - i];
        names[names.Length - 1 - i] = temp;
    }
    return names;
}
```

### 寫成ref參數方法(無回傳值)
使用[ref](https://riivalin.github.io/posts/ref/)參數，將字串陣列帶入方法中，在方法中改變後，再帶出來
```c#
string[] names = { "日本", "加拿大", "巴西", "美國", "巴西" };
ToReverse(ref names);

//ref參數方法(無回傳值)
public static void ToReverse(ref string[] names)
{
    for (int i = 0; i < names.Length / 2; i++) //會交換(names.Length/2)次
    {
        string temp = names[i];
        names[i] = names[names.Length - 1 - i];
        names[names.Length - 1 - i] = temp;
    }
}
```

## 練習：計算圓的面積和周長(out參數)
面積是PI*R*R    
周長是2*Pi*R    
使用[out](https://riivalin.github.io/posts/out/)參數，讓方法可以返回多個回傳值出來
```c#
double r = 5;
double perimeter;
double area;
R.GetPerimeterArea(r, out perimeter, out area); //call out參數方法

Console.WriteLine(perimeter);
Console.WriteLine(area);
Console.ReadKey();

//使用out參數 計算圓的面積和周長
public static void GetPerimeterArea(double r, out double perimeter, out double area)
{
    perimeter = 2 * r * 3.14; //周長是2*Pi*R
    area = 3.14 * r * r; //面積是PI*R*R
}
```
[ref](https://riivalin.github.io/posts/ref/)
[out](https://riivalin.github.io/posts/out/)