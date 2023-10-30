---
layout: post
title: "[閱讀筆記][Design Pattern] Ch15.抽象工廠模式(Abstract Factory)"
date: 2009-03-15 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

資料庫存取
- 用工廠方法模式
- 用抽象工廠模式
- 用簡單工廠來改進抽象工廠
- 用反射+抽象工廠
- 用反射+設定檔     

---

設計模式中的工廠模式（Factory Design pattern）是一個比較常用的創建型設計模式，其中可以細分為三種：簡單工廠（Simple Factory）、工廠方法(Factory Method)和抽象工廠(Abstract Factory)。那麼三者有什麼差別呢？

- 簡單工廠：只有唯一工廠（簡單工廠），一個產品介面/抽象類，根據簡單工廠中的靜態方法來創建具體產品物件。適用於產品較少，幾乎不擴展的情景
- 工廠方法：有多個工廠（抽象工廠+多個具體工廠），一個產品介面/抽象類，根據繼承抽象工廠中的方法來多態建立具體產品物件。適用於一個類型的多個產品
- 抽象方法：有多個工廠（抽象工廠+多個特定工廠），多個產品介面/抽象類，將產品子類別分組，根據繼承抽象工廠中的方法多態建立同組的不同特定產品物件。適用於多個類型的多個產品


# 抽象工廠模式(Abstract Factory)
抽象工廠模式(Abstract Factory)，提供一個建立一系列相關或相互依賴物件的介面，而無需指定它們具體的類別。

## 結構

- `AbstractProductA`、`AbstractProductB`是兩個抽象產品，之所以為抽象，是因為它們都有可能有兩種不同的實現。
- `ProductA1`、`ProductA2`、`ProductB1`、`ProductB2`就是對兩個抽象產品的具體分類的實現。
- `IFactory`是一個抽象工廠介面，它裡面應該包含所有產品建立的抽象方法。
- `ConcreteFactory1`、`ConcreteFactory2`就是具體工廠，通常是在執行時再建立一個`ConcreteFactory`類別的實體，這個具體的工廠再建立具有特定實現的產品物件，也就是說，為建立不同的產品物件，用戶端應使用不同的具體工廠。

```
Client
    AbstractFactory 抽象工廠介面，它裡面應該包含所有產品建立的抽象方法
    + CreateProductA()
    + CreateProductB()

        ConcreteFactory1 具體的工廠，建立具有特定實現的產品物件
        ConcreteFactory1

    AbstractProductA 抽象產品，它們都有可能有兩種不同的實現
        ProductA1 對兩個抽象產品的具體分類實現
        ProductA2
    AbstractProductB
        ProductB1
        ProductB2
```

## 優缺點

### 優點:
第一：最大的好處是易於交換產品系列，由於具體工廠類別，例如：`IFactory factory = new SqlServerFactory();`，在一個應用中只需要初始化的時候出現一次，這就使得改變一個應用的具體工廠變得非常容易，它只需要改變具體工作即可使用不同的產品設定。        

我們的設計不能去防止需求的更改，那麼我們的理想便是讓改動變得更小，現在如果要更改資料庫存取，只需要更改具體工廠就可以做到。      

第二：它讓具體的建立實體過程與用戶端分離，用戶端是透過它們的抽象介面操縱實體，產品的具體類別名也被具體工廠的實現分離，不會出現在客戶程式碼中。

### 缺點:

抽象工廠模式可以很方便地切換兩個資料庫存取的程式碼，但是，如果需求來自「增加功能」，那至少要增加三個類別：`IProduct`、`SqlServerProdject`、`AccessProject`，還需要更改三個類別`IFactory`、`SqlServerFactory`、`AccessFactory`，這太糟糕了。     

用戶端程式類別顯然不會只有一個，如果有100個調用資料庫存取的類別，那就要改100次了，這不能解決要更改資料庫存取時，改動一處就完全更改的要求。      

這樣大批量的改動，顯然是非常醜陋的做法。


# 最基本的資料存取程式

假設用戶類別只有ID和Name兩個欄位，其餘省略

```c#
class User {
    public int ID { get; set; }
    public string name { get;set; }
}
```

