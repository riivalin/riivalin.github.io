---
layout: post
title: "[C# 筆記][WinForm] 播放音樂"
date: 2011-01-27 21:49:00 +0800
categories: [Notes, C#]
tags: [C#]
---

```c#
//儲存音樂文件的全路徑的泛型集合
List<string> listSongs = new List<string>();
private void btnOpen_Click(object sender, EventArgs e)
{
    OpenFileDialog ofd = new OpenFileDialog();
    ofd.Title = "請選擇音樂文件";
    ofd.InitialDirectory = @"C:\Users\rivalin\Desktop\images";
    ofd.Multiselect = true;
    ofd.Filter = "音樂文件|*.wav|所有文件|*.*";
    ofd.ShowDialog();

    //獲得我們在文件夾中選擇所有文件的全路徑
    string[] path = ofd.FileNames;
    for (int i = 0; i < path.Length; i++)
    {
        //將音樂文件的文件名
        listBox1.Items.Add(Path.GetFileName(path[i]));
        //將音樂文件的全部路徑存到泛型集合中
        listSongs.Add(path[i]);
    }
}


/// <summary>
/// 點擊上一曲
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnPrevious_Click(object sender, EventArgs e)
{
    //獲得當前選中歌曲的index
    int index = listBox1.SelectedIndex;
    index++;
    //如果index己經是列表中的個數，代表己經超過列表，就從第一個開始，index設0
    if (index == listBox1.Items.Count) index = 0;

    //將改變後的索引重新賦值給我當前選中的索引
    listBox1.SelectedIndex = index;

    //播放音樂
    sp.SoundLocation = listSongs[index];
    sp.Play();
}
/// <summary>
/// 點擊下一曲
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
private void btnNext_Click(object sender, EventArgs e)
{
    int index = listBox1.SelectedIndex;
    index--;
    if (index < 0) index = listBox1.Items.Count - 1;
    //將改變後的索引重新賦值給我當前選中的索引
    listBox1.SelectedIndex = index;
    sp.SoundLocation = listSongs[index];
    sp.Play();
}
/// <summary>
/// 雙擊播放
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
SoundPlayer sp = new SoundPlayer(); //播放音樂的物件
private void listBox1_DoubleClick(object sender, EventArgs e)
{
    sp.SoundLocation = Path.GetFileName(listSongs[listBox1.SelectedIndex]);
    sp.Play();
}
```