---
layout: post
title: "[C# 筆記] ASP.Net Core Web API 使用 EF Core DB First 存取 DB (CRUD)"
date: 2023-07-09 23:59:00 +0800
categories: [Notes,Web API]
tags: [C#,Web API,.Net Core,CRUD,EF Core,DB First]
---

Take notes...

- `appsettings.json` 加入db連接字串
- `Product.cs` 建立與資料表結構相同的類別(建立對應至資料表結構的資料模型)
- `DbContext.cs` 建立資料存取類別(建立資料庫模型)
- `Program.cs` 註冊db連線字串
- `ProductController.cs` API Controller (CRUD操作)

---

1. 新增WebAPI專案：Add New Project > ASP.Net Core Web API   
2. NuGet安裝套件：NuGet > Microsoft.EntityFrameworkCore.SqlServer
3. 設定db連接字串：在`appsettings.json`設定資料庫連接字串
4. 建立Model：以產品為例`Product.cs` 
5. 建立`DbContext.cs`資料存取類別
> `DbContext` 是`EF Core` 跟資料庫溝通的主要類別，透過繼承 `DbContext` 可以定義跟資料庫溝通的行為。     
> (首先我們先建立一個類別繼承 `DbContext`，同時建立`DbSet`。)
6. 註冊資料庫連接：在`Program.cs`
7. 建立 API Controller，以產品為例`ProductController`
---

# 建立資料庫模型(DbContext、DbSet)
建立一個應用程式層級的資料庫模型，這個資料庫模型會對應到實體資料庫，其中包含資料表、資料欄位設定。      

再往下繼續前，須先了解兩個關鍵類別 `DbContext` 和 `DbSet`。     

首先我們先建立一個類別繼承 `DbContext` ，同時建立`DbSet`。 


## DbContext

應用程式主要透過 `DbContext` 物件與資料庫進行連線溝通，對資料庫進行查詢、新增、修改等動作。     

首先我們會建立自己的資料庫模型 `TestDbContext`，程式碼很簡短，只做兩件事：

1. 繼承 `DbContext` 使之有連線資料庫等能力
2. 在建構式中接收 `options` 資料庫設定，並由父類別，也就是 `DbContext`，產生實體

```c#
//透過繼承 DbContext，可以定義跟資料庫溝通的行為
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { } 
}
```

## DbSet

資料庫裡面最主要的物件就是資料表了，這部分是利用 `DbSet` 物件封裝要處理的資料表，因此每個 `DbSet` 會對應到特定的資料表結構，並包含該資料表的實體資料集合。      

程式碼也短短的，做兩件事：      

1. 建立對應至資料表結構的資料模型，如 `Product`
2. 接續 `TestDbContext`，加入 `DbSet` 並指定其型別為 `Product`

在建立資料模型時，對應的資料型別是我們需要特別注意的，錯誤的型別會造成資料存取失敗，詳細的對照表請參考官方文件。        

另外，資料表欄位有些特性是資料庫中特有的，例如 `Primary Key`、資料長度，這些可以透過 `model Annotations` 來做設定，詳細資料請參考官方文件或查看原始碼。

### Product.cs
建立對應至資料表結構的資料模型，如 `Product`

```c#
public class Product
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public int Price { get; set; }
    public int Qty { get; set; }
}
```

### TestDbContext.cs
接著在 `TestDbContext`，加入 `DbSet` 並指定其型別為 `Product`

> 在建立資料模型時，對應的資料型別是我們需要特別注意的，錯誤的型別會造成資料存取失敗

```c#
//透過繼承 DbContext，可以定義跟資料庫溝通的行為
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { } //DbContext類別名為db名       
    public DbSet<Product> Product { get; set; } //DbSet屬性名為資料表名
}
```

## 小結

使用 `Entity Framework Core` 建立資料庫模型非常容易上手，但在建立的過程中我們希望要符合以下慣例：

1. `DbContext` 類別名稱為資料庫名稱
2. `DbSet` 屬性名稱為資料表名稱
3. 資料模型的屬性名稱為資料表欄位名稱
4. 資料模型中使用 Id 作為主鍵
慣例是可以透過 model Annotations 打破的，使用 model Annotations 來手動指定資料表名稱、欄位名稱等特性。


# 設定連線字串 & 註冊服務
## 設定連線字串 (appsettings.json)
建立完資料庫模型後，要和實體資料庫建立連線，在 `appsettings.json` 中增加資料庫連線字串。

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "model Source=RIVAWIN10\\MSSQLSERVER_2019;Initial Catalog=Test;User ID=riva;Password=1234;Trust Server Certificate=True"
    }
}
```

連線字串不確定怎麼寫，可以從專案設定中取得：     

檢視 > 伺服器總管 > 連線到資料庫 > 加入連接 > 選擇：伺服器名稱、SQL Sever驗証、ID&PW、加密True、勾選信任伺服器憑証、選擇資料庫 > 測試連線       

確定連線成功後，在「進階屬性」 > Copy `model Source`

> 每種資料庫的連線字串都不一樣，可以參考 [The Connection Strings Reference](https://www.connectionstrings.com) 這個網站查詢連線字串的寫法。

## 註冊服務 (Program.cs)
接者在 `Program.cs` 中註冊 `TestDbContext` 服務，並將 `DefaultConnection` 連線字串設定給資料庫模型。

```c#
//註冊服務-設定讀取db連線字串
builder.Services.AddDbContext<TestDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

