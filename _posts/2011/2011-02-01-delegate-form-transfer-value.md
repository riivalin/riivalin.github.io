---
layout: post
title: "[C# 筆記][WinForm] Delegate 委派表單傳值"
date: 2011-02-01 01:19:00 +0800
categories: [Notes, C#]
tags: [C#,WinForm,Delegate]
---

- 表單配置：  
    - Form1: label + button
    - Form2: textbox + button

- 設計需求：  
在Form1按下button後，會顯示Form2  
在Form2的textbox填寫文字，按下button後  
文字會顯示在Form1的label上

- 實作思路：  

在Form1寫一個方法傳入參數可以顯示文字 => ShowMsg  
但Form1有方法沒有值，Form2有值沒有方法，怎麼辦？  
使用委派類型可以傳方法    

而在Form1我有new Form2，代表我可以透過構造函式傳方法給Form2  
在Form2聲明委派，並在構造函式中加一個參數，可以接收方法  
方法存哪？在Form2宣告一個字段(field)來存方法  
在構造函式中，將方法賦值給字段  
Form2按下button去調用方法

## Form1
在Form1寫一個方法傳入參數可以顯示文字 => ShowMsg  
但Form1有方法沒有值，Form2有值沒有方法，怎麼辦？  
使用委派類型可以傳方法    

而在Form1我有new Form2，代表我可以透過構造函式傳方法給Form2  
```c#
namespace 委派視窗傳值
{
    public partial class Form1 : Form
    {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //透過Form2的構造函數，把方法傳給Form2
            Form2 f = new Form2(ShowMsg); //傳入ShowMsg方法
            f.Show();
        }
        //form1有方法沒有值，form2有值沒有方法
        //所以需要在form2 使用委派，讓form1可以在new form2時傳入方法給他
        void ShowMsg(string str) {
            label1.Text = str;
        }
    }
}
```

## Form2
而在Form1我有new Form2，代表我可以透過構造函式傳方法給Form2  
在Form2聲明委派，並在構造函式中加一個參數，可以接收方法  
方法存哪？在Form2宣告一個字段(field)來存方法  
在構造函式中，將方法賦值給字段  
Form2按下button去調用方法

```c#
namespace 委派視窗傳值
{
    //聲明一個委派可以指向ShowMsg()方法
    public delegate void DelTest(string str);

    public partial class Form2 : Form
    {
        //宣告一個字段，它是誰呀，他是form1的ShowMsg()方法
        public DelTest _del; 
        public Form2(DelTest del) //讓Form2的構造函數可以傳入方法
        {
            this._del = del; //把傳進來的方法給_del字段
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _del(textBox1.Text); //調用方法
        }
    }
}

```
