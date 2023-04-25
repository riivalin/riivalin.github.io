---
layout: post
title: "[CSS] CSS練習"
date: 2011-02-08 00:01:51 +0800
categories: [Notes,CSS]
tags: [CSS]
---

大部分網站當中，你看到的每一塊東西，基本上都是`div`。

## 練習
![](/assets/img/post/css-practics.png)
  
Steps:
- 先放一個大div，裡面放一個table
- table的第一行，放一個div當標題
- table的第二行，放一個table呈現內容
- 使用link連結css檔(寫好的css另外存一個檔案)  
`<link type="text/css" rel="stylesheet" href="Test.css"/>`

### HTML碼
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
    <link type="text/css" rel="stylesheet" href="Test.css"/>
</head>
<body>
    <div class="divIndex">
        <table>
            <tr>
                <td><div class="div1"><p>.Net培訓開班訊息</p></div></td>
            </tr>
            <tr>
                <td>
                    <table id="divTable">
                        <tr>
                            <td colspan="2" class="td1">.Net基礎班</td>
                        </tr>
                        <tr>
                            <td class="td2">台北-2011/07/22</td>
                            <td class="td3">預約報名中</td>
                        </tr>
                        <tr>
                            <td class="td2">台北-2011/3/22</td>
                            <td class="td4">爆滿已開班</td>
                        </tr>
                        <tr>
                            <td class="td2">台中-2011/06/22</td>
                            <td class="td3">預約報名中</td>
                        </tr>
                        <tr>
                            <td class="td2">高雄-2011/5/22</td>
                            <td class="td4">爆滿已開班</td>
                        </tr>
                        <tr>
                            <td colspan="2" class="td1">.Net就業班</td>
                        </tr>
                        <tr>
                            <td class="td2">台北-2011/07/22</td>
                            <td class="td3">預約報名中</td>
                        </tr>
                        <tr>
                            <td class="td2">台北-2011/3/22</td>
                            <td class="td4">爆滿已開班</td>
                        </tr>
                        <tr>
                            <td class="td2">台中-2011/06/22</td>
                            <td class="td3">預約報名中</td>
                        </tr>
                        <tr>
                            <td class="td2">高雄-2011/5/22</td>
                            <td class="td4">爆滿已開班</td>
                        </tr>
                        <tr>
                            <td colspan="2" class="td1">.Net遠端班</td>
                        </tr>
                        <tr>
                            <td class="td2">台北-2011/04/12</td>
                            <td class="td3">基礎班預約報名中</td>
                        </tr>
                        <tr>
                            <td class="td2">台北-2011/5/22</td>
                            <td class="td3">就業班預約報名中</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
```
### CSS樣式檔

Test.css

```css
* { /* 星號代表所有的元素*/
    margin: 0px; /*設置四周無間距*/
    font-size: 12px;
}

div.divIndex { /* class選擇器*/
    width: 290px;
    height: 380px;
    background-color: #F8F8F8;
    /*float: right;*/ /*讓div飄到右邊*/
    margin: 0px auto; /* 上邊下邊 | 左邊右邊 */
}

div.div1 {
    width: 290px;
    height: 35px;
    background-color: blue;
    color: white;
    text-align: center;
}

div.div1 p {
    padding: 8px; /*文字與周邊的間距*/
    font-size: 18px;
}

#divTable { /*id選擇器*/
    width: 280px;
    border: 1px;
}

td.td1 {
    font-weight: bolder; /*字體加粗*/
}

td.td2 {
    color: #246DB2;
    padding: 2px;
    border-bottom: 1px dashed #cccccc; /*虛線*/
}

td.td3 {
    font-weight: bolder;
    color: red;
    border-bottom: 1px dashed #cccccc;
    text-align: right;
}

td.td4 {
    font-weight: bolder;
    border-bottom: 1px dashed #cccccc;
    text-align: right;
}
```