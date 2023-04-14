---
layout: post
title: "[C# 筆記][Socket] Socket 通信基本流程圖"
date: 2011-02-03 00:00:19 +0800
categories: [Notes, C#]
tags: [C#,Socket]
---


![](/assets/img/post/socket-flowchart.png)


## Server端

```c#
using System.Net;
using System.Net.Sockets;
using System.Text;
/*
Server端會有兩個Socket：1.負責監聽的Socket 2.負責通信的Socket

Server: Socket() => Bind () => Listen () => Accept () => Recieve () => Send()
Client: Socket() => Connet () => Send () => Receive()
 */

namespace Server
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //第一個Socket():建立一個負責監聽的Socket(Stream流式的TCP)
                Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress ip = IPAddress.Any; //用any，ip有變化他會跟著變
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));

                //Bind()綁定監聽端口。讓負責監聽的Socket綁定IP位址和Port端口號
                socketWatch.Bind(point); //我綁定了point等於綁定了ip，因為point裡有IP:Port

                ShowMsg("監聽成功");

                //Listen()設置監聽隊列
                socketWatch.Listen(10);//在某一個時間點內，能夠連入Server端的最大Clinet端的數量(不是人滿的意思)，當到達限量時就只能排隊

                //Accept()循環等待接收客戶端的連接
                //開一個新的Thread執行緒去執行Listen方法
                Thread th = new Thread(Listen);
                th.IsBackground = true; //設為背景執行緒。只要前景執行緒結束，背景執行緒就會自動關閉
                th.Start(socketWatch); //把參數值從這帶進去。.Start()是告訴CPU我準備好了，你隨時可以執行
            } catch { }
        }

        /// <summary>
        /// 等待客戶端的連接，並創建與之通信的Socket
        /// </summary>
        Socket socketSend; //第二個Socket():負責跟客戶通信的Socket
        Dictionary<string, Socket> dicSocket = new Dictionary<string, Socket>(); //將遠端連接的客戶端的IP地址和Socket存入在集合中
        void Listen(object? o) //執行緒方法的參數，必須是object類型
        {
            Socket? socketWatch = o! as Socket;//as強制轉型

            while (true) //使用死循環，不停的接收等待接收客戶端的連接
            {
                try
                {
                    //第二個Socket():負責跟客戶通信的Socket
                    socketSend = socketWatch!.Accept();//Accept()循環等待接收客戶端的連接

                    //192.168.11.78 連接成功
                    ShowMsg(socketSend.RemoteEndPoint!.ToString() + "連接成功");

                    //將遠端連接的客戶端的IP地址和Socket存入集合中
                    dicSocket.Add(socketSend.RemoteEndPoint.ToString()!, socketSend); //<ip,socket>
                    //將遠端連接的客戶端的IP+Port顯示在下拉選單中
                    cboUsers.Items.Add(socketSend.RemoteEndPoint.ToString());

                    //Recieve() 客戶端連接成功後，服務器就要不停的接收客戶端發過來的消息
                    Thread th = new Thread(Recieve); //開一個新的執行緒去執行Recieve方法
                    th.IsBackground = true; //設為背景執行緒，只要前景執行緒全關閉，它就會自動關閉
                    th.Start(); //.Start()是告訴CPU我準備好了，你隨時可以執行

                } catch { }
            }
        }

        /// <summary>
        /// 服務器不停的接收客戶端發送過來的消息
        /// </summary>
        void Recieve()
        {
            while (true)//使用死循環，讓服務器不停的接收
            {
                try
                {
                    //Recieve() 客戶端連接成功後，服務器就要接收客戶端發過來的消息
                    byte[] buffer = new byte[1024 * 1024 * 2]; //宣告byte[]變數，用來把數據放到位元組陣列中
                    int r = socketSend.Receive(buffer); //返回實際接收到的有效bytes數。接收的數據需要存放在byte[]
                    if (r == 0) break; //如果遠端客戶端一關閉，就不要再接收了。
                    string str = Encoding.UTF8.GetString(buffer, 0, r); //把bytes轉成看得懂的字串，0從第一個開始讀，讀的長度為r
                    ShowMsg(socketSend.RemoteEndPoint + ":" + str);//顯示在文字框上。
                } catch { }
            }
        }

        void ShowMsg(string str)
        {
            //用Append追加文字。不用text，因為text會覆蓋掉之前的值
            txtLog.AppendText(str + "\r\n");
        }

        private void Server_Load(object sender, EventArgs e)
        {
            //取消跨線程的檢查
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        /// <summary>
        /// 服務端發送消息給客戶端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (IsSelectedIP() == false) return;

            string str = txtMsg.Text.Trim();
            byte[] buffer = Encoding.UTF8.GetBytes(str); //將訊息轉為byte[]

            //把要傳遞的位元組陣列byte[]前面都加上一個位元做為標識。0:表示文字、1:表示文件、2:震動
            List<byte> list = new List<byte>();
            list.Add(0); //第一個元素放0文字標記
            list.AddRange(buffer); //第二個元素放要傳送的訊息，buffer加到集合中
            byte[] newBuffer = list.ToArray(); //轉成byte[]。返回什麼陣列，取決於這個集合是什麼樣的類型

            //取得用戶下拉選單選中的IP位址
            string ip = cboUsers.SelectedItem.ToString()!;
            dicSocket[ip].Send(newBuffer); //發送消息。要把發送出去的消息轉成數組byte[]

            //socketSend.Send(buffer); //發送消息。要把發送出去的消息轉成數組byte[]
        }

        bool IsSelectedIP()
        {
            if (string.IsNullOrWhiteSpace(cboUsers.Text))
            {
                MessageBox.Show("請選擇客戶端的IP");
                return false;
            }
            return true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\rivalin\Desktop";
            ofd.Title = "請選擇要傳送的文件";
            ofd.Filter = "所有文件|*.*";
            ofd.ShowDialog();
            txtPath.Text = ofd.FileName;
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            if (IsSelectedIP() == false) return;

            //取得要發送文件的路徑
            string path = txtPath.Text.Trim();

            using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5]; //宣告byte[]陣列，存放讀進來的文件資料
                int r = fsRead.Read(buffer, 0, buffer.Length); //返回實際讀到的bytes數。把buffer放進來，0表示從頭開始讀，讀的長度為buffer長度

                //把要傳遞的位元組陣列byte[]前面都加上一個位元做為標識。0:表示文字、1:表示文件、2:震動
                List<byte> list = new List<byte>();
                list.Add(1); //1文件標識
                list.AddRange(buffer); //把buffer加入集合
                byte[] newBuffer = list.ToArray(); //轉為byte[]。返回什麼類型的陣列，取決於這個集合是什麼類型

                //發送文件。0從頭開始，長度r+1為返回實際讀到的bytes數再加上一個位元做為標識
                dicSocket[cboUsers.SelectedItem.ToString()!].Send(newBuffer, 0, r + 1, SocketFlags.None);
            }
        }

        private void btnShock_Click(object sender, EventArgs e)
        {
            if (IsSelectedIP() == false) return;
            byte[] buffer = new byte[1]; //送一個標記過去就可以了
            buffer[0] = 2; //2震動標識
            dicSocket[cboUsers.SelectedItem.ToString()!].Send(buffer);
        }
    }
}
```

## Client端

```c#
using System.Net;
using System.Net.Sockets;
using System.Text;

// Server: Socket() => Bind() => Listen() => Accept() => Recieve() => Send() 
// Client: Socket() => Connet() => Send() => Receive()
namespace Client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        Socket socketSend; //宣告Socket變數
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //Socket()創建負責通信的Socket
                socketSend = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //取得要連接遠端服務器的IP和Port
                IPAddress ip = IPAddress.Parse(txtIP.Text);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(txtPort.Text));

                //Connect()建立連接
                socketSend.Connect(point);
                ShowMsg("連接成功");

                //Recieve()接收數據
                Thread th = new Thread(Recieve); //開一個新的執行緒去執行Recieve方法
                th.IsBackground = true; //設為背景Thread。只要前景Thread全關閉，Thread就會關上自動關閉
                th.Start(); //告訴CPU我準備好了，隨時都可以執行(Start不是開始的意思)
            } catch { }
        }

        /// <summary>
        /// 不停的接收服務端發送過來的訊息
        /// </summary>
        void Recieve()
        {
            while (true) //要不停的去接收，所以用死循環
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 3]; //設置要存放的byte[]
                    int r = socketSend.Receive(buffer); //返回的是實際接收到的有效byte數。把收到的訊息存到byte[]
                    if (r == 0) break;//如果沒有接收到任何訊息，就跳出循環

                    //先拿到陣列第一位，判斷是什麼標記(0文字消息、1文件、2震動)
                    switch (buffer[0])
                    {
                        case 0: //文字
                            //把bytes轉成我們看得懂的字串。第一個元素不解析，1代表從第二個元素開始解析，長度為r-1
                            string str = Encoding.UTF8.GetString(buffer, 1, r - 1);
                            ShowMsg(socketSend.RemoteEndPoint + ":" + str);//訊息顯示在畫面上
                            break;
                        case 1: //文件
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.InitialDirectory = @"C:\Users\rivalin\Desktop";
                            sfd.Title = "儲存文件";
                            sfd.Filter = "所有文件|*.*";
                            sfd.ShowDialog(this);

                            string path = sfd.FileName;
                            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                fsWrite.Write(buffer, 1, r - 1); //1從第二個元素開始寫，寫的長度為r-1(實際長度-一位標記)
                            }
                            MessageBox.Show("儲存成功");
                            break;
                        case 2: //震動
                            Shock();
                            break;
                    }
                } catch { }
            }
        }
        /// <summary>
        /// 震動
        /// </summary>
        void Shock()
        {
            for (int i = 0; i < 200; i++)
            {
                //給兩個點，兩個坐標重新賦值，就會讓視窗感覺動起來
                this.Location = new Point(200, 200);
                this.Location = new Point(280, 280);
            }
        }

        void ShowMsg(string str)
        {
            //用Append追加文件。用Text會覆蓋掉原來的值
            txtLog.AppendText(str + "\r\n");
        }

        /// <summary>
        /// 客戶端發送消息給服務器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string str = txtMsg.Text.Trim();
            byte[] buffer = Encoding.UTF8.GetBytes(str); //把要發送出去的消息轉換成byte[]陣列
            socketSend.Send(buffer); //需要傳入byte[]
        }

        private void Client_Load(object sender, EventArgs e)
        {
            //取消跨執行緒的檢查
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            socketSend.Close();
        }
    }
}
```
