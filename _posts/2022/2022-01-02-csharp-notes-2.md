---
layout: post
title: "C# Notes - Part Ⅱ"
date: 2022-01-02 00:00:10 +0800
categories: [Notes, C#]
tags: [C#]
---

## 異常處理(捕捉異常) try-catch-finally
[範例] 讓用戶輸入兩個數字，輸入非整數類型，處理該異常，並讓用戶重新輸入，輸出這個兩個數的和
```c#
int num1 = 0, num2 = 0;
Console.WriteLine("請輸入第一個數：");
while (true)
{ //使用死循環，讓用戶可以重新輸入
    try {
        num1 = Convert.ToInt32(Console.ReadLine());
        break; //輸入正確就跳出循環

    } catch {
        Console.WriteLine("您輸入的不是整數，請重新輸入");
    }
}
Console.WriteLine("請輸入第二個數：");
while (true)
{ //使用死循環，讓用戶可以重新輸入
    try {
        num2 = Convert.ToInt32(Console.ReadLine());
        break; //輸入正確就跳出循環

    } catch {
        Console.WriteLine("您輸入的不是整數，請重新輸入");
    }
}
int sum = num1 + num2;
Console.WriteLine(sum);
Console.ReadKey();
```

## 類的定義和聲明
範列1：定義一個敵人的類(Enemy)，可以設置血量、速度，有AI,Move方法
```c#
public class Enemy
{
    private float hp; //血量
    private float speed; //速度

    public float HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float Speed {
        get { return speed; }
        set { speed = value; }
    }

    public void AI() {
        Move();
        Console.WriteLine("這是Enemy的AI方法");
    }
    public void Move()
    {
        Console.WriteLine("這是Enemy的Move方法");
    }
}
```
聲明Enemy對象，用new實體化Enemy類別
```c#
Enemy enemy = new Enemy();
enemy.AI(); //調用AI方法
```
## 屬性的定義
```c#
private float speed; //速度
public float Speed {
    get { return speed; }
    set { speed = value; }
}
```
簡寫
```c#
public float Speed { get; set; }
```
賦值時，可以先進行一些邏輯判斷(設置前，可以先對該值進行校驗)
```c#
private int age;
public int Age 
{
    set {
        if (value >= 0) age = value; //設置前，可以先對該值進行校驗
    }
}
```

## 匿名型別var
var 由編譯器自動推斷型別

## this 和 base關鍵字
- this 可以存取"當前類別"所有的成員
- base 可以存取"父類別"公開的成員
> this可以訪問當前類別中定義的字段(成員變數)、屬性(get/set)和方法，有沒有this都可以訪問。base可以調用父類別中公有的字段和方法。

## 繼承
父類別
```c#
public class Person {...}
```
子類別：加上「:Person」繼承父類別
```c#
public class Teacher : Person {...}
public class Student : Person {...}
```
## 虛擬方法virtual/override
父類別：方法加上virtual
```c#
public class Enemy
{
    public virtual void Move() { //virtual虛方法，繼承後可以重寫override此方法
        Console.WriteLine("這是Enemy的Move方法");
    }
}
```

子類別：方法加上override
```c#
public class Type2Enemy: Enemy
{
    public override void Move() { //override重寫，原本的方法就不在了
        Console.WriteLine("這裡是Type2Enemy的移動方法");
    }
}
```
> 使用virtul可以選擇要不要重寫，不重寫就會以父類中的函數為主，重寫就會以子類的函數為主。

## 隱藏方法-new關鍵字
父類別
```c#
public class Enemy
{
    public void Move() {
        Console.WriteLine("這是Enemy的Move方法");
    }
}
```
子類別：方法加上new關鍵字，隱藏父類別的move方法
> new隱藏，只是把父類別方法隱藏，看不到了，實際這個方法還存在
```c#
public class Type2Enemy: Enemy
{
    public new void Move() { //new隱藏，只是把父類別方法隱藏，看不到了，實際這個方法還存在
        Console.WriteLine("這裡是Type2Enemy的移動方法");
    }
}
```
一般很少用new關鍵字隱藏方法，容易造成混亂
```c#
Type2Enemy enemy1 = new Type2Enemy();
enemy1.Move(); //隱藏方法: 聲明子類對象，會調用子類的move方法
Enemy enemy2 = new Type2Enemy();
enemy2.Move(); //隱藏方法: 聲明父類對象，會調用父類的move方法
```
## 抽象類別abstract
- 抽象類不能實體化
- 抽象類只有函數定義，沒有函數體(相當於不完整的函數)
- 它也是虛擬virtual的，需要被重寫的override
- 類是一個模版，抽象類就是一個不完整的模版

**為什麼要使用抽象類？**
例如：我們定義了鳥類，但不同的鳥有不同的飛行方式，那麼，我們可以透過抽象方法，讓繼承鳥類的所有子類都必須重寫overrid飛行方法。
> 通過抽象類別裡面的抽象方法，定義了一些子類別必須要實現的一些功能

定義鳥類(父類)
```c#
//當一個類當中存在抽象函數abstract的時候，這個類必須也聲明為抽象的
//一個抽象類 就是不完整的模版，不可以使用抽象類去實體化構造對象
public abstract class Bird
{
    public float speed;
    public void Eat() { }
    //每個鳥類飛行的方式不同，就可以定義為抽象類abstract，讓不同子類去重寫方法
    //所有繼承bird的類，都要去實現abstract抽象方法
    public abstract void Fly(); //方法中有抽象函數abstract，class就要改成抽象類abstract
}
```
定義鳥鴉-繼承鳥類(子類)
```c#
//我們繼承了一個抽象類的時候，必須去實現抽象方法(給出函數體)
public class Crow : Bird {
    public override void Fly() {
        Console.WriteLine("鳥鴉在飛行");
    }
}
```
聲明使用
```c#
Crow crow = new Crow();
crow.Fly();
```
我們可以透過抽象類(Bird)去聲明對象(Crow)，但是不可以去構造(因為它是不完整)
```c#
Bird bird = new Crow(); //我們可以透過抽象類(Bird)去聲明對象(Crow)，但是不可以去構造(因為它是不完整的方法)
bird.Fly();

//Bird bird = new Bird(); //Error: 抽象類不能實體化
```
> 一定要重寫方法，就可以聲明抽象類
> 使用virtul可以選擇要不要重寫，不重寫就會以父類中的函數為主，重寫就會以子類的函數為主

## 密封類和密封方法sealed (很少去用)
對於類，表示不能被繼承，對於方法，表示不能重寫該方法。

密封類
```c#
sealed class BaseClass { } //這裡聲明一個密封類
```
```c#
public class DerivedClass: BaseClass { } //sealed密封類無法被繼承
```
密封方法
```c#
public class BaseClass {
    public virtual void Move() { }
}
```
```c#
public class DerivedClass: BaseClass { //sealed密封類無法被繼承
    public sealed override void Move() { //我們可以把重寫的方法聲明為密封方法，表示該方法不能被重寫
        base.Move();
    }
}
```
**什麼時候使用 密封類和密封方法？**
- 防止重寫某些類導致代碼混亂
- 商業原因

## 派生類的構造方法
1. 在子類別中調用父類別默認的構造方法(無參數)
會先調用父類的，再調用子類的
```c#
public class BaseClass {
    public BaseClass() {
        Console.WriteLine("Base Class無參構造函式");
    }
}
```
在子類的構造方法加上 base(), 可以調用父類中無參的構造函式
在這裡base()可以不寫，因為默認會調用父類中的默認構造函式
```c#
public class DerivedClass : BaseClass {
    public DerivedClass(): base() { //調用父類中無參的構造函式(base()可不寫)
        Console.WriteLine("這是Derived Class無參的構造函式");
    }
}
```
調用的順序，會先調用父類的無參構造函式，再調用子類的
```c#
DerivedClass o1 = new DerivedClass();//會先調用父類的，再調用子類的
Console.ReadKey();
```
2. 在子類別中調用父類別有參數的構造方法(有參數)

```c#
public class BaseClass {
    private int x;
    public BaseClass(int x) {
        this.x = x;
        Console.WriteLine("x賦值完成");
    }
}
```
在子類別有參數的構造方法加上base(x)，相當於把x參數傳給父類別的構造方法
```c#
public class DerivedClass : BaseClass {
    private int y;
    public DerivedClass(int x, int y): base(x) { //base(x)，相當於把x參數傳給父類別的構造方法
        this.y = y;
        Console.WriteLine("y賦值完成");
    }
}
```
調用的順序:
不管是什麼情況，都是會先調用父類當中的構造方法，再調用子類的
```c#
DerivedClass o1 = new DerivedClass(1, 2); //不管是什麼情況，都是會先調用父類當中的構造方法，再調用子類的
```
## 修飾符protected(保護的)

protected保護的，當沒有繼承時，它的作用跟private是一樣的，當有繼承的時候，表示可以被子類訪問的字段或方法。

設為protected保護的，只有繼承時，才可以訪問到:
```c#
public class BaseClass {
    private int x;
    protected int z; //protected保護的，只有繼承時，才可以訪問到
    public BaseClass(int x) {
        this.x = x;
        Console.WriteLine("x賦值完成");
    }
}
```
父類z變數設為protected保護的, 子類繼承後，就可以訪問到(可以使用base.來訪問)
```c#
public class DerivedClass : BaseClass {
    private int y;
    public DerivedClass(int x, int y): base(x) { //base(x)，相當於把x參數傳給父類別的構造方法
        this.y = y;
        base.z = 100;//父類z變數設為protected保護的, 子類繼承後，就可以訪問到
        Console.WriteLine("y賦值完成");
    }
}
```

## 其他修飾符
new 用來隱藏繼承的方法
virtual 虛擬方法，父類的方法加上virtual，子類可以重寫該方法。
```c#
public virtual void Test() {...}
```
## 父類(基類)，繼承的類(子類/派生類)

abstract 抽象類別或抽象方法，抽象方法只有定義方法，沒有提供實作代碼(也就是沒有方法體)
該類有抽象方法，類別一定要加上abstract成為抽象類別
```c#
public abstract class MyClass {
    public abstract void Test();
} 
``` 

override 重寫方法，重寫繼承的虛擬方法(virtual)或抽象方法(abstract)
```c#
public override void Test() {...}
```
sealed密封，可以用在類/方法/屬性，對於類，不能繼承該類，sealed用於override方法(對於己重寫的方法)，繼承該類就不能再重寫該方法
```c#
class X {
    protected virtual void F() { Console.WriteLine("X.F"); }
    protected virtual void F2() { Console.WriteLine("X.F2"); }
}
//class Y重寫F方法，並加上sealed密封
class Y : X {
    sealed protected override void F() { Console.WriteLine("Y.F"); }
    protected override void F2() { Console.WriteLine("Y.F2"); }
}
//class Z繼承Y, 因為F()加上sealed，就不能再重寫該方法
class Z : Y {
    // Attempting to override F causes compiler error CS0239.
    // protected override void F() { Console.WriteLine("Z.F"); }

    // Overriding F2 is allowed.
    protected override void F2() { Console.WriteLine("Z.F2"); }
}
```
static 靜態方法。應用於所有成員(字段/屬性/方法)
- 靜態的(公有的)，可以用來修飾字段/屬性/方法，通過類去訪問。
- 靜態只有一份，是共用的

## 定義和實現接口(介面)
```c#
//定義接口
interface IFly {
    void Fly();
}
//實現接口
class Bird : IFly {
    public void Fly() { //do something }
}
```
接口可以彼此繼承
```c#
//定義接口兩個接口並彼此繼承
interface IA {
    void Method1();
}
interface IB: IA {
    void Method2();
}
//繼承IB並實現接口
class Bird : IB {
    public void Method1() { //do something }
    public void Method2() { //do something }
}
```
## 集合類-列表List的創建和使用
創建空的列表
```c#
List<int> scoreList = new List<int>(); //指定類型創建
var scoreList = new List<int>()//匿名方式創建
```
創建列表並給上初始值123
```c#
var scoreList = new List<int>() { 1,2,3 }; //創建列表並給上初始值123
```
列表List加入數據
```c#
var scoreList = new List<int>(); //創建空的列表
scoreList.Add(123); //列表加入數據
Console.WriteLine(scoreList[0]); //索引方式取值
Console.ReadKey();
```
## 列表List遍歷
```c#
var list = new List<int>() { 1, 3, 4, 7, 84 };
//for
for (int i = 0; i < list.Count; i++) {
    Console.Write($"{list[i]} ");
}
//foreach
foreach (var item in list) {
    Console.Write($"{item} ");
}
```
## 操作列表List的屬性和方法
```c#
List<int> scoreList = new List<int>();
scoreList.Add(100); //加入元素
scoreList.Add(200);
scoreList.Add(300);
scoreList.Insert(1, -99); //插入元素，指定索引位置插入元素
scoreList.RemoveAt(0);//移除指定索引置的元素
int index = scoreList.IndexOf(200); //尋找元素的索引值
int index = scoreList.IndexOf(400); //元素不存在會返回-1
Console.WriteLine(scoreList.IndexOf(100));//有相同的元素值，會先返回前面的索引值
Console.WriteLine(scoreList.LastIndexOf(100));//從後面開始搜索，有匹配就返回值，元素不存在一樣返回-1
scoreList.Sort(); //從小到大排序
```
## 泛型類別的定義 ClassA<T> 

定義一個泛型類別，就是定義一個類別，這個類中某些字段的類型是不確定的，這些類型可以在類別構造(實體化)的時候確定下來。
```c#
//加上尖括號<>，裡面加上T，T代表類型，這就是泛型
public class ClassA<T> //T代表一個數據類型，當使用ClassA進行構造的時候，需要制定T的類型
{
    private T x;
    private T y;
    public ClassA(T x, T y) {
        this.x = x;
        this.y = y;
    }
    public string GetSum() { //因為還沒有確定類型，先用string
        return x +""+ y;
    }
}
```
當我們實體化泛型類別的時候，需要制定泛型的類型
```c#
var o1 = new ClassA<int>(23,45); //當我們利用泛型構造的時候，需要制定泛型的類型
string s = o1.GetSum();
Console.WriteLine(s);
var o2 = new ClassA<string>("www", ".google");
Console.WriteLine(o2.GetSum());
```

泛型可以多個類型<T,A>，用class聲明的時候，這兩個類型都要指定
```c#
public class ClassA<T,A> //這個泛型有兩個類型:T跟A
{
    private T x; //用T去聲明變量x
    private T y; //用T去聲明變量y
    private A z; //用A去聲明變量z
}
```
用ClassA聲明變量，指定T為string類型，A為int類型
```c#
var o2 = new ClassA<string, int>("www", ".google"); //構造時，T代表string類型，A代表int類型
Console.WriteLine(o2.GetSum());
```
## 泛型方法 Method<T>(T a, Tb) {…}
定義泛型方法就是定義一個方法，這個方法的參數類型是不確定，這調用這個方法的時候，再去確定方法的參數類型。

舉例: 實現任意類型組成字串的方法:
```c#
//泛型方法
public static string GetSum<T>(T a, T b) {
    return a +""+ b;
}
```
```c#
//調用泛型方法(用不同類型)
Console.WriteLine(GetSum(1,8));
Console.WriteLine(GetSum(9.31, 6.668));
Console.WriteLine(GetSum("www",".google"));
```
只有一個類型，不指定也可以，它會自動推斷類型

泛型方法也可以有多個類型:
```c#
static string GetSum<T, T2, T3, T4>(T a, T b) { //這裡聲明4個類型，在調用方法的時候，也要指定4個類型
    return a + "" + b;
}
```
聲明調用方法時，要指定所有類型
```c#
Console.WriteLine(GetSum<int, int, int, int>(1, 8));
Console.WriteLine(GetSum<double, double, double, double>(9.31, 6.668));
Console.WriteLine(GetSum<string, string, string, string>("www", ".google"));
```

