---
layout: post
title: "[C# 筆記] Process & Thread"
date: 2011-01-27 21:39:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## Process 進程
```c#
using System.Diagnostics;
//取得當前程序正在運行的進程
Process.GetProcesses();

//通過進程去打開指定的應用程式
Process.Start("calc");
Process.Start("iexplore", "https://goole.com");

//通過進程打開你指定的文件
ProcessStartInfo psi = new ProcessStartInfo();
Process p = new Process();
p.StartInfo.FileName = @"C:\Users\rivalin\Desktop\lala.wav";
p.StartInfo.UseShellExecute = true;
p.Start();
```
## 進程和線程的關係？(Process & Thread)
- 一個進程包含多個線程  
(一個Process包含多個Thread)   
- 單線程容易造成假死現象

## 前台線程 & 後台線程
我們應該使用後台線程

## Thread.Start()
Start() 是告訴CPU這個線程已經準備好了，可以隨時可以被執行，但具體時間、執行時間，由CPU決定。

## Thread.Abort()
Abort() 終止線程，終止完成之後不能再Start()

## Thread.Sleep()
可以讓進程停止一段時間再運行

## 單線程的程序
不管是控制台、WinForm哪一個應用程式，當我們運行這個程序的時候，咱們的系統就會分配給我們一個主線程來運行這個程序，是一個單線程的程序

## 線程中如何訪問控件
.net 中默認不允許跨線程的，  
所以用新線程去訪問主線程創建的框架，就會拋異常。  
那怎麼樣才能不拋異常？  
在程式加載的時候，取消它「不允許跨線程」的檢查

## 線程執行帶參數的方法
如果線程執行的方法需要參數，那麼要求這個參數必須是object類型

```c#
//如果線程執行的方法需要參數，那麼要求這個參數必須是object類型
void button_Click()
{
    Thread t = new Thread(Test);
    t.IsBackground = true;
    t.Start("123"); //從這裡給值，帶值進去
    //Test();

}
//如果線程執行的方法需要參數，那麼要求這個參數必須是object類型
void Test(object s) //object類型。 不能這樣寫=>Test(string s)
{
    //雖然是object類型，但可以強制轉型
    string ss = (string)s;
    for (int i = 0; i < 10000; i++) {
        Console.WriteLine(i);
    }
}
```