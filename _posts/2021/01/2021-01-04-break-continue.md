---
layout: post
title: "[C# 筆記] break、continue"
date: 2021-01-04 00:18:00 +0800
categories: [Notes, C#]
tags: [C#,break,continue]
---

`break`
- break 用於迴圈與switch語句當中。
- break 只能夠結束當前所屬的語句範圍。(結束掉離它最近的迴圈)

`continue`
- continue 用於迴圈中，可以跳過當前迴圈程式碼，強制開始下一次迴圈的執行。
- continue 通常用於「過濾」一些不需要執行的本次迴圈邏輯的情況；就是將異類鏟除掉。
> continue 用於結束本次的迴圈內容，開始下一次的迴圈。   
> (直接終結掉這次的迴圈，不執行下面的程式碼(下面的語句就不會執行了)，跳到 i++，開始下一個迴圈)

***

## break
- break 用於迴圈與switch語句當中。
- break 只能夠結束當前所屬的語句範圍。(結束掉離它最近的迴圈)

案例一：  
模擬用戶輸入密碼匹配的過程，如果不匹配則輸出「密碼錯誤，重新輸入」，如果匹配則輸入「登入成功」，如果連續三次輸入錯誤，則輸出「登入失敗」並退出程式。

分析：
- 定義字串變數password，作為正確密碼匹配。
- 定義字串變數input，作為用戶輸入的密碼。
- 使用for迴圈，循環次數為三次。
- 迴圈內，接受用戶鍵盤輸入，判斷密碼是否正確
    - 如果是，則登入成功
    - 如果不是，則繼續輸入直到迴圈結束

```c#
//1.定義正確密碼
const string password = "123";

//2.定義用戶輸入的密碼
string input;

//3.開始匹配
for (int i = 1; i <= 3; i++) 
{
    //3.1 用戶輸入密碼
    Console.WriteLine("請輸入密碼：");
    input = Console.ReadLine()!;

    //3.2 與正確密碼比對，正確則登入成功，否則登入失敗，再重新輸入
    if (input == password) {
        Console.WriteLine("登入成功");
        break; //結束掉離它最近的迴圈
    } else {
        Console.WriteLine("登入失敗，請重新輸入");
    }
}
Console.WriteLine("程式結束");
Console.Read();
```

範例：  
模擬Game《女孩心思你得猜》，女友發燒39度，請問你要說什麼?  
1.多喝熱開水啊  2.燒這麼歷害啊  3.開門吧，給你拿藥囉  4.不玩了  
1,2會被打了一頓，3獲得愛情，4結束遊戲

```c#
bool quit = false; //代表用戶是否想退出
for (; ; ) //無窮迴圈(死循環)
{ 
    Console.WriteLine("女友發燒39度，請問你要說什麼?");
    Console.WriteLine("1.多喝熱開水啊");
    Console.WriteLine("2.燒這麼歷害啊");
    Console.WriteLine("3.開門吧，給你拿藥囉");
    Console.WriteLine("4.不玩了");
    int input = Convert.ToInt32(Console.ReadLine());

    switch (input)
	{
		case 1:
        case 2:
            Console.WriteLine("被打了一頓");
            break;
        case 3:
            Console.WriteLine("女孩說：愛死你了");
            break;
        case 4:
            quit = true;
            break; //switch裡的break，是用來終結掉switch語句
	}
    if (quit) break; //這裡的break，會結束掉for迴圈
}
Console.WriteLine("遊戲結束");
Console.Read();
```
- 在switch中的break，是用來終結掉switch語句的，因為它是在switch大括弧之間。
- `if (quit) break;`，它當前所在的是`for迴圈`之間(是在for迴圈裡面)，所以它是用來終結掉for循環的。

## continue

- continue 用於迴圈中，可以跳過當前迴圈程式碼，強制開始下一次迴圈的執行。
- continue 通常用於「過濾」一些不需要執行的本次迴圈邏輯的情況；就是將異類鏟除掉

範例二：
計算1-100之間的偶數和。(也就是每次迴圈過程中，要過濾掉奇數)

```c#
int sum = 0;
for (int i = 1; i <= 100; i++)
{
    //continue直接終結掉這次的迴圈，下面的程式碼就不會執行了，跳到i++開始下一個迴圈
    if (i % 2 != 0) continue; //如果是奇數，直接過濾掉
    sum += i;
}
Console.WriteLine(sum);
Console.Read();
```
> continue 用於結束本次的迴圈內容，直開始下一次的迴圈。   
> (直接終結掉這次的迴圈，不執行下面的程式碼(下面的語句就不會執行了)，跳到 i++，開始下一個迴圈)

## 總結 break, continue

break與continue都代表什麼含義?
- `break` 強制結束所在的迴圈語句(能夠結束離它最近的迴圈)，或是結束所在的switch語句。
- `continue` 強制結束當前迴圈的過程，開始下一次的迴圈。