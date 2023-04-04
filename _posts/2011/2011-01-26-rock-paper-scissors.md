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
### Form1.cs
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

### Player.cs

```c#
/// <summary>
/// 玩家出拳
/// </summary>
/// <param name="fist">拳法：剪刀石頭布</param>
/// <returns>返回一個出拳的數字</returns>
public int ShowFist(string fist)
{
    int num = 0;
    switch (fist)
    {
        case "石頭":
            num = 1;
            break;
        case "剪刀":
            num = 2;
            break;
        case "布":
            num = 3;
            break;
    }
    return num;
}
```

### Computer.cs

```c#
internal class Computer
{
    /// <summary>
    /// 儲存電腦出的拳頭
    /// </summary>
    public string Fist { get; set; } //自動屬性
    public int ShowFist()
    {
        Random r = new Random(); //建立產生隨機數物件
        int num = r.Next(1, 4); //1-3
        switch (num)
        {
            case 1:
                this.Fist = "石頭";
                break;
            case 2:
                this.Fist = "剪刀";
                break;
            case 3:
                this.Fist = "布";
                break;
        }
        return num;
    }
}
```

### Judge.cs

```c#
public enum Result
{
    玩家贏, 電腦贏, 平手
}
/// <summary>
/// 裁判
/// </summary>
internal class Judge
{
    public static Result JudgeResult(int playerNumber, int computerNumber)
    {
        if (playerNumber - computerNumber == -1 || playerNumber - computerNumber == 2)
        {
            return Result.玩家贏;
        } else if (playerNumber - computerNumber == 0)
        {
            return Result.平手;
        } else
        {
            return Result.電腦贏;
        }
    }
}
```