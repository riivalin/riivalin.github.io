---
layout: post
title: "[CSS] Right Align Div CSS"
date: 1999-01-01 04:00:00 +0800
categories: [Notes,CSS]
tags: [CSS]
---

### [Problem Description]

I have some HTML:
```html
<div style="text-align: right;">
This is some text in a div element!
</div>
```
HTML div align right not working?

### [Issue Solved]

`"float:right;"` worked great!  
"sapn" also works.

```html
<div style="float:right;">
This is some text in a div element!
</div>
```


[right-align-div-css](https://stackoverflow.com/questions/73861223/right-align-div-css)  
[text-align-right-in-a-span](https://stackoverflow.com/questions/40427457/text-align-right-in-a-span)
