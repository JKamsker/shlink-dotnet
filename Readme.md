<p align="center">
  <a href="https://turbo.build">
    <picture>
      <source media="(prefers-color-scheme: dark)" srcset="https://raw.githubusercontent.com/JKamsker/shlink-dotnet/master/resources/icon.png">
      <img src="https://raw.githubusercontent.com/JKamsker/shlink-dotnet/master/resources/icon.png" height="128">
    </picture>
    <h1 align="center">Shlink .NET</h1>
  </a>
</p>

[![NuGet version (ShlinkDotnet)](https://img.shields.io/nuget/v/ShlinkDotnet.svg?style=flat-square)](https://www.nuget.org/packages/ShlinkDotnet)
[![Nuget](https://img.shields.io/nuget/dt/ShlinkDotnet)](https://www.nuget.org/packages/ShlinkDotnet)
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/JKamsker/shlink-dotnet/.NET)](https://github.com/JKamsker/shlink-dotnet/actions)
[![GitHub license](https://img.shields.io/github/license/JKamsker/shlink-dotnet)](https://github.com/JKamsker/shlink-dotnet/blob/master/LICENSE.txt)
[![PR](https://img.shields.io/badge/PR-Welcome-blue)](https://github.com/JKamsker/shlink-dotnet/pulls)

An API client for [shlink.io](https://github.com/shlinkio/shlink).

# How to use
[Example console app using DI](https://github.com/JKamsker/shlink-dotnet/blob/master/examples/ShlinkDotnet.Console/Program.cs)

## Basic sample:

Initialization:
```csharp
var restClient = ShlinkRestClient.Create("https://sh.link.cf", "1b7f1396-0c14-48ea-b581-07c2bf229ca1");
var apiClient = new ShlinkApiClient(restClient);
```
</br>

Enumerating through all shorted links:
```csharp
await foreach (var it in apiClient.EnumerateShortUrls())
{
    Console.WriteLine($"{it.ShortCode}: {it.LongUrl}");
}
```

Creating a new shortlink:
````csharp
var res = await apiClient.CreateOrUpdateAsync(new CreateShortUrlWithSlugRequest
{
    LongUrl = "https://www.google.at",
    ShortCode = "google"
});
````

</br>
<p align="center">
Made with <span style="color: #e25555;">&hearts;</span> in Austria <img src="https://images.emojiterra.com/google/noto-emoji/v2.034/128px/1f1e6-1f1f9.png" width="20" height="20"/> 
</p>
