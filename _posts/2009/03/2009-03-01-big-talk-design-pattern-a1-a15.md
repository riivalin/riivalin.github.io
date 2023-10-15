---
layout: post
title: "[閱讀筆記][Design Pattern] 物件導向基礎"
date: 2009-03-01 05:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 1. 類別與實體
## 什麼是物件？

- 一切事物皆為物件
- 所有的東西都是物件
- 物件就是：可以看到、感覺到、聽到、觸摸到、嚐到、或聞到的東西
- 物件是一個獨立自主的實體，用一組可識別的特性和行為來標示

## 什麼是類別？
- 類別就是：具有相同之屬性和功能的物件的抽象集合

```c#
class Cat {
    public string Shout() {
        return "喵";
    }
}
```

- `class`定義類別的關鍵字
- `Cat`類別的名稱
- `Shout`類別的方法

> 1. 類別名稱首字母一定要大寫
> 2. 多個單字則各個字首字母大寫
> 3. 對外公開的方法需要用`public`修飾子

## 如何應用類別？
只要將類別實體化一下就可以了。

## 什麼叫做實體化？
### 什麼叫實體？
實體，就是一個真實的物件。比如我們都是「人」，而你和我其實就是「人」類別的實體。

### 什麼叫做實體化？
而實體化就是建立物件的過程，使用`new`關鍵字來建立。

```c#
Cat cat = new Cat(); //將Cat類別實體化

class Cat {
    public string Shout() {
        return "喵";
    }
}
```

### `Cat cat = new Cat();` 其實做了兩件事

```c#
Cat cat; //1.宣告一個cat物件，物件名為cat
cat = new Cat(); //2.將cat物件實體化
```

`Cat`實體化後，等同於出生了一隻小貓`cat`，此時就可以讓小貓 `cat.Shout()`。      
在任何需要小貓叫的地方都可以實體化它`Cat`去調用`Shout()`小貓叫。


# 2. 建構式

我們希望小貓一出生就有姓名，那麼就應該寫一個有參數的建構式。        

## 什麼是建構式？

建構式，又叫做建構函式，其實就是對類別進行初始化。  
建構式與類別同名，無返回值，也不需要`void`，在`new`的時候調用。     

```c#
//在這段程式碼中，Cat()就是建構式
Cat cat = new Cat();
```
> 在類別建立時，就是調用建構函式的時候。        

所有的類別都有建構式，如果你沒有做任何定義，系統會自動產生空的建構式，若你有定義的建構式，預設的建構式就會失效了。      

也就是說，由於你沒有在`Cat`類別中定義過建構式，所以C#會自動生成一個空的建構函式`public Cat() { }`，而這個空的方法什麼也不做，只是為了讓你順利地實體化而己。      

## 建構式做什麼用的？

如果我們希望小貓一出生就有姓名，那麼就應該寫一個有參數的建構函式，這樣一來，我們在生成小貓時，就可以給小貓取名字了。

```c#
Cat cat = new Cat("Ri");
Console.WriteLine(cat.Shout()); //輸出:我的名字叫 Ri，喵!

class Cat {
    private string name; //宣告Cat類別的私有變數name
    //定義有參數的建構函式
    public Cat(string name) {
        this.name = name; //將參數賦值給私有變數name
    }

    public string Shout() {
        return $"我的名字叫 {name}，喵!";
    }
}
```


# 3. 方法重載 Overload

方法重載提供了建立同名的多個方法的能力，方法名相同，但參數類型或個數不同。

> 方法重載算是提供了函數可擴展的能力。      


如果不需要取名字，也可以出生小貓，可以用「方法重載」，新增一個同名無參數的建構函式。

```c#
class Cat {
    private string name; //宣告Cat類別的私有變數name

    //定義有參數的建構函式
    public Cat(string name) {
        this.name = name; //將參數賦值給私有變數name
    }
    //將建構函式重載
    public Cat() {
    }

    public string Shout() {
        return $"我的名字叫 {name}，喵!";
    }
}
```


# 4. 屬性與修飾符

「屬性是一個方法或一對方法(get/set)，但在調用它的程式碼看來，它是一個欄位，即屬性適合於以欄位的方式使用方法調用的場合。」
    
> 欄位的意思：欄位是與類別相關的變數，是儲存類別要滿足其設計所需要的資料。  
> 比如：`private string name = "";`， `name`就是一個欄位，它通常是私有的類別變數。    

那麼屬性是什麼樣呢？現在來增加一個「貓叫次數 `ShoutNum`」的屬性：   

```c#
private int shoutNum = 3; //宣告一個內部欄位，注意是private，預設叫聲次數為3
public int ShoutNum { //ShoutNum屬性，注意是public，有兩個方法：get/set
    get { return shoutNum; } //get表示：外界調用時可以得到shoutNum的值
    set { shoutNum = value; } //set表示：外界可以給內部shoutNum賦值
}
```

## 修飾符

`private`、`public`是修飾符：   
- `private`只允許同一個類別中的成員存取
- `public`可以允許其他類別來存取。      

如果類別中沒有加修飾符，預設是`private`。      

欄位通常是`private`私有變數，屬性都是`public`公有變數。 
> 屬性的名稱一般首字母大寫，而欄位一般首字母小寫，或是前面加上底線`_`。

## 屬性的 get/set 是什麼意思？

屬性有兩個方法：`get`、`set`：
- `get`表示：調用時可以得到內部欄位的值(或參考)
- `set`表示：調用時可以給內部的欄位賦值(或參考賦值)

> `set`存取器沒有顯式設定參數，但它有一個隱式參數，用關鍵字`value`表示，它的作用是調用屬性時，可以給內部的欄位或參考賦值。  

### Q：那又何必呢？我把欄位設為`public`不就可以做到對變數的既讀又寫了嗎？

對於對外界公開的資料，我們通常希望能做更多的控制，就好像我們的房子，我們並不希望房子是全透明的，那樣在家裡的所有活動全部被看得清清楚楚，毫無隱私可言。      

