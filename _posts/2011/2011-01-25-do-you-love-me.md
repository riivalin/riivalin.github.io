---
layout: post
title: "[C# 筆記][WinForm] Do you love me"
date: 2011-01-25 21:09:00 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---


```c#
private void btnLoveYou_Click(object sender, EventArgs e)
{
    MessageBox.Show("I Love You, too.");
    this.Close();
}

private void btnUnLove_Click(object sender, EventArgs e)
{
    MessageBox.Show("最終還是被你抓到了");
    this.Close();
}

private void btnUnLove_MouseEnter(object sender, EventArgs e)
{
    //給按鈕一個新的坐標
    //按鈕可移動的最大範圍: 視窗寬度-按鈕寬度
    int x = this.ClientSize.Width - btnUnLove.Width;
    int y = this.ClientSize.Height - btnUnLove.Height;

    //給按鈕一個隨機的坐標
    Random r = new Random(); //創建產生隨機數的物件
    btnUnLove.Location = new Point(r.Next(0, x + 1), r.Next(0, y + 1)); //新坐標
}
```