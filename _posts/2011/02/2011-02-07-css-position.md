---
layout: post
title: "[CSS] position 位置"
date: 2011-02-07 00:30:27 +0800
categories: [Notes,CSS]
tags: [CSS]
---

## 文檔流的概念
我們在這個網頁當中，都是遵循從上到下排列元素，沒有重合的，但我今天想要實現這個效果，重疊在一起，怎麼做呢？

- `position:absolute;`絕對定位，定到哪就是哪
- `z-index:3;`數字愈大，顯示愈上層

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>文檔流</title>
    <style type="text/css">
        div {
            height: 300px;
            width: 300px;
        }
        div.div1 {
            background-color: red;
            top:100px;
            left:100px;
            position:absolute; /*絕對定位，定到哪就是哪*/
            z-index:3; /*數字愈大顯示愈上面*/
        }
        div.div2 {
            background-color: yellow;
            top: 130px;
            left: 130px;
            position: absolute; /*絕對定位，定到哪就是哪*/
            z-index:2;
        }
        div.div3 {
            background-color: green;
            top: 160px;
            left: 160px;
            position: absolute; /*絕對定位，定到哪就是哪*/
            z-index:1;
        }
    </style>
</head>
<body>
    <div class="div1"></div>
    <div class="div2"></div>
    <div class="div3"></div>
</body>
</html>
```

## 位置

- `position: fixed;`固定定位

### 範例：讓區塊始終在右下角(無論網頁多長)
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
    <style type="text/css">
        div.div1 {
            right: 0px;
            bottom: 0px;
            position: fixed; /*固定定位*/
            background-color: yellow;
            height: 300px;
            width: 300px;
        }
    </style>
</head>
<body>
    <div class="div1"></div>
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
</body>
</html>
```