比如：門窗其實就是`public`，而房內的東西就是`private`，對於這個房子來說，門窗是可以控制的，我們並不是讓所有的人都可以從門隨意進出，也不希望有蚊子蒼蠅出入(安裝紗窗，只讓陽光和空氣進入)，這就是屬性的作用了。     

如果把欄位宣告為`public`，那就意味著不設防的門窗，任何時候，調用者都可以讀取或寫入，這是非常糟糕的一件事。如果把對外的資料寫成屬性，那情況就會好很多。      

### 去掉set，表示ShoutNum屬性是唯讀的

```c#
private int shoutNum = 3; //宣告一個內部欄位，注意是private，預設叫聲次數為3
public int ShoutNum { //ShoutNum屬性，注意是public，有兩個方法：get/set
    get { return shoutNum; } //get表示：外界調用時可以得到shoutNum的值
}
```

## 控制叫聲次數最多只能叫10聲

```c#
private int shoutNum = 3; //宣告一個內部欄位，注意是private，預設叫聲次數為3
public int ShoutNum { //ShoutNum屬性，注意是public，有兩個方法：get/set
    get { return shoutNum; } //get表示：外界調用時可以得到shoutNum的值
    set { //set表示：外界可以給內部shoutNum賦值
        //shoutNum = (value > 10) ? 10 : value;
        if(shoutNum > 10) {
            shoutNum = 10;
        } else {
            shoutNum = value; 
        }
    } 
}
```

## 套用屬性(修改Shout方法)

```c#
Cat cat = new Cat("Rii");
cat.ShoutNum = 5; //給屬性賦值
Console.WriteLine(cat.Shout()); //我的名字叫 Rii，喵喵喵喵喵喵喵喵喵喵!

public class Cat {
    private string name; //宣告Cat類別的私有變數name

    //叫聲屬性
    private int shoutNum = 3;
    public int ShoutNum {
        get { return shoutNum; }
        set { shoutNum = (value > 10) ? 10 : value; } //控制叫聲次數最多只能叫10聲
    }

    //定義有參數的建構函式
    public Cat(string name) {
        this.name = name; //將參數賦值給私有變數name
    }
    //將建構函式重載
    public Cat() {
    }

    public string Shout() {
        string result = "";
        //做一個迴圈相應小貓叫的次數
        for(int i = 0; i < shoutNum; i++) {
            result += "喵";
        }
        return $"我的名字叫 {name}，{result}!";
    }
}
```


# 5. 封裝

每個物件都包含它自己進行操作所需要的所有資訊，這個特性稱為「封裝」，因此物件不必依賴其他物件來完成自己的操作。方法和屬性包裝在類別中，透過類別的實體來實現。

## 封裝的好處
1. 良好的封裝能夠減少耦合
2. 類別內部的實現可以自由地修改
3. 類別具有清晰的對外介面(ShoutNum屬性、Shout方法)

封裝的好處很容易懂，比如房子就是一個類別的實體，室內的裝飾與擺設只能被室內的居住者欣賞與使用，如果沒有四面牆的遮擋，室內所有的活動在外人面前一覽無遺。由於有了封裝，房內的所有擺設都可以隨意地改變而不用影響他人。然而，如果沒有門窗，一個包裏得密密實實的黑箱子，即使它的空間再寬闊，也沒有實用價值。房屋的門窗，就是封裝物件暴露在外的屬性和方法，專門供人進出，以及流通空氣、帶來陽光。

## 增加狗叫的功能

```c#
Dog dog = new Dog("旺仔");
dog.ShoutNum = 5;
dog.Shout();
 
public class Dog {
    ...
}
```

但是，有沒有發現`Cat`和`Dog`有非常類似的程式碼，程式碼大量重複不是什麼好事情，這就要用到物件導向第二特性「繼承」。


# 6. 繼承

由於貓和狗是哺乳動物，所以貓和狗與哺乳動物是繼承關係。      

物件的繼承代表了一種`is-a`的關係。繼承的工作方式是，定義父類別和子類別(或叫做：基礎類別和衍生類別)，其中子類別繼承父類別的所有特性。子類別不但繼承父類別的所有特性，還可以定義新的特性。        

## 學習繼承要記住的三句話
如果子類別繼承父類別：
1. 子類別擁有父類別非`private`的屬性和功能
2. 子類別具有自己的屬性和功能，即子類別可以擴展父類別沒有的屬性和功能
3. 子類別還可以用自己的方式實現父類別的功能(方法重寫)


> `protected`表示繼承時，子類別可以對基礎類別有完全存取權。 
> 也就是說，用`protected`修飾的類別成員，對子類別公開，但不對其他類別公開


## 把相同程式碼放到Animal動物類別中

```c#
class Animal {
    protected string name = ""; //注意修飾符改為protected
    public Animal() {
        this.name = "無名";
    }
    public Animal(string name) {
        this.name = name;
    }

    protected int shoutNum = 3; //注意修飾符改為protected
    public int ShoutNum { get; set; }
}
```

## Cat 和 Dog 繼承Animal
重複的部分都不用再寫了，但對於建構函式，它不能被繼承，只能被調用。      

對於調用父類別的建構函式可以用`base` 關鍵字。


```c#
public class Cat: Animal 
{
    //繼承建構函式
    public Cat():base() {}

	//繼承有參數的建構函式
    public Cat(string name):base(name) {}
	
	public string Shout() {
        string result = "";
        for(int i = 0; i < shoutNum; i++) {
            result += "喵";
        }
        return $"我的名字叫 {name}，{result}!";
    }
}

public class Dog: Animal 
{
    //繼承建構函式
    public Dog():base() {}

	//繼承有參數的建構函式
    public Dog(string name):base(name) {}
	
	public string Shout() {
        string result = "";
        for(int i = 0; i < shoutNum; i++) {
            result += "汪";
        }
        return $"我的名字叫 {name}，{result}!";
    }
}
```

- 當兩個類別之間具備`is-a`的關係時，就可以考慮用繼承。
- 當兩個類別之間是`has-a`的關係時，此時就不適合用繼承。 

> - `is-a`表示一個類別是另一個類別的特殊種類。        
> - `has-a`代表某個角色具有某一項責任。

# 7. 多型

