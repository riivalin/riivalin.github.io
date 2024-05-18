---
layout: post
title: "[ADO.NET] Command 物件 (執行SQL命令)"
date: 2021-06-04 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,command,,DataReader,ExecuteReader,ExecuteNonQuery]
---

表示要對 SQL Server 資料庫執行的 Transact-SQL 陳述式或預存程序。        

`Command` 物件是用來對資料來源執行`SQL`命令：選取`Select`、新增`Insert`、修改`Update`、刪除`Delete`。       

`Command` 物件主要透過二種方式來執行`SQL`語法： 

1. `ExecuteReader()`方法：適合`Select`查詢語句，通過Reader取得對應的值
2. `ExecuteNonQuery()`方法：適合新刪修語句，回傳受影響的資料筆數

(`ExecuteScalar`從資料庫取得單一值，通常用在要傳回彙總函式的結果(`conut(*)`,`sum(price)`))

#### `ExecuteNonQuery()` 回傳的結果是什麼？     

- 第一種情況：用於`update`、`insert`、`delete`語句中傳回該受影響的行數
- 第二種情況：用於`select`語句傳回值為`-1`


> 對於 `ExcuteNonQuery`的回傳值，微軟在官方文件中給出了這樣的描述：[MSDN](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)     
> 對於 `UPDATE`、`INSERT` 和 `DELETE` 語句，傳回值為該指令**所影響的行數**。對於所有其他類型的語句，傳回值是 -1。       
> 所以`SELECT`這裡的回傳值是`-1`。


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
        if (dr.HasRows) //HasRows=有資料行
        {
            //使用Read()方法把資料讀進dr (Read會讀取一行的資料)
            while (dr.Read()) //遍歷所有記錄 (使用while讀到不能讀為止)
            {
                //讀取數據，並將其寫入主控台
                Console.WriteLine($"{dr[0]},{dr[1]},{dr[2]}");
            }
        } else
        {
            Console.WriteLine("No rows found.");
        }
    }
}
```

- `HasRows`：表示目前行SqlDataReader是否包含一行或多行，用來判斷是否有讀到資料 (`HasRows`=有資料行)     
- `Read()`：使得目前SqlDataReader前進到下一筆記錄，即向前讀取。(Read會讀取一行的資料)       å
- `While(dr.Reader()) //遍歷所有記錄` (使用while讀到不能讀為止)


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
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立與sql server的連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟資料庫連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備SQL語句(select)
    SqlCommand cmd = new SqlCommand("select * from Employee", conn);

    //執行SQL語句(如果只是想執行SQL，不關心回傳內容的話，可以呼叫以下方法)
    int result = cmd.ExecuteNonQuery(); //回傳值是-1, insert,update,delete是回傳異動的資料筆數
}
```

## ExcuteNonQuery()回傳的結果是什麼？

- 第一種情況：用於`update`、`insert`、`delete`語句中傳回該受影響的行數 (異動的資料筆數)。
- 第二種情況：用於`select`語句傳回值為`-1`。
       

對於 `ExcuteNonQuery`的回傳值，微軟在官方文件中給出了這樣的描述：

對於 `UPDATE`、`INSERT` 和 `DELETE` 語句，傳回值為該指令**所影響的行數**。對於所有其他類型的語句，傳回值是 -1。

所以`SELECT`這裡的回傳值是`-1`。


## ExecuteScalar (取得單一值)

從資料庫取得單一值        
使用 `ExecuteScalar` 物件的 `Command` 方法，從資料庫查詢傳回單一值。      

或許您需要以單一數值傳回資料庫資訊，而非以資料表或資料流的形式。 

- 例如，您或許要傳回彙總函式 (例如 `COUNT(*)`、`SUM(Price)` 或 `AVG(Quantity)`) 的結果。       
- `Command` 物件可讓您以 `ExecuteScalar` 方法傳回單一數值。       
- `ExecuteScalar` 方法會將結果集第一個資料列之第一個資料行的值當做純量值傳回。


## 範例

- 使用`SqlCommand`執行更新資料動作
- 所以會使用`ExecuteNoQuery()`方法(會返回異動筆數)

SQL語句正確寫法是要配合使用參數寫法：`@參數名稱` 加上 `SqlParameter 的方式放入`(不可以偷懶)，避免 SQL Injection 攻擊


```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立與sql server的連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟資料庫連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //SQL語句(正確寫法是要 配合使用參數寫法，避免 SQL Injection 攻擊 --  @參數名稱+SqlParameter 的方式放入)
    string sql = @"update emp set EmpName = 'OOO' where EmpId = 1";

    //告訴SqlCommand要執行的SQL
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        //取得異動的筆數
        int result = cmd.ExecuteNonQuery();

        //result=1 表示異動筆數共有一筆，代表更新成功
        if (result == 1)
        {
            Console.WriteLine("更新成功");
        } else
        {
            Console.WriteLine("更新失敗");
        }
    }
}
```

[MSDN - SqlCommand.ExecuteNonQuery 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)
[MSDN - SqlCommand 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)
[MSDN - SqlDataReader 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldatareader?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)       
[MSDN - 執行命令](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/executing-a-command)    
[MSDN - 從資料庫取得單一值](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/obtaining-a-single-value-from-a-database)   
[[ADO.NET] ExecuteNonQuery 的回傳值](https://riivalin.github.io/posts/2023/07/adonet-executenonquery/)