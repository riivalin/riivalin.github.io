---
layout: post
title: "[C# 筆記] 物件導向 複習結束" 
date: 2011-02-13 00:01:49 +0800
categories: [Notes,C#]
tags: [C#,virtual,abstract,interface,多型,OO,物件導向]
---

## 練習1：物件導向
定義父親類Father(姓lastName, 財產property, 血型bloodType)   
兒子類Son(玩遊戲PlayGame方法)，女兒類Daughter(跳舞Dance方法)    
調用父類構造函數(:base())給子類字段賦值     

```c#
//定義父親類Father(姓lastName, 財產property, 血型bloodType)
//兒子類Son(玩遊戲PlayGame方法)，女兒類Daughter(跳舞Dance方法)
//調用父類構造函數(:base())給子類字段賦值
Son son = new Son("張三", 1000, "AB");
son.PlayGame();
son.SayHello();
Dauther dauther = new Dauther("小月",26000,"B");
dauther.Dance();
dauther.SayHello();
Console.ReadKey();

public class Father
{
    public string lastName { get; set; }
    public decimal Property { get; set; }
    public string BloodType { get; set; }

    public Father(string name, decimal property, string bloodType)
    {
        this.lastName = name;
        this.Property = property;
        this.BloodType = bloodType;
    }
    public void SayHello() {
        Console.WriteLine($"我叫{this.lastName}，我有{this.Property}元，血型是{this.BloodType}");
    }
}
public class Son : Father
{
    public Son(string lastName, decimal property, string bloodType)
        : base(lastName, property, bloodType)
    {
    }
    public void PlayGame() {
        Console.WriteLine("兒子會玩遊戲");
    }
}
public class Dauther : Father
{
    public Dauther(string lastName, decimal property, string bloodType)
        : base(lastName, property, bloodType)
    {
    }
    public void Dance() {
        Console.WriteLine("女兒會玩跳舞");
    }
}
``` 
## 練習2：物件導向
定義汽車類Vehicle，屬性：brand(品牌)、color(顏色)、方法run     
子類卡車(Truck)，屬性：weight載重、方法:拉貨  
轎車(Car)：屬性：passenger載客數量、方法:載客量     

```c#
//定義汽車類Vehicle，屬性：brand(品牌)、color顏色)、方法run
//子類卡車(Truck)，屬性：weight載重、方法:拉貨
//轎車(Car)：屬性：passenger載客數量、方法:載客
Truck truck = new Truck("BMW", "黑色", 5000);
Car car = new Car("Benz", "寶藍", 5);

truck.PullGoods();
car.CarryPassenger();
Console.ReadKey();

/// <summary>
/// 汽車的父類
/// </summary>
public class Vehicle
{
    public string Brank { get; set; }
    public string Color { get; set; }

    public Vehicle(string brank, string color)
    {
        this.Brank = brank;
        this.Color = color;
    }
    public void Run()
    {
        Console.WriteLine("我是汽車，我會跑");
    }
}
public class Truck : Vehicle
{
    public decimal Weight { get; set; }

    public Truck(string brank, string color, decimal weight)
        : base(brank, color)
    {
        this.Weight = weight;
    }
    public void PullGoods()
    {
        Console.WriteLine($"我最多可以拉{this.Weight}kg貨物");
    }
}
public class Car : Vehicle
{
    public int Passenger { get; set; }

    public Car(string brank, string color, int passenger)
        : base(brank, color)
    {
        this.Passenger = passenger;
    }
    public void CarryPassenger()
    {
        Console.WriteLine($"我最多可以載{this.Passenger}人");
    }
}
```
## 練習3：物件導向-多型(virtual)
員工類、部目經理類、程式人員類  
(部門經理也是員工，所以要繼承自員工類。員工有上班打卡的方法。用類來模擬)        

- 員工有上班打卡的方法。用類來模擬 => 要實現多態(多型)
- 在實現「多態(多型)」的時候，一般都是聲明父類指向子類

```c#
//員工類、部門經理類、程式員類
//(部門經理也是員工，所以要繼承自員工類。員工有上班打卡的方法。用類來模擬=>實現多態)

//在實現多型的時候，一般都是聲明父類指向子類
Employee emp = new Employee(); //創建員工自己的物件
emp.ClockIn(); //員工打卡

Employee manager = new Manager();//創建經理的物件
manager.ClockIn(); //經理打卡

Employee programmer = new Programmer();//程式員的物件
programmer.ClockIn(); //程式員打卡

//員工類別
public class Employee
{
    //部門經理、程式員類都要繼承這個類，但這個方法有意義，所以用virtual虛方法
    public virtual void ClockIn()
    {
        Console.WriteLine("員工九點打卡");
    }
}
//部門經理類別
public class Manager : Employee
{
    public override void ClockIn()
    {
        Console.WriteLine("經理11點打卡");
    }
}
//程式員類
public class Programmer : Employee
{
    public override void ClockIn()
    {
        Console.WriteLine("程式員不打卡");
    }
}
```
## 練習4：物件導向-多型(abstract)
動物Animal，都有Eat和Bark的方法，狗Dog和貓Cat叫的方法不一樣，
父類中沒有默認的實現，所以考慮用抽象方法

- 實現多態的話，還是一樣聲明父類指向子類，但不能創建Animal物件(因為他是抽象類)
- 調的是父類的，本質上調的是子類的方法

```c#
//動物Animal，都有Eat和Bark的方法，狗Dog和貓Cat叫的方法不一樣，
//父類中沒有默認的實現，所以考慮用抽象方法

Animal dog = new Dog();
dog.Eat();
dog.Bark();

Animal cat = new Cat();
cat.Eat();
cat.Bark();

Console.ReadKey();

//抽象類的父類
public abstract class Animal
{
    public abstract void Eat();
    public abstract void Bark();
}
public class Dog : Animal
{
    public override void Eat()
    {
        Console.WriteLine("小狗咬著吃");
    }
    public override void Bark()
    {
        Console.WriteLine("小狗汪汪叫");
    }
}
public class Cat : Animal
{
    public override void Eat()
    {
        Console.WriteLine("小貓舔著吃");
    }
    public override void Bark()
    {
        Console.WriteLine("貓咪喵喵叫");
    }
}
```

## 練習5：物件導向-多型(interface)
鳥-麻雀 sparrow, 鴕鳥 ostrich,  
企鵝 pengui, 鸚鵡 parrot    
鳥能飛，鴕鳥、企鵝不能，你怎麼辦？  

- 不是所有的鳥都會飛，所以「飛」寫成`interface`介面
- 如果同時要繼承鳥類跟介面：鳥類要寫成前面，介面寫後面
- 要實現多態(多型)的話，要聲明`interface`介面去指向子類對象(物件)，現在只能指向誰呀？麻雀、鸚鵡，只有他們會飛

```c#
//鳥 - 麻雀 sparrow, 鴕鳥 ostrich,
//企鵝 pengui, 鸚鵡 parrot
//鳥能飛，鴕鳥、企鵝不能，你怎麼辦？

IFlyable parrot = new Parrot(); //鸚鵡
parrot.Fly();
IFlyable sparrow = new Sparrow();//麻雀
sparrow.Fly();

Console.ReadKey();

public class Bird
{
    //鳥都有一對翅膀
    public double Wings { get; set; }

    public void SayHello() {
        Console.WriteLine("我是小鳥");
    }
}

public class Pengui : Bird
{
}
public class Ostrich : Bird
{
}

public class Sparrow : Bird, IFlyable
{
    public void Fly()
    {
        Console.WriteLine("麻雀會飛");
    }
}
public class Parrot : Bird, IFlyable
{
    public void Fly()
    {
        Console.WriteLine("鸚鵡會飛");
    }
}

public interface IFlyable
{
    void Fly();
}
```
## 練習6：物件導向-多型(interface)
橡皮鴨子rubber、木鴨子wood、真實的鴨子realDuck  
三個鴨子都會游泳，而橡皮鴨子和真實的鴨子都會叫  
只是叫聲不一樣，橡子鴨子 唧唧叫，真實的鴨子 嘎嘎叫，木鴨子不會叫。 

- 這一題有兩種說法：  
    - 我們提一個父類出來叫Duck，不管真鴨子、橡皮鴨子、木鴨子都是Duck的子類。
    - 或者是乾脆把真鴨子當作父類。  
- 木鴨子不會叫，所以不能在Duck裡面寫「叫」的方法，橡皮鴨子和真鴨子叫的方式也不一樣，寫成介面interface。

- 「游泳」可以在Duck寫成虛方法virtual。
- 也可以拿虛方法來實現多態

```c#
//橡皮鴨子rubber、木鴨子wood、真實的鴨子realDuck
//三個鴨子都會游泳，而橡皮鴨子和真實的鴨子都會叫
//只是叫聲不一樣，橡子鴨子 唧唧叫，真實的鴨子 嘎嘎叫，木鴨子不會叫。 

//1 draft
//用介面來實現多態
//IBark realDuck = new RealDuck(); //真實的鴨子
//realDuck.Bark();
//IBark rubber = new Rubber(); //橡子鴨子
//rubber.Bark();

//最好的寫法是，創建三個對象
RealDuck realDuck = new RealDuck();
Rubber rubber = new Rubber();
Wood wood = new Wood();

//2.拿介面來實現多態
//IBark realduck = realDuck; //拿介面 直接去指向真鴨子
//realDuck.Swim();
//realduck.Bark();

//3.也可以拿虛方法來實現多態
Duck realduck = new RealDuck();
realduck.Swim();

Console.ReadKey();

public class Duck
{
    public virtual void Swim()
    {
        Console.WriteLine("是鴨子都會游泳");
    }
}
public class Wood : Duck
{
    public override void Swim()
    {
        Console.WriteLine("木鴨子會游泳");
    }
}
public class RealDuck : Duck, IBark
{
    public override void Swim()
    {
        Console.WriteLine("真鴨子會游泳");
    }
    public void Bark()
    {
        Console.WriteLine("真實的鴨子 嘎嘎叫");
    }
}
public class Rubber : Duck, IBark
{
    public override void Swim()
    {
        Console.WriteLine("橡皮鴨子會游泳");
    }
    public void Bark()
    {
        Console.WriteLine("橡子鴨子 唧唧叫");
    }
}
public interface IBark
{
    void Bark();
}
``` 

## 練習7：物件導向-多型(abstract)
計算形狀shape的面積、周長   
(圓circle、矩形square、正方形rectangle)

- 因為每個形狀的計算方式都不一樣，所以用abstract抽象
- 因為長寬高都不一樣，所以求面積方法也不用寫參數了

```c#
//計算形狀shape的面積、周長
//(圓circle、矩形square、長方形rectangle)

//聲明父類去指向子類
Shape circle = new Circle(20);
double area = circle.GetArea();
double perimeter = circle.GetPerimeter();
Console.WriteLine($"圓 面積:{area}, 周長{perimeter}");

Shape rectangle = new Rectangle(5,7);
double area2 = rectangle.GetArea();
double perimeter2 = circle.GetPerimeter();
Console.WriteLine($"長方形 面積:{area2}, 周長{perimeter2}");
Console.ReadKey();

public abstract class Shape
{
    public abstract double GetArea(); //面積
    public abstract double GetPerimeter(); //周長
}
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        this.Width = width;
        this.Height = height;
    }
    public override double GetArea()
    {
        return this.Width * this.Height;
    }
    public override double GetPerimeter()
    {
        return (this.Width + this.Height) * 2;
    }
}

public class Circle : Shape
{
    public double R { get; set; } //半徑

    public Circle(double r)
    {
        this.R = r;
    }
    public override double GetArea()
    {
        return Math.PI * this.R * this.R;
    }
    public override double GetPerimeter()
    {
        return 2 * Math.PI * this.R * this.R;
    }
}
```
    
[https://www.bilibili.com/video/BV1vG411A7my?p=50](https://www.bilibili.com/video/BV1vG411A7my?p=50)