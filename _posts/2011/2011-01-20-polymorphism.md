---
layout: post
title: "[C# 筆記] 多型-虛方法 Virtual (virtual,override)"
date: 2011-01-20 22:39:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 多態的虛方法
概念：讓一個物件能夠表現出多種的狀態(類型)    

## 實現多態的三種手段：  
1. 虛方法
2. 抽象類
3. 接口

## 虛方法步驟
使用多態-虛方法，父類方法加`virtual`，子類方法加`override`。  

1. 將父類的方法標記為虛方法，使用關鍵字`virtual`，這個函式可以被子類重新寫一遍(重寫)。
2. 子類的方法使用關鍵字`override`。  

## virtual、override 實現了什麼效果？
調的依然是父類的，只不過方法被子類重寫了  
裝誰的對象，就調誰的方法    

## 沒有使用多態-虛方法(virtual)
在還沒有使用多態虛方法(virtual)的時候，調用子類它們各自的方法，要通過轉換類型(轉成子類類型)，要寫一大坨程式碼

```c#
Chinese cn1 = new Chinese("張三");
Chinese cn2 = new Chinese("李四");
Janpenese j1 = new Janpenese("樹下子");
Janpenese j2 = new Janpenese("井邊子");
Korea k1 = new Korea("金秀習");
Korea k2 = new Korea("金元秀");
American a1 = new American("Ken");
American a2 = new American("Xiz");

//創建Person陣列的物件, 把8個子類物件賦給父類陣列
Person[] pers = { cn1, cn2, j1, j2, k1, k2, a1, a2 };

//遍歷每一元素
for (int i = 0; i < pers.Length; i++)
{
    //雖然裡面裝的是子類物件，但表現出來的只能是父類物件，因為它是父類類型
    //所以調用的方法，全是父類的方法
    //pers[i].SayHello();

    //如果想要看到子類打招呼的方式，就要轉成子類類型
    if (pers[i] is Chinese) //使用is轉換類型: 成功true 失敗false
    {
        ((Chinese)pers[i]).SayHello();
    } else if (pers[i] is Janpenese)
    {
        ((Janpenese)pers[i]).SayHello();
    } else if (pers[i] is Korea)
    {
        ((Korea)pers[i]).SayHello();
    } else
    {
        ((American)pers[i]).SayHello();
    }
}

//父類
public class Person
{
    //構造函式
    public Person(string name) {
        this.Name = name;
    }

    private string _name; //字段：用來保護屬性的
    public string Name  //屬性
    {
        get { return _name; }
        set { _name = value; }
    }

    public void SayHello() {
        Console.WriteLine($"我是人類，我叫{this.Name}");
    }
}

//子類
public class Chinese : Person //繼承Person
{
    //通過Chinese的構造函式 去調用父類的構造函式
    public Chinese(string name)
        : base(name) //base調用父類的構造函式，傳給父類一個name
    {
        //這裡面不用再寫程式碼了
        //因為有調用父類的構造函式
        //父類的構造函式裡已經有寫將參數賦值給屬性
    }

    //子類與父類的成員名相同，會造成父類成員的隱藏
    //直接體現的情況就是調不到
    public void SayHello() {
        Console.WriteLine($"我是中國人，我叫{this.Name}");
    }
}

public class Janpenese : Person
{
    public Janpenese(string name)
        : base(name)
    { }

    public void SayHello() {
        Console.WriteLine($"我是日本人，我叫{this.Name}");
    }
}

public class Korea : Person
{
    public Korea(string name)
        : base(name)
    { }

    public void SayHello() {
        Console.WriteLine($"我是韓國人，我叫{this.Name}");
    }
}

public class American : Person
{
    public American(string name)
        : base(name)
    { }

    public void SayHello() {
        Console.WriteLine($"我是美國人，我叫{this.Name}");
    }
}
```

## 使用多態-虛方法(virtual,override)

虛方法步驟：  
1. 將父類的方法標記為虛方法，使用關鍵字`virtual`，這個函式可以被子類重新寫一遍(重寫)。
2. 子類的方法使用關鍵字`override`。 

