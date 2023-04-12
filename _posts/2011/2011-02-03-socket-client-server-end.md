---
layout: post
title: "[C# 筆記][WinForm] Socket-Client Server 結束-5"
date: 2011-02-03 00:00:09 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---

我Client客戶端要怎麼區分對方Server端發過來的是"文本文字"消息還是"文件"、還是"震動"？傳過去的都是bytes陣列，怎區分？

## 實現傳送文件
- 接收數據是文字？還是文件？震動？
- 設計「協議」
    - 把要傳遞的位元組陣列`byte[]`前面都加上一個位元做為標識。0：表示文字；1：表示文件
    - 即：文字：0+文字(位元組陣列`byte[]`表示)
    - 文件：1+文件的二進制信息
> 不管你發送什麼：文字、文件、震動，都是發送bytes位元組

Client端接收到`byte[]`的時候，先判斷第一個字元是0還1

```text
文字消息、文件、震動

byte[] buffer
buffer[0] = 0; //文字消息
buffer[0] = 1; //文件
buffer[0] = 2; //震動

對你發過來的第一個元素做判斷
if(buffer[0]==0) {
    按照文字
} else if(buffer[0]==1) {
    按照文件
} else {
    震動
}
```
但是，陣列的長度是不可變的，Server端發送時不可以`buffer[0]=0`會把原本第一個元素給覆蓋了，必須要用插入的。    
        
我可以再聲明一個新陣列，將陣列的長度為`buffer.Length+1`，先給`buffer[0]=0`，再通過循環，將值給新的buffer陣列，但要從`buffer[1]`開始，因為`buffer[0]`己經賦值了。

但很麻煩呀    

陣列的長度不可變，但集合的長度可以變，我可以把陣列加到集合裡面，集合也可以轉換為陣列，這事就解決了。(泛型集合)    

