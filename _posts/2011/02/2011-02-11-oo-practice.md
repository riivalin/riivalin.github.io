---
layout: post
title: "[C# 筆記][多型] USB隨身碟 Mp3-抽象類練習"
date: 2011-02-11 00:01:31 +0800
categories: [Notes,C#]
tags: [C#,OO,物件導向,abstract,多型]
---

## 外部設備(抽象方法)
### 需求
模擬行動碟碟、USB隨身碟、MP3等行動儲存設備插到電腦上進行讀寫數據

### 設計思路(OO)
- 行動碟碟、USB隨身碟、MP3插到電腦上，他會自動讀寫嗎？不會。
- 最終你這個讀寫數據的函數，得由誰去調用？電腦來調用。
- 也就是說，在行動碟碟、USB隨身碟、MP3裡面，每一個類都有一方法。一個是讀、一個是寫。
- 為了咱們的程式可擴展性，我們應該讓這些子類，統一的去繼承一個父類。
- 因為他們裡面是不是都有相同的成員(讀/寫)，那邊就讓他們去繼承一個父類。
- 在父類當中，你應該提供兩個函數：一個是讀、一個是寫。
- 但父類不提供PlayMusic()，因為這是MP3自己獨有的
- 那你知道父類怎麼去讀？怎麼去寫嗎？不知道
- 因為每種設備讀寫數據的方式是各不相同的
- 所以說，你應該將這個父類標記為 `abstract` 抽象的
- 而父類的這兩個函數(讀/寫)也是 `abstract` 抽象的

```
父類 抽象的
- Read()
- Write()

子類
- MobileDisk
  Read()
  Write()

- UDisk
  Read()
  Write()

- MP3
  Read()
  Write()
  PlayMusic()
```

好，剩下的就寫我們的電腦。  
在電腦裡面也應該提供兩個方法去讀寫  
- 電腦這兩個函數：
    - 一個是調用插在我電腦身上的行動儲存設備「讀的方法」
    - 一個是調用插在我電腦身上的行動儲存設備「寫的方法」
    
也就是說，我在這個電腦裡面，我必須要想辦法獲得插在我電腦上面的行動儲存設備。    

你有幾種方式能夠在電腦這個類裡面拿到 插在電腦上面的行動儲存設備？
你電腦不知道誰會插上來，你只能按誰去處理？按父類去處理。    
也就是說，你這裡面要拿到一個父類的物件。    

你有幾種方式拿到父類？兩種。
- 第一種方式：把父類傳給這兩個函數
- 第二種方式：寫一個屬性來儲存 插到電腦上的行動設備
    - 這個屬性會是父類類型的屬性        

為什麼是父類類型的屬性？    
因為你根本就不知道誰會插上來。  
我們按父類來寫的話，是不是屏蔽了所有子類的差異呀。    

```
電腦
要獲得插在電腦上面的行動儲存設備

- CpuRead()  => 按 父類 去處理取得
- CpuWrite() => 按 父類 去處理取得

拿到父類方式
1.把父類傳給這兩個函數
  public void CpuRead(MobileStorage ms) {  ms.Read(); }
2.寫一個屬性來儲存 插到電腦上的行動設備。父類類型的屬性
  public MobileStorage MS { get; set; }
```     

### 開始實作
#### 父類別
承上，我們在寫的時候，應該先去寫誰呀？父類，行動儲存設備     

```c#
/// <summary>
/// 抽象的儲存設備父類
/// </summary>
public abstract class MobileStorage
{
    //在這個類裡面，我們只需要提供兩個抽象函數：讀/寫
    public abstract void Read();
    public abstract void Write();
}
```
#### 子類別
再寫 子類，子類繼承父類，並重寫父類的抽象方法

```c#
public class MobileDisk : MobileStorage
{
    //繼承了抽象類，就必須要重寫抽象類的成員 override
    public override void Read()
    {
        Console.WriteLine("行動硬碟在讀取數據");
    }
    public override void Write()
    {
        Console.WriteLine("行動硬碟在寫入數據");
    }
}

public class UsbDisk : MobileStorage
{
    public override void Read()
    {
        Console.WriteLine("USB隨身碟在讀取數據");
    }
    public override void Write()
    {
        Console.WriteLine("USB隨身碟在寫入數據");
    }
}

public class MP3 : MobileStorage
{
    //MP3除了重寫父類的 讀&寫函數 外，還多了一個播放音樂函數
    public void PlayMusic()
    {
        Console.WriteLine("MP3可以自己播放音樂");
    }
    public override void Read()
    {
        Console.WriteLine("MP3在讀取數據");
    }
    public override void Write()
    {
        Console.WriteLine("MP3在寫入數據");
    }
}
```
#### 電腦類別
最後寫電腦類別      
想辦法在電腦類別拿到父類，為什麼要拿到父類，因為我不知道哪一個子類會插在電腦上，所以由父類處理。
- 寫一個屬性來儲存 插到電腦上的行動設備。父類類型的屬性
- 當外部賦值給MS屬性了，讀寫方法這兩個地方相當於拿到行動設備了
- 我表面上雖然 調的是父類的讀取方法，實際上調的是子類的方法，因為你這個方法己經被子類重寫了
- 具體會調用哪個子類，取決於你把哪個物件賦值給這個MS屬性

```c#
//電腦類別不需要繼承父類，因為他是獨立於他們存在的一個東西
public class Computer
{
    //這兩個函數要去調用插在我電腦上儲存設備的讀寫方法
    //那我就必須要拿到父類物件，兩種方式：
    //1.傳參進來(把父類傳進來)
    //  public void CpuRead(MobileStorage ms) {  ms.Read(); }
    //2.寫一個屬性來儲存(父類類型的屬性)
    //  public MobileStorage MS { get; set; }

    /// <summary>
    /// 行動設備屬性
    /// 寫一個屬性來儲存 插到電腦上的行動設備。父類類型的屬性
    /// </summary>
    public MobileStorage MS { get; set; }
    public void CpuRead()
    {
        //當外部賦值給MS屬性了，這地方相當於拿到行動設備了
        //我表面上雖然 調的是父類的讀取方法
        //但是你這個方法己經被子類重寫了
        //具體會調用哪個子類，取決於你把哪個物件賦值給這個MS屬性
        this.MS.Read();
    }
    public void CpuWrite()
    {
        this.MS.Write();
    }
}
```
#### 開始模擬把行動裝置插到電腦上
現在來看實體化這個物件

```c#
//創建子類物件：行動碟碟、USB隨身碟、MP3
MobileDisk md = new MobileDisk();
UsbDisk usb = new UsbDisk();
MP3 mp3 = new MP3();

//開始模擬把行動裝置插到電腦上
//創建電腦物件
Computer computer = new Computer();

//這行代表 把子類插到電腦上
//我雖然需要父類，但我子類可以給他，為什麼？
//因為子類可以賦值給父類(里氏轉換)
computer.MS = usb; 

//當我我賦值給MS屬性了，computer.CpuRead()相當於拿到行動設備了
//我表面上雖然 調的是父類的讀取方法
//但是你這個方法己經被子類重寫了
//具體會調用哪個子類，取決於你把哪個物件賦值給這個MS屬性
computer.CpuRead();
computer.CpuWrite();

Console.ReadKey();
```

- [https://www.bilibili.com/video/BV1vG411A7my?p=47](https://www.bilibili.com/video/BV1vG411A7my?p=47)
- [https://www.bilibili.com/video/BV1vG411A7my?p=48](https://www.bilibili.com/video/BV1vG411A7my?p=48)