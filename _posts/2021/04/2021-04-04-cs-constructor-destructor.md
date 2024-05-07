---
layout: post
title: "[C# 筆記] 建構函式(Constructor) & 解構函式(Destructor)"
date: 2021-04-04 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,方法與參數,Constructor,destructor,建構函式(Constructor),解構函式(Destructor)]
---

# 建構函式(Constructor) 初始化新物件成員

`Constructor`就是「建構函式」為類別的方法，當對類別進行實體化成為物件時，便會自動執行建構函式的方法，其特點：

- 「建構函式名稱」與「類別名稱」相同。
- 當對類別透過 `new` 進行實體化成為物件時，會自動執行建構函式內的敘述(程式碼)。
- 「建構函式」主要作用在於：對物件進行初始化設定。
- 「建構函式」可以建立多個來達成多載(`Overloading`)。
- 若「建構函式」透過 `static` 關鍵字而成「靜態建構函式」時，便不能使用存取修飾詞，也無法使用參數來傳遞。
- 「靜態建構函式」是不能夠被直接叫用(`Invoke`)。

> 靜態建構函式可用來初始化任何 靜態 數據，或執行只需要執行一次的特定動作。 在建立第一個執行個體或參考任何靜態成員之前，會自動進行呼叫。 靜態建構函式最多會呼叫一次。(靜態建構函式只會執行一次)


## 範例

使用構造函式，只要一行，在初始化物件時去賦值
```c#
//在初始化Student物件時去賦值
Student student = new Student("小明", 18, '男', 89, 60);


public class Student 
{
    //構造函式
    public Student(string name, int age, char gender, int english, int math)
    {
        this.Name = name;
        this.Age = age;
        this.Gender = gender;
        this.English = english;
        this.Math = math;
    }

    public string Name { get; set; }
    public int Age { get; set; }
    public char Gender { get; set; }
    public int English { get; set; }
    public int Math { get; set; }
}
```

## new 關鍵字

```c#
Person person = new Person();
```

`new` 幫助我們做了三件事：

1. 在內存中開闢一塊空間
2. 在開闢的空間中創建物件
3. 調用物件的「構造函數」進行初始化物件


# 解構函式(Destructor) 釋放物件所佔用的資源

`Destructor`就是「解構函式」為類別的方法，主要用來釋放該物件所配置的資源，當物件被 `Dispose`或`Close`時，便會自動執行「解構函式」，其特點：

- 「解構函式名稱」與「類別名稱」相同，但須於「解構函式」前面加一個「`~`」符號。
- 「解構函式」只能於類別中定義，而且一個類別只能有一個「解構函式」。
- 「解構函式」主要作用在於：對物件進行資源釋放動作。
- 「解構函式」會隱含呼叫物件的基底類別`Finalize()`方法。
- 「解構函式」不使用存取修飾詞或參數。
- 「解構函式」會自動被叫用，並不能直接被呼叫。
- 「解構函式」不能被多載。

        
## 範例

```c#
//解構函式
//作用：幫助我們釋放資源
~Student() 
{
    // Cleanup statements...    
}
```

# 範例

```c#
class Student
{
    //建構函式
    public Student() {
        IQ = 100;
        EnrollScore = 0;
    }
    public Student(int iq, int enrollScore ) {
        this.IQ = iq;
        this.EnrollScore = enrollScore;
    }
    public int IQ { get; set; } //智商
    public int EnrollScore { get; set; } //入學成績
    public string? ClassResult { get; set; } //分配班級結果

    public override string ToString()
    {
        if (IQ > 150 && EnrollScore > 500)
        {
            ClassResult = "資優班";
        } else {
            ClassResult = "普通班";
        }
        return ClassResult;
    }

    //解構函式
    ~Student() 
    {
        Console.WriteLine("呼叫解構函式，Student物件資源釋放中");
    }
}
```


[MSDN - 靜態建構函式 (C# 程式設計手冊)](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/static-constructors)      
[MSDN -  完成項 (舊稱為解構函式) ](https://learn.microsoft.com/zh-tw/dotnet/csharp/programming-guide/classes-and-structs/finalizers)        
[[C# 筆記] Constructor 構造函式  by R](https://riivalin.github.io/posts/2011/01/constructor/)       
[[C# 筆記] destructor 解構函式  by R](https://riivalin.github.io/posts/2011/01/destructor/)