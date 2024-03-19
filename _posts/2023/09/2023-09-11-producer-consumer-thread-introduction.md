---
layout: post
title: "[C# 筆記] Threading - C# Producer Consumer Thread Introduction"
date: 2023-09-11 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,thread]
---

有了多執行緒，可以進行大量的工作，      
如果將其分解為各個執行緒可以處理的部分，是有意義的。      

# 回顧

[回顧一下之前的範例](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-3/)，現在有這個數據陣列，我們實際上有5億bytes，我將它們視為整數，     
而這個數據陣列會先放入隨機的數字，陣列再平均劃分成大致相等的部分，讓執行緒去工作。       

也就是說，我將該陣列劃分4部分，每個執行緒都被賦予了自己的陣列部分去工作，       
在將這些數據分配出去前，這個數據陣列會放入隨機的數字，      
陣列會分成大致相等的長度，讓執行緒運行。   

執行緒在自己的部分各自加總求和，所以每個線程都得到它的結果，將其儲存在結果陣列中，最後再將結果陣列中的數值相加得到最終值。

## 為何不用lock

而該範例中，並沒有執行任何鎖定`Lock`，為什麼？      

因為，我們在啟動執行縮之前，我們先生成了所有的數字，數據已經準備就緒，      
我們在任何執行緒有機會從中讀取陣列的數據之前，己經將數字寫入該陣列了。      

此外，我們為每個個執行緒提供了自己的陣列部分，從`thread 1`讀取第0個區域：`data[0]-data[5]`，`thread 2`讀取第1個區域：`data[6]-data[10]`，`thread 3`讀取第2個區域：`data[11]-data[15]`，以此類推，每個執行緒都有自己的部分範圍，不會踩踏到其他線程的區域。       

反之，如果我們是在生成所有數字之前，啟動執行緒進行求和，就會發生問題。


# C# Producer Consumer Thread Introduction
假設我有三個執行緒，有一個數字產生機，會隨機吐出數字，      
這三個執行緒會自動的去抓取這些數字，去消耗它們...      
這時候可能會發生：線程1 和 線程2 去找同一個數字，三個線程都會相同踩踏， 
這時候就需要一些協調了...       


在有意義的時候創建線程，我們可以使我們的應用程式運行得更快，        
或者說，將事物分解為執行緒也是有意義的，        
但是必須要小心，我們在計算機科學中所做的幾乎所有事情都需要權衡，當我們創建執行緒時，我們需要了解權衡，如果有意義的話，你得到了資源，如果沒有，那就得重新考慮自己的設計。

 


[C# Producer Consumer Thread Introduction](https://www.youtube.com/watch?v=t850VUYAlpQ&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=11)        
[[C# 筆記] Threading - Divide and Conquer Example Part 1](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-1/)        
[[C# 筆記] Threading - Divide and Conquer Example Part 2](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-2/)        
[[C# 筆記] Threading - Divide and Conquer Example Part 3](https://riivalin.github.io/posts/2023/09/threading-divide-and-conquer-example-part-3/)
