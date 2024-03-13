---
layout: post
title: "[C# 筆記] 在 ASP.NET Core MVC 使用 EF Core + Server SQL 操作 CRUD"
date: 2023-08-01 23:59:00 +0800
categories: [Notes,C#,.NET Core,MVC]
tags: [C#,.Net Core,MVC,EF Core,CRUD]
---

Take notes...

- 建立ASP.NET Core MVC專案
- 安裝 Nuget：`Microsoft.EntityFrameworkCore.SqlServer`、`Microsoft.EntityFrameworkCore.Tools`
- 建立 Model，建立一個 Employee Model 來取得資料 (對應資料表所有的欄位，寫成屬性)
- 新增`DbContext.cs`類別(用來跟資料庫溝通)
    1. 繼承`DbContext`
    2. 新增建構函式，調用父類的構造方法。(選擇有`options`參數的建構子，可將`options`傳入父類)
    3. 在`DbContext.cs`中建立`DbSet<Employee>`屬性

> `DbContext` 是`EF Core` 跟資料庫溝通的主要類別，透過繼承 `DbContext` 可以定義跟資料庫溝通的行為。     
> 先建立一個類別繼承 `DbContext`，同時建立`DbSet`。

```c#
namespace ASPNETMVCCRUD.Data
{
    //1. 繼承 DbContext
    public class MVCDemoDbContext : DbContext
    {
        //2. 新增建構函式，使用 base 調用父類建構函式，並將options傳入
        public MVCDemoDbContext(DbContextOptions options) : base(options) { }
        //3. 建立員工實體集合的屬性(Employee型別的DbSet物件)
        public DbSet<Employee> Employees { get; set; }
    }
}
```

- 在`Program.cs`中註冊加入`DbContext`服務(`AddDbContext`)

```c#
//加入DbContext服務
builder.Services.AddDbContext<MVCDemoDbContext>(options => 
    options.UseSqlServer(builder.Configuration
    .GetConnectionString("MvcDemoConnectionString")));
```



# Introduction
## CRUD
- C - Creat
- R - Read
- U - Update
- D - Delete

## MVC
- M - Model
- V - View
- C - Control

## EF Core 
Entity Framework (EF) Core 是常見Entity Framework 資料存取技術的輕量型、可擴充、開放原始碼且跨平台版本。

> Entity Framework Core 工具有助於設計階段開發工作。 它們主要用來管理「移轉」以及將資料庫結構描述反向工程以支援 `DbContext` 和實體類型。

# Create New ASP.NET MVC
Create a new project > ASP.NET Core Web App (MVC) 

# Install Entity Framework Core (EF Core)
專案 > 管理 NuGet 套件：

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

> - `Microsoft.EntityFrameworkCore.SqlServer`   
> 告訴實體框架，我們想要用SQL Server資料庫以建立連接
> - `Microsoft.EntityFrameworkCore.Tools`      
> 要執行CRUD的操作


## 建立 DbContext 類別
- 前置準備：建立 `Employee.cs`，建立 Employee Model 來取得資料，其對應資料表Employee中所有的欄位，寫成屬性。
- 新增 `DbContext.cs` 資料存取的類別，用來跟資料庫溝通，和定義資料Model

### 新增 DbContext.cs
安裝完這兩個套件後，接下來就要建立`DbContext`類別：     
建立「Data」資料夾 > 新增類別「MVCDemoDbContext.cs」

> `DbContext` 是`EF Core` 跟資料庫溝通的主要類別，透過繼承 `DbContext` 可以定義跟資料庫溝通的行為。 
> (先建立一個類別繼承 `DbContext`，同時建立`DbSet`。)       

> `DbContext` 是用來對`DB`操作的一個`Class`，主要用來處理`DB`的`CURD`，以及管理DB連線等等。

### 繼承 DbContext

```c#
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Data {
    public class MVCDemoDbContext : DbContext //繼承 DbContext
    {
    }
}
```

### 在 DbContext.cs 新增建構函式

可以使用快速鍵`Ctrl + .`，選擇帶有`options`參數的建構函式方法，以便將`options`傳入基類(父類)。

![](/assets/img/post/mvc-dbcontext-constructor.png)

```c#
//調用父類的構造函數
public MVCDemoDbContext(DbContextOptions options) : base(options) { }
```

```c#
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Data 
{
    public class MVCDemoDbContext : DbContext //繼承 DbContext
    { 
        //調用父類的構造函數
        public MVCDemoDbContext(DbContextOptions options) : base(options) 
        {
        }
    }
}
```

## 建立屬性

使用快速鍵建立屬性：`prop`，然後按兩下`tab`鍵，將屬性設為 `DbSet<Employee>`

```c#
//Employee所有實體的集合
public DbSet<Employee> Employees { get; set; }
```

```c#
using ASPNETMVCCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Data
{
    public class MVCDemoDbContext : DbContext //繼承 DbContext
    {
        //構造函數:調用父類的構造函數
        public MVCDemoDbContext(DbContextOptions options) : base(options) { }

        //Employee所有實體的集合
        public DbSet<Employee> Employees { get; set; }
    }
}
```

> `DbSet<TEntity>` 類別       
> DbSet 代表內容中所有實體的集合，或可從指定型別的資料庫查詢的實體集合。 DbSet 物件是使用 DbCoNtext.Set 方法從 DbCoNtext 建立的。

## 建立 Model

建立一個 Employee Model，為員工`Employee.cs`寫一些屬性。     
(Employee Model 對應資料表Employee中的所有的欄位，寫成屬性)

```c#
namespace ASPNETMVCCRUD.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Salary { get; set; }
        public string? Department { get; set; }   
    }
}
```

## 設定DB連線
### Program.cs 加入DbContext服務(DB連線)

在`builder.Services.AddControllersWithViews();`方法之後，加入`DbContext`服務，型態為`<MVCDemoDbContext>`，它需要一些選項，我想要在`Sql Server`資料庫中使用它作為`DbContext`，所以選項就使用`UseSqlServer`，其連線字串

```c#
//加入DbContext服務:資料庫連線
builder.Services.AddDbContext<MVCDemoDbContext>(options =>  //DbContext型態為MVCDemoDbContext
    options.UseSqlServer(builder.Configuration //使用 Sql Server
    .GetConnectionString("MvcDemoConnectionString"))); //連線字串(appsettings.json設定檔)
```

### appsettings.json 設定連線字串

設定連線字串

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "MvcDemoConnectionString": "Data Source=RIVAWIN10\\MSSQLSERVER_2019;Initial Catalog=MvcDemoDb;User ID=riva;Password=1234;Trust Server Certificate=True"
  }
}
```

> NuGet主控台:兩個指令 快速建立db        
> Add-Migration "Initial Migration"     
> Update-Database       


# Create New Controller - Add Action Method (MVC)

1. 新增 Controller (EmployeesController.cs)
2. 在 EmployeesController 新增 Add 方法 (`HttpGet`方法，導向用戶輸入資料的頁面)
3. 製作 Razor View 表單 (用戶輸入資料的頁面)
4. 建立 View Model (AddEmployeeViewModel.cs)，這些值會來自瀏覽器(將用戶輸入的資料寫成屬性)
5. 在 EmployeesController 新增 Add 方法 (`HttpPost`方法，取得用戶輸入的資料，存入db)


## 用戶輸入 Add 方法 (HttpGet) 
### 新增 Controller

- 新增一個 「MVC Controller - Empty」> EmployeesController.cs
- 新增 Add 方法(`HttpGet`會導向用戶輸入頁面)

```c#
[HttpGet]
public IActionResult Add()
{
    return View();
}
```

### 新增 Razor View 
- 新增檢視 Razor View (Add.cshtml)
點選`return View();`的`View` > 右鍵「新增檢視」> 選擇「Razor View - Empty」     

- Shared 資料夾 > _Layout.cshtml > 加入導航連結link

```html
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Employees" asp-action="Add">Add Employees</a>
</li>
```

![](/assets/img/post/mvc-employees-view-layout.png)


### 取得製作表單的結構

到 [bootstarp 5](https://getbootstrap.com/docs/5.0/getting-started/introduction/) > Forms > Overview > Copy 基本表單的 html     

Go to `Add.cshtml` > 貼上剛複製的 html > 只需要html的結構，所以刪除多餘的html

```html
<!--form方法為post，action為對應controller的Add方法-->
<form method="post" action="Add">
    <div class="mb-3">
        <label for="" class="form-label">Name</label>
        <input type="text" class="form-control">
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
</form>
```

### 建立 View Model

新增一個類別 `AddEmployeeViewModel.cs`，希望這些值來自瀏覽器 (將用戶輸入的值存在View Model)

```c#
namespace ASPNETMVCCRUD.Models
{
    //希望這些值來自瀏覽器
    public class AddEmployeeViewModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Salary { get; set; }
        public string? Department { get; set; }
    }
}
```

### 製作 Razor View 表單

在`Add.cshtml` 加上model `@model` View Model

```html
@model ASPNETMVCCRUD.Models.AddEmployeeViewModel
```

逐步加上對應View Model的屬性 `asp-for`對應`View Model`的屬性

```html
<div class="mb-3">
    <label for="" class="form-label">Name</label>
    <input type="text" class="form-control" asp-for="Name"> <!--Name對應到View Model的屬性-->
