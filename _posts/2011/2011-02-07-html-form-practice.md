---
layout: post
title: "[HTML] form表單 練習"
date: 2011-02-07 00:12:03 +0800
categories: [Notes,HTML]
tags: [HTML]
---

## 練習1：帳號密碼登入

- 先寫form，再寫table
- form 要有兩個屬性：action, method
- 要Server端的數據，要寫內部屬性name

```html
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>
    <form action="https://google.com" method="get">
        <table border="1" cellpadding="0" cellspacing="0">
            <tr>
                <td>帳號</td>
                <td><input type="text" name="txtName" /></td>
            </tr>
            <tr>
                <td>密碼</td>
                <td><input type="password" name="txtPwd" /></td>
            </tr>
            <tr>
                <td>驗証碼</td>
                <td><input type="text" name="txtJudge" style="width:80px;"/><img src="1.jpg" style="height:20px; width:60px;"/></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="checkbox" name="chkRemember" />記住密碼</td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input type="submit" value="登入" />
                    <input type="reset" value="重置" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
```

## 練習2:實現註冊頁面
