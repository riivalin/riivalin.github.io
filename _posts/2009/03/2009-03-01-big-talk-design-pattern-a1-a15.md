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


# 7. 多型
# 8. 重構
# 9. 抽象類別
# 10. 介面
# 11. 集合
# 12. 泛型
# 13. 委託與事件
# 14. 客套