## Server端Send()

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

    List<byte> list = new List<byte>();
    list.Add(0); //第一個元素放0，代表"文字消息"的標記
    list.AddRange(buffer); //buffer加到集合裡
    //將泛型集合轉為陣列
    byte[] newBuffer = list.ToArray(); //返回什麼陣列，取決於這個集合是什麼樣的類型

    //獲得用戶在下拉框中選中的IP地址
    string ip = cboUsers.SelectedItem.ToString();
    dicSocket[ip].Send(newBuffer);
    //socketSend.Send(buffer); //把要發送出去的消息轉成數組byte[]
}
```

## Client端Recieve()-處理文字消息

```c#
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

            switch (buffer[0]) //先拿到陣列第一位，判斷是什麼標記(0文字消息、1文件、2震動)
            {
                case 0: //文字消息
                    string s = Encoding.UTF8.GetString(buffer, 1, r - 1); //第一個元素不解析，1代表從第二個元素開始解析，長度為r-1，將bytes轉成我們看得懂的字串
                    ShowMsg(socketSend.RemoteEndPoint + ":" + s); //訊息顯示在畫面上
                    break;
                case 1: //文件
                    break;
                case 2: //震動
                    break;
            }
        } catch { }
    }
}
```

## Server端-選擇要發送的文件
```c#
/// <summary>
/// 選擇要發送的文件
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnSelect_Click(object sender, EventArgs e)
{
    OpenFileDialog ofd = new OpenFileDialog();
    ofd.InitialDirectory = @"C:\Users\rivalin\Desktop"; //設定初始目錄
    ofd.Title = "請選擇您要傳送的文件";
    ofd.Filter = "所有文件|*.*";
    ofd.ShowDialog();
    txtPath.Text = ofd.FileName;
}
```

## Server端-發送的文件

```c#
/// <summary>
/// 發送文件
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnSendFile_Click(object sender, EventArgs e)
{
    //取得要發送文件的路徑
    string path = txtPath.Text;
    using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
    {
        byte[] buffer = new byte[1024 * 1024 * 5]; //宣告一個位元組陣列
        int r = fsRead.Read(buffer, 0, buffer.Length); //返回實際讀到的bytes數。把buffer放進來，0表示從頭開始讀，讀的長度為buffer長度

        List<byte> list = new List<byte>();
        list.Add(1); //第一個元素標記為1表示文件
        list.AddRange(buffer); //第二個元素放要傳送的buffer
        byte[] newBuffer = list.ToArray();//轉為byte[]

        dicSocket[cboUsers.SelectedItem.ToString()!].Send(newBuffer, 0, r + 1, SocketFlags.None); //拿到通信的Socket就可以把他Send出去了。從0開始，r+1表示實際長度r再加上第一元素
    }
}
```

## Client端-接收文件

```c#
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

            switch (buffer[0]) //先拿到陣列第一位，判斷是什麼標記(0文字消息、1文件、2震動)
            {
                case 0: //文字消息
                    string s = Encoding.UTF8.GetString(buffer, 1, r - 1); //第一個元素不解析，1代表從第二個元素開始解析，長度為r-1，將bytes轉成我們看得懂的字串
                    ShowMsg(socketSend.RemoteEndPoint + ":" + s); //訊息顯示在畫面上
                    break;
                case 1: //文件
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\Users\rivalin\Desktop";
                    sfd.Title = "請選擇要儲存的文件";
                    sfd.Filter = "所有文件|*.*";
                    sfd.ShowDialog(this);
                    string path = sfd.FileName;

                    using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fsWrite.Write(buffer, 1, r - 1); //從1開始寫，寫r-1的長度
                    }
                    MessageBox.Show("儲存成功!");
                    break;
                case 2: //震動
                    break;
            }
        } catch { }
    }
}
```
TODO:
1. 儲存文件時不知道副檔名，要再寫
2. 發送大文件

## Server端-發送震動
震動也不用發送什麼位元組bytes，就發送一個"標記"過去就好了

```c#
/// <summary>
/// 發送震動
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnShock_Click(object sender, EventArgs e)
{
    byte[] buffer = new byte[1]; //發送"標記"就好了，只要一個元素，長度1
    buffer[0] = 2; //震動的標記為2
    dicSocket[cboUsers.SelectedItem.ToString()!].Send(buffer); 
}
```
## Client端-接收震動

1. 寫一個震動方法，怎麼寫？給兩個點，兩個坐標重新賦值，就會讓視窗感覺動起來
2. 接收時去調用震動方法

### 震動方法
```c#
/// <summary>
/// 震動
/// </summary>
void Shock()
{
    for (int i = 0; i < 500; i++)
    {   //給兩個點，兩個坐標重新賦值，就會讓視窗感覺動起來
        this.Location = new Point(200, 200);
        this.Location = new Point(280, 280);
    }
}
```

### 接收震動-調用震動方法

```c#
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

            switch (buffer[0]) //先拿到陣列第一位，判斷是什麼標記(0文字消息、1文件、2震動)
            {
                case 0: //文字消息
                    string s = Encoding.UTF8.GetString(buffer, 1, r - 1); //第一個元素不解析，1代表從第二個元素開始解析，長度為r-1，將bytes轉成我們看得懂的字串
                    ShowMsg(socketSend.RemoteEndPoint + ":" + s); //訊息顯示在畫面上
                    break;
                case 1: //文件
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\Users\rivalin\Desktop";
                    sfd.Title = "請選擇要儲存的文件";
                    sfd.Filter = "所有文件|*.*";
                    sfd.ShowDialog(this);
                    string path = sfd.FileName;
                    using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fsWrite.Write(buffer, 1, r - 1); //從1開始寫，寫r-1的長度
                    }
                    MessageBox.Show("儲存成功!");
                    break;
                case 2: //震動
                    Shock();
                    break;
            }
        } catch { }
    }
}
```

## Socket通信基本流程圖
![](/assets/img/post/socket-flowchart.png)

## 完整Code
### Client
```c#
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
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

                    switch (buffer[0]) //先拿到陣列第一位，判斷是什麼標記(0文字消息、1文件、2震動)
                    {
                        case 0: //文字消息
                            string s = Encoding.UTF8.GetString(buffer, 1, r - 1); //第一個元素不解析，1代表從第二個元素開始解析，長度為r-1，將bytes轉成我們看得懂的字串
                            ShowMsg(socketSend.RemoteEndPoint + ":" + s); //訊息顯示在畫面上
                            break;
                        case 1: //文件
                            SaveFileDialog sfd = new SaveFileDialog();
                            sfd.InitialDirectory = @"C:\Users\rivalin\Desktop";
                            sfd.Title = "請選擇要儲存的文件";
                            sfd.Filter = "所有文件|*.*";
                            sfd.ShowDialog(this);
                            string path = sfd.FileName;
                            using (FileStream fsWrite = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                            {
                                fsWrite.Write(buffer, 1, r - 1); //從1開始寫，寫r-1的長度
                            }
                            MessageBox.Show("儲存成功!");
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
            for (int i = 0; i < 500; i++)
            {  //給兩個點，兩個坐標重新賦值，就會讓視窗感覺動起來
                this.Location = new Point(200, 200);
                this.Location = new Point(280, 280);
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
### Server
```c#
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
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

        void Listen1(object o)//必須是object類型
        {
            Socket socketWatch = o as Socket; //用as強制轉型
            while (true)
            {
                try
                {
                    //Accept()-等待客戶端的連接，並且創建跟客戶端通信的Socket
                    socketSend = socketWatch.Accept();
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
                } catch
                {

                }

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

            List<byte> list = new List<byte>();
            list.Add(0);
            list.AddRange(buffer); //buffer加到集合裡
            //將泛型集合轉為陣列
            byte[] newBuffer = list.ToArray(); //返回什麼陣列，取決於這個集合是什麼樣的類型


            //獲得用戶在下拉框中選中的IP地址
            string ip = cboUsers.SelectedItem.ToString();
            dicSocket[ip].Send(newBuffer);
            //socketSend.Send(buffer); //把要發送出去的消息轉成數組byte[]
        }

        /// <summary>
        /// 選擇要發送的文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\Users\rivalin\Desktop"; //設定初始目錄
            ofd.Title = "請選擇您要傳送的文件";
            ofd.Filter = "所有文件|*.*";
            ofd.ShowDialog();
            txtPath.Text = ofd.FileName;
        }
        /// <summary>
        /// 發送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            //取得要發送文件的路徑
            string path = txtPath.Text;
            using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024 * 5]; //宣告一個位元組陣列
                int r = fsRead.Read(buffer, 0, buffer.Length); //返回實際讀到的bytes數。把buffer放進來，0表示從頭開始讀，讀的長度為buffer長度

                List<byte> list = new List<byte>();
                list.Add(1); //第一個元素標記為1表示文件
                list.AddRange(buffer); //第二個元素放要傳送的buffer
                byte[] newBuffer = list.ToArray();//轉為byte[]

                dicSocket[cboUsers.SelectedItem.ToString()!].Send(newBuffer, 0, r + 1, SocketFlags.None); //拿到通信的Socket就可以把他Send出去了。從0開始，r+1表示實際長度r再加上第一元素
            }
        }

        /// <summary>
        /// 發送震動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShock_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1]; //發送"標記"就好了，只要一個元素，長度1
            buffer[0] = 2; //震動的標記為2
            dicSocket[cboUsers.SelectedItem.ToString()!].Send(buffer); 
        }
    }
}
```

[https://www.bilibili.com/video/BV17G4y1b78i?p=199](https://www.bilibili.com/video/BV17G4y1b78i?p=199)