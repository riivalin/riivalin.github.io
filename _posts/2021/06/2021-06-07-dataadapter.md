---
layout: post
title: "[ADO.NET] DataAdapter 物件(資料配接器)"
date: 2021-06-07 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,command,DataAdapter,DataSet,DataTable]
---



## DataAdapter

SqlDataAdapter：DataSet與SQL Server之間的橋接器(中介角色)。     

把DataAdapter 物件所執行的 SQL命令的結果 填入 DataSet 中，並更新解析回 DB

> - [SqlDataAdapter 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldataadapter?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)代表一組資料命令和資料庫連線，用來填入 DataSet 並更新 SQL Server 資料庫。 此類別無法獲得繼承。     
> - [DataSet 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.dataset?view=net-8.0) 代表資料的記憶體內部快取。


```c#
// Assumes that connection is a valid SqlConnection object.  
string queryString = "SELECT CustomerID, CompanyName FROM dbo.Customers";  
SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);  
  
DataSet customers = new DataSet();  
adapter.Fill(customers, "Customers");
```


## 填入資料 & 取出資料

```c#
SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);  
DataSet ds = new();
adapter.Fill(ds); //填入dataSet, ds.Tables[0] 就是你所要查詢的結果集
string id = dt.Rows[0]["id"].ToString(); //dt.Rows[0]表示第一行資料，table.Rows[0]["id"]表示table中列id的值
```

當`adapter.Fill(dataSet);`執行後，程式會透過SQL引擎回傳一個結果集存放在記憶體中，這個結果集就是一個`DataTable`,並且這個`DataTable`會被添加到`ds.Tables`中，所以 `ds.Tables[0]`，就是你所要查詢的結果集。

`table.Rows[0]`表示table的第一行數據，`table.Rows[0]["id"]`表示table中列id的值，        
`table`的所有列是你在SQL中所查詢的所有列


### 寫法一：將資料放入DataTable中

```c#
//建立 DataAdapter 來執行sql語句，並告訴它是連接哪個db
using (SqlDataAdapter da = new(sql, conn))
{
    //建立DataTable，用來存放DataAdapter執行sql 的結果
    DataTable dt = new();

    //DataAdapter呼叫Fill方法，Fill 會將執行sql 的結果 放入DataTable中
    da.Fill(dt);
}
```

直接把資料放入 DataTable 中

取資料時用: `dt.Rows`       
(第一行第一列`dt.Rows[0][0]`，第一行第二列`dt.Rows[0][1]`)


### 寫法二：將資料放入DataSet中

資料结果放到dataset中，若要用哪個datatable，可以這樣：`dataset[0]` 

```c#
//建立 DataAdapter 來執行sql語句，並告訴它是連接哪個db
using (SqlDataAdapter da = new(sql, conn))
{
    //建立DataSet，用來存放DataAdapter執行sql 的結果
    DataSet ds = new();

    //DataAdapter呼叫Fill方法，Fill 會將執行sql 的結果 放入DataSet中
    da.Fill(ds); // 或者加上table名   da.Fill(ds,"emp")
}
```

數據結果放到DataSet中，若要用到哪個table，      
可以這樣取：`ds.Tables[0]`      
(第一行第一列`ds.Tables[0].Rows[0][0]`，第一行第二列`ds.Tables[0].Rows[0][0]`)


或者 加上填入dataset時 加上table名： 

```c#
//建立 DataAdapter 來執行sql語句，並告訴它是連接哪個db
using (SqlDataAdapter da = new(sql, conn)) 
{
    //建立DataSet，用來存放DataAdapter執行sql 的結果
    DataSet ds = new();

    //DataAdapter呼叫Fill方法，Fill 會將執行sql 的結果 放入DataSet中
    da.Fill(ds, "employee");
}
```

用的時這樣取：`ds.Tables["employee"]` 


## 範例1: 資料放入DataTable

