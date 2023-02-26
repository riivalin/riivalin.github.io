---
layout: post
title: 記錄常用的commands
date: 2023-02-20 02:10:00 +0800
categories: [Notes, Commands]
tags: [Notes, Commands]
---

```shell
rbenv uninstall 2.6.5  //移除Ruby 2.6.5版本
ruby -v  //Ruby當前版本
```

Run jekyll serve in VSCode (zsh shell)
```shell
bundle //安裝相關依賴
bundle exec jekyll s //啟動jekyll服務
```

## Ruby rbenv commands
[rebnv](https://devhints.io/rbenv)
### Managing versions
```shell
rbenv install -l	//List all available versions
rbenv install 2.2.1	  //Install Ruby 2.2.1
rbenv uninstall 2.2.1	//Uninstall Ruby 2.2.1
rbenv versions	 //See installed versions
rbenv version	//See current version
rbenv which <NAME>	 //Display path to executable
rbenv rehash	//Re-write binstubs
```

### Using versions
### Locally
```shell
rbenv local 2.2.2	//Use Ruby 2.2.2 in project
rbenv local --unset	 //Undo above
```
### Globally
```shell
rbenv global 2.2.2	//Use Ruby 2.2.2 globally
rbenv global --unset	//Undo above
```


### setup
[ruby_rbenv.](https://samkennerly.github.io/tldrs/ruby_rbenv.html)
```shell
brew install rbenv	//install rbenv with Homebrew
brew uninstall rbenv	//uninstall rbenv with Homebrew
brew upgrade rbenv ruby-build	//upgrade rbenv with Homebrew
eval "$(rbenv init -)"	//modify shell to use rbenv
usage
rbenv install 2.6.3	 //install Ruby version 2.6.3
rbenv install --list	//show available Rubies
rbenv shell 2.6.3	//use Ruby version 2.6.3
rbenv shell --unset	 //revert to using default Ruby
rbenv uninstall 2.6.3	//remove Ruby version 2.6.3
rbenv versions	//show installed Rubies
```

## VSCode
### Unable to run the Jekyll in VS code
```shell
echo 'export PATH="/usr/local/opt/ruby/bin:/usr/local/lib/ruby/gems/3.2.1/bin:$PATH"' >> ~/.zshrc
source ~/.zshrc
```
> line 1: 更新 shell 配置文件中的 PATH 路径 (3.2.1為當前安裝的Ruby新版本)  
> line 2: 刷新 shell 配置文件，使其生效

### Set default Ruby version in rbenv
在VS Code 終端機執行下面兩行指令，再執行 ruby -v 確認是否為最新版本：
```shell
eval "$(rbenv init -)" //Set default Ruby version in rbenv
rbenv global  // 或是 PATH="$HOME/.rbenv/bin:$PATH"
```