> 使用多態-虛方法，父類方法加`virtual`，子類方法加`override`。   


- 改成多態-虛方法，不用再判斷轉換類型(轉換成子類型)，少寫很多程式碼和增加擴展性。

- `virtual`, `override`實現了什麼效果？  
調的依然是父類的，只不過方法被子類重寫了   
簡單說：裝誰的對象(物件)，就調誰的方法   

```c#
Chinese cn1 = new Chinese("張三");
Chinese cn2 = new Chinese("李四");
Janpenese j1 = new Janpenese("樹下子");
Janpenese j2 = new Janpenese("井邊子");
Korea k1 = new Korea("金秀習");
Korea k2 = new Korea("金元秀");
American a1 = new American("Ken");
American a2 = new American("Xiz");

//創建Person陣列的物件, 把8個子類物件賦給父類陣列
Person[] pers = { cn1, cn2, j1, j2, k1, k2, a1, a2 };

//改成多態-虛方法，下面 mark 起來的程式碼全不用寫，
//只要一行pers[i].SayHello()，少寫很多程式碼和增加擴展性
for (int i = 0; i < pers.Length; i++)
{
    //雖然裡面裝的是子類物件，但表現出來的只能是父類物件，因為它是父類型態
    //所以調用的方法，全是父類的方法
    //pers[i].SayHello();

    //如果要想要看到不用子類打招呼的方式，就要轉成子類類型
    //if (pers[i] is Chinese)
    //{
    //    ((Chinese)pers[i]).SayHello();
    //} else if (pers[i] is Janpenese)
    //{
    //    ((Janpenese)pers[i]).SayHello();
    //} else if (pers[i] is Korea)
    //{
    //    ((Korea)pers[i]).SayHello();
    //} else
    //{
    //    ((American)pers[i]).SayHello();
    //}

    //virtual, override實現了什麼效果？ 
    //調的依然是父類的，只不過方法被子類重寫了   
    //裝誰的對象，就調誰的方法
    pers[i].SayHello();
}

//父類
public class Person
{
    //構造函式
    public Person(string name)
    {
        this.Name = name;
    }
    private string _name; //字段：用來保護屬性的
    public string Name  //屬性
    {
        get { return _name; }
        set { _name = value; }
    }

    //將父類的方法標記為虛方法，使用關鍵字`virtual`，這個函式可以被子類重新寫一遍(重寫)
    public virtual void SayHello() {
        Console.WriteLine($"我是人類，我叫{this.Name}");
    }
}

//子類
public class Chinese : Person //繼承Person
{
    //通過Chinese的構造函式 去調用父類的構造函式
    public Chinese(string name)
        : base(name) //base調用父類的構造函式，傳給父類一個name
    {
        //這裡面不用再寫程式碼了
        //因為有調用父類的構造函式
        //父類的構造函式裡已經有寫將參數賦值給屬性
    }

    //子類與父類的成員名相同，會造成父類成員的隱藏
    //直接體現的情況就是調不到
    //public void SayHello() { };

    //父類的方法標記為虛方法(virtual)，這個函式可以被子類重新寫一遍(重寫)
    //所以子類的方法加上要關鍵字`override`
    public override void SayHello() {
        Console.WriteLine($"我是中國人，我叫{this.Name}");
    }
}

public class Janpenese : Person
{
    public Janpenese(string name)
        : base(name)
    { }

    public override void SayHello() {
        Console.WriteLine($"我是日本人，我叫{this.Name}");
    }
}

public class Korea : Person
{
    public Korea(string name)
        : base(name)
    { }

    public override void SayHello() {
        Console.WriteLine($"我是韓國人，我叫{this.Name}");
    }
}

public class American : Person
{
    public American(string name)
        : base(name)
    { }

    public override void SayHello() {
        Console.WriteLine($"我是美國人，我叫{this.Name}");
    }
}
```

## 使用多態-虛方法，增加擴充性
如果我想要再多加一個子類，就只要在創建物件的地方加入它

