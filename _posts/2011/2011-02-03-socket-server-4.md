---
layout: post
title: "[C# 筆記][WinForm] Socket-給指定的客戶端發送消息-4"
date: 2011-02-03 00:00:05 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---


現在有一個問題，我有多個客戶端，我只能發給最後一個客戶端，之前的客戶端我想發也發不了，Server端只能給最後一個連進來的客戶端發消息。    

造成這個情況的原因是什麼？    

在Server端的`Listen()`創建負責通信的Socket，下一個客戶端連過來之後，跟原來那個通信的Socket就沒有了，取而代之是新的Socket的，因為我們沒有把它儲存起來。    

所以，每來一個新的客戶端，原來那個就沒有了，服務器發回來的永遠都是最後一個Client端。   

所以怎麼做？存起來，不存起來就消失了，存哪？把IP+Port存到下拉框    
    
【需求】
- 把客戶端的IP+Port存到下拉框，透過下拉框自己決定要發送給哪一個客戶端
- 透過IP地址找到客戶端的Socket。將遠端連接的客戶端的IP地址和Socket存入集合中
    
【TODO】
1. 先聲明一個<key,value>字典集合
2. 當有客戶端連進來時，將ip地址和socket存入集合中
3. 並將ip地址顯示在下拉框中
4. 當要發送消息時，透過選中的ip去找到它的socket來發送消息

## 聲明<key,value>字典集合以儲存IP和socket

```c#
//將遠端連接的客戶端的IP地址和Socket存入集合中
Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();
```

## Listen()

- 當有客戶端連進來時，將ip地址和socket存入集合中
- 並將ip地址顯示在下拉框中

```c#
/// <summary>
/// 等待客戶端的連接，並且創建與之通信的Socket
/// </summary>
Socket socketSend;
void Listen(object o) //必須是object類型
{
    Socket? socketWatch = o as Socket; //用as強制轉型

    //等待客戶端的連接，並且創建跟客戶端通信的Socket
    while (true)
    {
        try //有可能發生異常的地方要用try-catch包起來
        {
            //負責跟客戶端通信的Socket
            socketSend = socketWatch.Accept(); //Accept接收客戶端的連接
            //將遠端連接的客戶端的IP地址和Socket存入集合中
            dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend); //IP, Socket
            //將遠端連的客戶端IP地址和Port端口號儲存下拉框中
            cboUsers.Items.Add(socketSend.RemoteEndPoint.ToString());
            //192.168.11.78 連接成功
            ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");

            //Recieve() 開啟一個新線程不停的接收客戶端發送過來的消息
            Thread th = new Thread(Recieve);
            th.IsBackground = true;
            th.Start(socketSend);
        } catch { }
    }
}
```
> 透過`socketSend.RemoteEndPoint`可以取得IP+Port

## Send()

當要發送消息時，透過選中的IP地址去找到它的Socket來發送消息

```c#
/// <summary>
/// 服務器給客戶端發送消息
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnSend_Click(object sender, EventArgs e)
{

    string str = txtMsg.Text;
    byte[] buffer = Encoding.UTF8.GetBytes(str);
    //獲得用戶在下拉框中選中的IP地址
    string ip = cboUsers.SelectedItem.ToString();
    dicSocket[ip].Send(buffer);
    //socketSend.Send(buffer); //把要發送出去的消息轉成數組byte[]
}
```

## 實作畫面

![](/assets/img/post/socket-for-mult-user.png)

## Server完整Code

```c#
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public partial class frmServer : Form
    {
        public frmServer() {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //Socket()-創建一個負責監聽的Socket (流式的tcp)
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //創建IP地址和Port端口號 對象
                IPAddress ip = IPAddress.Any; //你ip有變化，它會跟著變 //IPAddress.Parse(txtIP.Text);//寫死了，不好
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
                //Bind()-讓負責監聽的Socket綁定IP位址和Port端口號
                socketWatch.Bind(point); //我綁定了point等於綁定了ip，因為point裡有IP:Port

                ShowMsg("監聽成功");

                //Listen()-監聽
                socketWatch.Listen(10);//設置監聽隊列

                //開新線程去執行Listen
                Thread th = new Thread(Listen);
                th.IsBackground = true;
                th.Start(socketWatch); //從這傳入Listen方法要的參數
            } catch { }
        }

        /// <summary>
        /// 等待客戶端的連接，並且創建與之通信的Socket
        /// </summary>
        Socket socketSend;
        void Listen(object o) //必須是object類型
        {
            Socket? socketWatch = o as Socket; //用as強制轉型
            if (socketWatch == null) return;

            //等待客戶端的連接，並且創建跟客戶端通信的Socket
            while (true)
            {
                try
                {
                    //負責跟客戶端通信的Socket
                    socketSend = socketWatch.Accept(); //Accept接收客戶端的連接
                    //將遠端連接的客戶端的IP地址和Socket存入集合中
                    dicSocket.Add(socketSend.RemoteEndPoint.ToString(), socketSend); //IP, Socket
                    //將遠端連的客戶端IP地址和Port端口號儲存下拉框中
                    cboUsers.Items.Add(socketSend.RemoteEndPoint.ToString());
                    //192.168.11.78 連接成功
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");

                    //Recieve() 開啟一個新線程不停的接收客戶端發送過來的消息
                    Thread th = new Thread(Recieve);
                    th.IsBackground = true;
                    th.Start(socketSend);
                } catch { }
            }
        }
        //將遠端連接的客戶端的IP地址和Socket存入集合中
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>();

        /// <summary>
        /// 服務器不停的接收客戶端發送過來的消息
        /// </summary>
        /// <param name="o">Socket:socketSend</param>
        void Recieve(object o) //寫成方法拿不到socketSend了，加個參數可以傳進來
        {
            Socket socketSend = o as Socket; //as強制轉型
            while (true)//=>遠程客戶端一關，就會死在這裡
            {
                //凡是有可能發生異常的地方，就要try-catch
                try
                {
                    //Receive()-客戶端連接成功後，服務器應該接受客戶端發來的消息
                    byte[] buffer = new byte[1024 * 1024 * 2]; //把數據放到bytes裡
                                                               //實際接受到的有效bytes數
                    int r = socketSend.Receive(buffer);
                    if (r == 0) break; //必須要加上這一段，如果客戶端一關，就不要再接收了

                    //因為看不懂bytes所以要轉成字串
                    string str = Encoding.UTF8.GetString(buffer, 0, r);
                    ShowMsg(socketSend.RemoteEndPoint + ":" + str);

                } catch
                {

                }
            }
        }

        void ShowMsg(string str)
        {
            //用Append追加文字。不用text，因為text會覆蓋掉之前的值
            txtLog.AppendText(str + "\r\n");
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            //取消跨線程的檢查
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 服務器給客戶端發送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {

            string str = txtMsg.Text;
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            //獲得用戶在下拉框中選中的IP地址
            string ip = cboUsers.SelectedItem.ToString();
            dicSocket[ip].Send(buffer);
            //socketSend.Send(buffer); //把要發送出去的消息轉成數組byte[]
        }
    }
}
```

## Client完整Code
```c#
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public partial class frmClient : Form
    {
        public frmClient() {
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

                //開啟一個新的線程不停的接收服務端發來的消息
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

https://www.bilibili.com/video/BV17G4y1b78i?p=198