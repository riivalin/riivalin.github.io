---
layout: post
title: "CSS 中的選擇器 Selector"
date: 2011-02-07 00:24:17 +0800
categories: [Notes,CSS]
tags: [CSS]
---

## HTML Selector(選擇器)
你寫哪個標籤，就顯示哪個標籤的樣式
    
就是嵌入樣式表(需要在head標籤內寫`<style type="text/css"></style>`)

HTML選擇器=嵌入樣式表
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>HTML選擇器/嵌入樣式表</title>
    <!-- HTML選擇器(嵌入樣式表): 需要在head標籤內寫-->
    <style type="text/css">
        p {
            background-color: lightpink;
            font-size: x-large;
        }
        tt {
            background-color:greenyellow;
            font-size:xx-large;
        }
    </style>
</head>
<body>
    <p>今天天氣真好呀!</p>
    <p>今天天氣真好呀!</p>
    <p>今天天氣真好呀!</p>
    <tt>Hello，你好嗎？今天天氣真好呀!</tt>
</body>
</html>
```

## CSS Selector(class選擇器、類選擇器)
前面加`.`    

需要給設置樣式的元素的class屬性賦值。  
> CSS 選擇器用法：想要給某個標籤做呈現，就是HTML選擇器(嵌入樣式表)加上calss屬性值，標籤要設置class屬性值。  
    
- CSS選擇器: tag加上class屬性名  
    
- HTML選擇器=嵌入樣式表    
- CSS選擇器=HTML選擇器做法+tag加上class屬性名

CSS選擇器=Class選擇器=類選擇器
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>CSS選擇器/Class選擇器/類選擇器</title>
    <!--嵌入樣式表(HTML選擇器): 需要在head標籤內寫-->
    <style type="text/css">
        p {
            background-color: lightpink;
            font-size: x-large;
        }
        /* CSS選擇器: tt標籤加上class屬性名(tt標籤class屬性要賦值) */
        tt.tt1 {
            background-color: blue;
        }
        tt.tt2 {
            background-color: pink;
            font-size: xx-large;
        }       
        tt.tt3 {
            font-size: xx-small;
        }
    </style>
</head>
<body>
    <!-- tt標籤設置class屬性-->
    <tt class="tt1">Hello，你好嗎？今天天氣真好呀!</tt>
    <tt class="tt1">Hello，你好嗎？今天天氣真好呀!</tt>
    <tt class="tt2">Hello，你好嗎？今天天氣真好呀!</tt>
    <tt class="tt3">Hello，你好嗎？今天天氣真好呀!</tt> 
</body>
</html>
```

## ID Selector(選擇器)
前面加`#`    

ID 選擇器的用法跟CSS選擇器很像，CSS選擇器用`.`，ID選擇器前面是加`#`

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>ID 選擇器</title>
    <!--嵌入樣式表(HTML選擇器): 需要在head標籤內寫-->
    <style type="text/css">
        /*ID 選擇器的用法跟CSS選擇器很像，CSS選擇器用`.`，ID選擇器前面是加`#`*/
        #p1 {
            background-color:pink;
        }
        #p2 {
            font-size:xx-large;
        }
    </style>
</head>
<body>
    <p id="p1">今天天氣真好呀!</p>
    <p id="p2">今天天氣真好呀!</p>
</body>
</html>
```

## 什麼用ID選擇器(ID)？CSS選擇器(Class)？
- 單一的用`id`
- 同一類的用`class`
    
- 當頁面某些元素要顯示同一類樣式class
- id不管在前端、後端、db來說，對我們而言都是唯一的，所以，盡量不要給標籤都賦值同一個id
- 如果想要兩個標籤都要顯示同一個樣式，就用class

CSS選擇器=Class選擇器=類選擇器

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>ID選擇器、Class選擇器</title>
    <!--嵌入樣式表(HTML選擇器): 需要在head標籤內寫-->
    <style type="text/css">
        /*CSS選擇器(class)*/
        .p1 {
            background-color:green;
        }
        /*ID選擇器(id)的用法跟CSS選擇器很像，CSS選擇器用`.`，ID選擇器前面是加`#`*/
        #p2 {
            background-color:pink;
        }
        #p3 {
            font-size:xx-large;
        }
    </style>
 
