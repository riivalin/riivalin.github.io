---
layout: post
title: "[C# 筆記] 密封類 vs 密封成員"
date: 2010-01-17 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,R,class]
---

## 密封類 vs 密封成員

- 使用 `sealed` 修飾符
- 防止類繼承、防止派生類重寫
- `sealed` 修飾符不僅可以用來修飾`class`，同樣也可以修飾類成員

它可以防止當前類被繼承，或者防止派生類(子類)在繼承過程中重寫某一個方法，`sealed` 修飾符不僅可以用來修飾`class`，同樣也可以修飾類的成員。        

- 如果`sealed`關鍵字使用在 `class` 上，這個類將無法被別人繼承
- 如果`sealed`關鍵字使用在成員方法上，方法將無法被重寫

例如：Circle類加上 `sealed`關鍵字，那麼這個Circle就不能被其他的 class 繼承了

```c#
public sealed class Circle:Shape {
    public void Draw() {
        ......
    }
}
```

或者，我們也可以把`sealed`修飾符加在它的方法上，這個時候其他的class 在繼承 circle的時候，就不可以重寫Draw這個方法了

```c#
public class Circle:Shape {
    public sealed void Draw() {
        ......
    }
}
```
所以，`sealed`修飾符跟`abstract`修飾符剛好相反。        

※ 套用至方法或屬性時，sealed 修飾詞必須一律與 override 搭配使用。
[MSDN - keywords/sealed](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/sealed)

## 為什麼要使用 sealed
TODO...

## 可以使用 sealed 的場景
- 靜態類
- 需要儲存敏感的數據
- 虛方法太多，重寫的代價過高的時候
- 追求性能提升 (???)

從來沒用過 sealed... XDDD

[https://www.bilibili.com/video/BV1Ss4y1B7zE?p=36](https://www.bilibili.com/video/BV1Ss4y1B7zE?p=36)