---
layout: post
title: "[Jekyll] Windows 上安装 jekyll"
date: 2024-08-05 06:23:00 +0800
categories: [Notes,Jekyll,WDM,wdm-errors]
tags: [jekyll,windows ruby-installer,]
---

- Windows 需要先安裝 RubyInstaller，將 Ruby
- 備用方案（未測試過）：使用 Scoop 安裝 Ruby
 
## Windows 安裝 Ruby 和 RubyGems
安裝包下載和安裝        
開啟 <https://rubyinstaller.org/downloads/> 選擇 WITH DEVKIT下面的安裝包

- 推薦使用最新版本
- 如果電腦是 32位元系統選擇 x86 的包，64位元系統選擇 x64
- Ruby + Devkit 是整合了 gem 的安裝

現在完後打開安裝包，除了第一個介面選擇同意條款外，後面都直接都是用預設選項，點下一步。

安裝完後會引導執行 ridk install，預設是勾選上的，點選完成自動執行 CMD終端

在 CMD 終端介面輸入`3` 並按 Enter 回車鍵，選擇安裝 ` 3 - MSYS2 and MINGW development toolchain`

執行完後會再次提示，為確保萬無一失，請再次輸入3 並按 Enter 回車鍵。再次提示按 Enter 回車鍵會自動關閉 cmd終端機。

在開始功能表點擊`Start Command Prompt with Ruby` 開啟並使用Ruby。 這一步不能少。

檢查安裝是否成功，使用檢視版本指令如果有回傳版本號碼就是安裝好了    

- 查看 Ruby 版本：ruby -v
- 查看 gem 版本：gem -v
- 然後 替換來源鏡像，再更新 Ruby gem

##  更新 Ruby gem

```cmd
gem update
```

## 安装 Jekyll 组件

```cmd
gem install jekyll bundler
```

## 查看 jekyll 當前版本

```cmd
jekyll -v
```

## 推薦環境安裝後執行下面指令避免一些報錯
安装 webrick

```cmd
bundle add webrick
```

## 運行服務：

```cmd
bundle exec jekyll serve
```


[[MacOS] 在VSCode 啟動Jekyll服務預覽網站](https://riivalin.github.io/posts/2023/02/vscode-run-jekyll-serve/)    
[Windows 上安装 jekyll ](https://1px.run/jekyll/windows/)