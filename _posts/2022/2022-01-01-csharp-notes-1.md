---
layout: post
title: "C# Notes - Part I"
date: 2022-01-01 00:00:10 +0800
categories: [Notes, C#]
tags: [C#]
---

## 數學運算符-自加(++)自減(- -)

++不管是放在操作數的前面或後面，都是讓操作數+1
```c#
int num = 8;
num++; //8
++num; //8
```
如果++和- -是放在運算式裡：

++如果放在操作數的後面，會先賦值運算，再自加
```c#
int res = num++; //res:8, num:9
```

++如果放在操作數的前面，會先自加，再進行運算賦值
```c#
int res = ++num; //res:9, num:9
```

## while/ do-while 循環語句
while 會先進行條件判斷，再根據判斷的結果去判定是否執行循環體(執行次數>=0)
```c#
int index = 1;
while (index <= 9)
{
    Console.WriteLine(index);
    index++;
}
```

do-while 會先執行一次循環體，再進行條件判斷(執行次數>=1)
```c#
int index = 1;
do
{
    Console.WriteLine(index);
    index++;
} while (index <= 9);
```
## 循環中斷break(終止當前的循環)
```c#=c#
while (true)
{
    Console.WriteLine(index);
    if (index == 9) break; //立刻跳出循環
    index++;
}
```

練習：接受用戶輸入，並顯示到螢幕上，直到用戶輸入一個0，結束程式。
```c#
while (true) //死循環
{
    string? str = Console.ReadLine(); //接受用戶輸入
    if (str == "0") 
        break; //跳出循環
    else
        Console.WriteLine(str); //顯示用戶的輸入
}
```
## 循環中斷continue(只會終止當次的循環)
continue 只會終止當次的循環，繼續下一個循環
```c#
int index = 0;
while (true)
{
    index++;
    if (index == 5) continue; //當index==5的時候，使用continue關鍵字，continue後面的代碼就不會去執行(終止當次循環)，進行下一個循環
    if (index == 10) break; //當index==10的時候，使用break關鍵字，立刻終止循環直接跳出
    Console.WriteLine(index); //1,2,3,4,6,7,8,9
}
```

練習：接受用戶輸入的整數，如果用戶的輸入是大於0的偶數，就相加，如果是大於0的奇數就不相加，如果用戶輸入0，就把和輸出並退出程式。
```c#
int sum = 0;
while (true)
{
    int num = Convert.ToInt32(Console.ReadLine()); //用戶輸入
    if (num == 0) break; //輸入0就立刻終止循環
    if (num % 2 == 1) continue; //如果是奇數就不相加
    sum += num; //求合
}
Console.WriteLine(sum);
Console.ReadKey();
```
## 循環中斷goto(可以直接跳到某一個位置)

練習: 接受用戶輸入，如果輸入0，就使用goto退出循環
```c#
while (true)
{
    int num = Convert.ToInt32(Console.ReadLine()); //用戶輸入
    if (num == 0) goto myLabel; //輸入0，使用goto跳出循環
}
myLabel:
Console.WriteLine("跳出循環了!");
Console.ReadKey();
```
## 循環中斷return(跳出循環、跳出函式)

練習: 接受用戶輸入，如果輸入0，就使用return跳出循環
(基本上很少 return來跳出循環，它是用來終止方法的)
```c#
while (true)
{
    int num = Convert.ToInt32(Console.ReadLine());
    if (num == 0) return; //用來終止方法的，表示方法運行結束，後面的代碼就不會執行了
}
Console.WriteLine("跳出循環了!");
Console.ReadKey();
```
## 練習: for循環

練習1：求1~1000之間可以被7整除的數，並計算和輸出每5個的和
```c#
int count = 0; //計數器
int sum = 0; //總合
for (int i = 1; i <= 1000; i++) //1~1000之間的整數
{
    if (i % 7 == 0) //可以被7整除
    {
        count++; //計數器+1
        sum += i; //求和
        Console.WriteLine($"{i}可以被7整除");
        
        if (count == 5) //計數器==5的時候，輸出和
        {
            Console.WriteLine($"這5個的和是: {sum}");
            count = 0; //計數器歸零
            sum = 0; //歸零重新計算
        }
    }
}
Console.ReadKey();
```
練習2：求1~100的平方、平方根
```c#
for (int i = 1; i <= 100; i++)
{
    int square = i * i;
    double sqrt = Math.Sqrt(i);
    Console.WriteLine($"{i}的平方是{square},平方根是{sqrt}");
}
Console.ReadKey();
```

練習3：1~100能被3整除，但不能被5整除的數，並統計有多少這樣的數
```c#
int count = 0;
for (int i = 1; i <= 100; i++) //1~100
{
    if ((i % 3 == 0) && (i % 5 != 0)) //能被3整除，但不能被5整除的數
    {
        count++;
        Console.WriteLine(i);
    }
}
Console.WriteLine($"這樣的數總共有{count}個");
Console.ReadKey();
```

練習4：求1~1000的質數
(質數是大於1，且除了1和本身之外，不能被其他自然數整除)
```c#
for (int i = 2; i <= 1000; i++)
{
    bool isPrime = true;
    for (int j = 2; j <= i - 1; j++) //檢查i是不是質數，有沒有因子，去遍歷所有比i小的所有數，看能不能被整除
    {
        if (i % j == 0) //可以被整除，就代表有一個因子，就不是質數
        {
            isPrime = false;
            break; //不是質數，就跳出int j 這個迴圈
        }
    }
    if (isPrime) Console.WriteLine(i); //是質數就輸出
}
Console.ReadKey();
```

練習5：99乘法表
```c#
for (int i = 1; i < 10; i++)
{
	for (int j = 1; j < 10; j++)
	{
		Console.Write($"{i}*{j}={i*j}\t");
	}
	Console.WriteLine();
}
Console.ReadKey();
```
練習6：編寫一個擲骰子100次的程序，並印出各種點數的出現次數
```c#
int num1 = 0, num2 = 0, num3 = 0, num4 = 0, num5 = 0, num6 = 0;
Random random = new Random();
for (int i = 0; i < 100; i++)
{
    int num = random.Next(1, 7); //產生一個1~6的隨機數(random是不包含Max最大數,所以maxVaule是7)
    switch (num)
    {
        case 1:
            num1++;
            break;
        case 2:
            num2++;
            break;
        case 3:
            num3++;
            break;
        case 4:
            num4++;
            break;
        case 5:
            num5++;
            break;
        case 6:
            num6++;
            break;
    }
}
Console.WriteLine($"{num1}\n{num2}\n{num3}\n{num4}\n{num5}\n{num6}");
Console.ReadKey();
```
練習7：1~5平方值(for, while, do-while)
for
```c#
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i * i);
}
```
while
```c#
int index = 1;
while (index < 6)
{
    Console.WriteLine(index*index);
    index++;
}
Console.ReadKey();
```
do-while
```c#
int index = 1;
do
{
    Console.WriteLine(index * index);
    index++;
} while (index < 6);
Console.ReadKey();
```
## 練習: 循環結構、字串處理
練習1：要求用戶輸入5個大寫字母，如果用戶信息不滿足要求，提示幫助訊息並要求重新輸入
```c#
while (true) //死循環
{
    string str = Console.ReadLine()!; //用戶輸入
    bool isAllUpper = true;
    for (int i = 0; i < 5; i++)
    {
        if (str[i] >= 'A' && str[i] <= 'Z')
        {
            //Todo something.
        } 
        else
        {
            isAllUpper = false; break; //有不是大寫的字母，跳出for迴圈
        }
    }
    if (!isAllUpper)
        Console.WriteLine("你輸入的5個字母，不全是大寫，請重新輸入");
    else
        break; //5個全是大寫字母，跳出while迴圈結束
}
Console.ReadKey();
```
練習2：
1. 接受一個整數n
2. 如果接收值為正數，輸出1~n之間的全部整數
3. 如果接收值為負數，退出程式
4. 如果為0，則繼續1.接收下一個整數
```c#
while (true) //死循環
{
    int num = Convert.ToInt32(Console.ReadLine()); //接受用戶輸入
    if (num > 0) {
        for (int i = 1; i <= num; i++) {
            Console.WriteLine(i);
        }
    } else if (num < 0) {
        return;
    }
}
```
練習3：列出1~1000的完全數。6=1+2+3
(完數：除了自身以外的因數的和，恰好等於它本身)
```c#
for (int i = 1; i <= 1000; i++)
{
	string str = "1";
	int sum = 1; //1是所有數的因子
	for (int j = 2; j < i; j++) //遍歷所有比它小的數
	{
		if (i % j == 0) { //取得所有因子
			str += $"+ {j}";
			sum += j; //計算因子和
		}
	}
	if (sum == i) {
		Console.WriteLine($"{i}是完全數: {str}");
	}
}
Console.ReadKey();
```

## 顯式轉換、隱式轉換
- 隱式轉換：編譯器自動識別，不需要我們寫更多的代碼。
- 顯式轉換：需要我們告訴編譯器，什麼類型轉換成什麼類型。

```c#
byte myByte = 123;
int myInt = myByte; //隱式轉換：把小類型的數據複製給大類型的變數，編譯器會自動進行類型的轉換
myByte = (byte)myInt; //顯式轉換：當把大類型賦值給小類型的變數，需要進行顯示轉換(強制類型轉換)
```

**隱式轉換**
當小盒子放進大盒子的時候，可以自動進行類型轉換。
```c#
char c = 'a';
float myfloat = c;
```

**顯式轉換**
當大盒子的數據放進小盒子裡面的時候，有可能小盒子裝不下，所以編譯器不允許直接這麼寫：
```c#
int i = 123;
short j = i;
```
我們可以通過顯式轉換的方式：
```c#
short j = (short)i;
```
> 使用Convert命令進行顯式轉換：
> Convert.ToInt32(val)、Convert.ToDecimal(val)…

## 列舉類型enum

**除了簡單的變數類型之外，C#還提供了三個複雜的變量：列舉enum、結構、數組。**
```c#
//定義列舉
enum GameState: byte //修改該enum的儲存類型，默認為int
{ 
    Pause, //遊戲暫停，默認值是0
    Failed, //遊戲失敗，默認值是1
    Success, //遊戲成功，默認值是2
    Start //開始，默認值是3
}
```
```c#
GameState state = GameState.Start; //利用定義好的列舉 去宣告變數
if (state == GameState.Start) {
    Console.WriteLine("當前處於遊戲開始狀態");
}
```
## 結構/結構體Struct
如果我們表示一個向量的話，需要定義三個類型x,y,z：
```c#
float enemy1X = 44;
float enemy1Y = 23;
float enemy1z = 345;
```
這樣比較麻煩，不容易管理，我們可以使用結構struct：

**舉例1：表示一個遊戲物體(主角或敵人的坐標)，需要三個小數。**
```c#
//定義坐標位置的結構體
struct Position {
    public float x;
    public float y;
    public float z;
}
```
```c#
//第一個敵人的位置
Position enemy1Position;
enemy1Position.x = 12;
enemy1Position.y = 33;
enemy1Position.z = 323;


//第二個敵人的位置
Position enemy2Position;
enemy2Position.x = 33;
enemy2Position.y = 99;
enemy2Position.z = 89;
```
**舉例2：定義一個表示路徑的結構，路徑有一個方向和距離所組成，假定方向只能是東西南北**

```C#
//定義方向:東西南北
enum Direction { 
    East,
    West,
    South,
    North
}
```
```C#
//定義路徑:方向+距離
struct Path {
    public float distance; //距離
    public Direction dir; //方向
}
```
```C#
//利用結構體去宣告一個變數
Path path1;
path1.dir = Direction.South;
path1.distance = 1000;
```
Q：什麼時候使用列舉enum呢?
enum用來表示可取的範圍有幾個的時候(有限的範圍)，我們可以聲明一個新的類型，增加可讀性。

Q：什麼時候使用結構struct呢?
結構體是幾個類型的一個結合體，它可以當成是幾個類型組合成一個新的類型。

## 陣列的定義和初始化
陣列初始化的方式
```c#
int[] scores = { 1, 2, 3, 4, 5 }; //隱含型別，直接賦值
int[] scores = new int[] { 1, 2, 3, 4, 5 }; //不指定大小
int[] scores = new int[5] { 1, 2, 3, 4, 5 }; //指定大小
```
分開寫(宣告和初始化分開做)，先宣告陣列變數，當要賦值給這變數時，就要加new關鍵字
```c#
int[] scores; //宣告陣列變數
scores = new int[3]; //初始化
scores = new int[] { 1, 2, 3 }; //不指定長度，並具體表明裡面的元素
scores = new int[3] { 1, 2, 3 }; //指定長度為3
//scores = { 1,2,3 }; //Error,使用這種方式賦值，只能用在宣告陣列變數的時候賦值：int[] arr = {1,2,3}
```
## 陣列的遍歷for, while, foreach
**for**
```c#
int[] scores = { 71, 52, 83, 45, 77, 99, 60 };
for (int i = 0; i < scores.Length; i++) {
    Console.WriteLine(scores[i]);
}
```
**while**
```c#
int[] scores = { 71, 52, 83, 45, 77, 99, 60 };
int i = 0;
while (i <scores.Length) {
    Console.WriteLine(scores[i]);
    i++;
}
```
**foreach**
```c#
int[] scores = { 71, 52, 83, 45, 77, 99, 60 };
foreach (int e in scores) {
    Console.WriteLine(e);
}
```
## 字串的處理
遍歷字串中的每一個字元
```c#
string str = "www.google.com";
for (int i = 0; i < str.Length; i++) {
    Console.WriteLine(str[i]);
}
```
其他：ToLower, ToUpper, Trim, TrimStart, TrimEnd
```c#
string str = "  www.google.COM  ";
Console.WriteLine(str.ToLower()); //轉小寫
Console.WriteLine(str.ToUpper()); //轉大寫
Console.WriteLine(str.Trim()); //去掉前後空白
Console.WriteLine(str.TrimStart()); //去掉前面空白
Console.WriteLine(str.TrimEnd()); //去掉後面空白
```
Split()按照指定的字符進行拆分
```c#
string str = "www.google.com";
string[] arr = str.Split('.'); //按照點(.)的字符進行拆分
foreach (var item in arr) {
    Console.WriteLine(item);
}
```
## 練習: 找到100~999的水仙花數
```c#
for (int i = 100; i < 1000; i++)
{
    int unitsDigit = i % 10;//個位數
    int tensDigits = (i / 10) % 10; //十位數(除以10先把個位數拿掉)
    int hundredsDigit = i / 100; //百位數
     //求立方和:(個位數3次方)+(十位數3次方)+(百位數3次方)
    int res = unitsDigit * unitsDigit * unitsDigit + tensDigits * tensDigits * tensDigits + hundredsDigit * hundredsDigit * hundredsDigit; //取立方(3次方)相加
    
    if (res == i) { //和==自己本身就是水仙花數
        Console.WriteLine(i);
    }
}
```
## 練習: 三個可樂瓶可以兌換一個可樂，現在有364瓶可樂，我們總共可以喝多少可樂，還剩餘多少空瓶。
```c#
int sum = 364; //表示現在可以喝多少可樂(現在有364瓶可樂)
int numEmpty = sum; //表示現在有多少空瓶數

while (numEmpty >= 3) //空瓶子>=3的時候才能兌換
{
    int newCount = numEmpty / 3; //可以兌換多少新的可樂
    sum += newCount; //可以喝的可樂數+兌換的可樂數
    int remainEmpty = numEmpty % 3; //剩餘幾個的空瓶沒有兌換
    numEmpty = newCount + remainEmpty; //兌換後，還有多少空瓶
}
Console.WriteLine($"我們一共喝了{sum} 瓶可樂，剩餘的空瓶子個數是：{numEmpty}");
Console.ReadKey();
```
## 練習: 猜數字遊戲
請用戶輸入0~50之間的數字 
```c#
Random random = new Random();
int number = random.Next(1, 51); //隨機數0~50

Console.WriteLine("我有一個數字，請猜猜是多少，請輸入0~50之間的數字");
while (true)
{
    int temp = Convert.ToInt32(Console.ReadLine()); //接收用戶輸入
    if (temp > number) {
        Console.WriteLine("您猜大了，比這個數字小");
    } else if (temp < number) {
        Console.WriteLine("您猜小了，比這個數字大");
    } else {
        Console.WriteLine($"您猜對了，這個數字為{number}，遊戲結束");
        break;
    }
}
Console.ReadKey();
```
## 練習: 字母加密
加密規則：大小寫字母向後移動三個位置，不是字母的就不動
ex: a→d,b→e, c→f, y→b, A→D, Z→C
```c#
string str = Console.ReadLine()!; //接收用戶輸入
string tempStr = "";

for (int i = 0; i < str.Length; i++) //遍歷每一個字母
{
    if (str[i] >= 'a' && str[i] <= 'z') //說明這個字元是小寫字母
    {
        int num = Convert.ToInt32(str[i]); //將字元轉換成數字
        num += 3;//轉換成數字後+3，相當於向後移動三個字元
        char temp = (char)num; //再轉回字元，此為向後移動三個位的字元

        if (temp > 'z') {  //表示超過範圍
            temp = (char)(temp - 'z' + 'a' - 1); //超出26個字母的範圍，就轉換到開頭'a'
        }
        tempStr += temp;
    } 
    else if (str[i] >= 'A' && str[i] <= 'Z')  //說明這個字元是大寫字母
    {
        int num = Convert.ToInt32(str[i]); //將字元轉換成數字
        num += 3;//轉換成數字後+3，相當於向後移動三個字元
        char temp = (char)num; //再轉回字元，此為向後移動三個位的字元

        if (temp > 'Z') {  //表示超過範圍
            temp = (char)(temp - 'Z' + 'A' - 1); //超出26個字母的範圍，就轉換到開頭'a'
        }
        tempStr += temp;
    } 
    else //不是字母的就不動
    {
        tempStr += str[i];
    }
}
Console.WriteLine(tempStr);
Console.ReadKey();
```
## 練習: Array.Sort方法、冒泡排序
用戶輸入一組數字(用空格間隔)，對用戶輸入的數字從小到大排序(Array.Sort方法和冒泡排序)

先將字串轉成int陣列
```c#
string str = Console.ReadLine()!; //接收用戶輸入
string[] strArray = str.Split(' '); //空隔分割
int[] numArray = new int[strArray.Length];

for (int i = 0; i < strArray.Length; i++) {
    numArray[i] = Convert.ToInt32(strArray[i]);
}
```
使用Array.Sort內置的快速排序(一般用這個就可以了)
```c#
Array.Sort(numArray); //使用內置的快速排序
```
使用自己的冒泡排序(Array.Sort會比自已寫的快)
```c#
//使用自己的冒泡排序
for (int j = 1; j <= str.Length-1; j++) //外層for循環，用來控制內層for循環的次數
{
    for (int i = 0; i < numArray.Length - 1 -j+1; i++)
    {
        //numArray[i] numArray[i+1]做比較，大的放後面
        if (numArray[i + 1] < numArray[i])
        {
            int temp = numArray[i];
            numArray[i] = numArray[i + 1];
            numArray[i + 1] = temp;
        }
    }
}
//輸出排序結果
for (int i = 0; i < numArray.Length; i++) {
    Console.Write($"{numArray[i]} ");
}
Console.ReadKey();
```
內循環for的優化 **-j+1**，執行會更快
```c#
for (int i = 0; i < numArray.Length-1-j+1; i++)
```
為什麼要-j+1呢？隨著外循環的增加，內循環的次數是要減少的。
-j+1的優化，首先我們第一次做循環的時候，最大值已經放到最後一個索引值的位置了，第二次做循環的時候，就不需要再跟最後一個位置做比較，所以當第一次執行for循環的時候，內循環是要執行Length-1次，當第二次執行的時候，裡面只要執行Length-2次就可以了，所以隨著j的增加，裡面的執行的次數是要減少的。
- j=1, 內循環 -1+1，不影響。  
- j=2, 內循環 -2+1，相當於-1，比第一次減1了，就少執行一次，這一次相當於因為上一次循環已經把最大值排在最後面了，所以這一次我們就不需要跟最後一個值做比較了。

## 練習: 猴子吃桃
猴子吃桃問題：猴子第一天摘下若干個桃子，當即吃了一半，還不癮，又多吃了一個, 第二天早上又將剩下的桃子吃掉一半，又多吃了一個。以後每天早上都吃了前一天剩下的一半零一個。到第n天早上想再吃時，只剩下一個桃子了。求第一天共摘了多少。

```c#
//桃子減少公式：(n/2)-1=n
//桃子增加公式：(n+1)*2=n
int n = Convert.ToInt32(Console.ReadLine());
int count = 1; //剩下的桃子
for (int i = n-1; i >= 1; i--) { //使用負循環，往前走，走到第一天
    count = (count + 1) * 2;
}
Console.WriteLine($"第一天摘了{count}個桃子");
```
## 練習: 找出最小值，與索引為0的位置交換
輸入n個數，找出最小數，將它與最前面的數交換後，輸出這些數
```c#
//先將字串轉成整數
string str = Console.ReadLine()!;
string[] strArray = str.Split(' '); //分割字串
int[] numArray = new int[strArray.Length];
for (int i = 0; i < strArray.Length; i++) {
    numArray[i] = Convert.ToInt32(strArray[i]);
}

//找出最小數
int min = numArray[0];//最小數
int minIndex = 0;//記錄最小數的索引
for (int i = 1; i < numArray.Length; i++)
{
    if (numArray[i] < min) {
        minIndex = i;
        min = numArray[i];
    }
}

//最小值與第一個數(索引為0的位置)交換
int temp = numArray[0];
numArray[0] = min;
numArray[minIndex] = temp;

//輸出
for (int i = 0; i < numArray.Length; i++) {
    Console.Write($"{numArray[i]} ");
}
Console.ReadKey();
```
## 練習: 將一整數插入到已經從小到大排序好的序列中，其新的序列仍然有序
有n個整數，已經從小到大排序好，現在另外給新的整數x，請將該數插入序列中，並使新的序列仍然有序
```c#
int[] numArray = { 1, 2, 4, 5, 6, 9, 66 };
int num = Convert.ToInt32(Console.ReadLine());
int[] newNum = new int[numArray.Length + 1];
int index = 0; //臨時索引
bool isInsert = false; //被插入
for (int i = 0; i < newNum.Length; i++)
{
    //表示最大值且還沒被插入
    if (i == numArray.Length && isInsert == false) {
        newNum[i] = num; //插入
        isInsert = true;
        break;
    }
    if (num <= numArray[index] && isInsert == false)
    {
        newNum[i] = num; //插入
        isInsert = true;
    } 
    else
    {
        newNum[i] = numArray[index];
        index++;
    }
}
for (int i = 0; i < newNum.Length; i++) {
    Console.Write($"{newNum[i]} ");
}
Console.ReadKey();
```

## 練習: 計算發工資要準備多少鈔票及硬幣
```c#
int num = Convert.ToInt32(Console.ReadLine()); //輸入工資
int count1000 = num / 1000; //一千的
int remain = num % 1000; //剩餘的錢
int count500 = remain / 500;
remain = remain % 500;
int count100 = remain / 100;
remain = remain % 100;
int count50 = remain / 50;
remain = remain % 50;
int count10 = remain / 10;
remain = remain % 10;
int count5 = remain / 5;
remain = remain % 5;
Console.WriteLine($"1000準備{count1000}");
Console.WriteLine($"500準備{count500}");
Console.WriteLine($"100準備{count100}");
Console.WriteLine($"50準備{count50}");
Console.WriteLine($"10準備{count10}");
Console.WriteLine($"5準備{count5}");
Console.WriteLine($"1準備{remain}");
Console.ReadKey();
```

## 練習: 字串校驗: C#合法標識符
* 字首：允許a-z,A-Z,_,@，不允許0-9
* 後續字符：允許a-z,A-Z,_,0-9，不允許@
```c#
string str = Console.ReadLine()!;
bool isRight = true;
//字首：表示合法的大小寫字母、_底線、@符號
if ((str[0] >= 'a' && str[0] <= 'z') || (str[0] >= 'A' && str[0] <= 'Z' || str[0] == '_' || str[0] == '@')) {
   
} else {
    isRight = false;
}
//後續字符：允許a-z,A-Z,_,0-9
for (int i = 0; i < str.Length; i++)
{
    `if ((str[i] >= 'a' && str[i] <= 'z') || (str[i] >= 'A' && str[i] <= 'Z') ||
        (str[i] >= '0' && str[i] <= '9') || str[i] == '_') {

    } else {
        isRight = false;
    }
}

if(isRight==false)
    Console.WriteLine("不是合法的標識符");
else
    Console.WriteLine("是合法的標識符");
Console.ReadKey();
```

## 練習: 判斷字串是否"回文串"
"回文串"是一個正讀和反讀都一樣的字符串，比如 level 或者 noon 等等就是回文串。
```c#
string str = Console.ReadLine()!;
bool isPalindrome = true;
for (int i = 0; i < str.Length/2; i++) //從一半開始
{
    //i str.length-1-i 當前索引對應的另一個索引
    if (str[i] != str[str.Length - 1 - i]) {
        isPalindrome = false; break; //不是回文串，跳出迴圈
    }
}
if (isPalindrome) {
    Console.WriteLine("是回文串");
} else {
    Console.WriteLine("不是回文串");
}
Console.ReadKey();
```
## 練習: 設定安全密碼
1.長度>=8，不超過16
2.密碼字符應符合下面四組中的至少三組
1)大寫字母: A-Z
2)小寫字母: a-z
3)數字: 0-9
4)特殊符號:~!@#$%^
```c#
string str = Console.ReadLine()!;
if (str.Length >= 8 && str.Length <= 16) {
    bool isUpper = false;
    bool isLower = false;
    bool isHaveNumber = false;
    bool isHaveSpacial = false;

    for (int i = 0; i < str.Length; i++) {
        if (str[i] >= 'A' && str[i] <= 'Z') isUpper = true;
        if (str[i] >= 'a' && str[i] <= 'z') isLower = true;
        if (str[i] >= '0' && str[i] <= '9') isHaveNumber = true;
        if (str[i] == '~' || str[i] == '!' || str[i] == '@' || str[i] == '#' || 
            str[i] == '$' || str[i] == '%' || str[i] == '^') isHaveSpacial = true;
    }

    int count = 0;
    if (isUpper) count++;
    if (isLower) count++;
    if (isHaveNumber) count++;
    if (isHaveSpacial) count++;

    if (count >= 3) {
        Console.WriteLine("這是安全密碼");
    } else {
        Console.WriteLine("這密碼不安全");
    }
} else {
    Console.WriteLine("這密碼不安全，密碼長度不符合規則");
}
Console.ReadKey();
```

## 函數的定義與使用
為什麼使用函數呢？ 
1.如果想要重複執行某段代碼，就需要寫重複的代碼。
2.目前我們寫的代碼，寫多了，結構非常混亂，不容易閱讀。
所以函數來了，函數也叫做方法. 

定義函數(定義方法)
```c#
void Write() {
   //Todo
}
```
使用函數(調用方法)
```c#
Write();
```
函數定義的時候，參數叫形式參數(形參)
```c#
//num1,num2在這裡就是形參
static int Plus(int num1, int num2) { 
    int sum = num1 + num2;
    return sum;
}
```

當調用函數的時候，這裡傳遞的參數叫實際參數(實參)
實參的值會傳遞給形參做運算
```c#
int num1 = 2;
int num2 = 3;
int res1 = Plus(num1, num2); //num1,num2叫實際參數(實參)
int res2 = Plus(1, 2);
Console.WriteLine(res1);
Console.WriteLine(res2);
Console.ReadKey();
```
## 練習: 輸入一個數，找出該數字所有的因數
```c#
int number = Convert.ToInt32(Console.ReadLine()); //接收用戶輸入
int[] array = GetDivisor(number); //調用方法，找出該數字所有的因數
foreach (int temp in array) { //印出所有的因數
    Console.Write($"{temp} ");
}
Console.ReadKey();

//找出該數字所有的因數
static int[] GetDivisor(int number) {
    int count = 0; //記錄因子的個數
    for (int i = 1; i <= number; i++) { //計算因子的個數
        if (number % i == 0) count++;
    }

    //找出因子，並放入陣列中
    int[] array = new int[count]; //存放因子的陣列
    int index = 0; //記錄陣列的索引值
    for (int i = 1; i <= number; i++) {
        if (number % i == 0) { //表示因子
            array[index] = i; //因子放入陣列
            index++; //索引+1
        }
    }
    return array;
}
```
## 練習: 從一組陣列中找到最大值
定義一個函數，用來取得一個數組中的最大值
```c#
int[] array = { 324, 6546, 687, 9, 234, 46, 95276 };
Console.WriteLine(Max(array));
Console.ReadKey();

static int Max(int[] array) {
    int max = array[0]; //從索引0第一個值開始做比較
    for (int i = 1; i < array.Length; i++) { //i=1開始跟max做比較
        if (array[i] > max) max = array[i]; //依次跟max做比較
    }
    return max;
}
```
## 參數數組params

參數數組params 與數組參數的不同，在於函數的調用：
-參數數組params，可以傳遞任意多個參數，編譯器會自動幫我們自動組成一個數組。
-參數數組params，可以幫我們減少了一個創建數組的過程。
```c#
int sum2 = Plus(1, 2, 3, 4, 5); //編譯器會自動這些參數構造成一個數組
```
參數如果是數組參數，這個數組我們要自己手動去創建。
```c#
int sum1 = Sum(new int[] { 1, 2, 3, 4, 5 }); //需要自己構造一個數組
```

範例: 定義一個函數，用來取得數字的和，但是數字的個數不確定
解決方法：
1.定義一個函數，參數傳遞過來一個數組。
2.定義一個參數個數不確定的函式，這時候我們就要使用參數數組params。
```c#
int sum1 = Sum(new int[] {1, 2, 3, 4, 5 });  //需要自己構造一個數組
Console.WriteLine(sum1);
int sum2 = Plus(1, 2, 3, 4, 5); //編譯器會自動把這些參數構造成一個數組
Console.WriteLine(sum2);
Console.ReadKey();

//方法1:數組參數
static int Sum(int[] array) {
    int sum = 0;
	for (int i = 0; i < array.Length; i++) {
		sum += array[i];
	}
	return sum;
}

//方法2:參數數組params
//加上params，調用方法時，我們可以傳遞任意多個參數，編譯器會幫我們自動組成一個數組
static int Plus(params int[] array) {
    int sum = 0;
    for (int i = 0; i < array.Length; i++) {
        sum += array[i];
    }
    return sum;
}
```
## 結構函數的定義和使用
```c#
struct CustomerName {
    public string firstName;
    public string lastName;
    public string GetName() { //定義結構函數
        return $"My name is {firstName} {lastName}";
    }
}

CustomerName myName;
myName.firstName = "Rii";
myName.lastName = "La";
Console.WriteLine(myName.GetName()); //使用結構函數
```
## 練習: 結構函數
定義一個Vector3的類(這個類可以表示坐標、向量)，在裡面定義Distance方法，用來取得一個向量的長度
```c#
Vector3 myVector3;
myVector3.x = 3;
myVector3.y = 3;
myVector3.z = 3;
Console.WriteLine(myVector3.Distance());
Console.ReadKey();

struct Vector3 {
    //向量x,y,z
    public float x;
    public float y;
    public float z;
//向量的長度
    public double Distance() {
        return Math.Sqrt(x * x + y * y + z * z);
    }
}
```
## 函數的重載Overload
函數名相同，參數不同，這個叫做函數的重載(編譯器通過不同的參數去識別應該調用哪一個函式)

假如我們有一個函數用來實現求得一個數組的最大值
```c#
static int MaxValue(params int[] array) { ... } //處理int類型
```
上面這個函數只能用於處理int類型，如果想要處理double類型的話，就要再定義一個函數，如下：
```c#
static double MaxValue(params double[] array) { ... } //處理double類型
```

範例：
定義兩個名為MaxValue的函式，分別用來處理int和double類型
```c#
static int MaxValue(params int[] array)
{
    Console.WriteLine("int類型的MaxValue被調用");
    int maxValue = array[0]; //默認第一個值為最大值
    for (int i = 1; i < array.Length; i++) { //從第二個值開始依序與max做比較
        if (array[i] > maxValue) {
            maxValue = array[i];
        }
    }
    return maxValue;
}
```
```c#
static double MaxValue(params double[] array)
{
    Console.WriteLine("double類型的MaxValue被調用");
    double maxValue = array[0]; //默認第一個值為最大值
    for (int i = 1; i < array.Length; i++)  { //從第二個值開始依序與max做比較
        if (array[i] > maxValue)  {
            maxValue = array[i];
        }
    }
    return maxValue;
}
```
調用函式：編譯器會依據你傳遞過來的實參的類型去判定調用哪一個函數
```c#
int res1 = MaxValue(23, 4, 76, 8, 9); //編譯器會依據你傳遞過來的實參的類型去判定調用哪一個函數
double res2 = MaxValue(33.4, 76.22, 457.99);
Console.WriteLine(res1);
Console.WriteLine(res2);
Console.ReadKey();
```
## 委託(delegate)的定義
委託相當於聲明一個函數(指向一個函數)，指定了一個返回類型和參數列表
> 可以利用委託聲明的變量，去指向一個函數。

可以通過委託去宣告一個變數，這個變數它可以儲存一個函數(方法)，就是它可以指向一個函數。
> 目前我們定義的這些變數，都是儲存數據的：字串、整數、小數、數組…

**委託的使用分兩步：**
1.定義 
2.聲明(宣告變數)
結構體struct, 列舉enum同上使用，都分為定義和聲明

整數、數組、字串類型，都是直接聲明變量的，因為類型的定義已經完成了(CLR中已經完成定義)

舉例：
```c#
//定義delegate
//定義一個委託跟函數差不多，區別在於
//1.定義委託需要加上delegate關鍵字
//2.委託的定義不需要函數體
public delegate double MyDelegate(double param1, double param2);
class Program {
    static double Multiply(double param1, double param2) {
        return param1 * param2;
    }
    static double Divide(double param1, double param2) {
        return param1 / param2;
    }
    static void Main(int[] args) {
        MyDelegate de; //利用我們定義的委託類型聲明一個新的變量
        de = Multiply; //將de指向乘法函數。當我們給一個委託的變量賦值的時候，返回值與參數列表必須一樣，否則無法賦值
        Console.WriteLine(de(34.23, 66.99));
        de = Divide;
        Console.WriteLine(de(10.4,2.2));
        Console.ReadKey();
    }
}
```
## 練習: 函數的遞迴調用
一個函數調用它自身的時候，就叫做函數的遞迴調用。
> 函數調用自身，叫做遞迴調用

f(n)=f(n-1)+f(n-2), f(0)=2, f(1)=3, 求得f(40)
```c#
static int F(int n) {
    if (n == 0) return 2; //這兩個是函數終止遞迴的條件
    if (n == 1) return 3;
    return F(n - 1) + F(n - 2); //函數調用自身，叫做遞迴調用
}
```
```c#
int res = F(40);
Console.WriteLine(res);
int res2 = F(2);
Console.WriteLine(res2);
Console.ReadKey();
```
## 練習: 自由落體反彈距離與高度
一球從100米高度自由落下，每落地後反跳回原高度的一半，再落下，求它第10次落地時，共經過多少米？第10次反彈多高?
```c#
float height = 100; //目前高度
float distance = 0; //距離
for (int i = 0; i < 10; i++)
{
    distance += height; 
    height /= 2; //每落地後反跳回原高度的一半
}
Console.WriteLine($"經過了{distance}米，第10次反彈{height/2}米");
Console.ReadKey();
```
## 練習: 求1+2!+3!...+20!的和
```c#
//計算階乘
static int Factorial(int n) {
    int res = 1;
    for (int i = 1; i <= n; i++) {
        res *= i; //n=3, 1*2*3
    }
    return res;
}
//1~20階乘的和
int sum = 0;
for (int i = 1; i <= 20; i++) {
    sum += Factorial(i);
}
Console.WriteLine(sum);
Console.ReadKey();
```
## 練習: 利用遞迴求5!
公式：f(n) = n*f(n-1)
```c#
static int Factorial(int n) {
    if (n == 1) return 1; //終止遞迴的條件
    return n * Factorial(n - 1); //f(n) = n*f(n-1)
}
Console.WriteLine(Factorial(5));
Console.ReadKey();
```
## 練習: 結構體struct
編寫一個程序，定義結構類型(有學號、姓名、性別、成績)，聲明結構類型變量，用賦值語句對變量賦值後輸出。

```c#
struct Student {
    public string number;
    public string name;
    public bool isGirl;
    public int score;

    public void Show() {
        Console.WriteLine($"學號:{number}、姓名:{name}、性別:{(isGirl?"女":"男")}、成績:{score}");
    }
}
```
```c#
Student stu;
stu.number = "1002";
stu.name = "Rii";
stu.isGirl = true;
stu.score = 99;
stu.Show();
Console.ReadKey();
```
## 練習: 四捨五入
編寫一個程序，輸入一個正數，對該數進行四捨五入到個位數
```c#
double number = Convert.ToDouble(Console.ReadLine());
int numberInteger = (int)number / 1; //取得整數部分
double numberDouble = number - numberInteger; //取得小數部分
if (numberDouble >= 0.5f) {
	numberInteger++;
}
Console.WriteLine(numberInteger);
Console.ReadKey();
```
更簡單的做法：
直接加上0.5後，轉成整數，相當於把小數部分自動略掉
```c#
int res = (int)(number + 0.5f); //直接加上0.5，轉成整數，會自動略掉小數
```

