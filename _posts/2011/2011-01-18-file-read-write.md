---
layout: post
title: "[C# 筆記] File 讀寫文件"
date: 2011-01-18 23:49:00 +0800
categories: [Notes, C#]
tags: [C#]
---

### File 檔案讀寫

File最大的缺點：只能讀小文件  

讀寫文本、影音、圖片…
- File.ReadAllBytes() 
- File.WriteAllBytes() 

只能讀文本
- File.ReadAllLines()
- File.ReadAllText()


```text
ReadAllBytes() -> 字串陣列 -> 字串  
Encoding.Default.GetString(字串陣列)  

WriteAllBytes() -> 字串 -> 字串陣列    
Enconding.Default.GetBytes(字串) 
``` 

### `File.ReadAllBytes()` 讀檔  
將byte[]陣列中的每一個元素，都要按照我們指定的編碼格式解碼成字串   
UTF-8、GB2312、GBK、ASCII、Unicode  

ReadAllBytes() -> 字串陣列 -> 字串   
Encoding.Default.GetString(字串陣列)  
```c#
using System.Text;

byte[] buffer = File.ReadAllBytes(@"C:\Users\rivalin\Desktop\new.txt");
//將byte[]陣列中的每一個元素，都要按照我們指定的編碼格式解碼成字串  
//UTF-8 GB2312 GBK ASCII Unicode 
string s = Encoding.Default.GetString(buffer);
Console.WriteLine(s);
```

### `File.WriteAllBytes()` 寫檔
沒有這個文件的話，會給你新建一個，有的話，會給你覆蓋掉。  

WriteAllBytes() -> 字串 -> 字串陣列     
Enconding.Default.GetBytes(字串)  
```c#
string s = "今天天氣真好啊";
//需要將寫入的字串轉成byte[]陣列
byte[] buffer = Encoding.Default.GetBytes(s);
File.WriteAllBytes(@"C:\Users\rivalin\Desktop\lala.txt", buffer);
Console.WriteLine("done");
```

### `File.ReadAllLines()` 以行的形式讀出
```c#
string[] contents = File.ReadAllLines(@"C:\Users\rivalin\Desktop\new.txt", Encoding.Default);
foreach (string item in contents) {
    Console.WriteLine(item);
}
```

### `File.WriteAllLines()` 以行的形式寫入
```c#
File.WriteAllLines(@"C:\Users\rivalin\Desktop\new.txt", new string[] { "aoc","aec"});
Console.WriteLine("done");
```

### `File.ReadAllText()`
```c#
string contents = File.ReadAllText(@"C:\Users\rivalin\Desktop\new.txt", Encoding.Default);
Console.WriteLine(contents);
```

### `File.WriteAllText()`
```c#
File.WriteAllText(@"C:\Users\rivalin\Desktop\new.txt", "今天天氣真好啊");
Console.WriteLine("done");
```


### `File.AppendAllText()` 內容不會覆蓋過去
有相同檔名的文件，內容不會覆蓋過去，寫入的文字只會追加在後面  
```c#
File.AppendAllText(@"C:\Users\rivalin\Desktop\new.txt", "Append All Text 看我有覆蓋嗎");
Console.WriteLine("done");
```

## 絕對路徑 & 相對路徑
### 絕對路徑
通過給定的個路徑，能直接在我的電腦中找到這個文件。
`C:\Users\rivalin\Desktop\1.txt`

### 相對路徑
文件相對於應用程式的路徑。
`..\1.txt`

我們在開發中應該儘量使用「相對路徑」

### R Note
- `ReadAllBytes()` 除了可以讀txt檔，要讀讀影音檔、圖片…，就用它
 
只能讀文本格式  
- `ReadAllLine()` 可以拿到字串陣列，如果要對每一個元素做處理，就用它
- `ReadAllText()` 只能拿到一個字串