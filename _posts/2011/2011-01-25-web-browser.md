---
layout: post
title: "[C# 筆記][WinForm] Web Browser 控件"
date: 2011-01-25 23:39:00 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---

思路：
Uri 沒用過的類別，去看看他的定義

```c#
private void button1_Click(object sender, EventArgs e)
{
    //webBroswer.Url 類型不是string,
    //沒用過的類別，怎麼辦？思路…
    //查看定義，他要的是uri類型
    //需要uri, 但uri怎麼來？移至定義看
    //public Uri(string uriString);
    //這是建構函數，什麼時候為調用建構函數？
    //創建物件new的時候就會調用
    string text = textBox1.Text;
    //Uri uri = new Uri(text); //無效uri，為什麼？因為沒有https://
    Uri uri = new Uri("https://" + text);
    webBroswer.Url = uri;
}
```
