---
layout: post
title: "[C# 筆記] Delegate(委託)、Lambda、Event(事件)"
date: 2022-06-30 00:00:10 +0800
categories: [Notes, C#]
tags: [C#, Delegate, Lambda, Event]
---

## 委託的定義和使用 1
什麼是委託？
如果我們要把方法當參數來傳遞的話，就要用到委託，簡單來說，委託是一個類型，這個類型可以賦值一個方法的引用。
- 一般我們變數都是儲存數據，像是string, int, float, double...
- 委託變數可以把方法複製過來，通過變數去調用這個方法

### 聲明委託
在C#中使用一個"類別"有兩個階段：定義、聲明，「定義」就是告訴編譯器這個"類別"由哪些字段和方法組成的，「聲明」就是使用這個類別，實體化對象。

在我們使用委託時，也需要經過這兩個階段：
1. 定義委託：告訴編譯器，我們這個委託可以指向哪些方法
(這個類型的委託，可以複製什麼樣的方法過來，在定義委託的時候決定的)。
```c#
delegate void IntMethodInvoker(int x);
```
- 定義一個委託叫做IntMethodInvoker，這個委託可以向什麼類型的方法呢？
- 這個方法要帶有一個int類型的參數，並且方法沒有回傳值(void)的。 
- 定義一個委託，要定義方法的參數和回傳值，使用關鍵字delegate。

定義其他委託的案例:
```c#
delegate double TwoLongOp(double x, double y);
delegate string GetString();
```
2. 聲明委託：然後創建該委託的實例，使用這個委託去創建變量(聲明委託)。

### 範例:
```c#
//定義一個委託類型，這個委託類型的名字叫做GetAString。
private delegate string GetString();
static void Main()
{
    int x = 20;
    //使用委託 創建實例
    GetString a = new GetString(x.ToString);//a指向了x的ToString方法
    string s = a();//通過委託實例 去調用x中的tostring 方法
    Console.WriteLine(s);//通過委託類型是調用一個方法，跟直接調用這個方法 作用是一樣的
}
```
> 另一種方式：
> GetString a = x.ToString;//初始化
> string s = a.Invoke(); //通過invoke()方法調用a所引用的方法

## 委託的定義和使用2- 把委託當作參數來使用
### 1. 定義委託、將委託當作參數來傳遞
```c#
private delegate void PrintString(); //定義委託
static void PrintStr(PrintString print) //委託當作參數來傳遞
{
    print(); //調用Method1
}

static void Method1() {
    Console.WriteLine("Method1");
}        
static void Method2() {
    Console.WriteLine("Method2");
}
```
### 2. 聲明委託
```c#
//使用委託類型作為方法的參數
PrintString method = Method1;
PrintStr(method);//調用Method1方法
method = Method2;
PrintStr(method);//調用Method2方法
```
委託可以使用靜態方法，也可以引用一個類別裡的普通方法

## Action委託(void)和Func委託
 Action 委託引用一個void的方法，T表示方法參數
```text
Action
Action<in T>
Action<in T1, in T2>
Action<in T1, in T2...T16>
```
Func 委託引用了一個帶有返回值的方法,前面是方法傳入的參數，最後一個是返回值類型**
```
Func<out TResult>
Func<in T, out TResult>
Func<in T1, in T2...T16, out TResult>
```
Action和Func可以傳遞0~16個參數
### Action委託 (void)
- Action是沒有返回值的
- Action是系統內置(預定義)的一個委託類型，它可以指向一個沒有返回值、沒有參數的方法。
- Action聲明泛型去指定個方法的參數類型(後面跟上一個泛型，這個泛型的類型就是參數類型)
- 方法的參數類型是根據委託類型去決定的。

Action指向沒有參數的方法：
```c#
static void PrintString() {
    Console.WriteLine("hello world");
}
Action a = PrintString; //Action是系統內置(預定義)的一個委託類型，它可以指向一個沒有返回值、沒有參數的方法
```
Action後面跟上一個泛型(這個泛型的類型就是參數類型)，可以指向一個沒有返回值，有一個參數的方法。

Action指向帶有一個參數的方法：
```c#
static void PrintInt(int i) { 
   Console.WriteLine(i);
}
Action<int> a = PrintInt; //Action後面跟上一個泛型，可以指向一個沒有返回值，有一個參數的方法
```
Action指向帶有二個參數的方法：
```c#
static void PrintDoubleInt(int x, int y) {
    Console.WriteLine(x+y);
}
Action<int, int> a = PrintDoubleInt;
a(3, 4);
```

> Action最多可以16個類型，類型的順序要對應上方法參數
> Action<int, int, bool, string, double, string> a;

### Func委託
- Func 後面必須要指定一個返回值
- Func 後面的泛型是什麼類型，它的返回值就是什麼類型
- Func 後面可以有很多類型，最後一個類型是方法的返回值類型，前面的類型是方法的參數類型，
- Func 參數類型必須跟指向方法的參數類型按照順序對應。
- Func 後面的參數可以有很多個，後面必須指定一個返回值類型，最後一個是返回值類型，前面的參數可以有0~16個，先寫參數類型，最後一個是返回值類型。

Func<int> 帶有int返回值的方法
```c#
static int Test1() {
    return 1;
}
Func<int> a = Test1;
int res = a();
Console.WriteLine(res); //1
```

Func<string ,int> 指向的方法是1個string參數, 返回值為int
```c#
static int Test2(string s) {
    Console.WriteLine(s);
    return 100;
}
Func<string, int> a = Test2;
int res = a("aa");
Console.WriteLine(a(res)); //aa,100
```

Func<int,int,int> 指向的方法是有2個int參數, 返回值為int
```c#
static int Test3(int x, int y) {
    return x + y;
}
Func<int, int, int> a = Test3;
int res = a(1, 2); //3
Console.WriteLine(res);
```
> Func 後面的參數可以有很多個, 後面必須指定一個返回值類型，最後一個是返回值類型，前面的參數可以有0~16個，先寫參數類型，最後一個是返回值類型。

## int 類型的冒泡排序

```c#
static void Sort(int[] sortArray)
{
    bool swapped = true;
    do {
        swapped = false; //終止循環的條件
        for (int i = 0; i < sortArray.Length - 1; i++) {
            if (sortArray[i] > sortArray[i + 1]) {
                //交換
                int temp = sortArray[i];
                sortArray[i] = sortArray[i + 1];
                sortArray[i + 1] = temp;
                swapped = true; //只要有發生交換就設true，就代表無序，就再一次執行循環
            }
        }
    } while (swapped); //有交換就繼續循環
}

int[] sortArray = new int[] { 2343, 546, 6879, 232, 1, 454,2 };
Sort(sortArray);
foreach (var item in sortArray) {
    Console.Write($"{item} ");
}
Console.ReadKey();
```
## 擴展通用的冒泡排序方法
泛型+委託-可以傳入任何類型的泛型通用方法，對我們原本的排序方法做擴展

### 範例: 使用泛型+委託，可以對員工薪水做排序
定義 Employee Class
```c#
public class Employee
{
    public string Name { get; private set; }
    public int Salary { get; private set; }
    public Employee(string name, int salary) {
        this.Name = name;
        this.Salary = salary;
    }
    //比較薪水，如果e1薪水>e2薪水, 回傳true,否則回傳false
    public static bool Compare(Employee e1, Employee e2) {
        if (e1.Salary > e2.Salary) return true;
        return false;
    }
    //重寫ToString方法，不重寫它預設是base.ToString();，輸出的是父類+類別名
    public override string ToString() {
        return $"{Name}:{Salary}";
    }
}
```
原本的冒泡排序方法
```c#
static void Sort(int[] sortArray) {
    bool swapped = true;
    do {
        swapped = false; //終止循環的條件
        for (int i = 0; i < sortArray.Length - 1; i++) {
            if (sortArray[i] > sortArray[i + 1]) {
                //交換
                int temp = sortArray[i];
                sortArray[i] = sortArray[i + 1];
                sortArray[i + 1] = temp;
                swapped = true; //只要有發生交換就設true，就代表無序，就再一次執行循環
            }
        }
    } while (swapped); //有交換就繼續循環
}
int[] sortArray = new int[] { 2343, 546, 6879, 232, 1, 454, 2 };
Sort(sortArray);
foreach (var item in sortArray) {
    Console.Write($"{item} ");
}
```
改寫為：泛型+委託-可以傳入任何類型的泛型通用方法，對我們原本的排序方法做擴展:
```c#
//泛型+委託-可以傳入任何類型的泛型通用方法，對我們原本的排序方法做擴展
static void CommonSort<T>(T[] sortArray, Func<T,T,bool> compareMethod) {
    bool swapped = true;
    do
    {
        swapped = false; //終止循環的條件
        for (int i = 0; i < sortArray.Length - 1; i++)
        {
            if (compareMethod(sortArray[i], sortArray[i + 1]))
            {
                //交換
                T temp = sortArray[i];
                sortArray[i] = sortArray[i + 1];
                sortArray[i + 1] = temp;
                swapped = true; //只要有發生交換就設true，就代表無序，就再一次執行循環
            }
        }
    } while (swapped);
}
```
聲明調用方法(泛型+委託)
```c#
Employee[] employees = new Employee[] { //聲明employee類，並初始化幾筆員工資料
    new Employee("aa", 183),
    new Employee("bb", 22),
    new Employee("cc", 3),
    new Employee("ee", 453),
    new Employee("ff", 99),
    new Employee("hh", 345),
};
CommonSort<Employee>(employees, Employee.Compare); //對員工薪水做排序
foreach (Employee em in employees) { //輸出
    Console.WriteLine(em);
}
Console.ReadKey();
```
## 多播委託
簡單說，就是利用委託引用多法。
委託可以引用一個方法，也可以引用多個方法，就叫做多播委託(就像是一個廣播性質的委託)。
多播委託可以按照順序調用多個方法，多播委託只能得到調用最後一個方法的結果。

```c#
static void Test1() {
    Console.WriteLine("Test1");
}
static void Test2() {
    Console.WriteLine("Test2");
}
//多播委託
Action a = Test1;
a += Test2; //表示添加一個委託的引用
a(); //先輸出:Test1再輸出Test2
```
```c#
static void Test1() {
    Console.WriteLine("Test1");
}
static void Test2() {
    Console.WriteLine("Test2");
}
Action a = Test1;
a += Test2; //表示添加一個委託的引用(有2個方法), 先輸出:Test1再輸出Test2
a -= Test1; //去掉Test1方法, 輸出:Test2
a -= Test2; //沒有指向方法(調用的話會拋出異常null)
//當一個委託沒有指向位何方法時，調用的話會拋出異常null
if(a!=null) a();
```
如果在調用過程中，在其中一個方法發生異常，下面的方法就不會再調用了
```c#
static void Test1() {
    Console.WriteLine("Test1");
    throw new Exception();//拋出異常
}
static void Test2() {
    Console.WriteLine("Test2");
}
Action a = Test1;
a += Test2; //在Test1發生異常，Test2就不會再調用了
//當一個委託沒有指向位何方法時，調用的話會拋出異常null
if(a!=null) a();
```
## 取得多播委託中所有方法的委託
```c#
static void Test1() {
    Console.WriteLine("Test1");
}
static void Test2() {
    Console.WriteLine("Test2");
}
Action a = Test1;
a += Test2;
//得到所有的委託
Delegate[] delegates = a.GetInvocationList();
foreach (Delegate de in delegates) {
    de.DynamicInvoke();
}
```
> 我們可以取得所有的委託，再進行單獨調用

## 匿名方法
匿名方法就是沒有名字的方法。匿名跟委託也有點關係…
    
正常使用委託的方式:
```c#
static int Test(int arg1, int arg2) {
    return arg1 + arg2;
}
//委託
Func<int, int, int> Plus = Test;
```
修改成匿名方法的型式
```c#
Func<int, int, int> Plus = delegate (int arg1, int arg2) {
    return arg1 + arg2;
};
```
> - 這段就是匿名方法 delegate (int arg1, int arg2) { return arg1 + arg2; };  
> - 匿名方法，本質上它還是一個方法，只是沒有名字。    
> - 使用委託變量的地方，都可以使用匿名方法賦值。  
    
什麼時候會需要用到？
當別的地方不需要調用這個方法時，就用匿名方法，把它聲明一個委託類型，可以減少代碼量、減少代碼複雜性。

## Lambda表達式
Lambda表達式相當於是匿名方法的簡寫，就是拿來代替匿名方法的。
所以一個Lambda表達式也是定義一個方法。
Lambda表達式的參數，是不需要聲明類型的

### Lambda表達式的基礎使用:
- 拿掉delegate關鍵字，
- 小括號中只寫參數名(因為委託類型已經定義參數類型了)
- 後面接著=>再加大括號{}
- 在大括號{}中寫方法體

```c#
Func<int, int, int> plus = (arg1, arg2) => {  //Lambda表達式的參數，是不需要聲明類型的
    return arg1 + arg2;
};
Console.WriteLine(plus(10,40));
```
Lambda表達式只有一個參數的簡寫方式：
- 當Lambda表達式只有一個參數時，可以不用加上小括號。
- 當函數體的語句只有一句話的時候，我們可以不加大括號，也可以不加上return語句。

```c#
Func<int, int> test2 = a => 12;
Func<int, int> test3 = a => a + 1;//參數只有一個的時候，不用括號，可以直接給參數,當我們方法體，只有一句話時，不用大括號，也可以不加上return
Func<int, int> test4 = (a) => { //上面的原始寫法
    return a + 1; 
};
```
Lambda表達式可以訪問Lambda表達式區塊外的變量。
但如果不能正確使用，也會很危險，會造成結果的不可控，容易出現編程問題，用的時候要謹慎。

## 事件Event的聲明
正常委託使用的方式:
```c#
class Program {
    public delegate void MyDelegate();//定義一個委託
    public MyDelegate myDelegate;//聲明一個委託類型的變量，作為類的成員(利用這個委託去聲明一個變量)

    static void Main() {
        Program p = new Program();
        p.myDelegate = Test1; //委託指向Test1方法
        p.myDelegate(); //調用委託，它會執行Test1方法
        Console.ReadKey();
    }
    static void Test1() {
        Console.WriteLine("Test1");
    }
}
```

Event的用法：
- 聲明一個事件的變量，它只能是類別的成員。
    (其實就是在委託前面加上event, 聲明為一個事件，event的本質上還是一個委託)
- Event的用法跟委託一樣，只要在委託類型的變量前面加上event關鍵字。
- Event跟委託的差別在於，event只能是類別的成員，而委託可以是類的成員，也可以是局部變量(方法裡的變量)。

```c#
class Program {
    public delegate void MyDelegate();//定義一個委託
    //public MyDelegate myDelegate;//聲明一個委託類型的變量，作為類的成員(利用這個委託去聲明一個變量)
    public event MyDelegate myDelegate;//聲明一個事件的變量，它只能是類別的成員(其實就是在委託前面加上event, 聲明為一個事件，event的本質上還是一個委託)

    static void Main() {
        Program p = new Program();
        p.myDelegate = Test1; //委託指向Test1方法
        p.myDelegate(); //調用委託方法
        Console.ReadKey();
    }
    static void Test1() {
        Console.WriteLine("Test1");
    }
}
```