「多型」表示不同的物件，可以執行相同的動作，但要透過它們自己實現的程式碼來執行。        

怎麼用呢？我們還需要瞭解一些概念：虛方法`virtual`、方法重寫`override`

## 虛方法、方法重寫概念

- 虛方法`virtual`：父類別的方法加上`virtual`，子類就可以重寫方法`override`

為了使子類別的實體完全接替父類別的方法，父類別必須將該方法宣告為虛擬的。透過在該方法的返回類型之前加上`virtual`關鍵字來實現。       

虛擬方法是有方法體的，可以實際做些事情，然後，子類別可以使用`overrid`關鍵字，將父類別實現替換為它自己的實現，這就是方法重寫`Override`，或者叫做方法覆寫。

> 通常虛擬的是方法，除了欄位不能虛擬，屬性、事件和索引器都是可以虛擬的。
  

```c#
//父類別
class Animal {
    //加上修飾符virtual，表示虛擬方法，可以被子類別重寫
    public virtual string Shout() { return ""; }
}

//子類別
class Cat: Animal {
    //加上修飾符override，重寫方法
    public override string Shout() { return "喵"; }
}
```

## 範例：實現多型

由於`Cat`和`Dog`都有「叫聲的方法`Shout()`」，只是叫的聲音不同，所以可以讓`Animal`父類別有一個虛方法`virtual`，然後讓`Cat`和`Dog`去重寫`Shout()`這個方法，就可以用貓或狗來代替`Animal`的叫聲，來達到「多型」的目的。  

```c#
public static void Main()
{
    //宣告一個動物陣列，這個陣列必須宣告成父類別Animal，而不是子類別
    Animal[] arrAnimal = new Animal[2];
    
    //實體化的物件是子類別
    arrAnimal[0]= new Cat("Rii"); //實體化貓類別
    arrAnimal[1]= new Dog("Ki"); //實體化狗類別
    
    //遍歷動物陣列，讓牠們都Shout()
    foreach(Animal item in arrAnimal) {
        //由於有了「多型性」，所以叫的時候，程式會自動去找item是什麼物件，然後用那個重寫方法
        Console.WriteLine(item.Shout());
    }
}

class Animal {
    protected string name = "";
    public string Name { get; set;}

    protected int shoutNum = 3;
    public int ShoutNum { get; set;}

    public Animal() {
        this.name = "無名";
    }

    public Animal(string name) {
        this.name = name;
    }

    public virtual string Shout() {
        return $"我的名字叫{name}";
    }
}

class Cat: Animal {
    public Cat(): base() { }
    public Cat(string name): base(name) { }

    public override string Shout() {
        string result = "";
		for(int i = 0; i < shoutNum; i++) {
			result += "喵";
		}
 		return $"我的名字叫{name}，{result}";
    }
}
class Dog: Animal {
    public Dog(): base() { }
    public Dog(string name): base(name) { }

    public override string Shout() {
        string result = "";
		for(int i = 0; i < shoutNum; i++) {
			result += "汪";
		}
 		return $"我的名字叫{name}，{result}";
    }
}
```

## 多型原理

### 怎樣才能實現多型？
這個物件的宣告必須是父類別，不是子類別，而實體化的必須是子類別。      

```c#
//方法一
Animal animal = new Cat();

//方法二
Cat cat = new Cat();
Animal animal = cat;
```

多型的原理是：當方法被調用時，無論物件是否被轉換為其父類別，都只有位於物件繼承鏈最末端的方法實現會被調用。也就是說，虛擬方法是按照其執行時的類型，而非編譯時類別進行動態繫結調用的。


# 8. 重構

如果現在又來了小牛、小羊，該如何做？    

有沒有發現：貓狗牛羊四個類別，除了叫聲不同，幾乎沒有任何差異，所以除了建構函式外，還有重複的地方，應該要改造它。        

## 範例：重構

1. 把`Shout()`拿掉`virtual`，改成普通的公共方法。   
2. 增加一個「得到叫聲」的虛擬方法`GetShoutSound()`。    
3. 「得到叫聲」的虛擬方法`GetShoutSound()`，讓子類別重寫，只需給繼承的子類別使用，所以用`protected`修飾符。 

```c#
//調用
public static void Main()
{
    Animal[] arrAnimal = new Animal[4];
    
    arrAnimal[0]= new Cat("Rii");
    arrAnimal[1]= new Dog("Ki");
    arrAnimal[2]= new Cattle("ii");
    arrAnimal[3]= new Sheep("Pipi");
    foreach(Animal item in arrAnimal) {
        Console.WriteLine(item.Shout());
    }
}

//父類別：動物
class Animal {
    protected string name;
    public string Name{ get;set; }
    
    protected int shoutNum = 3;
    public int ShoutNum{ get; set; }

    public Animal() {
        this.name = "無名";
    }
    public Animal(string name) {
        this.name = name;
    }

    //1.拿掉virtual，改成普通的公共方法。
    public string Shout() {
        string result = "";
        for(int i = 0; i < shoutNum; i++) {
            //3.改成調用「得到叫聲」的虛擬方法
            result += GetShoutShound();
        }
        return $"我的名字叫{name}，{result}";
    }

    //2.增加一個「得到叫聲」的虛擬方法
    protected virtual string GetShoutShound() {
        return "";
    }
}

//子類別：貓
class Cat: Animal {
    public Cat(): base() { }
    public Cat(string name): base(name) { }

    protected override string GetShoutShound() {
 		return "喵";
    }
}
//子類別：狗
class Dog: Animal {
    public Dog(): base() { }
    public Dog(string name): base(name) { }
	
    protected override string GetShoutShound() {
 		return "汪";
    }
}
//子類別：牛
class Cattle: Animal {
    public Cattle(): base() { }
    public Cattle(string name): base(name) { }
	
    protected override string GetShoutShound() {
 		return "哞";
    }
}
//子類別：羊
class Sheep: Animal {
    public Sheep(): base() { }
    public Sheep(string name): base(name) { }
	
    protected override string GetShoutShound() {
 		return "咩";
    }
}
```

## 重構的思維過程 