```c#
using Microsoft.EntityFrameworkCore;
using WebAPIDemo.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//設定讀取db連線字串
builder.Services.AddDbContext<TestDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

# 實作 CRUD 的 API
完成了 `Entity Framework Core` 的主要建置，接下來實作透過 `WebAPI` 來對資料庫進行 `CRUD` 動作。

## Controller 中設定 DbContext

上一步驟中，已將 `TestDbContext` 註冊到我們的應用程式中，因此可以直接在建構式中傳入 `TestDbContext` 服務，進行資料庫讀寫動作。

```c#
namespace WebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly TestDbContext db;
        public ProductController(TestDbContext testDbContext) {
            db = testDbContext;
        }
        //...
    }
}
```

## 讀取 (HttpGet)

### 讀取全部資料
```c#
//取得所有資料
[HttpGet]
public IActionResult Get() 
{
    //使用try-catch檢查是否有未處理的異常
    try
    {
        //取得所有資料
        var product = db.Product.ToList();

        //檢查是否有資料
        if (product == null || product.Count == 0)
        {
            //返回自定義訊息
            return NotFound("無資料可顯示。");
        }

        return Ok(product);
    } catch (Exception ex)
    {
        //返回錯誤訊息
        return BadRequest(ex.Message);
    }
}
```

### 以 id 讀取資料
```c#
[HttpGet("id")]
public IActionResult Get(int id)
{
    try
    {
        //以id搜尋資料
        var product = db.Product.Find(id);

        //如果資料為空，就返回自定義訊息
        if (product == null)
        {
            return NotFound($"無產品ID:{id}的資料。");
        }
        return Ok(product);
    } catch (Exception ex)
    {
        //返回錯誤訊息
        return BadRequest(ex.Message);
    }
}
```

## 新增 (HttpPost)
```c#
[HttpPost]
public IActionResult Post(Product model)
{
    if (string.IsNullOrWhiteSpace(model.ProductName)) {
        return BadRequest("請輸入資料");
    }

    try
    {
        //寫入DB
        db.Add(model);
        db.SaveChanges();
        return Ok("資料已新增。");
    } catch (Exception ex)
    {
        //返回錯誤訊息
        return BadRequest(ex.Message);
    }
}
```

## 修改 (HttpPut)

```c#
[HttpPut]
public IActionResult Put(Product model) 
{
    if (model == null) {
        return BadRequest("請輸入資料");
    }
    if (model.Id == 0) {
        return BadRequest("Id不可為空");
    }

    try
    {
        //搜尋該id的產品資料
        var product = db.Product.Find(model.Id);
        //未找到該id的產品
        if (product == null) {
            return BadRequest($"無產品ID:{model.Id}的資料可修改。");
        }

        //修改資料
        product.ProductName = model.ProductName;
        product.Price = model.Price;
        product.Qty = model.Qty;
        db.SaveChanges();

        return Ok("資料已更新。");

    } catch (Exception ex)
    {
        //返回錯誤訊息
        return BadRequest(ex.Message);
    }
}
```

## 刪除 (HttpDelete)

```c#
[HttpDelete]
public IActionResult Delete(int id) 
{
    if (id == 0) { return BadRequest("Id不可為空"); }
    try
    {
        //搜尋該id的產品資料
        var product = db.Product.Find(id);

        //未找到該id的產品
        if (product == null)
        {
            return BadRequest($"無產品ID:{id}的資料可刪除。");
        }

        //刪除資料
        db.Product.Remove(product);
        db.SaveChanges();

        return Ok("資料已刪除。");

    } catch (Exception ex)
    {
        //返回錯誤訊息
        return BadRequest(ex.Message);
    }
}
```

# Swagger Errors: Failed to load API definition.

## 錯誤訊息：
```
Failed to load API definition.

Errors 
Fetch error
response status is 500 https://localhost:44314/swagger/v1/swagger.json
```

## 錯誤原因：
因為有兩個同名的`Get()`方法。

## 解決方法：

將以id取得資料的方法，`[HttpGet]` 加上id參數 `[HttpGet("id")]`。        

```c#
[HttpGet]
public IActionResult Get() { }

[HttpGet("id")]
public IActionResult Get(int id) { }
```


[Asp.Net Core Web API - CRUD operations in REST API using Entity Framework Core DB first & SQL Server](https://www.youtube.com/watch?v=nSHi9fwrue8)     
[MSDN - DbContext 的存留期、設定與初始化](https://learn.microsoft.com/zh-tw/ef/core/dbcontext-configuration/)       
[MSDN - Tutorial: Get started with EF Core in an ASP.NET MVC web app](https://learn.microsoft.com/en-us/aspnet/core/model/ef-mvc/intro?view=aspnetcore-8.0)       
[EF Core 筆記 1 - 概論](https://blog.darkthread.net/blog/efcore-notes-1/)       
[在 ASP.NET Core WebAPI 使用 Entity Framework Core 存取資料庫](https://github.com/poychang/DemoEFCore)