---
layout: post
title: "[Mac] 在 Mac 上格式化外接硬碟/隨身碟，讓 Mac/Windows 皆可用"
date: 2023-10-01 23:59:00 +0800
categories: [Notes,Others]
tags: [mac]
---

# 步驟

1. 在 Launchpad（應用程式）裡打開「其他」>「磁碟工具程式」      
在開啟的視窗左側找到你的外接硬碟或隨身碟，單擊「清除」按鈕。

2. 介面上會彈出一個視窗要你確認是否要清除硬碟，現在先不要急著點「清除」。       
你可以看到視窗中的「格式」和「架構」下各有下拉選單，選擇不同的格式與架構可以決定你的硬碟是否可用於 Mac 或 Windows 電腦。

3. 如果你想讓外接硬碟同時可用於 Mac 與 Windows 系統，則選：     
「ExFAT」（用於容量超過 32 GB 的 Windows 卷宗）以及「主開機記錄」架構。     

將硬碟格式轉化成 ExFAT 格式後， Windows 和 Mac 都可以讀寫了。

> - 「MS-DOS（FAT）」格式（用於容量 32GB 或更小的 Windows 卷宗）        
> - 「ExFAT」（用於容量超過 32 GB 的 Windows 卷宗）


4. 設定完成後，按「清除」進行格式化。等到格式化完成後取出外接硬碟就可。


注意：MS-DOS（FAT）亦叫 FAT32，這個格式雖然可以讓你的磁碟同時在 Windows 與 Mac 上存取，但儲存的檔案不能超過 4GB（一部高質影片都有可能大於 4GB）。

# 格式選項：

- Mac OS 擴充格式（日誌式）：使用 Mac 格式（日誌式 HFS Plus）來保護階層式檔案系統的完整性。

- Mac OS 擴充格式（日誌式、已加密）：使用 Mac 格式、要求密碼及加密分割區。

- Mac OS 擴充格式（區分大小寫、日誌式）：使用 Mac 格式且檔案夾名稱有區分大小寫。例如，名稱為「Homework」和「HOMEWORK」的檔案夾會是兩個不同的檔案夾。

- Mac OS 擴充格式（區分大小寫、日誌式、已加密）：使用 Mac 格式、檔案夾名稱有區分大小寫、要求密碼及加密分割區。

- MS-DOS（FAT）：用於 32GB 或更小的 Windows 卷宗。

- ExFAT：用於超過 32GB 的 Windows 卷宗。

# 架構選項：

- GUID 分割區配置表：用於所有 Intel 基準的 Mac 電腦。

- 主開機記錄：用於將會格式化為「MS-DOS（FAT）」或「ExFAT」的 Windows 分割區。

- Apple 分割區配置表：用於較舊 PowerPC 基準的 Mac 電腦相容性。



<https://www.macube.com/tw/how-to/format-external-hard-drive-mac.html>