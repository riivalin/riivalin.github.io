---
layout: post
title: "[C# 筆記] 命名空間(Namespace)"
date: 2021-05-08 24:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,namespace,using]
---

# 什麼是命名空間(Namespace)？

命名空間(`Namenspac`)主要功能「用來宣告範圍」。(1).能有效管控專案中的類別和方法的範圍。只要透過 `using` 關鍵字就可引入命名空間(在一個專案中，引用另一個專案，必須加入參考)，就可以使用該命名空間中的定義。(2).能夠加以擴充原有命名空間的功能。例如：宣告`using System.IO {}`來擴充原有系統`IO`功能。


- 用來指明程式所屬範圍的機制。
- 為類別建立層級組織(Hierarchical Organization)，方便管理。
- 避免相同名稱類別產生衝突。
- 控制類別的範圍。
- 功能擴充。


> 用於解決類別重名問題，可以看做「類別的文件夾」        
> 一個資料夾(目錄)中可以包含多個資料夾，每個資料夾中不能有相同的檔案名，但不同資料夾中的檔案可以相同的檔名。

## 範例

設計一個簡單的命名空間宣告範例：    

- 程式包含**巢狀式***的命名空間
- 包含了`enum`列舉宣告、`struct`結構宣告
- 可以瞭解：交通工具(Vehicle)汽車(Car)底下有跑車(SportCar)、敞篷車(Convertible)、休旅車(SUVs)類別

### 巢狀式命名空間

```c#
namespace Vehicle { //宣告交通工具命名空間
	namespace Car { //宣告汽車命名空間
        public class SportCar { } //跑車
		public class Convertible { } //敞篷車
		public class SUVs { } //休旅車
	}
	namespace Train { //宣告火車命名空間
		class MagLev { } //磁浮火車
	}
	namespace Airplane { //宣告飛機命名空間
		class Airsuperiority { } //戰鬥機
	}
}
```

### 加上 enum 列舉宣告、 struct 結構宣告

```c#
namespace Vehicle //宣告交通工具命名空間
{ 
	namespace Car  //宣告汽車命名空間
    { 
        //宣告汽車輪胎尺寸的列舉
        enum CarWheel //enum 列舉宣告
        {
            Eighteen = 18, Seventeen = 17, Sixteen = 16, Fifteen = 15
        }

        //宣告汽車特性的結構
        struct CarProfile //struct 結構宣告
        {
            public string CarTechnology; //引擎技術
        }

        public class SportCar { } //跑車
		public class Convertible { } //敞篷車
		public class SUVs { } //休旅車
	}
	namespace Train { //宣告火車命名空間
		class MagLev { } //磁浮火車
	}
	namespace Airplane { //宣告飛機命名空間
		class Airsuperiority { } //戰鬥機
	}
}
```

### 加上屬性、方法

```c#
namespace Vehicle //宣告交通工具命名空間
{
    namespace Car  //宣告汽車命名空間
    {
        //宣告汽車輪胎尺寸的列舉
        enum CarWheel //enum 列舉宣告
        {
            Eighteen = 18, Seventeen = 17, Sixteen = 16, Fifteen = 15
        }

        //宣告汽車特性的結構
        struct CarProfile //struct 結構宣告
        {
            public string CarTechnology; //引擎技術
        }

        //跑車類別
        public class SportCar
        {
            //設定輪胎尺寸為18吋
            public static int wheel = (int)CarWheel.Eighteen;

            //汽車屬性
            private static string carName;
            public static string CarName
            {
                get 
                {
                    //若設定跑車屬性內容值為"Audi R8"，則回傳值會增加"奧迪當家跑車"字串
                    return (carName == "Audi R8") ? $"奧迪當家跑車：{carName}" : carName;
                }
                set { carName = value; }
            }

            //方法
            public static string Turbo(bool isTurbo) 
            {
                CarProfile cp;
                cp.CarTechnology = (isTurbo) ? "渦輪增壓" : "自然進氣";
                return cp.CarTechnology;
            }
        }
        public class Convertible { } //敞篷車
        public class SUVs { } //休旅車
    }
    namespace Train
    { //宣告火車命名空間
        class MagLev { } //磁浮火車
    }
    namespace Airplane
    { //宣告飛機命名空間
        class Airsuperiority { } //戰鬥機
    }
}
```

### 調用執行

```c#
//叫用Vehicle命名空間，設定跑車名稱
Vehicle.Car.SportCar.CarName = "Audi R8";

//顯示取得跑車名稱的屬性
Console.WriteLine(Vehicle.Car.SportCar.CarName);

// 執行結果：
// 奧迪當家跑車：Audi R8
```

## 定義命名空間

在命名空間內，可以包含下列一或多個類型：
- `class`類別
- `interface`介面
- `struct`結構
- `enum`列舉
- `delegate`委派
- `namespace`命名空間(可以宣告巢狀命名空間，但不能在檔案範圍的命名空間宣告中)

```c#
namespace 命名空間名稱
{
    [namespace|class|interace|struct|enum|delegate]
}
```

