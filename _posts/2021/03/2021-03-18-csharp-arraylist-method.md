---
layout: post
title: "[C# 筆記] ArrayList 常用方法"
date: 2021-03-18 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,arraylist]
---


# ArrayList 常用方法

- `Add()`：將物件加入`ArrayList`位置中的末端。
- `AddRange()`：將 `ICollection` 的元素加入到 `ArrayList` 的末尾。
- `Clear()`：清除`ArrayList`中的所有元素。
- `Contains()`：確定某元素是否在 ArrayList 中。
- `CopyTo()`：將 ArrayList 或它的一部分複製到一维陣列(已有陣列)。
- `IndexOf()`：傳回 ArrayList 或它的一部分中某個值的第一個符合項的從零開始的索引。
- `Insert()`：新增物件到指定的`ArrayList`的索引位置。
- `InsertRange()`：將集合中的元素插入 ArrayList 的指定索引處。
- `LastIndexOf()`：返回 ArrayList 或它的一部分中某個值的最後一个匹配项的從零開始的索引。
- `Remove()`：移除`ArrayList`特定的第一個符合元素。
- `RemoveAt()`：移除`ArrayList`的指定索引位置的元素。
- `RemoveRange()`：從 ArrayList 中移除一系列元素。
- `Reverse()`：反轉整個`ArrayList`中所有元素的順序。
- `Sort()`：排序整個`ArrayList`中所有元素的順序。
- `ToArray()`：將 ArrayList 的元素複製到新的陣列中。(將 ArrayList 集合轉換為 object 陣列)
- `TrimToSize()`：將陣列大小設定為`ArrayList`中的實際元素數，可以有效減少容量浪費。
> `Capacity` 始终大於或等於 `Count`。


## Add(Object)

將物件加入 ArrayList 的結尾處。     

ArrayList 接受 `null` 作為有效值，並允許重複元素。

```c#
ArrayList list = new ArrayList();
//增加3個元素
list.Add("Rii");
list.Add(null); //ArrayList接受null作為有效值
list.Add("Kiki");
list.Add("Kiki"); //並允許重複元素

//看結果
foreach(var e in list) {
    Console.WriteLine(e);
}
```

執行結果：

```
Rii

Kiki
Kiki
```

## AddRange(ICollection)
將 `ICollection` 的元素加入到 `ArrayList` 的末尾。

```c#
ArrayList list = new ArrayList() {"張三", "李四"};
ArrayList nums = new ArrayList() {1, 2, 3};

list.AddRange(nums); //將nums加入 list 尾端

//看結果
foreach(var e in list) {
    Console.WriteLine(e);
}
```

執行結果：

```
張三
李四
1
2
3
```

## Clear()
從 ArrayList 中移除所有元素。


```c#
ArrayList list = new ArrayList() {"張三", "李四", "王五"};
list.Clear(); //清空list

Console.WriteLine($"目前ArrayList有{list.Count}個元素");

//執行結果：
//目前ArrayList有0個元素
```

## Contains(Object) 
確定某元素是否在 ArrayList 中。

```c#
public virtual bool Contains (object? item);
```

- 參數 item 
    - Object：要在 ArrayList 中尋找的 Object。 這個值可以是 `null`。

- 傳回 Boolean
    - 如果在 `true` 中找到 item，則為 ArrayList，否則為 `false`。


```c#
ArrayList nums = new ArrayList() { 123, 456, 789 };
string s = nums.Contains(456) ? "集合中有456數字" : "集合中沒有456數字"

Console.WriteLine(s); //集合中有456數字
```

[MSDN - ArrayList.Contains(Object) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.contains?view=net-8.0)      


## CopyTo()
將 ArrayList 或它的一部分複製到一维陣列(已有陣列)。     

[MSDN - ArrayList.CopyTo 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.copyto?view=net-8.0)


### (1). CopyTo(Array)

從目標陣列的開頭開始，將整個 ArrayList 複製到相容的一維 Array。 (替換原有位置元素)

```c#
public virtual void CopyTo (Array array);
```
- 參數 array
    - Array：一維 Array，是從 ArrayList 複製過來之項目的目的端。 Array 必須有以零為起始的索引。


從目標陣列 array 的第 0 個位置開始，將整個集合中的元素複製到類型相容的陣列 array 中。

