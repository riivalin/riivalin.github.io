---
layout: post
title: "[C# 筆記][WinForm] 圖片上一張下一張"
date: 2011-01-25 21:39:00 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---


```c#
private void Form1_Load(object sender, EventArgs e)
{
    //當視窗載入時，載入第一張圖片
    pictureBox1.Image = Image.FromFile(@"C:\Users\rivalin\Desktop\images\1.jpg");
}

//記錄path的索引index
int i = 0;
//獲得指定的文件夾中所有文件的全路徑
string[] path = Directory.GetFiles(@"C:\Users\rivalin\Desktop\images");
/// <summary>
/// 點擊下一張圖片
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnNext_Click(object sender, EventArgs e)
{
    i++;
    //判斷是否超過索引值
    if (i == path.Length) {
        i = 0;
    }
    pictureBox1.Image = Image.FromFile(path[i]);
}
/// <summary>
/// 點擊上一張圖片
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnPrevious_Click(object sender, EventArgs e)
{
    i--;
    //判斷是否小於索引值
    if (i < 0) {
        i = path.Length - 1;
    }
    pictureBox1.Image = Image.FromFile(path[i]);
}
}
```