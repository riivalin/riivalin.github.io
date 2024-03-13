---
layout: post
title: "[C# 筆記] InvalidOperationException: The view 'Index' was not found. The following locations were searched"
date: 2023-08-02 23:59:00 +0800
categories: [Notes,C#]
tags: [C#,MVC,.Net Core,issue]
---

## 錯誤訊息
InvalidOperationException: The view 'Index' was not found. The following locations were searched

![](/assets/img/post/mvc-dotnet-core-error-index-was-not-found.png)

All the archovios are created and in the right place.

## 解決方法

1. Go to your project solution --> Right click and add nuget Packages       
called *`Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation`* install this.

2. Add one line on Programm.cs file     
i.e "`builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();`"



<https://stackoverflow.com/questions/75808380/invalidoperationexception-the-view-index-was-not-found-the-following-locatio>