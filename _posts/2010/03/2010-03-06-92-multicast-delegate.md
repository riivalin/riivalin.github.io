---
layout: post
title: "[C# 筆記] 多播委託 Multicast-Delegate"
date: 2010-03-06 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,委託,多播委託,Delegate,Multicast-Delegate]
---

# 委託的形象化表述

```
按鈕 -> 
我被點擊後會調用一個委託方法，類型是 OnClickDelegate：
public delegate void OnClickDelegate();
        ↓
真正被調用的委託方法的名字是 OnClick：
public OnClickDelegate OnClick;

如果你需要知道我被點擊了，就把方法委託給我OnClick吧
        ↓
好的，我這裡有一個方法：
void StartGame();
你被點擊的時候，就調用一下我這個方法開始遊戲吧
```
![](/assets/img/post/delegate.png)


比如說，我有一個按鈕，我被點擊後會調用一個委託方法，類型是：`public delegate void OnClickDelegate();`，而真正被調用的委託方法的名字是：`OnClick`這個方法。      

`public OnClickDelegate OnClick;`，也就是說，我被點擊了，會調用一個方法，而這個方法是 `OnClickDelegate`這種類型的方法，而這個類型的變量，或者說，它的委託變量是什麼呢？是`OnClick`。        

也就是說，如果你需要知道我被點擊了，就把方法委託給我這個`OnClick`去執行，來進行代理，來進行委託。  

好的，我這裡有一個方法：`void StartGame();`，你被點擊的時候，就調用一下我這個方法開始遊戲吧。       

為什麼這個`OnClick`會是一種叫做委託的類型，其實說白了，就是別人委託給了這個`OnClick`來執行一些方法。        

那`OnClick`執行的時機呢？是不是就由這個按鈕來進行決定啊，點它了才執行`OnClick`。


# 委託的多播
- 多播：一個**主委託對象**可以容納多個其他的**子委託對象**，當調用主委託對象，會將所有的子委託全部按序運行

![](/assets/img/post/multicast-delegate.png)

比如說，按下「遊戲開始」按鈕後，系統會加載地圖、玩家數據、怪物關卡數據，如果都寫在一個方法裡，就會顯得邏輯混亂且擁擠，那麼。        

現在我們把它分成三個不同的方法：
- `initMap`：加載遊戲地圖
- `initPlayer`：初始化玩家系統
- `initMonsters`：初始化怪物系統

但是，你看唷，拆成三個方法後，就會存在一系列的問題：      
「遊戲開始」是不是有一個叫做`OnClick`的這麼一個委託呀，之前學的知識是說這個`OnClick`委託裡面呢，它背後是不是存在著一個方法呀，如果我們把`initMap`方法給了`OnClick`委託去執行，當我們點擊「遊戲開始」的時候，`OnClick`就會運行，其實運行的就是`initMap`方法，對吧，但是你看剩下的兩個方法，是不是沒有得到運行啊。        

所以說，有沒有什麼辦法能夠這樣子呢，當我點擊「遊戲開始」之後，運行了`OnClick`這樣一個委託，然後這個委託裡面，能不能夠去蘊含著三個不同的方法，然後讓他依次運行，這樣是不是就很方便，就叫做「多播」。     

也就是說，其實C#的委託，它給我們提供了一種功能，它不僅能夠讓它內部去蘊含著某一個方法，它也可以讓這個委託內部去蘊念著多個不同的方法，那在運行這個`OnClick`委託的時候呢，它會將它內部所蘊含的多個方法去依次執行，這個就叫做「委託的多播」，你看，是不是好像廣播一樣呀，運行`OnClick`委託的時候，它就會通知A方法運行一下、B方法你也運行一下，C方法你也運行一下，就像廣播一樣。

# 案例

