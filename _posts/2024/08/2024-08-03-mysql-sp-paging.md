---
layout: post
title: [MySql][SP] 分頁 - 使用 SQL_CALC_FOUND_ROWS、FOUND_ROWS()
date: 2024-08-03 06:23:00 +0800
categories: [Notes,MySql]
tags: [MySql,分頁,paging,SQL_CALC_FOUND_ROWS,FOUND_ROWS, do-while]
---

Take notes for me.

## 測試 SQL_CALC_FOUND_ROWS 和 FOUND_ROWS()

```sql
-- 查詢數據並計算總記錄數
SELECT SQL_CALC_FOUND_ROWS id, name_en
FROM leagues
LIMIT 1, 100;

-- 查詢總記錄數
SELECT FOUND_ROWS() AS totalRecords;  
```

## 分頁

```sql
CREATE PROCEDURE `paging_get`(
    IN pageNum INT,        -- 當前頁碼
    IN pageSize INT        -- 每頁顯示數據量
)
BEGIN
    -- 計算起始點
    DECLARE `offset` INT;
    SET `offset` = (pageNum - 1) * pageSize;

    -- 查詢數據並計算總記錄數
    SELECT SQL_CALC_FOUND_ROWS id, season
    FROM leagues
    LIMIT `offset`, pageSize;

    -- 查詢總記錄數
    SELECT FOUND_ROWS() AS totalRecords;   
END
```

執行結果：

會返回兩個結果集：
1. 查詢的數據
2. totalRecords

## C# 調用
### 調用SP取得兩個結果集的資料

```c#
/// <summary>
/// 取得 Season 的集合和總筆數
/// </summary>
/// <param name="pageNum">當前頁碼</param>
/// <param name="pageSize">每頁顯示數據量</param>
/// <returns>總筆數和 Season(賽季) 集合</returns>
public (int totalCount, IEnumerable<Season> seasons) GetSeason(int pageNum, int pageSize)
{
    // 獲取數據庫連接
    using var conn = dbHelper.DbConnection();

    // 使用 Dapper 調用預存程序
    var query = conn.QueryMultiple("season_get",
        new { pageNum, pageSize },
        commandType: System.Data.CommandType.StoredProcedure);

    // 獲取 Season 集合(賽季)
    var data = query.Read<Season>();

    // 獲取總筆數
    int totalCount = query.ReadSingle<int>();

    // 返回總筆數和 Season(賽季) 集合
    return (totalCount, data);
}
```

## C# 處理拿到的資料

```c#
// 從DB取得資料： totalCount:記錄資料總筆數， datas: 查詢的資料數據
(int totalCount, var datas) = RTest.GetSeason(pageNum, pageSize);
```

```c#
public async Task SetResultsAsync()
{
    const int pageSize = 500;   // 每頁顯示數據量
    int pageNum = 1;            // 當前頁碼
    int totalCount;             // 資料總筆數

    do //使用後置迴圈，至少先會執行一次, 再判斷
    {
        // 從DB取得資料： totalCount:記錄資料總筆數， datas: 查詢的資料數據
        (totalCount, var datas) = RTest.GetSeason(pageNum, pageSize);

        // 逐一將資料取出
        foreach (var item in datas)
        {
            // TODO Something...
            
            // 從 RTest 非同步取得 Seaon 賽季 數據
            var results = await RDemo.GetResultsDataAsync(item.Id, item.CurrSeason).ConfigureAwait(false);
            
            // 將取得的資料儲存到 RDemo 中
            RDemo.SetResults(results);
        }

        pageNum++;
    } while (pageNum <= Math.Ceiling((double)totalCount / pageSize));
}
```

## Note: 只做 limit 處理

(SP改成只傳入limit的參值)       

```c#
const int count = 500;  // 要顯示的數據量
int totalCount;         // 資料總筆數

do {
    // TODO Something...

} while (totalCount > count); // 如果未處理的資料數量超過 count，繼續進行下一輪的上傳處理
``` 


```c#
/// <summary>
/// 設定圖片檔案的位置，將圖片上傳S3，並取得S3檔案位置
/// </summary>
public async Task SyncSetFootballTeamImageS3Async()
{
    const int count = 500;  // 要顯示的數據量
    int totalCount;         // 資料總筆數

    do
    {
        // 從資料庫取得圖片資料：totalCount 代表資料總筆數，datas 是尚未上傳至 S3 的圖片列表
        (totalCount, var datas) = Test.ImageS3(count);

        // 逐一處理每筆圖片資料
        foreach (var item in datas)
        {
            try
            {
                // 將圖片上傳到 S3，並取得 S3 上的檔案位置，附加URL圖片參數 imageUrlParam
                item.LogoS3 = await awsS3Helper.UploadAsync($"{item.Logo}{imageUrlParam}", $"{folderRoot}RivaTest");

                // 將 S3 圖片位置資料儲存到 Test 中
                Test.ImagesS3(item);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
        }
    } while (totalCount > count); // 如果未處理的資料數量超過 count，繼續進行下一輪的上傳處理
}
```