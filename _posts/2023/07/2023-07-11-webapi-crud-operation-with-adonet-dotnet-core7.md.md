---
layout: post
title: "[C# 筆記] .NET Core 7.0 使用 ADO.NET 實作 CRUD 操作"
date: 2023-07-11 23:59:00 +0800
categories: [Notes,Web API]
tags: [C#,Web API,.Net Core,CRUD,ADO.NET]
---

Take notes...       


- 建立新專案 > ASP.NET Core WebAPI
- NuGet安裝套件 > `Microsoft.Data.SqlClient`
- 設定db連線字串：`appsettings.json`加入db連線字串
- 建立 Model：建立一個Model來取得資料 (對應資料表所有的欄位，寫成屬性)
- 建立 Controller：建立一個API Controller

---

## 設定db連線字串
`appsettings.json`加入db連線字串

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=RIVAWIN10\\MSSQLSERVER_2019;Initial Catalog=CRUDWithWebAPI;User ID=riva;Password=1234;Trust Server Certificate=True"
}
```

## 建立 Model：
建立一個Model來取得資料 (對應資料表所有的欄位，寫成屬性)        

Models 資料夾 > ProductModel.cs 類別

```c#
public class ProductModel
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public int Price { get; set; }
    public int Qty { get; set; }
}
```

## 建立 Controller

建立一個 API Controller

### 宣告IConfiguration類型的變數

`IConfiguration`是用來讀取設定配置的介面。

```c#
//[Route("api/[controller]")] //mark起來，使用自己的Route
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IConfiguration _configuration; //IConfiguration用來讀取設定配置的介面
    public ProductController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
}
```

### 讀取 HttpGet

取得所有資料

```c#
[Route("GetAllProduct")] //使用自己的Route
[HttpGet]
public async Task<IActionResult> GetAllProduct()
{
    List<ProductModel> list = new List<ProductModel>();

    //sql:連接資料庫取得資料
    SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")); //取得連線字串
    if (conn.State != ConnectionState.Open) conn.Open(); //打開通道，建立連線
    SqlCommand cmd = new SqlCommand("select * from Product", conn); //sql語句
    SqlDataAdapter adapter = new SqlDataAdapter(cmd); //配接器

    //取得的資料放入data table
    DataTable dt = new DataTable();
    adapter.Fill(dt);

    //有資料
    if (dt.Rows.Count > 0)
    {
        //資料放入list中
        foreach (DataRow row in dt.Rows)
        {
            ProductModel m = new ProductModel();
            m.Id = Convert.ToInt32(row["Id"]);
            m.ProductName = row["ProductName"].ToString();
            m.Price = Convert.ToInt32(row["Price"]);
            m.Qty = Convert.ToInt32(row["Qty"]);
            list.Add(m);
        }
    }

    return Ok(list);
}
```

### 新增 HttpPost

```c#
[Route("PostProduct")]
[HttpPost]
public async Task<IActionResult> PostProduct(ProductModel obj) 
{
    try
    {
        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            //開啟db
            if (conn.State != ConnectionState.Open) conn.Open();

            //sql語句
            string sql = "insert into product(productName,price,qty) values(@productName,@price,@qty)";

            //定義參數及相關屬性和要傳入的值
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@productName", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@price", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@qty", SqlDbType.Int));
            cmd.Parameters["@productName"].Value = obj.ProductName;
            cmd.Parameters["@price"].Value = obj.Price;
            cmd.Parameters["@qty"].Value = obj.Qty;

            //執行sql語句
            cmd.ExecuteNonQuery(); //如果用來新增修改刪除,成功它會返回受影響的列數,失敗回0.
        }

        return Ok(obj);
    } catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

### 更新 HttpPut

