---
layout: post
title: "[C# 筆記][FileStream] 使用 FileStream 來讀寫文件"
date: 2011-01-20 22:09:00 +0800
categories: [Notes, C#]
tags: [C#,FileStream]
---

## FileStream 和 File 區別
- `File` 是一下子都讀過來
- `FileStream` 是一點一點讀過來，對內存來說比較沒有壓力

## FileStream、StreamWriter、StreamReader區別
### FileStream
`FileStream` 操作位元組(byte)的  
代表可以操作任何文件  

### StreamReader 和 StreamWrite
`StreamReader`、`StreamWrite` 操作字元的  
只能操作文本文件  

它們都適合處理大文件，要處理小文件用File就夠了  

## 範例：使用 FileStream 來讀取數據
```c#
//靜態類：直接調方法
//非靜態類：建立物件->調方法

//創建FileStream物件
//FileMode.OpenOrCreate: 存在就打開，沒有就建立(防止拋異常)
//FileAccess.Read: 讀文件
FileStream fsRead = new FileStream(@"C:\Users\rivalin\Desktop\lala.txt", FileMode.OpenOrCreate, FileAccess.Read);

//位元組byte
byte[] buffer = new byte[1024 * 1024 * 5]; //5M

//讀取來的數據放到位元組陣列 buffer中，從頭開始讀，所以是0，讀取長度為buffer的長度
//返回實際讀到的有效byte[]數 ex: 3.8M
int r = fsRead.Read(buffer, 0, buffer.Length);

//byte[]陣列看不懂，所以要進行編碼，轉換為string
//將byte[]陣列中每一個元素按照指定的編碼格式 解碼成字串
string contents = Encoding.UTF8.GetString(buffer, 0, r); //r只解碼實際讀到byte[]數

//關閉Stream(流)
fsRead.Close();
//釋放Stream(流)所占用的資源
fsRead.Dispose();

Console.WriteLine(contents);
Console.ReadKey();
```
> FileStream 讀寫文件，用`Default`會出現亂碼，改成`UTF8`    
string contents = Encoding.UTF8.GetString(buffer, 0, r); //r只解碼實際讀到byte[]數

## 範例：使用 FileStream 來寫入數據
將創建文件流對象的過程寫在`using`當中，會自動的幫助我們釋放所占用的資源。  

`using{...}`幫我們做這兩件事：
- 關閉Stream(流)
- 釋放Stream(流)所占用的資源  

```c#
using (FileStream fsWrite = new FileStream(@"C:\Users\rivalin\Desktop\lala.txt", FileMode.OpenOrCreate, FileAccess.Write))
{
    //要寫入的字串
    string s = "看看我有沒有把你覆蓋掉";

    //需要將字串轉為byte[]
     byte[] buffer = Encoding.UTF8.GetBytes(s); //用Default會是亂碼

    //寫入檔案。從頭開始寫，所以是0。寫的長度為buffer長度
    fsWrite.Write(buffer, 0, buffer.Length);
}
Console.ReadKey();
```
> FileStream 寫入文件，用`Default`會出現亂碼，改成`UTF8`     
byte[] buffer = Encoding.UTF8.GetBytes(s);
    
---
  
## R Note:

```text
靜態類：直接調方法
非靜態類：建立物件 -> 調方法

FileStream 讀寫文件，用 Default 會出現亂碼，可以改成 UTF8  

字元 = 字符
位元組(byte) = 字節
```