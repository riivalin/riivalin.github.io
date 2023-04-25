---
layout: post
title: "[C# 筆記] 搖獎機"
date: 2011-01-27 22:09:00 +0800
categories: [Notes, C#]
tags: [C#,Thread,Random]
---

思路：
- 先實作讓搖獎機能夠不停的轉(使用死循環while(true))
- 加bool判斷搖獎機的轉動狀態，同一個button去做切換(開始&停止)

步驟：
- 先寫一個方法，用死循環while(true)讓它能不停轉
- 方法裡需要用到隨機數
- 開一個新的執行緒 Thread去執行這個方法 (Thread要設背景線程)
- 程序加載時，取消「跨線程的檢查」
- Button切換邏輯

使用技術：
- Thread
- Random
- Control.CheckForIllegalCrossThreadCalls = false;

## 先實作讓搖獎機能夠不停的轉
```c#
private void Form1_Load(object sender, EventArgs e)
{
    //lable是主線程創建的，
    //新線程無法跨線程訪問
    //所以會拋出異常
    //怎麼解決？
    //在程式加載時，取消檢查「跨線程的檢查」
    Control.CheckForIllegalCrossThreadCalls = false;
}

private void btnStart_Click(object sender, EventArgs e)
{
    //創建新的線程去執行PlayGame()方法
    Thread t = new Thread(PlayGame);
    t.IsBackground = true; //設為背景線程
    t.Start(); //告訴CPU線程己經準備好了，隨時可以執行它

    //不能直接調用，因為方法裡有個死循環
    //主線程去執行這個函式死循環，就會出不來
    //就會造成視窗卡死(假死)
    //所以要新開一個線程讓它去執行這個方法 
    //PlayGame(); 
}

void PlayGame()
{
    //建立一個產生隨機數的物件
    Random r = new Random();

    //不停轉，就是代表要不停的給值
    while (true) //死循環
    {
        label1.Text = r.Next(0, 10).ToString();
        label2.Text = r.Next(0, 10).ToString();
        label3.Text = r.Next(0, 10).ToString();
    }
}
```
> 不能直接調用PlayGame()，因為方法裡有個死循環  
  主線程去執行這個函式死循環，就會出不來  
  就會造成視窗卡死(假死)，  
  所以要新開一個線程讓它去執行這個方法  
  

## 實作搖獎機 完整版
加bool變數，判斷搖獎機的轉動狀態，讓按鈕切換可以開始和停止轉動

```c#
private void Form1_Load(object sender, EventArgs e)
{
    //lable是主線程創建的，
    //新線程無法跨線程訪問
    //所以會拋出異常
    //怎麼解決？
    //在程式加載時，取消檢查「跨線程的檢查」
    Control.CheckForIllegalCrossThreadCalls = false;
}

//搖獎機的轉動狀態
bool isSpining = false;
private void btnStart_Click(object sender, EventArgs e)
{
    //判斷搖獎機是否正在轉
    if (isSpining == false)
    {
        isSpining = true; //設為正在轉
        btnStart.Text = "停止";

        //創建新的線程
        Thread t = new Thread(PlayGame);
        t.IsBackground = true; //設為背景線程
        t.Start();
    } else //isSpining == true
    {
        isSpining = false;
        btnStart.Text = "開始";
    }

    //不能直接調用，因為方法裡有個死循環
    //主線程去執行這個函式死循環，就會出不來
    //就會造成視窗卡死(假死)
    //PlayGame(); 
}

private void PlayGame()
{
    //建立一個產生隨機數的物件
    Random r = new Random();

    //不停轉，就是代表要不停的給值
    while (isSpining) //死循環
    {
        label1.Text = r.Next(0, 10).ToString();
        label2.Text = r.Next(0, 10).ToString();
        label3.Text = r.Next(0, 10).ToString();
    }
}
```