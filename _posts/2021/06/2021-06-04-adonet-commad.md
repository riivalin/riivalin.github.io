---
layout: post
title: "[ADO.NET] Command 物件 (執行SQL命令)"
date: 2021-06-04 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,command,ExecuteReader,ExecuteNonQuery]
---

`Command` 物件是用來對資料來源執行`SQL`命令：選取`Select`、新增`Insert`、修改`Update`、刪除`Delete`。       

`Command` 物件主要透過二種方式來執行`SQL`語法： 

1. `ExecuteReader()`方法：適合`Select`查詢語句，通過Reader取得對應的值
2. `ExecuteNonQuery()`方法：適合新刪修語句，回傳受影響的資料筆數


> 對於 `ExcuteNonQuery`的回傳值，微軟在官方文件中給出了這樣的描述：     
> 對於 `UPDATE`、`INSERT` 和 `DELETE` 語句，傳回值為該指令**所影響的行數**。對於所有其他類型的語句，傳回值是 -1。       
> 所以`SELECT`這裡的回傳值是`-1`。


- 從資料庫取得單一值：使用 `ExecuteScalar` 物件的 `Command` 方法，從資料庫查詢傳回單一值。


## 1. ExecuteReader()

與`DataReader`搭配使用，將SQL語法執行結果 傳給`ExecuteReader()`物件。       
此法專注於`Select 命令`「查詢結果」。

### 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;TrustServerCertificate=True";

//建立與sql server的連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟資料庫連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備SQL語句(select)
    SqlCommand cmd = new SqlCommand("select * from Employee", conn);

    //執行SQL語句，並接收execute傳回的資料
    using (SqlDataReader dr = cmd.ExecuteReader())
    {
        //檢查是否有資料
        if (dr.HasRows)
        {
            //使用Read()方法把資料讀進dr
            while (dr.Read()) //遍歷所有記錄
            {
                Console.WriteLine($"{dr[0]},{dr[1]},{dr[2]}");
            }
        } else
        {
            Console.WriteLine("No rows found.");
        }
    }
}
```

- `HasRows`：表示目前行SqlDataReader是否包含一行或多行
- `Read()`：使得目前SqlDataReader前進到下一筆記錄，即向前讀取。      
    - `While(dr.Reader()) //遍歷所有記錄`

執行結果：

```
1,Ken,男
2,Ben,男
3,Tim,男
```


## 2. ExecuteNonQuery()

此法不回傳資料錄。      
但可回傳異動(`Insert`、`Update`、`Delete`)的資料筆數。(`Select`返回`-1`)     
專注於：資料的新增`Insert`、修改`Update`、刪除`Delete`。       


### 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;TrustServerCertificate=True";

//建立與sql server的連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟資料庫連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備SQL語句(select)
    SqlCommand cmd = new SqlCommand("select * from Employee", conn);

    //執行SQL語句(如果只是想執行SQL，不關心回傳內容的話，可以呼叫以下方法)
    int result = cmd.ExecuteNonQuery(); //回傳值是-1
}
```

## ExcuteNonQuery的回傳值

對於 `ExcuteNonQuery`的回傳值，微軟在官方文件中給出了這樣的描述：

對於 `UPDATE`、`INSERT` 和 `DELETE` 語句，傳回值為該指令**所影響的行數**。對於所有其他類型的語句，傳回值是 -1。

所以`SELECT`這裡的回傳值是`-1`。


## ExecuteScalar (取得單一值)

從資料庫取得單一值        
使用 `ExecuteScalar` 物件的 `Command` 方法，從資料庫查詢傳回單一值。      

或許您需要以單一數值傳回資料庫資訊，而非以資料表或資料流的形式。 

- 例如，您或許要傳回彙總函式 (例如 COUNT(*)、SUM(Price) 或 AVG(Quantity)) 的結果。       
- Command 物件可讓您以 ExecuteScalar 方法傳回單一數值。       
- ExecuteScalar 方法會將結果集第一個資料列之第一個資料行的值當做純量值傳回。


[MSDN - SqlDataReader 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldatareader?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)       
[MSDN - 執行命令](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/executing-a-command)    
[MSDN - 從資料庫取得單一值](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/obtaining-a-single-value-from-a-database)   
[[ADO.NET] ExecuteNonQuery 的回傳值](https://riivalin.github.io/posts/2023/07/adonet-executenonquery/)