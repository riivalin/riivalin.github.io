---
layout: post
title: "[ADO.NET] DataReader 和 DataAdapter 的區別"
date: 2021-06-10 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,command,DataAdapter,DataReader]
---

1. DataReader 和 DataAdapter 區別
2. SqlDataAdapter 和 SqlCommand 區別
3. SqlDataAdapter用法

[MSDN：DataAdapter 和 DataReader](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/dataadapters-and-datareaders?redirectedfrom=MSDN)     

您可以使用 ADO.NET DataReader，從資料庫擷取順向唯讀資料流。 執行查詢時會傳回結果，並一直儲存於用戶端上的網路緩衝區中，直到您使用 DataReader 的 Read 方法對其加以要求為止。 使用 DataReader 可以提高應用程式的效能，方法是立即擷取可用的資料，及 (依預設) 一次只將一個資料列儲存到記憶體中，進而減少系統負荷。       

DataAdapter 可用於從資料來源擷取資料，並填入 DataSet 內的資料表。 DataAdapter 亦可將對 DataSet 所做的變更解析回資料來源。 DataAdapter 會使用 .NET Framework 資料提供者的 Connection 物件連接到資料來源，並使用 Command 物件從資料來源擷取資料，以及將變更解析回資料來源。

note: 
- DataReader：順向、唯讀、連線資料存取(連線操作)、手動conn.open/close
- DataAdapter：離線操作、fill方法會自動conn.open/close


## 1. DataReader 和 DataAdapter 區別

SqlDataReader是一個向前的指針，本身不包含數據，調用一次 Read() 方法它就向前到下一條記錄，**一個SqlDataReader必須單獨佔用一個打開的資料庫連接**。

在使用 SqlDataReader 時，關聯的 SqlConnection 正忙於為 SqlDataReader 服務，對 SqlConnection 無法執行任何其他操作。**除非呼叫 SqlDataReader 的 Close 方法，否則會一直處於此狀態**。

SqlDataAdapter像一座橋樑，一頭連起資料庫表，一頭連起一個DataSet 或者DataTable ，在把資料庫中的資料填入DataSet 或DataTable 後就可以「過河拆橋」，不用再連接到資料庫，而可以直接從DataSet 或DataTable中獲取數據。

SqlDataAdapter提供了許多的方法，來方便我們對一些特定的資料集合進行操作比如，填充一個查詢結果到DataTable ,或DataSet 中其實是類似於：創建一個SqlCommand 然後執行`Select * from [Table]` 然後執行ExcuteReader()方法得到一個IDataReader物件然後逐行讀取資料並存放到一個集合物件中（如DataTable）經過測試，如果有大量的資料操作最好是自己寫SqlCommand ，會比SqlDataAdapter操作資料庫快很多

SqlDataReader只能讀取資料庫，而且所操作的表必須處於連線狀態，但是要對資料庫進行寫操時，只能藉助SqlCommand 類，SqlDataAdapter 它建立在SqlCommand 物件之上，它具有SqlCommand 類別的一切功能，能夠將資料填入DataSet 物件中，而且不用再連接到資料庫，而可以直接從DataSet 或DataTable 取得資料。 (因為它採用的無連接傳輸模式)

SqlDataReader物件可以從資料庫中得到唯讀的、只能向前的資料流，還可以提高應用程式的效能，減少系統開銷，同一時間只有一條行記錄在記憶體中。

SqlDataAdapter物件可以自動開啟和自動關閉資料庫連接(不需人為管理)，配接器的主要工作流程：
1. SqlConnection 物件建立與資料庫的連接
2. SqlDataAdapter 物件經由 SqlCommand 物件返回给SqlDataAdapter，最後將SqlDataAdapter物件加入到 DataSet 物件的 DataTables 物件中。(或是由SqlDataAdapter物件執行sql)


### 總結：

- 效能上：SqlDataReader一次只在記憶體中儲存一行，減少了系統開銷。優於SqlDataAdapter。
- 讀取時：SqlDataReader需透過呼叫自身Read()方法循環讀取資料到指定物件。而SqlDataAdapter可透過呼叫Fill()方法一次填入資料到DataSet。也可將對 DataSet 所做的變更解析回資料來源。
- 操作上：SqlDataReader需透過呼叫自身的Close()方法斷開連線。而SqlDataAdapter可以讀取完資料庫後自動斷開連線.


## 2. SqlDataAdapter 和 SqlCommand 區別

- SqlCommand就是命令了,可以用它來執行SQL指令；
- SqlDataAdapter就是資料配接器了,它是用於在資料來源與資料集之間通訊的一組物件；

