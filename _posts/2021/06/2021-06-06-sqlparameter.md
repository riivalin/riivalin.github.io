---
layout: post
title: "[ADO.NET] SqlParameter 參數的使用"
date: 2021-06-06 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,command,SqlParameter,AddWithValue]
---

SqlParameter可以防止sql注入問題
表示SqlCommand物件的參數，或與DataSet中列的對應。

> SQL語句正確寫法是要 配合使用參數寫法，避免 `SQL Injection`攻擊 (@參數名稱 + SqlParameter的方式放入)


常用屬性：
- DbType：表示參數的資料類型（資料庫中的類型）
- Direction：參數的類型（輸入、輸出、輸入輸出、回傳值參數）
- ParameterName：參數的名稱
- Size：參數最大大小，以位元組為單位
- Value：參數值
- SqlValue：作為Sql類型的參數的值

---

## 為何使用 SQLParameter 物件？

1. 在插入資料的SQL陳述句，若能使用 Parameter 將會提高安全性，若使用者輸入了特別符號，也比較不會出問題；     
2. Parameter 可以 (1)檢查參數的型別 (2)檢查資料長度 (3)確保參數為非可執行的SQL命令 


## 使用 SQLParameter 物件

使用具名參數，例如：

```sql
SELECT * FROM Customers WHERE CustomerID = @CustomerID -- @CustomerID 具名參數
```

主要是使用"@參數名稱"，來代替原本的變數。

```c#
//原本的sql語句
string sql = @"update emp set EmpName = '張三' where EmpId = 1";

//改成 使用"@參數名稱"，來代替原本的變數
string sql = @"update emp set EmpName = @EmpName where EmpId = @EmpId";

// @EmpName 為參數，empName 為對應參數的數值
cmd.Parameters.AddWithValue("@EmpName", empName);
```

### 寫法一：Parameters.AddWithValue

```c#
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.AddWithValue("@id", id);
```

### 寫法二：Parameters.Add

```c#
cmd.Parameters.Add("@id", SqlDbType.NVarChar); 
cmd.Parameters["@id"].Value = id;
```

### 寫法三：Parameters.Add 方法加入 SQLParameters 類別

```c#
cmd.Parameters.Add(new SqlParameter("@id", id));
```

### 寫法四：Parameters.AddRange

```c#
 //定義參數和要傳入的值
 SqlParameter[] parameters = { //參數陣列
     new SqlParameter("@EmpId", 1),
     new SqlParameter("@EmpName","張三")
 };
 cmd.Parameters.AddRange(parameters);
```


## 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立與sql server的連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟資料庫連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //SQL語句(正確寫法是要 配合使用參數寫法，避免 SQL Injection 攻擊 --  @參數名稱+SqlParameter 的方式放入)
    string sql = @"update emp set EmpName = @EmpName where EmpId = @EmpId";

    //告訴SqlCommand要執行的SQL
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        //使用SqlParameter參數

        //寫法1：Add
        cmd.Parameters.Add("@EmpId", SqlDbType.Int);
        cmd.Parameters.Add("@EmpName", SqlDbType.NVarChar);
        cmd.Parameters["@EmpId"].Value = 1;
        cmd.Parameters["@EmpName"].Value = "張三";

        //寫法2：AddWithValue
        //定義參數和要傳入的值
        cmd.Parameters.AddWithValue("@EmpId", 1);
        cmd.Parameters.AddWithValue("@EmpName", "張三");

        //寫法3：Add方法裡加SqlParameter類別
        cmd.Parameters.Add(new SqlParameter("@EmpId", 1));
        cmd.Parameters.Add(new SqlParameter("@EmpName", "張三"));

        //寫法4：AddRange
        //定義參數和要傳入的值
        SqlParameter[] parameters = { //參數陣列
            new SqlParameter("@EmpId", 1),
            new SqlParameter("@EmpName","張三")
        };
        cmd.Parameters.AddRange(parameters);


        // 1代表 異動筆數共有一筆，代表更新成功
        if (cmd.ExecuteNonQuery() == 1)
        {
            Console.WriteLine("更新成功");
        } else
        {
            Console.WriteLine("更新失敗");
        }
    }
}
```


[MSDN - SqlParameterCollection.AddWithValue(String, Object) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlparametercollection.addwithvalue?view=netframework-4.8.1&redirectedfrom=MSDN)       
[MSDN - SqlParameter 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlparameter?view=netframework-4.8.1&redirectedfrom=MSDN)    
[SqlCommand.CommandType 屬性](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand.commandtype?view=netframework-4.8.1)         
[MSDN - SqlCommand.CommandText 屬性](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand.commandtext?view=netframework-4.8.1)        
[CSDN - Ado.Net学习——基础知识记录](https://blog.csdn.net/SQWH_SSGS/article/details/109303103)       
[[ADO.NET] 為何 / 如何 使用 SQLParameter 物件 by 余小章](https://dotblogs.com.tw/yc421206/2009/06/14/8819)  
[[ADO.NET] Command 物件 -- Draft  by R](https://riivalin.github.io/posts/2021/06/adonet-commad-draft/)    
[[ADO.NET] Command 物件 (執行SQL命令) by R](https://riivalin.github.io/posts/2021/06/adonet-command/)
[[C# 筆記] .NET Core 使用 ADO.NET 預存程序(Stored Procedure) 實作 CRUD  by R](https://riivalin.github.io/posts/2023/07/webapi-crud-operation-with-sp-with-adonet-netcore7/)     
[[C# 筆記] .NET Core 7.0 使用 ADO.NET 實作 CRUD 操作 by R](https://riivalin.github.io/posts/2023/07/webapi-crud-operation-with-adonet-dotnet-core7.md/)     