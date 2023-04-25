---
layout: post
title: "[C# 筆記] StreamReader & StreamWrite"
date: 2011-01-20 22:29:00 +0800
categories: [Notes,C#]
tags: [C#,StreamReader,StreamWrite]
---

## StreamReader、StreamWrite 和 FileStream 的區別

- `FileStream`操作位元組byte的  
可以處理所有的檔案、可以處理大檔案。  
- `StreamReader`&`StreamWrite`操作字元的  
只能處理文本的。

所以`FileStream`必須要掌握的。    

使用`using(){...}`兩個作用：  
- 關閉Stream
- 釋放Stream所占的資源  

## 使用StreamReader來讀取一個文本文件
```c#
using (StreamReader sr = new StreamReader(@"C:\Users\rivalin\Desktop\lala.txt"))
{
    //因為是一行一行的話，所以需要用迴圈
    //EndOfStream是否讀到結尾
    //!EndOfStream如果沒有讀到最後，就不停的讀, 不停的輸出
    while (!sr.EndOfStream)
	{
        Console.WriteLine(sr.ReadLine()); //sr.ReadLine一行一行的讀
    }
}
```
> 如果讀出有亂碼，就加上`Encoding.Default`    
using (StreamReader sr = new StreamReader(@"C:\Users\rivalin\Desktop\lala.txt", Encoding.Default))

## 使用StreamWrite來寫入一個文本文件

```c#
using (StreamWriter sw = new StreamWriter(@"C:\Users\rivalin\Desktop\new.txt"))
{
    sw.Write("今天天氣真好啊");
}
Console.WriteLine("done.");
```

## 重複寫入會覆蓋掉內容，設定Append=ture即可

設定`Append=ture`不會覆蓋掉原來的內容，只會追加在文本的最後  
StreamWriter sw = new StreamWriter(path, append: true)

```c#
using (StreamWriter sw = new StreamWriter(@"C:\Users\rivalin\Desktop\new.txt", true))
{
    sw.Write("我是新增的喔~");
}
Console.WriteLine("done.");
```


