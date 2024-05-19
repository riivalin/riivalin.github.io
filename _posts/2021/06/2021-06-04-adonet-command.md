---
layout: post
title: "[ADO.NET] Command 物件 (執行SQL命令)"
date: 2021-06-05 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,command,DataReader,ExecuteReader,ExecuteNonQuery]
---

SqlCommand
- ExecuteNonQuery：通常用來執行insert,update,delete，會回傳異動的筆數。(select會回傳-1)
- ExecuteReader：執行select，返回一個SqlDataReader物件。唯讀，資料逐筆讀取(只能前進不能後退)。
- ExecuteScalar：從資料庫中擷取(單一值)，如T-SQL指令中的 Count()函數。

SqlParameter
- SqlCommand 中加入參數


## SqlCommand 的用途 

SqlCommand 負責與 Connection 溝通，有兩個用途：
1. 執行 SQL 語法(CommandText) (Select、Insert、Update、Delete)   
2. 預存程序（store procedure）

可以將 SQL 語法定義在 `SqlCommand(字串,conn)`的字串中，而字串後面接的 `conn` 就是指定要使用這個連線來執行 SQL 語法，連線是一定要指定的，否則電腦會不知要使用哪個連線。      

如果有參數的話則要加入 `Parameters` 設定參數。   

在做資料資料存取時，會使用到 SqlCommand 這個類別，當中與它互相搭配的其他方法，      

像是SqlDataReader、ExecuteReader()、ExecuteNonQuery()等，各自代表著不同意思。   

### 重要屬性

- Connection：SqlCommand物件使用的SqlConnection
- CommandText：設定要執行的T-SQL語句 或 預存程序名      
- CommandType：列舉值
   - CommandType.Text：執行的是一個SQL語句     
   - CommandType.StoredProcedure：執行的是一個預存程序     
- Parameters：SqlCommand物件的命令參數集合。預設為空集合。
- Transaction：取得或設定要在其中執行的交易。


## 執行命令的方法

在 SqlCommand 類別中常使用的執行命令方法有下列幾種：

- `ExecuteNonQuery()`：執行SQL語句，並返回受影響的行數(select返回-1)，通常用在insert,update,delete。執行時conn必須是open的。
- `ExecuteReader()`：執行查詢語句，返回一個SqlDataReader物件。資料逐筆讀取(只能前進不能後退)，唯讀。
- `ExecuteScalar()`：從資料庫中擷取(單一值)，如T-SQL指令中的 `Count()`函數。


## ExecuteNonQuery

- 執行T-SQL語句，並傳回受影響的行數。
- 命令類型：插入、更新、刪除。 DML。
- 執行條件：連線物件（conn）必須是Open狀態

### 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立DB連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟db連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備sql語句
    string sql = "update Emp set EmpName = 'OOO' where EmpId = 1";

    //告訴SqlCommand要 執行的sql 和 連線的db
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        // 回傳1表示 異動筆數 共有一筆，代表更新成功 (會更新多筆，就要用 >0 來判斷)
        if (cmd.ExecuteNonQuery() == 1) //select會回傳-1
        {
            Console.WriteLine("更新成功");
        } else
        {
            Console.WriteLine("更新失敗"); //失敗會回傳0
        }
    }
}
```

## ExecuteReader

- 執行查詢語句，傳回一個物件（SqlDataReader）。唯讀。
- SqlDataReader：即時讀取，讀取方式固定，不靈活（只進不出，只能前進，不能後退）。
- 適用：唯讀，不做資料修改的情況；資料量小。(適合單一資料表)
- Read 的每次調用都會從結果集中返回一行。(Read只會讀取一行的資料)

只提供唯獨且順向的資料，就是只會一行一行一直往下讀，沒有辦法往上回溯

只要執行Read()方法，每讀完一筆資料就會把指標指向下一筆      

#### 注意：使用ExecuteReader讀取資料的時候，要即時保存，因為會讀一條，丟一條


### 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立db連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟db連線
    if (conn.State != ConnectionState.Open) conn.Open(); //如果db是關閉 就打開

    //準備SQL語句
    string sql = "select top 3 EmployeeID, EmployeeName, EmployeeBirth from Employee";

    //宣告SqlCommand，並告訴SqlCommand要執行的SQL語句 和連到哪個DB
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        //執行SQL語句，並宣告一個SqlDataReader，用來接收ExecuteReader所傳回的資料
        SqlDataReader dr = cmd.ExecuteReader();

        //檢查是否有讀到資料
        if (dr.HasRows) //HasRows=有資料
        {
            //叫用Read方法讀取資料(Read 只會讀取一行的資料)
            while (dr.Read()) //使用while讀到不能讀為止
            {
                //讀取數據，並將其寫入主控台
                Console.WriteLine($"{dr[0]}, {dr[1]}, {Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd")}");
            }
        }
    }
}
```

執行結果：

```
1, 張三, 1999-09-09
2, 李四, 1989-12-09
3, 王五, 1979-08-09
```

## ExecuteScalar

- 執行查詢語句或預存程序，傳回查詢結果集中的第一行第一列的值，忽略其他行或列。
- 傳回值為Object類型。
- 適用：作查詢，並只傳回一個值作為結果。如：統計資料量conut(*),sum(price)；插入資料後，想傳回自動產生的標識的列的值。
- 命令類型：查詢類型。 DQL（資料查詢語句）
- 返回結果集為：第一列的第一行。

### 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立db連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟db連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備SQL語句 用count(*)統計數量
    string sql = "select count(*) from Employee"; //20筆資料

    //宣告SqlCommand，並告訴SqlCommand要執行的SQL語句 和連到哪個DB
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        //用ExecuteScalar回傳 統計的資料量conut(*)
        var result = cmd.ExecuteScalar();

        //將結果輸出主控台
        Console.WriteLine(result); //20個Employee
    }
}
```


[MSDN - SqlCommand.CommandText 屬性](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand.commandtext?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)        
[MSDN - SqlCommand.ExecuteNonQuery 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)    
[MSDN - SqlCommand 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)        
[MSDN - SqlDataReader 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldatareader?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)       
[MSDN - 執行命令](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/executing-a-command)       
[MSDN - 從資料庫取得單一值](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/obtaining-a-single-value-from-a-database)  
[CSDN - Ado.Net学习——基础知识记录](https://blog.csdn.net/SQWH_SSGS/article/details/109303103)   
[MVCNF03-ADO.NET  by JohnsonNote](https://hackmd.io/@johnsonnote/webdesign/https%3A%2F%2Fhackmd.io%2F%40johnsonnote%2Fadonet)  
[[ADO.NET] Command 物件 -- Draft  by R](https://riivalin.github.io/posts/2021/06/adonet-commad-draft/)      
[[ADO.NET] Connection 物件 (連線到 SQL Server)  by R](https://riivalin.github.io/posts/2021/06/adonet-connect/)
