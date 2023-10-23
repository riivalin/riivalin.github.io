---
layout: post
title: "[閱讀筆記][Design Pattern] Ch9.原型模式 Prototype (深淺複製 MemberwiseClone())"
date: 2009-03-09 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 原型模式 Prototype
原型模式(Prototype)：用原型實例指定建立物的種類，並且透過拷貝這些原型建立新的物件。

「原型模式(Prototype)」其實就是從一個物件再建立另一個可訂製的物件且不需要知道任何建立的細節。


## 原型模式(Prototype)結構

- `Client`用戶端：讓一個原型複製自身，從而建立一個新的物件
- `Prototype` 原型類別：聲明一個複製自身的介面
- `ConcretePrototype` 具體原型類別：實現一個複製自身的操作

```
Client 讓一個原型複製自身，從而建立一個新的物件
- prototype

Prototype 原型類別，聲明一個複製自身的介面
+ Clone()

    ConcretePrototype1 具體原型類別，實現一個複製自身的操作
    + Clone()

    ConcretePrototype2
    + Clone()

```

## 原型模式(Prototype)程式碼

### 原型類別(抽象類別)
「抽象類別」關鍵就是只有這樣一個`Clone`方法. 

```c#
abstract class Prototype {
    private string id;
    public string Id {get;}

    public Prototype(string id) {
        this.id = id;
    }

    //抽象類別關鍵就是只有這樣一個Clone方法
    public abstract Prototype Clone();
}
```

### 具體原型類別

使用C#的 深淺複製 `MemberwiseClone()`來實現。

```c#
class ConcretePrototype1: Prototype {
    public ConcretePrototype1(string id): base(id) { }

    //重寫抽象Clone方法
    public override Prototype Clone() {
        //使用C#的 深淺複製 `MemberwiseClone()`來實現
        return (Prototype)this.MemberwiseClone();
    }
}
```

> 其實對於.NET而言，原型類別`Prototype`是用不著的，因為複製實在太常用了，所以 .NET在System命名空間中提供了`ICloneable`介面，其唯一的一個方法是`Clone()`，只需要實現這個介面就可以完成原型模式了。


### 用戶端程式碼

```c#	
public static void Main()
{
    ConcretePrototype1 p1 = new ConcretePrototype1("I");
    ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
    Console.WriteLine(c1.Id);
}
```

## 履歷程式v1.0-初步實現
### 履歷類別
```c#
//履歷類別
class Resume {
    string name;
    string sex;
    string age;
    string timeArea;
    string company;
    public Resume(string name) {
        this.name = name;
    }
    //設定個人資訊
    public void SetPersonInfo(string sex, string age) {
        this.sex = sex;
        this.age = age;
    }
    //設定工作經歷
    public void SetWorkExperience(string timeArea, string company) {
        this.timeArea = timeArea;
        this.company = company;
    }
    //顯示
    public void Show() {
        Console.WriteLine($"{name} {sex} {age}");
        Console.WriteLine($"{timeArea} {company}");
    }
}
```

### 用戶端程式碼調用
如果要三份履歷

```c#
public static void Main()
{
    //第一份履歷
    Resume resume1 = new Resume("Rii");
    resume1.SetPersonInfo("女",100);
    resume1.SetWorkExperience("1900-1910","A company");
    resume1.Show();

    //第二份履歷
    Resume resume2 = resume1; //傳參考
    resume2.Show();

    //第三份履歷
    Resume resume3 = resume1;
    resume3.Show();
}
```


## 履歷程式v2.0-原型實現

.NET在System命名空間中提供了`ICloneable`介面，其唯一的一個方法是`Clone()`，只需要實現這個介面就可以完成原型模式了。

### 履歷結構

```
簡歷類別
+ Clone(): object
+ 設定個人資料(in sex: string, in age: string)
+ 設定工作經歷(in timeArea: string, in company: string)
+ 顯示()
```

### 履歷類別

```c#
//履歷類別
class Resume: ICloneable{
    string name;
    string sex;
    string age;
    string timeArea;
    string company;
    public Resume(string name) {
        this.name = name;
    }
    //設定個人資訊
    public void SetPersonInfo(string sex, string age) {
        this.sex = sex;
        this.age = age;
    }
    //設定工作經歷
    public void SetWorkExperience(string timeArea, string company) {
        this.timeArea = timeArea;
        this.company = company;
    }
    //顯示
    public void Display() {
        Console.WriteLine($"個人資訊: {name} {sex} {age}");
        Console.WriteLine($"工作經歷: {timeArea} {company}");
    }
    //實現ICloneable介面的Clone()方法以完成原型模式
    public Object Clone() {
        return this.MemberwiseClone();
    }
}
```

### 用戶端調用的程式碼
只需要調用`Clone()`方法就可實現新履歷的產生，並且可以再修改新履歷的細節。

