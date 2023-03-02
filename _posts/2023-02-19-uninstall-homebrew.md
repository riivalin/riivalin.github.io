---
layout: post
title: "[MacOS] 卸載刪除 Homebrew"
date: 2023-02-19 00:10:00 +0800
categories: [Notes, Commands]
tags: [Homebrew, Commands]
---

將安裝 Homebrew 的指令 install 改為 uninstall 就可以了。
```
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/uninstall.sh)"
```

> Steps:  
> 打開終端機(Terminal)
> 1. 複製 Homebrew 指令 ([到Homebrew官網複製安裝指令](https://brew.sh/index_zh-tw))
> 2. 將 install 改為 uninstall
> 3. 系統會提示確認操作，輸入y



Reference: [https://www.imymac.tw/mac-cleaner/how-to-uninstall-homebrew-on-mac.html](https://www.imymac.tw/mac-cleaner/how-to-uninstall-homebrew-on-mac.html)