---
layout: post
title: "[C# 筆記] 泛型約束"
date: 2010-03-04 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,泛型,泛型約束,T,where T,generic,generic constraint]
---

# 思考

如果想要實現下面泛型類別的程式碼，該怎麼做呢？      

```c#
class AGenericClass<T>
{
    public void Say(T t) {
        t.Hello();
    }
}
```
`t.Hello()`意味著我傳進來的一定是一個`class`，而這個`class`一定要有`Hello()`這個方法。      

那麼，該用什麼要規範？用`interface`，讓它實現`IHello`介面。

```c#
interface IHello
{
    void Hello();
}
```

## 泛型類加上約束 where T
特化`T`的類別，只要實現`IHello`介面就行。       

那泛型類要怎麼加上約束呢？換句話說，我們要怎麼去描述這個`T`，能夠讓它表示一個必須繼承了`IHello`這個介面類別呢？     

加上`where T: IHello`(這段就是泛型的約束)，`T`這樣一個類型必須要繼承自`IHello`這樣的介面。

```c#
//where T: IHello 這段就是泛型的約束
class AGenericClass<T> where T: IHello
{
    public void Say(T t) {
        //在外界傳進來的這個替代類型T，一定是繼承自IHello這樣的方法的
        t.Hello();
    }
}

interface IHello
{
    void Hello();
}
```

# 泛型約束
- 泛型約束：對泛型中傳入的類型進行「校驗」，規定其必須**滿足的條件**

## 格式

在泛型類`<T>`後面加上`where T: 條件`

```c#
public class AGenericClass<T> where T: 條件
{......}
```

## 舉例(用class來表示引用類型)
`where T: class`後面加的是`class`，`class`代表`T`它必須是「引用類型」

```c#
//指T必須是引用類型
public class AGenericClass<T> where T: class
{......}
```

### 正確用法
`string` 是引用類型，所以它可以用來去特化當前泛型類。

```c#
//正確用法，因為string是引用類型
AGenericClass<string> g = new AGenericClass<string>();
```

### 錯誤用法
因為`int`不是引用類型，`int`是值類型`value type`。

```c#
//錯誤用法，因為int不是引用類型，int是值類型value type
AGenericClass<int> g = new AGenericClass<int>();//報錯
```

# 泛型約束分類

剛提到了兩個：  
1. 第一個是說咱們的`T`必須繼承於某個介面`interface`。 `where T: IHello`
2. 第二個是說咱們的`T`必須是一個「引用類型」。`where T: class`       

現在來一一列舉：

```
泛型約束分類：
1. class: 泛型T必須是引用類型
2. struct: 泛型T必須是值類型
3. new():泛型T必須是包含一個無參構造方法的class，如果同時有其他約束，new()必須放在最後
4. 類名(Person)：泛型T要馬就是Person類，或是Person的子類/孫子類
5. 介面約束：泛型T必須實現一個或多個介面
6. 多佔位符約束: 使用where關鍵字對多個佔位符進行各自約束
```

- 引用類型約束 `class`：泛型必須是引用類型

```c#
public class AGenericClass<T> where T: class {...}
```

- 值類型約束 `struct`：泛型必須是值類型

```c#
public class AGenericClass<T> where T: struct {...}
```

- `new`約束：泛型必須包含無參構造方法   
如與其他約束共用，則放在最後

```c#
public class AGenericClass1<T> where T: new() {...}
public class AGenericClass2<T> where T: class, new() {...}
```

- 類名約束：泛型必須是**某類**或派生自**某類**
與介面共用，則放在介面前方

```c#
//T必須是Person，或是Person的子類
public class AGenericClass<T> where T: Person {...}
public class AGenericClass<T> where T: Person, IHello {...}
```

- 介面約束：泛型必須實現一個或多個介面

```c#
public class AGenericClass<T> where T: IOne, ITwo, IThree {...}
```

- 多泛型約束
泛型類可以有兩個佔位符，我們也可以對這兩個佔位符，各自做出各自的約束

```c#
class AGenericClass<T, B> where T : IOne where B : class { }
```

---

## 1. class：泛型T必須是引用類型
- 引用類型約束 `class`：泛型必須是引用類型

用`class`來表示引用類型
```c#
//1. class: 引用類型
class AGenericClassForClass<T> where T : class {  }

static void Main(string[] args)
{
    //1.class表示引用類型約束，int是值類型，所以不行
    //AGenericClassForClass<int> g = new AGenericClassForClass<int>();
    AGenericClassForClass<string> g = new AGenericClassForClass<string>();
}
```

## 2. struct：泛型T必須是值類型
值類型約束`struct`：泛型必須是值類型

