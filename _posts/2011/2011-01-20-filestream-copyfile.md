---
layout: post
title: "[C# 筆記][FileStream] 使用 FileStream 實現多媒體文件的複製"
date: 2011-01-20 22:19:00 +0800
categories: [Notes, C#]
tags: [C#,FileStream]
---

## 使用FileStream 實現多媒體文件的複製
- `FileStream`操作位元組byte的
可以處理所有的檔案、可以處理大檔案。  
- `StreamReader`&`StreamWrite`操作字元的
只能處理文本的。

所以`FileStream`必須要掌握的。

### 思路
先將要複製的多媒體文件讀取出來，然後寫入到你指定的位置  

## 設定來源路徑、目標路徑和調用方法
```c#
string source = @"C:\Users\rivalin\Desktop\lucky.wav"; //要複製檔案的路徑
string target = @"C:\Users\rivalin\Desktop\lucky.wav"; //複製完後要放置的路徑
CopyFile(source, target);
Console.WriteLine("Done");
```

## 寫一個「複製文件」的方法
```c#
/// <summary>
/// 將文件複製到另一個所指定的路徑
/// </summary>
/// <param name="source">要複製文的原路徑</param>
/// <param name="target">目標路徑</param>
public void CopyFile(string source, string target)
{
    //1.創建讀取Stream的物件
    using (FileStream fsRead = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Read))
    {
        //2.創建寫入Stream的物件
        using (FileStream fsWrite = new FileStream(target, FileMode.OpenOrCreate, FileAccess.Write))
        {
            //宣告要讀進緩衝區的大小
            byte[] buffer = new byte[1024 * 1024 * 5];//5M

            //.wav 有34.8M
            //因為文件可能會比較大，所以我們在讀取的時候，應該通過一個循環去讀取
            while (true)
            {
                //會返回實際讀到的byte數
                int r = fsRead.Read(buffer, 0, buffer.Length); //從頭開始讀，所以是0, 讀的長度為緩衝區的長度

                //返回一個0，意味什麼也讀不到實際，代表讀完了，就跳出迴圈
                if (r == 0) break;

                //寫入檔案
                fsWrite.Write(buffer, 0, r); //從頭開始讀，所以是0, 讀的長度為實際讀到的長度
            }
        }
    }
}
```