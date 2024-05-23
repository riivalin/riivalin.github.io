---
layout: post
title: "[SQL] 註解範例"
date: 2000-01-04 06:30:00 +0800
categories: [Notes,SQL]
tags: [註解]
---

```sql
-- =============================================
-- Author:		RiiDemo
-- Create date: 2000/01/04
-- Description:	新增單筆使用者
-- =============================================
CREATE PROCEDURE AddUserData  
@UserId nvarchar(50),
@UserName nvarchar(50),
@IsValid bit
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Users (UserId , UserName , IsValid) VALUES (@UserId , @UserName , @IsValid)
END
GO
```