SqlCommand 對應 DateReader；      
SqlDataAdapter 對應 DataSet；     

SqlCommand是C#中與Sql資料庫打交道的對象，幾乎所有的Sql資料庫操作都需要使用該物件來實現，但其功能有限，只是簡單的實現了與Sql資料庫的介面而已；       

SqlDataAdapter是一個功能強大的SqL資料配接器，也用來操作Sql資料庫，但它的操作都要透過SqlCommand來實現（有一個屬性物件的型別就是SqlCommand），也就是說，可以把SqlDataAdapter看成一個把一些特殊功能封裝了、增強了的SqlCommand。

---

因為DataSet是離線的，所以SqlDataAdapter這個物件是連接DataSet和資料庫的橋樑，所有對DataSet的操作（填充，更新等）都要通過他   

#### Ado.net資料存取有兩種方式：

1. 離線：透過DataSet,然後離線增，刪，改，最後透過SqlDataAdapter解析到資料庫中(存回db)
2. 連線：直接對資料庫運算SqlCommand (Update,Insert,Delete)

SqlCommand就是指令了,可以用它來執行SQL指令      
SqlDataAdapter就是資料適配器了,它是用於在資料來源和資料集之間通訊的一組對象     
SqlCommand對應DateReader        
SqlDataAdapter對應DataSet       
SqlCommand是C#中與Sql資料庫打交道的對象，幾乎所有的Sql資料庫操作都需要使用該對象來實現，但其功能有限，只是簡單的實現了與Sql資料庫的介面而已；       

SqlDataAdapter是一個功能強大的SqL資料適配器，也用來操作Sql資料庫，但它的操作都要透過SqlCommand來實現（有一個屬性物件的型別就是SqlCommand），也就是說，可以把SqlDataAdapter看成一個把一些特殊功能封裝了、增強了的SqlCommand！

SqlCommand與ADO時代的Command一樣，SqlDataAdapter則是ADO.NET中的新事物，它配合DataSet來使用。其實，DataSet就像是駐留在記憶體中的小資料庫，在DataSet中可以有多張DataTable，這些DataTable之間可以相互關聯，就像在資料庫中表格關聯一樣！        

SqlDataAdapter的功能是將資料從資料庫中提取出來，放在DataSet中，當DataSet中的資料變更時，SqlDataAdapter再將資料庫中的資料更新，以確保資料庫中的資料和DataSet中的資料是一致的！

用微軟顧問的話講：DataAdapter就像是一把鐵鍬，它負責把資料從資料庫鏟到DataSet中，或是將資料從DataSet鏟到資料庫中！

---

### SqlDataReader
- 只能順序讀，不能修改資料庫
- 在程式沒執行完之前一直保持連接
- 一次讀出,可以放在DATASET裡的。可以用來修改資料庫

### DataSet
- 作用：DataSet，DataAdapter讀取資料。

**Q：什麼是DataAdapter？**
答：DataAdapter物件在DataSet與資料之間起橋樑作用

### 範例

```c#
string connString = "uid=帳號;pwd=密碼;database=資料庫;server=伺服器";//SQL Server連結字串
SqlConnection conn = new SqlConnection (connString); //Sql連結類別的實體化

conn.Open ();//開啟資料庫

string sql = "SELECT * FROM 表名1 "; //要執行的SQL

SqlDataAdapter da = new SqlDataAdapter(strSQL,ConnSql); //建立DataAdapter資料配接器實體
DataSet ds = new DataSet();//建立DataSet實體
da.Fill(ds,"自訂虛擬表名");//使用DataAdapter的Fill方法(填充)，呼叫SELECT指令

ConnSql.Close ();//關閉資料庫
```

[MSDN - DataAdapter 和 DataReader](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/dataadapters-and-datareaders?redirectedfrom=MSDN)   
[MSDN - 由 DataReader 擷取的資料](https://learn.microsoft.com/zh-tw/sql/connect/ado-net/retrieve-data-by-datareader?view=sql-server-ver16)  
[MSDN - 從 DataAdapter 填入資料集](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/populating-a-dataset-from-a-dataadapter)      
[MSDN - SqlDataAdapter 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldataadapter?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)        
[MSDN - DataSet 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.dataset?view=net-8.0)        
[ADO.NET DataReader和DataAdapter的区别](https://www.cnblogs.com/shengwei/p/4578516.html)        
[SqlDataAdapter和SqlCommand区别](https://www.cnblogs.com/caojiajun/p/14244570.html)     
[[ADO.NET] DataAdapter 物件(資料配接器) by R](https://riivalin.github.io/posts/2021/06/dataadapter/)