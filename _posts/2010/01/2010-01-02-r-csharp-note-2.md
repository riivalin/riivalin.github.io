---
layout: post
title: "[C# 筆記] 方法"
date: 2010-01-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,Lambda]
---

## 什麼是方法？
- 大括號包起來的程式碼區塊
- 可以調用
- 可以執行
- 在C#中，所有的指令都必須在方法中才能執行

```c#
<Access Specifier> <Modifier> <Return Type> <Method Name> (Parameter List) {
    Method Body
}
訪問修飾符 聲明修飾符 返回類型 方法名稱(參數) {
    程式碼邏輯
}
```

### 訪問修飾符 Access Specifier
方法的訪問修飾符

```
1. Public 公有方法，可以被外部調用
2. Private 表示私有，方法會被隱藏起來，其他的class不可以調用
3. Protected 表示受保護，只能在它的類本身或是它的派生類中訪問
4. Private protected
5. Internal 內部方法，同一個程序集中的所有類都可以訪問
6. Protected internal
```

### 聲明修飾符 Modifier

```
1. Static  表示靜態類型
2. Abstract 表示抽象類型
3. Virtual 允許派生類重寫的虛方法
4. Override 允許方法繼承後重寫
5. New 可以隱藏基類成員
6. Sealed 表示不能被繼承
7. Partial 允許同一個程序集分散定義
8. Extern 用於聲明外部實現的Extern
```

### 返回類型 Return Type
方法返回類型的數據類型。
- `return` 方法的返回值，也就是方法的最終計算結果。
- 如果方法不返回任何值，則返回類型為`void`.  

### Method Name 方法名稱
方法的名稱，它是唯一的標識符，區分大小寫。

### 參數 Parameter List
參數列表
1. 方法接數據
2. 參數間用逗號相隔

### 程式碼邏輯 Method Body
完成任務的指令集。

## 方法練習
取得最大值

```c#
static int GetMax(int num1, int num2) {
    return num1 > num2 ? num1 : num2;
}
```
## 方法總結
- Access Specifier：訪問修飾符，決定了方法是否可以外部訪問
- Return Type：返回類型，方法的最終計算結果，如果方法不返回任何值，則返回類型為`void`
- Method Name：方法名稱，用以從外部調用
- Parameter List：參數列表，可選項目
- Method Body：方法主體，包含了完成任務所需的邏輯程式碼

## 形參與實參
- 形參，parameter，方法中定義的形式上的參數
- 實參，argument，真正調用方法過程中傳入的具體數據

### 形參 parameter
「形參」只會出現在定義方法中        
比方如下：在小括號中的參數，num1,num2是形參。

```c#
static int GetMax(int num1, int num2) {
    return num1 > num2 ? num1 : num2;
}
```
### 實參 argument
「實參」就是調用方法中，給方法傳遞的實際數據。      
比方如下：1、99就是實參。

```c#
int result = GetMax(1, 99); //1、99就是實參
```

實參可以使用數據，也可以使用變量：
```c#
int x = 1;
int y = 99;
int result = GetMax(x, y);
```

## 判斷形參&實參
判斷「形參」與「實參」最簡單的四法：
- 「形參」定義在方法中
- 「實參」定義在方法外部

## 方法返回
- `return` 方法的返回值，也就是方法的最終計算結果。
- 如果方法不返回任何值，則返回類型為`void`.  
- 使用 `return` 則意味著方法徹底停止。
- 特殊需要，你也可以使用`return` 來提前結束程式碼。

## Lambda表達式(箭頭`=>`)

- 使用「箭頭`=>`」來簡化程式碼，主體使用「箭頭`=>`」來代替大括號，
```c#
public static int GetMa(int num1, int num2) => 
num1>num2?num1:num2;
```

## 練習：函數化用戶登入過程
[尚未函數化的用戶登入](https://riivalin.github.io/posts/2010/01/r-csharp-note-1/)

1.逆向思維，把「正向檢查」變為「逆向檢查」

```c#
if (username != "riva") {
    Console.WriteLine("查無此人");
    return; //提前結束程式
} else {
}
```

2.把`else`分支去掉，通過`return`提前結束程式流程，這樣程式碼看起來會更乾淨一點。

```c#
if (username != "riva") {
    Console.WriteLine("查無此人");
    return; //提前結束程式
}
```

3.檢查密碼也是，流程比較清晰。

```c#
do {
    //檢查帳號
    if(username!="riva") {
        Console.WriteLine("查無此人");
        continue;//本次結束，開始新的循環
    }
    //檢查密碼
    if(password!="1234") {
        Console.WriteLine("密碼錯誤");
        continue;//本次結束，開始新的循環;
    }
    //選擇主選單
    while(!=isExit) {
        Console.WriteLine("主表單：1.客戶管理 2.預約管理 3.系統管理 4.退出");
        string selection = Console.ReadLine();
        ...
    }
} while(!isExit)
```

4.把讀取用戶輸入寫成方法

```c#
static string CmdReader(string msg)
{
    Console.WriteLine(msg);
    return Console.ReadLine();
}
```

5.完成：套用方法，整理一下Code

```c#
static string CmdReader(string msg)
{
    Console.WriteLine(msg);
    return Console.ReadLine();
}

static void Main(string[] args)
{
    Console.WriteLine("======客戶管理系統======");
    Console.WriteLine("請登入");

    bool isExit = false;
    do
    {
        string username;
        string password;

        //檢查帳號
        username = CmdReader("請輸入帳號");
        if (username != "riva")
        {
            Console.WriteLine("查無此人");
            continue;//本次結束，開始新的循環
        }

        //檢查密碼
        password = CmdReader("請輸入密碼");
        if (password != "1234")
        {
            Console.WriteLine("密碼錯誤");
            continue;//本次結束，開始新的循環;
        }

        //選擇主表單
        while (!isExit)
        {
            string selection = CmdReader("主表單：1.客戶管理 2.預約管理 3.系統管理 4.退出/n請選擇：");
            switch (selection)
            {
                case "1":
                    Console.WriteLine("客戶管理");
                    break;
                case "2":
                    Console.WriteLine("預約管理");
                    break;
                case "3":
                    Console.WriteLine("系統管理");
                    break;
                case "4":
                default:
                    Console.WriteLine("退出");
                    isExit = true;
                    break;
            }
        }
    } while (!isExit);
}
```