---
layout: post
title: "[C# 筆記][WinForm] Directory 類別"
date: 2011-01-25 22:09:00 +0800
categories: [Notes, C#]
tags: [C#]
---

File Path FileStream StreamRead StreamWrite
directory 文件夾 目錄

## CreateDirectory 建立文件夾
```c#
//建立文件夾
Directory.CreateDirectory(@"c:\a");
```

## Delete 刪除文件夾
```c#
//刪除文件夾
//目錄裡有資料刪除會拋異常，真的要刪除就要加true
Directory.Delete(@"c:\a", true); 
```
> 會徹底刪除，不會在資源回收筒裡

## Move 剪下文件夾
```c#
//剪下，不是copy
Directory.Move(@"c:\a", @"C:\Users\rivalin\Desktop\new");
```

## Directory.GetFiles 取得指定目錄下所有檔案的全路徑
可指定檔案類型：`"*.txt"`、`"*.jpg"`、`"*.wav"`
```c#
//取得指定目錄下所有檔案的全路徑
string[] path = Directory.GetFiles(@"C:\Users\rivalin\Desktop\new", "*.jpg"); //"*.txt"

//輸出看結果
for (int i = 0; i < path.Length; i++) {
    Console.WriteLine(path[i]);
}
//輸出:
//C:\Users\rivalin\Desktop\new\1.jpg
//C:\Users\rivalin\Desktop\new\23.jpg
//C:\Users\rivalin\Desktop\new\good.jpg
//C:\Users\rivalin\Desktop\new\temp.jpg
```
## Directory.GetDirectories 取得指定目錄上所有文件夾的全路徑
```c#
//取得指定目錄上所有文件夾的全路徑
string[] path = Directory.GetDirectories(@"C:\Users\rivalin\Desktop\new");

for (int i = 0; i < path.Length; i++) {
    Console.WriteLine(path[i]);
}
//輸出:
//C:\Users\rivalin\Desktop\new\a
//C:\Users\rivalin\Desktop\new\b
//C:\Users\rivalin\Desktop\new\c
//C:\Users\rivalin\Desktop\new\temp
```

## Directory.Exists 判斷指定的文件夾是否存在
```c#
//判斷指定的文件夾是否存在
if (Directory.Exists(@"C:\a\b")) {
    for (int i = 0; i < 10; i++) {
        Directory.CreateDirectory(@"C:\a\b\" + i);
    }
}
Console.WriteLine("done");
```