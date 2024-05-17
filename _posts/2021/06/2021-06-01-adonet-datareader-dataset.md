---
layout: post
title: "[ADO.NET] DataReader 和 DataSet 的使用時機？"
date: 2021-06-01 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,DataReader,DataSet]
---

## DataReader 和 DataSet 的使用時機？

- 要使用`DataSet`：
    - 它在應用程式本地快取資料，所以可以操縱它。
    - 它動態地與資料互動，例如繫結到Windows表單控制元件。
    - 它允許在沒有開啟連線的情況下對資料執行處理。它可以在連線斷開的情況下工作。

如果需要除了上面提到的其他功能，可以使用`DataReader`來提高應用程式的效能。    
(因為可以減少`DataSet`消耗記憶體，並減少建立和填入`DataSet`內容所需處理的時間) 

- 要使用`DataReader`：
    - `DataReader`不以斷開模式執行。
    - 它要求`DataReader`物件必須與資料庫物件連線。

`DataReader`是以順向、唯讀的方式傳回資料。


## 使用 DataReader 擷取資料

若要使用 DataReader 擷取資料，請建立 Command 物件的執行個體，再藉由呼叫 Command.ExecuteReader 擷取資料來源的資料列，建立 DataReader。 DataReader 提供無緩衝的資料流，可使程序邏輯有效地循序處理來自資料來源的結果。 需要擷取大量資料時，DataReader 是很好的選擇，因為資料不會快取至記憶體。

### 使用 DataReader

```c#
reader = command.ExecuteReader();
```


### 範例

在 DataReader 物件中逐一查看，並從每個資料列傳回兩個資料行。

```c#
static void HasRows(SqlConnection connection)
{
    using (connection)
    {
        SqlCommand command = new(
            "SELECT CategoryID, CategoryName FROM Categories;",
            connection);
        connection.Open();

        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Console.WriteLine("{0}\t{1}", 
                    reader.GetInt32(0),
                    reader.GetString(1));
            }
        }
        else
        {
            Console.WriteLine("No rows found.");
        }
        reader.Close(); //關閉 DataReader
    }
}
```

關閉 DataReader
- 用完 DataReader 物件後，請一律呼叫 Close 方法。
- 如果您的 Command 包含輸出參數或傳回值，則必須等到 DataReader 關閉後才能使用這些值。
- 在 DataReader 開啟期間，Connection 只能供該 DataReader 使用。 必須等到原始 DataReader 關閉後，才能執行 Connection 的任何命令 (包括建立其他 DataReader)。



## DataSet

代表資料的記憶體內部快取。      

DataSet 物件對於支援 ADO.NET 的中斷連接、分散式的資料案例非常重要。         

DataSet 是常駐記憶體的資料表示，可提供與資料來源無關的一致性關聯式程式設計模型。        

它可與多個不同的資料來源一起使用、與 XML 資料一起使用，或管理應用程式的本機資料。       

DataSet 表示一組完整的資料，包括相關資料表、條件約束及資料表間的關聯性。 下圖顯示 DataSet 物件模型。        

[![](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/media/ado-1-bpuedev11.png)](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/media/ado-1-bpuedev11.png)

DataSet 物件模型        
DataSet 中的方法及物件與關聯式資料庫模型中的方法及物件一致。        
DataSet 還能以 XML 保存及重新載入其內容，以 XML 結構描述定義語言 (XSD) 結構描述保存及重新載入其結構描述。       

### 範例

下列範例包含數種方法，這些方法結合、建立及填滿 `DataSet` Northwind資料庫中的 。

```c#
using System;
using System.Data;
using System.Data.SqlClient;

namespace Microsoft.AdoNet.DataSetDemo
{
    class NorthwindDataSet
    {
        static void Main()
        {
            string connectionString = GetConnectionString();
            ConnectToData(connectionString);
        }

        private static void ConnectToData(string connectionString)
        {
            //Create a SqlConnection to the Northwind database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Create a SqlDataAdapter for the Suppliers table.
                SqlDataAdapter adapter = new SqlDataAdapter();

                // A table mapping names the DataTable.
                adapter.TableMappings.Add("Table", "Suppliers");

                // Open the connection.
                connection.Open();
                Console.WriteLine("The SqlConnection is open.");

                // Create a SqlCommand to retrieve Suppliers data.
                SqlCommand command = new SqlCommand(
                    "SELECT SupplierID, CompanyName FROM dbo.Suppliers;",
                    connection);
                command.CommandType = CommandType.Text;

                // Set the SqlDataAdapter's SelectCommand.
                adapter.SelectCommand = command;

                // Fill the DataSet.
                DataSet dataSet = new DataSet("Suppliers");
                adapter.Fill(dataSet);

                // Create a second Adapter and Command to get
                // the Products table, a child table of Suppliers.
                SqlDataAdapter productsAdapter = new SqlDataAdapter();
                productsAdapter.TableMappings.Add("Table", "Products");

                SqlCommand productsCommand = new SqlCommand(
                    "SELECT ProductID, SupplierID FROM dbo.Products;",
                    connection);
                productsAdapter.SelectCommand = productsCommand;

                // Fill the DataSet.
                productsAdapter.Fill(dataSet);

                // Close the connection.
                connection.Close();
                Console.WriteLine("The SqlConnection is closed.");

                // Create a DataRelation to link the two tables
                // based on the SupplierID.
                DataColumn parentColumn =
                    dataSet.Tables["Suppliers"].Columns["SupplierID"];
                DataColumn childColumn =
                    dataSet.Tables["Products"].Columns["SupplierID"];
                DataRelation relation =
                    new System.Data.DataRelation("SuppliersProducts",
                    parentColumn, childColumn);
                dataSet.Relations.Add(relation);

                Console.WriteLine(
                    "The {0} DataRelation has been created.",
                    relation.RelationName);
            }
        }

        static private string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Data Source=(local);Initial Catalog=Northwind;"
                + "Integrated Security=SSPI";
        }
    }
}
```


[MSDN - ADO.NET 架構 (選擇 DataReader 或 DataSet)](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/ado-net-architecture)     
[MSDN - 使用 DataReader 擷取資料](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/retrieving-data-using-a-datareader)        
[MSDN - DataSet 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.dataset?view=net-8.0)        
[MSDN - ADO.NET 資料集](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/ado-net-datasets)
[MSDN - SqlCommand.ExecuteNonQuery 方法](https://learn.microsoft.com/zh-tw/dotnet/api/microsoft.data.sqlclient.sqlcommand.executenonquery?view=sqlclient-dotnet-standard-5.1)  