## Using 關鍵字
只要透過 `using` 關鍵字就可引入命名空間(在一個專案中，引用另一個專案，必須加入參考)，就可以使用該命名空間中的定義。

### 範例

例如，我們使用了`System`命名空間中`Console`類別的`WriteLine`方法        
使用全名會是這樣寫：

```c#
System.Console.WriteLine("Hi");
```

使用`using`引入命名空間，就不需要寫完整全名：

```c#
using System; //使用using引入命名空間
Console.WriteLine("Hi");
```


> `using`作用：
> 1. `using` 指令：引入命名空間
> 2. `using static` 指令：無需指定類型名稱即可存取其靜態成員
> 3. `using` 建立别名
> 4. `using` 語句：將實體與程式碼綁定，結束後自動Dispose，釋放實體資源。 【與資料庫互動時常用到】


## 控制類別的範圍

- 當類別太多時，可以用命名空間加以分類      
- 當類別名稱相同或類似時，可以用命名空間加以分類        

```c#
namespace 命名空間1 {   // 命名空間1
    class 類別1 {       // 命名空間1.類別1
        class 類別 { }  // 命名空間1.類別1.類別
    }

    namespace 命名空間2 { // 命名空間1.命名空間2
        class 類別 { }   // 命名空間1.命名空間2.類別
    }
}
```

## 功能擴充

已存在的命名空間或是其它廠商開發的程式，可以宣告同名的命名空間以擴充其功能。

```c#
namespace System  //擴充.Net Framework提供的System命名空間的功能
{
    class 類別 { }
}
```

## 巢狀命名空間

`namespace`是可以巢狀定義的。       
下面兩個方式定義的其實是一模一樣的。 

```c#
// 寫法一：
namespace N1.N2  {
    class A { }
    class B { }
}

// 寫法二：
namespace N1 {
    namespace N2 {
        class A { }
        class B { }
    }
}
```

使用方式如下：

```c#
// 寫法一：
N1.N2.A test = new();

// 寫法二：
using N1.N2;
A test = new();
```

為巢狀命名空間 建立別名

```c#
// 寫法一：
using Test = N1.N2 //為巢狀命名空間 起別名
Test.C1 c = new(); //使用別名宣告物件

// 寫法一：
using Test = N1.N2.C1 //為巢狀命名空間.類別 起別名
Test t = new(); //使用別名宣告物件

//巢狀命名空間
namespace N1 {
    namespace N2 {
        class C1 { }
    }
}
```
        
## 使用命名空間的原因

C# 程式設計大量使用命名空間的原因有兩個。 
1. .NET 會使用命名空間組織其多種類別
2. 宣告您自己的命名空間，將有助於在較大型的程式設計專案中控制類別和方法名稱的範圍。

### 1. 使用命名空間組織其多種類別

.NET 會使用命名空間組織其多種類別，能有效管控專案中的類別和方法的範圍。如下所示：

```c#
System.Console.WriteLine("Hi");
```

`System` 是命名空間，而 `Console` 是該命名空間中的類別。        
您可以使用 `using` 關鍵字就可完成命名空間的匯入，如此就不需要完整名稱，如下列範例所示：

```c#
using System;
Console.WriteLine("Hi");
```

### 2. 宣告自己的命名空間

宣告您自己的命名空間，將有助於在較大型的程式設計專案中控制類別和方法名稱的範圍。

```c#
//宣告自己的命名空間
namespace TestNamespace { //命名空間
    class TestClass { //類別
        public void TestMethod() { //方法
            Console.WriteLine("Hi");
        }
    }
}
```

C#10 開始，命名空間`namespace`可節省水平空間和大括弧，此新語法的優點是較為簡單， 可讓您的程式碼更容易讀取。

```c#
namespace TestNamespace; //命名空間

class TestClass { //類別
    public void TestMethod() { //方法
        Console.WriteLine("Hi");
    }
}
```

## 命名空間概觀

命名空間具有下列屬性：

- 命名空間可組織大型程式碼專案。
- 命名空間會使用 `.` 運算子分隔。
- `using` 指示詞讓您不需要指定每個類別的命名空間名稱。
- `global` 命名空間是「根」命名空間：`global::System` 一律會參考 .NET `System` 命名空間。

`global`與 `::` 一起時才有作用，而不能使用`global.命名空間`

```c#
class 類別 { }
    
class 類別1
{
    global::類別 物件 = new global::類別();
}
```

        
[MSDN - 宣告命名空間，組織型別](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/types/namespaces)      
[MSDN - 命名空間 ](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/namespace)   
[[C#] 命名空間(Namespace) by yehyeh](http://notepad.yehyeh.net/Content/CSharp/CH01/04Namespace/2NameSpace/index.php)          
[CSDN -  C#【中级篇】C# 命名空间（Namespa](https://blog.csdn.net/sinat_40003796/article/details/125214814)
[[C# 筆記] namespace 命名空間  by R](https://riivalin.github.io/posts/2011/01/namespace/)   
[[C# 筆記] Using 作用](https://riivalin.github.io/posts/2021/05/cs-using/)     