```c#
//2. struct: 值類型
class AGenericClassForValueType<T> where T : struct { }

static void Main(string[] args)
{
    //2.struct表示值類型約束，string是引用類型，所以不行
    //AGenericClassForValueType<string> g = new AGenericClassForValueType<string>();
    AGenericClassForValueType<float> g = new AGenericClassForValueType<float>();
}
```

## 3. new()：當前的T，必須包含無參構造方法
`new()`：泛型`T`必須是包含一個無參構造方法的`class`，如果同時有其他約束，`new()`必須放在最後

```c#
//3. new(): 當前的T，必須包含無參構造方法
class TestForNew { }
class AGenericClassForNew<T> where T : new() { }

static void Main(string[] args)
{
    //3.new()表示T必須包含無參構造方法
    AGenericClassForNew<TestForNew> g3 = new AGenericClassForNew<TestForNew>();
}
```

`class TestForNew { }`系統會自動帶一個無參的構造方法，如果手動加一個有參的構造方法，那系統就不會為這個一個類去生成一個無參的構造方法，就會報錯。

```c#
//如果手動加一個有參的構造方法，那系統就不會為這個一個類去生成一個無參的構造方法，就會報錯
class TestForNew {
    public TestForNew(int x) { }
}
class AGenericClassForNew<T> where T : new() { }

static void Main(string[] args)
{
    //3.new()表示T必須包含無參構造方法
    AGenericClassForNew<TestForNew> g3 = new AGenericClassForNew<TestForNew>();
}
```

那怎麼辦呢？再手動加上一個無參的構造方法就可以了。

```c#
class TestForNew {
    public TestForNew() { } //無參構造方法
    public TestForNew(int x) { } //有參構造方法
}
class AGenericClassForNew<T> where T : new() { }

static void Main(string[] args)
{
    //3.new()表示T必須包含無參構造方法
    AGenericClassForNew<TestForNew> g3 = new AGenericClassForNew<TestForNew>();
}
```

如果同時有其他約束，`new()`必須放在最後

```c#
class AGenericClassForNew<T> where T : class, new() 
{ }
```

## 4. 類名(Person)：泛型T要馬就是Person類，或是Person的子類/孫子類
如果與介面同時存在，則放在介面的前面

```c#
//4. 類名: T必須是本類，或是其子類/孫子類...
class Person { } //父類
class Teacher : Person { } //子類
class EnglishTeacher : Teacher { } //孫子類
class AGenericClassForClassName<T> where T : Person { }

static void Main(string[] args)
{
    //4.類名約束:T必須是當前類，或是子類/孫子類
    AGenericClassForClassName<Person> g41 = new AGenericClassForClassName<Person>();
    AGenericClassForClassName<Teacher> g42 = new AGenericClassForClassName<Teacher>();
    AGenericClassForClassName<EnglishTeacher> g43 = new AGenericClassForClassName<EnglishTeacher>();
}
```

## 5. 介面約束: T必須實現一個或者多個介面

```c#
//5. 介面約束: T必須實現一個或者多個介面
interface IFireble {
    void fire();
}
interface IRunnable {
    void Run();
}
class Tank : IFireble, IRunnable
{
    public void fire() { }
    public void Run() { }
}
class AGenericClassForInterface<T> where T : IFireble,IRunnable { }

static void Main(string[] args)
{
    //5.介面約束:T必須實現一個或多個介面
    AGenericClassForInterface<Tank> g5 = new AGenericClassForInterface<Tank>();
}
```

### 類名約束+介面約束
再結合第四點： 類名約束(如果與介面同時存在，則放在介面的前面)

```c#
interface IFireble {
    void fire();
}
interface IRunnable {
    void Run();
}
class Machine { }
class Tank : Machine, IFireble, IRunnable
{
    public void fire() { }
    public void Run() { }
}

//類名約束:如果與介面同時存在，則放在介面的前面
class AGenericClassForInterface<T> where T : Machine, IFireble, IRunnable { }

static void Main(string[] args)
{
    //5.介面約束:T必須實現一個或多個介面
    AGenericClassForInterface<Tank> g5 = new AGenericClassForInterface<Tank>();
}
```
`Tank class`符合三個條件:
1. 它是 `Machine`的子類
2. 它繼承了 `IFireble`介面
3. 它繼承了兩個介面


## 6. 多佔位符約束: 使用where關鍵字對多個佔位符進行各自約束

```c#
//6. 多類型佔位符約束
//T:必須是一個引用類型(class)、必須有一個無參構造方法(new())
//B:必須實現IFireble介面
class AGenericClassForMulti<T, B> where T : class, new() where B : IFireble, IRunnable { }

static void Main(string[] args)
{
    //6.多佔位符約束
    AGenericClassForMulti<TestForNew, Tank> g6 = new AGenericClassForMulti<TestForNew, Tank>();
}
```


