---
layout: post
title: "[C# 筆記] return break continue"
date: 2011-01-10 05:10:00 +0800
categories: [Notes, C#]
tags: [C#]
---

- `return` 兩個作用：  
    - 立即結束退出方法  
    - 在方法中返回要返回的值    

- `break`：跳出整個迴圈   
- `continue`：強制結束本次迴圈，開始下一次迴圈    

## break
跳出離開while迴圈，再往下執行   
```c#
while (true)
{
    Console.WriteLine("Hello, world");
    break; //跳出離開while迴圈，再往下執行
}
Console.WriteLine("Hi,.Net");
```
輸出：    
Hello, world    
Hi,.Net 

## continue
強制結束這次迴圈，再回到while迴圈，開始下一個迴圈   
```c#
while (true)
{
    Console.WriteLine("Hello, world"); //會一直執行這段
    continue; //強制結束這次迴圈，再回到while迴圈，開始下一個迴圈
}
Console.WriteLine("Hi,.Net"); //用continue不會執行這段
```
輸出：      
Hello, world    
Hello, world... (無限次)    

## return
 結束退出這個方法   
```c#
while (true)
{
    Console.WriteLine("Hello, world");
    return; //立即結束退出方法
}
Console.WriteLine("Hi,.Net");
```
輸出：      
Hello, world    


> return兩個作用：  
1.立即結束退出方法      
2.在方法中返回要返回的值    