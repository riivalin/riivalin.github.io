---
layout: post
title: "[MacOS] 使用Jekyll + Github Pages 建立免費Blog"
date: 2023-02-21 00:10:00 +0800
categories: [Notes, Blog]
tags: [Jekyll Blog, Github Pages]
---

Open terminal:

```shell
gem install jekyll bundler //安裝jekyll
jekyll new riivalin.github.io //建立新的 jekyll blog 架構 (jekyll new myblog)
cd riivalin.github.io //切换目錄到本地 github pages 的資料夾 (cd myblog)
bundle exec jekyll s //在本地啟動 Jekyll 服務 (bundle exec jekyll serve)
```
Server address: [http://127.0.0.1:4000/](http://127.0.0.1:4000/)


Reference: [Jekyll Docs](https://jekyllrb.com/docs/)