---
layout: post
title: "[C# 筆記] Interface 介面 -練習2"
date: 2011-01-23 23:19:00 +0800
categories: [Notes, C#]
tags: [C#,interface]
---

## 練習：
真的鴨子會游泳 木頭鴨子不會游泳 橡皮鴨子會游泳    
用多型來實現
```c#
//真的鴨子會游泳 木頭鴨子不會游泳 橡皮鴨子會游泳
//用多型來實現

//介面去指向真的鴨子
ISwimming swim = new RealDuck();
swim.Swim(); //真的鴨子在游泳
Console.ReadKey();


public class RealDuck : ISwimming {
    public void Swim() {
        Console.WriteLine("真的鴨子靠翅膀游泳");
    }
}

public class MuDuck { }

public class XPDuck : ISwimming {
    public void Swim() {
        Console.WriteLine("橡皮鴨子漂著游泳");
    }
}

//游泳介面
public interface ISwimming {
    void Swim();
}
```

- 什麼時候用虛方法來實現多型?(virtual)(父類可以實現方法)
- 什麼時候用抽象類來實現多型?(abstract)(父類不能實現方法)
- 什麼時候用介面來實現多型?(interface)
    
我提供給你的這幾個類中，你能夠抽象出一個父類出來，並且能在父類當中寫出這幾個子類能共用的方法，然後呢？你還不知道怎麼如何去寫這個方法，就用抽象類。    

反之，抽象出來的這個父類，這個方法可以寫，並且我還可以創建這個父類的對象，就用虛方法。    
 
介面呢？這幾類裡面你根本找不出父類，但是他們都有共同的行為、共同能力，這時候就用介面。例如：鳥類、飛機，鳥類和飛機沒有什麼父類，但是他們都會飛，「飛」就寫成介面，你能寫個父類讓他們繼承嗎？不能，因為你根本就提不出來父類。    

會能幹什麼、能幹什麼、就是透過「能力」，「能力(游泳)」就給介面來做。    

可以把真的鴨子提出來做父類，但不能裡面寫游泳方法，因為木頭鴨子不會游泳。 

能用抽象方法嗎？不能，因為真的鴨子需要被創建對象，真的鴨子有意義呀，他真的會游泳。    

虛方法也不行，因為木頭鴨子不會游泳，所以不能用虛方法。

所以誰最合適，介面。   