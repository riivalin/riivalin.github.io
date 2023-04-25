---
layout: post
title: "[C# 筆記] File類、Path類、Directory類-複習"
date: 2011-02-09 00:12:21 +0800
categories: [Notes,C#]
tags: [C#,File,Path,Directory]
---

## Path類
### ChangeExtension
`Path.ChangeExtension`只是操作字串，不會真的給文件改副檔名
```c#
string path = @"C:\Users\rivalin\Desktop\temp.txt";
string s = Path.ChangeExtension(path, ".jpg");
Console.WriteLine(s);
//輸出：C:\Users\rivalin\Desktop\temp.jpg
```

### GetDirectoryName
`Path.GetDirectoryName`取得文件所在目錄
```c#
string path = @"C:\Users\rivalin\Desktop\temp.txt";
Console.WriteLine(Path.GetDirectoryName(path);
Console.ReadKey();
//輸出：C:\Users\rivalin\Desktop
```

## File類
操作文件的：複製、剪下、創建、移除

### Create
`File.Create`創建文件
```c#
File.Create(@"C:\Users\rivalin\Desktop\new.txt");
Console.WriteLine("創建成功");
Console.ReadKey();
```
### Delete
`File.Delete`刪除文件
```c#
File.Delete(@"C:\Users\rivalin\Desktop\new.txt");
Console.WriteLine("刪除成功");
Console.ReadKey();
```
### Copy
`File.Copy`複製文件
```c#
File.Copy(@"C:\Users\rivalin\Desktop\temp.txt", @"C:\Users\rivalin\Desktop\new123.txt");
Console.WriteLine("複製成功");
Console.ReadKey();
```

### Move
`File.Move`剪下文件    
原本的文件就不在了，被取而代之新的文件了。

```c#
File.Move(@"C:\Users\rivalin\Desktop\new.txt", @"C:\Users\rivalin\Desktop\1.txt");
Console.WriteLine("剪下成功");
Console.ReadKey();
```

## 使用File類來讀取數據
讀數據提供了三個方法：  
- `ReadAllBytes` 以二進制的形式讀取
- `ReadAllLines` 一行一行的讀取
- `ReadAllText` 把所有字串一次性讀進來
    
這三種方式在使用File類來讀數據的時候，有一個很大的特點，就是不管你的數據有多大，我在讀的時候都是怎麼讀進來，一次性讀進來，那這就意味著當我操作大文件的時候，對我們的內存負荷特別大，對吧。    

所以說讀大文件的話，用我們學的那個`FileStream`。    
但是小文件，我們`File類`就可以搞定了。    
- 大文件：`FileStream`
- 小文件：`File類`

### ReadAllBytes
`ReadAllBytes`是以字節(位元組)的形式進行讀取，而我們所有的文件都是以字節(位元組)的形式儲存的呀，對吧。    

所以一個`ReadAllBytes`可以讀取所有類型的文件。他會返回一個整體的字節數組
    
讀取一個文本文件
```c#
//ReadAllBytes是以字節(位元組)的形式去讀取這個文本文件
//所以給咱們返回的也是一個讀取到的直接數據(字節(位元組))
byte[] buffer = File.ReadAllBytes(@"C:\Users\rivalin\Desktop\temp.txt");

//但是我們並不理解這個字節數組裡面放的是一些什麼內容
//咱們需要再將這算字節數組轉換成我們所熟知的字串
//這時候我們需要把這個直接數組當中，每一個元素都按照我們指定的解碼方式，編碼方式進行轉碼
string str = Encoding.UTF8.GetString(buffer, 0, buffer.Length); //開始解碼，從第0個解碼到buffer的長度
```
Q：什麼是編碼？    
編碼：把字串以怎樣的形式儲存為二進制。     
ASCII  GBK  GB2123  UTF-8

### ReadAllLines
`ReadAllLines`以字符串一行一行的來進行讀取

```c#
//以字符串一行一行的來進行讀取，會返迴字串陣列
string[] str = File.ReadAllLines(@"C:\Users\rivalin\Desktop\temp.txt"); //如果有亂碼，可以加 Encoding.Default/UTF8...
//如果想要看內容，得遍數組裡面每一行的數據
foreach (var item in str) {
    Console.WriteLine(item);
}
Console.ReadKey();
```

#### Q：什麼時候用 `ReadAllBytes`？什麼時候用 `ReadAllLines`？    
如果我們需要處理文本文件每一行數據的時候，這個時候你就必須要用`ReadAllLines`，因為他返回的是字串陣列，這個陣列裡就是一行一行的數據。  

`ReadAllBytes`、`ReadAllText` 不能讓你一行一行操作數據，他給你返回的都是一個整體，`ReadAllBytes`他會返回一個整體的字節數組，`ReadAllText`他會返回一坨字串，沒有辦法拿到一行一行的數據。     
     
如果你僅僅只想讀進來看一看，那麼這個`ReadAllText`是最簡單的。

### ReadAllText
 
把所有字串一次性讀進來
```c#
string s= File.ReadAllText(@"C:\Users\rivalin\Desktop\temp.txt");
Console.WriteLine(s);
Console.ReadKey();
```
## File類-寫入
- `WriteAllBytes` 以byte[]類型寫入，寫入的字串需編碼成byte
- `WriteAllLines` 一行一行的寫入
- `WriteAllText`

### WriteAllBytes
重複寫入會覆蓋掉之前的內容

```c#
string s = "Hello";//要寫入的字串
//要把字串以WriteAllBytes的形式寫到文本，我需要編碼成byte[]
//先聲明一個字節數組來接收
byte[] buffer = Encoding.UTF8.GetBytes(s);
File.WriteAllBytes(@"C:\Users\rivalin\Desktop\temp.txt", buffer); //要寫入的路徑,要寫入的字節bytes
Console.WriteLine("OK");
Console.ReadKey();
```

### WriteAllLines
`WriteAllLines`一行一行的寫入    
重複寫入會覆蓋掉之前的內容

```c#
//每個元素都會佔一行
File.WriteAllLines(@"C:\Users\rivalin\Desktop\temp.txt", new string[] { "AA", "BB", "CC" });
Console.WriteLine("done");
Console.ReadKey();
```
### WriteAllText
重複寫入會覆蓋掉之前的內容
```c#
File.WriteAllText(@"C:\Users\rivalin\Desktop\temp.txt", "哈哈");
Console.WriteLine("done");
Console.ReadKey();
```

### AppendAllText
用 `AppendAllText`追加，重複寫入不會覆蓋掉之前的內容，而是追加在後面。

```c#
File.AppendAllText(@"C:\Users\rivalin\Desktop\temp.txt", "Hello");
Console.WriteLine("done");
Console.ReadKey();
```

## Directory靜態類

`Directory`靜態類，靜態類不能創建對象，我們如果調用靜態類的成員，他肯定也是靜態類的。

### CreateDirectory
建立資料夾
```c#
Directory.CreateDirectory(@"C:\Users\rivalin\Desktop\aa");
Console.WriteLine("done");
Console.ReadKey();
```

### Delete
如果我們刪除文件夾裡面有內容的話，是不給刪的，他不允許你刪的，如果你真要刪，就在後面加 `true`。

```c#
//沒加true,不不允許刪除，加true，裡面有資料也會一併刪掉
Directory.Delete(@"C:\Users\rivalin\Desktop\aa", true); 
Console.WriteLine("done");
Console.ReadKey();
```
刪除後，連資源回收筒都沒有，是徹底的刪除。

### Move
搬移
```c#
//原本aa資料夾就會不見了，被取代而之bb資料夾
Directory.Move(@"C:\Users\rivalin\Desktop\aa", @"C:\Users\rivalin\Desktop\bb");
Console.WriteLine("done");
Console.ReadKey();
```

### GetFiles
`Directory.GetFiles` 這個路徑下的所有文件的全路徑

```c#
string[] s = Directory.GetFiles(@"C:\Users\rivalin\Desktop\images");
foreach (var item in s) {
    Console.WriteLine(item);
}
Console.WriteLine("done");
Console.ReadKey();
```

如果我只想要列出某個類型的檔案呢？    
就在後面加上副檔名`"*.jpg"`

```c#
string[] s = Directory.GetFiles(@"C:\Users\rivalin\Desktop\images","*.txt"); //*.jpg  *.wmv
foreach (var item in s) {
    Console.WriteLine(item);
}
Console.WriteLine("done");
Console.ReadKey();
```