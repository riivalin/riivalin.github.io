---
layout: post
title: "[C# 筆記] StringBuilder 類別"
date: 2021-03-27 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,string,StringBuilder]
---

# StringBuilder 類別

`System.Text.StringBuilder`類別主要功能是用來「管理字串」，包括：附加、移除、取代或插入字串等。     

## StringBuilder 和 string 比較表

|                 | StringBuilder 物件              | String 物件  | 
|:---------------:|:--------------------------------|:------------|
| 串連作業配置記憶體 | 緩衝區太小無法容納新資料才配置。     | 永遠配置新記憶體。|
| 串連作業         | 適合未知數目的動態串連。            | 適合固定數目的串連。|
| 字串管理功能      | 專注於字串的附加、移除、取代、插入等。 | 提供較多的方法來操作運算字串。
|                 |                                | 例如：分割字串、搜尋字串、去空白、轉大小寫等。|


## StringBuilder 常用屬性

| 屬性名稱   | 功能說明                                   |
|:----------|:------------------------------------------|
| Chars     | 用來取得 StringBuilder 物件中指定的位置的字元。(取出索引位置字元)|
| `Length`  | 用來取得 StringBuilder 物件的字元總數。(計算長度)|
| `Capacity` | 用來取得 StringBuilder 物件的目前容量。|
| `MaxCapacity` | 用來取得 StringBuilder 物件的最大容量。|


### 範例

```c#
StringBuilder sb = new StringBuilder("I Love C#.");
Console.WriteLine(sb[2]); // L，取出索引2位置 字元
Console.WriteLine(sb.Length); // 長度: 10
Console.WriteLine(sb.Capacity); //目前容量: 16
Console.WriteLine(sb.MaxCapacity); //最大容量: 2147483647
```

## StringBuilder 常用方法

| 方法名稱          | 功能說明                                   |
|:-----------------|:------------------------------------------|
| `Append()`        | 將指定物件的字串，附加於目前 StringBuilder 字串的末端。|
| `AppendFormat()` | 將指定格式化的字串，附加於目前 StringBuilder 字串的末端。|
| `AppendLine()`   | 換行。加入換行字元|
| `EnsureCapacity()`| 指定目前 StringBuilder 字串容量。      |
| `Insert()`        | 在指定的索引位置上，插入字串。
| `Remove()`        | 移除指定的字元範圍。`Remove(2,3)`索引2 位置 開始移除 3個字元 |
| `Replace()`       | 取代字元。`Replace("要換的字", "取代的字")`         |
| `ToString()`      | 將 StringBuilder 物件轉換成 String物件 |


### Append()

```c#
// Append 附加文字
StringBuilder sb = new StringBuilder("I Love C#.");
sb.Append("您好");
Console.WriteLine(sb.ToString()); //I Love C#.您好
```

### AppendFormat()

```c#
// AppendFormat 格式化的字串
double n = 14000;
StringBuilder sb = new StringBuilder("NB價格：");
sb.AppendFormat("{0:C}", n);
Console.WriteLine(sb.ToString()); //NB價格：NT$14,000.00
```

### AppendLine()

```c#
// AppendLine 換行
StringBuilder sb = new StringBuilder("Come what may!");
sb.AppendLine(); //加入換行字元
sb.Append("I'm making an all-out effort.");
Console.WriteLine(sb.ToString());
```

### EnsureCapacity()

```c#
// EnsureCapacity 指定容量
sb.EnsureCapacity(50); //指定目前 StringBuilder 字串容量
Console.WriteLine(sb.Capacity); //目前容量
```

### Insert()

```c#
// Insert 在指定的索引位置上，插入字串。
StringBuilder sb = new StringBuilder("張三李四王五");
sb.Insert(1, 123); //張123三李四王五
Console.WriteLine(sb.ToString());
```

### Remove()

```c#
// Remove 移除
StringBuilder sb = new StringBuilder("張三五");
sb.Remove(1, 1); //張五，索引1 位置 開始移除 1個字元
Console.WriteLine(sb.ToString());
```

### Replace()

```c#
// Replace 取代字元
StringBuilder sb = new StringBuilder("張三五");
sb.Replace("三", "小"); //張小五
Console.WriteLine(sb.ToString());
```

## 範例：文字字串對齊格式化

```c#
StringBuilder sb = new StringBuilder();
sb.AppendFormat("[{0,-5}]",80);
sb.AppendLine(); //換行
sb.AppendFormat("[{0,5}]",99);
Console.WriteLine(sb.ToString());
```

執行結果：

```
[80   ]
[   99]
```

# string 和 StringBuilder 的差別

`string` 為什麼這麼慢？     
因為他要在內存開空間。        

`StringBuilder`因為沒有開空間，所以特別快。


- `string` 是不可變的。(都會分配新的內存空間)

一旦創建了一個 `string` 對象，它的值就不能被修改。如果對其進行操作，將會生成一個新的 `string` 對象。 (一旦創建了一個字串對象，就不能更改其內容。對字串進行修改實際上是建立一個新的字串物件。)

> `string` 是不可變的，所以每次對 `string` 執行操作（例如連接、分割、替換等），都會分配新的內存空間，這可能導致內存碎片化和性能下降。     

- `StringBuilder` 是可變的。(內容可以在緩衝區內進行修改，而無需分配新的內存空間。)

`StringBuilder` 是用於處理可變字串的類型，它允許動態修改其內容而不創建新的對象。這對於大量字串操作很有用，因為它避免了不斷創建新的 `string` 對象，從而提高了性能。 (它允許對字串進行動態的、原地的修改，而不必每次都建立新的物件。)

> `StringBuilder` 類型的對象具有內部緩衝區，用於儲存和修改字串的內容。當進行字串操作時，內容可以在緩衝區內進行修改，而無需分配新的內存空間。



## String

- 當你給字串賦值的時候，舊的資料並不會銷毀，而是在`Heap`(堆積)重新開闢一塊空間儲存新值，同時也產生很多內存垃圾。(所以，字串一旦聲明了就不再可以改變。)
- 當程式結束後，`GC`掃描整個內存，如果發現有的空間沒有被指向，則立即把它銷銷。
- 可以對它理解，每對`string`重新賦值，加一個字元，減一個字元...等等，每一個任何操作，只要操作一次，就會在內存當中開闢一塊新的空間。     

> - `string` 只要對它有任何的操作，就會在內存(記憶體空間)當中開闢一塊新的空間。     
> - `string`是 `ReferenceType`(參考類型)。



[[C# 筆記] string 不可變的特性 by R](https://riivalin.github.io/posts/2011/01/string1/)     
[[C# 筆記] 高效的 StringBuilder by R](https://riivalin.github.io/posts/2011/03/stringbuilder/)      
[[C# 筆記] string 和 StringBuilder 運行時間比較 by R](https://riivalin.github.io/posts/2011/01/string-builder/)     
[[C# 筆記] string 和 StringBuilder 的區別，兩者表現的比較](https://riivalin.github.io/posts/2017/02/csharp-string-stringbuilder/)       