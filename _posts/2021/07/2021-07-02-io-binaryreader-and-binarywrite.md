---
layout: post
title: "[C# 筆記] 讀取、寫入txt文字檔（BnaryReader & BinaryWrite)"
date: 2021-07-02 06:09:00 +0800
categories: [Notes,C#]
tags: [C#,FileStream,BinaryReader,StreamReader,File]
---

- `BinaryReader`：把指定的資料流(Stream)當成「二進位」值讀取。
- `BinaryWrite`：把資料用「二進位」方式寫進資料流(Stream)。

`BinaryReader` 是把指定的資料流當成二進位值讀取，當然二進位值以不同的編碼方式讀取的意義就會不同。       
例如：1 byte 的資料以整數的角度 跟以字元的角度 去看(讀取方式)就會有不同的結果。     

`BinaryWrite`提供特定編碼方式，以「二進位」的方式寫入指定的資料流。

---

C#的FileStream類別提供了最原始的位元組層級上的文件讀取和寫入功能，但我們習慣於對字串操作，於是StreamWriter和StreamReader類別增強了FileStream，它讓我們在字串層級上操作文件，

但有的時候我們還是需要在位元組層級上操作文件，卻又不是一​​個位元組一個位元組的操作，通常是2個、4個或8個位元組這樣操作，這便有了BinaryWriter和BinaryReader類，它們可以將一個字元或數字按指定個數byte寫入，也可以一次讀取指定個數byte轉為字元或數字。

(BinaryWriter 和 BinaryReader 類別用於讀取和寫入數據，而不是字串。)

- BinaryReader/BinaryWrite 以二進式方式 讀取/寫入檔案內容
- StreamReader/StreamWrite 以特定的編碼方式讀 取/寫入檔案內容


## 宣告方式

```c#
//BinaryReader和BinaryWrite 一開始需要接受資料流(stream) 才能進行相關操作
BinaryReader reader = new BinaryReader(stream名稱);
BinaryWrite write = BinaryWrite(stream名稱, 編碼方式)
```

### 命名空間

```c#
using System.IO;
```

### 用File開啟檔案

```c#
//用File開啟檔案
var stream = File.Open(path, FileMode.OpenOrCreate);
var reader = new BinaryReader(stream, Encoding.UTF8);
```

### 用 FileStream 開啟檔案

```c#
//用File開啟檔案
var fs = FileStream.Open(path, FileMode.OpenOrCreate);
var reader = new BinaryReader(fs, Encoding.UTF8);
```

## 寫入檔案

- `BinaryWrite`：把資料用「二進位」方式寫進資料流(Stream)。

```c#
//宣告FileStream，為開啟test.txt的檔案資料流，選擇FileMode.OpenOrCreate最保險，如果檔案不存在，就會新增一個
using (var fs = new FileStream(@"C:\Users\rivalin\Desktop\test.txt", FileMode.OpenOrCreate))
{
    //宣告BinaryReader用來寫入檔案
    using (var w = new BinaryWriter(fs, Encoding.UTF8, false))
    {
        //寫入不同資料型態的資料
        w.Write(1.250F);
        w.Write(@"c:\Temp");
        w.Write(10);
        w.Write(true);

        //寫入整數0~10
        for (int i = 0; i < 11; i++) {
            w.Write(i);
        }
    }
}
```


## 讀取檔案內容

- `BinaryReader`：把指定的資料流(Stream)當成「二進位」值讀取。

BinaryReader 以特定的編碼方式，將基本資料型別當做二進位值讀取。

```c#
float aspectRatio;
string tempDirectory;
int autoSaveTime;
bool showStatusBar;

//宣告FileStream，來開啟test.txt的檔案資料流
using (var fs = new FileStream(@"C:\Users\rivalin\Desktop\test.txt", FileMode.Open))
{
    //宣告BinaryReader的方式讀取資料 (把資料流當作二進制值讀取)
    using (var reader = new BinaryReader(fs, Encoding.UTF8, false))
    {
        aspectRatio = reader.ReadSingle();
        tempDirectory = reader.ReadString();
        autoSaveTime = reader.ReadInt32(); //以整數方式讀取4bytes並傳回整數值，最後將指標往後移4bytes
        showStatusBar = reader.ReadBoolean(); //以布林值的方式讀取1byte並傳回，並將指標移到下一個byte

        for (int i = 0; i < 11; i++) {
            Console.Write(reader.ReadInt32());
        }
    }
}
Console.WriteLine(Environment.NewLine);
Console.WriteLine($"Aspect ratio set to: {aspectRatio}");
Console.WriteLine($"Temp directory is: {tempDirectory}");
Console.WriteLine($"Auto save time set to: {autoSaveTime}");
Console.WriteLine($"Show status bar: {showStatusBar}");
```

執行結果：

```
012345678910

Aspect ratio set to: 1.25
Temp directory is: c:\Temp
Auto save time set to: 10
Show status bar: True
```

## 什麼是 FileStream 檔案資料流(文件流)？


## FileStream 和 File 區別

- File - 提供建立、複製、刪除、移動和開啟檔案的靜態方法，並協助建立 FileStream 物件。
- FileStream – 用於讀取和寫入檔案。
        
- 要處理小文件用File就夠了
- FileStream 操作位元組(byte)的，代表可以操作任何文件

---

- File 是一下子都讀過來
- FileStream 是一點一點讀過來，對內存來說比較沒有壓力

---

#### 檔案資料流 FileStream 和 File類  讀取文件的時候，有什麼區別呢？

例如：兩個大水缸，如果我們把一個缸中的水倒入另一個水缸中，有兩種方式：

- 直接把一個缸中的水舉起來倒入另一個缸中。 => File類    
- 用個桶來把一個缸中的水舀到另一個缸中。 => FileStream  

File類 相當於第一種方式，FileStream相當於第二種方式。

我們在讀大數據的時候，最好是用第二種方式 FileStream去讀，對計算機的負荷比較小

---


File：
- File類，是一個靜態類，支援對文件的基本操作，包括創建，拷貝，移動，刪除和打開一個文件。 
- File類別方法的參數很多時候都是路徑path。主要提供有關檔案的各種操作，在使用時需要引用System.IO命名空間。

FileStream：
- FileStream 檔案資料流 只能處理原始位元組(raw byte)。 
- FileStream 類別可以用於任何資料文件，而不僅僅是文字文件。 
- FileStream 物件可以用於讀取諸如圖像和聲音的文件，
- FileStream 讀取出來的是位元組陣列，然後透過編碼轉換將位元組數組轉換成字串。

區別：
- file:是一個文件的類別，對文件進行操作的；
- filestream:檔案資料流(文件流)。對txt, xml等文件寫入內容的時候需要使用的一個工具.



[MSDN - 檔案和資料流 I/O](https://learn.microsoft.com/zh-tw/dotnet/standard/io/)       
[MSDN - BinaryReader 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.binaryreader?view=net-8.0)        
[MSDN - FileStream 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.filestream?view=net-8.0)        
[BinaryWriter和BinaryReader用法](https://www.cnblogs.com/wang7/archive/2012/05/17/2506701.html)
[[C# 筆記][FileStream] 文件流-複習  by R](https://riivalin.github.io/posts/2011/02/filestream-1/)       
[[C# 筆記][FileStream] 使用 FileStream 來讀寫文件  by R](https://riivalin.github.io/posts/2011/01/file-stream/)     
[[C# 筆記][FileStream] 使用 FileStream 實現多媒體文件的複製 by R](https://riivalin.github.io/posts/2011/01/filestream-copyfile/)