---
layout: post
title: "[C# 筆記] .NET Core 使用 ADO.NET 預存程序(Stored Procedure) 實作 CRUD"
date: 2023-07-12 23:59:00 +0800
categories: [Notes,Web API]
tags: [C#,Web API,.Net Core,CRUD,ADO.NET,sp]
---

Take notes...       

## 前置作業-準備sp
新刪除查 同一個SP

```sql
create proc uspProduct
	@Action int,
	@Id int,
	@ProductName nvarchar(50),
	@Price int,
	@Qty int	
as
begin
	set nocount on;
	if(@Action = 1) --新增
	begin
		insert into Product(ProductName,Price,Qty) values(@ProductName,@Price,@Qty)
	end
	else if(@Action = 2) --修改
	begin
		update Product set ProductName=@ProductName, Price=@Price, Qty=@Qty where Id=@Id
	end
	else if(@Action = 3) --刪除
	begin
		delete from Product where Id=@Id
	end
	else if(@Action = 4) --查詢
	begin
		select * from Product order by Id desc
	end
end
go
```


## API- 新刪除查 同一個方法

```c#
[Route("Product")]
[HttpPost]
public async Task<IActionResult> Product(ProductModel obj) 
{
    SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt = new DataTable();

    try
    {
        //開啟db連線
        if (conn.State != ConnectionState.Open) conn.Open();

        //定義參數及相關屬性和要傳入的值
        cmd = new SqlCommand("uspProduct", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Action", obj.Action);
        cmd.Parameters.AddWithValue("@Id", obj.Id);
        cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
        cmd.Parameters.AddWithValue("@Price", obj.Price);
        cmd.Parameters.AddWithValue("@Qty", obj.Qty);

        da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        //action=4為查詢所有資料
        if (obj.Action == 4)
        {
            //沒資料
            if (dt.Rows.Count <= 0) return NotFound("無資料可顯示");

            //有資料資料放入list中
            var list = new List<ProductModel>();
            foreach (DataRow row in dt.Rows)
            {
                ProductModel m = new ProductModel();
                m.Id = Convert.ToInt32(row["Id"]);
                m.ProductName = row["ProductName"].ToString();
                m.Price = Convert.ToInt32(row["Price"]);
                m.Qty = Convert.ToInt32(row["Qty"]);
                list.Add(m);
            }
            return Ok(list);
        }
        //執行sql語句(如果用來新增修改刪除,成功它會返回受影響的列數,失敗回0.)
        if (cmd.ExecuteNonQuery() == 0) return BadRequest("操作失敗");

        return Ok(obj);
    } catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

## Code
### ProductModel.cs

```c#
public class ProductModel
{
    public int Action { get; set; }
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public int Price { get; set; }
    public int Qty { get; set; }
}
```

### ProductController.cs

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

                ////執行sql語句
                //cmd.ExecuteNonQuery();


                //執行sql語句 ((如果用來新增修改刪除,成功它會返回受影響的列數,失敗回0.))
                if (cmd.ExecuteNonQuery() == 0) return BadRequest("新增失敗");
            }

            return Ok(obj);
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

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

    //使用SP
    [Route("Product")]
    [HttpPost]
    public async Task<IActionResult> Product(ProductModel obj) 
    {
        SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt = new DataTable();

        try
        {
            //開啟db連線
            if (conn.State != ConnectionState.Open) conn.Open();

            //定義參數及相關屬性和要傳入的值
            cmd = new SqlCommand("uspProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", obj.Action);
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmd.Parameters.AddWithValue("@Price", obj.Price);
            cmd.Parameters.AddWithValue("@Qty", obj.Qty);

            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            //action=4為查詢所有資料
            if (obj.Action == 4)
            {
                //沒資料
                if (dt.Rows.Count <= 0) return NotFound("無資料可顯示");

                //有資料資料放入list中
                var list = new List<ProductModel>();
                foreach (DataRow row in dt.Rows)
                {
                    ProductModel m = new ProductModel();
                    m.Id = Convert.ToInt32(row["Id"]);
                    m.ProductName = row["ProductName"].ToString();
                    m.Price = Convert.ToInt32(row["Price"]);
                    m.Qty = Convert.ToInt32(row["Qty"]);
                    list.Add(m);
                }
                return Ok(list);
            }
            //執行sql語句(如果用來新增修改刪除,成功它會返回受影響的列數,失敗回0.)
            if (cmd.ExecuteNonQuery() == 0) return BadRequest("操作失敗");

            return Ok(obj);
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}


```

---
## 前置準備sp

新增資料

```sql
create proc uspInsertProduct
	@ProductName nvarchar(50),
	@Price int,
	@Qty int	
as
begin
	set nocount on;
	insert into Product(ProductName,Price,Qty) values(@ProductName,@Price,@Qty)
end
go
```

測試sp

```sql
exec uspInsertProduct 'test123',100,999 --執行sp
select * from product --查看資料
```




[CRUD Operation with Store Procedure with ADO .NET with ASP .NET Core 7.0](https://www.youtube.com/watch?v=489nQ98iUFI)     
[[C# 筆記] .NET Core 7.0 使用 ADO.NET 實作 CRUD 操作](https://riivalin.github.io/posts/2023/07/webapi-crud-operation-with-adonet-dotnet-core7.md/)      