> 注意：目標陣列的容量要大於等於ArrayList中的元素個數。


#### 範例:

```c#
ArrayList nums = new ArrayList() { 123, 456, 789 };
int[] arr = { 1,2,3}; //類型要兼容

nums.CopyTo(arr); //用nums中的元素来替换array中的元素

//操作後的結果
foreach(var e in arr) {
    Console.WriteLine(e);
}
```

執行結果:       
(arr陣列的元素，全部變成 ArrayList的元素)

```
123
456
789
```

### (2). CopyTo(Array, Int32)

從目標陣列的指定索引開始，將整個 ArrayList 複製到相容的一維 Array。

```c#
public virtual void CopyTo (Array array, int arrayIndex);
```

- array
    - Array: 一維 Array，是從 ArrayList 複製過來之項目的目的端。 Array 必須有以零為起始的索引。
- arrayIndex
    - Int32: array 中以零起始的索引，即開始複製的位置。


從目標數組 array 的指定索引 arraylndex 處，將整個集合中的元素賦值到類型相容的陣列 array 中。

> 注意：從 arrayIndex 到目標 array 末端之間的可用空間大於等於來源 ArrayList 中的元素個數。

#### 範例:

```c#
ArrayList list = new ArrayList() {"張三", "李四", "王五"};
string[] arr = { "AA", "BB", "CC", "DD", "EE", "FF"};

//將ArrayList的元素copy到 array中，從第二個元素開始(index:1)
list.CopyTo(arr,1); //從 array第二个元素開始替换

//操作後的結果
foreach(var e in arr) {
    Console.WriteLine(e);
}
```

執行結果：

```
AA
張三
李四
王五
EE
FF
```


### (3). CopyTo(Int32, Array, Int32, Int32)

從目標陣列的指定索引開始，將項目範圍從 ArrayList 複製至相容的一維 Array

```c#
public virtual void CopyTo (int index, Array array, int arrayIndex, int count);
```

- index
    - Int32: 來源 ArrayList 中以零為起始的索引，位於複製開始的位置。
- array
    - Array: 一維 Array，是從 ArrayList 複製過來之項目的目的端。 Array 必須有以零為起始的索引。
- arrayIndex
    - Int32: array 中以零起始的索引，即開始複製的位置。
- count
    - Int32: 要複製的項目數目。


從目標陣列 array 的指定索引 arrayindex 處，將集合中從指定索引 index 開始的 count 個元素複製到類型相容的陣列 array 中。

> 注意：        
> 
> index 小於或等於來源 ArrayList 的 Count 數。      
> 從 index 到來源 ArrayList 的末端的元素數要小於等於從 arrayIndex 到目標 array 的末端的可用空間。       
> 這裡的 count 是要複製的元素數。       


#### 範例:

```c#
ArrayList list = new ArrayList() {"張三", "李四", "王五", "小六"};
string[] arr = { "AA", "BB", "CC", "DD", "EE", "FF"};

list.CopyTo(1, arr, 2, 3); //截取list中自索引1开始的3個数，替换到array中自索引2開始的3個数

//操作後的結果
foreach(var e in arr) {
    Console.WriteLine(e);
}
```

執行結果：

```
AA
BB
李四
王五
小六
FF
```

## IndexOf()

傳回 ArrayList 或其中一部分中，第一次出現某值之以零為起始的索引。

### (1)、IndexOf(Object)

搜尋指定的 Object，並傳回整個 ArrayList 中第一個符合項目的從零開始的索引。

```c#
public virtual int IndexOf (object? value);
```

參數
- value
    - Object: 要在 ArrayList 中尋找的 Object。 這個值可以是 null。

傳回
- Int32
    - 整個 value 中第一個出現 ArrayList 之以零為起始的索引 (如有找到)，否則為 -1。


取得 value 值在集合中第一次出現的位置。如果找到，則為整個 value 中 ArrayList 第一個符合項目的從零開始的索引；否則為 -1。

#### 範例:

```c#
ArrayList test = new ArrayList() {"aaa","bbb","abc","ccc"};
int index = test.IndexOf("abc");////自索引0 開始查找 abc
Console.WriteLine((index != -1)? "集合中存在元素 abc": "集合中不存在元素 abc"); //集合中存在元素 abc
```

