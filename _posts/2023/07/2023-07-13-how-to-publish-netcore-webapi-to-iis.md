---
layout: post
title: "[C# 筆記] 如何將 ASP.NET Core Web API 發佈到 IIS"
date: 2023-07-13 23:59:00 +0800
categories: [Notes,C#,IIS,Web Deploy]
tags: [C#,Web API,.Net Core,IIS,Web Deploy]
---

Take notes...

# 1. 建立 ASP.NET Core Web API專案
- 新建一個ASP.NET Core Web API專案
- 執行專案，以確保它可以運行 (Swagger UI頁面可正確讀取資料)

# 2. 在IIS新增網站 前置準備
在這之前需完成：安裝`Hosting Bundle`、開啟`IIS`功能。

## 下載安裝 Hosting Bundle
搜尋 [.net core hosting bundle 6](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)，下載 Windows `Hosting Bundle`並安裝。

## 開啟 IIS 功能
Path: 控制台 > 程式集 > 程式和功能 > 開啟或關閉Windows功能 > 勾選 `IIS` > 勾選 `WWW 服務`        

把「`Internet Information Services`」打開，然後直接到該功能底下的「`World Wide Web服務`」，「應用程式開發功能」中開啟相關的功能(.NET擴充性、ASP、ASP.NET、ISAPI...)

# 3. 在 IIS 新增網站

建立新的網站：IIS > 站台 > 新增網站 > 輸入：
- 站台名稱：`PublishCoreApiTest`
- 實體路徑：`D:\Site\PublishCoreApiTest`
- Port:`8081`(自訂)

# 4. 確認網站運行

確認是否正常運行：點擊站台`PublishCoreApiTest` > 右鍵:管理站台 > 瀏覽。      
按下「瀏覽」後，會開啟一個網頁，URL為 <http://localhost:8081/>

# 5. 設定VS發佈(Publish)
返回vs進行發佈：專案 > 右鍵點擊「發佈(Publish)」 > `IIS` > `Web Deploy`

# 6. 設定 Web Deploy

設定`Web Deploy`：(`IIS`連線)

- 伺服器：因為是本機，所以輸入 `localhost`
- 網站名稱：剛建立的站台名稱 `PublishCoreApiTest`
- 目的地URL：該網站的URL `http://localhost:8081/`
- 點擊「完成」

![web deploy](/assets/img/post/vs-iis-web-deploy.png)       

# 7. 將專案「發佈」到 IIS 

最後一步，需要將專案「發佈」到 `IIS` (需要「以系統管理員身份執行」VS).      
再一次點選「專案」 > 右鍵「發佈(Publish)」，        
同時開啟剛網站的實體路徑的資料夾：`D:\Site\PublishCoreApiTest`，        
原本是空的，按下「發佈(Publish)」後，裡面已經有資料文件了。

![web deploy iis](/assets/img/post/vs-web-deploy-iis.png)   

# 8. 瀏覽網站

回到 `IIS` > 站台 > 管理網站 > 瀏覽     
會出現 `HTTP ERROR 404`，那是因為沒有默認訪問到 web api。      
將 <http://localhost:8081> 加上 API Controller 的名稱 weatherforecast： <http://localhost:8081/weatherforecast> 就可以看到天氣的json資料了。        


[How To Publish ASP.NET Core Web API to IIS](https://www.youtube.com/watch?v=Lt3wve_nb0g)