先是有一個`Cat`類別，然後再有一個`Dog`類別，觀察後，發現它們有類似之處，於是泛化出`Animal`類別，透過「重構」改善既有程式碼的設計。所以說，抽象類別往往都是透過重構得來的。

# 9. 抽象類別 Abstract

我們再來觀察，會發現，`Animal`類別其實根本就不可能實體化的：`new Animal();`即實體化一個動物。   

動物是一個抽象的名詞，沒有具體物件與之對應。        

所以我們完全可以考慮把實體化沒有任何意義的父類別，改成「抽象類別」，對於`Animal`類別的` GetShoutShound()`方法，其實方法體沒有任何意義，所以可以將`virtual`改成`abstract`使之成為抽象方法。

```c#
abstract class Animal { //抽象類別
   protected abstract GetShoutShound(); //抽象方法：抽象方法沒有方法體
}

```
> 如果類別中有抽象方法，那麼類別就必須定義為抽象類別，不論是否還包含其他一般方法。

## 抽象類別重點

1. 抽象類別不能實體化：例如`Animal`實體化是沒有意義的。
2. 抽象方法必須被子類別重寫：不重寫就沒有存在的意義。
3. 如果類別中有抽象方法，那麼類別就必須定義為抽象類別，不論是否還包含其他一般方法。

# 10. 介面 Interface

介面是把隱式公共方法和屬性組合起來，以封裝特定功能的一個集合。一旦類別實現了介面，類別就可以支援介面所指定的所有屬性和成員。        

介面不能實體化，不能有建構函式和欄位，不能有修飾子，如：`private`、`public`等，不能宣告虛擬`virtual`或靜態`static`等。      

實現介面的類別就必須要實現介面中的所有方法和屬性。      

一個類別可以支援多個介面，多個類別可以支援相同的介面。

## 介面的規範

- 介面的命名，前面要加一個大寫的`I`, 這是規範。  
- 介面用`interface`宣告。
- 介面中的方法或屬性前面不能有修子、方法沒有方法體。


## 範例

如果動物裡，還有動物是有特異功能的，那怎麼辦？例如：小叮噹、孫悟空、蜘蛛人、蝙輻俠等。      

我們先建立一個介面，它是用來「變東西」用的。

### 建立介面
```c#
//宣告一個 IChange介面，此介面有一個方法ChangeThing，返回一字串，參數是一個字串參數
interface IChange {
    string ChangeThing(string thing);
}
```

### 建立機器貓類別
```c#
//機器貓繼承貓，並實現IChange介面
class MachineCat: Cat, IChange {
    public MachineCat(): base() { }
    public MachineCat(string name): base(name) { }

    //實現介面方法，注意不能加override修飾符
    public string ChangeThing(string thing) {
        //base.Shout()表示調用父類別Cat的方法
        return $"{base.Shout()} 我有萬能口袋，我可以變出{thing}";
    }
}
```

### 調用
```c#
//建立兩個類別的實體
MachineCat machineCat = new MachineCat("小叮噹");
StoneMoney stoneMoney = new StoneMoney("孫悟空");

//宣告一個介面陣列，將兩個類別實體賦值陣列
IChange[] arr = new IChange[2];
arr[0] = machineCat;
arr[1] = stoneMoney;

//利用多型，實現不同的ChangeThing
arr[0].ChangeThing("任意門")
arr[1].ChangeThing("72變")

//output:
//我的名字叫小叮噹，喵喵喵 我有萬能口袋，我可以變出任意門
//我的名字叫孫悟空，吱吱吱 我會72變
```

## 什麼時候用介面？

要讓兩個不相干的物件，來做同樣的事情，就可以用「介面」。    

由於我要讓兩個完全不相干的物件，小叮噹和孫悟空來做同樣的事情「變出東西」，我得讓他們不得不實現這件「變出東西」的介面，這樣的話，當我調用介面的「變出東西」的方法時，程式就會根據我實現介面的物件來做出反應，如果是小叮噹，就是萬能口袋，如是孫悟空，就是72變，利用「多型性」完成了兩個不同物本來不可以完成的任務。      

例如：同樣是「飛」，鳥用翅膀飛，飛機用引擎加機翼飛，而超人呢？舉起雙手，握緊拳頭就能飛，他們是完全不同的物件，但是，如果硬要把他們放在一起的話，用一個飛行行為的介面，比如命名為「`IFly`」的介面來處理，就是非常好的辦法。

## 抽象類別 vs 介面
「抽象類別」是自「底而上」抽象出來的，而「介面」是由「頂而下」設計出來的。

### 從形態上區分
- 「抽象類別」可以給出一些成員的實現，「介面」卻不包含成員的實現。
- 「抽象類別」的抽象成員可以被子類別部分實現，「介面」的成員需要實現類別完全實現。
- 一個類別只能繼承一個「抽象類別」，但可繼承實現多個「介面」。  


### 思維過程區分
1. 「類別」是對物件的抽象，「抽象類別」是對類別的抽象，「介面」是對行為的抽象。  
> 介面是對類別局部(行為)進行的抽象，而抽象類別是對類別整體(欄位、屬性、方法)的抽象。
2. 如果行為跨越不同類別的物件，可使用「介面」；對於一些相似的類別物件，用繼承「抽象類別」。
> 比如貓、狗其實都是動物，牠們之間有很多相似的地方，所以我們應該讓牠們去繼承動物這個「抽象類別」，而飛機、麻雀、超人是完全不相關的類別，小叮噹是動漫角色，孫悟空是古代神話人物，這也是不相關的類別，但他們又有共同點，前三個都會「飛」，而後兩個都會「變出東西」，所以此時讓他們去實現相同的介面來達到我們的設計目的就很合適了。
> 其實「實現介面」和繼承「抽象類別」並不衝突，可以讓超人繼承人類，再實現飛行介面。
3. 從設計角度講，「抽象類別」是從子類別中發現了公共的東西，泛化出父類別，然後子類別繼承父類別，而「介面」是根本不知子類別的存在，方法如何實現還不確認，預先定義。