### (2)、IndexOf(Object, Int32)

在 ArrayList 中從指定的索引開始到最後一個項目這段範圍內，搜尋指定的 Object 第一次出現的位置，並傳回其索引值 (索引以零起始)。

```c#
public virtual int IndexOf (object? value, int startIndex);
```

參數
- value
    - Object: 要在 ArrayList 中尋找的 Object。 這個值可以是 null。
- startIndex
    - Int32: 搜尋之以零為起始的起始索引。 0 (零) 在空白清單中有效。

傳回
- Int32
    - 在 ArrayList 中從 startIndex 開始到最後一個元素的範圍內，第一次出現 value 的位置之以零為起始的索引 (如有找到)，如未找到則為 -1。

取得 value 值在集合的 startindex 位置開始往後第一次出現的位置。     

#### 範例:

```c#
ArrayList test = new ArrayList() {"張三", "999", "李四", "bbb","abc","ccc"};
int index = test.IndexOf("999", 3); //自索引3 開始查找 999
Console.WriteLine((index != -1)? "本次查询到了元素 999" : "本次没有查询到元素 999"); //本次没有查询到元素 999
```


### (3)、IndexOf(Object, Int32, Int32)

在 ArrayList 中從指定索引開始且包含指定項目個數的範圍內，搜尋指定的 Object 並傳回第一次出現的以零為起始的索引。

```c#
public virtual int IndexOf (object? value, int startIndex, int count);
```

參數
- value
    - Object: 要在 ArrayList 中尋找的 Object。 這個值可以是 null。
- startIndex
    - Int32: 搜尋之以零為起始的起始索引。 0 (零) 在空白清單中有效。
- count
    Int32: 區段中要搜尋的項目數目。

傳回
- Int32: 在 ArrayList 中從 startIndex 開始且包含 count 個元素的範圍內，第一次出現 value 之以零為起始的索引，如未找到則為 -1。


取得 value 值在集合的 startindex 位置開始向後推移 count 個元素中第一次出現的位置。

#### 範例:

```c#
ArrayList test = new ArrayList() {"張三", "999", "李四", "567", "王五", "789", "小六"};
int index = test.IndexOf("789", 1, 5); //在test中查找自 索引1 開始的 5 個元素中是否有元素 789
Console.WriteLine((index != -1)? "本次查询到了元素 789" : "本次没有查询到元素 789"); //本次查询到了元素 789
```


[MSDN - ArrayList.IndexOf 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.indexof?view=net-8.0)

        
## Insert(Int32, Object)
將元素插入 ArrayList 的指定索引處。

```c#
public virtual void Insert (int index, object? value);
```

參數
- index
    - Int32: 應在 value 插入以零為起始的索引。
- value
    - Object: 要插入的 Object。 這個值可以是 null。


傳回 value 向集合中的指定索引 index 處插

### 範例

```c#
ArrayList test = new ArrayList() { "abc", "123", "cdf", "678"};
test.Insert(1, "李四"); //在索引1的位置插入"李四"

foreach(var e in test) {
    Console.WriteLine(e);
}
```

執行結果：

```
abc
李四
123
cdf
678
```

## InsertRange(Int32, ICollection) 

將集合中的元素插入 ArrayList 的指定索引處。

```c#
public virtual void InsertRange (int index, System.Collections.ICollection c);
```

- index
    - Int32: 應插入新項目處的以零為起始的索引。
- c
    - ICollection: ICollection，其項目應插入 ArrayList。 集合本身不能是 null，但它可以包含為 null 的項目。


向集合中的指定索引 index 處插入一個集合

### 範例

```c#
ArrayList list = new ArrayList() {"張三", "李四", "王五", "小六"};
ArrayList nums = new ArrayList() {1,2,3};
list.InsertRange(1, nums); //將 集合nums 插入到 集合list 的 索引1 處

foreach(var e in list) {
    Console.WriteLine(e);
}
```

執行結果：

```
張三
1
2
3
李四
王五
小六
```


[MSDN - ArrayList.InsertRange(Int32, ICollection) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.insertrange?view=net-8.0)     


## LastIndexOf() 

返回 ArrayList 或它的一部分中某個值的最後一个匹配项的從零開始的索引。

### (1)、LastIndexOf(Object)

