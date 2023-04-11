---
layout: post
title: "[C# 筆記][WinForm] Socket-服務器給客戶端發送消息-3"
date: 2011-02-02 00:00:45 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---


- [Server Socket-創建和客戶端通信的Socket -1](https://riivalin.github.io/posts/socket-server-1/)
- [Server Socket-服務器接收客戶端發送過來的消息 -2](https://riivalin.github.io/posts/socket-server-2/)

## Server端 Send()

```c#
/// <summary>
/// 服務器給客戶端發送消息
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnSend_Click(object sender, EventArgs e)
{
    string str = txtMsg.Text;
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
    socketSend.Send(buffer); //把要發送出去的消息轉成數組byte[]
}
```

## Client端 




https://www.bilibili.com/video/BV17G4y1b78i?p=196