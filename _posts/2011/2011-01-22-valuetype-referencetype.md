---
layout: post
title: "[C# 筆記] 值傳遞 & 引用傳遞"
date: 2011-01-22 22:29:00 +0800
categories: [Notes, C#]
tags: [C#,stack,heap,ValueType,ReferenceType]
---

## 實值類型 & 參考類型

- 實值類型(Stack 堆疊/棧)
int, double, char, decimal, bool enum, struct    

- 參考類型(Heap 堆積/堆)
string、陣列、自定義類、集合、object、介面    

他們在記憶體上儲存的特點是：    
實值類型的值是儲存在Stack堆疊/棧上    
參考類型的值是儲存在Heap堆積/堆上    

## 值傳遞 & 引用傳遞
- 值類型在複製的時候，傳遞的是這個值的本身。
- 引用類型在複製的時候，傳遞的是這個物件的引用。

### 值傳遞
``` c#
int n1 = 10;
int n2 = n1;
n2 = 20;
Console.WriteLine(n1);//10
Console.WriteLine(n2);//20
Console.ReadKey();
```
他傳遞值的方式是直接把n1的值本身複製給n2，n2再直接重新賦值20，沒有影響到n1。

### 引用傳遞
傳遞的是物件的地址(地址的引用)    

new的時候，會在heap開一塊空間，裡面存著物件。對象是在heap中儲存著。    

new 的時候會做三件事：
1. 在heap堆積中開闢一塊空間
2. 在開闢的空間中，創建一個對象
3. 調用這個對象的構造函式

### 舉例1
```c#
//p1, p2 都指向同一個內存、同一個空間、同一個對象
//所以不管操作p1、p2，他們兩個都會改變
Person p1 = new Person();
p1.Name = "張三";
Person p2 = p1; //指向同一個對象
p2.Name = "李四";
Console.WriteLine(p1.Name); //李四

public class Person {
    private string _name;
    public string Name {
        get { return _name; }
        set { _name = value; }
    }
}
```
p1, p2 都指向同一個內存、同一個空間、同一個對象  
所以不管操作p1、p2任一個，另一個也會改變    

p1、p2在Stack(堆疊/棧)上儲存的是地址位置(追蹤地址)，  
內容是儲存在Heap(堆積/堆)上，
Heap(堆積/堆)上的內容是一樣的，因為p1、p2都指向同一個對象

怎麼驗証它？    
在即時運算視窗輸入 & p1 & p2

```text
& p1
0x000000340897e5d8 //占存地址
    *& p1: 0x000001abaddeca58 //追蹤地址
& p2
0x000000340897e5d0
    *& p2: 0x000001abaddeca58
```
占存地址不同，追蹤地址是一樣的

### 舉例2

```c#
//p1, p2 都指向同一個內存、同一個空間、同一個對象
//所以不管操作p1、p2，他們兩個都會改變
Person p1 = new Person();
p1.Name = "張三";
Person p2 = p1; //指向同一個對象
p2.Name = "李四";
Console.WriteLine(p1.Name); //李四

//舉例2: p1,p2,test()裡的p, 這三個都是指向同一個對象
Test(p2);
Console.WriteLine(p1.Name); //AAA
Console.WriteLine(p2.Name); //AAA
Console.ReadKey();

static void Test(Person pp) {
    Person p = pp; //指向同一個對象
    p.Name = "AAA";
}

public class Person {
    private string _name;
    public string Name {
        get { return _name; }
        set { _name = value; }
    }
}
```
p1、p2、p (Test()裡的p), 這三個都是指向同一個對象。  
所以在 Test()裡重新賦值 p.Name = "AAA"，p1、p2 輸出的的Name也會是"AAA"。

## 比較特的是字串string 不可變性
string 每次給他賦值，都會重新開闢一塊新的空間

```c#
string s1 = "張三";
string s2 = s1;
s2 = "李四";
Console.WriteLine(s1); //張三
Console.WriteLine(s2); //李四

Console.ReadKey();
```
因為string每次給他賦值，都會重新開闢一塊新的空間  
所以s1、s2在heap堆積中是兩塊不同的空間，  
s1、s2不是同一塊空間，
你改變其中一個，另一個不會有影響。  
這就是字串的不可變性。  
  
---
  
### 值類型傳遞
```c#
int number = 10;
Test(number);
Console.WriteLine(number); //10

void Test(int n) {
    n += 10;
}
```
number、n 兩塊不同的空間

### 參數加上ref
```c#
int number = 10;
Test(ref number);
Console.WriteLine(number); //20

void Test(ref int n) {
    n += 10;
}
```
為什麼number=20？  
因為ref的作用能把一個變量以參數的形式帶到一個方法中進行改變。  
再將改變後的值從方法中帶出來。  

他的原理是什麼？    
```text
& number
0x000000094677e518 //Stack地址
    *& number: 20 //Stack存的值
& n
0x000000094677e518
    *& n: 10
```
number、n 他們本來不是同一塊空間，神奇的發現，加了ref，他們在Stack(堆疊/棧)地址是一樣的，變成同一塊空間。