搜尋指定的 Object，並傳回在整個 ArrayList 中最後一個符合項目之以零為起始的索引。

```c#
public virtual int LastIndexOf (object? value);
```

參數
- value
    - Object: 要在 ArrayList 中尋找的 Object。 這個值可以是 null。

傳回
- Int32: 如果找到的話，則為整個 ArrayList 中最後一次出現 value 之以零為起始的索引 (如有找到)，如未找到則為 -1。

返回 value值在集合中最後一次出现的位置

> 注意：從最後一个元素開始向前搜索ArrayList，並在第一个元素處结束


#### 範例

```c#
ArrayList test = new ArrayList() {"張三", "李四", "234", "王五", "小六"};
int index = test.LastIndexOf("234");
Console.WriteLine((index != -1) ? "本次查询含有元素234" : "本次查询不含元素234"); //本次查询含有元素234
```


### (2)、LastIndexOf(Object, Int32)

在 ArrayList 中從第一個項目開始到指定的索引這個範圍內，搜尋指定的 Object，並傳回最後一次出現的索引值 (以零為起始)。

```c#
public virtual int LastIndexOf (object? value, int startIndex);
```
參數
- value
    - Object: 要在 ArrayList 中尋找的 Object。 這個值可以是 null。
- startIndex
    - Int32: 向後搜尋之以零為起始的起始索引。

傳回
- Int32: 在 ArrayList 中從第一個元素開始到 startIndex 這段範圍內，如果有找到指定的 value 最後一次出現的位置，則為該位置以零為起始的索引，如未找到則為 -1。


獲取在集合指定的 startindex 位置開始直到第一個元素區間，值value最後一次出现的位置。

#### 範例

```c#
ArrayList test = new ArrayList() {"張三", "李四", "234", "王五", "999", "小六"};
int index = test.LastIndexOf("999", 3);//自 索引3 向前查询，元素999 最後一次出现的位置
Console.WriteLine((index != -1) ? "本次查询含有元素999" : "本次查询不含元素999"); //本次查询不含元素999
```

### (3)、LastIndexOf(Object, Int32, Int32)

搜尋指定的 Object，並傳回 ArrayList 中包含指定之項目數且結束於指定之索引的項目範圍內，最後一個相符項目之以零為起始的索引。

```c#
public virtual int LastIndexOf (object? value, int startIndex, int count);
```

參數
- value
    - Object: 要在 ArrayList 中尋找的 Object。 這個值可以是 null。
- startIndex
    - Int32: 向後搜尋之以零為起始的起始索引。
- count
    - Int32: 區段中要搜尋的項目數目。

傳回
- Int32: 如果找到，則為 ArrayList 中包含 count 個元素且結束於 startIndex 的元素範圍內，value 的最後一個相符項目之以零為起始的索引，否則為 -1。

返回 value值在集合的 startindex 位置向前開始 count 個元素中最後一次出现的位置


#### 範例

```c#
ArrayList test = new ArrayList() {"張三", "李四", "234", "王五", "999", "小六", "abc"};
int index = test.LastIndexOf("999", 5, 3);//自 索引5 向前查询 3個元素 含有999 最後一次出现的位置
Console.WriteLine((index != -1) ? "本次查询含有元素999" : "本次查询不含元素999"); //本次查询含有元素999
```


## Remove(Object)

從 ArrayList 中移除特定對象的第一個匹配项。

```c#
public virtual void Remove (object obj);
```

將集合中第一个匹配到的指定元素 obj 從集合中移除

### 範例

```c#
ArrayList test = new ArrayList() {"張三", "李四", "王五"};
test.Remove("張三"); //移除"張三"

foreach(var e in test) {
    Console.WriteLine(e);
}
```

執行結果：

```
李四
王五
```

## RemoveAt(Int32)
移除 ArrayList 的指定索引處的元素。

```c#
public virtual void RemoveAt (int index);
```
移除集合中指定位置 index 處的元素

### 範例

```c#	
ArrayList test = new ArrayList() {"張三", "李四", "王五"};
test.RemoveAt(1); //移除索引1的元素

foreach(var e in test) {
    Console.WriteLine(e);
}
```

執行結果：

```
張三
王五
```

## RemoveRange(Int32, Int32)
從 ArrayList 中移除一系列元素。

