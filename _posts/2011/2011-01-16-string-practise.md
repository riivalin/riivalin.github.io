---
layout: post
title: "[C# 筆記] string 練習(File.ReadAllLines + Split + Length + Substring + string.Join)"
date: 2011-01-16 22:59:00 +0800
categories: [Notes, C#]
tags: [C#]
---

練習：
使用 File.ReadAllLines + Split + Length + Substring + string.Join  

txt文件中儲存了多行文章標題、作者，  
標題和作者之間有若干空格(數量不定)隔開，每行一個 (標題+空格+作者名)
標題有長有短的，輸出到控制台的時候最多標題長度10，  
如果超過10，則截取長度8的子串並且最後添加"…"，  
並加一個豎線後輸出作者的名字。(標題|作者)  


txt文件儲存多行"標題+空格+作者名"  
```text
原子習慣：細微改變帶來巨大成就的實證法則          詹姆斯‧克利爾
SPY×FAMILY間諜家家酒 7                       遠藤達哉
好好再見不負遇                                黃山料
剛剛好的優雅：志玲姊姊修養之道                   林志玲
世界最神奇的24堂課                             查爾斯．哈奈爾
```
  
以格式"標題|作者"形式輸出  
標題超過10，就截取長度8+"…"  
```text
原子習慣：細微改...|詹姆斯‧克利爾
SPY×FAMI...|7|遠藤達哉
好好再見不負遇|黃山料
剛剛好的優雅：志...|林志玲
世界最神奇的24堂課|查爾斯．哈奈爾
```
  
```c#
string path = @"C:\Users\rivalin\Desktop\1.txt";
string[] contents = File.ReadAllLines(path); //

for (int i = 0; i < contents.Length; i++)
{
    string[] s = contents[i].Split(' ', StringSplitOptions.RemoveEmptyEntries); //空格分割，去掉空白
    if (s[0].Length > 10) { //如果超過10，則截取長度8的子串並且最後添加"…"
        s[0] = s[0].Substring(0, 8) + "..."; //標題超過10，就截取長度8+"…"
    }
    contents[i] = string.Join('|', s); //加一個豎線後輸出作者的名字。(標題|作者)
}

//輸出看結果
for (int i = 0; i < contents.Length; i++) {
    Console.WriteLine(contents[i]);
}
Console.ReadKey();
```

用三元運算子寫：如果標題超過10，則截取長度8的子串並且最後添加"…"
```c#
 s[0] = (s[0].Length > 10) ? s[0].Substring(0, 8) + "..." : s[0]; //如果超過10，則截取長度8的子串並且最後添加"…"
```

```c#
string path = @"C:\Users\rivalin\Desktop\1.txt";
string[] contents = File.ReadAllLines(path);

for (int i = 0; i < contents.Length; i++)
{
    string[] s = contents[i].Split(' ', StringSplitOptions.RemoveEmptyEntries); //空格分割，去掉空白
    s[0] = (s[0].Length > 10) ? s[0].Substring(0, 8) + "..." : s[0]; //如果超過10，則截取長度8的子串並且最後添加"…"
    contents[i] = string.Join('|', s); //加一個豎線後輸出作者的名字。(標題|作者)
}
```