---
layout: post
title: "[閱讀筆記][Design Pattern] Ch19.組合模式(Composite)"
date: 2009-03-19 06:01:00 +0800
categories: [Notes,Design Pattern]
tags: [大話設計模式,Design Pattern]
---

# 組合模式(Composite)

組合模式(Composite)，將物件組合成樹形結構以表示「部分-整體」的層次結構。組合模式(Composite)使得用戶對單個物件和組合物件的使用具有一致性。

## 結構

- `Component` 組合中的物件宣告介面，在適當情況下，實現所有類別共有介面的預設行為。宣告一個介面用於使用和管理`Component`的子部分。
- `Leaf` 在組合中表示葉節點物件，葉節點沒有子節點。
- `Composite` 定義有枝節點行為，用來儲存子部分，在`Component`介面中實現與子部分有關的操作。例如，增加`Add`和刪除`Remove`。

```
Client

    Component 組合中的物件宣告介面，在適當情況下，實現所有類別共有介面的預設行為。宣告一個介面用於使用和管理Component的子部分
    + Add(in c: Component)
    + Remove(in c: Component)
    + Display(in depth: int)

        Leaf 在組合中表示葉節點物件，葉節點沒有子節點
        + Display(in depth: int)

        Composite 定義有枝節點行為，用來儲存子部分，在Component介面中實現與子部分有關的操作。例如，增加Add和刪除Remove
        + Add(in c: Component)
        + Remove(in c: Component)
        + Display(in depth: int)
```

## 程式碼

### Component
`Component` 組合中的物件宣告介面，在適當情況下，實現所有類別共有介面的預設行為。宣告一個介面用於使用和管理Component的子部分。

```c#
abstract class Component {
    protected string name;
    public Component(string name) {
        this.name = name;
    }
    //通當都用Add和Remove方法來提供增加或移除樹葉或樹葉的功能
    public abstract void Add(Component c);
    public abstract void Remove(Component c);
    public abstract void Display(int depth);
}
```

### Leaf
`Leaf` 在組合中表示葉節點物件，葉節點沒有子節點。

```c#
class Leaf: Component {
    public Leaf(string name): base(name) { }

    //由於葉子沒有再增加分枝和樹葉，所以Add和Rmove方法實現它沒有意義，但這樣做可以消除葉節點和枝節點物件在抽象層次的區別，它們具備完全一致的介面
    public override void Add(Component c) {
        Console.WriteLine("Cannot add to a leaf");
    }
    public override void Remove(Component c) {
        Console.WriteLine("Cannot remove from a leaf");
    }

    //葉節點的具體方法，此處是顯示其名稱和級別
    public override void Display(int depth = 1) {
        Console.WriteLine($"{new String('-', depth)} {name}");
    }
}
```

### Composite
`Composite` 定義有枝節點行為，用來儲存子部分，在`Component`介面中實現與子部分有關的操作。例如，增加`Add`和刪除`Remove`。

```c#
class Composite: Component {
    //一個子物件集合，用來儲存其下屬的枝節點和葉節點
    List<Component> children = new List<Component>();

    public Composite(string name): base(name) {  }

    //通當都用Add和Remove方法來提供增加或移除樹葉或樹葉的功能
    public override void Add(Component c) {
        children.Add(c);
    }
    public override void Remove(Component c) {
        children.Remove(c);
    }
    public override void Display(int depth = 1) {
        Console.WriteLine($"{new String('-', depth)} {name}");
        foreach(Component c in children) {
            c.Display(depth + 2);
        }
    }
}
```

### 用戶端程式碼

```c#
public static void Main()
{
    //產生根節點root, 根上長出兩葉leafA, leafB
    Composite root = new Composite("root");
    root.Add(new Leaf("LeafA"));
    root.Add(new Leaf("LeafB"));

    //根上長出分支compsiteX, 分支上也有兩葉leafXA, leafXB
    Composite comp1 = new Composite("Composite X");
    comp1.Add(new Leaf("Leaf X-A"));
    comp1.Add(new Leaf("Leaf X-B"));

    //將compositeX加入根節點
    root.Add(comp1);

    //compsiteX上再長出分支compsiteXY, 分支上也有兩葉leafXYA, leafXYB
    Composite comp2 = new Composite("composite XY");
    comp2.Add(new Leaf("Leaf XY-A"));
    comp2.Add(new Leaf("Leaf XY-B"));

    //將compositeXY加入compositeX
    comp1.Add(comp2);

    //根節點再長出兩葉leafC, leafD
    root.Add(new Leaf("LeafC"));
    Leaf leaf = new Leaf("LeafD");
    root.Add(leaf);

    //LeafD被風吹走了
    root.Remove(leaf);

    //顯示樹狀圖
    root.Display();	
}

/* 執行結果:
- root
--- LeafA
--- LeafB
--- Composite X
----- Leaf X-A
----- Leaf X-B
----- composite XY
------- Leaf XY-A
------- Leaf XY-B
--- LeafC
*/
```

## 透明方式與安全方式

### 透明方式
樹葉不可以再長分枝，但`Leaf` 類別當中也有`Add`和 `Remove`方法，這就叫做「透明方式」。       

也就是說，在`Component`中宣告所有用來管理子物件的方法，其中包括`Add`、`Remove`等。這樣實現`Component`介面的所有子類別都具備了`Add`、`Remove`。      

這樣做的好處就是，葉節點和枝節點對於外界沒有區別，它們具備完全一致的行為介面。        