</div>
```

```html
@model ASPNETMVCCRUD.Models.AddEmployeeViewModel
@{
}

<!--form方法為post，action為對應controller的Add方法-->
<form method="post" action="Add">
    <div class="mb-3">
        <label for="" class="form-label">Name</label>
        <input type="text" class="form-control" asp-for="Name"> <!--Name對應到View Model的屬性-->
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Email</label>
        <input type="email" class="form-control" asp-for="Email"> <!--Email對應到View Model的屬性-->
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Birthday</label>
        <input type="date" class="form-control" asp-for="DateOfBirth">
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Salary</label>
        <input type="number" class="form-control" asp-for="Salary">
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Department</label>
        <input type="text" class="form-control" asp-for="Department">
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
</form>
```

## 執行
![](/assets/img/post/mvc-add-employee.png)

## 用戶提交 Add 方法 (HttpPost)

### 新增HttpPost 的Add 方法
新增 `HttpPost`的Add 方法，將資料存入db。       
傳入的參數為來自瀏覽器用戶輸入的值 ViewModel (AddEmployeeViewModel)

```c#
//用戶提交
[HttpPost]
public IActionResult Add(AddEmployeeViewModel data) 
{
}
```

### 撰寫HttpPost的Add 方法的邏輯

1. 新增建構函式、建立與db溝通的變數(DbContext)
2. 先將用戶輸入的值轉為Employee實體
3. 儲存到db
4. 重新導向至Add 頁面
5. 改成`async await`非同步寫法


### 新增建構函式、建立與db溝通的變數(DbContext)

```c#
//與資料庫溝通的DbContext
private readonly MVCDemoDbContext db;
public EmployeesController(MVCDemoDbContext dbContext) {
    db = dbContext;
}  
```

### 將用戶輸入的值存入db

```c#
//用戶提交
[HttpPost]
public IActionResult Add(AddEmployeeViewModel data) 
{
    //先將用戶輸入的值轉為employee
    var employee = new Employee {
        Id = Guid.NewGuid(),
        Name = data.Name,
        Email = data.Email,
        DateOfBirth = data.DateOfBirth,
        Salary = data.Salary,
        Department = data.Department
    };

    //儲存到db
    db.Employees.Add(employee); //資料加入到Employees
    db.SaveChanges(); //確認保存到db

    //回到員工列表的頁面
    return RedirectToAction("Index");
}
```

還沒有製作員工列表的頁面，就先回到新增Add頁面

```c#
//回到Add頁面
return RedirectToAction("Add");
```


### 改成 async await 非同步寫法

方法加上`async`關鍵字，並將此方法操作包裝在一個`Task<>`中，結構準備好了           

再將方法改成異步方法`AddAsync`、`SaveChangesAsync`，並在前面加上`await`關鍵字

```c#
//用戶提交
[HttpPost]
public async Task<IActionResult> Add(AddEmployeeViewModel data) 
{
    //...

    //儲存到db
    await db.Employees.AddAsync(employee); //資料加入到Employees
    await db.SaveChangesAsync(); //確認保存到db

    //...
}
```

完整程式碼`EmployeesController.cs`

```c#
public class EmployeesController : Controller
{
    //與資料庫溝通的DbContext
    private readonly MVCDemoDbContext db;
    public EmployeesController(MVCDemoDbContext dbContext) {
        db = dbContext;
    }    

