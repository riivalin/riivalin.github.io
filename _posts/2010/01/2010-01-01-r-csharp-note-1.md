---
layout: post
title: "[C# 筆記] 變數、決策、迴圈"
date: 2010-01-01 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,break,continue,for,do-while,while,switch-case]
---

## 【理論】變量與數據類型
### 變量的基本定義
```c#
type name = data;
//數據類型 變量名稱 = 具體數據

//ex. 保存一個整數 5
int number =  5;

//一次聲明多個變量
int num1, num2, num3;
```

### C#中的重要類型
- float(小數)，float pi = 3.1415
- bool(真、假)，bool isRound = true
- string(一段文字、字符串)，string hello = "hello world"
- char(字符)
- double(雙精度小數)
- decimal

### 內建類型，但不屬於基本類型
- 中文得用`string`類型      
`string`可以通過把多個`Unicode`字符拼接起來，顯示中文、甚至是`emoji`    
- `object`對象類型
- `dynamic`動態類型

## 決策與分支
### if
```c#
//if
if(條件) {
    //執行程式碼邏輯
}
//if-else
if(天氣好) {
    //我就去游泳
} else {
    //我就在家睡大覺
}
//if-else if
if(天氣好) {
    //我就去游泳
} else if(下雪) {
    //我就去堆雪人
} else {
    //我就在家睡大覺
}
```

### switch-case-break(return)
```c#
switch(條件表達式) {
    case 情況1:
        //執行邏輯
        break;
    case 情況2:
        //執行邏輯
        break;
    default: //默認情況(可選)
        //執行邏輯
        break;
}

switch(天氣情況) {
    case 天氣好: 我就去游泳; break;
    case 下雪: 我就去堆雪人; break;
    default: 在家睡大覺; break;
}
```

### :? 操作符(三元操作)
```c#
condition?consequent:alternative
條件(true or false) ? 結果1 : 結果2
天氣==好?我就去游泳:我就在家睡大覺
```
## 程式循環(迴圈)
- for
- while
- do-while

- `break`：可以在迴圈中使用`break`終止循環。(徹底終止)
- `continue`：可以在迴圈中使用`continue`，只跳過本次循環，同時進行下一次循環。

### for
```c#
for(初始值; 循環條件; 變化量) {
    //執行代碼
}
for(i=0; i<10; i++) {

}
```

### while
- 當…的時候
- 先進行條件檢查，只有當條件滿足的時候才進入循環

```c#
int i=0;
while(i<5) {
    //程式碼邏輯
    i++;
}
```

### do-while
先循環，再檢查

```c#
int i=0;
do {
   //程式碼邏輯 
} while(i<5)
```
## break, continue
### break跳出迴圈
可以在迴圈中使用`break`跳出迴圈(終止循環)。

```c#
for (int i = 0; i < 10; i++) {
    if (i == 5) {
        Console.WriteLine("終止循環");
        break;
    }
    Console.WriteLine(i);
}
```
輸出：1,2,3,4

### continue只跳過本次循環
可以在迴圈中使用`continue`，只跳過本次循環，同時進行下一次循環，而不希望徹底終止循環。

```c#
for (int i = 0; i < 10; i++) {
    if (i % 2 == 0) {
        Console.WriteLine("跳過偶數");
        continue;
    }
    Console.WriteLine(i);
}
```
輸出：1,3,5,7,9

## 練習：完成用戶登入流程(循環)
主表單：1.客戶管理 2.預約管理 3.系統管理 4.退出

思路
- 先將邏輯程式碼寫好
- 因為要不斷的重複檢查帳號密碼，所以使用`do-while(true)`無限循環包起來。
- 選擇主表單後，就不該再輸入帳號密碼，所以還需要一個內部循環，把主表單包起來。
- 如果用戶輸入4退出，就不再循環，所以要加一個局部變量`isExit`，預設`false`。
- 而外部迴圈`do-while(true)`的`true`也要修改，要改為`!isExit`。
- 如果當用戶輸入4退出，要設置`isExit=true`。

```c#
Console.WriteLine("======客戶管理系統======");
Console.WriteLine("請登入");

bool isExit = false;
do
{
    string username;
    string password;

    Console.WriteLine("請輸入帳號");
    username = Console.ReadLine()!;

    if (username == "riva")
    {
        Console.WriteLine("請輸入密碼");
        password = Console.ReadLine()!;
        if (password == "1234")
        {
            while (!isExit)
            {
                Console.WriteLine("請選擇主表單：1.客戶管理 2.預約管理 3.系統管理 4.退出");
                string selection = Console.ReadLine()!;
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

        } else
        {
            Console.WriteLine("密碼錯誤，請重新輸入");
        }
    } else
    {
        Console.WriteLine("帳號錯誤，請重新輸入");
    }
} while (!isExit);
```

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=13](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=13)