```c#
//創建Person陣列的物件, 把8個子類物件賦給父類陣列
Person[] pers = { cn1, cn2, j1, j2, k1, k2, a1, a2, new English("格林"), new English("米蘭") };

//新加的子類
public class English : Person
{
    public English(string name)
        : base(name)
    {
    }
    public override void SayHello() {
        Console.WriteLine($"我是英國人，我叫{this.Name}");
    }
}
```

## 練習1：真的鴨子嘎嘎叫，木頭鴨子吱吱叫，橡皮鴨子唧唧叫
### 沒用虛方法的寫法
```c#
RealDuck real = new RealDuck();
real.Bark();
MuDuck mu = new MuDuck();
mu.Bark();
XPDuck xp = new XPDuck();
xp.Bark();
Console.ReadKey();

public class RealDuck {
    public void Bark() {
        Console.WriteLine("真的鴨子嘎嘎叫");
    }
}

public class MuDuck {
    public void Bark() {
        Console.WriteLine("木頭鴨子吱吱叫");
    }
}

public class XPDuck {
    public void Bark() {
        Console.WriteLine("橡皮鴨子唧唧叫");
    }
}
```

### 使用虛方法 virtual
思路：  
1. 把真鴨子做成父類，木頭鴨子、橡皮鴨子 承繼牠
2. 聲明真的鴨子陣列，裡面放子類鴨子
3. 真鴨子方法加`virtual`，木頭鴨子、橡皮鴨子加`override`  

使用虛方法`virtual`，可以重寫也可以不重寫，要重寫就加上`override`。  

```c#
//RealDuck real = new RealDuck();
//real.Bark();
//MuDuck mu = new MuDuck();
//mu.Bark();
//XPDuck xp = new XPDuck();
//xp.Bark();

RealDuck real = new RealDuck();
MuDuck mu = new MuDuck();
XPDuck xp = new XPDuck();

//聲明真的鴨子陣列，裡面放子類鴨子
RealDuck[] ducks = { real, mu, xp };

for (int i = 0; i < ducks.Length; i++) {
    ducks[i].Bark();
}
Console.ReadKey();

//把真鴨子做成父類
//木頭鴨子, 橡皮鴨子 承繼他
public class RealDuck {
    public virtual void Bark() {
        Console.WriteLine("真的鴨子嘎嘎叫");
    }
}

public class MuDuck : RealDuck {
    public override void Bark() {
        Console.WriteLine("木頭鴨子吱吱叫");
    }
}

public class XPDuck : RealDuck {
    public override void Bark() {
        Console.WriteLine("橡皮鴨子唧唧叫");
    }
}
```

## 練習2：經理11點打卡，員工9點打卡，程式員不打卡
1. 員工當父類，其他承繼它。
2. 使用多態-虛方法，父類方法加`virtual`，子類方法加`override`。
3. 宣告父類陣列，裡面放經理、員工、程式員物件。
4. 遍歷陣列每一個元素去調用打卡函式

```c#
//創建經理、員工、程式員物件
Employee emp = new Employee();
Manager mg = new Manager();
Programmer pm = new Programmer();

//創建父類陣列，裡面放經理、員工、程式員物件
Employee[] emps = { emp, mg, pm };

//遍歷陣列每一個元素去調用他們打卡的函式
for (int i = 0; i < emps.Length ; i++) {
    emps[i].ClockIn();
}

//員工當父類，其他承繼它
public class Employee {
    public virtual void ClockIn() {
        Console.WriteLine("9點打卡");
    }
}

public class Manager : Employee {
    public override void ClockIn() {
        Console.WriteLine("經理11點打卡");
    }
}

public class Programmer : Employee {
    public override void ClockIn() {
        Console.WriteLine("程式員不打卡");
    }
}
```



多型(Polymorphism) / 多態




[/object-oriented/polymorphism](https://learn.microsoft.com/zh-tw/dotnet/csharp/fundamentals/object-oriented/polymorphism)