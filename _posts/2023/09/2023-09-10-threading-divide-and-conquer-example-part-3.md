---
layout: post
title: "[C# 筆記] Threading - Divide and Conquer Example Part 3"
date: 2023-09-10 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---

[承上範例，模擬大數據在使用單一執行緒和多執行緒後的時間上的效率差異](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-2/)        

# Threading - Divide and Conquer Example Part 3
## 修改for的可讀性

這段for 迴圈太長了，為增加可讀性調整一下...

```c#
//加總
for (int i = portionNumberAsInt * portionSize; i < portionNumberAsInt * portionSize + portionSize; i++) {
    sum += values[i];
}
```

為增加可讀性，調整為：

```c#
int baseIndex = portionNumberAsInt * portionSize; //開始元素的index

//加總
for (int i = baseIndex; i < baseIndexe + portionSize; i++) {
    sum += values[i];
}
```

接下來要做的是：        
所有的執行緒，都會調用同一個方法，同時執行這個方法，    
這意味著，劃分的每個部分，都在自己的線程上，並同時的進行工作


## 建立多個執行緒，讓它們完成自己該做的事(執行同一個加總方法)

在 `Main()` 中，        
建立多個執行緒(使用`Environment.ProcessorCount`有幾個CPU就建立幾個線程)     
去執行同一個加總方法`SumYourPortion()`，        
讓它們完成自己該做的部分。  


