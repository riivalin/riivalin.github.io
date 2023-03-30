---
layout: post
title: "[C# 筆記] 多態-抽象類 Abstract：模擬行動硬碟、隨身碟、MP3"
date: 2011-01-21 22:19:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 模擬行動硬碟、隨身碟、MP3

用多態來實現 將 行動硬碟、隨身碟、MP3    
插到電腦上進行讀寫資料    


- 父類：可移動儲存設備：：Read(), Write()
- 子類：
  - 行動硬碟：Read(), Write()
  - 隨身碟：Read(), Write()
  - MP3：Read(), Write(), PlayMusic()
- Computer: CpuRead(), CpuWrite()

表面上是父類在調用  
但事實上調是子類方法  

```c#
//用多態來實現 將 行動硬碟、隨身碟、MP3插到電腦上進行讀寫資料

//1.這樣寫，沒有多態的感覺
//MobileDisk md = new MobileDisk(); //創建行動硬碟物件
//UDisk ud = new UDisk(); //創建隨身碟物件
//MP3 mp3 = new MP3(); //創建MP3物件
//Computer computer = new Computer(); //創建電腦物件
//computer.CpuRead(ud); //隨身碟插在電腦上讀取資料
//computer.CpuWrite(ud); //隨身碟插在電腦上寫入資料

//2.Computer傳參數方式拿到父類
//MobileStorage ms = new MP3();//new MobileDisk();//new UDisk(); //宣告父類去指向子類物件
//Computer computer = new Computer(); //建立電腦物件
//computer.CpuRead(ms); //要的是父類類型，但我父類裡面裝的是子類類型
//computer.CpuWrite(ms);


//3.Computer用屬性的方式拿到父類
MobileStorage ms = new MP3(); //new MobileDisk();//new UDisk();//宣告父類去指向子類物件
Computer computer = new Computer(); //建立電腦物件
computer.MS = ms; //電腦插的是mp3。電腦的行動儲存屬性是MobileStorage，但裡面裝的是MP3物件
computer.CpuRead();
computer.CupWrite();
Console.ReadKey();

/// <summary>
/// 抽象的父類
/// </summary>
public abstract class MobileStorage
{
    //先寫一個父類的行動儲存類別
    //因為不知道每個子類的讀寫方式是什麼
    //所以用抽象類，讓子類去重寫方法
    public abstract void Read();
    public abstract void Write();
}
/// <summary>
/// 子類:行動硬碟
/// </summary>
public class MobileDisk : MobileStorage
{
    //重寫父類的抽象方法Read
    public override void Read()
    {
        Console.WriteLine("行動硬碟在讀取");
    }
    public override void Write()
    {
        Console.WriteLine("行動硬碟在寫入");
    }
}

/// <summary>
/// 子類:隨身碟
/// </summary>
public class UDisk : MobileStorage
{
    public override void Read()
    {
        Console.WriteLine("隨身碟在讀取");
    }
    public override void Write()
    {
        Console.WriteLine("隨身碟在寫入");
    }
}

/// <summary>
/// 子類：MP3
/// </summary>
public class MP3 : MobileStorage
{
    public override void Read()
    {
        Console.WriteLine("MP3在讀取");
    }
    public override void Write()
    {
        Console.WriteLine("MP3在寫入");
    }
    public void PlayMusic()
    {
        Console.WriteLine("MP3可以自己播放音樂");
    }
}

public class Computer
{
    //在這裡拿到父類
    private MobileStorage _ms; //欄位:用來保護屬性的
    public MobileStorage MS
    {
        get { return _ms; }
        set { _ms = value; }
    }

    public void CpuRead()
    {
        this.MS.Read();
    }
    public void CupWrite()
    {
        this.MS.Write();
    }

    //使用屬性，就不用傳參數進去方法裡
    
    /// <summary>
    /// 電腦CPU讀取
    /// </summary>
    /// <param name="ms">雖然是父類物件，但可以傳入子類物件</param>
    //public void CpuRead(MobileStorage ms)
    //{
    //    //必須在這裡面拿到父類，怎麼拿? 傳參數進來
    //    //要的是父類類型，但我父類裡面裝的是子類類型
    //    //表面上是調父類的，事實上是已經被子類重寫了
    //    //所以最終你調的是誰？是你傳的那個子類物件的函式
    //    ms.Read();
    //}
    //public void CpuWrite(MobileStorage ms)
    //{
    //    ms.Write();
    //}
}
```