> 如果只有小貓的時候，你就去設計動物類別，這就極有可能成為過度設計了。所以說「抽象類別」往往都是透過「重構」得來的。    
> 而「介面」就完全不是一回事，比如動物比賽大會，所有的比賽項目都有可能是完全不相同的動物在比，他們將如何去實現這些行為也不得而知，此時，能做的事就是事先定義這些比賽項目的「行為介面」。


## 完整程式碼

```c#
public static void Main()
{ 
    //建立兩個類別的實體
    MachineCat machineCat = new MachineCat("小叮噹");
    StoneMoney stoneMoney = new StoneMoney("孫悟空");

    //宣告一個介面陣列，將兩個類別實體賦值陣列
    IChange[] arr = new IChange[2];
    arr[0] = machineCat;
    arr[1] = stoneMoney;

    //利用多型，實現不同的ChangeThing
    Console.WriteLine(arr[0].ChangeThing("任意門"));
    Console.WriteLine(arr[1].ChangeThing("72變"));

    //output:
    //我的名字叫小叮噹，喵喵喵 我有萬能口袋，我可以變出任意門
    //我的名字叫孫悟空，吱吱吱 我會72變
}

//宣告一個 IChange介面，此介面有一個方法ChangeThing，返回一字串，參數是一個字串參數
interface IChange {
    string ChangeThing(string thing);
}

//機器貓繼承貓，並實現IChange介面
class MachineCat: Cat, IChange {
    public MachineCat(): base() { }
    public MachineCat(string name): base(name) { }

    //實現介面方法，注意不能加override修飾符
    public string ChangeThing(string thing) {
        return $"{base.Shout()} 我有萬能口袋，我可以變出{thing}";
    }
}
//孫悟空繼承猴，並實現IChange介面
class StoneMoney: Money, IChange {
	public StoneMoney(): base() { }
    public StoneMoney(string name): base(name) { }
	//實現介面方法，注意不能加override修飾符
    public string ChangeThing(string thing) {
        return $"{base.Shout()} 我會{thing}";
    }
}

//父類別：動物
class Animal {
    protected string name;
    public string Name{ get;set; }
    
    protected int shoutNum = 3;
    public int ShoutNum{ get; set; }

    public Animal() {
        this.name = "無名";
    }
    public Animal(string name) {
        this.name = name;
    }

    //1.拿掉virtual，改成普通的公共方法。
    public string Shout() {
        string result = "";
        for(int i = 0; i < shoutNum; i++) {
            //3.改成調用「得到叫聲」的虛擬方法
            result += GetShoutShound();
        }
        return $"我的名字叫{name}，{result}";
    }

    //2.增加一個「得到叫聲」的虛擬方法
    protected virtual string GetShoutShound() {
        return "";
    }
}

//子類別：貓
class Cat: Animal {
    public Cat(): base() { }
    public Cat(string name): base(name) { }

    protected override string GetShoutShound() {
 		return "喵";
    }
}
//子類別：猴
class Money: Animal {
    public Money(): base() { }
    public Money(string name): base(name) { }

    protected override string GetShoutShound() {
 		return "吱";
    }
}
```


# 11. 集合 ArrayList

`.NetFramework`提供了用於資料儲存和檢索的專用類別，這些類別統稱「集合」。這些類別提供對堆疊、佇列、列表和雜湊表的支持。     

大多數集合類別實現相同的介面，最常用的一種：`ArrayList`。

集合`ArrayList`：它可以根據使用大小動態調整，不用事先設定其大小的限制。還可以隨意地增加、插入或移除某一個範圍的元素，比陣列要方便。

> 但集合`ArrayList`也有不足，`ArrayList`不管你是什麼物件都是接受的，因為在它眼裡所有元素都是`Object`，所以不管是`animalList.Add(123);` 或者是`animalList.Add("HelloWorld");`，在編譯的時候都是沒有問題的，但在執行時，`foreach(Animal e in arrayAnimal)` 需要明確集合中的元性是`Animal`類型，而`123`是整數型，`HelloWorld`是字串型，這就會在執行到此處時發生錯誤，顯然，這是典型的「類型不相符」的錯誤。        
>
> 換句話說，`ArrayList`不是類型安全的。還有就是`ArrayList`對於存放值類型的資料，比如`int`、`string`型，或者`struct`結構的資料，用`ArrayList`就意味著都需要將值類型裝箱為 `Object`物件，使用集合元素時，還需要執行拆箱操作，這就帶來了很大的效能損耗。

## 陣列優缺點

陣列：建立時必須要指定陣列變數的大小，比如：「動物報名」用的是`Animal`類別的物件陣列，你設定了陣列的長度為5，也就是說最多只能有5個動物報名參加，多了就不行，這顯然是非常不合理的。  

### 陣列優點
陣列在記憶體中連續儲存，因此可以快速而容易地從頭到尾走遍元素，可以快速修改元素。

### 陣列缺點
建立時必須要指定陣列變數的大小，這可能使得陣列長度設定過大，造成記憶體空間浪費，長度設定過小造成溢出。還有在兩個元素之間加入元素也比較困難。

```c#
//建立時必須要指定陣列變數的大小
//這可能使得陣列長度設定過大，造成記憶體空間浪費，長度設定過小造成溢出
int[] array = new int[2];
```
## 集合 vs 陣列

「陣列」的容量是固定的，而「集合`ArrayList`」的容量可根據需要自動擴充。     

> `ArrayList`的容量是`ArrayList`可以保存的元素數。`ArrayList`的預定初始容量為`0`。隨著元素被加到`ArrayList`中，容量會根據需要，透過重新分配自動增加。使用整數索引可以存取此集合中的元素。此集合中的索引從`0`開始。      

`ArrayList`是命名空間`System.Collections`下的一部分，它是使用大小可按需動態增加的陣列實現`IList`介面。      

也就是說：`IList`介面定義了很多集合的方法，`ArrayList`對這些方法做了具體的實現。    

> 由於實現了`IList`，所以`ArrayList`提供新增、插入或移除某一範圍元素的方法。

## 範例

### 動物報名-使用集合

