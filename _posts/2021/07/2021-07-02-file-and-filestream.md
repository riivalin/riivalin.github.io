---
layout: post
title: "[C# 筆記] FileStream(檔案資料流)的用法"
date: 2021-07-02 06:19:00 +0800
categories: [Notes,C#]
tags: [C#,FileStream,File,using]
---


# 什麼是 FileStream 檔案資料流(文件流)？

FileStream 類別：主要用於對檔案進行讀取、寫入、開啟和關閉操作，並對其他與檔案相關的作業系統句柄進行操作，如管道、標準輸入和標準輸出。讀寫操作可以指定為同步或非同步操作。 FileStream 對輸入輸出進行緩衝，從而提高效能。 [MSDN](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.filestream?view=net-8.0)

> 簡單點說：`FileStream`類別 可以對**任意類型的檔案**進行讀取操作，可根據需要來指定每次讀取的位元組長度（這是比`File`類別優秀的地方之一），以此減少記憶體的消耗，提高讀取效率。



# FileStream 和 File 區別

- `File` 是一個 靜態類別；
- `FileStream` 是一個 非靜態類別。

最直接差異：將讀取文件比喻為是從A桶往B桶倒水。使用File就是整個用桶子倒進去，使用FileStream就是使用水管慢慢輸送，應用場景不同，大檔案推薦FileStream，不會炸記憶體。

`File`：是一個文件的類別，對文件進行操作。其內部封裝了對檔案的各種操作      
(MSDN: 提供用於建立、複製、刪除、移動和開啟單一檔案的靜態方法，並協助建立FileStream物件)。

`FileStream`：是一個文件流的類別，處理文件的原始位元組(byte)，即處理byte[]。      
對txt，xml，avi等任何檔案進行內容寫入、讀取、複製...

---

- File - 提供建立、複製、刪除、移動和開啟檔案的靜態方法，並協助建立 FileStream 物件。
- FileStream – 用於讀取和寫入檔案。
        
- 要處理小文件用File就夠了
- FileStream 操作位元組(byte)的，代表可以操作任何文件

---

##  FileStream 和 File 讀取文件的時候，有什麼區別呢？

例如：兩個大水缸，如果我們把一個缸中的水倒入另一個水缸中，有兩種方式：

- 直接把一個缸中的水舉起來倒入另一個缸中。 => File類    
- 用個桶來把一個缸中的水舀到另一個缸中。 => FileStream  

File類 相當於第一種方式，FileStream相當於第二種方式。

我們在讀大數據的時候，最好是用第二種方式 FileStream去讀，對記憶體的負荷比較小
        
- File 是一下子都讀過來
- FileStream 是一點一點讀過來，對記憶體來說比較沒有壓力

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


# FileStream 的用法
## 用using 主動釋放記憶體(釋放資源)

`using`有兩個主要用途：

1. 作為指令，用於為命名空間建立別名或匯入其他命名空間中定義的類型。     
2. 作為語句，用於定義一個範圍，在此範圍的末尾將釋放對象，using 的物件必須是實現IDisposable介面的。

這裡用到了第二種，如果一個類別實現了介面 IDisposable（這個介面只有一個方法void Dispose()）,當這個類在using中創建的時候，using區塊 結束時會自動調用這個類中實現了介面IDisposable的Dispose()方法，也就是釋放資源。一般來說，文件流都要主動釋放資源的，因為讀寫文件是會加鎖的，不釋放的話，別的程式就無法使用文件了，這也就是FileStream一般和using配套使用的原因。當然，也可以不用using，但用完後需要顯示呼叫三行程式碼：

```c#
//不用using就要手動釋放資源
fs.Flush();//清除緩衝區
fs.Close();//關閉
fs.Dispose();//釋放
```

## 範例: 讀取文件

```c#
static void Main(string[] args)
{
    using (FileStream fs = new FileStream(@"C:\Users\rivalin\Desktop\test.txt", FileMode.OpenOrCreate, FileAccess.Read))
    {   //在using中建立FileStream物件fs，然後執行大括號內的程式碼，
        //執行完後，釋放被using的物件fs（後台自動呼叫了Dispose）
        byte[] vs = new byte[1024]; //陣列大小依自己喜歡設定，太高佔記靜體，太低讀取慢。
        while (true) //因為檔案可能很大，而我們每次只讀取一部分，因此需要讀很多次
        {
            int r = fs.Read(vs, 0, vs.Length);
            string s = Encoding.UTF8.GetString(vs, 0, r);
            Console.WriteLine(s);

            if (r == 0) break; //當讀取不到，跳出循環
        }
    }
    Console.ReadKey();
}
```

- `FileStream fs = new FileStream(@"C:\Users\rivalin\Desktop\test.txt", FileMode.OpenOrCreate, FileAccess.Read)`

這個構造函數有很多重載，上面用的是最常用的一個。第一個參數填路徑；第二個參數選擇形式，選擇`OpenOrCreate`最保險，檔案不存在就建立一個；第三個參數選read，即讀取。

- `byte[] vs = new byte[1024]`

新緩存陣列，數組大小根據自己喜歡設定（注意：太高佔記憶體，太低讀取慢。）

- 循環讀取數據，對應while循環（因為文件可能很大，而我們每次只讀取一部分，因此需要讀很多次，當讀取完後，跳出循環）


## 範例：寫入文件

```c#
static void Main(string[] args)
{
    string s = "寫入檔案寫入檔案寫入檔案寫入檔案寫入檔案寫入檔案";
    using (FileStream fs = new FileStream(@"C:\Users\rivalin\Desktop\test.txt", FileMode.OpenOrCreate, FileAccess.Write))
    {   //在using中建立FileStream物件fs，然後執行大括號內的程式碼，
        //執行完後，釋放被using的物件fs（後台自動呼叫了dispose）
        byte[] buffer = Encoding.UTF8.GetBytes(s); //把要寫入的東西轉換成byte陣列
        fs.Write(buffer, 0, buffer.Length); //寫入
    }
    Console.ReadKey();
}
```

其中有以下重點：        

`FileStream fs = new FileStream(@"C:\Users\rivalin\Desktop\test.txt", FileMode.OpenOrCreate, FileAccess.Write)`     
第一個參數填路徑；第二個參數選擇形式，選擇`OpenOrCreate`最保險，檔案不存在就建立一個；第三個參數選 Write，即寫入。


## 複製大文件（讀寫同步）

```c#
static void Main(string[] args)
{
    string sourcePath = @"C:\Users\rivalin\Desktop\source.mp4";//需要被複製的檔案的路徑
    string targetPath = @"C:\Users\rivalin\Desktop\target.mp4";//複製到的路徑
    using (FileStream fsRead = new FileStream(sourcePath, FileMode.OpenOrCreate, FileAccess.Read))
    {   //建立讀取檔案的流(stream)
        using (FileStream fsWrite = new FileStream(targetPath, FileMode.OpenOrCreate, FileAccess.Write))
        {   //建立寫入檔案的流
            byte[] buffer = new byte[1024 * 1024 * 2];//快取設定2MB；

            while (true)//循環讀取
            {
                int r = fsRead.Read(buffer, 0, buffer.Length); //讀取數據
                if (r == 0) break; //讀不到資料了，跳出循環
                fsWrite.Write(buffer, 0, r);//寫數據
            }
        }
    }
    Console.WriteLine("複製完成！");
    Console.ReadKey();
}
```



[MSDN - 檔案和資料流 I/O](https://learn.microsoft.com/zh-tw/dotnet/standard/io/)            
[MSDN - FileStream 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.filestream?view=net-8.0)    
[C#文件流FileStream的用法[通俗易懂]](https://cloud.tencent.com/developer/article/2107597)  
[C#文件流FileStream的用法[通俗易懂](原創)](https://javaforall.cn/162870.html)   
[[C# 筆記][FileStream] 文件流-複習  by R](https://riivalin.github.io/posts/2011/02/filestream-1/)       
[[C# 筆記][FileStream] 使用 FileStream 來讀寫文件  by R](https://riivalin.github.io/posts/2011/01/file-stream/)     
[[C# 筆記][FileStream] 使用 FileStream 實現多媒體文件的複製 by R](https://riivalin.github.io/posts/2011/01/filestream-copyfile/)