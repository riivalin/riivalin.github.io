---
layout: post
title: [MySQL Workbench] 無法匯出資料(Data Export)：mysqldump Version Mismatch。log:MySQL Workbench Database Export Error Unknown table column statistics in information schema 1109.
date: 2024-08-30 06:23:00 +0800
categories: [Notes,MySQL,Workbench]
tags: [issue,mysql,mysqldump,MySQL Workbench,MariaDB]
---


> 遇到Workbench內建mysqldump版本與資料庫mysqldump導致不相容的問題。

# 錯誤訊息

MariaDB 11.5        
Workbench 8.038          

伺服器在 11.5 上面運行，在使用 MySQL Workbench 8.038 執行Ｄata Ｅxport 時發生錯誤，出現下面畫面：

原因是WORKBENCH版本是8.038，而MYSQL版本是11.5.2，所以是2個版本不合的原因，無法匯出資料。


MySQL Workbench警告mysqldump Version Mismatch   

![](/assets/img/post/mysql-workbench-mysqldump-export-error.png)


![](/assets/img/post/mysql-workbench-mysqldump-export-error-log.png)
```
mysqldump: Couldn't execute 'SELECT COLUMN_NAME, JSON_EXTRACT(HISTOGRAM, '$."number-of-buckets-specified"') FROM information_schema.COLUMN_STATISTICS WHERE SCHEMA_NAME = 'mls' AND TABLE_NAME = 'office';': Unknown table 'COLUMN_STATISTICS' in information_schema (1109)
```

由於低版本MySQL資料庫的information_schema中沒有名為COLUMN_STATISTICS的資料表，因此可以透過使用–column-statistics=0命令列參數來禁止該行為。

理論上，MySQL Workbench 8.0.13以上版本可以透過取消「使用列統計」，但8.0.14和8.0.15上選項該被錯誤地移除了：

# 解決方法

1.下載MYSQL相對應版本的程式 
<https://dev.mysql.com/downloads/mysql/>


Edit --> preferences --> Administrator --> Path to mysqldump tool: 将其路径设为：C:\Program Files\MySQL\MySQL Server 5.6\bin\mysqldump.exe






ref:      
<https://www.isharkfly.com/t/mariadb-mysql-workbench/216>         
<https://blog.csdn.net/uwoerla/article/details/128425524>


---


# MariaDB是MySQL嗎？

MySQL 是最廣泛採用的開放原始碼資料庫。 它是許多熱門網站、應用程式和商業產品的主要關聯式資料庫。 MariaDB 是MySQL 的修改版本。 MariaDB 是由MySQL 原始開發團隊製作的，原因是MySQL 被Oracle Corporation 收購後產生的授權和分發問題。

# MariaDB 是免費的嗎？      

MariaDB 是免費的開源關聯式資料庫管理系統（RDBMS ）。 它是由MySQL 的原始開發人員所建立，因為MySQL 擔心在Oracle 於2009 年收購MySQL 後將會進行商業化。 MariaDB 以C 和C++ 編寫，並支援多種程式設計語言，包括C、C#、Java、Python、PHP 和Perl。