---
layout: post
title: "[C# 筆記] 用戶管理(重構)"
date: 2010-01-10 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R]
---

[用戶登入過程](https://riivalin.github.io/posts/2010/01/r-csharp-note-2/#練習函數化用戶登入過程)        

重構用戶登入：
- 用戶系統-用戶登入 `User`
- 菜單系統-顯示菜單 `Menu`
- 控制系統-流程控制 `CMSController`

## 用戶系統-用戶登入 User
- 新增一個`User class`，把登入的部分放到 `User class` 中
- 還需要一個 `isUserLogin`變量

```c#
public class User
{
    public bool isUserLogin = false;
    public void Login()
    {
        string username;
        string password;

        //檢查帳號
        username = CmdReader("請輸入帳號");
        if (username != "riva")
        {
            Console.WriteLine("查無此人");
            return;
        }

        //檢查密碼
        password = CmdReader("請輸入密碼");
        if (password != "1234")
        {
            Console.WriteLine("密碼錯誤");
            return;
        }
        isUserLogin = true;
    }
}
```

## 菜單系統-顯示菜單 Menu
新增一個`Menu class`，把顯示菜單的部分放到 `Menu class` 中

```c#
public class Menu
{
    public void ShowMenu()
    {
        bool isExit = false;
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
    }
}
```

## 控制系統-流程控制 CMSController
- 新增一個`CMSController class`，用來控制整個流程
- 新增`Start`方法，可以傳入 `User`, `Menu`
- 裡面的程式碼就是：登入和顯示菜單

```c#
public class CMSController
{
    public void Start(User user, Menu menu)
    {
        //login
        do
        {
            user.Login();
        } while (!user.isUserLogin);

        //顯示菜單
        menu.ShowMenu();
    }
}
```

## 整合套用
- 回到`Main`方法，初始化這三大系統：用戶系統`User`、菜單系統`Menu`、流程系統`CMSController`
- 調用流程系統的 `Start` 方法，並傳入`User`和`Menu`

```c#
static void Main(string[] args)
{
    //初始化用戶系統
    User user = new User();
    //初始化菜單系統
    Menu menu = new Menu();
    //初始化cmscontroller
    CMSController cms = new CMSController();

    Console.WriteLine("======客戶管理系統======");
    Console.WriteLine("請登入");

    //啟動cms
    cms.Start(user, menu);

    Console.Read();
}

static string CmdReader(string msg)
{
    Console.WriteLine(msg);
    return Console.ReadLine();
}

public class User
{
    public bool isUserLogin = false;
    public void Login()
    {
        string username;
        string password;

        //檢查帳號
        username = CmdReader("請輸入帳號");
        if (username != "riva")
        {
            Console.WriteLine("查無此人");
            return;
        }

        //檢查密碼
        password = CmdReader("請輸入密碼");
        if (password != "1234")
        {
            Console.WriteLine("密碼錯誤");
            return;
        }
        isUserLogin = true;
    }
}

public class Menu
{
    public void ShowMenu()
    {
        bool isExit = false;
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
    }
}

public class CMSController
{
    public void Start(User user, Menu menu)
    {
        //login
        do
        {
            user.Login();
        } while (!user.isUserLogin);

        //顯示菜單
        menu.ShowMenu();
    }
}
```

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=26](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=26)