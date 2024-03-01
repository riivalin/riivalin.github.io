---
layout: post
title: "[C# 筆記] Error：Only the invariant culture is supported in globalization-invariant mode"
date: 2023-07-10 23:59:00 +0800
categories: [Notes,Web API]
tags: [C#,Web API,.Net Core,error,issue]
---

## 錯誤訊息

Error: response status is 400       

Only the invariant culture is supported in globalization-invariant mode.        
en-us is an invalid culture identifier.     

## 解決方法

`project.csproj`中的`<InvariantGlobalization>`改成`false`

```c#
<PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>false</InvariantGlobalization>
</PropertyGroup>
```