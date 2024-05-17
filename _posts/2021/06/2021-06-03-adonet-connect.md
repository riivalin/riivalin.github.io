---
layout: post
title: "[ADO.NET] Connection 物件 (連線到 SQL Server)"
date: 2021-06-03 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,connection,連線字串]
---

`Connection`物件是用來與特定的資料來源建立連接。

## 導​​入相關的資源

因為新建的項目並沒有各個資料庫的連線類別(資料庫驅動)，需手動的安裝。   

- SQL Server資料庫： `Microsoft.Data.SqlClient`
- Mysql資料庫：`MySql.Data`
- Oracle資料庫：`System.Data.OracleClient`

在專案右鍵`NuGet`套件管理       
(需要什麼包就去上面Down就行了)

> [MSDN](https://learn.microsoft.com/zh-tw/azure/azure-sql/database/azure-sql-dotnet-quickstart?view=azuresql&tabs=visual-studio%2Cpasswordless%2Cservice-connector%2Cportal)
> - 請務必安裝 `Microsoft.Data.SqlClient` 而非 System.Data.SqlClient。        
> - `Microsoft.Data.SqlClient` 是較新版本的 SQL 用戶端程式庫，可提供額外的功能。

## NuGet 安裝套件

在專案右鍵`NuGet`套件管理

1. 在 [方案總管] 視窗中，以滑鼠右鍵按一下專案的 [相依性] 節點，然後選取 [管理 `` 套件]。
2. 在產生的視窗中，搜尋 `SqlClient`。 找出 `Microsoft.Data.SqlClient` 結果，然後選取 [安裝]。


## 連線字串

```c#
string connString = "Data Source=192.168.0.1;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;TrustServerCertificate=True;"
```

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=192.168.0.1;Initial Catalog=TestDb;User ID=riva;Password=1234;Encrypt=true;TrustServerCertificate=True;"
}
```

- `Data Source`：資料來源
    - 本機
        - `.` 或 `127.0.0.1`
    - SQL Server Express
        - `.\SQLEXPRESS` (本機\SQLEXPRESS)
        - `Win11\SQLEXPRESS` (電腦名稱\SQLEXPRESS)
    - SQL Server 2019
        - `192.168.0.1` (IP)

SQL 連線加密 & 加密憑證：

- `Encrypt`：加密連線
    - SQL Server Management Studio(SSMS) > 登入頁面 > [選項] > 勾選 [加密連線]
    - 連線字串加上`Encrypt=true;`       

- `Trust Server Certificate`：信任伺服器憑證
    - SQL Server Management Studio(SSMS) > 登入頁面 > [選項] > 勾取  [信任伺服器憑證]
    - 連線字串加上 `TrustServerCertificate=True;` 


SSMS 在登入時有兩個額外選項「`Encrypt connection`」 及「`Trust server certificate`」，勾選後便可使用 TLS 加密連線：

[![](https://blog.darkthread.net/Posts/files/Fig1_637645049129082748.png)](https://blog.darkthread.net/Posts/files/Fig1_637645049129082748.png)     

SQL Server 安裝時已預設支援加密連線，不需額外設定，但預設用的 TLS 憑證是自我簽署憑證(Self-Signed Certificate)，故要勾選 Trust server certificate，否則會因無法驗證憑證有效性出現錯誤：      

[!(https://blog.darkthread.net/Posts/files/Fig2_637645049129757546.png)](https://blog.darkthread.net/Posts/files/Fig2_637645049129757546.png)


## 連線到 SQL Server

如何建立及開啟與 SQL Server 資料庫的連接

```c#
// Assumes connectionString is a valid connection string.  
using (SqlConnection connection = new SqlConnection(connectionString))  
{  
    connection.Open();  
    // Do work here.  
}
```


## 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Trust Server Certificate=True";

using (SqlConnection conn = new SqlConnection(connString))
{
    if (conn.State != ConnectionState.Open) conn.Open();
    Console.WriteLine("db open");
}
```

> 關閉連接：C# 中的 `Using 區塊`也會在程式碼結束該區塊時自動處理連接


## 為什麼連不到MSSQL資料庫(或無法登入)

檢查服務是否有啟動？      

- 開始 > 找到安裝的SQL Server版本 > 展開之後選擇 `Configuration Tools`(設定管理員) > `Sql Server Configuration Manager` > `Sql Server服務` > 確認狀態是`Running`。


[MSDN - 連接字串語法](https://learn.microsoft.com/zh-tw/sql/connect/ado-net/connection-string-syntax?view=sql-server-ver16)     
[MSDN - 連線至資料庫並使用 .NET 和 Microsoft.Data.SqlClient 程式庫查詢 Azure SQL 資料庫](https://learn.microsoft.com/zh-tw/azure/azure-sql/database/azure-sql-dotnet-quickstart?view=azuresql&tabs=visual-studio%2Cpasswordless%2Cservice-connector%2Cportal)     
[MSDN - 建立連線](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/establishing-the-connection)   
[MSDN - SQL Server Express 使用者執行個體](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/sql/sql-server-express-user-instances)
[MSDN - 設定 SQL Server 資料庫引擎來加密連線](https://learn.microsoft.com/zh-tw/sql/database-engine/configure-windows/configure-sql-server-encryption?view=sql-server-2017&ranMID=24542&ranEAID=je6NUbpObpQ&ranSiteID=je6NUbpObpQ-mn8Hq2SM7S9SLSBGAI5MfA&epi=je6NUbpObpQ-mn8Hq2SM7S9SLSBGAI5MfA&irgwc=1&OCID=AIDcmm549zy227_aff_7593_1243925&tduid=(ir__2qoas2sogkkfaxbmc3jolugtu22xdrgwu1bns9za00)(7593)(1243925)(je6NUbpObpQ-mn8Hq2SM7S9SLSBGAI5MfA)()&irclickid=_2qoas2sogkkfaxbmc3jolugtu22xdrgwu1bns9za00)     
[[SQL]為什麼連不到MSSQL資料庫 - 故障排除指南](https://blog.alantsai.net/posts/2017/11/sql-troubleshooting-guide-mssql-connection-problem)     
[啟用 SQL Server SSL 連線加密  by 余小章](https://dotblogs.azurewebsites.net/yc421206/2019/05/23/enable_sql_server_ssl_connection_encrypt)     
[SQL 連線加密觀察及加密憑證檢查  by darkthread](https://blog.darkthread.net/blog/view-sql-encrypt-certificate/)

