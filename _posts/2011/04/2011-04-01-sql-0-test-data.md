---
layout: post
title: "[SQL] 測試用Script"
date: 2011-04-01 04:53:00 +0800
categories: [Notes,SQL]
tags: [sql script]
---

## Table

```sql
/*
 * 製作表單指令檔(SQL Server 版)
 *
 * 已確認可在SQL Server 2005 + Windows XP Service Pack 2中執行
 */

-- 製作表單：商品清單
CREATE TABLE 商品清單 (
    商品ID     NUMERIC NOT NULL,
    商品名稱     VARCHAR(30) NOT NULL,
    群組名稱   VARCHAR(20) NOT NULL,
    進貨單價   NUMERIC,
    販賣單價     NUMERIC,
    CONSTRAINT pk_shohin PRIMARY KEY (商品ID)
);

-- 製作表單：負責人清單
CREATE TABLE 負責人清單 (
    負責人ID   NUMERIC NOT NULL,
    負責人姓名   VARCHAR(20) NOT NULL,
    姓名拼音   VARCHAR(20) NOT NULL,
    MGR_ID     NUMERIC,
    出生日期   DATETIME NOT NULL,
    性別       NUMERIC NOT NULL,
    CONSTRAINT pk_tanto PRIMARY KEY (負責人ID),
    CONSTRAINT fk_mgr FOREIGN KEY (MGR_ID) REFERENCES 負責人清單(負責人ID),
    CONSTRAINT ck_gender CHECK (性別=0 OR 性別=1)
);

-- 製作表單：顧客清單
CREATE TABLE 顧客清單 (
    顧客ID     NUMERIC NOT NULL,
    顧客名稱     VARCHAR(20) NOT NULL,
    聯絡地址     VARCHAR(20) NOT NULL,
    CONSTRAINT pk_kokyaku PRIMARY KEY (顧客ID)
);

-- 製作表單：販賣資料
CREATE TABLE 販賣資料 (
    傳票編號   NUMERIC NOT NULL,
    列編號     NUMERIC NOT NULL,
    處理日     DATETIME NOT NULL,
    商品ID     NUMERIC NOT NULL,
    負責人ID   NUMERIC NOT NULL,
    顧客ID     NUMERIC NOT NULL,
    數量       NUMERIC NOT NULL,
    CONSTRAINT pk_uriage PRIMARY KEY (傳票編號, 列編號),
    CONSTRAINT fk_shohin FOREIGN KEY (商品ID) REFERENCES 商品清單(商品ID),
    CONSTRAINT fk_tanto FOREIGN KEY (負責人ID) REFERENCES 負責人清單(負責人ID),
    CONSTRAINT fk_kokyaku FOREIGN KEY (顧客ID) REFERENCES 顧客清單(顧客ID)
);

-- 製作表單：分店負責人清單
CREATE TABLE 分店負責人清單 (
    分店負責人ID   NUMERIC NOT NULL,
    分店負責人姓名   VARCHAR(20) NOT NULL,
    姓名拼音   VARCHAR(20) NOT NULL,
    MGR_ID     NUMERIC,
    出生日期   DATETIME NOT NULL,
    性別       NUMERIC NOT NULL,
    CONSTRAINT pk_siten_tanto PRIMARY KEY (分店負責人ID),
    CONSTRAINT fk_siten_mgr FOREIGN KEY (MGR_ID) REFERENCES 分店負責人清單(分店負責人ID),
    CONSTRAINT ck_siten_gender CHECK (性別=0 OR 性別=1)
);
```

## Data

