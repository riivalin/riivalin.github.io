---
layout: post
title: "[C# 筆記] 類之間的轉換(向上轉型 vs 向下轉型)"
date: 2010-01-12 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,as,is]
---

```
基類(父類)
派生類(子類)
```

當我們有父類、子類的時候，有時候會需要對這兩個類進行類型的轉換，類之間的轉換，就是這節課的學習任務。

## 明確一下概念
- 向上轉型(upcasting)：把一個派生類(子類)類型轉換為他的基類(父類)
- 向下轉型(downcasting)：把一個基類(父類)轉換為他的某個派生類(子類)


## 向上轉型 vs 向下轉型
- 向上轉型：在轉換過程中，不需要做任何特殊的處理，直接賦值就解決了
- 向下轉型：如果轉換不了，它會拋異常，所以最好要先做判斷

### 向上轉型 upcasting

```c#
//父類/基類
public class Shape {
    //comment
}
//子類/派生類/衍生類
public class Circle:Shape {
    //comment
}
```

- 首先創建一個`Circle`實體化物件，初始化的時候，使用的是`Circle類型`
- 在轉換過程中，不需要做任何特殊的處理，直接使用基類型來處理，直接賦值就解決了。

```c#

Circle circle = new Circle();
Shape shape = Circle; //向上轉型：這個轉化過程是「隱式」的，implicit
Circle circle2 = (Circle)shape; //向下轉型：顯示(explicit)轉化

Car car = (Car)shape; //轉換失敗，拋異常
```
這個轉化過程是「隱式」的，implicit

### 向下轉型 downcasting

向下轉型就稍微複雜一點，需要透過「顯示」的語法操作才能完成轉化。    
※「顯示」：隱含、明確(強制型轉)         

在聲明派生類的同時，使用小括號來指定需要轉換的類型。    

```c#
Shape shape = new Shape();
Circle circle = (Circle)shape; //顯示(explicit)轉化
```

向下轉換有一個非常重要的注意事項：就是轉換有可能會失敗。        
對於失敗的轉化，系統會拋出異常。        

例如下面這種情況，我們強行把一個`shape` 轉化為汽車類：

```c#
Car car = (Car)shape; //轉換失敗，拋異常
```
對於這種情況，當然是轉換不成功了，這情況，系統就會拋出異常，終止程序。      

所以我們在進行向下轉化的時候，必須非常非常的小心，有必要的必的需要使用`try-catch`來捕捉異常。

## as 關鍵詞
- 防止這種低級錯誤，我們可以使用`as`關鍵詞

```c#
Car car = (Car)obj; //可能會拋出異常，終止程序運行
Car car = obj as Car; //不會拋出異常，而是會把轉換失敗的car這個物件設置為空
if(car != null) {
    //執行後續操作
}
```

## is 關鍵詞
- 使用 `is` 關鍵詞，我們可以檢查對象(物件)的類型

```c#
if(obj is Car) {
    Car car = (Car)obj; //絕對不會出錯
}
```

## 練習-向上轉型

先兩個class：`Shape`、`Text`
- 父類：`Shape`形狀
- 子類：`Text`文字

```c#
public class Shape
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public void Draw()
    {
        Console.WriteLine($"width: {Width}, Height: {Height}, position: {x}, {y}");
    }
}

public class Text: Shape
{ 
    public int FontSize { get; set; }
    public string FontName { get; set; }
}

static void Main(string[] args)
{
    //向上轉型
    var text = new Text();
    Shape shape = text; //直接賦值就可以了

    Console.Read();
}
```

在轉換過程中，不需要做任何特殊的處理，直接賦值就解決了。        
C# 中，向上轉型非常強大，`shape`和`text` 對象都同時指向的是同一塊內存地址，也就是說，他們所代表的數據是一模一樣的，但是，同樣的數據，卻展現出不同的表現形式，

---

- TODO 向下轉型, not yet


[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=31](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=31)