```c#
public virtual void RemoveRange (int index, int count);
```
移除集合中自索引 index 處向後的 count 個元素

### 範例

```c#
ArrayList test = new ArrayList() {"張三", "李四", "王五", "小六", "123", "567"};
test.RemoveRange(2,3); //移除 自 索引2 向後的 3 個元素

foreach(var e in test) {
    Console.WriteLine(e);
}
```

執行結果：

```
張三
李四
567
```

## Reverse()

### (1)、Reverse()
將 ArrayList 或它的一部分中元素的顺序反轉。

```c#
ArrayList test = new ArrayList() {"張三", "李四", "王五", "小六", "123", "567"};
test.Reverse();

foreach(var e in test) {
    Console.WriteLine(e);
}
```

執行結果：

```
567
123
小六
王五
李四
張三
```

### (2)、Reverse(Int32, Int32)

將指定範圍中元素的顺序反轉。

```c#
public virtual void Reverse (int index, int count);
```
將集合中從指定位置 index 處向后的 count 個元素反轉。

```c#
ArrayList test = new ArrayList() {"張三", "李四", "王五", "小六", "123", "567"};
test.Reverse(2, 3); //自 索引2 向後的 3 個元素

foreach(var e in test) {
    Console.WriteLine(e);
}
```

執行結果：

```
張三
李四
123
小六
王五
567
```

## Sort()
對 ArrayList 或它的一部分中的元素進行排序。

將集合中的元素排序，默認从小到大排序

### 範例

```c#
ArrayList test = new ArrayList() {999, 888, 123, 456, 111};
test.Sort();

foreach(var e in test) {
    Console.WriteLine(e);
}
```

執行結果：

```
111
123
456
888
999
```

## ToArray()

將 ArrayList 的元素複製到新的陣列中。

### (1)、ToArray()

將 ArrayList 的元素複製到新 Object 陣列中。

```c#
public virtual object[] ToArray ();
```

將 ArrayList 集合轉換為 object 陣列

#### 範例

```c#
ArrayList test = new ArrayList() { 1, 2, 3, 4, 5 };
var arr = test.ToArray();

foreach(var e in arr) {
    Console.WriteLine(e); //依序輸出:12345
}
```


### (2)、ToArray(Type)
将 ArrayList 的元素複製到新的指定元素類型陣列中。

```c#
public virtual Array ToArray (Type type);
```

将 ArrayList 集合轉換為指定類型的陣列。

#### 範例

```c#
ArrayList test = new ArrayList() { 1, 2, 3, 4, 5 };
Array arr = test.ToArray(typeof(int));

foreach(var e in arr) {
    Console.WriteLine(e);
}
```

## TrimToSize()
将容量設置為 ArrayList 中元素的實際數目。

```c#
public virtual void TrimToSize ();
```

将集合的大小設置為集合中元素的實際個數。

### 範例

```c#
ArrayList test = new ArrayList() { 1, 2, 3, 4, 5 };
Console.WriteLine($"Trim前，集合的count是：{test.Count}，容量Capacity是：{test.Capacity}"); //5, 8
test.TrimToSize(); //Trim
Console.WriteLine($"Trim後，集合的count是：{test.Count}，容量Capacity是：{test.Capacity}"); //5, 5
```

執行結果：

```
Trim前，集合的count是：5，容量Capacity是：8
Trim後，集合的count是：5，容量Capacity是：5
```


[[C# 筆記] ArrayList 集合 by R](https://riivalin.github.io/posts/2011/01/arraylist/)        
[C#中 数组、ArrayList、List＜T＞的区别](https://blog.csdn.net/Dust_Evc/article/details/114984023)       
[C#集合 ArrayList 的常用方法和属性](https://blog.csdn.net/qq_42007357/article/details/104278427)        
[MSDN - ArrayList.Contains(Object) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.contains?view=net-8.0)      
[MSDN - ArrayList.CopyTo 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.copyto?view=net-8.0)       
[MSDN - ArrayList.IndexOf 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.indexof?view=net-8.0)     
[MSDN - ArrayList.InsertRange(Int32, ICollection) 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.collections.arraylist.insertrange?view=net-8.0)         
Book: Visual C# 2005 建構資訊系統實戰經典教本   