    //用戶輸入
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    //用戶提交
    [HttpPost]
    public async Task<IActionResult> Add(AddEmployeeViewModel data) 
    {
        //先將用戶輸入的值轉為employee
        var employee = new Employee {
            Id = Guid.NewGuid(),
            Name = data.Name,
            Email = data.Email,
            DateOfBirth = data.DateOfBirth,
            Salary = data.Salary,
            Department = data.Department
        };

        //儲存到db
        await db.Employees.AddAsync(employee); //資料加入到Employees
        await db.SaveChangesAsync(); //確認保存到db

        //回到員工列表的頁面
        return RedirectToAction("Index");
    }
}
```


# Create Employee List Action Method

1. 新增顯示所有員工資料的方法`public async Tas<IActionResult> Index() { ... }`
2. 方法名右鍵 > 新增檢視(Razor View) > `Index.cshtml`
3. 製作列表頁面
    - 使用`@model` 導入員工(Employee Model)，因為是列表，所以加上`List<>`
    - 使用table 先將要顯示的結構弄出來
    - 使用 `@foreach` 跑迴圈將資料顯示出來
4. 回到方法，加上 return Veiw 顯示資料 (`return View(employee);`)
5. _Layout.cshtml 加上連結


## Employee List 方法
新增 列表方法，一樣用異步方法，方法加上`async`包裝在一個`Task`中      
裡面也使用異步方法`ToListAsync`，方法的前面加上`await`

```c#
//列表
[HttpGet]
public async Task<IActionResult> Index() {
    await db.Employees.ToListAsync();
    //todo view...
}
```

## 製作 Razor View 列表頁面

### `@model` 使用 Employee Model `List<Employee>`

因為還沒有顯示面頁，點擊`Indext`按右鍵，新增檢視 Razor View     
因為是取得所有員工列表，所以`@model`可以導入 Employees(Model)，     
因為是列表，所以前面加上`List<>`

```html
<!--因為是取得所有員工列表資料，所以可以導入Employee (Model)，因為是列表，所以加上List<>-->
@model List<ASPNETMVCCRUD.Models.Domain.Employee>
```

### @foreach 跑迴圈將資料顯示出來

#### 使用table 先將結構弄出來

```html
<table class="table">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Date Of Birth</th>
            <th>Salary</th>
            <th>Department</th>
        </tr>
    </thead>
    <tbody>
        <!--跑迴圈將資料顯示出來-->
        <!-- TODO -->
    </tbody>
