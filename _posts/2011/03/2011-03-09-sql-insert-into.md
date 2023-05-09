---
layout: post
title: "[SQL筆記] Insert Into 新增數據"
date: 2011-03-09 23:43:00 +0800
categories: [Notes,SQL]
tags: [T-SQL,sql script,insert into,insert into select]
---

## INSERT INTO 敘述句 (SQL INSERT INTO Statement)

INSERT INTO 是用來新增資料至某資料表 (table)。

## INSERT INTO 語法 (SQL INSERT INTO Syntax)
```sql
INSERT INTO table_name (column1, column2, column3...)
VALUES (value1, value2, value3...);
```
或，可以簡寫成這樣：
```sql
INSERT INTO table_name
VALUES (value1, value2, value3...);
```
> 使用簡寫的語法每個欄位的值都必需要依序輸入。

## INSERT INTO 用法 (Example)

假設我們想從下面的 customers 資料表中新增一顧客的資料：
```
C_Id	Name	City	Address	Phone
1	張一	台北市	XX路100號	02-12345678
2	王二	新竹縣	YY路200號	03-12345678
```
我們可以使用以下的 INSERT INTO 敘述句：
```sql
INSERT INTO customers (C_Id, Name, City, Address, Phone)
VALUES (3, '李三', '高雄縣', 'ZZ路300號', '07-12345678');
```
或，簡寫：
```sql
INSERT INTO customers
VALUES (3, '李三', '高雄縣', 'ZZ路300號', '07-12345678');
```
查詢新增後的結果如下：

```
C_Id	Name	City	Address	Phone
1	張一	台北市	XX路100號	02-12345678
2	王二	新竹縣	YY路200號	03-12345678
3	李三	高雄縣	ZZ路300號	07-12345678
```
只輸入幾個特定的欄位值

你也可以只輸入幾個特定的欄位值：

```sql
INSERT INTO customers (C_Id, Name, City)
VALUES (3, '李三', '高雄縣');
```
查詢新增後的結果如下：

```
C_Id	Name	City	Address	Phone
1	張一	台北市	XX路100號	02-12345678
2	王二	新竹縣	YY路200號	03-12345678
3	李三	高雄縣		
```

## 一次新增多筆資料 (INSERT INTO SELECT)
### 語法：
```sql
INSERT INTO table_name
VALUES (value1_1, value2_2, value3_3,···),
(value2_1, value2_2, value2_3,···),
(value3_1, value3_2, value3_3,···),
······;
```

或利用子查詢，從其它的資料表中取得資料來作一次多筆新增：

```sql
INSERT INTO table_name (column1, column2, column3,...)
SELECT othercolumn1, othercolumn2, othercolumn3,...
FROM othertable_name;
```
在子查詢中你也可以利用 WHERE、GROUP BY 及 HAVING 等子句來作有條件的新增資料。

[https://www.fooish.com/sql/insert-into.html](https://www.fooish.com/sql/insert-into.html)