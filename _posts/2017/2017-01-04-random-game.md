---
layout: post
title: "[C# 筆記] 猜數字遊戲"
date: 2017-01-04 23:59:00 +0800
categories: [Notes, C#]
tags: [C#]
---

遊戲流程：
- 系統產生一個隨機數，作為猜測目標
- 輸出：請輸入猜測數字(下限-上限)
- 接受用戶輸入的數字，並判斷：
    - 如果與目標匹配，則輸出「恭喜猜對了」
    - 如果與目標大，則輸出「猜的大了」
    - 如果與目標小，則輸出「猜的小了」

遊戲規則：
- 用戶共有10次猜數字的機會，超過十次則輸出「遊戲失敗，是否重來？」
- 用戶如果猜成功後，則輸出「遊戲過關，是否重來？」
- 用戶輸入1則表示重來，輸入0則表示退出
- 如果用戶選擇重來，則清理之前所有的輸出；如果用戶選擇退出，則直接退出


```c#
//1.產生一個隨機數作為目標數字
//----需要限制隨機數的上下限，使用變數
int lowerBound = 10; //下限
int upperBound = 20; //上限

Random random = new Random();
int targetNumber = random.Next(lowerBound, upperBound);

Console.WriteLine($"產生了一個隨機數，範圍是{lowerBound}到{upperBound}之間，請猜測");

while (true) //無窮迴圈(死循環)
{
    //用戶共有10次猜數字的機會
    for (int i = 0; i < 10; i++)
    {
        //2.接收用戶輸入的一個數字
        int input = Convert.ToInt32(Console.ReadLine());

        //3.比對用戶輸入與目標數字之間的關係
        //3.1 如果匹配，猜測成功
        //3.2 如果更大，輸出數字太大
        //3.3 如果更小，輸出數字太小
        if (input == targetNumber)
        {
            Console.WriteLine("恭喜猜對了");
            //!!!!用戶猜對了，直接終結當前迴圈
            break;

        } else if (input > targetNumber)
        {
            Console.WriteLine("數字太大");
        } else
        {
            Console.WriteLine("數字太小");
        }
        Console.WriteLine("請繼續猜測");
    }

    Console.WriteLine("*****是否重來?*****");
    Console.WriteLine("1 重來    0 退出");

    //拿到用戶輸入的選項，判斷是否重來
    int playAgain = Convert.ToInt32(Console.ReadLine());
    if (playAgain == 1)
    {
        targetNumber = random.Next(lowerBound, upperBound); //重新產生隨機數
        Console.Clear();//清除之前所有的輸出
        Console.WriteLine($"產生了一個隨機數，範圍是{lowerBound}到{upperBound}之間，請猜測");
    } else if (playAgain == 0) {
        break;
    }
}
Console.WriteLine("遊戲退出");
Console.Read();
```