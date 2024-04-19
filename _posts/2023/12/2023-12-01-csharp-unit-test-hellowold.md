---
layout: post
title: "[C# 筆記][UnitTest] 單元測試簡單的 HelloWorld"
date: 2023-12-01 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,單元測試,MSTest,UnitTest,Assert]
---

# 建立單元測試

1. 在 Visual Studio 中開啟您要測試的專案。

```c#
namespace HelloWorld
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
```

2. 在 [方案總管] 中，選取解決方案節點。 然後，從頂端功能表列中，選取 [檔案]>[新增]>[新增專案]。
3. 在 [新增專案] 對話方塊中，尋找要使用的單元測試專案。

在搜尋方塊中輸入「test」，以尋找單元測試專案範本，例如 `MSTest` 

![](https://learn.microsoft.com/zh-tw/visualstudio/test/media/vs-2022/add-new-test-project.png?view=vs-2022)

按一下 [下一步]，選擇測試專案的名稱，然後按一下 [建立]。

![](https://learn.microsoft.com/zh-tw/visualstudio/test/media/vs-2022/solution-explorer.png?view=vs-2022)   

> 專案命名規則：「`專案名稱.UnitTests`」 (Test是複數要加s，`Tests`)，例如：`HelloWorldTests`。

4. 在單元測試專案中，以滑鼠右鍵按一下 [參考] 或 [相依性]，然後選擇 [新增參考] 或 [新增專案參考]，在您想要測試的專案中新增參考。

![](https://learn.microsoft.com/zh-tw/visualstudio/test/media/vs-2022/solution-explorer.png?view=vs-2022)

5. 選取包含您要測試之程式碼的專案，然後按一下 [確定]OK。

![](https://learn.microsoft.com/zh-tw/visualstudio/test/media/vs-2022/reference-manager.png?view=vs-2022)

6. 將程式碼新增至單元測試方法。

```c#
namespace HelloWorldTests
{
    [TestClass]
    public class UnitTest1
    {
        private const string Expected = "Hello, World!";

        [TestMethod]
        public void TestMethod1()
        {
            using (var sw = new StringWriter()) {
                Console.SetOut(sw);
                HelloWorld.Program.Main();

                var reslut = sw.ToString().Trim();
                Assert.AreEqual(Expected, reslut);
            }
        }
    }
}
```


# 執行單元測試

1. 開啟 [測試總管]。

若要開啟測試總管，請從頂端功能表列選擇 [測試]>[測試總管] (或按 Ctrl + E、T)。

2. 按一下 [全部執行] 執行您的單元測試 (或按 Ctrl + R、V)。

![](https://learn.microsoft.com/zh-tw/visualstudio/test/media/vs-2022/test-explorer-run-all.png?view=vs-2022)

測試完成之後，綠色的核取記號表示測試通過。 紅色的 "x" 圖示表示測試失敗。        

![](https://learn.microsoft.com/zh-tw/visualstudio/test/media/vs-2022/unit-test-passed.png?view=vs-2022)


## 成功

![](/assets/img/post/unit-test-hellowold-ok.png)

## 失敗

![](/assets/img/post/unit-test-hellowold-faild.png)

[MSDN - 開始使用單元測試](https://learn.microsoft.com/zh-tw/visualstudio/test/getting-started-with-unit-testing?view=vs-2022&tabs=dotnet%2Cmstest)       
[[C# 筆記][UnitTest] 介面與單元測試](https://riivalin.github.io/posts/2010/01/r-csharp-note-25/)