```c#
public delegate int Calculate(int x, int y);

public static int Add(int x, int y) {
    Console.WriteLine(x+y);
    return x + y;
}
public static int Multiply(int x, int y) {
    Console.WriteLine(x*y);
    return x * y;
}
......
Calculate calc1 = new Calculate(Add);
Calculate calc2 = new Calculate(Multiply);
Calculate calc3 = new Calculate(Add);
//==================================
//多播委託
Calculate calc = null;
calc += calc1;
calc += calc2;
calc += calc3;
calc(1,3); //輸出: 4,3,4

//多播調用的返回值是最後一個執行委託的返回值
int i = calc(1,3); //i=4, 也就是最後一個calc3的Add方法
```

這段程式碼是這樣說的：它是先定義了一個委託的類型，叫做`Calculate`，那這個類型代表的委託是什麼樣呢？是返回 `int`類型數據，而且呢，需要傳入兩個`int`類型的數據做為參數 這種委託。     

然後咱們定義了兩個方法，一個叫做`Add`，一個叫做`Multiply`，他們都需要傳入兩個整數。     

隨後咱們定義了三個委託對象/物件，`calc1`、`calc3`代表的是`Add`方法，`calc2`代表的是`Multiply`方法。

現在來看一下，「多播」是怎麼播出去的。      

首先我們定義了一個`Calculate`類型的委託對象/物件，一開始設為空 `null`，就是沒有給它分配任何的委託的方法，然後也沒有去`new`這樣一個委託對象/物件。       

然後，`calc`依次`+=` `calc1`、`calc2`、`calc3`，先把 1號委託放到了我這個委託裡，又將2號放進來，再放3號進來。這意思是說，向`calc`這樣的委託裡面去添加一些「子委託」，那123 這三個委託都屬於`calc`的子委託。      

你可以將`calc`理解成為一個蓄水池，他裡面就儲放著`calc1`、`calc2`、`calc3`這123號這三個委託，最後去執行`calc`這樣的委託，傳入的參數是1、3，運行一下，他會輸出 4,3,4。        

他會將蘊含的三個委託：1號2號3號 分別進行執行，執行這三個委託是不是要傳入對應的參數呀，於是1和3就傳入了。        

你看，在一個「主委託」裡，包含著三個通過`+=`這樣的符號，讓它包含了三個「子委託」，那「主委託」一執行，那三個「子委託」就分別執行進行輸出了。        

這時候你可能會很疑惑，`calc`是`null`啊，為什麼可以就在這直接做`+=`呢？      

`calc`主委託等於`null`，`null`的意思就是說當前這個委託對象它還沒有被分配內存，它是一個空的，那對於一個空對象來說，你竟然直接`+=`加等加等加等這個直接用它來做一些操作了，這樣能行嗎？      

大家放心，這裡C#提供一個良好的機制，雖然它是空，但是在它`+=`這樣一個另外一個委託的時候，就是將1號放入到`calc`當中的這一刻，那`calc`會主動的，就是說咱們C#會主動的幫咱們去生成一個`calc`對象，這個對象呢，就是在這一步生成的，生成之後才將`calc1`1號放進去，隨後放`calc2`2號放`calc3`3號。       

所以，`calc`是`null`，為什麼可以直接做`+=`，因為C#背後為咱們自動的生成了這個`calc`對象，是在第一個`+=`的時候生成的。        


# 知識點

- 通過`+`增加「子委託」，通過`-`刪除某個「子委託」
- 多播調用的返回值是最後一個執行委託的返回值

# 多播委託創建
## 1.創建包含三個的新多播委託1

```c#
Calculate calc = calc1 + calc2 + calc3;
```
## 2.創建包含三個的新多播委託2

```c#
Calculate calc = null;
calc += calc1;
calc += calc2;
calc += calc3;
```
## 3.創建包含三個的新多播委託(`calc`與`calc1`不是同一個) 

```c#
Calculate calc = calc1;
calc += calc2;
calc += calc3;
```

委託的對象/物件創建，或者對象/物件的這樣的相等操作，並不會讓二者簡簡單單的指向同一個位置，也就是說，它不是將`calc1`它所蘊含的這個對象地址給到`calc`，而是說，需要創建新的多播委託。     