</table>
```

#### 使用 `foreach` 讀取 `Model` 資料

```html
<table class="table">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Date Of Birth</th>
            <th>Salary</th>
            <th>Department</th>
        </tr>
    </thead>
    <tbody>
        <!--跑迴圈將資料顯示出來-->
        @foreach(var employee in Model)
        {
            <tr>
                <td>@employee.Id</td>
                <td>@employee.Name</td>
                <td>@employee.Email</td>
                <td>@employee.DateOfBirth.ToString("yyyy-MM-dd")</td>
                <td>@employee.Salary</td>
                <td>@employee.Department</td>
            </tr>
        }
    </tbody>
</table>
```

### 列表方法顯示View + 導航連結

#### 回到列表方法，將employees放入View中

```c#
//列表
[HttpGet]
public async Task<IActionResult> Index() {
    var employees = await db.Employees.ToListAsync();
    return View(employees);
}
```

#### 導航加上連結 (Shared > _Layout.cshtml)

```html
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Employees" asp-action="Index">Employees</a>
</li>
```

## 執行
![](/assets/img/post/mvc-employee-list.png)

## 完整程式碼 (EmployeeController.cs)
```c#
using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        //與資料庫溝通的DbContext
        private readonly MVCDemoDbContext db;
        public EmployeesController(MVCDemoDbContext dbContext) {
            db = dbContext;
        }

        //列表
        [HttpGet]
        public async Task<IActionResult> Index() {
            //從db取得所有的員工資料
            var employee = await db.Employees.ToListAsync();
            //將員工資料放入view中
            return View(employee);
        }

        //用戶輸入
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //用戶提交
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel data) 
        {
            //先將用戶輸入的值轉為employee
            var employee = new Employee {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Email = data.Email,
                DateOfBirth = data.DateOfBirth,
                Salary = data.Salary,
                Department = data.Department
            };

            //儲存到db
            await db.Employees.AddAsync(employee); //資料加入到Employees
            await db.SaveChangesAsync(); //確認保存到db

            //回到員工列表的頁面
            return RedirectToAction("Index");
        }
    }
}
```

## 完整程式碼 (Index.cshtml)

```html
<!--因為是取得所有員工列表資料，所以可以用Employee Model，因為是列表，所以加上List<>-->
@model List<ASPNETMVCCRUD.Models.Domain.Employee>

