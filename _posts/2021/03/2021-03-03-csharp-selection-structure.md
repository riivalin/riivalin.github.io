---
layout: post
title: "[C# 筆記] 選擇結構 if(Condition)"
date: 2021-03-03 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,流程控制]
---

「選擇結構(Selection Structure)」會根據程式的「判斷條件」是否成立來決定程式最後要往哪一流程(程序)去跑。     

選擇結構包含：
- `if`陳述句
- `switch`陳述句
- `?:`運算子

# 單層 if
## 語法

如果 condition 條件成立(為真)，則執行敘述A。

```c#
if(condition) { //條件
    statement; //敘述A
}
```

## 範例

設計一個簡單的計算95無鉛汽油油價程式，當使用者輸入油價超過 $26 時，則顯示「太貴了」訊息。
```c#
double oilPrice = double.Parse(Console.ReadLine()!);

if (oilPrice > 26) {
    Console.WriteLine("太貴了!!!");
}
```

# 雙層 if...else
## 語法

如果 condition 條件成立(為真)，則執行敘述A，否則就執行敘述B。

```c#
if(condition) { //條件
    statement; //敘述A
} else {
    statement; //敘述B
}
```

## 範例

設計一個簡單的計算95無鉛汽油油價程式，當使用者輸入油價超過 $26 時，則顯示「太貴了」訊息，否則就顯示「尚可接受」訊息。
```c#
double oilPrice = double.Parse(Console.ReadLine()!);

if (oilPrice > 26) {
    Console.WriteLine("太貴了!!!");
} else {
    Console.WriteLine("尚可接受"); 
}
```


# 多層 if...else (巢狀式多重判斷)
## 語法

如果 condition1 條件成立(為真)，則執行敘述A，       
如果 condition2 條件成立(為真)，則執行敘述B，        
如果 condition1、condition2、conditionN 都不成立，則就執行敘述D。       

```c#
if(condition1) { //條件
    statement; //敘述A
} else if(condition2) {
    statement; //敘述B
} else if(condition3){
    statement; //敘述C
} 
... //第N次條件判斷
else {
    statement; //敘述D
}
```

## 範例

設計一個簡單的計算95無鉛汽油油價程式，當使用者輸入油價：  
- `< 20` 則顯示「非常合理」訊息
- `>= 20`、`< 25` 則顯示「尚可接受」訊息
- 否則就顯示「搶$$$哦!!!」訊息

```c#
double oilPrice = double.Parse(Console.ReadLine()!);
if (oilPrice < 20)
{
    Console.WriteLine("非常合理。");
} 
else if (oilPrice < 25)
{
    Console.WriteLine("尚可接受");
} 
else
{
    Console.WriteLine("搶$$$哦!!!");
}
```
