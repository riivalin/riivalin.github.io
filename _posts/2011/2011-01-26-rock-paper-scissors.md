---
layout: post
title: "[C# 筆記][WinForm] 剪刀石頭布"
date: 2011-01-25 21:09:00 +0800
categories: [Notes, C#]
tags: [C#,WinForm]
---

## 剪刀石頭布

Player 玩家
Computer 電腦(隨機)
Judge 裁判

## 分析
類別裡面要寫哪寫成員？    

比較字串可以嗎？可以，但要把所有的組合都寫進去，總共有三種拳套，如果今天又多了3種拳法，有6種拳套，要寫非常多個判斷就要不斷加，就廢了，不太好。


- 三種拳法：剪刀石頭布

```text
石頭1 剪刀2 布3  
- 玩家贏了：1-2=-1 2-3=-1 3-1=2  
- 平手：相減=0;
- 另一種情況：電腦贏了
```

- Player 玩家
    - 出拳方法
- Computer 電腦(隨機)
    - 出拳方法
- Judge 裁判
    - 判斷結果(playerNumber,computerNumber) 相減

## 實作

```c#
private void btnRock_Click(object sender, EventArgs e)
{
    string str = "石頭";
    PlayGame(str);
}

private void PlayGame(string str)
{
    lblPlayer.Text = str;

    //玩家出拳
    Player player = new Player();
    int playerNum = player.ShowFist(str);

    //電腦出拳
    Computer computer = new Computer();
    int computerNum = computer.ShowFist(); //隨機出拳，所以沒有參數
    lblComputer.Text = computer.Fist;

    //裁判
    Result result = Judge.JudgeResult(playerNum, computerNum);
    lblJudge.Text = result.ToString();
}

private void btnScissors_Click(object sender, EventArgs e)
{
    string str = "剪刀";
    PlayGame(str);
}

private void btnPaper_Click(object sender, EventArgs e)
{
    string str = "布";
    PlayGame(str);
}
```