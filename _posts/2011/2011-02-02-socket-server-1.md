---
layout: post
title: "[C# 筆記][WinForm] Socket-創建和客戶端通信的Socket-1"
date: 2011-02-02 00:00:05 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---

## Server端-監聽(draft)
設計要點：
```text
Socket()
Bind()綁定監聽端口
Listen() 設置監聽隊列
Accept() 循環等待客戶端連接
Receive()
Send()
```

```c#
//Socket(): 創建一個負責監聽的Socket (流式的tcp)
Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
IPAddress ip = IPAddress.Any; //你ip有變化，它會跟著變 
IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
//Bind()綁定監聽端口: 讓負責監聽的Socket綁定IP位址和Port端口號
socketWatch.Bind(point);
ShowMsg("監聽成功");
//Listen() 設置監聽隊列
socketWatch.Listen(10);//設置監聽隊列
//Accept() 循環等待客戶端連接
Socket socketSend = socketWatch.Accept(); //TODO: thread + while
//Receive()
ShowMsg(socketSend.RemoteEndPoint.ToString()+"連接成功");
```

### 完整Code(Draft)

```c#
private void btnStart_Click(object sender, EventArgs e)
{
    //創建一個負責監聽的Socket (流式的tcp)
    Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //創建IP地址和Port端口號 對象
    IPAddress ip = IPAddress.Any; //你ip有變化，它會跟著變 //IPAddress.Parse(txtIP.Text);//寫死了，不好
    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
    //讓負責監聽的Socket綁定IP位址和Port端口號
    socketWatch.Bind(point); //我綁定了point等於綁定了ip，因為point裡有IP:Port

    ShowMsg("監聽成功");

    //設置監聽隊列
    //監聽隊列指的就是，
    //在某一個時間點內，能夠連入Server端的最大Clinet端的數量(不是人滿的意思)，當到達限量時就只能排隊

    //例如廁所裡面，廁所有5間，最大容量5個人，第6個就只能排隊了
    //而這邊的監聽隊列就是，當到達這個限定的時候(最大容量)，只能排隊了
    //如果有人問，像這種情況資料庫怎麼優化呀？
    //最簡單的方法就是，花錢多買服務器就解決啦 XDD，但不能這麼說，
    //就說"排隊咩"，儘量把等待時間減少就可以了

    socketWatch.Listen(10);//設置監聽隊列

    //Accept()等待客戶的連接
    //負責監聽的Socket來接收客戶端的連接
    //創建跟客戶端通信的Socket
    //為什麼只能連一個，因為Accept它接收完了程式就往下走了
    //如果要連很多就要加上while循環，並用線程才不會卡死畫面
    Socket socketSend = socketWatch.Accept(); //這行讓視窗卡死了，有人連進來就不卡了，要拿線程去做這件事
    ShowMsg(socketSend.RemoteEndPoint.ToString()+"連接成功");
}

void ShowMsg(string str) {
    //用Append追加文字。不用text，因為text會覆蓋掉之前的值
    txtLog.AppendText(str+"\r\n");
}
```
TODO：需要解決兩件事：
1. 只能接受一個用戶端 => while(true)解決
2. 尚未有用戶端連進來前，畫面會假死(卡死) => 開一個新的thread
 
## Server端-監聽2
設計要點：
1. 使用循環`while(true)`等待客戶連接 => 解決只能接受一個用戶端的問題。
2. 開一個新的`thread`執行緒去執行`.Accept()`=> 解決尚未有用戶端連進來前，畫面會假死(卡死)的問題。
  
> `thread`執行緒帶參數的方法，傳入的參數必須是`object`類型

```c#
private void btnStart_Click(object sender, EventArgs e)
{
    //創建一個負責監聽的Socket (流式的tcp)
    Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //創建IP地址和Port端口號 對象
    IPAddress ip = IPAddress.Any; //你ip有變化，它會跟著變 //IPAddress.Parse(txtIP.Text);//寫死了，不好
    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
    //監聽
    socketWatch.Bind(point);
    ShowMsg("監聽成功");
    socketWatch.Listen(10);//設置監聽隊列

    Thread th = new Thread(Listen);
    th.IsBackground = true;
    th.Start(socketWatch);
}
/// <summary>
/// 等待客戶端的連接，並且創建與之通信的Socket
/// </summary>
void Listen(object o) {
    Socket socketWatch = o as Socket;
    while (true)
    {
        //等待客戶端的連接，並且創建跟客戶端通信的Socket
        Socket socketSend = socketWatch.Accept();
        //192.168.11.78 連接成功
        ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");
    }
}
```

### 完整Code

