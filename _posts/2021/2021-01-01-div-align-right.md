---
layout: post
title: "[CSS] Right Align Div CSS"
date: 2021-01-01 10:00:00 +0800
categories: [Notes, CSS]
tags: [CSS, Div, CSS float]
---

[Problem Description]

I have some HTML:
```html
<div style="text-align: right;">
This is some text in a div element!
</div>
```
HTML div align right not working?

[Issue Solved]

`"float:right;"` worked great! 

```html
<div style="float:right;">
This is some text in a div element!
</div>
```


[https://stackoverflow.com/questions/73861223/right-align-div-css](https://stackoverflow.com/questions/73861223/right-align-div-css)
