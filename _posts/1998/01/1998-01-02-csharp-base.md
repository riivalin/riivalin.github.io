---
layout: post
title: "[C#筆記] C# 基本語法"
date: 1998-01-02 05:30:00 +0800
categories: [Notes,C#]
tags: [基礎語法]
---

Just a note to myself...

# 1. 三元運算式撰寫：判斷i是否為奇數/偶數

`? :`

```c#
int i = 21;
//如果除以2為0，則為偶數，否則為奇數
Console.WriteLine((i % 2 == 0)? "偶數":"奇數");
```

# 2. switch case 判斷式
`switch case`與`break`搭配使用。      
達成一定條件時，就做出相應的事。

`default`為以上條件皆未達成時才會執行。

> 在C#中，若無輸入`break;`，在編譯過程中會出錯。

```c#
int i = Convert.ToInt32(Console.ReadLine()); 
Console.WriteLine("菜菜專賣店");
Console.WriteLine("1: 九層塔茄子炒豆包和豆腐（純素)");
Console.WriteLine("2: 自選菜單");

switch (i) {
    case 1:
        Console.WriteLine("九層塔茄子炒豆包和豆腐（純素)，50元，謝謝惠顧");
        break;
    case 2:
        Console.WriteLine("自選菜單，100元，，謝謝惠顧");
        break;
    default:
        Console.WriteLine("沒有第三選項唷");
        break;
}
```

執行結果：(輸入非1或2)

```
菜菜專賣店
1: 九層塔茄子炒豆包和豆腐(純素)
2: 自選菜單
沒有第三選項唷
```

# 3. 計數式迴圈
## for迴圈
### 列出1~100的奇數

```c#
//列出1~100的奇數
for(int i = 1; i < 100; i += 2) {
    Console.Write($"{i} \t");
}
```

執行結果：

```
1     3     5     7     9     
11     13     15     17     
19     21     23     25     
27     29     31     33  ...
```

### 99乘法表

```c#
for(int i = 1; i < 10; i++) {
    for(int j = 2; j < 10; j++){
        Console.Write($"{j} * {i} = {i*j} \t");
    }
    Console.WriteLine(); //用來換行
}
```

執行結果：

```
2 * 1 = 2     3 * 1 = 3     4 * 1 = 4     5 * 1 = 5     6 * 1 = 6     7 * 1 = 7     8 * 1 = 8     9 * 1 = 9     
2 * 2 = 4     3 * 2 = 6     4 * 2 = 8     5 * 2 = 10     6 * 2 = 12     7 * 2 = 14     8 * 2 = 16     9 * 2 = 18     
2 * 3 = 6     3 * 3 = 9     4 * 3 = 12     5 * 3 = 15     6 * 3 = 18     7 * 3 = 21     8 * 3 = 24     9 * 3 = 27     
2 * 4 = 8     3 * 4 = 12     4 * 4 = 16     5 * 4 = 20     6 * 4 = 24     7 * 4 = 28     8 * 4 = 32     9 * 4 = 36     
2 * 5 = 10     3 * 5 = 15     4 * 5 = 20     5 * 5 = 25     6 * 5 = 30     7 * 5 = 35     8 * 5 = 40     9 * 5 = 45     
2 * 6 = 12     3 * 6 = 18     4 * 6 = 24     5 * 6 = 30     6 * 6 = 36     7 * 6 = 42     8 * 6 = 48     9 * 6 = 54     
2 * 7 = 14     3 * 7 = 21     4 * 7 = 28     5 * 7 = 35     6 * 7 = 42     7 * 7 = 49     8 * 7 = 56     9 * 7 = 63     
2 * 8 = 16     3 * 8 = 24     4 * 8 = 32     5 * 8 = 40     6 * 8 = 48     7 * 8 = 56     8 * 8 = 64     9 * 8 = 72     
2 * 9 = 18     3 * 9 = 27     4 * 9 = 36     5 * 9 = 45     6 * 9 = 54     7 * 9 = 63     8 * 9 = 72     9 * 9 = 81     
```

## foreach
### 列出陣列的值

```c#
string[] names = new string[] {"張三","李四","王五"};
foreach(var e in names) {
    Console.WriteLine(e);
}
```

# 4. 條件式迴圈

- `while`：先做判斷再做
- `do while`：做了再判斷
只要合乎條件，就續繼執行

## while

先判斷再做

```c#
//設i=1, 印出 i, i=1就停止迴圈
int i = 1;
while(i < 1) { //先判斷
    Console.WriteLine(i); //再做
    i++;
}
```

輸出結果：無

## do while

先做再判斷

```c#
int i = 1;
do { //先做
    Console.WriteLine(i);
    i++;
} while(i < 1); //再判斷
```

輸出結果：1


# 5. break 與 continue

- `break`：一跳出，就不會再回到迴圈
- `continue`：只會跳出最內層迴圈

## break

一跳出，就不會再回到迴圈

```c#
for(int i = 0; i < 5; i++) {
    Console.WriteLine($"{i} \t");
    if(i == 3) break; //當i=3時，就跳出迴圈
}
```
執行結果：

```
0     1     2     3  
```

## continue

只會跳出最內層迴圈

```c#
for(int i = 0; i < 5; i++) {
    if(i==3) continue; //當i=3時，跳過不印
    Console.Write($"{i} \t");
}
```

執行結果：

```
0     1     2     4 
```

# 6. using 

- 引用命名空間(namespace) `using System;`
- 定義別名：`解決命名衝突或簡化類型名稱 using Test= Project.PC;`
- 做為陳述式使用：`using { } 來釋放資源`
- using static 指令：無需指定類型名稱即可存取其靜態成員 `using static System.Console;`


[[C# 筆記] Using 作用 by R](https://riivalin.github.io/posts/2021/05/cs-using/)     
[[C# 筆記] using 關鍵字的作用 by R](https://riivalin.github.io/posts/2017/02/the-role-of-using-keyword/)


# 7. try...catch...finally 例外狀況處理 (ExceptionHandling機制)


- `try`：正常執行的程式碼
- `catch`：定義錯誤發生時所執行的程式碼
- `finally`：無論錯誤與否，一定會執行的程式碼

```c#
try {
    //正常執行的程式碼
} catch {
    // 錯誤發生時所執行每的程式碼
} finally {
    //最終必定執行的程式碼，無論錯誤是否發生都會執行的程式碼
}
```

## 範例

```c#
int x = 1;
int y = 0;
try
{
    var result = x/y;
    Console.WriteLine(result);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    Console.WriteLine("執行完成!");
}
```

執行結果：

```
Attempted to divide by zero.
執行完成!
```

# 8. ToList\<TSource> 方法 (List\<T>)

```c#
//宣告水果的陣列
string[] fruits = { "apple", "passionfruit", "banana", "mango",
                    "orange", "blueberry", "grape", "strawberry" };

//使用ToList方法，將陣列轉換成 List<TSource>的型態 (List<int>)
List<int> lengths = fruits.Select(e => e.Length).ToList(); //查詢取得水果的長度

foreach (var e in lengths) {
    Console.WriteLine(e);
}
```

執行結果：

```
5
12
6
5
6
9
5
10
```

[MSDN - Enumerable.ToList<TSource>(IEnumerable<TSource>) 方法
](https://learn.microsoft.com/zh-tw/dotnet/api/system.linq.enumerable.tolist?view=net-8.0)

# 9. class 類別

存取層級

|||
|:---|:----|
| `public` | 公開層級，存取無限制 (任何人都可以存取使用) |
| `protected` | 受保護的，可由此宣告類別或子類別進行存取 |
| `internal` | 存取只限於此專案 (只有同一個 `namespace`的才能存取) (存取限於目前組件) |
| `protected internal` | 存取只限於此專案或是子類別 (`protected`+`internal`的概念) |
| `private` | 私有層級，存取只限於宣告的類別主體 (只有自身類別才能存取使用) |


> `protected`: 只有自身類別與子類別才能存取使用       
> (只要是繼承關係，不管兩者是否在同一程序集中，子類都有訪問父類的權限)

[MSDN - 存取範圍層級 (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/accessibility-levels)        
[[C# 筆記] Protected 存取修飾符 by R](https://riivalin.github.io/posts/2011/02/protected/)


# 10. 欄位(Field)

在類別主體中可能包含數個欄位，用來儲存類別中變數的資料

```c#
public class Test {
    public int number; //建立公開的number欄位
}
```

# 11. 方法(Method)

方法為類別的成員，方法擁有自己的名稱，不可與變數、常數及類別中的屬性名稱重複。    

方法內宣告的變數屬於區域變數，只存在於該方法內，因此各方法間不會影響。

- 方法：有 系統方法 & 自定義方法
- 自定義方法：有 靜態方法 & 物件方法
- 靜態方法：使用`static`，不必建立物件，可直接取用方法，離開方法後，其變數中儲存的值將不會被釋放，在下次呼叫時，變數中保留的資料仍可繼續使用，提供多個執行個體共用。

## 範例：宣告靜態方法

```c#
static void Main(string[] args) {
    //呼叫靜態方法
    var result = Add(10, 20);
}

//靜態方法
static int Add(int x, int y) {
    return x + y;
}
```
    
## 範例：宣告物件方法

```c#
Calc calc = new Calc(); //建立Calc物件
var result = calc.Add(10, 20); //呼叫Calc物件中的 Add方法

//公開的 Calc類別
public class Calc {
    //公開的 Add方法
    public int Add(int x, int y) {
        return x + y;
    }
}
```

## 使用系統方法

```c#
//使用Console類別中的 WriteLine()和 ReadLine()方法
Console.WriteLine("Hello");
Console.ReadLine();
```

# 12. 屬性(Property)

屬性包含欄位和方法

- 對於物件的使用者，屬性會以欄位出現
- 對於類別的實作者，屬性是一或二個程式碼區塊(`get`和`set`)

## 自動屬性

```c#
//不用宣告私有變數，系統會自動建立
public int Month { get; set; }
```

## 非自動屬性

```c#
public class Date
{
    //宣告私有變數 month 儲存月份，並給予初始值0
    private int month = 0;

    //定義月份屬性
    public int Month {
        //回傳目前月份
        get { return month; }

        //設定月份
        set {
            //月份介面1~12的數字
            if (value > 0 && value < 13) {
                //若判斷為真，設定月份
                month = value;
            }
        }
    }
}
```

# 13. 方法多載(Overload)

可以建立具有相同名稱，不同參數型別及數量的方法，呼叫方法時會依照傳入的參數決定執行的方法。

```c#
//建立回傳型別為int的Add方法，接受兩個int型別的參數
int Add(int x, int y) {
    return x + y;
}

//建立回傳型別為decimal的Add方法，接受兩個decimal型別的參數
decimal Add(decimal x, decimal y) {
    return x + y;
}
```
        
[[C# 筆記] Overload 方法的重載 by R](https://riivalin.github.io/posts/2011/01/overload/)


# 14. 繼承與覆寫 (Inheritance & Override)

- 一個類別可以承接另一個類別的內容。    
- 此外還可以利用覆寫方式修改承接而來的內容。

## 繼承(Inheritance)

- 一個類別可以繼承另一個類別。
- 繼承後的類別即為：子類別、或是衍生類別。
- 子類別會擁有父類別定義的資料或行為。
- 繼承方式為：在宣告類別時於類別名稱後加上「`:`冒號」，然後指定父類別的名稱。

```c#
public class Person { } //父類別
public class Man: Person { } //子類別(衍生類別)
```

## 覆寫(Override)

- 父類別中的方法可以使用`virtual`修飾詞定義此方法可被覆寫。 
- `virtual`修飾詞不能與 `static`、`abstract`、`private`、`override`等修飾詞一起使用，且不可以用於靜態方法上。   
- 繼承的子類別可以使用關鍵字`override`修飾詞，用來擴充或修改繼承的方法及屬性…等父類別的成員。

## 範例

```c#
Add o = new Add();
Console.WriteLine(o.Calc(1,2)); //3

//父類別
public class Cal {
    //建立Calc方法，此方法可以被子類別覆寫 (加上virtual)
    public virtual int Calc(int x, int y) {
        return x * y;
    }
}

//子類別(衍生類別)
public class Add : Cal { //繼承Cal類別
    //覆寫Calc方法 (加上override，修改成加法運算)
    public override int Calc(int x, int y) { 
        return x + y; 
    }
}
```


# 15. 介面(Interface)

介面可以定義為抽象的概念。      

與類別(Class)相異之處：
- 介面有包含屬性和方法，但只有宣告，不包含實作內容。
- 實作內容須藉由類別繼承的方式來實作介面。

類別實作介面時兩個規則：
1. 類別只能繼承一個父類別，但可以同時實作多個介面。
2. 類別實作介面時，只會得到方法的名稱，但不包含任何實作內容。

> 介面本身是不提供實作的

## 範例

```c#
//宣告IMove介面類型的 car物件
IMove car = new Car();
//呼叫實作的Move方法
car.Move();

//宣告一個介面IMove
public interface IMove {
    //宣告一個方法，名稱為Move
    void Move();
}

//宣告一個Car類別，並實作IMove介面
public class Car : IMove {
    //實作介面中的Move方法
    public void Move() {
        Console.WriteLine("Car 移動 10M");
    }
}
```


[[C# 筆記] 介面與實作 by R](https://riivalin.github.io/posts/2021/05/cs-interface/)