@{
}

<h1>Employee List</h1>

<table class="table">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Date Of Birth</th>
            <th>Salary</th>
            <th>Department</th>
        </tr>
    </thead>
    <tbody>
        <!--跑迴圈將資料顯示出來-->
        @foreach(var employee in Model)
        {
            <tr>
                <td>@employee.Id</td>
                <td>@employee.Name</td>
                <td>@employee.Email</td>
                <td>@employee.DateOfBirth.ToString("yyyy-MM-dd")</td>
                <td>@employee.Salary</td>
                <td>@employee.Department</td>
            </tr>
        }
    </tbody>
</table>
```

## 完整程式碼(_Layout.cshtml)

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - ASPNETMVCCRUD</title>
    <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    <link rel="stylesheet" href="~/ASPNETMVCCRUD.styles.css" asp-append-version="true" />
</head>
<body>
    <header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container-fluid">
                <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">ASPNETMVCCRUD</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Employees" asp-action="Index">Employees</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link text-dark" asp-area="" asp-controller="Employees" asp-action="Add">Add Employees</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    <div class="container">
        <main role="main" class="pb-3">
            @RenderBody()
        </main>
    </div>

    <footer class="border-top footer text-muted">
        <div class="container">
            &copy; 2024 - ASPNETMVCCRUD - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
        </div>
    </footer>
    <script src="~/lib/jquery/dist/jquery.min.js"></script>
    <script src="~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/site.js" asp-append-version="true"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```


# Create View/Edit Employee Page

## View Employee Page

### 1. 列表中加上「預覽」按鈕

- 加上View按鈕
- View按鈕的網址路徑會顯示員工ID

想要在列表中有個「預覽」按鈕，可以檢視該員工資料的連結       
Employee Folder > Index.cshtml  

在table中加上一個空的`<th>`, 跑迴圈的地方加上一個`<td>`,        
裡面放入連結<a href="#"></a>，該連結會顯示ID