```c#
using System.Collections; //增加此命名空間
					
public class Program
{
	public static void Main()
	{
        //宣告一個集合變數，可以用介面IList，也可以直接宣告 ArrayList animalList;
        IList animalList;

		//實體化ArrayList物件，注意，此時並沒有指定animalList的大小，這與陣列並不相同
		animalList = new ArrayList();
		
		//調用集合的Add方法增加物件，其參數是object，所以new Cat和 new Dog都沒有問題
		animalList.Add(new Cat("Rii"));
		animalList.Add(new Cat("小黑"));
		animalList.Add(new Dog("小白"));
		animalList.Add(new Money("Dii"));
		animalList.Add(new MachineCat("小叮噹"));
		animalList.Add(new StoneMoney("孫悟空"));
		
		//集合的Count可以得到現在元素的個數
		Console.WriteLine(animalList.Count.ToString()); //輸出：6
	}
}
```

### 動物叫聲比賽

```c#
//遍歷animalList集合
foreach(Animal animal in animalList){
    animal.Shout();
}
```

### 將動物從名單中移除
如果有動物報名完後，由於某種原因(比如：政治、宗教、服用禁藥、健康等等)放棄比賽，此時應該將其從名單中移除。      

可以應用集合的`RemoveAt`方法，它的作用是移除指定索引處的集合項目。

```c#
//小黑、小白要退出比賽
animalList.RemoveAt(1); //移除小黑
animalList.RemoveAt(1); //移除小黑後，整個後序的物件都前移一位了，所以索引次序不是原來的2
```

> 集合的變化是影響全局的，它可以維持元素的連續性。所以當「小黑」被移除了集合，此時「小白」的索引次序不是原來的2，而是1，等於後序物件都向前移一位了。

## 裝箱 vs 拆箱
### 裝箱 Boxing

裝箱：就是把值類型打包到`Object`參考類型中。比如：將整數變數`i`賦值給物件`o`。

```c#
//裝箱：就是把值類型打包到Object參考類型中。
int i = 123;
object o = i; //裝箱boxing，將整數變數i賦值給物件o
```
### 拆箱 Unboxing

拆箱：就是指從物件中提取值類型。比如：物件`o`拆箱，並將其賦值給整數型變數`i`。

```c#
//拆箱：就是指從物件中提取值類型
object o = 123;
int i = (int)o; //拆箱 Unboxing，物件o拆箱，並將其賦值給整數型變數i
```

裝箱和拆箱過程需要進行大量的計算。對值類型進行裝箱時，必須分配並構造一個全新的物件。其次，拆箱所需的強制轉換，也需要進行大量的計算。        

總之，裝箱和拆箱是耗資料和時間的。而`ArrayList`集合在使用「值類型」資料時，其實就是不斷地做裝箱和拆箱的工作，這顯然是非常糟糕的事情。       

從這點上來看，它就不如陣列了，因為陣列事先就指定了資料類型，就不會有類型安全的問題，也不存在裝箱和裝的事情了，他們和有利弊。        

所以`2.0`就推出了新的技術來解決這個問題，那就是「泛型」。

## 完整程式碼

```c#
using System;
using System.Collections; //增加此命名空間
					
public class Program
{
	public static void Main()
	{	
		//宣告一個集合變數，可以用介面IList，也可以直接宣告 ArrayList animalList;
		IList animalList;
		//實體化ArrayList物件，注意，此時並沒有指定animalList的大小，這與陣列並不相同
		animalList = new ArrayList();
		
		//調用集合的Add方法增加物件，其參數是object，所以new Cat和 new Dog都沒有問題
		animalList.Add(new Cat("Rii"));
		animalList.Add(new Cat("小黑"));
		animalList.Add(new Dog("小白"));
		animalList.Add(new Money("Dii"));
		animalList.Add(new MachineCat("小叮噹"));
		animalList.Add(new StoneMoney("孫悟空"));
		
		//集合的Count可以得到現在元素的個數
		Console.WriteLine(animalList.Count.ToString()); //輸出：6
		
		//動物叫聲比賽-遍歷animalList集合
		foreach(Animal animal in animalList){
			Console.WriteLine(animal.Shout());
		}
		
		//小黑、小白要退出比賽
		animalList.RemoveAt(1); //移除小黑
		animalList.RemoveAt(1); //移除小黑後，整個後序的物件都前移一位了，所以索引次序不是原來的2
		
		//動物叫聲比賽-遍歷animalList集合
		foreach(Animal animal in animalList){
			Console.WriteLine(animal.Shout());
		}
	}
}
//宣告一個 IChange介面，此介面有一個方法ChangeThing，返回一字串，參數是一個字串參數
interface IChange {
    string ChangeThing(string thing);
}

//機器貓繼承貓，並實現IChange介面
class MachineCat: Cat, IChange {
    public MachineCat(): base() { }
    public MachineCat(string name): base(name) { }

    //實現介面方法，注意不能加override修飾符
    public string ChangeThing(string thing) {
        return $"{base.Shout()} 我有萬能口袋，我可以變出{thing}";
    }
}
//孫悟空繼承猴，並實現IChange介面
class StoneMoney: Money, IChange {
	public StoneMoney(): base() { }
    public StoneMoney(string name): base(name) { }
	//實現介面方法，注意不能加override修飾符
    public string ChangeThing(string thing) {
        return $"{base.Shout()} 我會{thing}";
    }
}

//父類別：動物
class Animal {
    protected string name;
    public string Name{ get;set; }
    
    protected int shoutNum = 3;
    public int ShoutNum{ get; set; }

    public Animal() {
        this.name = "無名";
    }
    public Animal(string name) {
        this.name = name;
    }

    //1.拿掉virtual，改成普通的公共方法。
    public string Shout() {
        string result = "";
        for(int i = 0; i < shoutNum; i++) {
            //3.改成調用「得到叫聲」的虛擬方法
            result += GetShoutShound();
        }
        return $"我的名字叫{name}，{result}";
    }

    //2.增加一個「得到叫聲」的虛擬方法
    protected virtual string GetShoutShound() {
        return "";
    }
}

//子類別：貓
class Cat: Animal {
    public Cat(): base() { }
    public Cat(string name): base(name) { }

    protected override string GetShoutShound() {
 		return "喵";
    }
}
//子類別：狗
class Dog: Animal {
    public Dog(): base() { }
    public Dog(string name): base(name) { }
	
    protected override string GetShoutShound() {
 		return "汪";
    }
}
//子類別：猴
class Money: Animal {
    public Money(): base() { }
    public Money(string name): base(name) { }

    protected override string GetShoutShound() {
 		return "吱";
    }
}
```

