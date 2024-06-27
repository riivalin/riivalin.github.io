---
layout: post
title: Share a folder from Win11 to Macbook
date: 2024-04-06 05:23:00 +0800
categories: [Notes,Others]
tags: [win11,mac]
---

Just a note to myself...

## 1. Win11共享資料夾

前置作業：先設定好要共用的資料夾。


控制台 > 網路和網際網路 > 網路和共用中心 > 變更進階共用設定     

因為我是「以密碼保護的共用」所以是這樣：

![](/assets/img/post/win11-setting-share-folder-hasidpw.png)

設定完成後：        

![](/assets/img/post/win11-share-folder-done.png)

## 2. MacBook連接

前往 > 連接伺服器 > `smb://ip` (或是你的電腦名稱) > 連接 > 選擇要裝載的卷宗 > 輸入帳號密碼


![](/assets/img/post/mac-connect-server-1.png)


![](/assets/img/post/mac-connect-server-2.png)



> 不知道ip，可以 `cmd` 下指令 `ipconfig`


## Windows 11 如何新增本機一般使用者帳戶

設定 > 帳戶 > 其他用戶 > 新增帳戶 >        
我沒有這位人員的登入資訊 > 新增沒有 Microsoft 帳戶的使用者 > 建立 ID&PW