```html
<table>
    <thead><!--標題-->
        <th></th>
    </thead>
    <tbody><!--內容-->
        <td><a href="Employees/View/@employee.Id">View</a></td>
    </tbody>
</table>
```

![](/assets/img/post/mvc-employee-list-view.png)


```html
<!--因為是取得所有員工列表資料，所以可以用Employee Model導入 Employee，因為是列表，所以加上List<>-->
@model List<ASPNETMVCCRUD.Models.Domain.Employee>
@{
}

<h1>Employee List</h1>
<table class="table">
    <thead>
        <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Date Of Birth</th>
            <th>Salary</th>
            <th>Department</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <!--跑迴圈將資料顯示出來-->
        @foreach(var employee in Model)
        {
            <tr>
                <td>@employee.Id</td>
                <td>@employee.Name</td>
                <td>@employee.Email</td>
                <td>@employee.DateOfBirth.ToString("yyyy-MM-dd")</td>
                <td>@employee.Salary</td>
                <td>@employee.Department</td>
                <td><a href="Employees/View/@employee.Id">View</a></td>
            </tr>
        }
    </tbody>
</table>
```

![](/assets/img/post/mvc-employee-list-view.png)

### 2. EmployeesController 新增 View方法 + 快速新增檢視(View.cshtml)

```c#
//預覽員工資料
[HttpGet]
public IActionResult View(Guid id) {
    return View();
}
```

下中斷點，執行：    
- 確認是否能正確取得到員工id
- 核對員工id正確性，是否和頁面&DB相同 

### 3. 撰寫邏輯(draft)

```c#
//預覽員工資料
[HttpGet]
public async Task<IActionResult> View(Guid id) 
{
    //從db搜尋該員工
    var employee = await db.Employees.FirstOrDefaultAsync(e => e.Id == id);

    //將資料傳入View，如果員工不存在，也交給View處理，這裡不做判斷處理
    return View(employee);
}
```

### 4. 新增一個View Model (`UpdateEmployeeViewModel.cs`)，並在View中取得該Model

轉到View頁面        
我們不應該用Employee Model，而是希望 Model 與 ViewModel 之間有隔離，        
因此我們新增一個不同的View Model    
所以我們新增一個`UpdateEmployeeViewModel.cs`類別   

#### 新增 View Model
`UpdateEmployeeViewModel.cs`

```c#
namespace ASPNETMVCCRUD.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Salary { get; set; }
        public string? Department { get; set; }
    }
}
```

#### 製作檢視頁面

Copy 之前的 Add表單來製作。

Razor View(`View.cshtml`)

```html
@model ASPNETMVCCRUD.Models.UpdateEmployeeViewModel

@{
}

<h1>Edit Employee</h1>

<!--form方法為post，action為對應controller的Viwe方法-->
<form method="post" action="View" class="mt-5">
    <div class="mb-3">
        <label for="" class="form-label">Id</label>
        <input type="text" class="form-control" asp-for="Id" readonly> <!--Id對應到View Model的屬性，並設唯讀-->
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Name</label>
        <input type="text" class="form-control" asp-for="Name"> <!--Name對應到View Model的屬性-->
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Email</label>
        <input type="email" class="form-control" asp-for="Email"> <!--Email對應到View Model的屬性-->
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Birthday</label>
        <input type="date" class="form-control" asp-for="DateOfBirth">
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Salary</label>
        <input type="number" class="form-control" asp-for="Salary">
    </div>
    <div class="mb-3">
        <label for="" class="form-label">Department</label>
        <input type="text" class="form-control" asp-for="Department">
    </div>
    <button type="submit" class="btn btn-primary">Submit</button>
</form>
```

### 5. 撰寫程式邏輯

