---
layout: post
title: "[C# 筆記] 簡單工廠和抽象類別-複習"
date: 2011-02-08 00:09:21 +0800
categories: [Notes,C#]
tags: [C#]
---

## 語法複習
## 註解
`//` `/**/` `///`
1. 單行註解 `//` 註解單行代碼
2. 多行註解 `/* 要註解的內容 */`
3. 文檔註解 `///`註解類別和方法
4. HTML `<!--要註解的內容-->`
5. CSS`/* 要註解的內容 */`

## 命名規範
- Camel駱駝命名：要求首單詞的或字母小寫，其餘單詞首字母大寫，變數/變量、欄位/字段
    - `int age` `string name` `char gender` `string highSchool` 
    - `int _chiness`
- Pascal帕斯卡命名：類別、方法、屬性名
    - `GetMax` `GetSum`

- 定義的變量或方法，名字要有意義
    - 方法名：動詞
        - `Write()` `Open()` `Close()` `Dispose()` `GetUserId()`...(方法都是要做一件事情)
    - 變量名：按功能命名、按方法的返回值內容命名
        - `userName = GetUserName();`

## 物件導向/面向對象

### 進程 process
```c#
//使用進程打開指定的文件
Process p = new Process();
p.StartInfo.FileName = @"C:\1.txt";// = psi;
p.StartInfo.UseShellExecute = true;
p.Start();
```

> OLD，很慢才打開
```c#
//old before vs2013 很慢才打開
ProcessStartInfo psi = new ProcessStartInfo(@"C:\1.txt");
Process p = new Process();
p.StartInfo = psi;
p.Start();
```

### 寫成OO概念
【需求】

```
1. 在控制台提示用戶要進入的硬碟路徑
D:\
2. 提示用戶輸入要打開的文件名稱
1.txt
=> 不曉得用戶會入什麼類型的文件，按照父類別去處理(抽象方法)
```

【OO概念】
<h3>父類：文件的父類</h3>
- OpenFile();打開文件
寫一個抽象方法(不知道用戶會輸入什麼類型的文件)  

```c#
public abstract void OpenFile(string extension, string fileName);
```

`public abstract void OpenFile(全路徑)` 
方法1：傳參  
方法2：寫屬性  


<h3>子類：</h3>
- .txt 只能打開txt文件
- .wmv 只能打開wmv文件
- .jpg 只能打開jpg文件

<h3>簡單工廠</h3>
不知道用戶會輸入什麼類型的文件，所以給用戶返回一個父類，但是父類中裝的肯定是子類對象(子類物件)。

## 程式碼(簡單工廠和抽象類別)

```c#
namespace OO練習
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("請輸入的硬碟路徑");
                string path = Console.ReadLine(); //D:\
                Console.WriteLine("請輸入要打開的文件名稱");
                string fileName = Console.ReadLine();//1.txt
                //文件全路徑: path+fileName

                //表面調的是父類，但實際上調的是子類的方法(子類己經重寫方法)
                FileFather ff = GetFile(fileName, path + fileName);
                ff.OpenFile();
                Console.ReadKey();
            }
        }

        public static FileFather GetFile(string fileName, string fullPath)
        {
            FileFather ff = null;
            string extension = Path.GetExtension(fileName); //取得副檔名
            switch (extension)
            {
                case ".txt":
                    ff = new TxtPath(fullPath); //返回父類，但是裝的是子類物件
                    break;
                case ".jpg":
                    ff = new JpgPath(fullPath);
                    break;
                case ".wmv":
                    ff = new WmvPath(fullPath);
                    break;
            }
            return ff;
        }
    }
    //父類
    public abstract class FileFather
    {
        public string FullPath { get; set; }
        public FileFather(string fullPath)
        {
            this.FullPath = fullPath;
        }
        public abstract void OpenFile();
    }
    //子類: txt
    public class TxtPath : FileFather
    {
        public TxtPath(string fullPath) : base(fullPath) { } //base繼承父類的構造函數
        public override void OpenFile() //base繼承父類的構造函數
        {
            //使用進程打開指定的文件
            Process p = new Process();
            p.StartInfo.FileName = this.FullPath;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }
    }
    public class WmvPath : FileFather
    {
        public WmvPath(string fullPath) : base(fullPath) { }
        public override void OpenFile()
        {
            //使用進程打開指定的文件
            Process p = new Process();
            p.StartInfo.FileName = this.FullPath;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }
    }
    public class JpgPath : FileFather
    {
        public JpgPath(string fullPath) : base(fullPath) { }
        public override void OpenFile()
        {
            //使用進程打開指定的文件
            Process p = new Process();
            p.StartInfo.FileName = this.FullPath;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }
    }
}
```