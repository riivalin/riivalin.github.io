---
layout: post
title: "[C# 筆記][Socket] 客戶端接收服務端發來的消息-4"
date: 2011-02-02 00:00:35 +0800
categories: [Notes, C#]
tags: [C#,Socket]
---

[Client Socket-客戶端給服務器發送消息 -1](https://riivalin.github.io/posts/socket-4/)

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

## Recieve() 客戶端接收服務端發來的消息

```c#
/// <summary>
/// 不停的接收服務器發來的消息
/// </summary>
void Recieve()
{
    while (true)//要不停的去接收，所以要用死循環
    {
        byte[] buffer = new byte[1024 * 1024 * 3];//設置要接收到的byte陣列
        int r = socketSend.Receive(buffer);//返回的是實際接收到的有效byte數
        if (r == 0) break; //沒有接收到就跳出迴圈
        string s = Encoding.UTF8.GetString(buffer, 0, r); //將bytes轉成我們看得懂的字串
        ShowMsg(socketSend.RemoteEndPoint + ":" + s); //訊息顯示在畫面上
    }
}
```

## 開新的Thread去執行Recieve()

因為有死循環，所以會畫面卡死，所以要開新的線程來執行這個函數

```c#
//開啟一個新的線程不條的接收服務端發來的消息
Thread th = new Thread(Recieve); //要執行的函數
th.IsBackground = true; //設為背景執行緒
th.Start(); //告訴cpu我準備好了，隨時都可以執行
```

```c#
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

    //開啟一個新的線程不條的接收服務端發來的消息
    Thread th = new Thread(Recieve); //要執行的函數
    th.IsBackground = true; //設為背景執行緒
    th.Start(); //告訴cpu我準備好了，隨時都可以執行
}

/// <summary>
/// 不停的接收服務器發來的消息
/// </summary>
void Recieve()
{
    while (true)//要不停的去接收，所以要用死循環
    {
        byte[] buffer = new byte[1024 * 1024 * 3];//設置要接收到的byte陣列
        int r = socketSend.Receive(buffer);//返回的是實真接收到的有效byte數
        if (r == 0) break; //沒有接收到就跳出迴圈
        string s = Encoding.UTF8.GetString(buffer, 0, r); //將bytes轉成我們看得懂的字串
        ShowMsg(socketSend.RemoteEndPoint + ":" + s); //訊息顯示在畫面上
    }
}
```

## 取消跨線程的檢查

```c#
private void frmClient_Load(object sender, EventArgs e)
{
    //取消跨線程的檢查
    Control.CheckForIllegalCrossThreadCalls = false;
}
```

## 包try-catch
如果Server端沒有開啟，我Client端一直連接，就會報錯拋異常，所以要包try-catch給他提示點訊息

## 完整程式碼

```c#
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

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
            try
            {
                //創建負責通信的Socket
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ip = IPAddress.Parse(txtIP.Text);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
                //獲得要連接的遠程服務器應用程序的IP地址和Port端口號
                socketSend.Connect(point);
                ShowMsg("連接成功");

                //開啟一個新的線程不條的接收服務端發來的消息
                Thread th = new Thread(Recieve); //要執行的函數
                th.IsBackground = true; //設為背景執行緒
                th.Start(); //告訴cpu我準備好了，隨時都可以執行
            } catch { MessageBox.Show("連接失敗"); }
        }

        /// <summary>
        /// 不停的接收服務器發來的消息
        /// </summary>
        void Recieve()
        {
            while (true)//要不停的去接收，所以要用死循環
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 3];//設置要接收到的byte陣列
                    int r = socketSend.Receive(buffer);//返回的是實真接收到的有效byte數
                    if (r == 0) break; //沒有接收到就跳出迴圈
                    string s = Encoding.UTF8.GetString(buffer, 0, r); //將bytes轉成我們看得懂的字串
                    ShowMsg(socketSend.RemoteEndPoint + ":" + s); //訊息顯示在畫面上
                } catch { }
            }
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

        private void frmClient_Load(object sender, EventArgs e)
        {
            //取消跨線程的檢查
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
```