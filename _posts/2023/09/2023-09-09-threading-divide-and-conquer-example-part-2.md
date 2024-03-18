---
layout: post
title: "[C# 筆記] Threading - Divide and Conquer Example Part 2"
date: 2023-09-09 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---


[這個範例，耗費10.92秒](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-1/)，其實可以做得更好...     

```c#
internal class Program
{
    static byte[] values = new byte[500000000];
    static void GenerateInts() 
    {
        var rnd = new Random();
        for (int i = 0; i < values.Length ; i++) {
            values[i] = (byte)rnd.Next(10);
        }
    }
    static void Main(string[] args)
    {
        GenerateInts(); //產生很多整數
        Console.WriteLine("Summing...");

        //使用Stopwatch來看花了多少時間加總
        Stopwatch watch = new Stopwatch();
        watch.Start();//開始計時

        long total = 0;
        for (int i = 0; i < values.Length; i++) {
            total += values[i];
        }
        watch.Stop(); //結束計時
        Console.WriteLine($"Total value is: {total}");
        Console.WriteLine($"Time to sum: {watch.Elapsed}");
        //Console.WriteLine($"Time to sum: {watch.ElapsedMilliseconds/1000} 秒");
    }
} 

/* 執行結果
Summing...
Total value is: 2249935218
Time to sum: 00:00:10.9200492
*/
```

如果我有8個CPU，我希望看到所有8個CPU的執行緒都在啟動努力的運作    
10.92秒/8的話，所以我想建立8個執行緒，讓程式跑得更快得到結果   


假設我有很長、無數個數字...
把它分成偶數8塊，然後把它們送走，
這不一定是很好的算法，但它是一個相當不錯的算法XD        

它不是很好的算法的原因是：有些執行緒會先比其他執行緒完成，
而那些在其他執行緒之前完成的執行緒，理論上可以彌補其他執行緒的無法接管的不足        

但無論如何，我們都將其平均分配8塊...    
所有執行緒都從陣列中讀取，沒有一個執行緒寫入陣列        
所以每個人都想讀的話，這是安全的，      
讀掉它是讓執行緒同步        


問題是，當我們寫入時，你能想到我們正在寫入這個陣列時，而其他執行緒正在讀取...       
我們正在生成隨機數，而其他執行緒正在嘗試將它們相加，並且一些隨機數還沒有準備好...       

那麼我們還有一些的同步問題，還有一些問題需要協調：      
- 該執行緒需要知道它正在執行陣列這一部分      
- 每個執行緒都應該報告其總計


## 宣告變數，存放每個執行緒報告的總計、要讀取多少數據的大小

- 宣告變數`portionResults`，用來存放每個執行緒回報的加總    
- 在主執行緒`Main()`中，初始化`portionResults`陣列，長度為「CPU處理器核心數」   
使用`Environment.ProcessorCount`取得「CPU處理器核心數」     

- 宣告變數`portionSize`，表示每個執行緒所擁有值的大小
- 在主執行緒`Main()`中，設置它的大小(平均分配)：擁有的所有數字 / 挪有的處理器的數量

```c#
//每個執行緒回報的加總
static long[] portionResults;
portionResults = new long[Environment.ProcessorCount]; //長度為該電腦的CPU處理器核心數(有8個cpu就會保存8個值)

//每個執行緒所擁有值的大小(也就是要讀取多少的數據，平均分配)
static int portionSize;
portionSize = values.Length / Environment.ProcessorCount; //大小(平均分配): 所有數字量/挪有的處理器的數量
```

```c#
internal class Program
{
    static byte[] values = new byte[500000000]; //放置隨機產生的很多的數字
    static long[] portionResults; //存放每個執行緒回報的加總
    static int portionSize; //每個執行緒所擁有值的大小(也就是要讀取多少的數據)

    //隨機產生很多數字
    static void GenerateInts() 
    {
        var rnd = new Random(); //隨機數
        for (int i = 0; i < values.Length ; i++) {
            values[i] = (byte)rnd.Next(10); //因為是存放到byte陣列，所以值的範圍是0~9
        }
    }

    static void Main(string[] args)
    {
        //長度為該電腦的CPU處理器核心數(有8個cpu就會保存8個值)
        portionResults = new long[Environment.ProcessorCount];
        //大小(平均分配): 所有數字量/挪有的處理器的數量
        portionSize = values.Length / Environment.ProcessorCount;

        GenerateInts(); //產生很多整數
        Console.WriteLine("Summing...");

        //使用Stopwatch來看花了多少時間加總
        Stopwatch watch = new Stopwatch();
        watch.Start();//開始計時

        long total = 0;
        for (int i = 0; i < values.Length; i++) {
            total += values[i];
        }
        watch.Stop(); //結束計時
        Console.WriteLine($"Total value is: {total}");
        Console.WriteLine($"Time to sum: {watch.Elapsed}");
        //Console.WriteLine($"Time to sum: {watch.ElapsedMilliseconds/1000} 秒");
    }
}
```

## 求和方法：所有執行緒都執行這方法

加一個方法`SumYourPortion()`，使用[之前所提到的-參數化線程啟動 thread.Start(object? parameter)](https://riivalin.github.io/posts/2023/09/c-sharp-multiple-threads/)

```c#
//參數化執行緒啟動
static void SumYourPortion(object portionNumber) 
{ 
    //加總
    //存放加總結果
}
```

每個執行緒都會調用這一個方法去做加總，並存放結果

```c#
//參數化執行緒-每個線程都會執行該方法，加總自己部分的數字
static void SumYourPortion(object portionNumber)
{
    long sum = 0;
    int portionNumberAsInt = (int)portionNumber;

    //加總
    for (int i = portionNumberAsInt * portionSize; i < portionNumberAsInt * portionSize + portionSize; i++) {
        sum += values[i];
    }

    //存放加總結果
    portionResults[portionNumberAsInt] = sum;
}
```

```c#
 internal class Program
 {
     static byte[] values = new byte[500000000]; //放置隨機產生的很多的數字
     static long[] portionResults; //存放每個執行緒回報的加總
     static int portionSize; //每個執行緒所擁有值的大小(也就是要讀取多少的數據)

     static void GenerateInts()
     {
         var rnd = new Random();
         for (int i = 0; i < values.Length; i++)
         {
             values[i] = (byte)rnd.Next(10);
         }
     }

     //參數化執行緒-每個線程都會執行該方法，加總自己部分的數字
     static void SumYourPortion(object portionNumber)
     {
         long sum = 0;
         int portionNumberAsInt = (int)portionNumber;

         //加總
         for (int i = portionNumberAsInt * portionSize; i < portionNumberAsInt * portionSize + portionSize; i++)
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
     }
 }
```

[待續...](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-3/)        



[Threading - Divide and Conquer Example Part 2](https://www.youtube.com/watch?v=5LU8WRL3xaE&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=9&pp=iAQB)        
[[C# 筆記] Threading - C# Multiple Threads](https://riivalin.github.io/posts/2023/09/c-sharp-multiple-threads/)
[[C# 筆記] Threading - Divide and Conquer Example Part 1](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-1/)        
[[C# 筆記] Threading - Divide and Conquer Example Part 2](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-2/)