但問題也很明顯，因為 `Leaf`類別本身不具備`Add()`、`Remove()`方法的功能，所以實現它是沒有意義的。    

### 安全方式

在`Component`介面中不去宣告`Add`、`Remove`方法，那麼子類別的 `Leaf`也就不需要去實現它，而是在`Composite`宣告所有用來管理子類別物件的方法，這樣做就不會出現剛的問題，但由於不透明，所以葉節點和枝節點將不具有相同的介面，用戶端的調用需要做相應的判斷，帶來了不便。


## 何時使用？
當你發現需求中是表現部分與整體層次的結構時，以及你希望用戶可以忽略組合物件與單個物件的不同，統一地使用組合結構中的所有物件時，就應該考慮組合模式了。

> ASP.NET中的TreeView控制元件就是典型的組合模式應用，事實上，所有的Web控制元的基礎類別都是System.Web.UI.Control，而Control 基礎類別中就有Add和Remove方法，這就是典型的組合模式的應用。

## 好處
- 組合模式定義了「基本物件」和「組合物件」的類別層次結構
- 「基本物件」可被組合成更複雜的「組合物件」，而這個「組合物件」又可以被組合，只要不斷的遞迴下去，程式碼中任何用到基本物件的地方都可以使用組合物件。
- 用戶不用關心到底是處理一個葉節點，還是處理一個組合元件，也就用不著為定義組合而寫一些選擇判斷敘述了。

> 簡單說，「組合模式」就是讓客戶可以一致地使用組合結構和單個物件。

# 分公司/部門

## 結構

```
公司
+ 增加(in c: 公司)
+ 移除(in c: 公司)
+ 顯示(in depth: int)
+ 履行職責()

    財務部
    + 增加(in c: 公司)
    + 移除(in c: 公司)
    + 顯示(in depth: int)
    + 履行職責()

    人資部
    + 增加(in c: 公司)
    + 移除(in c: 公司)
    + 顯示(in depth: int)
    + 履行職責()

    具體公司
    + 增加(in c: 公司)
    + 移除(in c: 公司)
    + 顯示(in depth: int)
    + 履行職責()
```

## 程式碼

```c#
//公司 (介面或抽象類別)
abstract class Company {
    protected string name;
    public Company(string name) {
        this.name = name;
    }
    public abstract void Add(Company c); //增加
    public abstract void Remove(Company c); //移除
    public abstract void Display(int depth=1); //顯示
    public abstract void LineOfDuty(); //履行職責
}

//具體公司 (實現介面、樹枝節點)
class ConcreteCompany: Company  {
    List<Company> children = new List<Company>();
    public ConcreteCompany(string name): base(name) { }
    
    //增加
    public override void Add(Company c) {
        children.Add(c);
    }

    //移除
    public override void Remove(Company c) {
        children.Remove(c);
    }

    //顯示
    public override void Display(int depth=1) {
        Console.WriteLine($"{new String('-', depth)} {name}");
        foreach(Company c in children) {
            c.Display(depth+2);
        }
    }

    //履行職責
    public override void LineOfDuty() {
        foreach(Company c in children) {
            c.LineOfDuty();
        }
    }
}

//人資部 (樹葉節點)
class HRDepartment: Company {
    public HRDepartment(string name): base(name) { }
    public override void Add(Company c) { } //增加
    public override void Remove(Company c) { } //移除

    //顯示
    public override void Display(int depth=1) { 
        Console.WriteLine($"{new String('-',depth)} {name}");
    } 

    //履行職責
    public override void LineOfDuty() {
        Console.WriteLine($"{name} 員工招聘教育訓練");
    } 
}

//財務部 (樹葉節點)
class FinanceDepartment: Company {
    public FinanceDepartment(string name): base(name) { }
    public override void Add(Company c) { } //增加
    public override void Remove(Company c) { } //移除
    
    //顯示
    public override void Display(int depth=1) { 
        Console.WriteLine($"{new String('-',depth)} {name}");
    } 
    //履行職責
    public override void LineOfDuty() {
        Console.WriteLine($"{name} 公司財務收支管理");
    } 
}

//用戶端
public static void Main()
{
    Console.WriteLine("結構圖:");
    Company root = new ConcreteCompany("台北總公司");
    root.Add(new HRDepartment("總公司-人資部"));
    root.Add(new FinanceDepartment("總公司-財務部"));

    Company comp1 = new ConcreteCompany("台中分公司");
    comp1.Add(new HRDepartment("台中分公司-人資部"));
    comp1.Add(new FinanceDepartment("台中分公司-財務部"));

    root.Add(comp1);

    Company comp2 = new ConcreteCompany("高雄分公司");
    comp2.Add(new HRDepartment("高雄分公司-人資部"));
    comp2.Add(new FinanceDepartment("高雄分公司-財務部"));

    root.Add(comp2);
    root.Display();

    Console.WriteLine("職責:");
    root.LineOfDuty();
}
/* 執行結果:

- 台北總公司
--- 總公司-人資部
--- 總公司-財務部
--- 台中分公司
----- 台中分公司-人資部
----- 台中分公司-財務部
--- 高雄分公司
----- 高雄分公司-人資部
----- 高雄分公司-財務部

職責:
總公司-人資部 員工招聘教育訓練
總公司-財務部 公司財務收支管理 
台中分公司-人資部 員工招聘教育訓練
台中分公司-財務部 公司財務收支管理 
高雄分公司-人資部 員工招聘教育訓練
高雄分公司-財務部 公司財務收支管理 

*/
```