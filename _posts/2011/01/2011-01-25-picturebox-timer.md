---
layout: post
title: "[C# 筆記][WinForm] PictureBox Timer 每秒鐘換一張圖"
date: 2011-01-25 21:59:00 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---

## Timer
timer: 在指定的時間間隔內做一件指定的事  
Interval：1000毫秒=1秒  
timer1.Enabled = true;  


## 程式碼
取得文件裡的每一個文件的完整路徑`Directory.GetFiles()`

```c#
private void Form1_Load(object sender, EventArgs e)
{
    //播放音樂
    SoundPlayer sp = new SoundPlayer();
    sp.SoundLocation = @"C:\Users\rivalin\Desktop\hi.wav";
    sp.Play();

    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
    pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
    pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
    pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
    //視窗載入時，給每一個pictureBox賦值圖片路徑
    pictureBox1.Image = Image.FromFile(@"C:\Users\rivalin\Desktop\images\1.jpg");
    pictureBox2.Image = Image.FromFile(@"C:\Users\rivalin\Desktop\images\1.jpg");
    pictureBox3.Image = Image.FromFile(@"C:\Users\rivalin\Desktop\images\1.jpg");
    pictureBox4.Image = Image.FromFile(@"C:\Users\rivalin\Desktop\images\1.jpg");
    pictureBox5.Image = Image.FromFile(@"C:\Users\rivalin\Desktop\images\1.jpg");
    pictureBox6.Image = Image.FromFile(@"C:\Users\rivalin\Desktop\images\1.jpg");
}

//取得文件裡的每一個文件的完整路徑
string[] path = Directory.GetFiles(@"C:\Users\rivalin\Desktop\images\");
int i = 0; //記錄path的index
Random r = new Random();//建立產生隨機數物件
private void timer1_Tick(object sender, EventArgs e)
{
    //每一秒鐘 換一張圖片
    i++;
    if (i == path.Length) i = 0;
    pictureBox1.Image = Image.FromFile(path[r.Next(0, path.Length)]);//(path[i]);
    pictureBox2.Image = Image.FromFile(path[r.Next(0, path.Length)]);
    pictureBox3.Image = Image.FromFile(path[r.Next(0, path.Length)]);
    pictureBox4.Image = Image.FromFile(path[r.Next(0, path.Length)]);
    pictureBox5.Image = Image.FromFile(path[r.Next(0, path.Length)]);
    pictureBox6.Image = Image.FromFile(path[r.Next(0, path.Length)]);
}
```