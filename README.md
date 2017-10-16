# BinanceDotNet <img src="https://i.imgur.com/x2YPVe6.png" width="56" />

## C# Wrapper for the official Binance exchange API
<img src="https://img.shields.io/nuget/dt/BinanceDotNet.svg" />
<img src="https://img.shields.io/nuget/v/BinanceDotNet.svg" />

This repository provides a C# wrapper for the official Binance API, and provides rate limiting features _(set to 10 by 10 out the box)_, a `IAPICache` interface to allow users to provide their own cache implementations, all `REST` endpoints covered, and a best practice solution coupled with strongly typed responses and requests. It is built on the latest .NET Framework and in .NET Core

Feel free to raise issues and Pull Request to help improve the library.

## Features
- Rate limiting, with 10 requests in 10 seconds _(disabled by default)_
- Dotnet core 2.0
- Binance WebSockets
- `IAPICache` abstraction for providing your own cache. _(Currently only one endpoint has caching)_
- Basic console app with examples ready to launch _(provide API keys)_

## Installation
The package is available in NuGet, or feel free to download:
https://www.nuget.org/packages/BinanceDotNet/

**Nuget PM**
```
Install-Package BinanceDotNet
```

**dotnet cli**
```
dotnet add package BinanceDotNet
```

## Usage
General usage just requires setting up the client with your credentials, and then calling the Client as necessary.
```c#
var client = new BinanceClient(new ClientConfiguration()
{
    ApiKey = "YOUR_API_KEY",
    SecretKey = "YOUR_SECRET_KEY",
});

IReponse response = await client.GetCompressedAggregateTrades(new GetCompressedAggregateTradesRequest(){
  Symbol = "BTCETH",
  StartTime = DateTime.UtcNow().AddDays(-1),
  EndTime = Date.UtcNow(),
});
```

For WebSocket endpoints, just instantiate the `BinanceClient`, and provide it into the `BinanceWebSocketClient`
```c#
var client = new BinanceClient(new ClientConfiguration()
{
    ApiKey = "YOUR_API_KEY",
    SecretKey = "YOUR_SECRET_KEY",
});

using (var binanceWebSocketClient = new BinanceWebSocketClient(client))
{
    binanceWebSocketClient.ConnectToDepthWebSocket("ETHBTC", data =>
    {
        System.Console.WriteLine($"DepthCall: {JsonConvert.SerializeObject(data)}");
    });

    Thread.Sleep(180000);
}
```

## Roadmap
- In depth documentation on GitHub - 1.3.0
- Build out Unit Test support - 1.4.0
- Add in `log4net` logger - 1.5.0
- Provide additional CacheLayer - 1.6.0
- Provide Builder support for queries - 2.0.0
- Abstract out the HttpClient - 2.0.0


