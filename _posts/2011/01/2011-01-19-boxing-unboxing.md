---
layout: post
title: "[C# 筆記] Boxing & Unboxing 裝箱&拆箱"
date: 2011-01-19 22:19:00 +0800
categories: [Notes, C#]
tags: [C#,Boxing Unboxing,裝箱 拆箱]
---

`ArrayList`和`Hashtable` 很少在用，為什麼?  

除了取數據不方便外，花費時間較多、效率低外，因為涉及到裝箱、拆箱的問題。  

## 裝箱&拆箱
- 裝箱：將值類型轉成引用類型
- 拆箱：將引用類型轉成值類型

```c#
int n = 10;
object o = n; //裝箱
int nn = (int)o; //拆箱
```

## ArrayList

值類型`i`放入引用類型`ArrayList`, 發生了裝箱
```c#
ArrayList list = new ArrayList();
//這個循環發生了1000萬次的裝箱操作
for (int i = 0; i < 10000000; i++) { //裝箱，裝了1000萬次
    list.Add(i); //值類型`i`放入引用類型`ArrayList`, 發生了裝箱
}
```

## 驗証裝箱效率 ArrayList & List
### ArrayList
`ArrayList.Add();`中的参數是`Object`類型，輸入的是`int`類型，所以發生了由實值型別到參考型別的轉換，裝箱。

```c#
ArrayList list = new ArrayList();

Stopwatch sw = new Stopwatch();
sw.Start();
//這個循環發生了1000萬次的裝箱操作
//00:00:18.721029
for (int i = 0; i < 10000000; i++) //裝箱，裝了1000000次
{   //值類型`i`放入引用類型`ArrayList`, 發生了裝箱
    list.Add(i); //arraylist加入數據的類型，都是object類型，i都是int類型，所以發生了裝箱
}
sw.Stop();
Console.WriteLine(sw.Elapsed); //00:00:18.721029
Console.ReadKey();
```
耗費 00:00:18.721029 秒  

### List 泛型集合
泛型集合添加數據的類型，都是int類型，所以沒有發生裝箱

```c#
List<int> list = new List<int>();

Stopwatch sw = new Stopwatch();
sw.Start();
//00:00:03.1942654
for (int i = 0; i < 10000000; i++)  {
    list.Add(i); //泛型集合添加數據的類型，都是int類型，所以沒有發生裝箱
}
sw.Stop();
Console.WriteLine(sw.Elapsed); //00:00:03.1942654
Console.ReadKey()
```
耗費 00:00:03.1942654 秒  

### 檢驗效能結果
ArrayList 和 List 跑1000萬次迴圈加入數值  
得到結果：  
00:00:18.721029 ArrayList   
00:00:03.1942654 List    

由上可知，裝箱會影響效能，因為一直在做類型轉換。   
所以程式碼當中，避免裝箱。

## 裝箱&拆箱的判別
看兩種類型是否發生了裝箱或是拆箱，要看，這兩種類型是否存在繼承關係。

### string轉換int
沒有發生任何類型的 裝箱&拆箱  
```c#
string str =“123”;
int n = Convert.ToInt32(str);
```
上面程式碼，雖然是由參考類型(string)轉換為實值類型(int)，
但是並沒有發生任何的裝箱和拆的操作，因為他們兩者(string 和 int)沒有繼承關係。  
> string 參考類型，是儲存在 Heap 堆積中  
> int 實值類型，是儲存在 Stack 堆疊中


### int轉換IComparable
發生了裝箱

```c#
int n = 10;
IComparable i = n; //裝箱，IComparable是介面(參考型別)，int繼承IComparable，有繼承關係
```
上述程式碼發生了裝箱：
首先`int`類型與`IComparable`類型是繼承關係，`int`繼承`IComparable`，有繼承關係，又是實值型別轉換成參考型別，所以發生了裝箱。    

查看`int`定義
```c#
//Represents a 32-bit signed integer.
public readonly struct Int32 : IComparable, IComparable<Int32>, IConvertible, IEquatable<Int32>, ISpanFormattable, IFormattable
{ … }
```
> `int`類型繼承了`IComparable`

---

參考型別 = 引用類型  
實值型別 = 值類型    
