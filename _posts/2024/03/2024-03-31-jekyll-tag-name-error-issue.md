---
layout: post
title: "[Jekyll] ERROR: Invalid first code point of tag name U+57FA"
date: 2024-03-31 06:23:00 +0800
categories: [Notes,Blog,Jekyll]
tags: [Jekyll,error,issue,markdown]
---


## 錯誤訊息：

57:1196: ERROR: Invalid first code point of tag name U+57FA.

## 錯誤原因：

內容有這個`<`符號，被JS視為HTML而產生錯誤。

原因是：HTML標籤中 特殊字元 如大於小於號的寫法要用 原始碼 也就是轉義字符        

## 解決方式：

方法一：可以用`&lt;`取代`<`符號
方法二： 或是加上 「`\`(反斜線)」轉義符號 => `\<`
