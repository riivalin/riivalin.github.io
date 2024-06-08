---
layout: post
title: 不蒜子極簡網頁計數器
date: 2023-03-01 01:08:09 +0800
categories: [Notes,Jekyll]
tags: [Jekyll Blog,網頁計數器]
---

不蒜子- 极简网页计数器 [https://busuanzi.ibruce.info](https://busuanzi.ibruce.info)

兩行代碼 搞定計數
```html
<script async src="//busuanzi.ibruce.info/busuanzi/2.3/busuanzi.pure.mini.js"></script>
<span id="busuanzi_container_site_pv">本站总访问量<span id="busuanzi_value_site_pv"></span>次</span>
```

```html
<!-- 不蒜子計數 -->
<script async src="//busuanzi.ibruce.info/busuanzi/2.3/busuanzi.pure.mini.js"></script>
<span id="busuanzi_container_site_pv">本站总访问量<span id="busuanzi_value_site_pv"></span>次</span>
<span id="busuanzi_container_site_uv">本站总访客数<span id="busuanzi_value_site_uv"></span>人</span>
<span id="busuanzi_container_page_pv">本文总阅读量<span id="busuanzi_value_page_pv"></span>次</span>
<!-- 不蒜子計數 -->
```

記錄一下…我放置在這兩處： 
```text 
_includes/footer.html  
_layout/post.html
```



- [不蒜子](http://ibruce.info/2015/04/04/busuanzi/)
- [使用不蒜子添加访客统计](https://blog.mikelyou.com/2020/08/18/busuanzi-visitor-counts-and-sitetime/)
- [给文章加上了访问统计](http://blog.tangyuewei.com/posts/%E7%BB%99%E6%96%87%E7%AB%A0%E5%8A%A0%E4%B8%8A%E4%BA%86%E8%AE%BF%E9%97%AE%E7%BB%9F%E8%AE%A1/)
- [使用不蒜子增加Jekyll博客访问量统计](https://jueee.github.io/2020/07/2020-07-09-使用不蒜子增加Jekyll博客访问量统计/)

