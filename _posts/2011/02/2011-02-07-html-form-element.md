---
layout: post
title: "[HTML] form 表單中的元素"
date: 2011-02-07 00:09:01 +0800
categories: [Notes,HTML]
tags: [HTML]
---

## 為什麼要使用表單
用於向服務器傳數據

## HTML表單
- HTML表單都是用於搜集用戶輸入的
- HTML表單都在一對`form`標籤中
- `<form>`的常用屬性
    - `action`表示提交的目標服務器
    - `method`提交的方法`get` `post`
    - `get`默認，以url提交，就是以地址欄的方式提交
    - `post`通過報文提交

## input標籤
- `Text`單行文本輸入框
- `Password`密碼框
- `TextArea`多行文本框
- `Raio`單選
- `CheckBox`複選
- `Reset`重置按鈕
- `Select`下拉列表
- `Submit`提交按鈕

## form輸入用戶+密碼後，提交到Google
- 服務器需要透過內部屬性`name`取得資料
    
屬性 Name="資料欄位名稱"指示為必要設定值，提供給後端程式處理的變數值 (取名稱時最好與欄位有關)

```html
<!--action提交的目標服務器, get提交的方式以地址欄-->
<form action="https://www.google" method="get">
<!--服務器需要透過內部屬性name取得資料-->
用戶名：<input type="text" name="txtName"/><br/> 
密碼：<input tye="password" name="txtPwd"/><br/>
<input type="submit" value="提交到google"/><!--value改變按鈕內的文字-->
<input type="reset" value="清空"/>
</form>
```
`method="get"`內容會在URL顯示：  
https://www.google/?txtName=test&txtPwd=1234


## radio性別&婚姻狀況(fieldset分組)
```html
<fieldset><!--分組效果(外框框起來)-->
<legend>性別</legend><!--標題-->
<input type="radio" name="sex"/>男<br/><!--單選，所以name值設一樣-->
<input type="radio" name="sex"/>女<br/>
</fieldset>
<fieldset>
<legend>婚姻狀況</legend>
<input type="radio" name="married"/>已婚<br/>
<input type="radio" name="married"/>未婚<br/>
</fieldset>
```
- `<FieldSet> ... </FieldSet>`美化表單的外框而已。
- `<Legend> ... </Legend>`標題文字
- 屬性 Name="資料欄位名稱"為必要設定值，提供給後端程式處理的變數值 (取名稱時最好與欄位有關)。

## select下拉選項
`<select><option>...<option></select>`

- select下拉選單，option選項，size顯示幾個option選項
- `<select size="1">`顯示1個option選項，size="4"顯示4個

```html
<!--select下拉選單，option選項，size顯示幾個option選項-->
<select size="1"><!--顯示1個option-->
    <option>台北</option>
    <option>台中</option>
    <option>台南</option>
    <option>高雄</option>
</select>
```

## select-optgroup下拉式選單內的群組

`<select><optgroup><option>...<option></optgroup></select>`

- `<optgroup>`分組

```html
<!--select下拉群組選單，optgroup群組，option選項，size顯示幾個option選項-->
<select size="1"><!--顯示1個option-->
<optgroup label="台北市"><!--選單內的分組/群組-->
    <option>大安區</option>
    <option>文山區</option>
    <option>內湖區</option>
</optgroup>
<optgroup label="新北市">
    <option>中和</option>
    <option>永和</option>
    <option>新店</option>
</optgroup>
</select>
```

## File選擇檔案

選擇檔案(靜態)

```html
<input type="file"/>
```
## TextArea多行文本框
通常用在合同上，看完後，最後讓你勾選我同意…
```html
<textarea cols="100" rows="20">
    <!--TODO 內容-->
</textarea>   
```

