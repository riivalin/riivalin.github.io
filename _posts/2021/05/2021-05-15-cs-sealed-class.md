---
layout: post
title: "[C# 筆記] 密封類別(Sealed Class)"
date: 2021-05-15 23:49:00 +0800
categories: [Notes,C#]
tags: [C#,基礎語法,物件導向,OO,sealed]
---

## 什麼是密封類別(Sealed Class)?

`sealed`：密封類別，不允許被繼承，但能夠繼承其他的類別。      

「密封類別(`Sealed Class`)」其主要作用在於「限制某一類別不能夠被繼承(衍生)」，一旦類別宣告成「密封類別」，就表示該密封類別不可能有子類別(衍生類別)。


`Sealed` 密封類別，表示它不能被別人繼承，但它能去繼承別人。     

```c#
//Sealed 密封類別- 能被別人繼承，但他能去繼承別人
public sealed class Teacher : Person { 
}

//沒有東西可以讓你繼承，硬要繼承，就會報錯
//Error: 'Test': 無法衍生自密封類型 'Teacher'
public class Test: Teacher { 
}
```

若強制將某類別繼承 `Sealed` 密封類別 就會報錯「無法衍生自密封類型 」。

## sealed 不能被繼承

它有一個最大的特點，就是不能被繼承。

```c#
public sealed class Test { } //密封類別(Sealed Class)
public class Person : Test { } //報錯，密封類不能被繼承
```

Error 錯誤訊息：「'Person': 無法衍生自密封類型 'Test'」。

## sealed 可以繼承別人

但它可以繼承別人

```c#
//密封類別(Sealed Class)繼承 Person
public sealed class Test: Person { } 
public class Person { }
```

## 密封類別 vs 密封成員

- 使用 `sealed` 關鍵字  
- 防止類別繼承、防止派生類重寫  
- `sealed` 修飾符不僅可以用來修飾`class`，同樣也可以修飾類成員  

它可以防止當前的類別`Class`被繼承，或者防止衍生類(子類)在繼承過程中重寫某一個方法，`sealed` 修飾符不僅可以用來修飾`class`，同樣也可以修飾類的成員。

- 如果`sealed`關鍵字使用在 `class` 上，這個類別將**無法被別人繼承**。   
- 如果`sealed`關鍵字使用在 `Method`上，這個方法將**無法被重寫**。   


### 類別 Class 加上 sealed

如果`sealed`關鍵字使用在類別 `class` 上，這個類別將**無法被別人繼承**。         

例如：A類別 加上 sealed關鍵字，那麼這個A就不能被其他的 class 繼承了。       

```c#
sealed class A: B {} //密封類別A 可以繼承 B
class B {} //但類別B 不能繼承A，因為密封類別 不能被別人繼承
```

### 方法 Mthod 加上 sealed

如果`sealed`關鍵字使用在 `Method`上，這個方法將**無法被重寫**。     

或者，我們也可以把sealed修飾符加在方法上，這個時候其他的class 在繼承 該類別的時候，就不可以 重寫這個方法了

例如，三個類別 X、Y、Z，     
X 提供虛方法讓繼承它的類別可以重寫，Y繼承X後，我們把`sealed`修飾符加在它的 F()方法 上，這個時候`Z class` 在繼承 Y 的時候，就不可以重寫 F() 這個方法了。

```c#
class X
{   //虛方法
    protected virtual void F() { Console.WriteLine("X.F"); }
    protected virtual void F2() { Console.WriteLine("X.F2"); }
}

class Y : X
{   //重寫虛方法，F() 方法加上 sealed 密封，不讓它被重寫
    sealed protected override void F() { Console.WriteLine("Y.F"); }
    protected override void F2() { Console.WriteLine("Y.F2"); }
}

class Z : Y
{   // 硬要重寫 F()方法，就會報錯
    // 錯誤訊息：無法重寫繼承的成員 F()，因為已密封
    // Attempting to override F causes compiler error CS0239.
    // protected override void F() { Console.WriteLine("Z.F"); }

    // Overriding F2 is allowed.
    protected override void F2() { Console.WriteLine("Z.F2"); }
}
```

類別Y，將F()方法加上sealed 密封，不讓它被重寫，     
類別Z 繼承Y，如果硬要重寫 F()方法，就會報錯，       
錯誤訊息：無法重寫繼承的成員 F()，因為已密封        
(Attempting to override F causes compiler error CS0239.)


> 所以，`sealed`修飾符跟`abstract`修飾符剛好相反。      
> ※ 套用至方法或屬性時，`sealed` 修飾詞必須一律與 `override` 搭配使用。


## 可以使用 sealed 的場景
- 當你確定某些類別不會有子類別(衍生類別)，就可以宣告成密封類別。(執行時期時，能得到較佳的執行效能)
- 靜態類
- 需要儲存敏感的數據
- 虛方法太多，重寫的代價過高的時候
- 追求性能提升 (???)

[MSDN - sealed (C# 參考)](https://learn.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/sealed)      
[[C# 筆記] Sealed 密封類別   by R](https://riivalin.github.io/posts/2011/01/sealed/)        
[[C# 筆記] Sealed 密封類 vs 密封成員   by R](https://riivalin.github.io/posts/2010/01/r-csharp-note-17/)        
[[C# 筆記] 關鍵字 by R](https://riivalin.github.io/posts/2011/02/keyword-1/#sealed-密封類)