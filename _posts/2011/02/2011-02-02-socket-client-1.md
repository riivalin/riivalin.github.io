---
layout: post
title: "[C# 筆記][Socket] 客戶端給服務器發送消息-3"
date: 2011-02-02 00:00:25 +0800
categories: [Notes, C#]
tags: [C#,Socket]
---

客戶端  

```text
Socket()
Connect() 連接建立
Send() 發送數據
Receive() 接收數據
Close()
```

## 連接Server

```c#
private void btnStart_Click(object sender, EventArgs e)
{
    //創建負責通信的Socket
    Socket socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    IPAddress ip = IPAddress.Parse(txtIP.Text);
    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
    //獲得要連接的遠程服務器應用程序的IP地址和Port端口號
    socketSend.Connect(point);
    ShowMsg("連接成功");
}
void ShowMsg(string str)
{
    txtLog.AppendText(str + "\r\n");
}
```

## Send() 客戶端給服務器發送消息

```c#
/// <summary>
/// 客戶端給服務器發送消息
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnSend_Click(object sender, EventArgs e)
{
    string str = txtMsg.Text.Trim();
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
    socketSend.Send(buffer); //需要傳入 byte[]
}
```

## 完整程式碼
```c#
namespace ClientSocket
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();
        }

        Socket socketSend; //宣告Socket變數
        private void btnStart_Click(object sender, EventArgs e)
        {
            //創建負責通信的Socket
            socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(txtIP.Text);
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
            //獲得要連接的遠程服務器應用程序的IP地址和Port端口號
            socketSend.Connect(point);
            ShowMsg("連接成功");
        }

        void ShowMsg(string str)
        {
            txtLog.AppendText(str + "\r\n");
        }

        /// <summary>
        /// 客戶端給服務器發送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string str = txtMsg.Text.Trim();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            socketSend.Send(buffer); //需要傳入byte[]
        }
    }
}
```

https://www.bilibili.com/video/BV17G4y1b78i?p=196