```c#
public static void Main()
{
    //第一份履歷
    Resume resume = new Resume("Rii");
    resume.SetPersonInfo("女","100");
    resume.SetWorkExperience("1900-1910","XX公司");
    //第二份履歷
    Resume resume1 = (Resume)resume.Clone(); //只需要調用Clone()方法就可實現新履歷的產生
    resume1.SetWorkExperience("1910-1920","YY公司");//並且可以再修改新履歷的細節
    //第二份履歷
    Resume resume2 = (Resume)resume.Clone();
    resume2.SetPersonInfo("女","99");

    resume.Display();
    resume1.Display();
    resume2.Display();
}
/* 結果顯示
個人資訊: Rii 女 100
工作經歷: 1900-1910 XX公司
個人資訊: Rii 女 100
工作經歷: 1910-1920 YY公司
個人資訊: Rii 女 99
工作經歷: 1900-1910 XX公司
*/
```

如果物件每`NEW`一次，都需要執行一次建構式，如果建構式的執行時間很長，那麼多次的執行這個初始化操作就太沒效率了。     

一般在初始化的資訊不發生變化的情況下，複製是最好的辦法。這既隱藏了物件的細節，又對性能大大的提高。      

它等於不用重新初始化物件，而是動態地獲得物件執行時的狀態。

# C# 深淺複製 MemberwiseClone()

所謂深淺複製可解讀為：

- 淺複製：在C#中呼叫`MemberwiseClone()`方法即為淺複製。如果欄位是值類型的，則對欄位執行逐位複製，如果欄位是參考類型的，則複製物件的參考，而不複製物件，因此：原始物件和其副本參考同一個物件！

> 什麼意思呢？也就是說如果你的「履歷」類別當中有物件參考，那麼參考的物件資料是不會被複製過來的。(參考的物件都仍然指向原來的物件)

- 深複製：如果欄位是值類型的，則對欄位執行逐位複製，如果欄位是引用類型的，則把參考型別的物件指向一個全新的物件！

> 「深複製」把參考物件的變數指向複製過的新物件，而不是原有的被參考的物件。      

由於在一些特定場合，會經常涉及「深淺複製」，比如說：        
資料集物件`DataSet`它就有`Clone()`和`Copy()`方法    
- 淺複製：`DataSet`的`Clone()`方法，用來複製`DataSet`的結構，但不複製`DataSet`的資料，實現了原型模式的「淺複製」。
- 深複製：`DataSet`的`Copy()`方法，不但複製結構，也複製資料，其實就是實現了原型模式的「深複製」。


## 履歷程式v3.0-淺複製實現

由於它是「淺複製」，所以對於值類型，沒什麼問題，對參考類型，就只是複製了參考，參考的物件還是指向了原來的物件。      

所以就會出現我給resume1、resume2、resume3三個參考設定了「工作經歷」，但卻同時看到三個參考都是後一次設定，因為三個參考都指向了同一個物件。

### 結構(淺複製)
「履歷類別」當中的「設定工作經歷」方法，在現實設計當中，通常會再有一個「工作經歷」類別，當中有「時間區間」和「公司名稱」等屬性，「履歷」類別直接調用這個物件即可。

```
簡歷: ICloneable
+ Clone():object
+ SetPersonInfo(in sex: string, in age: string)
+ SetWorkExperience(in timeArea: string, in company: string)
+ Display()

工作經歷
+ TimeArea: string
+ Company: string
```

### 程式碼(淺複製)

```c#
class Resume: ICloneable {
    string name;
    string sex;
    string age;

    //參考「工作經歷」物件
    WorkExperience work; //宣告「工作經歷」物件

    public Resume(string name) {
        this.name = name;
        //在簡歷類別實體化時，同時實體化「工作經歷」物件
        work = new WorkExperience();
    }

	//設定個人資訊
    public void SetPersonInfo(string sex, string age) {
        this.sex = sex;
        this.age = age;
    }
    //設定工作經歷
    public void SetWorkExperience(string timeArea, string company) {
        //調用此方法時，給物件的兩個屬性賦值
        work.TimeArea = timeArea;
        work.Company = company;
    }
    //顯示
    public void Display() {
        Console.WriteLine($"個人資訊: {name} {sex} {age}");
        Console.WriteLine($"工作經歷: {work.TimeArea} {work.Company}");
    }

    //使用MemberwiseClone複製
    //實現ICloneable介面的Clone()方法以完成原型模式
    public Object Clone() {
        return this.MemberwiseClone();
    }
}

//工作經歷類別
class WorkExperience {
    public string TimeArea { get; set; }
    public string Company { get; set; }
}
```

### 用戶端調用程式碼(淺複製)

沒有達到我們的要求，三次顯示的結果都是最後一次設定的值。     

由於它是「淺複製」，所以對於值類型，沒什麼問題，對參考類型，就只是複製了參考，參考的物件還是指向了原來的物件。      

所以就會出現我給resume1、resume2、resume3三個參考設定了「工作經歷」，但卻同時看到三個參考都是後一次設定，因為三個參考都指向了同一個物件。