# 12. 泛型 List
通常情況下，都建議使用「泛型集合」，因為這樣可以獲得類型安全的直接優點，也不必對元素進行裝箱。      

`List`和`ArrayList`在功能上是一樣的，不同就在於，`List`它在宣告和實體化時，都需要指定其內部項目的資料或物件類型，這就避免了剛才提到的類型安全問題和裝箱拆箱的效能問題了。

而`List`類別是`ArrayList`類別的泛型等效類別，該類別使用大小可按需動態增加的陣列實現`IList`介面。

> 泛型 `List`集「`ArrayList`集合」和「`Array`陣列」優點於一身

## 用法
- 首先「泛型集合」需要`using System.Collections.Generic;`命名空間。
- 用法上關鍵就是在`IList`和`List`後面加`<T>`，這個`T`就是你需要指定的集合之資料或是物件類型。

```c#
IList<int> list1 = new List<int>();
List<string> list2 = new List<string>();
```

## 範例

```c#
using System.Collections.Generic; //增加泛型集合的命名空間
					
public class Program
{
	public static void Main()
	{	
		
		//宣告一個泛型集合變數，用介面 IList<Animal>表示此集合變數只能接受Animal類型，其他不可以
		//也可以直接宣告 List<Animal> animalList;
		IList<Animal> animalList;

		//實體化 List物件，注意，此時也需要指定 List<T> 的 T 是Animal
		animalList = new List<Animal>();

        //調用集合的Add方法增加物件，其參數是object，所以new Cat和 new Dog都沒有問題
		animalList.Add(new Cat("Rii"));
		animalList.Add(new Cat("小黑"));
		animalList.Add(new Dog("小白"));
		animalList.Add(new Money("Dii"));
		animalList.Add(new MachineCat("小叮噹"));
		animalList.Add(new StoneMoney("孫悟空"));
		
		//集合的Count可以得到現在元素的個數
		Console.WriteLine(animalList.Count.ToString()); //輸出：6
    }
}
```

此時，如果你再寫`animalList.Add(123);`，或者`animalList.Add("HelloWorld");`，結果就是：編譯就會出現錯誤，因為`Add`的參數類型必須是`Animal`或者是`Animal`的子類型才行。

# 13. 委託與事件

- 「委託」是對函數的封裝，可以當作給方法的特徵指定一個名稱。
- 「事件」則是「委託」的一種特殊形式，當發生有意義的事情時，事件物件處理通知過程。

> 「事件」其實就是設計模式中，觀察者模式在.NET中的一種實現方式。

## 用法

### 宣告
- 委託物件用關鍵字`delegate`來宣告
- 而事件是說：在發生其他類別或物件關注的事情時，類別或物件可透過事件通知它們。事件物件用`event`關鍵字宣告。

```c#
//宣告委託CatShoutEventHandler
public delegate void CatShoutEventHandler();

//宣告事件CatShout，它的事件類型是委託CatShoutEventHandler
public event CatShoutEventHandler CatShout;
```

## 範例
### 需求
有一隻貓叫Tom，有兩隻老鼠叫Jerry和Jack，Tom只要一叫「喵，我是Tom」，兩隻老鼠就說「老貓來了，快跑」。     

### 分析
先分析一下，這裡應該有幾個類別，如何處理類別之間的關係？        

應該有`Cat`和`Mouse`類別，當`Cat`的`Shout()`方法觸發時，`Mouse`就執行`Run()`方法。      

不過這裡如何讓`Shout()`方法觸發時，通知兩隻老鼠呢？顯然老貓不會認識老鼠，也不會主動通知牠們「我來了，你們快跑。」。     

所以在`Cat`類別當中，是不應該關聯`Mouse`類別的。此時用「委託事件」的方式就是好的處理辦法了。    

> 注意，「委託」是一種參考方法的類型。一旦為委託分配了方法，委託將與該方法具有完全相同的行為。

### Cat貓類別

這裡就是宣告了一個委託，委託的名稱叫做：`CatShoutEventHandler`，而這個委託所能代表的方法是：無參數、無返回值的方法
```c#
public delegate void CatShoutEventHandler();
```

然後宣告了一個對外公開的`public`的事件`CatShout`，它的事件類型是委託`CatShoutEventHandler`。表明事件發生時，執行被委託的方法。

```c#
public event CatShoutEventHandler CatShout;
```

### 實際程式碼Cat

```c#
class Cat {
	string name;
	public Cat(string name) {
		this.name = name;
	}
	
	//宣告委託CatShoutEventHandler
	public delegate void CatShoutEventHandler();
	//宣告事件CatShout，它的事件類型是委託CatShoutEventHandler
	public event CatShoutEventHandler CatShout;

	public void Shout() {
		Console.WriteLine($"喵，我是{name}");
		if(CatShout != null) {
			//表明當執行Shout()方法時，如果CatShout中有物件登記事件，就執行CatShout()
			CatShout();
		}
	}
}
```

> 為什麼`CatShout()`是無參數、無返回值的方法？      
> 因為事件`CatShout()`的類型是委託`CatShoutEventHandler`，而`CatShoutEventHandler`就是無參數、無返回值的方法。

### Mouns老鼠類別

```c#
class Mouse {
	string name;
	public Mouse(string name) {
		this.name = name;
	}
	//用來逃跑的方法
	public void Run() {
		Console.WriteLine($"老貓來了，{name}快跑");
	}
}
```

### 關鍵是Main函數的寫法

