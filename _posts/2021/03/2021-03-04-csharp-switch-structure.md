---
layout: post
title: "[C# 筆記] switch 陳述句"
date: 2021-03-03 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,流程控制,switch-case]
---

「選擇結構(Selection Structure)」會根據程式的「判斷條件」是否成立來決定程式最後要往哪一流程(程序)去跑。     

選擇結構包含：
- `if`陳述句
- `switch`陳述句
- `?:`運算子

## switch 多向選擇結構

`switch`是「透過案例(`case`)的判斷來切換(`switch`)至符合案例(`case`)的區塊中」。        

當判斷條件「超過三個以上」時，採用「`switch` 陳述句」會是較佳的選擇。       

## 語法

- 值常見的資料型態：`char`(字元)、`string`(字串)、`int`(數值)

```c#
switch(運算式) 
{
    case [值1]:
        //statement
        break;
    case [值2]:
        //statement
        break;
    case [值3]:
        //statement
        break;
    default:
        //statement
        break;
}
```

## 範例

透過`switch`實作「成績好壞」的功能：
- 成績A：高手
- 成績B：低空飛過
- 成績C：明年再來
- 以上皆非：非判級資料

```c#
char score = char.Parse(Console.ReadLine()!);
switch (score)
{
    case 'A':
        Console.WriteLine("高手");
        break;
    case 'B':
        Console.WriteLine("低空飛過");
        break;
    case 'C':
        Console.WriteLine("明年再來");
        break;
    default:
        Console.WriteLine("非判級資料");
        break;
}
```