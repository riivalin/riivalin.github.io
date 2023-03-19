---
layout: post
title: "[C# 筆記] array 練習"
date: 2011-01-08 10:00:00 +0800
categories: [Notes, C#]
tags: [C#]
---

## 練習1：從一個整數數組中取出最大的整數、最小整數、總和，平均值

```c#
int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
int max = nums[0]; //儲存最大值, 並將一個元素賦值給max做參照比較, 不一定要給nums[0]
int min = nums[0]; //儲存最小值, 並將一個元素賦值給max做參照比較, 不一定要給nums[0]
int sum = 0; // //儲存總和

//for迴圈每個元素都跟max,min進行比較
for (int i = 0; i < nums.Length; i++)
{
    max = max > nums[i] ? max : nums[i]; //最大值：max與每個陣列元素比較
    min = min < nums[i] ? min : nums[i]; //最小值：min與每個陣列元素比較
    sum += nums[i]; //將每個元素加到總和中
}
Console.WriteLine($"最大整數: {max} \r\n最小整數: {min}\r\n總和: {sum}\r\n平均值: {sum / nums.Length}");
```

## 練習2：數組裡面都是人的名字，分割成：例如:小明|小王|小三|小四…

解題思路：  
通過一個循環，獲得字串陣列中的每一個元素。  
然後，將每一個元素都累加到一個字串中，以`|`分隔。 

```c#
string[] names = { "小明", "小王", "小三", "小四", "小五" };
string str = null;

//通過for循環, 獲得字串陣列中的每一個元素
for (int i = 0; i < names.Length - 1; i++)
{
    str += names[i] + "|"; //將每一個元素都累加到一個字串中，以|分隔
}
Console.WriteLine(str + names[names.Length - 1]); //最後一個名字，手動加上去
```

## 練習：把一個整數陣列正數+1,負數-1, 0不改變
把一個整數陣列的每一個元素進行如下的處理：  
如果元素是正數，則將這個位置的元素的值加1   
如果元素是負數，則將這個位置的元素的值減1   
如果元素是0，則不改變。 

解題思路：  
通過一個循環，獲得陣列中的每一個元素    
對每個元素進行判斷  

```c#
int[] nums = { 1, -2, 3, -4, 5, 6 };

//通過for循環，獲得陣列中的每一個元素 
for (int i = 0; i < nums.Length; i++)
{
    if (nums[i] > 0)
    {
        nums[i] += 1; //如果元素是正數，則將這個位置的元素的值加1
    } else if (nums[i] < 0)
    {
        nums[i] -= 1; //如果元素是負數，則將這個位置的元素的值減1
    } else
    {
        //nums[i] = 0; //如果元素是0，則不改變。
    }
}

//輸出看結果
for (int i = 0; i < nums.Length; i++)
{
    Console.WriteLine(nums[i]);
}
Console.ReadKey();
```

## 練習：將一個字串陣列的元素的順序進行反轉

 ```text
 { "我", "是", "好人" }  =>  { "好人", "是","我" }
 ```

解題思路：  
5個元素交換了2次    
6個元素交換了3次   
N個元素交換(N/2)次     


|a  b   c   d   e   f                        ||||
|0  1   2   3   4   5                        ||||
|:---------|:---------|:------|:-----|---------:|   
|第一次交換：|a 跟 f 交換|0 5 交換| i=0 |Length-1-0 |     
|第二次交換：|b 跟 e 交換|1 4 交換| i=1 |Length-1-1 |    
|第三次交換：|c 跟 d 交換|2 3 交換| i=2 |Length-1-2 |  


|`nums[index]`|跟 `nums[index]` 交換|
|:----|----------------:|       
| i=0 |Length-1-0       |
| i=1 |Length-1-1       |
| i=2 |Length-1-2       |

> 仔細看，它有一個規律在，  
當Length-1-0 1 2，0 1 2 恰好是i值， 
所以可以規律出：    
 `nums[i]` 跟 `nums[Length-1-i]` 交換   

程式碼：
```c#
string[] names = { "我", "是", "好人" };

//反轉字串陣列
for (int i = 0; i < names.Length / 2; i++) //N個元素交換(N/2)次
{
    string temp = names[i]; //宣告一個暫存變數
    names[i] = names[names.Length - 1 - i]; //i=0, names[0]跟 names[5]交換
    names[names.Length - 1 - i] = temp; //names[0]暫存在temp, 賦值給 names[5]
}

//輸出看結果
for (int i = 0; i < names.Length; i++) {
    Console.WriteLine(names[i]);
}
```