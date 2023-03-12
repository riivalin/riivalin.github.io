---
layout: post
title: "[C# 筆記] 使用泛型和索引器來實現一個自己的集合類List"
date: 2018-06-30 00:00:10 +0800
categories: [Notes, C#]
tags: [C#, "泛型<T>", "索引器[index]"]
---

## 創建構造函數和Capacity Count屬性
定義一個泛型類別 public class MyList<T>
```c#
public class MyList<T> //定義一個泛型類別
{
    private T[] array;//定義數組，來儲存我們的元素
    private int count = 0;//當前添加的元素的個數

    //構造函數(有參數)
    public MyList(int size)
    {
        if (size >= 0) {
            array = new T[size]; //在這裡設定陣列的大小
        }
    }
    //構造函數(無參數)
    public MyList()
    {
        array = new T[0]; //空的陣列
    }

    //容量大小
    public int Capacity
    {
        get { return array.Length; }//獲取數組的容量大小
    }
    //元素的個數
    public int Count
    {
        get { return count; }
    }
}
```
## 添加Add方法
```c#
//添加元素
public void Add(T item) {
    if (Capacity == count) //判斷元素個數與列表容量大小是否一樣，表示陣列容量不夠了
    {
        if (Capacity == 0)
        {
            array = new T[4]; //當陣列長度為0的時候，創建一個長度為4的陣列
        } else {
            var newArray = new T[Capacity * 2]; //當長度不為0的時候，創建一個長度為原來2倍的陣列
            Array.Copy(array, newArray, count); //將舊陣列中的值複製到新的陣列中，複製前count個，array-->newArray
            array = newArray; //把新陣列給array
        }
    }
    array[Count] = item; //加入新的元素
    count++; //元素個數自增
}
```

完整Code
```c#
public class MyList<T> //定義一個泛型類別
{
    private T[] array;//定義數組，來儲存我們的元素
    private int count = 0;//當前添加的元素的個數

    //構造函數(有參數)
    public MyList(int size)
    {
        if (size >= 0) {
            array = new T[size]; //在這裡設定陣列的大小
        }
    }
    //構造函數(無參數)
    public MyList()
    {
        array = new T[0]; //空的陣列
    }

    //容量大小
    public int Capacity
    {
        get { return array.Length; }//獲取數組的容量大小
    }
    //元素的個數
    public int Count
    {
        get { return count; }
    }
    //添加元素
    public void Add(T item) {
        if (Capacity == count) //判斷元素個數與列表容量大小是否一樣，表示陣列容量不夠了
        {
            if (Capacity == 0)
            {
                array = new T[4]; //當陣列長度為0的時候，創建一個長度為4的陣列
            } else {
                var newArray = new T[Capacity * 2]; //當長度不為0的時候，創建一個長度為原來2倍的陣列
                Array.Copy(array, newArray, count); //將舊陣列中的值複製到新的陣列中，複製前count個，array-->newArray
                array = newArray; //把新陣列給array
            }
        }
        array[Count] = item; //加入新的元素
        count++; //元素個數自增
    }
}
```
## 通過索引器訪問元素

索引器：通過[index]這種形式去訪問數據，就是索引器。

```c#
//使用索引取得數據
public T GetItem(int index)
{
    if (index >= 0 && index <= count - 1) {
        return array[index];
    } else {
        throw new Exception("索引超出了範圍");
    }
}
//索引器
public T this[int index] //T是索引器的類型，this代表當前的對象，[int index]傳遞過來的參數
{
    get { return GetItem(index); } //通過索引器取值
    set //通過索引器設值
    {
        if (index >= 0 && index <= count - 1) {
            array[index] = value; //設過來的值
        } else {
            throw new Exception("索引超出了範圍");
        }
    }
}
```
```c#
MyList<int> myList = new MyList<int>();
myList.Add(1);
myList.Add(2);
myList.Add(3);
myList.Add(4);
myList.Add(5);
myList[0] = 100; //通過索引器設值
for (int i = 0; i < myList.Count; i++) {
    //Console.WriteLine(myList.GetItem(i));
    Console.WriteLine(myList[i]); //通過索引器取值
}
Console.ReadKey();
```
完整Code
```c#
public class MyList<T> //定義一個泛型類別
{
    private T[] array;//定義數組，來儲存我們的元素
    private int count = 0;//當前添加的元素的個數

    //構造函數(有參數)
    public MyList(int size)
    {
        if (size >= 0)
        {
            array = new T[size]; //在這裡設定陣列的大小
        }
    }
    //構造函數(無參數)
    public MyList()
    {
        array = new T[0]; //空的陣列
    }

    //容量大小
    public int Capacity
    {
        get { return array.Length; }//獲取數組的容量大小
    }
    //元素的個數
    public int Count
    {
        get { return count; }
    }
    //添加元素
    public void Add(T item)
    {
        if (Capacity == count) //判斷元素個數與列表容量大小是否一樣，表示陣列容量不夠了
        {
            if (Capacity == 0)
            {
                array = new T[4]; //當陣列長度為0的時候，創建一個長度為4的陣列
            } else
            {
                var newArray = new T[Capacity * 2]; //當長度不為0的時候，創建一個長度為原來2倍的陣列
                Array.Copy(array, newArray, count); //將舊陣列中的值複製到新的陣列中，複製前count個，array-->newArray
                array = newArray; //把新陣列給array
            }
        }
        array[Count] = item; //加入新的元素
        count++; //元素個數自增
    }
    //使用索引取得數據
    public T GetItem(int index)
    {
        if (index >= 0 && index <= count - 1) {
            return array[index];
        } else {
            throw new Exception("索引超出了範圍");
        }
    }
    //索引器
    public T this[int index] //T是索引器的類型，this代表當前的對象，[int index]傳遞過來的參數
    {
        get { return GetItem(index); } //通過索引器取值
        set //通過索引器設值
        {
            if (index >= 0 && index <= count - 1) {
                array[index] = value; //設過來的值
            } else {
                throw new Exception("索引超出了範圍");
            }
        }
    }
}
```
## 插入元素Insert()方法

```c#
//插入元素
public void Insert(int index, T item) {
    if (index >= 0 && index <= count - 1) {
        if (Count == Capacity) { //容量不夠，進行擴充
            var newArray = new T[Capacity * 2]; //擴充容量大小為原來陣列的2倍
            Array.Copy(array, newArray, Count); //將舊陣列元素複製到新的陣列中
            array = newArray; //將新陣列引用給array
        }
        for (int i = count-1; i >= index; i--) { //元素從後面開始移動
            array[i + 1] = array[i]; //把i位置的值放在後面，就是向後移動一個單位
        }
        array[index] = item; //插入元素
        count++; //元素的個數自增
    } else {
        throw new Exception("索引超出範圍");
    }
}
```
myList.Insert(1, 99); //插入元素
```c#
MyList<int> myList = new MyList<int>();
myList.Add(1);
myList.Add(2);
myList.Add(3);
myList.Add(4);
myList.Add(5);
myList[0] = 100; //通過索引器設值
myList.Insert(1, 99); //插入元素
for (int i = 0; i < myList.Count; i++) {
    //Console.WriteLine(myList.GetItem(i));
    Console.WriteLine(myList[i]); //通過索引器取值
}
Console.ReadKey();
```
完整Code
```c#
public class MyList<T> //定義一個泛型類別
{
    private T[] array;//定義數組，來儲存我們的元素
    private int count = 0;//當前添加的元素的個數

    //構造函數(有參數)
    public MyList(int size)
    {
        if (size >= 0)
        {
            array = new T[size]; //在這裡設定陣列的大小
        }
    }
    //構造函數(無參數)
    public MyList()
    {
        array = new T[0]; //空的陣列
    }

    //容量大小
    public int Capacity
    {
        get { return array.Length; }//獲取數組的容量大小
    }
    //元素的個數
    public int Count
    {
        get { return count; }
    }
    //添加元素
    public void Add(T item)
    {
        if (Capacity == count) //判斷元素個數與列表容量大小是否一樣，表示陣列容量不夠了
        {
            if (Capacity == 0)
            {
                array = new T[4]; //當陣列長度為0的時候，創建一個長度為4的陣列
            } else
            {
                var newArray = new T[Capacity * 2]; //當長度不為0的時候，創建一個長度為原來2倍的陣列
                Array.Copy(array, newArray, count); //將舊陣列中的值複製到新的陣列中，複製前count個，array-->newArray
                array = newArray; //把新陣列給array
            }
        }
        array[Count] = item; //加入新的元素
        count++; //元素個數自增
    }
    //使用索引取得數據
    public T GetItem(int index)
    {
        if (index >= 0 && index <= count - 1) {
            return array[index];
        } else {
            throw new Exception("索引超出了範圍");
        }
    }
    //索引器
    public T this[int index] //T是索引器的類型，this代表當前的對象，[int index]傳遞過來的參數
    {
        get { return GetItem(index); } //通過索引器取值
        set //通過索引器設值
        {
            if (index >= 0 && index <= count - 1) {
                array[index] = value; //設過來的值
            } else {
                throw new Exception("索引超出了範圍");
            }
        }
    }
    //插入元素
    public void Insert(int index, T item) {
        if (index >= 0 && index <= count - 1) {
            if (Count == Capacity) { //容量不夠，進行擴充
                var newArray = new T[Capacity * 2]; //擴充容量大小為原來陣列的2倍
                Array.Copy(array, newArray, Count); //將舊陣列元素複製到新的陣列中
                array = newArray; //將新陣列引用給array
            }
            for (int i = count-1; i >= index; i--) { //元素從後面開始移動
                array[i + 1] = array[i]; //把i位置的值放在後面，就是向後移動一個單位
            }
            array[index] = item; //插入元素
            count++; //元素的個數自增
        } else {
            throw new Exception("索引超出範圍");
        }
    }
}
```
##  移除指定位置的元素RemoveAt
```c#
//移除元素
public void RemoveAt(int index)
{
    if (index >= 0 && index <= count - 1) //判斷這個索引是否存在
    {
        for (int i = index+1; i < count; i++)
        {
            array[i - 1] = array[i];//元素要向前移動一個位置
        }
        count--; //個數要自減
    } else {
        throw new Exception("索引超出範圍");
    }
}
```
myList.RemoveAt(3);//移除元素
```c#
MyList<int> myList = new MyList<int>();
myList.Add(1);
myList.Add(2);
myList.Add(3);
myList.Add(4);
myList.Add(5);
//myList[0] = 100; //通過索引器設值
//myList.Insert(1, 99); //插入元素
myList.RemoveAt(3);//移除元素
for (int i = 0; i < myList.Count; i++) {
    //Console.WriteLine(myList.GetItem(i));
    Console.WriteLine(myList[i]); //通過索引器取值
}
Console.ReadKey();
```
完整Code
```c#
public class MyList<T> //定義一個泛型類別
{
    private T[] array;//定義數組，來儲存我們的元素
    private int count = 0;//當前添加的元素的個數

    //構造函數(有參數)
    public MyList(int size)
    {
        if (size >= 0)
        {
            array = new T[size]; //在這裡設定陣列的大小
        }
    }
    //構造函數(無參數)
    public MyList()
    {
        array = new T[0]; //空的陣列
    }

    //容量大小
    public int Capacity
    {
        get { return array.Length; }//獲取數組的容量大小
    }
    //元素的個數
    public int Count
    {
        get { return count; }
    }
    //添加元素
    public void Add(T item)
    {
        if (Capacity == count) //判斷元素個數與列表容量大小是否一樣，表示陣列容量不夠了
        {
            if (Capacity == 0)
            {
                array = new T[4]; //當陣列長度為0的時候，創建一個長度為4的陣列
            } else
            {
                var newArray = new T[Capacity * 2]; //當長度不為0的時候，創建一個長度為原來2倍的陣列
                Array.Copy(array, newArray, count); //將舊陣列中的值複製到新的陣列中，複製前count個，array-->newArray
                array = newArray; //把新陣列給array
            }
        }
        array[Count] = item; //加入新的元素
        count++; //元素個數自增
    }
    //使用索引取得數據
    public T GetItem(int index)
    {
        if (index >= 0 && index <= count - 1) {
            return array[index];
        } else {
            throw new Exception("索引超出了範圍");
        }
    }
    //索引器
    public T this[int index] //T是索引器的類型，this代表當前的對象，[int index]傳遞過來的參數
    {
        get { return GetItem(index); } //通過索引器取值
        set //通過索引器設值
        {
            if (index >= 0 && index <= count - 1) {
                array[index] = value; //設過來的值
            } else {
                throw new Exception("索引超出了範圍");
            }
        }
    }
    //插入元素
    public void Insert(int index, T item) {
        if (index >= 0 && index <= count - 1) {
            if (Count == Capacity) { //容量不夠，進行擴充
                var newArray = new T[Capacity * 2]; //擴充容量大小為原來陣列的2倍
                Array.Copy(array, newArray, Count); //將舊陣列元素複製到新的陣列中
                array = newArray; //將新陣列引用給array
            }
            for (int i = count-1; i >= index; i--) { //元素從後面開始移動
                array[i + 1] = array[i]; //把i位置的值放在後面，就是向後移動一個單位
            }
            array[index] = item; //插入元素
            count++; //元素的個數自增
        } else {
            throw new Exception("索引超出範圍");
        }
    }
    //移除元素
    public void RemoveAt(int index)
    {
        if (index >= 0 && index <= count - 1) //判斷這個索引是否存在
        {
            for (int i = index+1; i < count; i++)
            {
                array[i - 1] = array[i];//元素要向前移動一個位置
            }
            count--; //個數要自減
        } else {
            throw new Exception("索引超出範圍");
        }
    }
}
```                                         
## 創建IndexOf、LastIndexOf和Sort排序方法
創建IndexOf、LastIndexOf
```c#
//搜尋元素的索引值
public int IndexOf(T item) 
{
    for (int i = 0; i < count; i++) {
        if (array[i].Equals(item)) {
            return i;
        }
    }
    return -1; //沒有找到值
}
//從最後面開始搜尋元素的索引值
public int LastIndexOf(T item) {

    for (int i = Count-1; i >=0; i--) {
        if (array[i].Equals(item)) {
            return i;
        }
    }
    return -1; //沒有找到值
}
```
myList.IndexOf(33); //從前往後搜索
myList.LastIndexOf(33); //從後往前搜索
```c#
MyList<int> myList = new MyList<int>();
myList.Add(1);
myList.Add(2);
myList.Add(33);
myList.Add(33);
myList.Add(5);
//myList[0] = 100; //通過索引器設值
//myList.Insert(1, 99); //插入元素
//myList.RemoveAt(3);//移除元素
for (int i = 0; i < myList.Count; i++) {
    //Console.WriteLine(myList.GetItem(i));
    Console.WriteLine(myList[i]); //通過索引器取值
}
Console.WriteLine(myList.IndexOf(33)); //從前往後搜索
Console.WriteLine(myList.LastIndexOf(33)); //從後往前搜索
Console.ReadKey();
```

創建Sort排序方法

為Sort()方法加上where T: IComparable 就可以使用CompareTo來比較大小
> 主要是要讓sort()方法可以調用CompareTo方法
```c#
public class MyList<T> where T: IComparable
```
> where T:表示對T加個約束，繼承IComparable接口，可以比較大小的接口

```c#
//排序
public void Sort() {
    for (int j = 0; j < Count-1; j++) { //要做多少次循環
        for (int i = 0; i < Count - 1-j; i++) //-j，因為當第一次循環後，就不需要再跟最後一個值做比較，次數就可以減一次，以此類推
        { //把最大值放到最後面
            if (array[i].CompareTo(array[i + 1]) > 0)
            { //跟後面的值做比較，大於0就要做交換
                T temp = array[i];
                array[i] = array[i + 1];
                array[i + 1] = temp;
            }
        }
    }
}
```
myList.Sort();//排序
```c#
MyList<int> myList = new MyList<int>();
myList.Add(1);
myList.Add(2);
myList.Add(33);
myList.Add(33);
myList.Add(5);
myList[0] = 100; //通過索引器設值
myList.Insert(1, 99); //插入元素
myList.RemoveAt(3);//移除元素
for (int i = 0; i < myList.Count; i++) {
    //Console.WriteLine(myList.GetItem(i));
    Console.Write($"{myList[i]} "); //通過索引器取值
}
Console.WriteLine();
Console.WriteLine(myList.IndexOf(33)); //從前往後搜索
Console.WriteLine(myList.LastIndexOf(33)); //從後往前搜索
myList.Sort();//排序
for (int i = 0; i < myList.Count; i++) {
    //Console.WriteLine(myList.GetItem(i));
    Console.Write($"{myList[i]} "); //通過索引器取值
}
Console.ReadKey();
```
完整Code
```c#
//定義一個泛型類別
public class MyList<T> where T: IComparable //where T:表示對T加個約束，繼承IComparable接口，可以比較大小的接口，主要是要讓sort()方法可以調用CompareTo方法
{
    private T[] array;//定義數組，來儲存我們的元素
    private int count = 0;//當前添加的元素的個數

    //構造函數(有參數)
    public MyList(int size)
    {
        if (size >= 0)
        {
            array = new T[size]; //在這裡設定陣列的大小
        }
    }
    //構造函數(無參數)
    public MyList()
    {
        array = new T[0]; //空的陣列
    }

    //容量大小
    public int Capacity
    {
        get { return array.Length; }//獲取數組的容量大小
    }
    //元素的個數
    public int Count
    {
        get { return count; }
    }
    //添加元素
    public void Add(T item)
    {
        if (Capacity == count) //判斷元素個數與列表容量大小是否一樣，表示陣列容量不夠了
        {
            if (Capacity == 0)
            {
                array = new T[4]; //當陣列長度為0的時候，創建一個長度為4的陣列
            } else
            {
                var newArray = new T[Capacity * 2]; //當長度不為0的時候，創建一個長度為原來2倍的陣列
                Array.Copy(array, newArray, count); //將舊陣列中的值複製到新的陣列中，複製前count個，array-->newArray
                array = newArray; //把新陣列給array
            }
        }
        array[Count] = item; //加入新的元素
        count++; //元素個數自增
    }
    //使用索引取得數據
    public T GetItem(int index)
    {
        if (index >= 0 && index <= count - 1) {
            return array[index];
        } else {
            throw new Exception("索引超出了範圍");
        }
    }
    //索引器
    public T this[int index] //T是索引器的類型，this代表當前的對象，[int index]傳遞過來的參數
    {
        get { return GetItem(index); } //通過索引器取值
        set //通過索引器設值
        {
            if (index >= 0 && index <= count - 1) {
                array[index] = value; //設過來的值
            } else {
                throw new Exception("索引超出了範圍");
            }
        }
    }
    //插入元素
    public void Insert(int index, T item) {
        if (index >= 0 && index <= count - 1) {
            if (Count == Capacity) { //容量不夠，進行擴充
                var newArray = new T[Capacity * 2]; //擴充容量大小為原來陣列的2倍
                Array.Copy(array, newArray, Count); //將舊陣列元素複製到新的陣列中
                array = newArray; //將新陣列引用給array
            }
            for (int i = count-1; i >= index; i--) { //元素從後面開始移動
                array[i + 1] = array[i]; //把i位置的值放在後面，就是向後移動一個單位
            }
            array[index] = item; //插入元素
            count++; //元素的個數自增
        } else {
            throw new Exception("索引超出範圍");
        }
    }
    //移除元素
    public void RemoveAt(int index)
    {
        if (index >= 0 && index <= count - 1) //判斷這個索引是否存在
        {
            for (int i = index+1; i < count; i++)
            {
                array[i - 1] = array[i];//元素要向前移動一個位置
            }
            count--; //個數要自減
        } else {
            throw new Exception("索引超出範圍");
        }
    }
    //搜尋元素的索引值
    public int IndexOf(T item) 
    {
        for (int i = 0; i < count; i++) {
            if (array[i].Equals(item)) {
                return i;
            }
        }
        return -1; //沒有找到值
    }
    //從最後面開始搜尋元素的索引值
    public int LastIndexOf(T item) {

        for (int i = Count-1; i >=0; i--) {
            if (array[i].Equals(item)) {
                return i;
            }
        }
        return -1; //沒有找到值
    }
    //排序
    public void Sort() {
        for (int j = 0; j < Count-1; j++) { //要做多少次循環
            for (int i = 0; i < Count - 1-j; i++) //-j，因為當第一次循環後，就不需要再跟最後一個值做比較，次數就可以減一次，以此類推
            { //把最大值放到最後面
                if (array[i].CompareTo(array[i + 1]) > 0)
                { //跟後面的值做比較，大於0就要做交換
                    T temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                }
            }
        }
    }
}
```