```c#
private void btnStart_Click(object sender, EventArgs e)
{
    //創建一個負責監聽的Socket (流式的tcp)
    Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //創建IP地址和Port端口號 對象
    IPAddress ip = IPAddress.Any; //你ip有變化，它會跟著變 //IPAddress.Parse(txtIP.Text);//寫死了，不好
    IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
    //讓負責監聽的Socket綁定IP位址和Port端口號
    socketWatch.Bind(point); //我綁定了point等於綁定了ip，因為point裡有IP:Port

    ShowMsg("監聽成功");

    //設置監聽隊列
    //監聽隊列指的就是，
    //在某一個時間點內，能夠連入Server端的最大Clinet端的數量(不是人滿的意思)，當到達限量時就只能排隊

    //例如廁所裡面，廁所有5間，最大容量5個人，第6個就只能排隊了
    //而這邊的監聽隊列就是，當到達這個限定的時候(最大容量)，只能排隊了
    //如果有人問，像這種情況資料庫怎麼優化呀？
    //最簡單的方法就是，花錢多買服務器就解決啦 XDD，但不能這麼說，
    //就說"排隊咩"，儘量把等待時間減少就可以了

    socketWatch.Listen(10);//設置監聽隊列

    //負責監聽的Socket來接收客戶端的連接
    //等待客戶端的連接，並且創建跟客戶端通信的Socket
    //為什麼只能連一個，因為Accept它接收完了程式就往下走了
    //如果要連很多就要加上while循環，並用線程才不會卡死畫面
    //Socket socketSend = socketWatch.Accept(); //這行讓視窗卡死了，有人連進來就不卡了，要拿線程去做這件事

    //192.168.11.78 連接成功
    //ShowMsg(socketSend.RemoteEndPoint.ToString()+"連接成功");

    Thread th = new Thread(Listen);
    th.IsBackground = true;
    th.Start(socketWatch); //從這傳入Listen方法要的參數

}
/// <summary>
/// 等待客戶端的連接，並且創建與之通信的Socket
/// </summary>
void Listen(object o) { //必須是object類型
    Socket socketWatch = o as Socket  //用as強制轉型
    while (true)
    {
        //等待客戶端的連接，並且創建跟客戶端通信的Socket
        Socket socketSend = socketWatch.Accept();
        //192.168.11.78 連接成功
        ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");
    }
}

void ShowMsg(string str) {
    //用Append追加文字。不用text，因為text會覆蓋掉之前的值
    txtLog.AppendText(str+"\r\n");
}

private void frmServer_Load(object sender, EventArgs e)
{
    //取消跨線程的檢查
    Control.CheckForIllegalCrossThreadCalls = false;
}
```

## 創建和客戶端通信的Socket(Code)

```c#
namespace ServerSocket
{
    public partial class frmServer : Form {
        public frmServer() {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //創建一個負責監聽的Socket (流式的tcp)
            Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //創建IP地址和Port端口號 對象
            IPAddress ip = IPAddress.Any; //你ip有變化，它會跟著變 //IPAddress.Parse(txtIP.Text);//寫死了，不好
            IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));
            //讓負責監聽的Socket綁定IP位址和Port端口號
            socketWatch.Bind(point); //我綁定了point等於綁定了ip，因為point裡有IP:Port

            ShowMsg("監聽成功");

            //設置監聽隊列
            //監聽隊列指的就是，
            //在某一個時間點內，能夠連入Server端的最大Clinet端的數量(不是人滿的意思)，當到達限量時就只能排隊

            //例如廁所裡面，廁所有5間，最大容量5個人，第6個就只能排隊了
            //而這邊的監聽隊列就是，當到達這個限定的時候(最大容量)，只能排隊了
            //如果有人問，像這種情況資料庫怎麼優化呀？
            //最簡單的方法就是，花錢多買服務器就解決啦 XDD，但不能這麼說，
            //就說"排隊咩"，儘量把等待時間減少就可以了
            //監聽
            socketWatch.Listen(10);//設置監聽隊列

            //負責監聽的Socket來接收客戶端的連接
            //等待客戶端的連接，並且創建跟客戶端通信的Socket
            //為什麼只能連一個，因為Accept它接收完了程式就往下走了
            //如果要連很多就要加上while循環，並用線程才不會卡死畫面
            //Socket socketSend = socketWatch.Accept(); //這行讓視窗卡死了因為客戶端不連，我主線程就卡死了，有人連進來就不卡了，要拿線程去做這件事

            //192.168.11.78 連接成功
            //ShowMsg(socketSend.RemoteEndPoint.ToString()+"連接成功");

            //開新線程去執行Listen
            Thread th = new Thread(Listen);
            th.IsBackground = true;
            th.Start(socketWatch); //從這傳入Listen方法要的參數

        }
        /// <summary>
        /// 等待客戶端的連接，並且創建與之通信的Socket
        /// </summary>
        void Listen(object o) { //必須是object類型
            Socket socketWatch = o as Socket; //用as強制轉型
            while (true)
            {
                //等待客戶端的連接，並且創建跟客戶端通信的Socket
                Socket socketSend = socketWatch.Accept();
                //192.168.11.78 連接成功
                ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");
            }
        }

        void ShowMsg(string str) {
            //用Append追加文字。不用text，因為text會覆蓋掉之前的值
            txtLog.AppendText(str+"\r\n");
        }

        private void frmServer_Load(object sender, EventArgs e)
        {
            //取消跨線程的檢查
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
```
TODO: Receive()、Send()    

[待續-Socket-服務器接收客戶端發送過來的消息](https://riivalin.github.io/posts/socket-3/)

## Thread執行緒帶參數的方法
Thread執行緒執行的方法，如果有參數，那麼這個參數必須是`object`類型

```c#
namespace 多線程執行帶參數的方法 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //線程執行的方法，如果有參數，那麼這個參數必須是object類型
            Thread t = new Thread(Test);
            t.IsBackground = true;
            t.Start("123"); //在這裡傳參數
        }
        public void Test(object str) {
            string s = (string)str;
            MessageBox.Show(s);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //取消跨線程的檢查
            Control.CheckForIllegalCrossThreadCalls = false;
        }
    }
}
```


## Win10啟用Telnet

```text
Step1. 控制台 > "開啟或關閉Windows功能"  
Step2. 勾選"Telnet用戶端" > 確定  
```
```shell
telnet 192.168.11.58 50000
```
監聽成功
10.211.55.7:50765 連接成功
