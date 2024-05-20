---
layout: post
title: "[ADO.NET] DataReader 物件(唯讀順向)"
date: 2021-06-05 23:49:00 +0800
categories: [Notes,ADO.NET,C#]
tags: [C#,ADO.NET,command,DataReader,ExecuteReader]
---

DataReader 是以**順向**、**唯讀**的方式來傳回資料。

## DataReader(MSDN)

若要使用 DataReader 擷取資料，請建立 Command 物件的執行個體，再藉由呼叫 Command.ExecuteReader 擷取資料來源的資料列，建立 DataReader。 DataReader 提供無緩衝的資料流，可使程序邏輯有效地循序處理來自資料來源的結果。 需要擷取大量資料時，DataReader 是很好的選擇，因為資料不會快取至記憶體。     

使用 DataReader，其中 reader 代表有效的 DataReader，而 command 代表有效的 Command 物件。

```c#
SqlDataReader reader = command.ExecuteReader();
```

使用 DataReader.Read 方法，從查詢結果取得資料列。 您可以將資料行的名稱或循序編號傳遞給 DataReader，以存取傳回資料列的每個資料行。 不過，為了達到最佳效能，DataReader 也提供了一系列方法，讓您以資料行的原生資料類型 (GetDateTime、GetDouble、GetGuid、GetInt32 等等) 存取資料行的值。 如需資料提供者特有 DataReaders 的具型別存取子方法清單，

---

## DataReader

僅有 SQL Select 指令讀取資料時可使用 DataReader ，從資料來源使用 Command 物件執行指令，取得唯讀（Read-Only）和只能向前（Forward-Only）的串流資料，每一次只能從資料來源讀取一列資料（即一筆）儲存到記憶體，所以執行效率非常高。
        
- 執行查詢語句，傳回一個物件（SqlDataReader）。唯讀。
- SqlDataReader：即時讀取，讀取方式固定，不靈活（只進不出，只能前進，不能後退）。
- 適用：唯讀，不做資料修改的情況；資料量小。(適合單一資料表)
- Read 的每次調用都會從結果集中返回一行。(Read只會讀取一行的資料)

只提供唯讀且順向的資料，就是只會一行一行一直往下讀，沒有辦法往上回溯        

只要執行Read()方法，每讀完一筆資料就會把指標指向下一筆      

#### 注意：使用ExecuteReader讀取資料的時候，要即時保存，因為會讀一條，丟一條

## 範例

```c#
string connString = "Data Source=.;Initial catalog=DBTEST;User id=riva;Password=1234;Encrypt=true;Trust Server Certificate=True";

//建立db連線
using (SqlConnection conn = new SqlConnection(connString))
{
    //開啟db連線
    if (conn.State != ConnectionState.Open) conn.Open();

    //準備sql語句
    string sql = "select * from Emp";

    //建立SqlCommand，並告訴SqlCommand要執行的SQL語句 和連到哪個DB
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        //建立 DataReader, 用來接收ExecuteReader執行SQL後 所傳回的資料
        SqlDataReader dr = cmd.ExecuteReader();

        //檢是否有讀到資料
        if (dr.HasRows) //HasRow=有資料
        {
            //叫用 read 方法讀取資料 (Read只會讀取一行的資料)
            while (dr.Read()) //使用while讀到不能讀為止
            {
                //讀取數據，並將其寫入主控台
                Console.WriteLine($"{dr[0]}, {dr[1]}, {Convert.ToDateTime(dr[2]).ToString("yyyy-MM-dd")}");

                //或是這樣寫
                //以資料行的原生資料類型 (GetDateTime、GetDouble、GetGuid、GetInt32 等等) 存取資料行的值
                Console.WriteLine($"ID:{dr.GetInt32(0)}, Name:{dr.GetString(1)}");

            }
        }
    }
}
```

- HasRows：表示目前行SqlDataReader是否包含一行或多行，用來判斷是否有讀到資料 (HasRows=有資料行)
- Read()：使得目前SqlDataReader 前進到下一筆記錄，即向前讀取。(Read 會讀取一行的資料)
- While(dr.Reader()) //遍歷所有記錄 (使用while 讀到不能讀為止)     

執行結果：

```
1, 張三, 1999-09-09
2, 李四, 1989-12-09
3, 王五, 1979-08-09
```


[MSDN - SqlCommand 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)        
[MSDN - SqlDataReader 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.data.sqlclient.sqldatareader?view=netframework-4.8.1&viewFallbackFrom=dotnet-plat-ext-8.0)       
[MSDN - 執行命令](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/executing-a-command)  
[MSDN - 使用 DataReader 擷取資料](https://learn.microsoft.com/zh-tw/dotnet/framework/data/adonet/retrieving-data-using-a-datareader)
[MVCNF03-ADO.NET  by JohnsonNote](https://hackmd.io/@johnsonnote/webdesign/https%3A%2F%2Fhackmd.io%2F%40johnsonnote%2Fadonet)  
[CSDN - Ado.Net学习——基础知识记录](https://blog.csdn.net/SQWH_SSGS/article/details/109303103)   
[[ADO.NET] Command 物件 -- Draft  by R](https://riivalin.github.io/posts/2021/06/adonet-commad-draft/)    
[[ADO.NET] Command 物件 (執行SQL命令) by R](https://riivalin.github.io/posts/2021/06/adonet-command/)