[使用參數化執行緒方法-Thread.Start(object parameter)](https://riivalin.github.io/posts/2023/09/c-sharp-multiple-threads/#參數化線程-threadstartobject-parameter)

```c#
//參數化執行緒-每個線程都會執行該方法，加總自己部分的數字
static void SumYourPortion(object portionNumber)
{
    long sum = 0;
    int portionNumberAsInt = (int)portionNumber;
    int baseIndex = portionNumberAsInt * portionSize; //開始元素的index

    //加總
    for (int i = baseIndex; i < baseIndex + portionSize; i++) {
        sum += values[i];
    }

    //存放加總結果
    portionResults[portionNumberAsInt] = sum;
}
```

## Main()中建立多個執行緒

Main()主執行緒中，建立多個執行緒，讓他們去調用同一個方法，並把可以代表自己的id帶入

```c#
//建立執行緒(有幾個CPU就建幾個)去執行SumYourPortion()加總方法，讓他們完成自己該做的部分
for (int i = 0; i < Environment.ProcessorCount; i++)
{
    //讓所有執行緒都執行SumYourPortion()方法做加總
    var thread = new Thread(SumYourPortion);
    thread.Start(i); //啟動執行緒
}
```

## 宣告陣列，將所有執行緒放在一起

我們必須等待所有的執行緒都完成自己加總的部分後，才能在繼續...       

一個簡單的方法：將所有執行緒放在一起

```c#
//建立執行緒(有幾個CPU就建幾個)去執行SumYourPortion()加總方法，讓他們完成自己該做的部分
Thread[] threads = new Thread[Environment.ProcessorCount]; //宣告陣列，將所有執行緒放在一起
for (int i = 0; i < Environment.ProcessorCount; i++)
{
    //讓所有執行緒都執行SumYourPortion()方法做加總
    threads[i] = new Thread(SumYourPortion);
    threads[i].Start(i); //啟動執行緒
}
```

## 將所有執行緒加總的值，全部加起來

最後，將所有執行緒個自加總的值，全部加起來

```c#
long total2 = 0;
for (int i = 0; i < Environment.ProcessorCount; i++) {
    total2 += portionResults[i];
}
```

## 加上Stopwatch看花了多少時間

再重新計時，看使用多個執行緒後，花費多少時間

```c#
static void Main(string[] args)
{
    //長度為該電腦的CPU處理器核心數(有8個cpu就會保存8個值)
    portionResults = new long[Environment.ProcessorCount];
    //大小: 所有數字 /挪有的處理器的數量
    portionSize = values.Length / Environment.ProcessorCount;

    GenerateInts(); //產生很多整數
    Console.WriteLine("Summing...");

    //使用Stopwatch來看花了多少時間加總
    Stopwatch watch = new Stopwatch();
    watch.Start();//開始計時

    long total = 0;
    for (int i = 0; i < values.Length; i++)
    {
        total += values[i];
    }
    watch.Stop(); //結束計時
    Console.WriteLine($"Total value is: {total}");
    Console.WriteLine($"Time to sum: {watch.Elapsed}");
    //Console.WriteLine($"Time to sum: {watch.ElapsedMilliseconds/1000} 秒");

    //重新計時(使用多個執行緒)
    watch.Reset();
    watch.Start();

    //建立執行緒(有幾個CPU就建幾個)去執行SumYourPortion()加總方法，讓他們完成自己該做的部分
    Thread[] threads = new Thread[Environment.ProcessorCount];
    for (int i = 0; i < Environment.ProcessorCount; i++)
    {
        //讓所有執行緒都執行SumYourPortion()方法做加總
        threads[i] = new Thread(SumYourPortion);
        threads[i].Start(i); //啟動執行緒
    }

    //
    for (int i = 0; i < Environment.ProcessorCount; i++) {
        threads[i].Join();
    }

    //將所有執行緒個自加總的值，全部加起來
    long total2 = 0;
    for (int i = 0; i < Environment.ProcessorCount; i++) {
        total2 += portionResults[i];
    }
    watch.Stop(); //結束計時
    Console.WriteLine($"Total value is: {total2}");
    Console.WriteLine($"Time to sum: {watch.Elapsed}");
}
```

執行結果：      

在多個執行緒工作下，有比單一執行緒快，      
所以如果有大數據時，是可以在多個執行緒之間劃分工作。

```console
Summing...
Total value is: 2249935218
Time to sum: 00:00:10.9200492  ==> 10.92秒

Total value is: 2249935218
Time to sum: 00:00:01.3650061  ==> 4.365秒
```

# 完整程式碼

```c#
internal class Program
{
    static byte[] values = new byte[500000000]; //放置隨機產生的很多的數字
    static long[] portionResults; //存放每個執行緒回報的加總
    static int portionSize; //每個執行緒所擁有值的大小(也就是要讀取多少的數據)

    static void GenerateInts()
    {
        var rnd = new Random(); //隨機數
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = (byte)rnd.Next(10); //產生0-9範圍的數字
        }
    }

    //參數化執行緒-每個線程都會執行該方法，加總自己部分的數字
    static void SumYourPortion(object portionNumber)
    {
        long sum = 0;
        int portionNumberAsInt = (int)portionNumber;
        int baseIndex = portionNumberAsInt * portionSize; //開始元素的index

        //加總
        for (int i = baseIndex; i < baseIndex + portionSize; i++)
        {
            sum += values[i];
        }

        //存放加總結果
        portionResults[portionNumberAsInt] = sum;
    }

    static void Main(string[] args)
    {
        //長度為該電腦的CPU處理器核心數(有8個cpu就會保存8個值)
        portionResults = new long[Environment.ProcessorCount];
        //大小: 所有數字 /挪有的處理器的數量
        portionSize = values.Length / Environment.ProcessorCount;

        GenerateInts(); //產生很多整數
        Console.WriteLine("Summing...");

        //使用Stopwatch來看花了多少時間加總
        Stopwatch watch = new Stopwatch();
        watch.Start();//開始計時

        long total = 0;
        for (int i = 0; i < values.Length; i++)
        {
            total += values[i];
        }
        watch.Stop(); //結束計時
        Console.WriteLine($"Total value is: {total}");
        Console.WriteLine($"Time to sum: {watch.Elapsed}");
        //Console.WriteLine($"Time to sum: {watch.ElapsedMilliseconds/1000} 秒");


        //重新計時(使用多個執行緒)
        watch.Reset();
        watch.Start();

        //建立執行緒(有幾個CPU就建幾個)去執行SumYourPortion()加總方法，讓他們完成自己該做的部分
        Thread[] threads = new Thread[Environment.ProcessorCount];
        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            //讓所有執行緒都執行SumYourPortion()方法做加總
            threads[i] = new Thread(SumYourPortion);
            threads[i].Start(i); //啟動執行緒
        }

        //
        for (int i = 0; i < Environment.ProcessorCount; i++) {
            threads[i].Join();
        }

        //
        long total2 = 0;
        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            total2 += portionResults[i];
        }
        watch.Stop(); //結束計時
        Console.WriteLine($"Total value is: {total2}");
        Console.WriteLine($"Time to sum: {watch.Elapsed}");
    }
}
```


[Threading - Divide and Conquer Example Part 3](https://www.youtube.com/watch?v=0v8sDF58mME&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=10&pp=iAQB)