```c#
//預覽員工資料
[HttpGet]
public async Task<IActionResult> View(Guid id) {
    //從db搜尋該員工
    var employee = await db.Employees.FirstOrDefaultAsync(e => e.Id == id);

    //檢查是否有值，FirstOrDefaultAsync 有可能是null
    if (employee != null) 
    {
        //取得的員工資料，轉為View Model
        var viewModel = new UpdateEmployeeViewModel {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            DateOfBirth = employee.DateOfBirth,
            Salary = employee.Salary,
            Department = employee.Department
        };
        //將資料傳入View
        return await Task.Run(()=> View("View",viewModel));
    }

    //重新導向員工列表頁面
    return RedirectToAction("Index");
}
```

執行

![](/assets/img/post/mvc-employee-view-to-edit.png)

## Edit Employee Page

使用剛剛的形式來更新View Model資料 (員工列表 > View 按鈕 > 預覽頁面(可編輯))

```c#
//更新員工資料
[HttpPost]
public async Task<IActionResult> View(UpdateEmployeeViewModel model)
{
    //搜尋該員工是否存在
    var employee = await db.Employees.FindAsync(model.Id);

    //有該員工
    if (employee != null)
    {
        //更新資料
        employee.Name = model.Name;
        employee.Email = model.Email;
        employee.DateOfBirth = model.DateOfBirth;
        employee.Salary = model.Salary;
        employee.Department = model.Department;

        //確認保存資料
        await db.SaveChangesAsync();

        //返回員工列表頁面
        return RedirectToAction("Index");
    }
    //TODO:導向員工不存在的錯誤頁面
    return RedirectToAction("Error");
}
```


# Delete Employee Controller Action Method and View
## View

在View.cshtml 加上刪除按鈕，樣式改成`danger`紅色警示，再加上`asp-controller=""`

```html
<button type="submit" class="btn btn-danger" 
    asp-action=""
    asp-controller="">Delete</button>
```

完整View.cshtml 
```html

```

## Delete Method
一樣使用view model取得表單資料

```c#
//刪除員工資料
[HttpPost]
public async Task<IActionResult> Delete(UpdateEmployeeViewModel model) 
{
    //搜尋該員工
    var employee = await db.Employees.FindAsync(model.Id);

    //員工存在
    if (employee != null) {
        //刪除該員工
        db.Employees.Remove(employee);
        //確認保存db
        await db.SaveChangesAsync();

        //重新導向員工列表頁面
        return RedirectToAction("Index");
    }
    
    //重新導向員工列表頁面，或是員工不存在錯誤頁面
    return RedirectToAction("Index");
}
```

## 回到View.cshtml
回到View.cshtml 填寫：與controller 溝通 `Employees`，調用`Delete`方法

- action 加上方法名 `Delete`
- controller 加上 `Employees`

```html
<button type="submit" class="btn btn-danger" 
    asp-action="Delete"
    asp-controller="Employees">Delete</button>
```

執行
![](/assets/img/post/mvc-view-to-delete.png)


[ASP.NET Core MVC CRUD - .NET 6 MVC CRUD Operations Using Entity Framework Core and SQL Server](https://www.youtube.com/watch?v=2Cp8Ti_f9Gk)      
[MSDN - Entity Framework Core](https://learn.microsoft.com/zh-tw/ef/core/)      
[MSDN - Entity Framework Core 工具參考](https://learn.microsoft.com/zh-tw/ef/core/cli/)     
[MSDN - 安裝 Entity Framework Core](https://learn.microsoft.com/zh-tw/ef/core/get-started/overview/install)     
[MSDN - 使用 ASP.NET Core 應用程式中的資料](https://learn.microsoft.com/zh-tw/dotnet/architecture/modern-web-apps-azure/work-with-data-in-asp-net-core-apps)        
[MSDN - DbContext 的存留期、設定與初始化](https://learn.microsoft.com/zh-tw/ef/core/dbcontext-configuration/)   

DbSet<TEntity> 類別 <https://learn.microsoft.com/zh-tw/dotnet/api/system.data.entity.dbset-1?view=entity-framework-6.2.0>

