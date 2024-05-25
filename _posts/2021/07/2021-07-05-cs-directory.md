---
layout: post
title: "[C# 筆記] Directory 目錄存取"
date: 2021-07-05 06:09:00 +0800
categories: [Notes,C#]
tags: [C#,File,Directory]
---

命名空間: `System.IO`

公開建立、移動和全面列舉目錄和子目錄的靜態方法。 此類別無法獲得繼承。


#### 備註：[MSDN](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.directory?view=net-8.0)      

針對 Directory 一般作業使用 類別，例如複製、移動、重新命名、建立和刪除目錄。    

- 若要建立目錄，請使用其中 CreateDirectory 一種方法。
- 若要刪除目錄，請使用其中 Delete 一種方法。
- 若要取得或設定應用程式的目前目錄，請使用 GetCurrentDirectory 或 SetCurrentDirectory 方法。
- 若要操作DateTime與建立、存取和寫入目錄相關的資訊，請使用 和 SetCreationTime之類的SetLastAccessTime方法。

類別的 Directory 靜態方法會對所有方法執行安全性檢查。 如果您要重複使用物件數次，請考慮改用 對應的實例方法 DirectoryInfo ，因為安全性檢查不一定是必要的。    

如果您只執行一個目錄相關動作，使用靜態 Directory 方法，而不是對應的 DirectoryInfo 實例方法可能會更有效率。 大部分 Directory 的方法都需要您要操作之目錄的路徑。

# Directory 操作文件夾

`CreateDirectory` 建立文件夾
`Delete` 刪除文件夾
`Move` 剪下文件夾
`Exists` 判斷是否存在
`GetFiles` 取得指定的目錄下所有文件的全路徑
`GetDirectory` 取得指定的目錄下所有文件夾的全路徑

# 常用函數

## CreateDirectory 建立文件夾

```c#
Directory.CreateDirectory(@"c:\test.txt");
```

## Delete 刪除文件夾

目錄裡有資料刪除會拋異常，真的要刪除就要加`true`

```c#
//刪除文件夾
//目錄裡有資料刪除會拋異常，真的要刪除就要加true
Directory.Delete(@"c:\Test", true);
```
會徹底刪除，不會在資源回收筒裡

## Exists 判断資料夾是否存在

```c#
//判断資料夾是否存在
if (Directory.Exists(@"c:\Test")) {
    Directory.Delete(@"c:\Test", true); //存在就刪除
}
```

## GetFiles 取得這個資料夾中所有檔案的路徑(完整路徑)

可指定檔案類型："`*.txt`"、"`*.jpg`"、"`*.wav`"

```c#
string[] paths = Directory.GetFiles(@"C:\temp");

foreach (var item in paths) {
    Console.WriteLine(item);
}
```

執行結果：

```
C:\temp\image.jpg
C:\temp\2077.log
C:\temp\ConsoleApp1.csproj
C:\temp\Program.cs
C:\temp\77.txt
```

### 只列出某類型的檔案

如果我只想要列出某個類型的檔案呢？      
例如：jpg檔 就在後面加上副檔名"*.jpg"

```c#
//只列出 .log 檔案
string[] paths = Directory.GetFiles(@"C:\temp", "*.log"); //後面加上副檔名"*.log"

foreach (var e in paths) {
    Console.WriteLine(e);
}
```

執行結果：

```
C:\temp\77.log
C:\temp\test.log
C:\temp\UninstalItems.log
```

## Directory.GetDirectories 取得這個資料夾中所有的子資料夾

```c#
//取得"C:\temp\source"路徑下 所有的資料夾
string[] folders = Directory.GetDirectories(@"C:\temp\source");
foreach (var item in folders) {
    Console.WriteLine(item);
}
```

執行結果：

```
C:\temp\source\HelloWorld
C:\temp\source\old
C:\temp\source\repos
```

## Move 移動資料夾 (剪下文件夾)

```c#
Directory.Move(@"C:\temp", @"C:\Users\rivalin\Desktop\TEST");
```

TEST資料夾是不存在的，會自動建立TEST資料夾，然後放入整個temp資料夾


    
[MSDN - Directory 類別](https://learn.microsoft.com/zh-tw/dotnet/api/system.io.directory?view=net-8.0)       
[[C# 筆記][WinForm] Directory 類別  by R](https://riivalin.github.io/posts/2011/01/directory/#directorygetfiles-取得指定目錄下所有檔案的全路徑)     
[[C# 筆記] Directory操作文件夾,Process,Thread -15th  by R](https://riivalin.github.io/posts/2011/01/15th/)      
