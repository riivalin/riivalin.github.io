---
layout: post
title: "[MySQL Workbench][Data Export] Error Unknown table column_statistics in information_schema"
date: 2025-01-01 05:06:10 +0800
categories: [Notes,MySQL,Workbench]
tags: [issue,mysql,mysqldump,MySQL Workbench,MariaDB,Data Export,mysql-workbench,mysql-8.0,column_statistics]
---

MariaDB 11.5        
Workbench 8.038          

> 遇到Workbench內建mysqldump版本與資料庫mysqldump導致不相容的問題。


伺服器在 11.5 上面運行，在使用 MySQL Workbench 8.038 執行Ｄata Ｅxport 時發生錯誤，出現下面畫面：

![](/assets/img/post/mysql-workbench-mysqldump-export-error.png)


原因是WORKBENCH版本是8.038，而MYSQL版本是11.5.2，所以是2個版本不合的原因，無法匯出資料。


# 錯誤訊息

```
14:50:22 Dumping db (table_name) Running: mysqldump.exe --defaults-file="c:\users\username\appdata\local\temp\tmpvu0mxn.cnf" --user=db_user --host=db_host --protocol=tcp --port=1337 --default-character-set=utf8 --skip-triggers "db_name" "table_name" mysqldump: Couldn't execute 'SELECT COLUMN_NAME, JSON_EXTRACT(HISTOGRAM, '$."number-of-buckets-specified"') FROM information_schema.COLUMN_STATISTICS WHERE SCHEMA_NAME = 'db_name' AND TABLE_NAME = 'table_name';': Unknown table 'COLUMN_STATISTICS' in information_schema (1109)

Operation failed with exitcode 2 14:50:24 Export of C:\path\to\my\dump has finished with 1 errors
```


MySQL Workbench警告mysqldump Version Mismatch   

![](/assets/img/post/mysql-workbench-mysqldump-export-error-log.png)


由於低版本MySQL資料庫的information_schema中沒有名為COLUMN_STATISTICS的資料表，因此可以透過使用–column-statistics=0命令列參數來禁止該行為。

理論上，MySQL Workbench 8.0.13以上版本可以透過取消「使用列統計」，但8.0.14和8.0.15上選項該被錯誤地移除了：


## 解決方法：

### 1. 建立一個shell 腳本：mysqldump.cmd 
```cmd
@echo off
"C:\Program Files\MySQL\MySQL Workbench 8.0 CE\mysqldump.exe" %* --column-statistics=0
```
### 2. mysqldump.cmd 取代 mysqldump.exe 的路徑
Open MySQL Workbench > go to Edit > Preferences > Administration, change path to mysqldump tool and point it to mysqldump.cmd

![](/assets/img/post/mysql_data_export_mysqldump.png)



ref: <https://stackoverflow.com/questions/51614140/how-to-disable-column-statistics-in-mysql-8-permanently>