# 完整程式碼

```c#
/* 泛型約束分類
 *  1. class: 泛型T必須是引用類型
 *  2. struct: 泛型T必須是值類型
 *  3. new():泛型T必須是包含一個無參構造方法的class，如果同時有其他約束，new()必須放在最後
 *  4. 類名(Person)：泛型T要馬就是Person類，或是Person的子類/孫子類
 *  5. 介面約束：泛型T必須實現一個或多個介面
 *  6. 多佔位符約束: 使用where關鍵字對多個佔位符進行各自約束
 */
namespace HelloWorld
{

    //1. class: 引用類型
    class AGenericClassForClass<T> where T : class {  }

    //2. struct: 值類型
    class AGenericClassForValueType<T> where T : struct { }

    //3. new(): 當前的T，必須包含無參構造方法
    class TestForNew {
        public TestForNew() { } //無參構造方法
        public TestForNew(int x) { } //有參構造方法
    }
    class AGenericClassForNew<T> where T : class, new() { }

    //4. 類名: T必須是本類，或是其子類/孫子類...
    class Person { } //父類
    class Teacher : Person { } //子類
    class EnglishTeacher : Teacher { } //孫子類
    class AGenericClassForClassName<T> where T : Person { }

    //5. 介面約束: T必須實現一個或者多個介面
    interface IFireble {
        void fire();
    }
    interface IRunnable {
        void Run();
    }
    class Machine { }
    class Tank : Machine, IFireble, IRunnable
    {
        public void fire() { }
        public void Run() { }
    }
    class AGenericClassForInterface<T> where T : Machine, IFireble, IRunnable { }

    //6. 多類型佔位符約束
    //T:必須是一個引用類型(class)、必須有一個無參構造方法(new())
    //B:必須實現IFireble介面
    class AGenericClassForMulti<T, B> where T : class, new() where B : IFireble, IRunnable { }


    internal class Program
    {
        static void Main(string[] args)
        {
            //1.class表示引用類型約束，int是值類型，所以不行
            //AGenericClassForClass<int> g = new AGenericClassForClass<int>();
            AGenericClassForClass<string> g1 = new AGenericClassForClass<string>();

            //2.struct表示值類型約束，string是引用類型，所以不行
            //AGenericClassForValueType<string> g = new AGenericClassForValueType<string>();
            AGenericClassForValueType<float> g2 = new AGenericClassForValueType<float>();

            //3.new()表示T必須包含無參構造方法
            AGenericClassForNew<TestForNew> g3 = new AGenericClassForNew<TestForNew>();

            //4.類名約束:T必須是當前類，或是子類/孫子類
            AGenericClassForClassName<Person> g41 = new AGenericClassForClassName<Person>();
            AGenericClassForClassName<Teacher> g42 = new AGenericClassForClassName<Teacher>();
            AGenericClassForClassName<EnglishTeacher> g43 = new AGenericClassForClassName<EnglishTeacher>();

            //5.介面約束:T必須實現一個或多個介面
            AGenericClassForInterface<Tank> g5 = new AGenericClassForInterface<Tank>();

            //6.多佔位符約束
            AGenericClassForMulti<TestForNew, Tank> g6 = new AGenericClassForMulti<TestForNew, Tank>();
        }
    }
}
```

# 泛型約束與繼承
- 挪有泛型約束的類作為父類，則其子類可以選擇**特化父類**，或者 使用與父類**同樣/更嚴格**的約束

例如我們有一個父類`MyClass`、一個子類`MyClassSon`繼承`MyClass`
```c#
class MyClass {...} //父類
class MyClassSon: MyClass {...} //子類
```

現在有一個泛型類`AGenericClass`，它的`T`類型必須要滿足是一個`MyClass`這樣的類別, 
```c#
class AGenericClass<T> where T: MyClass {...}
```
## 特化父類
### 第一種特化方式

使用`MyClass`來特化父類
```c#
public class Test: AGenericClass<MyClass> {...}
```

### 第二種特化方式

使用`MyClassSon`來特化父類
```c#
public class Test: AGenericClass<MyClassSon> {...}
```

## 與父類相同約束

```c#
public class Test: AGenericClass<B> where B: MyClass
{...}
```
## 更嚴格約束

```c#
public class Test: AGenericClass<B> where B: MyClassSon
{...}
```

[https://www.bilibili.com/video/BV1kP41197YV/](https://www.bilibili.com/video/BV1kP41197YV/)