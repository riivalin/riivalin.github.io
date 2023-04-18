---
layout: post
title: "[CSS] 簡單的佈局"
date: 2011-02-08 00:03:11 +0800
categories: [Notes,CSS]
tags: [CSS]
---

- 一個盒子box就一個div，div跟div之間、div與網頁邊距之間的間距用`margin`。
- div裡面的內容跟div之間的間距，用`padding`來表示。
- `border`就是盒子div的邊框

![](/assets/img/post/div.png)

## Margin & Padding

- `*`星號代表所有網站
- `margin`div和網頁之間的間距，若寫4個值依序:上右下左(順時針)
- `padding`div和div裡面的內容之間的間距

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Margin和Padding</title>
    <style type="text/css">
        * { /* 星號代表所有網站*/
            margin: 0px; /*若寫4個值依序:上右下左(順時針)*/
        }
        .div1 {
            width: 200px;
            height: 300px;
            background-color: red;
            padding: 10px;
        }
        .p1 {
            width: 200px;
            height: 100px;
            background-color: yellow;
        }
    </style>
</head>
<body>
    <div class="div1"><p class="p1">我是P標籤</p></div>
</body>
</html>
```
> 
```
CSS選擇器 = class選擇器 = 類選擇器 (用「.」點來表示)

二種寫法：
1. 標籤.Class值 `div.p1` (最好加上標籤比較清楚)
2. .Class值 `.p1`
```

## Layout佈局

先把框架拉好了，再填內容  

- 盒子與盒子之間的距離用`margin`
- `margin: 0px auto;`margin寫2個值代表「上下|左右」：`上下0 | 左右auto`
- `float` 浮動，依據文檔流，兩個div左右放，並不會水平在一起，所以用飄浮float把他飄到左右邊去

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Layout佈局</title>
    <style type="text/css">
        .divIndex {
            width:900px;
            height:800px;
            background-color:yellow;
            margin: 0px auto; /*margin寫2個值代表：上下0| 左右auto*/
        }
        .divLogo {
            width: 900px;
            height:100px;
            background-color:red;
        }
        .divContent {
            width: 900px;
            height:300px;
            background-color:blue;
        }
        .divPicture {
            height:300px;
            width:300px;
            background-color:pink;
            float:left;
        }
        .divText {
            height:300px;
            width:600px;
            background-color:green;
            float:right; /*依據文檔流，它不會在右邊，所以用飄浮float把他飄到右邊去*/
        }
    </style>
</head>
<body>
    <div class="divIndex">
        <!--Logo-->
        <div class="divLogo">
            <img src="img/1.jpg" width="900" height="100"/>
        </div>
        <!--Content-->
        <div class="divContent">
            <div class="divPicture"></div>
            <div class="divText"></div>
        </div>
    </div>
</body>
</html>
```