```sql
/*
 * 範例資料
 */

-- 插入資料：商品清單
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (1, '桌上型電腦', '電腦主機', 150000, 180000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (2, '筆記型電腦', '電腦主機', 230000, 270000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (3, '17吋螢幕', '周邊設備', 40000, 50000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (4, '19吋螢幕', '周邊設備', 80000, 95000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (5, '15吋液晶螢幕', '周邊設備', 100000, 120000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (6, '數位相機', '周邊設備', NULL, NULL);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (7, '印表機', '周邊設備', 20000, 25000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (8, '掃描器', '周邊設備', 25000, 30000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (9, 'HUB', '網路設備', 5000, 7000);
INSERT INTO 商品清單 (商品ID, 商品名稱, 群組名稱, 進貨單價, 販賣單價) VALUES (10, '網路卡', '網路設備', 15000, 20000);

-- 插入資料：負責人清單
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別) VALUES (1, '鈴木', 'SUZUKI', '1960-01-23', 1);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別) VALUES (2, '小野', 'ONO', '1960-08-02', 1);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別) VALUES (3, '齋藤', 'SAITO', '1963-10-15', 1);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (4, '藤本', 'FUJIMOTO', '1972-07-18', 1, 3);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (5, '小林', 'KOBAYASHI', '1971-02-11', 0, 3);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (6, '伊藤', 'ITO', '1972-04-01', 0, 2);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (7, '佐瀨', 'SASE', '1975-02-21', 1, 2);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (8, '宇賀神', 'UGAJIN', '1975-12-22', 1, 1);
INSERT INTO 負責人清單 (負責人ID, 負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (9, '岡田', 'OKADA', '1972-03-18', 1, 4);

-- 插入資料：顧客清單
INSERT INTO 顧客清單 (顧客ID, 顧客名稱, 聯絡地址) VALUES (1, 'Waikiki軟體', '090-AAAA-AAAA');
INSERT INTO 顧客清單 (顧客ID, 顧客名稱, 聯絡地址) VALUES (2, '鈴木商事', '090-BBBB-BBBB');
INSERT INTO 顧客清單 (顧客ID, 顧客名稱, 聯絡地址) VALUES (3, '齋藤模型店', '090-CCCC-CCCC');
INSERT INTO 顧客清單 (顧客ID, 顧客名稱, 聯絡地址) VALUES (4, 'MicroHard', '090-DDDD-DDDD');
INSERT INTO 顧客清單 (顧客ID, 顧客名稱, 聯絡地址) VALUES (5, 'Lanru', '090-EEEE-EEEE');

-- 插入資料：販賣資料
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (1, 1, '2006-04-06', 1, 1, 2, 3);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (1, 2, '2006-04-06', 4, 1, 2, 3);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (2, 1, '2006-04-12', 1, 2, 1, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (3, 1, '2006-04-18', 1, 2, 2, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (4, 1, '2006-04-26', 2, 3, 4, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (4, 2, '2006-04-26', 7, 3, 4, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (4, 3, '2006-04-26', 8, 3, 4, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (5, 1, '2006-05-08', 3, 6, 1, 3);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (6, 1, '2006-05-12', 1, 2, 5, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (6, 2, '2006-05-12', 3, 2, 5, 2);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (7, 1, '2006-05-19', 2, 5, 4, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (8, 1, '2006-05-22', 2, 6, 1, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (9, 1, '2006-05-25', 5, 8, 2, 5);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (10, 1, '2006-06-02', 5, 2, 1, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (11, 1, '2006-06-06', 2, 3, 3, 2);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (11, 2, '2006-06-06', 10, 3, 3, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (12, 1, '2006-06-12', 2, 6, 2, 1);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (13, 1, '2006-06-15', 9, 7, 5, 5);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (13, 2, '2006-06-15', 2, 7, 5, 2);
INSERT INTO 販賣資料 (傳票編號, 列編號, 處理日, 商品ID, 負責人ID, 顧客ID, 數量) VALUES (13, 3, '2006-06-15', 10, 7, 5, 1);

-- 插入資料：分店負責人清單
INSERT INTO 分店負責人清單 (分店負責人ID, 分店負責人姓名, 姓名拼音, 出生日期, 性別) VALUES (4, '藤本', 'FUJIMOTO', '1972-07-18', 1);
INSERT INTO 分店負責人清單 (分店負責人ID, 分店負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (9, '岡田', 'OKADA', '1972-03-18', 1, 4);
INSERT INTO 分店負責人清單 (分店負責人ID, 分店負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (10, '田中', 'TANAKA', '1975-05-23', 1, 9);
INSERT INTO 分店負責人清單 (分店負責人ID, 分店負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (11, '井上', 'INOUE', '1980-02-18', 1, 9);
INSERT INTO 分店負責人清單 (分店負責人ID, 分店負責人姓名, 姓名拼音, 出生日期, 性別, MGR_ID) VALUES (12, '佐佐木', 'SASAKI', '1968-10-10', 1, 9);
```