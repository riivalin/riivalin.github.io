---
layout: post
title: "[ADO.NET] 關於資料庫連接的開啟與關閉(使用SqlDataReader和SqlDataAdapter)"
date: 2021-06-08 23:29:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,connection,SqlDataReader,DataAdapter,DataSet,DataTable]
---


## SqlDataAdapter

如果使用SqlDataAdapter來查詢資料傳回DataSet或DataTable時需要注意以下幾點:

1. 如果SqlDataAdapter的SelectCommand的連線並沒有打開，使用SqlDataAdapter的Fill方法時會自動開啟資料庫連 接，並在方法執行完畢自動關閉連線。如果連線在使用Fill方法之前已經打開，方法執行結束後會保持連線的現有狀態，不會關閉連線。

2. 如果你在同一個Connection上有一系列的連續操作，例如執行多個Fill操作，你應該在最開始使用Connection的Open()方法打開連接，避免使用Fill方法時執行額外的打開連接/關閉連接操作，從而提高了程式的效能。

3. 在使用SqlDataAdapter中的SqlCommand物件時，你可以重複的使用同一個SqlCommand去多次執行相同類型的操作，比如說執行多次查詢，但是不要使用同一個SqlCommand去執行不同類型的操作。


### MSDN上的DataAdapter.Fill的說明：

Fill 方法使用相關的 SelectCommand 屬性所指定的 SELECT 陳述式，從資料來源擷取資料列。與 SELECT 陳述式關聯的連接物件必須是有效的，但不需要是開啟的。如果在呼叫 Fill 之前關閉連接，它會先開啟以擷取資料，然後再關閉。如果在呼叫 Fill 之前開啟連接，它會保持開啟。

## SqlDataReader

4. 當SqlDataReader沒有關閉之前，資料庫連線會一直保持open狀態，所以在使用SqlDataReader時，使用完畢應該馬上呼叫SqlDataReader.Close()關閉它。

5. 一個連線只能被一個SqlDataReader使用，這也是為什麼要儘早關閉SqlDataReader的原因。

6. 使用完SqlDataReader後，你可以在程式中顯示的呼叫資料庫連接物件的Close方法關閉連接，也可以在呼叫Command物件的ExecuteReader方法時傳遞CommandBehavior.CloseConnection 這個枚舉變量，這樣在呼叫SqlDataReader的Close方法時會自動關閉資料庫連線。

7. 使用SqlDataReader時盡量使用和資料庫欄位類型相符的方法來取得對應的值，例如對於整形的欄位使用GetInt32,對字元類型的欄位使用GetString。這樣會減少因為型別不一致而額外增加的型別轉換操作。

8. 使用SqlDataReader取得多筆記錄時，如果沒有存取到取出記錄的結尾時想要關閉SqlDataReader,應該先呼叫Command物件的 Cancel方法，然後再呼叫SqlDataReader的Close方法。 Command物件的Cancel方法使得資料庫不再將 SqlDataReader中未存取的資料傳送到呼叫端，如果不呼叫此方法直接關閉SqlDataReader，資料庫會傳送和 SqlDataReader未存取資料等長的空資料流到呼叫端。

9. 如果想透過SqlCommand的ExecuteReader方法取得預存程序的回傳值或輸出參數，必須先呼叫SqlDataReader的Close方法後，才能取得輸出參數的值或回傳值。

10. 如果使用SqlDataReader只傳回一筆記錄，那麼在呼叫Command的ExecuteReader方法時，就指定

CommandBehavior.SingleRow參數，這個參數的是否使用對SQL Server .NET Data Provider沒有什麼影響，但是當你使用OLE DB .NET Data Provider時，指定這個參數後，DataPrivider內部將使用IRow接口，而不是使用相對來說耗費資源的IRowSet介面。    




[MSDN - 從 DataAdapter 填入資料集](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/populating-a-dataset-from-a-dataadapter) 
[MSDN - SqlDataAdapter 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldataadapter?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)        
[MSDN - DataSet 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.dataset?view=net-8.0)   
[CSDN - 关于数据库连接的打开与关闭](https://blog.csdn.net/pitt_xiong/article/details/7170530)