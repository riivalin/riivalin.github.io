---
layout: post
title: "[ADO.NET] SqlDataAdapter的使用"
date: 2021-06-09 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,DataAdapter,DataSet,DataTable]
---


## SqlDataAdapter概述

　　SqlDataAdapter是 DataSet和 SQL Server之間的橋接器，用於擷取和保存資料。 SqlDataAdapter透過對資料來源使用適當的Transact-SQL語句來映射Fill(它可更改DataSet中的資料以符合資料來源中的資料)和Update(它可變更資料來源中的資料以符合DataSet中的資料)來提供這一橋接。當SqlDataAdapter填入 DataSet時，它為傳回的資料建立必要的表和列(如果這些表和列尚不存在)。

我們可以透過以下三種方法來建立SqlDataAdapter物件：

## 使用方法

### 1、透過連接字串和查詢語句

```c#
string connString ="uid=帳號;pwd=密碼;database=資料庫;server=伺服器";//SQL Server連結字串
string sql = "SELECT * FROM 表名";

SqlDataAdapter da = new SqlDataAdapter(sql,connString);
DataSet ds = new DataSet();//建立DataSet實體
da.Fill(ds,"自訂虛擬表名");//使用DataAdapter的Fill方法(填充)，呼叫SELECT指令
```

這種方法有一個潛在的缺陷。假設應用程式中需要多個SqlDataAdapter對象，用這種方式來建立的話，會導致創建每個SqlDataAdapter時，都同時建立一個新的SqlConnection對象，方法二可以解決這個問題

### 2.透過查詢語句和SqlConnection物件來創建

```c#
string connString = "uid=帳號;pwd=密碼;database=資料庫;server=伺服器";//SQL Server連結字串
SqlConnection conn = new SqlConnection(connString);

string sql = "SELECT * FROM 表名";

SqlDataAdapter da = new SqlDataAdapter(sql, conn);
DataSet ds = new DataSet();//建立DataSet實體
da.Fill(ds,"自訂虛擬表名");//使用DataAdapter的Fill方法(填充)，呼叫SELECT指令
```

### 3.透過SqlCommand物件來創建

```c#
string connStrng = "uid=帳號;pwd=密碼;database=資料庫;server=伺服器";//SQL Server連結字串

SqlConnection conn = new SqlConnection (connStrng); //Sql連結類別的實體化
conn.Open ();//開啟資料庫

//使用SqlDataAdapter時沒有必要從Connection.open()打開，
//SqlDataAdapter會自動開啟關閉它。

string sql = "SELECT * FROM 表名"; //要執行的SQL語句
SqlCommand cmd = new SqlCommand(sql,conn);

SqlDataAdapter da = new SqlDataAdapter(cmd); //建立DataAdapter資料配接器實體
DataSet ds = new DataSet();//建立DataSet實體
da.Fill(ds,"自訂虛擬表名");//使用DataAdapter的Fill方法(填充)，呼叫SELECT指令

ConnSql.Close ();//關閉資料庫
```

## 注意

　　如果只需要執行SQL語句或SP，就沒必要用到DataAdapter ，直接用SqlCommand的Execute系列方法就可以了。 sqlDataadapter的作用是實作Dataset和DB之間的橋樑：例如將對DataSet的修改更新到資料庫。

SqlDataAdapter的UpdateCommand的執行機制是：當呼叫SqlDataAdapter.Update()時，檢查DataSet中的所有行，然後對每一個修改過的Row執行SqlDataAdapter.UpdateCommand ，也就是說如果未修改DataSet中的數據，SqlDataAdapter. UpdateCommand不會執行。

## 使用要點
### 1.注意開啟和關閉連線的處理

　　在呼叫SqlCommand物件執行sql指令之前，需要確保與該物件關聯的SqlConnection物件時開啟的，否則SqlCommand的方法執行時將引發一個異常，但是我們在上面的程式碼中看到，SqlDataAdapter沒有這樣的要求。

　　如果呼叫SqlDataAdapter的Fill方法，且其SelectCommand屬性的SqlConnection是關閉狀態，則SqlDataAdapter會自動開啟它，然後提交查詢，取得結果，最後關閉連線。如果在呼叫Fill方法前，SqlConnection是開啟的，則查詢執行完畢後，SqlConnection也會是開啟的，也就是說SqlDataAdapter會保證SqlConnection的狀態恢復到原來的情形。
            
#### 這有時會導致效能問題，需要注意，例如下面的程式碼：

```c#
string connString = "uid=帳號;pwd=密碼;database=資料庫;server=伺服器";//SQL Server連結字串

SqlConnection conn = new SqlConnection(conn);
SqlDataAdapter daCustomers,daOrders;

sql = "SELECT * FROM Customers";
daCustomers = new SqlDataAdapter(sql, conn);

sql = "SELECT * FROM Orders";
daOrders = new SqlDataAdapter(sql, conn);

DataSet ds = new DataSet();
daCustomers.Fill(ds,"Customers"); //呼叫Fill方法 連線被開啟和關閉
daOrders.Fill(ds,"Orders");//呼叫Fill方法, 連線再一次的 被開啟和關閉
```

以上程式碼會導致連線被開啟和關閉兩次，在呼叫Fill方法時各一次。為了避免開啟和關閉SqlConnection對象，在呼叫SqlDataAdapter物件的Fill方法之前，我們可以先開啟SqlConnection對象，如果希望之後關閉連接，我們可以再呼叫Close方法，就像這樣：

```c#
conn.Open(); //連線先打開

daCustomers.Fill(ds,"Customers");
daOrders.Fill(ds,"Orders");

conn.Close(); //關閉連線
```

### 2.多次呼叫Fill方法需要注意資料重複和有效更新資料的問題

```c#
string connString = "uid=帳號;pwd=密碼;database=資料庫;server=伺服器";//SQL Server連結字串
SqlConnection conn = new SqlConnection(conn);

sql = "SELECT * FROM Customers";
SqlDataAdapter da = new SqlDataAdapter(sql, conn);
DataSet ds = new DataSet();

da.Fill(ds,"Customers");
//...
da.Fill(ds,"Customers");
```

我們分析上面的程式碼，透過兩次呼叫Fill方法，SqlDataAdapter執行兩次查詢，並兩次將查詢結果儲存到DataSet中，第一次呼叫在DataSet中建立了一個名為Customers的新表。第二次呼叫Fill方法將查詢的結果追加到DataSet中的同一個表中，因此，每個客戶的資訊將在DataSet中出現兩次!當然，如果資料庫管理員對Customers表定義了主鍵，則SqlDataAdapter在填成DataTable時，會判斷重複行，並自動丟棄掉舊的值。

思考一下，假定一個特定客戶在第一次呼叫Fill方法時，儲存於資料庫中，那麼SqlDataAdapter會將其加入到新建的DataTable中。如果後來這個客戶被刪除了，那麼第二次呼叫Fill方法時，SqlDataAdapter將不會在查詢結果中找到該客戶信息，但是它也不會將客戶資訊從DataSet中刪除。這就導致了數據更新的問題。

所以建議的做法是，在呼叫Fill方法前，先刪除本地DataSet中快取的資料!



[MSDN - 從 DataAdapter 填入資料集](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/populating-a-dataset-from-a-dataadapter)      
[MSDN - SqlDataAdapter 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldataadapter?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)        
[MSDN - DataSet 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.dataset?view=net-8.0)        
[C#中Sql DataAdapter的使用](https://www.cnblogs.com/winformasp/articles/12028135.html)