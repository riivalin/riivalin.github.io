---
layout: post
title: "[C# 筆記][FileStream] 文件流-複習"
date: 2011-02-10 00:01:21 +0800
categories: [Notes,C#]
tags: [C#,FileStream,StreamReader,StreamWrite]
---

## 什麼是 FileStream 文件流
文件流 FileStream 和 File類 讀取文件的時候，有什麼區別呢？  

例如：兩個大水缸，如果我們把一個缸中的水倒入另一個水缸中，有兩種方式：
1. 直接把一個缸中的水舉起來倒入另一個缸中。 => `File類`
2. 用個桶來把一個缸中的水舀到另一個缸中。 => `FileStream`


`File類` 相當於第一種方式，`FileStream`相當於第二種方式。

我們在讀大數據的時候，最好是用第二種方式 `FileStream`去讀，對計算機的負荷比較小

## FileStream 讀取數據| StreamReader&StreamWrite

### `FileStream` 和 `StreamReader` & `StreamWrite`兩個本質的區別
- `FileStream` 操作byte(字節/位元組)的
- `StreamReader` & `StreamWrite` 操作char/字符/字元的

所以說，我們必須要掌握的是`FileStream`。

我們的 GC（Garbage Collection）垃圾回收器並不會幫我們去自動釋放這個文件流所佔的資源，所以說我們必須要手動的close和dispose，代碼一多就會忘了這兩行，所以我們乾脆就用 `using(){ ... }`包覆起來，讓他自動的去幫助我們釋放資源。    


### FileStream 讀取數據

```c#
using (FileStream fsRead = new FileStream(@"C:\Users\rivalin\Desktop\temp.txt", FileMode.OpenOrCreate, FileAccess.Read))
{
    //給定byte[]，用來存放讀取到的數據
    byte[] buffer = new byte[1024 * 1024 * 5];//5M
    //r代表本實際讀取到的有效 bytes/字節數/ 位元組數
    int r = fsRead.Read(buffer, 0, buffer.Length);
    //buffer轉換成我們所認識的字串
    string str = Encoding.UTF8.GetString(buffer, 0, r); //r只解碼有效的bytes數
    Console.WriteLine(str);
}
Console.ReadKey();
```
程式說明：
- 第一個參數：填上你要操作的那個文件的路徑
- 第二個參數：你要針對於這個文件做一個什麼樣的操作。`OpenOrCreate`是最保險的，有就打開，沒有就創建。如果用 `Open`沒有檔的話就會拋異常。
- 第三個參數：我們要對裡面的數據`Read`讀取。
- 我們在讀取的時候，是先把數據放到一個給定的`byte[]/字節數組/位元組陣列` 當中。
- `new byte[1024 * 1024 * 5]` 這表示我每次讀取幾M的數據？5M     
> 不建議這樣寫`new byte[fsRead.Length]`，如果是大檔案的話，就會跟 `File類`一樣，是一次全部讀進來，對內存負荷非常的大。

- 讀到的數據放到 5M 當中，從0開始，因為全部讀，所以讀buffer.Length 的長度
- 會返回一個 int類型，r 代表本實際讀取到的有效 bytes/字節數/位元組數。

為什麼一定要返回一個 r 呢？    
因為假設我這個文件就300KB，那我每次讀到多少呀？5M，也就是說，只有300KB是有效 btyes(字節數)，剩下的四組裡面全是空的。

- 再將這個 byte[](字節數組/位元組陣列) 轉換成我們所認識的字串


### FileStream 寫入數據
- 在寫的時候，是以`byte[]`(字節數組/位元組陣列)的形式寫入，所以你要把寫的那個東西(要寫入的數據)轉換成`byte[]`
- 寫入成功，但是會覆蓋掉原本的內容，怎麼辦？改`FileMode`的方式為 `Append` => `FileMode.Append`
    - `FileMode.OpenOrCreate`會覆蓋原本的內容
    - `FileMode.Append`追加在後面，不會覆蓋原本的內容

```c#
using (FileStream fsWrite = new FileStream(@"C:\Users\rivalin\Desktop\temp.txt", FileMode.OpenOrCreate, FileAccess.Write))
{
    //所以要將寫入的資料轉成byte[]
    string s = "今天天氣真好啊~";
    byte[] buffer = Encoding.UTF8.GetBytes(s);
    fsWrite.Write(buffer,0,buffer.Length); //以byte[]形式寫入
    Console.WriteLine("done.");
}
```
#### 使用 FileMode.Append 會將寫入的資料，寫在原本內容的最後面
```c#
using (FileStream fsWrite = new FileStream(@"C:\Users\rivalin\Desktop\temp.txt", FileMode.Append, FileAccess.Write)) { ... }
```
## StreamReader & StreamWrite
### StreamReader 
```c#
using (StreamReader sr = new StreamReader(@"C:\Users\rivalin\Desktop\temp.txt"))
    {
        while (!sr.EndOfStream) //沒有讀到stream流的結尾
        {
            //讀取一行 輸出一行
            Console.WriteLine(sr.ReadLine());
        }
    }
    Console.ReadKey();
```

#### FileStream+StreamReader 以下範例，這樣寫沒有必要...
- 第一個參數：可以給路徑，也可以給一個steam流，FileStream它是繼承Stream，所以我這裡的參數，完全可以給一個FileStream對象(物件)

FileStream 去讀數據，StreamReader去讀FileStream，這樣寫沒有必要，直接把路徑丟下來就好啦
```c#
//FileStream 去讀數據，StreamReader去讀FileStream
using (FileStream fsRead = new FileStream(@"C:\Users\rivalin\Desktop\temp.txt", FileMode.OpenOrCreate, FileAccess.Read)) //沒必要這樣寫，把path丟下去就可以了
{
    using (StreamReader sr = new StreamReader(fsRead))
    {
        while (!sr.EndOfStream) //沒有讀到stream流的結尾
        {
            //讀取一行 輸出一行
            Console.WriteLine(sr.ReadLine());
        }
    }
}
Console.ReadKey();
```

### StreamWrite

```c#
using (StreamWriter sw = new StreamWriter(@"C:\Users\rivalin\Desktop\temp.txt",true)) //加上true, 用Append追加，不會覆蓋原本的內容
    {
        sw.WriteLine("oao oao 哈哈");
        Console.WriteLine("done");
    }
    Console.ReadKey();
```