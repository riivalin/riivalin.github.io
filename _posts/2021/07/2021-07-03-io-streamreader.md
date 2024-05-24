---
layout: post
title: "[C# 筆記] 讀取、寫入txt文字檔（StreamReader & StreamWrite)"
date: 2021-07-03 06:09:00 +0800
categories: [Notes,C#]
tags: [C#,FileStream,StreamReader,StreamWrite]
---

- BinaryReader/BinaryWrite 以二進式方式讀取/寫入檔案內容
- StreamReader/StreamWrite 以特定的編碼方式讀取/寫入檔案內容


## StreamReader、StreamWrite 和 FileStream 的區別

- FileStream 操作位元組(byte)的，可以處理所有的檔案、可以處理大檔案。
- StreamReader 和 StreamWrite 操作字元的，只能處理文字檔的。

所以FileStream必須要掌握的。

### 使用`using區塊`兩個作用：
- 關閉Stream
- 釋放Stream所佔的資源

---

1. StreamReader/StreamWriter類別：用來處理資料流數據，提供了高效的流讀寫功能。可以直接用字串進行讀寫，而不用轉換成位元組陣列。

2. 特性
- FileStream 是操作位元組(byte)的，因此可以操作包括文字以外的其它各種文件；
- StreamReader、StreamWriter 是操作字元的，因此只能操作文字檔案；
- StreamReader、StreamWriter 是專門用來操作檔案的，如果只針對檔案的話，用StreamReader和StreamWriter要比FileStream方便的多。

3. FileStream 類別 操作的是位元組和位元組陣列，而 Stream類別 操作的是字元資料。

這是這兩種類別的一個重要區別，如果你是準備讀取byte數據的話，用StreamReader讀取然後用System.Text.Encoding.Default.GetBytes轉化的話，，則可能出現數據丟失的情況，如byte數據的個數不對等。因此**操作 byte資料時要用 FileStream**。
 
---

命名空間

```c#
using System.IO;
```
## 宣告方式

```c#
StreamReader sr = new StreamReader(Stream stream, Encoding encoding); //資料流,編碼方式
StreamReader sr = new StreamReader(string path, Encoding encoding); //路徑,編碼方式
```
為指定的資料流/檔名，初始化 StreamReader 類別的新實體，並使用預設的編碼（UTF-8）讀取檔案。      
預設的編碼方式是UTF-8，Encoding.Default 表示的編碼方式也是 UTF-8

## 寫入 txt 檔

```c#
//寫入txt檔
using(var sw = new StreamWriter(@"C:\Users\rivalin\Desktop\test.txt")) {
    sw.Write("今天天氣真好啊");
}
```

重複寫入會覆蓋掉內容。  
設定`Append=ture`不會覆蓋掉原來的內容，只會追加在文本的最後。

`var sw = new StreamWriter(@"C:\Users\rivalin\Desktop\test.txt", append: true)`

## 讀取txt檔

- sr.Read()：讀一個字元，返回的是 字元的十進位值
- sr.ReadToEnd()：讀取資料流中**所有的資料**
- sr.ReadLine()：讀取資料中的一行，回傳一整行的值
- EndOfStream 是否讀到結尾
- 如果讀出有亂碼，就加上 Encoding.Default

```c#
//如果讀出有亂碼，就加上Encoding.Default 
using (StreamReader sr = new StreamReader(@"C:\Users\rivalin\Desktop\test.txt", Encoding.Default)) 
{
    //讀法1：ReadToEnd()：讀取全部的資料
    Console.WriteLine(sr.ReadToEnd());

    //讀法2：ReadLine()：一次只讀一行
    while (!sr.EndOfStream) { //!EndOfStream 如果沒有讀到最後，就不停的讀, 不停的輸出
        Console.WriteLine(sr.ReadLine());
    }
}
```

        


[MSDN - 利用 Visual C# 讀取及寫入文字檔](https://learn.microsoft.com/zh-tw/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file)      
[MSDN - StreamWriter 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.streamwriter?view=net-8.0)      
[CSDN - C# StreamReader/StreamWriter类](https://blog.csdn.net/BYH371256/article/details/89352656)          
[[C# 筆記] StreamReader & StreamWrite  by R](https://riivalin.github.io/posts/2011/01/streamread-streamwrite/)        
[[C# 筆記][FileStream] 文件流-複習  by R](https://riivalin.github.io/posts/2011/02/filestream-1/)       
[[C# 筆記][FileStream] 使用 FileStream 來讀寫文件  by R](https://riivalin.github.io/posts/2011/01/file-stream/)     
[[C# 筆記][FileStream] 使用 FileStream 實現多媒體文件的複製 by R](https://riivalin.github.io/posts/2011/01/filestream-copyfile/)