## 完整HTML碼
```html
<html>
  <head>
    <title></title>
  </head>
  <body>
  <!--action提交的目標服務器, get提交的方式以地址欄-->
    <form action="https://www.google" method="get">
	<!--服務器需要透過內部屬性name取得資料-->
	 用戶名：<input type="text" name="txtName"/><br/> 
	 密碼：<input tye="password" name="txtPwd"/><br/>
	 <input type="submit" value="提交到google"/>
	 <input type="reset" value="清空"/>
	 
	 <fieldset><!--分組效果(外框框起來)-->
	 <legend>性別</legend><!--標題-->
	 <input type="radio" name="sex"/>男<br/>
	 <input type="radio" name="sex"/>女<br/>
	 </fieldset>
	 <fieldset>
	 <legend>婚姻狀況</legend>
	 <input type="radio" name="married"/>已婚<br/>
	 <input type="radio" name="married"/>未婚<br/>
	 </fieldset>
	 
	 <!--select下拉選單，option選項，size顯示幾個option選項-->
	<select size="1"><!--顯示1個option-->
		<option>台北</option>
		<option>台中</option>
		<option>台南</option>
		<option>高雄</option>
	</select>
	 
     <!--select下拉群組選單，optgroup群組，option選項，size顯示幾個option選項-->
	 <select size="1"><!--顯示1個option-->
		<optgroup label="台北市"><!--分組/群組-->
		  <option>大安區</option>
		  <option>文山區</option>
		  <option>內湖區</option>
		</optgroup>
		<optgroup label="新北市">
		  <option>中和</option>
		  <option>永和</option>
		  <option>新店</option>
		</optgroup>
	 </select>
	 <br/>
	 
     <!--選擇檔案-->
	 <input type="file"/><br/>
	 <!--通常用在合同上，看完後，最後讓你勾選我同意…-->
	 <textarea cols="100" rows="20">
	 房屋租賃契約
出 租 人： (以下簡稱甲方)
立契約書人 承 租 人： (以下簡稱乙方)
連帶保證人： (以下簡稱丙方)
因房屋租賃事件，訂立本契約，雙方同意之條件如下：
第一條：房屋所在地及使用範圍：
第二條：租賃期限：自民國 年 月 日起至 年 月 日止，共計 年。
第三條：租金：1、每月租金新台幣 元整，每月 日以前繳納。
2、保證金新台幣 元整，於租賃期滿交還房屋時無息返還。
第四條：使用租賃物之限制：
1、本房屋係供 住家 /營業 之用。
2、未經甲方同意，乙方不得將房屋全部或一部轉租、出借、頂讓，或以其
他變相方法由他人使用房屋。
3、乙方於租賃期滿應即將房屋遷讓交還，不得向甲方請求遷移或任何費用。
4、房屋不得供非法使用，或存放危險物品影響公共安全。
5、房屋有改裝之必要，乙方取得甲方之同意後得自行裝設，但不得損害原
有建築，乙方於交還房屋時並應負責回復原狀。
第五條：危險負擔：乙方應以善良管理人之注意使用房屋，除因天災地變等不可抗
拒之情形外，因乙方之過失致房屋毀損，應負損害賠償之責。房屋因自然
之損壞有修繕必要時，由甲方負責修理。
第六條：違約處罰：
1、乙方違反約定方法使用房屋，或拖欠租金達兩個月以上，其租金約定於
每期開始支付者，並應於遲延給付二個月時，經甲方催告限期繳納仍不
支付時，不待期限屆滿，甲方得終止租約。
2、乙方於終止租約或租賃期滿不交還房屋，自終止租約或租賃期滿之翌日
起，乙方應支付按房租壹倍計算之違約金。
第七條：其他特約事項：
1、房屋之捐稅由甲方負擔；有關水電費、瓦斯費、大樓管理費及營業必須
繳納之捐稅，則由乙方自行負擔。
2、乙方遷出時，如遺留傢具雜物不搬者，視為放棄，應由甲方處理，費用
由乙方負擔。
3、雙方如覓有保證人，與被保證人負連帶保證責任。
4、契約租賃期限未滿，一方擬終止本合約時，應得他方同意，並應預先於
終止前壹個月以書面通知他方，並應賠償他方相當於壹個月租金額之損
害金。
5、甲、乙雙方就本合約有關履約事項之通知、催告送達或為任何意思表示，
均以本合約所載之地址為準，若有送達不到或退件者、悉以第一次郵寄
日期為合法送達日期，雙方均無異議。
6、乙方如將公司登記(或個人戶籍)遷入本租屋地址者，應於本租約屆滿時
自動遷出，否則，甲方得向主管機關申報其為空戶。
7、乙方應遵守本件租屋之住戶大樓管理規約及管理委員會之一切決議。
第八條：應受強制執行之事項：詳如公證書所載。
立契約書人
出 租 人：
身分證統一編號：
地 址：
承 租 人：
身分證統一編號：
地 址：
連 帶 保 證 人：
身分證統一編號：
地 址：
中 華 民 國 年 月 日
	 </textarea>
	 
	</form>
  </body>
</html>
```