- 使用 SqlDataAdapter 來執行 sql 語法
- 將執行sql命令的資料結果 放入DataTable 中
- 從 DataTable 取出資料

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立db連接
using (SqlConnection conn = new(connString))
{
    //開啟db
    if (conn.State != ConnectionState.Open) conn.Open();

    //建立 DataAdapter 來執行sql語句，並告訴它是連接哪個db
    using (SqlDataAdapter da = new("select * from emp", conn))
    {
        //建立DataTable，用來存放DataAdapter執行sql 的結果
        DataTable dt = new();

        //DataAdapter呼叫Fill方法，Fill 會將執行sql 的結果 放入DataTable中
        da.Fill(dt);
        
        //將資料從datatable取出，並輸出到控制台上
        //1.顯示第一筆資料
        Console.WriteLine($"編號：{dt.Rows[0][0]}, 姓名：{dt.Rows[0][1]}");

        //2.顯示全部資料: 兩次迴圈
        foreach (DataRow row in dt.Rows) {
            foreach (DataColumn column in dt.Columns) {
                Console.WriteLine($"{row[column]}");
            }
        }

        //3.如果想把某列的值拼接字串，那就去掉内層循环就行了
        foreach (DataRow row in dt.Rows) {
            Console.WriteLine($"編號：{row["EmpId"]}, 姓名：{row["EmpName"]}");
        }
    }
}
```

- `dt.Rows[0]`表示第一行資料   
- `dt.Rows[0][0]`表示table中第一行第一列的值
- `dt.Rows[0][1]`表示table中第一行第二列的值

執行結果： 顯示第一筆資料    

```
編號：1, 姓名：張三
```

### 遍歷 DataTable
#### 要顯示全部資料，只要做兩次循環即可：

```c#
foreach (DataRow row in dt.Rows)
{
    foreach (DataColumn column in dt.Columns)
    {
        Console.WriteLine($"{column}:{row[column]}");
    }
}
```
`row[column]` 中的column是檢索出來的表列名

執行結果：

```
EmpId：1
EmpName：張三
EmpId：2
EmpName：李四
```

#### 如果想把某列的值拼接字串，那就去掉内層循环就行了：

```c#
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine($"編號：{row["EmpId"]}, 姓名：{row["EmpName"]}");
}
```

執行結果：

```
編號：1, 姓名：張三
編號：2, 姓名：李四
編號：3, 姓名：王五
```

## 範例2: 資料放入DataSet

- 使用 SqlDataAdapter 來執行 sql 語法
- 將執行sql命令的資料結果 放入DataSet 中
- 從 DataTable 中取出一筆資料

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立db連接
using (SqlConnection conn = new(connString))
{
    //開啟db
    if (conn.State != ConnectionState.Open) conn.Open();

    //建立 DataAdapter 來執行sql語句，並告訴它是連接哪個db
    using (SqlDataAdapter da = new("select * from emp", conn))
    {
        //建立DataSet，用來存放DataAdapter執行sql 的結果
        DataSet ds = new();

        //DataAdapter呼叫Fill方法，Fill 會將執行sql 的結果 放入DataSet中
        da.Fill(ds); //或是 加上table名： da.Fill(ds,"Emp"); 取值：ds.Tables["Emp"].Rows[0][0]

        //從DataSet將資料取出，並輸出到控制台上。或是 ds.Tables["Emp"].Rows[0][0]
        Console.WriteLine($"編號：{ds.Tables[0].Rows[0][0]}, 姓名：{ds.Tables[0].Rows[0][1]}");
    }
}
```

資料填入DataSet時，可以加上 Table名：`da.Fill(ds,"Employee");`      
取值時，同樣也加上Table名：`ds.Tables["Employee"].Rows[0][0]`


執行結果：

```
編號：1, 姓名：張三
```

## DataSet & DataTable

可以把DataTable和DataSet看做是資料容器，例如你查詢資料庫後面得到一些結果，可以放到這種容器裡。

那你可能要問：我不用這種容器，自己讀到變數或陣列裡也一樣可以存起來啊，為什麼要用容器？

原因是，這種容器的功能比較強大，除了可以存數據，還可以有更大用途。      
舉例：在一個c/s結構的桌面資料庫系統裡，你可以把前面存放查詢結果的容器裡的資料顯示到你客戶端介面上，使用者在介面上對資料進行新增、刪除、修改，你可以把使用者的操作更新到容器，等使用者操作完畢了，要求更新，然後你才把容器整個的資料變化更新到資料庫，       

這樣做的好處是什麼？就是減少了資料庫操作，客戶端速度提高了，資料庫壓力減少了。

DataSet 可以比喻為一個記憶體中的資料庫，DataTable 是一個記憶體中的資料表，DataSet裡可以儲存多個DataTable。

- DataSet：資料集。一般包含多個DataTable，用的時候，dataset["表名"]得到 DataTable
- DataTable：資料表。


[MSDN - 從 DataAdapter 填入資料集](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/populating-a-dataset-from-a-dataadapter)      
[MSDN - SqlDataAdapter 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldataadapter?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-5.0)        
[MSDN - DataSet 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.dataset?view=net-8.0)
[C#之DataSet和DataTable](https://www.cnblogs.com/wenjie0904/p/7719751.html)