---
layout: post
title: "[ADO.NET] SqlCommand 對DB增刪改查"
date: 2021-06-13 06:30:00 +0800
categories: [Notes,ADO.NET]
tags: [ADO.NET,connect,command,ExecuteNonQuery,ExecuteReader,ExecuteScalar,SqlParameter,AddWithValue]
---


## SqlCommand 常用方法

- `ExecuteNonQuery`：通常用來執行insert,update,delete，會回傳異動的筆數。(select會回傳-1)
- `ExecuteReader`：執行select，返回一個SqlDataReader物件。唯讀，資料逐筆讀取(只能前進不能後退)。
- `ExecuteScalar`：從資料庫中擷取(單一值)，如T-SQL指令中的 Count()函數。

## 命名空間

NuGet下載對應的套件

```c#
using System.Data;
using Microsoft.Data.SqlClient;
```

## 連線db

連線資料庫使用的類別是`SqlConnection`，它需要一個連線字串，其包含：server是伺服器位址，一個點代表本機，也可以寫ip位址，存取別的機器，database是資料庫名稱，user id是用戶名，password(可以簡寫為pwd)是密碼。我使用這形式比較好記：

```c#
"server=.;database=test;user id=riva;password=1234;";
```

再加上`encrypt=true;trust server certificate=true;`     
(要加上 SQL 連線加密(Encrypt)和信任伺服器憑證(Trust Server Certificate)，不然會出現「無法驗證憑證有效性出現錯誤」。)

了解這兩個元素就可以連接資料庫了：

```c#
//注意，此時還沒有真正連接，我們需要呼叫open()方法，開啟連接
SqlConnection conn = new(connString);
conn.Open();
```

```c#
string connString = "server=.;database=test;user id=riva;password=1234;encrypt=true;trust server certificate=true";
using (SqlConnection conn = new(connString)) 
{
    //注意，此時還沒有真正連接，我們需要呼叫open()方法，開啟連接
    if (conn.State != ConnectionState.Open) conn.Open();
    // Do work here;
} //用 using{} 區塊，會自動關閉連線和自動釋放資源
```

## 執行SQL語法

連線到資料庫後，我們就可以進行下一步執行sql語句了，sql語句的執行需要依賴SqlCommond這個類別。 SqlComand這個類別需要傳入sql語句和連接對象，程式碼如下：

```c#
SqlCommand cmd = new SqlCommand("此處是sql語法", conn)
```

## SQL語句參數的使用

在將具體的增刪改查之前，我們還要了解一個類別叫SqlParameter，一個SqlParameter就是一個鍵值對，它的鍵是sql語句中的變量，值是就是執行sql時的實際的數據，具體聲明如下：

```c#
cmd.Parameters.AddWithValue("@name", "張三");
```
(sql語句中變數以@開頭)

主要是使用”@參數名稱”，來代替原本的變數。

```c#
//原本的sql語句
string sql = @"select * from employee where name = '張三'";

//改成 使用"@參數名稱"，來代替原本的變數
string sql = @"select * from employee where name = @name";

// @name 為參數，"張三"為對應參數的數值
cmd.Parameters.AddWithValue("@name", "張三");
```

## 查詢操作 ExecuteReader()

```c#
string sql = @"select * from employee where name = @name";
using (SqlCommand cmd = new SqlCommand(sql, conn)) 
{ 
    //執行sql時，"@name"實際的數據
    cmd.Parameters.AddWithValue("@name", "張三");

    //使用ExecuteReader執行sql命令，並宣告SqlDataReader物件來取得資料
    SqlDataReader dr = cmd.ExecuteReader();
}
```

練習一下查询，查詢客戶ID是 CONSH 的資料：

```c#
//連線字串
string connString = "server=.;database=Northwind;user id=riva;password=1234;encrypt=true;trust server certificate=true;";

//建立db連線
using (SqlConnection conn = new(connString))
{
    //打開db
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備SQL語句(參數寫法)
    string sql = "select * from Customers where CustomerID = @CustomerID";

    //告訴SqlCommand 要執行的sql 和 要連線的db
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        //設定SQL參數的值
        cmd.Parameters.AddWithValue("@CustomerID", "CONSH");

        //使用ExecuteReader執行sql，並宣告SqlDataReader存放取得的資料
        SqlDataReader dr = cmd.ExecuteReader();

        //檢示是否有資料
        if (dr.HasRows) //HasRows=有資料
        {
            //使用Read()方法將資料讀出來，並顯示在控制台上
            if (dr.Read()) Console.WriteLine($"{dr[0]}, {dr[1]}, {dr[2]}");
        }
    }
}
```

執行結果：

```
CONSH, Consolidated Holdings, Elizabeth Brown
```

## 插入，更新，删除：ExecuteNonQuery()

把這三個放在一塊是因為這三個在程式碼表現層面是一致的，都是呼叫SqlCommand的ExecuteNonQuery()方法，該方法傳回int類型的數據，也就是受影響的行數，

```c#
//連線字串
string connString = "server=.;database=dbtest;user id=riva;password=1234;encrypt=true;trust server certificate=true";

//建立db連線
using (SqlConnection conn = new(connString))
{
    //開啟db連接
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備SQL語句
    string sql = "insert into emp(EmpId,EmpName) values(@EmpId,@EmpName)";

    //宣告SqlCommand，告訴它要執行的sql和 要連接的db
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        //設定sql參數對應的值
        cmd.Parameters.AddWithValue("@EmpId", "E1020");
        cmd.Parameters.AddWithValue("@EmpName", "張三");

        //使用ExecuteNonQuery方法執行sql，成功回傳1，失敗回傳0  (select回傳-1)
        if (cmd.ExecuteNonQuery() == 1) Console.WriteLine("新增成功");
    }
}
```
刪除和更新也是一樣的，只不過是sql語句不一樣


## 執行聚合函數：ExecuteScalar()

SqlCommand類別提供了一個ExecuteScalar()來執行聚合函數，聚合函數的回傳值是不固定的，所以這個方法的回傳值是object，用法也是類似，傳回的這個object值就是查詢的結果，我們可以拆箱為對應的資料類型進行使用。

```c#
string connString = "server=.;database=dbtest;user id=riva;password=1234;encrypt=true;trust server certificate=true";

using (SqlConnection conn = new(connString))
{
    if (conn.State != ConnectionState.Open) conn.Open();

    string sql = "select count(*) from emp";
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        var count = cmd.ExecuteScalar();
        Console.WriteLine(count);
    }
}
```


[MSDN - SqlConnection 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlconnection?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)      
[MSDN - SqlConnection.Close 方法](https://learn.microsoft.com/zh-tw/dotnet/api/microsoft.data.sqlclient.sqlconnection.close?view=sqlclient-dotnet-standard-5.1)        
[sql-連線加密--加密憑證 from [ADO.NET] Connection 物件 (連線到 SQL Server) by R](https://riivalin.github.io/posts/2021/06/adonet-connect/#sql-連線加密--加密憑證)       
[[ADO.NET] SqlParameter 參數的使用 by R](https://riivalin.github.io/posts/2021/06/sqlparameter/)        
[[ADO.NET] Command 物件 (執行SQL命令) by R](https://riivalin.github.io/posts/2021/06/adonet-command/)