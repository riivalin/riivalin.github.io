---
layout: post
title: "[ADO.NET] ExecuteNonQuery 的回傳值"
date: 2021-06-01 23:59:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,ExecuteNonQuery]
---


### `ExecuteNonQuery` 方法不會返回任何資料庫的資料，它只會返回整數值來表示成功或受影響的資料列數目.

If use ExecuteNonQuery to create or modify database structure, eg. create table, this method returns -1 if success, returns 0 if fail.

### `ExecuteNonQuery` 用來創建或修改資料庫的結構，如建立table，成功回1，失敗回0。
If use ExecuteNonQuery to INSERT, UPDATE, DELETE, this method returns the Number of affected data row, but if fail, it returns 0.

### 如果用來新增修改刪除，成功它會返回受影響的列數，失敗回0

> 您可以使用 ExecuteNonQuery 來執行目錄作業 (，查詢資料庫的結構或建立資料庫物件，例如資料表) ，或藉由執行 UPDATE、INSERT 或 DELETE 子句來變更資料庫中 DataSet 的資料。  
>           
> - ExecuteNonQuery雖然 不會傳回任何資料列，但對應至參數的任何輸出參數或傳回值會填入資料。    
> - 對 UPDATE、INSERT 和 DELETE 陳述式而言，傳回值是受命令影響的資料列數目。 對其他類型的陳述式而言，傳回值為 -1。        
> - 當插入或更新的資料表上有觸發程式時，傳回值會包含受插入或更新作業影響的資料列數目，以及觸發程式或觸發程式所影響的資料列數目。 
> - 在連接上設定 SET NOCOUNT ON 時， (之前或做為執行命令的一部分，或是由命令執行所起始的觸發程式之一部分，) 個別語句所影響的資料列停止影響此方法所傳回的資料列計數。 如果未偵測到對計數造成貢獻的語句，則傳回值為 -1。 如果發生復原，傳回值也是 -1。



[MSDN - SqlCommand.ExecuteNonQuery 方法](https://learn.microsoft.com/zh-tw/dotnet/api/microsoft.data.sqlclient.sqlcommand.executenonquery?view=sqlclient-dotnet-standard-5.1)       
[[C#] ASP.NET ExecuteNonQuery 的回傳值 ](https://charleslin74.pixnet.net/blog/post/445312541-%5Bc%23%5D-asp.net-executenonquery-的回傳值)