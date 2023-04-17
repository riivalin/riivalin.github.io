---
layout: post
title: "[Html] 跨行和跨列的表格"
date: 2011-02-07 00:03:01 +0800
categories: [Notes,Html]
tags: [Html]
---

## HTML表格
### 為什麼要使用表格
在HTML文中，廣泛使用表格來存放諸頁上的文本和圖像進行佈局。

## HTML表格-語法  
```html
<table border="1">
    <tr><!--行-->
        <td>單元格內容</td>
    </tr>
</table>
```
- 橫向"行"row
- 縱向"列"col

## 練習:二行三列表格+超連結+圖片+設border  
|              |              |              |
|:-------------|:-------------|-------------:|
| 1行1列的單元格 | 1行2列的單元格 | 1行3列的單元格 |
| 2行1列的單元格 | 2行2列的單元格 | 2行3列的單元格 |

- 表格中的文字也可以添加超連結
- 表格中的文字也可以改變顏色、設置編號
- 試試把`table`的`border`屬性刪掉會怎樣？
- 建一個一行兩列的表格，左邊放一個圖片，右邊能放多行文本嗎？


```html
<html>
  <head>
    <title></title>
  </head>
  <body>
  <!--二行三列表格-->
  <table border="1px" cellspacing="0px" cellpadding="5px">
	  <tr><!--行-->
		<td><a href="#">超連結</a></td>
		<td>星期一</td> 
		<td>哈哈哈</td>
	  </tr>
	  <tr>
		<td>嗚嗚嗚</td>
		<td>哇哇哇</td> 
		<td><img src="1.jpg" height="50px" width="80px"/></td>
	  </tr>
  </table>
</html>
```

- `cellspacing="0px"`邊框無空隙
- `cellpadding="0px"`單元格周邊無空隙

```html
<!--table邊框無空隙, 單元格周邊無空隙-->
<table border="1px" cellspacing="0px" cellpadding="0px"></table>
<!--table空心邊框(有5px空隙), 單元格周邊有5px空隙-->
<table border="1px" cellspacing="5px" cellpadding="5px"></table>
```

## 跨行和跨列的表格
什麼是跨行和跨列的表格，使用`colspan`和`rowspan`屬性。
- 左右格子合併為跨列合併(col span)，設定colpan來達成。
- 上下格子合併為跨行合併(row span)，設定rowspan來達成。

     
- 縱向"列"col
- 橫向"行"row


### 練習:跨列的表格
使用`colspan`來實現
    
- 左右格子合併為跨列合併(col span)，設定colpan來達成。

```html
<table border="1px" cellspacing="0px" cellpadding="1px">
    <tr>
        <td colspan="2">學生成績</td>
    </tr>
    <tr>
        <td>國文</td>
        <td>99</td>
    </tr>
    <tr>
        <td>數學</td>
        <td>100</td>
    </tr>
</table>
```

### 練習:跨行的表格
使用`rowspan`來實現
    
上下格子合併為跨列合併(row span)，設定rowspan來達成。 

```html
<table border="1px" cellspacing="0px" cellpadding="10px" width="180px" height="120px">
    <tr>
        <td rowspan="2">張三</td>
        <td>國文</td>
        <td>80</td>
    </tr>
    <tr>
        <td>數學</td>
        <td>100</td>
    </td>
    <tr>
        <td rowspan="2">李四</td>
        <td>國文</td>
        <td>99</td>
    </tr>
    <tr>
        <td>數學</td>
        <td>89</td>
    </tr>
</table>
```

## 完整HTML碼

```html
<html>
  <head>
    <title></title>
  </head>
  <body>
  <!--二行三列表格-->
  <table  border="1px" cellspacing="0px" cellpadding="5px">
	  <tr><!--行-->
		<td><a href="#">超連結</a></td>
		<td>星期一</td> 
		<td>哈哈哈</td>
	  </tr>
	  <tr>
		<td>嗚嗚嗚</td>
		<td>哇哇哇</td> 
		<td><img src="1.jpg" height="50px" width="80px"/></td>
	  </tr>
  </table>
  <hr/>
   <!-- 跨列的表格:使用`colspan`來實現-->
  <table border="1px" cellspacing="0px" cellpadding="1px">
	<tr>
	  <td colspan="2">學生成績</td>
	</tr>
	<tr>
	  <td>國文</td>
	  <td>99</td>
    </tr>
	<tr>
	  <td>數學</td>
	  <td>100</td>
	</tr>
  </table>
  <hr/>
   <!--跨行的表格:使用`rowspan`來實現 -->
   <table border="1px" cellspacing="0px" cellpadding="10px" width="180px" height="120px">
     <tr>
	   <td rowspan="2">張三</td>
	   <td>國文</td>
	   <td>80</td>
	 </tr>
	 <tr>
	   <td>數學</td>
	   <td>100</td>
	 </td>
	 <tr>
	   <td rowspan="2">李四</td>
	   <td>國文</td>
	   <td>99</td>
	 </tr>
	 <tr>
	   <td>數學</td>
	   <td>89</td>
	 </tr>
   </table>
</html>
```