</head>
<body>
    <!--如果想要兩個標籤都要顯示同一個樣式，就用class-->
    <p class="p1">今天天氣真好呀!</p>
    <p class="p1">今天天氣真好呀!</p>
    <p id="p2">今天天氣真好呀!</p>
    <p id="p3">今天天氣真好呀!</p>
</body>
</html>
```

## 關聯選擇器

如果我只想對這一行`<p><em>今天天氣真好呀!</em></p>`做呈現，但不能動到其他有`<p>` `<em>`的標籤，怎麼做？    

`p em { background-color:pink;}`

```html
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>關聯選擇器</title>
    <style type="text/css">
        p em {
            background-color:pink;
        }
    </style>
</head>
<body>
    <p><em>今天天氣真好呀!</em></p>
    <em>今天天氣真好呀!</em>
    <em>今天天氣真好呀!</em>
    <p>今天天氣真好呀!</p>
    <p>今天天氣真好呀!</p>
    <p>今天天氣真好呀!</p>
    <p>今天天氣真好呀!</p>
</body>
</html>
```

## 組合選擇器
逗號`,`連接  

`h1, h2, h3, h4, h5, h6, p, table { background-color:pink; }`

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
    <style type="text/css">
        /*組合選擇器*/
        h1, h2, h3, h4, h5, h6, p, table {
            background-color:pink;
        }
    </style>
</head>
<body>
    <h1>今天天氣真好呀!</h1>
    <h2>今天天氣真好呀!</h2>
    <h3>今天天氣真好呀!</h3>
    <h4>今天天氣真好呀!</h4>
    <h5>今天天氣真好呀!</h5>
    <h6>今天天氣真好呀!</h6>
    <p>今天天氣真好呀!</p>
    <table border="1" cellpadding="0" cellspacing="0">
        <tr>
            <td>BBB</td>
            <td>CCC</td>
            <td>DDD</td>
        </tr>
        <tr>
            <td>BBB</td>
            <td>CCC</td>
            <td>DDD</td>
        </tr>
        <tr>
            <td>BBB</td>
            <td>CCC</td>
            <td>DDD</td>
        </tr>
    </table>
</body>
</html>
```
## 偽元素選擇器
偽元素選擇器是指對同一個HTML元素的各種狀態和期所包括的部分內容的一種定義方式。    
例如，對於超連結`<a></a>`的正常狀態(沒有任何動作前)、點擊過的狀態、選中狀態、游標移到超連結文本上的狀態，對於段落的首字母和心行，都可以使用偽元素選擇器來定義。
    
- `a:active` 選中超連結時的狀態
- `a:hover`滑鼠移至連結時的狀態
- `a:link`未連結的正常狀態
- `a:visited`己連結過的
- `p::first-letter`段落中的第一個字母
- `p::first-line`段落中的第一行文本

```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
    <style type="text/css">
        a:active { /* 選中的連結 */
            text-decoration: none;
        }
        a:hover { /* 滑鼠移至連結 */
            font-size: x-large;
        }
        a:visited { /* 已連結過 */
            color: green;
        }
        a:link { /* 未連結 */
            font-size: xx-large;
        }
        p::first-letter {/*字首(第一個字)*/
            font-size:xx-small;
        }
        p::first-line { /*第一個段落*/
            font-size:xx-large;
        }
    </style>
</head>
<body>
    <p>今天天氣真好呀!<br/>今天天氣真好呀!</p>
    <a href="#">超連結</a>
    <a href="#">超連結</a>
    <a href="#">超連結</a>
    <a href="#">超連結</a>
    <a href="#">超連結</a>
    <a href="#">超連結</a>
</body>
</html>
```