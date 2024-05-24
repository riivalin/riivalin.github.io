---
layout: post
title: "[C# 筆記] File 檔案存取"
date: 2021-07-04 06:09:00 +0800
categories: [Notes,C#]
tags: [C#,File]
---

File 類別：提供建立、複製、刪除、移動和開啟單一檔案的靜態方法，並協助 FileStream 物件的建立。[MSDN](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.file?view=net-8.0)      

File最大的缺點：只能讀小文件

# File 類別的基本操作

- File.Creat()：建立一個文件 (如果檔案已經存在，原本的檔案會被覆蓋掉。(也就是原本的內容就被清空))
- File.Delete()：刪除一個文件 (徹底刪除，資源回收筒也不會存在)
- File.Copy()：複製文件
- File.Move()：移動文件(剪下文件) (原本的文件就不在了)
- File.Exists()：判斷檔案是否存在

## 範例

```c#
//File.Exists 判斷檔案是否存在
if (!File.Exists(@"C:\test.txt")) {
    //do work here
    //File.Create(@"C:\test.txt");
}

//建立文件
//如果檔案已經存在，原本的檔案會被覆蓋掉。(也就是原本的內容就被清空)
File.Create(@"C:\test.txt");

//刪除文件
//徹底刪除，資源回收筒也不會存在
File.Delete(@"C:\test.txt");

//複製文件
//注意：new.txt不可以是 已經存在的檔案
File.Copy(@"C:\test.txt", @"C:\new.txt"); //要複製的文件,新文件名

//移動文件(剪下文件)
//原本的文件就不在了
File.Move(@"C:\1.txt", @"D:\test.txt"); //移動至d槽，檔名改為test.txt
```

[[C# 筆記] File 類別的基本操作  by R](https://riivalin.github.io/posts/2011/01/file/) 

# File 檔案讀寫

File最大的缺點：只能讀小文件

- 讀寫文本、影音、圖片...
    - File.ReadAllBytes()
    - File.WriteAllBytes()

- 只能讀文本
    - File.ReadAllLines()
    - File.ReadAllText()

> - `Encoding.UTF8.GetBytes()` 將 string 轉成 UTF8編碼的 byte[]
> - `Encoding.Default.GetString()`：將 byte[] 轉成 string       
> 將byte[]陣列中的每一個元素，都要按照我們指定的編碼格式解碼成字串           
> UTF-8 GB2312 GBK ASCII Unicode        

## 範例：讀寫文字檔、影音、圖片...

### File.ReadAllBytes() 讀檔

開啟二進位檔案，將檔案內容讀入位元組陣列，然後關閉檔案。

```c#
//File.ReadAllBytes() 讀檔
byte[] buffer = File.ReadAllBytes(@"C:\test.txt");

//將byte[]陣列中的每一個元素，都要按照我們指定的編碼格式解碼成字串  
//UTF-8 GB2312 GBK ASCII Unicode 
string s = Encoding.Default.GetString(buffer); //將 byte[] 轉成 string

//將結果輸出到控制台
Console.WriteLine(s);
```

- `Encoding.Default.GetString()`：將 byte[] 轉成 string
將byte[]陣列中的每一個元素，都要按照我們指定的編碼格式解碼成字串    
UTF-8 GB2312 GBK ASCII Unicode  

### File.WriteAllBytes() 寫檔

沒有這個文件的話，會給你新建一個，有的話，會給你覆蓋掉。

```c#
//要寫入的內容
string content = "今天天氣真好啊";

//需要將寫入的字串轉成byte[]陣列
byte[] buffer = Encoding.UTF8.GetBytes(content); //string轉成UTF8編碼的byte[]

//寫入檔案
File.WriteAllBytes(@"C:\test.txt", buffer);
```

- `Encoding.UTF8.GetBytes()` 將 string 轉成UTF8編碼的 byte[]


## 範例：只能讀寫文字檔

- File.ReadAllLines() 以行的形式讀出
- File.WriteAllLines() 以行的形式寫入
- File.ReadAllText() 讀取檔案中的所有文字
- File.WriteAllText() 如果檔案已經存在，內容會覆蓋過去
- File.AppendAllText() 如果檔案已經存在，內容不會覆蓋過去，會附加在後面


### File.ReadAllLines() 以行的形式讀出

```c#
//以Encoding.Default編碼方式讀取test.txt檔
string[] content = File.ReadAllLines(@"C:\Users\rivalin\Desktop\test.txt", encoding: Encoding.Default);

//將字串陣列內容輸出在控制台
foreach (var e in content) {
    Console.WriteLine(e);
}  
```

### File.WriteAllLines() 以行的形式寫入

建立新檔案，並於檔案中寫入一或多個字串，然後關閉檔案。

```c#
File.WriteAllLines(@"C:\test.txt", new string[] { "aaa", "bbb" });
```

### File.ReadAllText()

開啟文字檔，讀取檔案中的所有文字

```c#
//Encoding利用指定的編碼方式讀取檔案中的所有文字
string content = File.ReadAllText(@"C:\test.txt", encoding: Encoding.Default);
Console.WriteLine(content);
```

### File.WriteAllText()

建立新檔案，將內容寫入檔案，然後關閉檔案。 如果目標檔案已經存在，則會遭到截斷並覆寫。

```c#
File.WriteAllText(@"C:\test.txt", "今天天氣真好啊");
```

### File.AppendAllText() 內容不會覆蓋過去

有相同檔名的文件，內容不會覆蓋過去，寫入的文字只會追加在後面

```c#
//File.AppendAllText() 內容不會覆蓋過去
//有相同檔名的文件，內容不會覆蓋過去，寫入的文字只會追加在後面
File.AppendAllText(@"C:\Users\rivalin\Desktop\test.txt", "今天天氣真好啊");
```

[[C# 筆記] File 讀寫文件  by R](https://riivalin.github.io/posts/2011/01/file-read-write/) 

## 使用File類來讀取數據

讀數據提供了三個方法：

- ReadAllBytes 以二進制的形式讀取
- ReadAllLines 一行一行的讀取
- ReadAllText 把所有字串一次性讀進來

這三種方式在使用File類來讀數據的時候，有一個很大的特點，就是不管你的數據有多大，在讀的時候都是 **一次性讀進來**，那這就意味著當我操作大文件的時候，對我們的記憶體負荷特別大，
        
所以說：        
讀大文件的話，用 FileStream。       
讀小文件，用 File類 就可以搞定了。

- 大文件：`FileStream`
- 小文件：`File`


# Does it not expect to be UTF-8 ?

- On .NET Framework, it's your configured Windows code page. 
- On .NET Core, it is UTF-8.


[MSDN - System.Text.Encoding.Default 屬性](https://learn.microsoft.com/zh-tw/dotnet/fundamentals/runtime-libraries/system-text-encoding-default)        
[MSDN - Encoding.GetBytes 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.text.encoding.getbytes?view=net-8.0)        
[MSDN - File 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.file?view=net-8.0)        
[MSDN - 檔案和資料流 I/O](https://learn.microsoft.com/zh-tw/dotnet/standard/io/)     
[File by R](https://riivalin.github.io/tags/file/)      
[[C# 筆記] File 類別的基本操作  by R](https://riivalin.github.io/posts/2011/01/file/)       
[[C# 筆記] File 讀寫文件  by R](https://riivalin.github.io/posts/2011/01/file-read-write/)      
[[C# 筆記] File類、Path類、Directory類-複習  by R](https://riivalin.github.io/posts/2011/02/file-path-directory/)       
[[C# 筆記][File] 工資翻倍-練習   by R](https://riivalin.github.io/posts/2011/02/file-1/)        