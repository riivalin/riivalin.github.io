---
layout: post
title: "[MySql][SP] 傳入參數值使用 json array 方式帶入"
date: 2024-05-02 06:23:00 +0800
categories: [Notes,MySql]
tags: [mysql,json,sp,sp (json array - ids)]
---

Take notes for me.

## 測試：使用 JSON_TABLE 將 JSON 陣列轉為表格

```sql
SET @J = '[{"A":1,"B":2},{"A":2,"B":3}]';

-- 使用 JSON_TABLE 将 JSON 数组转为表格
SELECT * 
FROM JSON_TABLE(
    @J,                        -- JSON 数据
    '$[*]'                     -- JSON 数组的每个元素路径
    COLUMNS (
        A INT PATH '$.A',      -- 映射 JSON 中的 "A" 键到列 A
        B INT PATH '$.B'       -- 映射 JSON 中的 "B" 键到列 B
    )
) AS jt;                        -- 给表函数一个别名
```

執行結果：

```
# A, B
'1', '2'
'2', '3'
```


## SP

```sql
WHERE id IN (SELECT m.matchId FROM JSON_TABLE(matchIds, '$[*]' COLUMNS (matchId INT PATH '$')) AS m)
```

```sql
CREATE PROCEDURE `json_array_mapping_get`(
	IN matchIds TEXT -- IDs
)
BEGIN
	SELECT 
		m.id,
		l.color,
		l.`type` AS kind,
		m.league_id,
		l.name_en AS league_en,
		m.sub_league_id,
		s.sub_name_en AS sub_league_en,
        m.match_time,
		m.start_time,
		e.explain_en
	FROM
		matches AS m 
			INNER JOIN
		leagues AS l ON m.league_id = l.id # 內連接足球聯賽表，根據聯賽ID匹配，別名為 l
			LEFT JOIN
		sub_leagues AS s ON m.sub_league_id = s.id	# 左連接足球子聯賽表，根據子聯賽ID匹配，若無匹配則返回 NULL，別名為 s
			INNER JOIN
		teams AS h ON m.home_id = h.id # 內連接主隊表，根據主隊ID匹配，別名為 h
			INNER JOIN
		teams AS a ON m.away_id = a.id # 內連接客隊表，根據客隊ID匹配，別名為 a
			LEFT JOIN
		explain AS e ON e.match_id = m.id # 左連接比賽說明表，根據比賽ID匹配，若無匹配則返回 NULL，別名為 e
	WHERE
		# 從 JSON_ARRAY 中提取所有 matchId，定義 matchId 列，將 JSON 數組中的每個元素映射到整數類型
		m.id IN (SELECT m.matchId FROM JSON_TABLE(matchIds, '$[*]' COLUMNS (matchId INT PATH '$')) AS m);
END
```

## 執行方式

```sql
-- [1,2,3,4,5] array 方式傳入
call match_get('[999,888,31530,1520,2519]');
```