```c#
public static void Main()
{
    //沒有達到我們的要求，三次顯示的結果都是最後一次設定的值
    Resume resume1 =  new Resume("Rii");
    resume1.SetPersonInfo("女","100");
    resume1.SetWorkExperience("1900-1910","AAA");

    Resume resume2 = (Resume)resume1.Clone();
    resume2.SetWorkExperience("1910-1920","BBB");

    Resume resume3 = (Resume)resume1.Clone();
    resume3.SetWorkExperience("1920-1930","CCC");

    resume1.Display();
    resume2.Display();
    resume3.Display();
}

/* 結果顯示:
個人資訊: Rii 女 100
工作經歷: 1920-1930 CCC
個人資訊: Rii 女 100
工作經歷: 1920-1930 CCC
個人資訊: Rii 女 100
工作經歷: 1920-1930 CCC
*/
```

## 履歷程式v4.0-深複製實現

剛例子，我們希望resume1、resume2、resume3三個參考的物件都是不同的，複製時就一變二，二變三，此時，我們就稱這種方式叫「深複製」。

> 把參考型別的物件指向一個「全新的物件」，而不是指向「原有的物件」！

### 結構(深複製)

```
簡歷: ICloneable
+ Clone():object
+ SetPersonInfo(in sex: string, in age: string)
+ SetWorkExperience(in timeArea: string, in company: string)
+ Display()

工作經歷: ICloneable
+ TimeArea: string
+ Company: string
+ Clone(): object
```

### 程式碼(深複製)

1. 新增一個「私有建構函式」，調用「工作經歷」的 Clone()方法，以便複製「工作經歷」的資料
2. 呼叫私有的建構函式，讓「工作經歷」複製完成，然後再給這個「履歷」物件的相關欄位賦值，最終回傳一個「深複製」的履歷物件

```c#
class Resume: ICloneable {
    string name;
    string sex;
    string age;

    //參考「工作經歷」物件
    WorkExperience work; //宣告「工作經歷」物件
	
	public Resume(string name) {
		this.name = name;
        //在簡歷類別實體化時，同時實體化「工作經歷」物件
		work = new WorkExperience();
	}
    //私有建構函式
    private Resume(WorkExperience work) {
        //調用「工作經歷」的 Clone()方法，以便複製「工作經歷」的資料
        this.work = (WorkExperience)work.Clone();
    }

	//設定個人資訊
    public void SetPersonInfo(string sex, string age) {
        this.sex = sex;
        this.age = age;
    }
    //設定工作經歷
    public void SetWorkExperience(string timeArea, string company) {
        //調用此方法時，給物件的兩個屬性賦值
		work.TimeArea = timeArea;
		work.Company = company;
    }
    //顯示
    public void Display() {
        Console.WriteLine($"個人資訊: {name} {sex} {age}");
        Console.WriteLine($"工作經歷: {work.TimeArea} {work.Company}");
    }

	//使用MemberwiseClone複製
    //實現ICloneable介面的Clone()方法以完成原型模式
	public Object Clone() {
		//呼叫私有的建構函式，讓「工作經歷」複製完成，然後再給這個「履歷」物件的相關欄位賦值，最終回傳一個「深複製」的履歷物件
        Resume obj = new Resume(this.work);
        obj.name = this.name;
        obj.sex = this.sex;
        obj.age = this.age;
        return obj;
	}
}

//工作經歷類別
class WorkExperience {
    public string TimeArea { get; set; }
    public string Company { get; set; }

    //實現ICloneable介面的Clone()方法複製
	public Object Clone() {
		return this.MemberwiseClone();
	}
}
```

### 用戶端調用的程式碼(深複製)

同之前的用戶端程式碼，其結果顯示：達到了我們希望的三次顯示結果各不同的需求。

```c#
public static void Main()
{
    //同之前的用戶端程式碼，其結果顯示：達到了我們希望的三次顯示結果各不同的需求
    Resume resume1 =  new Resume("Rii");
    resume1.SetPersonInfo("女","100");
    resume1.SetWorkExperience("1900-1910","AAA");

    Resume resume2 = (Resume)resume1.Clone();
    resume2.SetWorkExperience("1910-1920","BBB");

    Resume resume3 = (Resume)resume1.Clone();
    resume3.SetWorkExperience("1920-1930","CCC");

    resume1.Display();
    resume2.Display();
    resume3.Display();
}

//個人資訊: Rii 女 100
//工作經歷: 1900-1910 AAA
//個人資訊: Rii 女 100
//工作經歷: 1910-1920 BBB
//個人資訊: Rii 女 100
//工作經歷: 1920-1930 CCC
```


Ref：
[MSDN Object.MemberwiseClone 方法](https://learn.microsoft.com/zh-tw/dotnet/api/system.object.memberwiseclone?view=net-7.0)     
[C# 深浅复制 MemberwiseClone](https://www.cnblogs.com/chenwolong/p/MemberwiseClone.html)