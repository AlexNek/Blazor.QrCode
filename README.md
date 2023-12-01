# Blazor.QrCode
> **Note** Not finished yet
TODO:
-[] use version number into github yml
-[] publish nuget package from pipeline
-[] update documentation

## Introduction

I have used [QRCode.js](https://github.com/davidshimjs/qrcodejs) as a base for the blazor QR code component. We support version .NET 8.0+
In your Blazor WASM application, you can use the Blazor component to generate QR codes without Java script.
Try [demo application ](https://blazorqrcodedemo.azurewebsites.net/)

![image](pics/main-page.png)

## Getting Started

You need to add 2 lines into index.html
```html
    <script src="./_content/Blazor.QrCode/qrcode.min.js"></script>
    <script type="module" src="./_content/Blazor.QrCode/qrcodeInterop.js"></script>
```

Simple use with default settings:
```csharp
<QrCode CanvasId="AnyId" Text="Any text"/>
```


Usage with customized settings:
```csharp
<QrCode CanvasId="AnyId" Text="Any text" Options="_options" />
@code{
 private QrCodeOptions _options = new QRCodeOptons(){Size = 128};
}
```



## Features

A detailed description of each feature in the project. This can be broken down into sections based on the feature's functionality. Each section should explain what the feature does and how to use it.

## Releases

1.0 Initial .Net8.0 release