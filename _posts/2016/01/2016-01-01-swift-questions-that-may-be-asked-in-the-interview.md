---
layout: post
title: "[Swift] Questions that may be asked in the interview. (Draft)"
date: 2016-01-01 06:30:00 +0800
categories: [Notes,Swift]
tags: [SwiftUI]
---


# 1. 如何做一個可回傳 Error的functionfunc numberToInt(number: Int) ->{...}

Ans:  用 `throws` 去丟 errorfunc    

```swift
numberToInt(number: Int) throws -> { ... }
```

# 2. struct vs class 差別?

Ans:        
- `struct`: `call by value`，存的是內容。實體在本地     
- `class`: `call by reference`，存的是地址。實體在遠端  

## 測試

![](/assets/img/post/swift-interview-struct-cat.png)


# 3. 如果已經有一長方型的長、寬，要如何宣告一個面積並且取得它的面積     

Ans:

![](/assets/img/post/swift-interview-calc-area-1.png)

![](/assets/img/post/swift-interview-calc-area-2.png)


