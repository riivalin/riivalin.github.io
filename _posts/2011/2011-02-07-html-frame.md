---
layout: post
title: "[HTML] Frame 框架標籤"
date: 2011-02-07 00:18:07 +0800
categories: [Notes,HTML]
tags: [HTML]
---

- right.html
- left.html
- index.html

---
## 分割頁面frameset/frame

index.html
- 上下分`rows`
- 左右分`clos`
- 固定分割線`noresize`

```html
<html>
	<head>
	  <title>Index</title>
	</head>
	<frameset cols="30%,*"><!--左右分割-->
	  <frame src="right.html" noresize="noresize"/><!--固定分割線不讓移動-->
	  <frame src="left.html">
	</frameset>
</html>
```
一個頁面只能"上下"或是"右右"分，那如果我要"上左右"三個區塊怎辦？    
先"上下"分完，下面的區塊再"左右"分

## 分割"上左右"三個區塊

```html
<html>
	<head>
	  <title>Index</title>
	</head>
	<frameset rows="15%,*"><!--先上下分-->
	  <frame src="top.html" noresize="noresize"/>
	  <frameset cols="30%,*"><!--下面的區塊，再左右分-->
	    <frame src="left.html" noresize="noresize"/>
		<frame src="right.html"/>
	  </frameset>
	</frameset>
</html>
```

## 點擊左邊連結，右邊顯示網頁

- 右邊的frame(right.html)要設內部屬性name變數值 =>`name=right`

### index.html
```html
<html>
	<head>
	  <title>Index</title>
	</head>
	<frameset rows="15%,*"><!--先上下分-->
	  <frame src="top.html" noresize="noresize"/>
	  <frameset cols="30%,*"><!--再左右分-->
	    <frame src="left.html" noresize="noresize"/>
		<frame src="right.html" name="right"/><!--右邊frame的name=right -->
	  </frameset>
	</frameset>
</html>
```

### left.html

```html
<html>
	<head>
	  <title>I'm Left.</title>
	</head>
	<body bgcolor="pink">
		<a href="http://tw.bing.com/" target="_blank">Bing 1</a><!--_blank開新一個頁面顯示-->
		<a href="http://tw.bing.com/" target="_self">Bing 2</a><!--_self在當前的頁面顯示-->
		<a href="http://tw.bing.com/" target="right">Bing 3</a><!--右邊顯示，右邊frame要設name=right -->
	</body>
</html>
```
### right.html

```html
<html>
	<head>
	  <title>Right</title>
	</head>
	<body bgcolor="green">
		I'm Right.
	</body>
</html>
```
### top.html
```html
<html>
	<head>
	  <title>Top</title>
	</head>
	<body bgcolor="yellow">
		I'm Top.
	</body>
</html>
```


[https://www.bilibili.com/video/BV1vG411A7my?p=20](https://www.bilibili.com/video/BV1vG411A7my?p=20)