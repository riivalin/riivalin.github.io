---
layout: post
title: "[C# 筆記][IOC][介面導向] 用戶管理(重構+介面)"
date: 2010-01-22 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,interface,多型,ioc,介面導向]
---

[[C# 筆記] 用戶管理(重構)](https://riivalin.github.io/posts/2010/01/r-csharp-note-10/#整合套用)

- 用戶系統-用戶登入 `User`
- 表單系統-顯示表單 `Menu`
- 控制系統-流程控制 `CMSController`

## User

- `isUserLogin`從「成員變量」改為「成員屬性」

改成屬性有什麼好處呢？我們的 `User`就可以通過接口(介面)來對 `IsUserLogin`這個屬性來進行訪問了。

```c#
public class User : IUser
{
    public bool IsUserLogin { get; set; }
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
        IsUserLogin = true;
    }
}
```
## 提取介面

### IUser

- 點選 `User` 按右鍵 > 提取介面

現在我們專案中就創建了一個 `IUser`

```c#
public interface IUser
{
    bool IsUserLogin { get; set; }
    void Login();
}
```
同樣的，我們給 `CMSController` 以及 `Menu` 加上他們所對應的接口(介面).  (在加入接口(介面)前，先修正程式碼的錯誤)

### ICMSController
- 點選 `CMSController` 按右鍵 > 提取介面

```c#
public interface ICMSController {
    void Start(Program.User user, Program.Menu menu);
}
```

### IMenu
- 點選 `Menu` 按右鍵 > 提取介面
```c#
public interface IMenu {
    void ShowMenu();
}
```

## 把User,Menu服務註冊到 CMSController
現在我們就得到了 `IUser`、`IMenu`、`ICMSController`這三個接口(介面)。       

現在到 `CMSController`，我們把 `IUser`、`IMenu`這兩個服務註冊到 `CMSController`中：

- 首先先創建這兩個私有服務所對應的成員變量  

```c#
private readonly IUser _user;
private readonly IMenu _menu;
```

- 然後把這兩個服務，通過構造方法的參數傳入進來  

```c#
public CMSController(IUser user, IMenu menu)
{
    _user = user;
    _menu = menu;
}
```

- 然後在 `Start()`方法中，把 `user`和 `menu`這兩個參數去掉  
- 修改一下變數，因為`user`和 `menu`已經從構造方法中傳遞進來了。

```c#
public void Start()
{
    //login
    do
    {
        _user.Login();
    } while (!_user.IsUserLogin);

    //顯示菜單
    _menu.ShowMenu();
}
```

- 還有介面 `ICMSController`，把 `user`和 `menu`這兩個參數一併刪除   

```c#
public interface ICMSController {
    void Start();
}
```

## 使用 IOC 反轉控制容器
### 創建IOC+注入服務

回到`Main 方法`，可以添加反轉控制容器了。     

在添加反轉控制容器之前，先刪除所有的程式碼，然後添加`IOC`容器框架：

- `NuGet` > `Microsoft.Extensions.DependencyInjection`
-  `using Microsoft.Extensions.DependencyInjection;`

準備工作都完成了，現在可以創建「`IOC`反轉控制容器」了。

- 初始化本地變量 `ServiceCollection`

```c#
ServiceCollection collection = new ServiceCollection(); //IOC
```

這個 `ServiceCollection`就是我們的`IOC`。       

現在向這個 `IOC` 中分別注入：用戶User、菜單Menu、程序控制 這三個服務，
使用` AddScoped`：

- 第一個用戶：使用「用戶服務介面 `IUser`」，實現`User`這個類
- 第二個菜單：使用「菜單服務介面 `IMenu`」，實現`Menu`這個類
- 第三個程序控制：使用「程序控制介面 `ICMSController`」，實現`CMSController`這個類

好了，服務全部註冊完了。        

```c#
using Microsoft.Extensions.DependencyInjection;

static void Main(string[] args)
{
    //創建「IOC反轉控制容器」
    ServiceCollection collection = new ServiceCollection(); //IOC

    //注入服務
    collection.AddScoped<IUser, User>(); //使用用戶服務介面 IUser，實現 User 這個類
    collection.AddScoped<IMenu, Menu>(); //使用菜單服務介面 IMenu，實現 Menu 這個類
    collection.AddScoped<ICMSController, CMSController>();


    Console.Read();
}
```

這就代表我們這三個服務的生命周期，全部都已經交給`IOC` 容器 `collection` 進行托管了。接下來我們只需要從`IOC`中，通過介面來撈取對應的服務就可以了，從此我們再也不需要手動創建和管理這三個服務了。     

### 創建 build service provider
接下來創建 `build service provider`

```c#
//創建 build service provider
var serviceProvider = collection.BuildServiceProvider();
```

### 提取的服務 CMSController
而我們需要提取的服務，其實就是這個 `CMSController`，不過，提取`CMSController`實體必須通過介面，但是需要泛型來指定介面

```c#
//所需提取的 CMSController
var cmsController = serviceProvider.GetService<ICMSController>();//提取CMSController實體必須通過介面，但是需要泛型來指定介面
```
這個就是我們所需提取的 `CMSController`。        

現在重構就進入尾聲了。      

## 啟動「整個CMS程序」
我們最後一步就是：通過這個 `cmsController`來啟動「整個CMS程序」

```c#
//啟動「整個CMS程序」
cmsController.Start();
```
而這個 `start()`我們不需要傳入 用戶`User`、菜單`Menu`，用戶`User`、菜單`Menu`這兩個系統也同樣已經托管給 `ICO`了。       

`IOC` 會自動選擇合適的時機，在`cmsController`實體化的同時，在構造方法中完成這兩個服務的依賴注入。 

```c#
static void Main(string[] args)
{
    //創建「IOC反轉控制容器」
    ServiceCollection collection = new ServiceCollection(); //IOC
    //注入服務
    collection.AddScoped<IUser, User>(); //使用用戶服務介面 IUser，實現 User 這個類
    collection.AddScoped<IMenu, Menu>(); //使用菜單服務介面 IMenu，實現 Menu 這個類
    collection.AddScoped<ICMSController, CMSController>();

    //創建 build service provider
    var serviceProvider = collection.BuildServiceProvider();
    //所需提取的 CMSController
    var cmsController = serviceProvider.GetService<ICMSController>();//提取CMSController實體必須通過介面，但是需要泛型來指定介面

    //啟動「整個CMS程序」
    cmsController.Start();

    Console.Read();
}
```
好了，重構完成!     

我們的程式碼經過「面向接口(介面導向)」的調整，實現了用戶、菜單、程序控制 這三個部分的完全解耦，任何一個組件我們都可以把它獨立出來，這樣的程式碼更加富有擴展性，以及維護性。


## 完整程式碼

```c#
//用戶
public class User : IUser
{
    public bool IsUserLogin { get; set; }
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
        IsUserLogin = true;
    }
}
//菜單
public class Menu : IMenu
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
//程序控制
public class CMSController : ICMSController
{
    private readonly IUser _user;
    private readonly IMenu _menu;

    public CMSController(IUser user, IMenu menu)
    {
        _user = user;
        _menu = menu;
    }

    public void Start()
    {
        //login
        do
        {
            _user.Login();
        } while (!_user.IsUserLogin);

        //顯示菜單
        _menu.ShowMenu();
    }
}

//User介面
public interface IUser {
    bool IsUserLogin { get; set; }
    void Login();
}
//Menu介面
public interface IMenu {
    void ShowMenu();
}
//CMSController介面
public interface ICMSController {
    void Start();
}

//整個CMS程序-Main方法
namespace CMS
{
    public partial class Program
    {
        public static string CmdReader(string msg)
        {
            Console.WriteLine(msg);
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            //創建「IOC反轉控制容器」
            ServiceCollection collection = new ServiceCollection(); //IOC
            //注入服務
            collection.AddScoped<IUser, User>(); //使用用戶服務介面 IUser，實現 User 這個類
            collection.AddScoped<IMenu, Menu>(); //使用菜單服務介面 IMenu，實現 Menu 這個類
            collection.AddScoped<ICMSController, CMSController>();

            //創建 build service provider
            var serviceProvider = collection.BuildServiceProvider();
            //所需提取的 CMSController
            var cmsController = serviceProvider.GetService<ICMSController>();//提取CMSController實體必須通過介面，但是需要泛型來指定介面

            //啟動「整個CMS程序」
            cmsController.Start();

            Console.Read();
        }
    }
}
```


[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=46](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=46)