也就是說，在第一句話的時候，其實是這樣執行的：發現`calc = calc1`，他們會首先創建一個新的一個委託對象叫`calc`，是一個新的對象/物件，跟這個`calc1`並不是同一個對象/物件。     

換句話說，在一個 `stack`/堆疊/棧、`heap`/堆積/堆，

在`stack`，他首先存在了一個`calc1`1號對象/物件，這個1號對象是不是應該指向`heap`上的某一個內存空間。     

然後呢，又有一個叫做`calc`的對象/物件，我們讓`calc = calc1`，並不會讓它直接指向同一個對象/物件，而是說，`calc`會自己創建一個新的委託對象/物件而去指向它。  

```
calc1 ---> ****
calc  ---> ****
stack      heap
```

這個和我們普通的引用類型的互相之賦值不太一樣，需要注意。        

那創建一個新的`calc`對象之後，這一步相等的操作還沒有執行完，它呢，會將`calc1`作為一個子委託對象，加入到`calc`的池子當中。

```
calc
-------
calc1
```

隨後，你看它再執行後面的程式碼，`+= calc2`2號也被放進來

```
calc
-------
calc1
calc2
```

`+= calc3`3號也會被放進來

```
calc
-------
calc1
calc2
calc3
```
隨後再去執行 `calc`的時候，其實是1號2號3號 依次執行，就是這樣一個原理。

# 練習

```c#
/*
 * 委託的多播
 *   含義：存在一個主委託，內函多個子委託，運行主委託的時候，會將蘊含的子委託依次執行
 */
namespace MultiDelegate
{
    internal class Program
    {
        //定義委託類型
        public delegate int CalculateDelegate(int x, int y);

        //定義一些方法
        public static int Add(int x, int y)
        {
            Console.WriteLine($"Add:{x + y}");
            return x + y;
        }
        public static int Multiply(int x, int y)
        {
            Console.WriteLine($"Multiply: {x * y}");
            return x * y;
        }

        static void Main(string[] args)
        {
            CalculateDelegate calc1 = new CalculateDelegate(Add);
            CalculateDelegate calc2 = new CalculateDelegate(Multiply);
            CalculateDelegate calc3 = new CalculateDelegate(Add);

            //1. 通過表達式創建多播委託
            CalculateDelegate calc = calc1 + calc2;
            //calc -= calc1;

            //2. 通過初始化為null，經過+=加法運算創建多播委託
            //CalculateDelegate calc = null;
            //calc += calc1;//calc的創建是在這句程式碼，創建全新的委託對象
            //calc += calc2;

            //3. 通過=等於某個子委託來創建多播委託
            //CalculateDelegate calc = calc1; //並不是將calc1地址給到calc，而是創建全新的calc對象
            //calc += calc2;

            //int result = calc(1, 3);
            //Console.WriteLine(result);

            //calc1(Add) calc2(Multiply)
            //calc = calc1 + calc2

            //特殊案例
            CalculateDelegate mcalc1 = calc1 + calc2; //Add + Multiply
            CalculateDelegate mcalc2 = calc1 + calc3; //Add + Add
            CalculateDelegate mcalc = mcalc1 + mcalc2;
            mcalc(1, 3);

            Console.Read();
        }
    }
}
```

# 委託的多播整理
![](/assets/img/post/multicast-delegate2.png)

# 委託的多播逐個調用 GetInvocationList()
- 如果需要拿到每個回調方法的執行返回值，可以採用逐個委託調用

## 舉例

```c#
//定義委託類型
public delegate int Calculate(int x, int y);
//定義一些方法
public static int Add(int x, int y) {
    return x + y;
}
public static int Multiply(int x, int y) {
    return x * y;
}
//定義三個委託
Calculate calc1= new Calculate(Add);
Calculate calc2= new Calculate(Multiply);
Calculate calc3= new Calculate(Add);

//創建一個總委託
Calculate calc = calc1 + calc2 + calc3;

//使用foreach，委託的GetInvocationList()方法 取得子委託
foreach (CalculateDelegate c in calc.GetInvocationList()) {
    int r = c(1, 3);
    Console.WriteLine(r);
}
```

