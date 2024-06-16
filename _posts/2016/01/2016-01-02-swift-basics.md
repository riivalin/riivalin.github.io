---
layout: post
title: "[Swift] Swift 基礎語法筆記 (Draft)"
date: 2016-01-02 06:30:00 +0800
categories: [Notes,Swift]
tags: [SwiftUI]
---

Riva Notes (just draft)

# try try? try! 的使用時機

## try
當開發者需要很完整的錯誤訊息，或是依據每個所拋出的錯誤的差異而有不同的事情要做時。

## try?

當開發者必須做 `Error Handling` 時，又不太需要知道錯誤的種類，只需要知道有錯誤發生，並依照開發的需求適當的處理錯誤發生時要做的事情。

## try!

當開發者非常、非常、非常確定**絕對不會有錯誤產生**，但又必須要做 Error Handling 時使用。


# do try catch

> ※ `do try catch` 是 `Error Handling` 最完整的寫法。

在使用`do try catch`時，我們必須將呼叫可能會拋出錯誤的方法及之後要做的事情包在 `do{}` 裡面，並在呼叫該方法前擺上 `try` 關鍵字，其後將錯誤出現時要做的事情放在 `catch{}`裡。


`try!`      
會使用 `try!` 通常都是開發人員`非常非常確定`不會有錯誤產生的時候。


`try?`      
在現實世界，問號通常都代表著不確定的因素。      
由於 `try?` 不擁有 `catch` 功能，所以當方法拋出錯誤訊息時，開發者僅能得知有錯誤發生，卻不會得知錯誤訊息的種類。     

`「_(底線)」 `    
用「`_`」(底線) 消除的忽略回傳值警告    


# 用「 _=」 底線消除的忽略回傳值警告

## 無認何變數，取代為_(底線)   

```swift
// 無認何變數，取代為_(底線)。
_ = semaphore.wait(timeout: DispatchTime.distantFuture) 
```

## 將迴圈 i 取代為_(底線)

如果單純只是想要跑迴圈，並無任何變數可以將`i`取代為`_`(底線)

```swift
//如果單純只是想要跑迴圈，並無任何變數可以將i取代為_(底線)
for _ in 1 ... 3 {
    print("Hi")
}
```

# 可變参数的函数 "..."

```swift
//可變参数的函数
func sum(input: Int...) -> Int {
    //...
}

func sum(input: Int...) -> Int {
    return input.reduce(0, combine: +)
}

print(sum(1,2,3,4,5))
// 输出：15
```

# function 注意事項

## call function時，參數後要指定參數名稱，不可以省略，如果要省略要加上底線

```swift
//call function時，參數後要指定參數名稱，不可以省略
func plus(a :Int, b:Int){
  print(a+b)
}
plus(a:1, b:3)

//如果要省略要加上底線
func plus(_ a :Int,_ b:Int){
  print(a+b)
}
plus(1, 3) 
```

# 外部參數名稱(External parameter Name)

事實上a前面的底線是External parameter Name的意思。      
External parameter Name用於外部呼叫Function時的參數識別名，若不寫則與內部名稱相同。     
而底線代表不在意參數識別名稱，即為可忽略的意思。

```swift
func plus(AAA a :Int,BBB b:Int){
  print(a+b)
}
plus(AAA:1, BBB:3)
```

# guard的用途 

`guard`比`if`能更好的實行Defensive Programming，        
因為在guard中的可選綁定可以在接下來code中使用。     

```swift
var text:String? = "abc"
if let apple = text{
	print(apple)
}
// 在這邊用會失敗
// print(apple)

var text:String? = "abc"
guard let apple = text else{
	print(apple)
}
// 照用
print(apple)
```


# Error
Swift丟出的異常是一種enum，實作時繼承Error。        

有幾種方法：    

1. 最基本的`do try catch` 
2. 向上拋擲`function`填`throw`  
3. `try?` : 如果出現錯誤則回傳`nil`(`不用再寫do catch`)，可搭配`optinal binding`    
4. `try!` : 一定成功。(`若沒成功會crash`)   


# Need to know 

## `weak` & `unowned` 

`[weak self, unowned inputCell]`

```swift
[weak self, unowned inputCell]
```

```swift
// A class
var resignationHandler: (() -> Void)?
func textFieldDidEndEditing(_ textField: UITextField) {
        resignationHandler?()
    }

// B class
inputCell.resignationHandler = {  
    [weak self, unowned inputCell] in
        if let text = inputCell.textField.text {
            self?.emojis = (text.map { String($0) } + self!.emojis).uniquified
        }
        self?.addingEmoji = false // 將新增狀態設為 false
        self?.emojiCollectionView.reloadData() // 更新 emoji 資料
}
```

[Stanford CS193p - Developing Apps for iOS  (斯坦福(Stanford) CS193p)](https://cs193p.sites.stanford.edu)           
[[Swift] Questions that may be asked in the interview.  by R](https://riivalin.github.io/posts/2015/12/swift-questions-that-may-be-asked-in-the-interview/)     
[Swift is a lot like C#  by R](https://riivalin.github.io/posts/1999/02/swift-is-a-lot-like-c-sharp/)