SqlServerUser類別，用於操作User表，假設只有「新增用戶」和「取得用戶」方法，其餘方法以及具體的SQL敘述省略。

```c#
class SqlServerUser {
    //新增
    public void InsertUser(User user) {
        Console.WriteLine("在SQL Server中給User新增一筆記錄");
    }
    //取得用戶
    public User GetUserById(int id) {
        Console.WriteLine("在SQL Server中根據ID取得User 表一筆記錄");
        return null;
    }
}
```

用戶端程式碼

```c#
User user = new User();
SqlServerUser su = new SqlServerUser();
su.InsertUser(user); //新增用戶
su.GetUserById(1); //取得用戶ID為1的資料
```


# 用工廠方法模式的資料存取
     
工廠方法模式是定義一個用於建立物件的介面，讓子類別決定實體化哪一個類別。    
用「[工廠方法模式](https://riivalin.github.io/posts/2009/03/ch8-factory/)」來封裝`new SqlServerUser()`所造成的變化。        

> 工廠方法：有多個工廠（抽象工廠+多個具體工廠），一個產品介面/抽象類，根據繼承抽象工廠中的方法來多態建立具體產品物件。適用於一個類型的多個產品。 

## 結構
```
IUser
    SqlServerUser
    AccessUser

IFactory
+ CreateUser()

    SqlServerFactory
    + CreateUser()
    AccessFactory
    + CreateUser()
```

## 程式碼
### IUser介面

- IUser介面，用於用戶端存取，解除與具體資料存取的耦合
- SqlServerUser類別，用於存取SqlServer的User
- AccessUser類別，用於存取Access的User

```c#
//IUser介面，用於用戶端存取，解除與具體資料存取的耦合
interface IUser {
    void InsertUser(User user);
    User GetUserById(int id);
}
//SqlServerUser類別，用於存取SqlServer的User
class SqlServerUser: IUser {
    //新增
    public void InsertUser(User user) {
        Console.WriteLine("在SQL Server中給User新增一筆記錄");
    }
    //取得用戶
    public User GetUserById(int id) {
        Console.WriteLine("在SQL Server中根據ID取得User 表一筆記錄");
        return null;
    }
}
//AccessUser類別，用於存取Access的User
class AccessUser: IUser {
    //新增
    public void InsertUser(User user) {
        Console.WriteLine("在Access中給User新增一筆記錄");
    }
    //取得用戶
    public User GetUserById(int id) {
        Console.WriteLine("在Access中根據ID取得User 表一筆記錄");
        return null;
    }
}
```

### IFactory介面

- IFactory介面，定義一個建立存取User表物件的抽象的工廠介面
- SqlServerFactory類別，實現IFactory介面，實體化SqlServerUser
- AccessFactory類別，實現IFactory介面，實體化AccessUser

```c#
//IFactory介面，定義一個建立存取User表物件的抽象的工廠介面
interface IFactory {
    IUser CreateUser();
}
//SqlServerFactory類別，實現IFactory介面，實體化SqlServerUser
class SqlServerFactory {
    public IUser CreateUser() {
        return new SqlServerUser();
    }
}
//AccessFactory類別，實現IFactory介面，實體化AccessUser
class AccessFactory {
    public IUser CreateUser() {
        return new AccessUser();
    }
}
```

### 用戶端程式碼

如果要更改db，只要把`new SqlServerFactory();`改成`new AccessFactory();`。   
由於多型的關係，使得宣告`IUser`介面的物件`user`事先根本不知道是誰在存取哪個資料庫，卻可以在執行時順利地完成工作，這就是所謂的業務邏輯與資料存取的解耦。

```c#
User user = new User();

IFactory factory = new SqlServerFactory(); //SqlServer資料庫
//要更改Access資料庫，只要將本句改成:
//IFactory factory = new AccessFactory(); //Access資料庫

IUser iuser = factory.CreateUser();
iuser.InsertUser(user);
iuser.GetUserById(1);
```

## 完整程式碼

```c#
//User表
class User {
    public int ID { get; set; }
    public string name { get;set; }
}

//IUser介面，用於用戶端存取，解除與具體資料存取的耦合
interface IUser {
    void InsertUser(User user);
    User GetUserById(int id);
}

//實作IUser介面：SqlServerUser類別，用於存取SqlServer的User
class SqlServerUser: IUser {
    //新增
    public void InsertUser(User user) {
        Console.WriteLine("在SQL Server中給User新增一筆記錄");
    }
    //取得用戶
    public User GetUserById(int id) {
        Console.WriteLine("在SQL Server中根據ID取得User 表一筆記錄");
        return null;
    }
}
//實作IUser介面：AccessUser類別，用於存取Access的User
class AccessUser: IUser {
    //新增
    public void InsertUser(User user) {
        Console.WriteLine("在Access中給User新增一筆記錄");
    }
    //取得用戶
    public User GetUserById(int id) {
        Console.WriteLine("在Access中根據ID取得User 表一筆記錄");
        return null;
    }
}

//IFactory介面，定義一個建立存取User表物件的抽象的工廠介面
interface IFactory {
    IUser CreateUser();
}
//實作IFactory介面：SqlServerFactory類別，實現IFactory介面，實體化SqlServerUser
class SqlServerFactory:IFactory {
    public IUser CreateUser() {
        return new SqlServerUser();
    }
}
//實作IFactory介面：AccessFactory類別，實現IFactory介面，實體化AccessUser
class AccessFactory:IFactory {
    public IUser CreateUser() {
        return new AccessUser();
    }
}

//用戶端調用
public static void Main()
{
    User user = new User();

    IFactory factory = new SqlServerFactory(); //SqlServer資料庫
    //要更改Access資料庫，只要將本句改成:
    //IFactory factory = new AccessFactory(); //Access資料庫

    IUser iuser = factory.CreateUser();
    iuser.InsertUser(user);
    iuser.GetUserById(1);
}
```

# 用抽象工廠方法模式(增加部門表)

資料庫裡不可能只有一個User表，還有很多其他表，比如：部門表Department。

```c#
class Department {
    public int ID {get; set; }
    public string DeptName{ get; set; }
}
```

> 抽象方法：有多個工廠（抽象工廠+多個特定工廠），多個產品介面/抽象類，將產品子類別分組，根據繼承抽象工廠中的方法多態建立同組的不同特定產品物件。適用於多個類型的多個產品

## 結構

```
IUser
    SqlServerUser
    AccessUser
IDepartment
    SqlServerDepartment
    AccessDepartment

IFactory
+ CreateUser()
+ CreateDepartment()

    SqlServerFactory
    + CreateUser()
    + CreateDepartment()
    AccessFactory
    + CreateUser()
    + CreateDepartment()
```

## 程式碼
### IDepartment介面

- IDepartment部門介面，用於用戶端存取，解除與具體資料庫存取的耦合
- SqlServerDepartment類別，用於存取SQL Server的Department
- AccessDepartment類別，用於存取Access的Department

```c#
//IDepartment部門介面，用於用戶端存取，解除與具體資料庫存取的耦合
interface IDepartment {
    void InsertDepartment(Department department);
    Department GetDepartmentById(int id);
}
//SqlServerDepartment類別，用於存取SQL Server的Department
class SqlServerDepartment: IDepartment {
    //新增
    public void InsertDepartment(Department department) {
        Console.WriteLine("在SQL Server中給Department新增一筆記錄");
    }
    //取得
    public Department GetDepartmentById(int id) {
        Console.WriteLine("在SQL Server中根據ID取得Department 表一筆記錄");
        return null;
    }
}
//AccessDepartment類別，用於存取Access的Department
class AccessDepartment: IDepartment {
    //新增
    public void InsertDepartment(Department department) {
        Console.WriteLine("在Access中給Department新增一筆記錄");
    }
    //取得
    public Department GetDepartmentById(int id) {
        Console.WriteLine("在Access中根據ID取得Department 表一筆記錄");
        return null;
    }
}
```

## IFactory介面

```c#
//IFactory介面，定義一個建立存取User、Department表物件的抽象的工廠介面
interface IFactory {
    IUser CreateUser();
    IDepartment CreateDepartment(); //增加的介面方法
}

//SqlServerFactory類別，實現IFactory介面，實體化SqlServerUser、SqlServerDepartment
class SqlServerFactory: IFactory {
    public IUser CreateUser() {
        return new SqlServerUser();
    }
    public IDepartment CreateDepartment() {
        return new SqlServerDepartment();
    }
}
//AccessFactory類別，實現IFactory介面，實體化AccessUser、SqlServerDepartment
class AccessFactory: IFactory {
    public IUser CreateUser() {
        return new AccessUser();
    }
    public IDepartment CreateDepartment() {
        return new AccessDepartment();
    }
}
```

### 用戶端程式碼

這樣就可以做到，只需要更改`IFactory factory = new SqlServerFactory();`為`IFactory factory = new AccessFactory();`，就實現了資料庫存取的切換了。

> 只有一個User類別和User操作類別時，是只需要工廠方法模式的，但現在顯然資料庫有很多的表，而SQL Server和 Access 又是兩大不同的分類，所以解決這種涉及到多個產品系列的問題，有一個專門的工廠模式叫「抽象工廠模式」。

```c#
User user = new User();
Department dempartment = new Department();

//切換資料庫：只需要確定實體化哪一個資料庫存取物件給factory
IFactory factory = new SqlServerFactory(); //SqlServer資料庫
//IFactory factory = new AccessFactory(); //Access資料庫

//使用者
IUser iuser = factory.CreateUser();
iuser.InsertUser(user);
iuser.GetUserById(1);

//部門
IDepartment idempartment = factory.CreateDepartment();
idempartment.InsertDepartment(dempartment);
idempartment.GetDepartmentById(1);

//執行結果:
//在SQL Server中給User新增一筆記錄
//在SQL Server中根據ID取得User 表一筆記錄
//在SQL Server中給Department新增一筆記錄
//在SQL Server中根據ID取得Department 表一筆記錄
```


### 完整程式碼

```c#
//User表
class User {
    public int ID { get; set; }
    public string name { get;set; }
}
//部門表
class Department {
    public int ID {get; set; }
    public string DeptName{ get; set; }
}

//
//IFactory介面，定義一個建立存取User、Department表物件的抽象的工廠介面
interface IFactory {
    IUser CreateUser();
    IDepartment CreateDepartment(); //增加的介面方法
}

//SqlServerFactory類別，實現IFactory介面，實體化SqlServerUser、SqlServerDepartment
class SqlServerFactory: IFactory {
    public IUser CreateUser() {
        return new SqlServerUser();
    }
    public IDepartment CreateDepartment() {
        return new SqlServerDepartment();
    }
}
//AccessFactory類別，實現IFactory介面，實體化AccessUser、SqlServerDepartment
class AccessFactory: IFactory {
    public IUser CreateUser() {
        return new AccessUser();
    }
    public IDepartment CreateDepartment() {
        return new AccessDepartment();
    }
}


//
//IUser介面，用於用戶端存取，解除與具體資料存取的耦合
interface IUser {
    void InsertUser(User user);
    User GetUserById(int id);
}

//實作IUser介面：SqlServerUser類別，用於存取SqlServer的User
class SqlServerUser: IUser {
    //新增
    public void InsertUser(User user) {
        Console.WriteLine("在SQL Server中給User新增一筆記錄");
    }
    //取得用戶
    public User GetUserById(int id) {
        Console.WriteLine("在SQL Server中根據ID取得User 表一筆記錄");
        return null;
    }
}
//實作IUser介面：AccessUser類別，用於存取Access的User
class AccessUser: IUser {
    //新增
    public void InsertUser(User user) {
        Console.WriteLine("在Access中給User新增一筆記錄");
    }
    //取得用戶
    public User GetUserById(int id) {
        Console.WriteLine("在Access中根據ID取得User 表一筆記錄");
        return null;
    }
}

//
//IDepartment部門介面，用於用戶端存取，解除與具體資料庫存取的耦合
interface IDepartment {
    void InsertDepartment(Department department);
    Department GetDepartmentById(int id);
}
//SqlServerDepartment類別，用於存取SQL Server的Department
class SqlServerDepartment: IDepartment {
    //新增
    public void InsertDepartment(Department department) {
        Console.WriteLine("在SQL Server中給Department新增一筆記錄");
    }
    //取得
    public Department GetDepartmentById(int id) {
        Console.WriteLine("在SQL Server中根據ID取得Department 表一筆記錄");
        return null;
    }
}
//AccessDepartment類別，用於存取Access的Department
class AccessDepartment: IDepartment {
    //新增
    public void InsertDepartment(Department department) {
        Console.WriteLine("在Access中給Department新增一筆記錄");
    }
    //取得
    public Department GetDepartmentById(int id) {
        Console.WriteLine("在Access中根據ID取得Department 表一筆記錄");
        return null;
    }
}

//
//用戶端程式碼
public static void Main()
{
    User user = new User();
    Department dempartment = new Department();

    //切換資料庫：只需要確定實體化哪一個資料庫存取物件給factory
    IFactory factory = new SqlServerFactory(); //SqlServer資料庫
    //IFactory factory = new AccessFactory(); //Access資料庫

    //使用者
    IUser iuser = factory.CreateUser();
    iuser.InsertUser(user);
    iuser.GetUserById(1);

    //部門
    IDepartment idempartment = factory.CreateDepartment();
    idempartment.InsertDepartment(dempartment);
    idempartment.GetDepartmentById(1);
}
//執行結果:
//在SQL Server中給User新增一筆記錄
//在SQL Server中根據ID取得User 表一筆記錄
//在SQL Server中給Department新增一筆記錄
//在SQL Server中根據ID取得Department 表一筆記錄
```

# 用簡單工廠來改進抽象工廠

> 簡單工廠：只有唯一工廠（簡單工廠），一個產品介面/抽象類，根據簡單工廠中的靜態方法來創建具體產品物件。適用於產品較少，幾乎不擴展的情景。       

用一個簡單模式來實現，去除`IFactory`、`SqlServerFactory`、`AccessFactory`三個工廠類別，取而代之的是 `DataAccess`類別。      

但是，需要增加Oracle資料存取，就還要在`DataAccess`類別中每個方法的 `switch` 中加 `case`。
## 結構

```
DataAccess
- db:string
+ CreateUser(): IUser
+ CreateDepartment(): IDepartment

    IUser
        SqlServerUser
        AccessUser
    IDepartment
        SqlServerDepartment
        AccessDepartment
```

## 程式碼

```c#
class DataAccess {
    private static readonly string db = "SqlServer";
    //private static readonly string db = "Access";

    public static IUser CreateUser() {
        IUser result = null;

        //根據db的設定，實體化相應的物件
        switch(db) {
            case "SqlServer":
                result = new SqlServerUser();
                break;
            case "Access":
                result = new AccessUser();
                break;     
        }
        return result;
    }

    public static IDepartment CreateDepartment() {
        IDepartment result = null;
        
        //根據db的設定，實體化相應的物件
        switch(db) {
            case "SqlServer":
                result = new SqlServerDepartment();
                break;
            case "Access":
                result = new AccessDepartment();
                break;     
        }
        return result;
    }
}

//用戶端
User user = new User();
Department department = new Department();

//使用者
IUser iuser = DataAccess.CreateUser();
iuser.insert(user);
iuser.GetUserById(1);

//部門
IDepartment idempartment =  DataAccess.CreateDepartment();
idempartment.InsertDepartment(dempartment);
idempartment.GetDepartmentById(1);
```

但是，需要增加Oracle資料存取，就還要在`DataAccess`類別中每個方法的 `switch` 中加 `case`。

# 用反射+抽象工廠

```c#
Assembly.Load("程式集名稱").CreateInstance("命名空間.類別名稱")
```

```c#
//常規的寫法
IUser result = new SqlServerUser();

//反射的寫法
using System.Reflection;

IUser result = (IUser)Assembly.Load("抽象工廠模式").CreateInstance("抽象工廠模式.SqlServerUser")
```

# 用反射+設定檔

加入一個 `App.Config`檔
```c#
<?xml version="1.0" encoding="utf-8">
<configuration>
    <appSettings>
        <add key="DB" value="SqlServer"/>
    </appSettings>
</configuration>
```

再參考 `System.configuration`

```c#
using System.Configuration;
//讀取設定檔
private static readonly string db = ConfigurationManager.AppSettings["DB"];
```
