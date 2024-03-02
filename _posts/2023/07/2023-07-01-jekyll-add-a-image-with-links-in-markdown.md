---
layout: post
title: "[Issue][Jekyll] How to add a image with links in markdown?"
date: 2023-07-01 21:59:00 +0800
categories: [Notes,Blog]
tags: [Jekyll,markdown,error,issue]
---


## 錯誤訊息
```
Htmlproofer Error        
internally linking to xxxxx, which does not exist...
```

## 解決方法

### 方法一：
增加 `{:target="_blank"}`

```markdown
![Alt text](https://example.com){:target="_blank"}
```

### 方法二：
以文字link方式中，放入Image連結的方式

```markdown
[![Alt text](https://example.com/image.png)](https://example.com)
```

---

Image:

```markdown
![Alt text for broken image link](assets/logo.png)
```

Link:

```markdown
[Alt text for broken link](httsp://example.com)
```

<https://talk.jekyllrb.com/t/how-to-add-a-image-with-links-in-markdown/5915/3>