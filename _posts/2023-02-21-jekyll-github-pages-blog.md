---
layout: post
title: "[MacOS] 使用Jekyll + Github Pages 建立免費Blog"
date: 2023-02-21 00:10:00 +0800
categories: [Notes, MacOS, Jekyll]
tags: [Notes, MacOS, Jekyll Blog, Github Pages]
---

Terminal:

```shell
gem install jekyll bundler //安裝jekyll
jekyll new riivalin.github.io //建立新的blog架構
cd riivalin.github.io //切换目錄到本地 github pages 的資料夾
bundle exec jekyll serve //在本地啟動 Jekyll 服務
```
Server address: [http://127.0.0.1:4000/](http://127.0.0.1:4000/)


Reference: [Jekyll Docs](https://jekyllrb.com/docs/)