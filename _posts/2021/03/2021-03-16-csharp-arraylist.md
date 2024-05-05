---
layout: post
title: "[C# 筆記] 可存放多樣型態的 ArrayList 類別"
date: 2021-03-16 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,array,arraylist]
---

可以將 `ArrayList` 視為`Array`的功能強化版。        

`Array`陣列裡面的元素的資料型態都必須一樣，     
如果我們想要在陣列中存放各種不同的資料型態，可以藉助`ArrayList` 類別來達成。        

- `Array` 長度不可變，類型單一(資料型別相同)。(若要改長度要用`Array.Resize()方法`)
- `ArrayList`集合的好處：長度可以任意改變，類型隨便。


# Array 和 ArrayList 比較表

|               | Array          | ArrayList |
|:--------------|:-----------------|:--------|
| 初始陣列資料型別 | 依宣告所決定     | 均為`Object`型態 |
| 資料型別        | 陣列內的所有元素都為相同資料型別 | 陣列內元素可以是任何`Object`可接受的資料型別|
| 陣列容量       | 可用 `Array.Resize()`方法來變更其陣列大小 | 根據需要「自動擴充」陣列大小   |
| 陣列下限設定      | 可以                   | 不可以，永遠都是零 |
| 陣列維度(Dimension) | 可以多維    | 永遠都是「一維」 |
| 命名空間          | `System`     | `System.Collections` |
| 佔用記憶體空間 | 以同樣元素個數來說，較小     | 以同樣元素個數來說，較大 |
| 使用時機          | 明確知道要存放的資料個數，並且資料型別相同   | 需存放各種不同資料型別元素，且元素存放個數不確定 |


# 宣告方式

```c#
ArrayList 陣列名稱 = new ArrayList();
ArrayList arrList = new ArrayList();
```

# ArrayList vs List 有何差異？

- 命名空間不同：
    - `ArrayList` 命名空間為 `System.Collections`
    - `List` 命名空間為 `System.Collections.Generic`

- 效能差異：
    - `ArrayList`可以存放不同類型的數據，所以常發生「裝箱&拆箱」的操作(會帶來很大的效能耗損)，`ArrayList`處理數據時，很可能會報類型不匹配的錯誤，所以`ArrayList`不是類型安全的。
    - 若存放相同資料型別元素時，用`List`，因為減少型別轉換，所以`List`獲得較高的效能。

- `List<T>` 類別在大多數情況下比 `ArrayList` 類別執行得更好且類型安全的

> 由於`ArrayList`存在不安全型別與裝箱拆箱的缺點，所以出現了`List<T>`泛型的概念。        
> (裝箱與拆箱的過程是很耗效能的。)

[C#中 数组、ArrayList、List＜T＞的区别](https://blog.csdn.net/Dust_Evc/article/details/114984023)


# 範例

有學生資料：學號、姓名、年齡、就學狀態，將這些資料存放在 ArrayList 中

```c#
static void Main(string[] args)
{
    //學生資料:學號、姓名、年齡、就學狀態
    string id = "S001";
    string name = "Rii";
    int age = 99;
    bool status = true;

    //宣告一個類型為 arraylist 的 student 的變數
    ArrayList student = new ArrayList();

    //將學生資料放入arraylist中
    student.Add(id);
    student.Add(name);
    student.Add(age);
    student.Add(status);

    //顯示資料型別
    foreach (var item in student)
    {
        Console.WriteLine(item.GetType());
    }
}
```

執行結果：

```
System.String
System.String
System.Int32
System.Boolean
```

# ArrayList 的 Trade-Off(衡權)?

`ArrayList`可以很便利地用來儲存任何「實值型別」或「參考型別」，但是這種便利會付出相當的代價。       

因為加入 `ArrayList`的任何「實值型別」或「參考型別」，都會轉換成`Object`類型。      

加入清單時，則會執行 `boxed`(裝箱)處理，      
擷取清單項目時，則會執行`unboxed`(拆箱)處理。   

> 裝箱與拆箱的過程是很耗效能的。    

所以當大量資料加到 `ArrayList`時，會變成「效能的殺手」，在使用時必須要特別小心注意。        

        

[[C# 筆記] ArrayList 集合 by R](https://riivalin.github.io/posts/2011/01/arraylist/)        
[C#中 数组、ArrayList、List＜T＞的区别](https://blog.csdn.net/Dust_Evc/article/details/114984023)