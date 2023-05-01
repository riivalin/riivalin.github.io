---
layout: post
title: "[C# 筆記][WinForm] 單例模式"
date: 2011-03-05 23:03:00 +0800
categories: [Notes, C#]
tags: [C#,singleton,WinForm]
---

## 單例模式
1. 將構造函數私有化
2. 提供一個靜態方法，返回一個對象
3. 創建一個單例

## Form1
```c#
private void button1_Click(object sender, EventArgs e)
{
    Form2 f = Form2.GetSingle();//new Form2();
    f.Show();
}
```

## Form2
```c#
//全局唯一的單例(全域靜態)
public static Form2 frmSingle = null;

//1.將構造函數私有化
private Form2()
{
    InitializeComponent();
}
//2.提供一個靜態方法，返回一個對象
public static Form2 GetSingle()
{
    //3.創建一個單例
    if (frmSingle == null) {
        frmSingle = new Form2();
    }
    return frmSingle;
}
```