# 練習

```c#
/*
 * 委託的多播
 *      含義：存在一個主委託，內函多個子委託，運行主委託的時候，會將蘊含的子委託依次執行
 */

namespace MultiDelegate
{
    internal class Program
    {
        //定義委託類型
        public delegate int CalculateDelegate(int x, int y);

        //定義一些方法
        public static int Add(int x, int y)
        {
            Console.WriteLine($"Add:{x + y}");
            return x + y;
        }
        public static int Multiply(int x, int y)
        {
            Console.WriteLine($"Multiply: {x * y}");
            return x * y;
        }

        static void Main(string[] args)
        {
            CalculateDelegate calc1 = new CalculateDelegate(Add);
            CalculateDelegate calc2 = new CalculateDelegate(Multiply);
            CalculateDelegate calc3 = new CalculateDelegate(Add);

            //1. 通過表達式創建多播委託
            CalculateDelegate calc = calc1 + calc2 + calc3;

            //1.1 獲取總委託裡面的子委託
            Delegate[] delegates = calc.GetInvocationList();

            //1.2 循環遍歷每一個子委託，並且執行打印返回值
            for (int i = 0; i < delegates.Length; i++)
            {
                CalculateDelegate c = (CalculateDelegate)delegates[i];
                int result = c(1, 3);
                Console.WriteLine(result);
            }

            foreach (CalculateDelegate c in calc.GetInvocationList())
            {
                int result = c(1, 3);
                Console.WriteLine(result);
            }

            //calc -= calc1;

            //2. 通過初始化為null，經過+=加法運算創建多播委託
            //CalculateDelegate calc = null;
            //calc += calc1;//calc的創建是在這句程式碼，創建全新的委託對象
            //calc += calc2;

            //3. 通過=等於某個子委託來創建多播委託
            //CalculateDelegate calc = calc1; //並不是將calc1地址給到calc，而是創建全新的calc對象
            //calc += calc2;

            //int result = calc(1, 3);
            //Console.WriteLine(result);

            //calc1(Add) calc2(Multiply)
            //calc = calc1 + calc2

            //特殊案例
            //CalculateDelegate mcalc1 = calc1 + calc2; //Add + Multiply
            //CalculateDelegate mcalc2 = calc1 + calc3; //Add + Add
            //CalculateDelegate mcalc = mcalc1 + mcalc2;
            //mcalc(1, 3);

            Console.Read();
        }
    }
}
```

# 總結

1. 委託」其實就是方法的引用，其聲明格式為：

```c#
public delegate 返回值類型 委託類型名字 (參數列表...);
public 委託類型名字 委託名字;
```

2. 委託用在某個事務完畢後回調，或者響應某個事件 
> 例如：計時器，計時器計時5秒後需要要通知你，其實計時器裡的class就是一個委託，當計時器完事之後，是不是可以調用這個委託呀，這個委託就會調用它背後的那個方法去執行一些事情。  

> 再比如說，咱們這個按鈕點擊後，按鈕裡面是不是有一個委託呀，然後按鈕一看，我被點擊了，我就趕緊去調用這個委託，我也不知道它他表的哪個方法，但是呢，我就一定要調用他，於是我們是不是可以把需要獲取這個按鈕點擊事件的這些方法給到這個委託呀，點這個按鈕，那這個委託被調用，他背後的那方法就被調用了。

3. 一個委託可以包含多個子委託，從而實現廣播調用

```c#
Calculate calc = null;
calc + = clac1;
calc + = clac2;
calc + = clac3;
clac(1,3);
```

[https://www.bilibili.com/video/BV1VL411q7t4](https://www.bilibili.com/video/BV1VL411q7t4)