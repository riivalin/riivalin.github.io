---
layout: post
title: "[C# 筆記] 多重繼承 vs 多重實現"
date: 2010-01-19 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class,interface,多型]
---

## C# 多重繼承?

C# 不能多重繼承，但可以通過接口(介面)來模仿多重繼承的實現方式，同時還可以避免多重繼承產生的問題。

```c#
public class UIText : UIBase, IDragable, ICopyable
{

}
public interface IDragable
{
    void Drag();
}
public interface ICopyable
{
    void Copy();
    void Paste();
}
public class UIBase
{
    public int Size { get; set; }
    public int Position { get; set; }
    public void Draw()
    {
    }
}
```
從上可以看出，對於接口(介面)來說，根本就不存在繼承關係，只有實現和被實現的關係。        

所以在 `UIText`中，可以複用的、有繼承關係的只有一個，也就是`UIBase` 這個類。

```c#
public class UIText : UIBase, IDragable, ICopyable
{
    public void Copy()
    {
        throw new NotImplementedException();
    }

    public void Drag()
    {
        throw new NotImplementedException();
    }

    public void Paste()
    {
        throw new NotImplementedException();
    }
}
public interface IDragable
{
    void Drag();
}
public interface ICopyable
{
    void Copy();
    void Paste();
}
public class UIBase
{
    public int Size { get; set; }
    public int Position { get; set; }
    public void Draw()
    {
    }
}
```

## 使用接口(介面)的目的
使用接口(介面)的目的，根本不是為了程式碼的複用，而是為了構建一個「低耦合」、「可擴展」、「可測試」的模塊化的系統接口(介面)。        

接口(介面)雖然不能帶來多重繼承，但是通過接口(介面)，我們卻可以實現類的多態(多型)的現象。


[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=44](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=44)