```c#
public static void Main()
{	
    //實體化老貓Tom及小老鼠Jerry、Jack
    Cat cat = new Cat("Tom");
    Mouse mouse1 = new Mouse("Jerry");
    Mouse mouse2 = new Mouse("Jack");
    
    //將Mouse的Run方法透過實體化委託Cat.CatShoutEventHandler登記到Cat事件CatShout當中
    //其中 += 表示 add_CatShout的意思
    cat.CatShout += new Cat.CatShoutEventHandler(mouse1.Run);
    cat.CatShout += new Cat.CatShoutEventHandler(mouse2.Run);
    
    //貓叫了
    cat.Shout();
}

/* 執行結果
喵，我是Tom
老貓來了，Jerry快跑
老貓來了，Jack快跑
*/
```

### 程式碼說明
- `new Cat.CatShoutEventHandler(mouse1.Run)`的含義是實體化一個委託，而委託的實體就是`Mouse`的`Run()`方法。
- `cat.CatShout +=`表示的就是`cat.add_CatShout(new new Cat.CatShoutEventHandler(mouse1.Run))`的意思。
- `+=`就是增加委託實體物件的意思。


## EventArgs
常看到在`IDE`產生的事件參數，比如`private void button1_click(object sender, EventArgs e)`，這裡的`sender`和`e`有什麼做用呢？

- `object sender`：就是傳遞發送通知的物件。
- `EventArgs e`：是包含事件數據的類別 

`EventArgs`是包含事件資料的類別的基礎類別。換句話說，這個類別的作用就是：用來在事件觸發時傳遞資料用的。     


## 範例
### 寫一個EventArgs子類別

我現在寫一個它的子類別`CatShoutEventArgs`，當中有屬性`Name`表示的就是`CatShout`事件觸發時，需要傳遞`Cat`物件的名字。 

```c#
class CatShoutEventArgs: EventArgs {
    public string Name { get;set; }
}
```

### 改寫Cat類別，重新定義委託
然後改寫`Cat`類別的程式碼，對委託`CatShoutEventHandler`進行重定義。     

增加兩個參數：
- 第一個參數`object`對象`sender`：是指向發送通知的對象。    
- 第二個參數`CatShoutEventArgs`的`args`：包含了所有通知接受者需要附件的資訊。   
在這裡顯然就是老貓的名字資訊。

```c#
class Cat {
	string name;
	public Cat(string name) {
		this.name = name;
	}
	
	//宣告委託CatShoutEventHandler，此時委託所代表的方法有兩個參數：object 和 CatShoutEventArgs
	public delegate void CatShoutEventHandler(object sender, CatShoutEventArgs args);
	//宣告事件CatShout，它的事件類型是委託CatShoutEventHandler
	public event CatShoutEventHandler CatShout;

	public void Shout() {
		Console.WriteLine($"喵，我是{name}");

		if(CatShout != null) {
			//宣告並實體化一個CatShoutEventArgs，並給Name屬性賦值為貓的名字
			CatShoutEventArgs e = new CatShoutEventArgs();
			e.Name = this.name; //給Name屬性賦值為貓的名字
			
			//表明當執行Shout()方法時，如果CatShout中有物件登記事件，就執行CatShout()
			CatShout(this, e); //當事件觸發時，通知所有登記過的物件，並將發送通知的自己(this)，以及需要的資料傳遞過去(e)
		}
	}
}
```
> - `object sender`：就是傳遞發送通知的物件。
> - `EventArgs e`：是包含事件數據的類別 

### Mouse類別也發生變化

由於有了傳遞過來的貓的名字，所以顯示的時候可以指是是老貓誰誰誰來了。

```c#
class Mouse {
	string name;
	public Mouse(string name) {
		this.name = name;
	}
	
	//逃跑的方法中，增加了兩個參數，並且可以在顯示時，說出老貓的名字args.Name
	public void Run(object sender, CatShoutEventArgs args) {
		Console.WriteLine($"老貓{args.Name}來了，{name}快跑");
	}
}
```

### Main函數的程式碼沒有變化，而結果顯示不一樣了
```
喵，我是Tom
老貓Tom來了，Jerry快跑
老貓Tom來了，Jack快跑
```

## 完整程式碼

```c#
public static void Main()
{	
    //實體化老貓Tom及小老鼠Jerry、Jack
    Cat cat = new Cat("Tom");
    Mouse mouse1 = new Mouse("Jerry");
    Mouse mouse2 = new Mouse("Jack");
    
    //將Mouse的Run方法透過實體化委託Cat.CatShoutEventHandler登記到Cat事件CatShout當中
    //其中 += 表示 add_CatShout的意思
    cat.CatShout += new Cat.CatShoutEventHandler(mouse1.Run);
    cat.CatShout += new Cat.CatShoutEventHandler(mouse2.Run);
    
    //貓叫了
    cat.Shout();
}

//事件數據
class CatShoutEventArgs:EventArgs{
	public string Name { get;set;}
}

//老貓
class Cat {
	string name;
	public Cat(string name) {
		this.name = name;
	}
	
	//宣告委託CatShoutEventHandler
	public delegate void CatShoutEventHandler(object sender,CatShoutEventArgs args);
	//宣告事件CatShout，它的事件類型是委託CatShoutEventHandler
	public event CatShoutEventHandler CatShout;
	
	public void Shout() {
		Console.WriteLine($"喵，我是{name}");
		
		if(CatShout != null) {
			//宣告並實體化一個CatShoutEventArgs，並給Name屬性賦值為貓的名字
			CatShoutEventArgs e = new CatShoutEventArgs();
			e.Name = this.name; //給Name屬性賦值為貓的名字
			
			//表明當執行Shout()方法時，如果CatShout中有物件登記事件，就執行CatShout()
			CatShout(this, e); //當事件觸發時，通知所有登記過的物件，並將發送通知的自己(this)，以及需要的資料傳遞過去(e)
		}
	}
}

//小老鼠
class Mouse {
	string name;
	public Mouse(string name) {
		this.name = name;
	}
	
	//逃跑的方法中，增加了兩個參數，並且可以在顯示時，說出老貓的名字args.Name
	public void Run(object sender, CatShoutEventArgs args) {
		Console.WriteLine($"老貓{args.Name}來了，{name}快跑");
	}
}
```

# 14. 客套