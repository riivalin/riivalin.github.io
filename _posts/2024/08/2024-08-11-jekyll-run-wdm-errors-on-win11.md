---
layout: post
title: "[Jekyll] WDM errors on Win11"
date: 2024-08-11 06:23:00 +0800
categories: [Notes,Jekyll,WDM,wdm-errors]
tags: [jekyll,wmd-errors]
---

## 錯誤訊息

錯誤：無法建置 gem 本機擴展
```
error occurred while installing wdm (0.1.1), and Bundler cannot continue
```

## 解決方法

這是透過反覆試驗發現的解決方案。
        
在 jekyll 目錄中名為「gemfile」的檔案中，有以下行：

```yml
gem "wdm", "~> 0.1.1", :platforms => [:mingw, :x64_mingw, :mswin]
```

刪除此行即可解決問題

所以這與 WDM「監視」服務有關。但這是不必要的，所以沒有它它也能工作。

文件建議替換一條稍微不同的線，我認為這是為了做同樣的事情。它也不起作用。        


<https://talk.jekyllrb.com/t/newbie-problems-with-wdm-errors/9233/4>