---
layout: post
title: "[C# 筆記] ArrayList 常用方法 Add()和 Insert() 有何不同？"
date: 2021-03-19 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,arraylist]
---

二者均可以將物件新增到  `ArrayList` 集合中，二者主要不同點在於：
        
`Insert()` 可以指定索引位置，而且優先權較高。
就算前面幾個物件都用`Add()`來新增，     
但只要`Insert()`索引指定為`0`便會將元素插入索引位置，       
而 `Add()`只能隨後附加上去。

- `Insert()`新增物件到指定的 `ArrayList` 的索引位置。
- `Add()`將物件加入 `ArrayList`的末端(最後)。

       
Book: Visual C# 2005 建構資訊系統實戰經典教本