---
layout: post
title: "[C# 筆記] while 迴圈"
date: 2021-03-08 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,重複結構,while,Progress Bar (進度條)]
---


「重複結構」就是「當程式需要反覆執行時就會用到，通常會在不符合某些測試條件時才會離開迴圈」。

- `for`、`foreach`、`while`、`do while`


# while

- `while`主要運作方式會根據是否符合條式(condition)來離開迴圈。
- 若條件式(condition)符合為真(`true`)，則會離開迴圈。
- `while` 與 `for`最大的不同在於：`while` 不需要確定迴圈所需執行次數，只要知道結束條件即可。

## 語法

```c#
while(條件式 condition) 
{
    statement; //敘述區塊
    [continue/break;]
}
```

## 範例

透過 `while` 迴圈實作一個進度百分比顯示的程式，讓使用者輸入執行迴圈次數，然後透過百分比來顯示目前執行進度。

```c#
static void Main(string[] args)
{
    int counter = int.Parse(Console.ReadLine()!); //使用者輸入迴圈次數

    int i = 0;
    while (i <= counter)
    {
        Console.SetCursorPosition(i / 4, 1);//設定光標位置，參數為第幾列與第幾行
        Console.Write("□");//移動進條

        Console.SetCursorPosition(0, 2);
        Console.Write($"{i}%");

        //模擬實際工作中的延連，否則進度太快
        System.Threading.Thread.Sleep(100);

        i++;
    }
    //工作完成,根据实际情况输出信息,而且清楚提示退出的信息
    Console.SetCursorPosition(0, 3);
}
```


## C#實作控制台顯示動態進度條

```c#
  static void Main(string[] args)
  {
      bool isBreak = false;
      ConsoleColor colorBack = Console.BackgroundColor;
      ConsoleColor colorFore = Console.ForegroundColor;

      //第一行信息
      Console.WriteLine("****** now working...******");

      //第二行绘制进度条背景
      Console.BackgroundColor = ConsoleColor.DarkCyan;

      for (int i = 0; ++i <= 25;)
      {
          Console.Write(" ");
      }
      Console.WriteLine(" ");
      Console.BackgroundColor = colorBack;

      //第三行输出进度
      Console.WriteLine("0%");

      //第四行输出提示,按下回车可以取消当前进度
      Console.WriteLine("Press Enter To Break.");

      //-----------------------上面绘制了一个完整的工作区域,下面开始工作
      //开始控制进度条和进度变化
      for (int i = 0; ++i <= 100;)
      {
          //先检查是否有按键请求,如果有,判断是否为回车键,如果是则退出循环
          if (Console.KeyAvailable && System.Console.ReadKey(true).Key == ConsoleKey.Enter)
          {
              isBreak = true; break;
          }

          //绘制进度条进度
          Console.BackgroundColor = ConsoleColor.Yellow;//设置进度条颜色
          Console.SetCursorPosition(i / 4, 1);//设置光标位置,参数为第几列和第几行
          Console.Write(" ");//移动进度条
          Console.BackgroundColor = colorBack;//恢复输出颜色

          //更新进度百分比,原理同上.
          Console.ForegroundColor = ConsoleColor.Green;
          Console.SetCursorPosition(0, 2);
          Console.Write("{0}%", i);
          Console.ForegroundColor = colorFore;

          //模拟实际工作中的延迟,否则进度太快
          System.Threading.Thread.Sleep(100);
      }

      //工作完成,根据实际情况输出信息,而且清楚提示退出的信息
      Console.SetCursorPosition(0, 3);
      Console.Write(isBreak ? "break!!!" : "finished.");
      Console.WriteLine(" ");

      //等待退出
      Console.ReadKey(true);
  }
```
[C#实现控制台显示动态进度条百分比](https://www.cnblogs.com/netcore5/p/15523934.html)