---
layout: post
title: "[C# 筆記][WinForm] Server Socket-服務器接收客戶端發送過來的消息 -2"
date: 2011-02-02 00:00:15 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---

[前提情要-創建和客戶端通信的Socket](https://riivalin.github.io/posts/socket-2/)
     
TODO: Receive()、Send()    
    
---
     
## Step1：Receive()

```c#
//Receive()-客戶端連接成功後，服務器應該接受客戶端發來的消息
byte[] buffer = new byte[1024 * 1024 * 2]; //把數據放到bytes裡
//實際接受到的有效bytes數
int r = socketSend.Receive(buffer);
//因為看不懂bytes所以要轉成字串
string str = Encoding.UTF8.GetString(buffer, 0, r);
ShowMsg(socketSend.RemoteEndPoint + ":" + str); 
```

```c#
/// <summary>
/// 等待客戶端的連接，並且創建與之通信的Socket
/// </summary>
void Listen(object o) { //必須是object類型
    Socket socketWatch = o as Socket; //用as強制轉型
    while (true)
    {
        //Accept()-等待客戶端的連接，並且創建跟客戶端通信的Socket
        Socket socketSend = socketWatch.Accept();
        //192.168.11.78 連接成功
        ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");

        //以下這段要寫成寫成一個方法，再開一個thread去執行它。

        //Receive()-客戶端連接成功後，服務器應該接受客戶端發來的消息
        byte[] buffer = new byte[1024 * 1024 * 2]; //把數據放到bytes裡
        //實際接受到的有效bytes數
        int r = socketSend.Receive(buffer);
        //因為看不懂bytes所以要轉成字串
        string str = Encoding.UTF8.GetString(buffer, 0, r);
        ShowMsg(socketSend.RemoteEndPoint + ":" + str); //只接收一次
    }
}
```
這樣寫只接收到一個字元，為啥？因為它執行完後，就回到while (true)去等待新的客戶端連線，原本的就不在了，那怎辦？就要再給他一個循環，但是這也是一個死循環，怎辦？寫成一個方法，再開一個thread去執行它。


## Step2：Receive()寫成一個方法，再開一個thread去執行它。

```c#
/// <summary>
/// 等待客戶端的連接，並且創建與之通信的Socket
/// </summary>
void Listen(object o) //必須是object類型
{ 
    Socket socketWatch = o as Socket; //用as強制轉型
    while (true)
    {
        //Accept()-等待客戶端的連接，並且創建跟客戶端通信的Socket
        Socket socketSend = socketWatch.Accept();
        //192.168.11.78 連接成功
        ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");

        //Recieve()
        //開啟一個新線程不停的接收客戶端發送過來的消息
        Thread th = new Thread(Recieve);
        th.IsBackground = true;
        th.Start(socketSend);
    }
}

/// <summary>
/// 服務器不停的接收客戶端發送過來的消息
/// </summary>
/// <param name="o">Socket:socketSend</param>
void Recieve(object o) //寫成方法拿不到socketSend了，加個參數可以傳進來
{
    Socket socketSend = o as Socket; //as強制轉型
    while (true) //=>遠程客戶端一關，就會死在這裡
    {
        //Receive()-客戶端連接成功後，服務器應該接受客戶端發來的消息
        byte[] buffer = new byte[1024 * 1024 * 2]; //把數據放到bytes裡
        //實際接受到的有效bytes數
        int r = socketSend.Receive(buffer);
        //因為看不懂bytes所以要轉成字串
        string str = Encoding.UTF8.GetString(buffer, 0, r);
        ShowMsg(socketSend.RemoteEndPoint + ":" + str);
    }
}
```
現在又有一個問題了，我客戶端關了，它怎還在發送消息，消息哪來的呀？他死在哪裡？死在Recieve()裡的死循環

## Step3：沒有收到客戶端訊息就要跳出死循環

因為有死循環`while (true)`，遠程客戶端一關，就會死在這裡，code裡面必須要加上這一段`if (r == 0) break;`，如果客戶端一關，就不要再接收了。 

```c#
/// <summary>
/// 服務器不停的接收客戶端發送過來的消息
/// </summary>
/// <param name="o">Socket:socketSend</param>
void Recieve(object o) //寫成方法拿不到socketSend了，加個參數可以傳進來
{
    Socket socketSend = o as Socket; //as強制轉型
    while (true)//=>遠程客戶端一關，就會死在這裡
    {
        //Receive()-客戶端連接成功後，服務器應該接受客戶端發來的消息
        byte[] buffer = new byte[1024 * 1024 * 2]; //把數據放到bytes裡
        //實際接受到的有效bytes數
        int r = socketSend.Receive(buffer);
        if (r == 0) break; //必須要加上這一段，如果客戶端一關，就不要再接收了

        //因為看不懂bytes所以要轉成字串
        string str = Encoding.UTF8.GetString(buffer, 0, r);
        ShowMsg(socketSend.RemoteEndPoint + ":" + str);
    }
}
```

## Step4. try-catch  
凡涉及到網路的，有可能會發生異常的地方，就要用`try-catch`包起來，但不拋出訊息，讓客戶感覺不出有問題。

### Socket() 創建一個負責監聽的Socket (流式的tcp)

```c#
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
```

### Listen() 監聽：等待客戶端的連接，並且創建與之通信的Socket

設置監聽隊列  
監聽隊列指的就是，  
在某一個時間點內，能夠連入Server端的最大Clinet端的數量(不是人滿的意思)，當到達限量時就只能排隊    

例如廁所裡面，廁所有5間，最大容量5個人，第6個就只能排隊了  
而這邊的監聽隊列就是，當到達這個限定的時候(最大容量)，只能排隊了  
如果有人問，像這種情況資料庫怎麼優化呀？  
最簡單的方法就是，花錢多買服務器就解決啦 XDD，但不能這麼說，  
就說"排隊咩"，儘量把等待時間減少就可以了  

```c#
/// <summary>
/// 等待客戶端的連接，並且創建與之通信的Socket
/// </summary>
void Listen(object o)//必須是object類型
{
    Socket socketWatch = o as Socket; //用as強制轉型
    while (true)
    {
        try
        {
            //Accept()-等待客戶端的連接，並且創建跟客戶端通信的Socket
            Socket socketSend = socketWatch.Accept();
            //192.168.11.78 連接成功
            ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");

            //Recieve 客戶端連接成功後，服務器應該接受客戶端發來的消息
            //開啟一個新線程不停的接收客戶端發送過來的消息
            Thread th = new Thread(Recieve);
            th.IsBackground = true;
            th.Start(socketSend);
        } catch {}
    }
}
```

### Recieve() 服務器不停的接收客戶端發送過來的消息

```c#
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

        } catch {}
    }
}
```

### others

```c#
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
```

## 完整Code+註解過程

```c#
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
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

                //設置監聽隊列
                //監聽隊列指的就是，
                //在某一個時間點內，能夠連入Server端的最大Clinet端的數量(不是人滿的意思)，當到達限量時就只能排隊

                //例如廁所裡面，廁所有5間，最大容量5個人，第6個就只能排隊了
                //而這邊的監聽隊列就是，當到達這個限定的時候(最大容量)，只能排隊了
                //如果有人問，像這種情況資料庫怎麼優化呀？
                //最簡單的方法就是，花錢多買服務器就解決啦 XDD，但不能這麼說，
                //就說"排隊咩"，儘量把等待時間減少就可以了

                //Listen()-監聽
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
            } catch { }
        }

        /// <summary>
        /// 等待客戶端的連接，並且創建與之通信的Socket
        /// </summary>
        void Listen(object o)//必須是object類型
        { 
            Socket socketWatch = o as Socket; //用as強制轉型
            while (true)
            {
                try
                {
                    //Accept()-等待客戶端的連接，並且創建跟客戶端通信的Socket
                    Socket socketSend = socketWatch.Accept();
                    //192.168.11.78 連接成功
                    ShowMsg(socketSend.RemoteEndPoint.ToString() + "連接成功");

                    //Recieve
                    //Receive()-客戶端連接成功後，服務器應該接受客戶端發來的消息
                    //byte[] buffer = new byte[1024 * 1024 * 2]; //把數據放到bytes裡
                    //實際接受到的有效bytes數
                    //int r = socketSend.Receive(buffer);
                    //因為看不懂bytes所以要轉成字串
                    //string str = Encoding.UTF8.GetString(buffer, 0, r);

                    //這樣寫只接收到一個字元，為啥？
                    //因為它執行完後，就回到while (true)去等待新的客放端連線，
                    //原本的就不在了，那怎辦？
                    //就要再給他一個循環，但是這也是一個死循環，怎辦？
                    //寫成一個方法，再開一個thread去執行它
                    //ShowMsg(socketSend.RemoteEndPoint + ":" + str); //只接收一次


                    //Recieve
                    //開啟一個新線程不停的接收客戶端發送過來的消息
                    Thread th = new Thread(Recieve);
                    th.IsBackground = true;
                    th.Start(socketSend);
                } catch{ }
            }
        }

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
                } catch{}
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
    }
}
```
TODO: Send()


https://www.bilibili.com/video/BV17G4y1b78i?p=195