```c#
[Route("UpdateProduct")]
[HttpPut]
public async Task<IActionResult> UpdateProduct(ProductModel obj) {

    using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) 
    {
        //開啟db
        if (conn.State != ConnectionState.Open) conn.Open();

        //sql語句
        string sql = "update Product set ProductName=@ProductName, Price= @Price, Qty=@Qty where Id=@Id";

        //定義參數及相關屬性和要傳入的值
        using (SqlCommand cmd = new SqlCommand(sql, conn)) 
        {
            //command.Parameters.AddWithValue("@Value", "值");
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmd.Parameters.AddWithValue("@Price", obj.Price);
            cmd.Parameters.AddWithValue("@Qty", obj.Qty);

            //執行sql語句(如果用來新增修改刪除,成功它會返回受影響的列數,失敗回0.)
            if (cmd.ExecuteNonQuery()==0) return BadRequest("更新失敗。");
        }
    }

    return Ok(obj);
}
```

### 刪除 HttpDelete

```c#
[Route("DeleteProduct")]
[HttpDelete]
public async Task<IActionResult> DeleteProduct(int id) 
{
    using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
    {
        //開啟db
        if (conn.State != ConnectionState.Open) conn.Open();

        //sql語句
        string sql = "delete Product where Id=@Id";

        //定義參數及相關屬性和要傳入的值
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            //command.Parameters.AddWithValue("@Value", "值");
            cmd.Parameters.AddWithValue("@Id", id);

            //執行sql語句(如果用來新增修改刪除,成功它會返回受影響的列數,失敗回0.)
            if (cmd.ExecuteNonQuery() == 0) return BadRequest("刪除失敗。");
        }
    }

    return Ok("資料已刪除");
}
```


## ExecuteNonQuery的回傳值

- `ExecuteNonQuery` 方法不會返回任何資料庫的資料, 它只會返回整數值來表示成功或受影響的資料列數目.       

If use ExecuteNonQuery to create or modify database structure, eg. create table, this method returns -1 if success, returns 0 if fail.      

- `ExecuteNonQuery` 用來創建或修改資料庫的結構，如建立table,成功回`1`，失敗回`0`。      

If use ExecuteNonQuery to INSERT, UPDATE, DELETE, this method returns the Number of affected data row, but if fail, it returns 0.       

- 如果用來新增修改刪除，成功它會返回受影響的列數，失敗回`0`


> 您可以使用 ExecuteNonQuery 來執行目錄作業 (，查詢資料庫的結構或建立資料庫物件，例如資料表) ，或藉由執行 UPDATE、INSERT 或 DELETE 子句來變更資料庫中 DataSet 的資料。      
> ExecuteNonQuery雖然 不會傳回任何資料列，但對應至參數的任何輸出參數或傳回值會填入資料。        
> 對 UPDATE、INSERT 和 DELETE 陳述式而言，傳回值是受命令影響的資料列數目。 對其他類型的陳述式而言，傳回值為 -1。        
> 當插入或更新的資料表上有觸發程式時，傳回值會包含受插入或更新作業影響的資料列數目，以及觸發程式或觸發程式所影響的資料列數目。 
> 在連接上設定 SET NOCOUNT ON 時， (之前或做為執行命令的一部分，或是由命令執行所起始的觸發程式之一部分，) 個別語句所影響的資料列停止影響此方法所傳回的資料列計數。 如果未偵測到對計數造成貢獻的語句，則傳回值為 -1。 如果發生復原，傳回值也是 -1。



[Implement CRUD Operation with ADO .NET with ASP .NET Core 7.0](https://www.youtube.com/watch?v=0O_nitsEJW0)        
[ASP.NET Core 的設定](https://learn.microsoft.com/zh-tw/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0)        
[MSDN - SqlConnection 類別: 當程式碼結束 using 區塊時，會自動關閉連線](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-8.0)       
[MSDN - SqlCommand.ExecuteNonQuery 方法](https://learn.microsoft.com/zh-tw/dotnet/api/microsoft.data.sqlclient.sqlcommand.executenonquery?view=sqlclient-dotnet-standard-5.1)       
[[C#] ASP.NET ExecuteNonQuery 的回傳值 ](https://charleslin74.pixnet.net/blog/post/445312541-%5Bc%23%5D-asp.net-executenonquery-的回傳值)
