---
layout: post
title: "[MacOS] 使用rbenv安裝 Ruby"
date: 2023-02-18 00:10:00 +0800
categories: [Notes, Jekyll]
tags: [Ruby, Jekyll, rbenv]
---

- 安裝 Homebrew
- 安裝 rbenv (使用 Homebrew 安裝 rbenv)
- 安裝 Ruby (使用 rbenv 安裝 Ruby)

*****

打開終端機Terminal
## Step 1: 安裝Homebrew
1. [到Homebrew官網複製指令：](https://brew.sh/index_zh-tw)
2. 在終端機輸入：
```
/bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)”
```
3. 按下 Enter 執行指令 
4. 輸入登入MacOS的密碼  > 按下Enter >  出現系統提示後，再次按下 Enter
5. 確認 Homebrew 是否安裝完成：(查看版本)
```
brew -v
``` 

## Step 2: 安裝 rbenv (使用 Homebrew 安裝 rbenv)
1. 在終端機輸入 
```
brew install rbenv
```
2. 確認 rbenv 是否安裝完成
rbenv 安裝完成後，輸入 rbenv -v 查看版本。
```
rbenv -v
```
3. rbenv 初始化
MacOS Catalina 使用的是 zsh，在終端機輸入以下指令：
```shell
echo 'export PATH="$HOME/.rbenv/bin:$PATH"' >> ~/.zshrc
echo 'eval "$(rbenv init -)"' >> ~/.zshrc
```

## Step 3: 安裝Ruby (使用 rbenv 安裝 Ruby)
使用 rbenv 安裝 ruby 3.2.1
1. 關閉重開終端機。(在安裝 ruby 之前需要關掉重開終端機)
2. 在終端機輸入 
``` 
rbenv install
```
3. 指定預設使用的 ruby 版本為 3.2.1
```
rbenv global 3.2.1
```

## Step 4: 確認是否安裝完成 (查看版本)
```
ruby -v
```
看到 ruby 3.2.1... 就代表安裝完成


[https://etrex.tw/free_chatbot_book/mac_dev/ruby.html](https://etrex.tw/free_chatbot_book/mac_dev/ruby.html)

<!-- script pointing to busuanzi.js start-->
<script async src="/assets/js/busuanzi.pure.mini.js"></script>
<span id="busuanzi_container_page_pv">Total <span id="busuanzi_value_page_pv"></span> views.</span>
<!-- script pointing to busuanzi.js end-->