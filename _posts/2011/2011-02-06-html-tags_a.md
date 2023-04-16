---
layout: post
title: "[Html] a 標籤的使用"
date: 2011-02-06 00:09:07 +0800
categories: [Notes,Html]
tags: [Html]
---

##  a 標籤的使用

- `a`標籤：超連結
  - `href`表示要連結到的地址 
  - `target`打開新網頁的方式
  - `_blank`表示打開一個新的網頁進行跳轉 
  - `_self`表示在當前頁面進行跳轉
  
```html
<a href="http://google.com" target="_self">Google</a>
```
   
- 實現頁面內部的跳轉`<a name="bottom" href="#top">`  
  
href屬性寫法：`#`+`name的值`

```html
<a name="top" href="#bottom">跳到最下面</a>
<a name="bottom" href="#top">跳到最上面</a>
```
- 實現頁面間的跳轉`<a name="a.html" href="b.html#bbb">`  
  
href屬性寫法：路徑+`#`+`name的值`

```html
<a name="a" href="b.html#b">a網頁 跳到 b網頁</a>
<a name="b" href="a.html#a">a網頁 跳到 b網頁</a>
```

- 發送Email 

```html
<a href="mailto:rivalintw@gmail.com">發mail給Riva</a>
```
- 連結到一張圖片 

`href`屬性給圖片路徑
    
一個圖片標籤，跳到另一個地方顯示圖片(連結指向另一個圖片地址)

```html
<a href="1.jpg">連到一張圖片</a>
```
- 想要連結，但又不想連到任何地方
   
`href`屬性加`#`字號就可以了

```html
<a href="#"></a>
```
   
---
   
## 實現頁面內部的跳轉
## 回到頂端 & 回到底端

1. 先為`a`標籤加`name`屬性，取個名字`name="值"` 
    - 頂端`name="top"`
    - 底端`name="bottom"`  

2. 想從底端回到最上面的頂端，就在底端的`a`標籤加上`href`屬性，內容值怎麼寫呢？加上`#`和頂端`name的值` ==> `href="#top"`。
- 頂端`<a name="top" href="#bottom">Go to bottom</a>`
- 底端`<a name="bottom" href="#top">Go to Top</a>` 

```html
<html>
  <head>
    <title>a標籤的使用</title>
  </head>
  <body>
    <!-- target打開新網頁的方式 _blank開新網頁跳連 _self在當前網網頁跳轉-->
    <a href="http://google.com" target="_self">Google</a>
	
	<br/>
    <!--回到頂端 & 回到底端 (頁面內部的跳轉) -->
	<a name="頂端" href="#底端">回到底端 Bottom</a>
	<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
	<a name="底端" href="#頂端">回到頂端 Top</a>
  </body>
</html>
```
## 實現頁面間的跳轉
## a網頁跳轉b網頁

`<a name="a" href="b.html#b">`  
`href`屬性連結的地址內容，先寫`路徑`再加上`#`字號和`name的值`。
      
例如我a.html要跳轉到b.html，先在b.html網頁的a標籤加上name屬性給他一個名字`<a name="快樂鼠">`, 在a.html的`href`屬性加上`#`和b.html的name值，然後怎麼跳轉？在`#名字`前面再加上路徑`<href="b.html#快樂鼠">`。
      
反之，如果想要再跳回原本的頁面，也是如此。     

a.html
```html
<a name="test" href="b.html#快樂鼠">走去看快樂鼠GOGOG~~~</a>  
```
b.html
```html
<a name="快樂鼠" href="a.html#test">快樂鼠在這裡</a>
```

## 練習：兩網頁可以相互跳轉
test.html

```html
<html>
  <head>
    <title>a標籤的使用</title>
  </head>
  <body>
	
    <!-- target開啟新網頁的方式 _blank開新網頁跳連 _self在當前網網頁跳轉-->
    <a href="http://google.com" target="_self">Google</a>
	
	<br/>

	<!--頁面間的跳轉(a.html 跳轉到 b.html)-->
	<a name="test" href="快樂鼠.html#快樂鼠">走去看快樂鼠GOGOG~~~</a>  
	  
	<!--頁面內部的跳轉(回到頂端 & 回到底端)-->
	<br/>
	<a name="頂端" href="#底端">回到底端 Bottom</a>
	<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
	<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>

	<a name="底端" href="#頂端">回到頂端 Top</a>
  </body>
</html>
```

快樂鼠.html

```html
<html>
    <head>
        <title>快樂鼠</title>
    </head>
    <body>
    <!--頁面間的跳轉(b.html 跳轉到回 a.html)-->
    <a name="快樂鼠" href="test.html#test">快樂鼠在這裡</a>
    <img src="1.jpg" />
    </body>
</html>
```

## 發送Email
```html
<a href="mailto:rivalintw@gmail.com">發送Email給Riva</a>
```
## HTML超連結兩種使用方式
### 連結到另一個文檔
```html
<a href="Test.htm">跳轉到指定的網頁</a>
```
### 跳轉指定的地方
```html
<a name="上面" href="#下面">跳到下面</a>
<a name="下面" href="#上面">跳到上面</a>
```
### 一個圖片標籤，跳到另一個地方顯示圖片(連結指向另一個圖片地址)
```html
<a href="1.jpg">快樂鼠真可愛</a>
```
## 想有連結效果，但又不想連到任何地方
`href`連結地址寫`#`就可以了，如果沒有`href`屬性，就變成純文字`<a>這不是連結</a>`
```html
<a href="#">這是連結嗎？</a>
```