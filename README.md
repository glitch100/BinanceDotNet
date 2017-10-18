# BinanceDotNet <img src="https://i.imgur.com/x2YPVe6.png" width="56" />

## C# Wrapper for the official Binance exchange API
<img src="https://img.shields.io/nuget/dt/BinanceDotNet.svg" />
<img src="https://img.shields.io/nuget/v/BinanceDotNet.svg" />

This repository provides a C# wrapper for the official Binance API, and provides rate limiting features _(set to 10 by 10 out the box)_, a `IAPICacheManager` interface to allow users to provide their own cache implementations, all `REST` endpoints covered, and a best practice solution coupled with strongly typed responses and requests. It is built on the latest .NET Framework and in .NET Core

Feel free to raise issues and Pull Request to help improve the library.

## Documentation
- [Binance Clients](/docs/BINANCE-CLIENTS.md)
- [REST API Calls](/docs/REST-API.md)
- [WebSocket API Calls](/docs/WEBSOCKET-API.md)


## Features
- Rate limiting, with 10 requests in 10 seconds _(disabled by default)_
- `NLog` support
- Dotnet core 2.0
- Binance WebSockets
- `IAPICacheManager` abstraction for providing your own cache or using the build in concrete implementation. _(Currently only one endpoint has caching)_
- Console app with examples ready to launch _(provide API keys)_

## Roadmap
- Build out Unit Test support - 1.6.0
- Provide Builder support for queries - 2.0.0
- Abstract out the HttpClient - 2.0.0

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
Code examples below, or clone the repository and run the `BinanceExchange.Console` project.
**This repository is built off dotnet core, and runs against C# 7.1**

### Creating a Client
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

### Creating a WebSocket Client
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

## Examples
More examples are available to play around with within the repositorys Console application.

### Building out a local cache per symbol from the depth WebSocket
The example is mainly 'all in one' so you can see a full runthrough of how it works. In your own implementations you may want to have a cache of only the most recent bids/asks, or perhaps will want the empty quanity/price trades.

You can also calculate volume and more from this cache.

```c#
// Code example of building out a Dictionary local cache for a symbol
var localDepthCache = new Dictionary<string, DepthCacheObject> {{ "BNBBTC", new DepthCacheObject()
{
    Asks = new Dictionary<decimal, decimal>(),
    Bids = new Dictionary<decimal, decimal>(),
}}};
var bnbBtcDepthCache = localDepthCache["BNBBTC"];

// Get Order Book, and use Cache
var depthResults = await client.GetOrderBook("BNBBTC", true, 100);
//Populate our depth cache
depthResults.Asks.ForEach(a =>
{
    if (a.Quantity != 0.00000000M)
    {
        bnbBtcDepthCache.Asks.Add(a.Price, a.Quantity);
    }
});
depthResults.Bids.ForEach(a =>
{
    if (a.Quantity != 0.00000000M)
    {
        bnbBtcDepthCache.Bids.Add(a.Price, a.Quantity);
    }
});

// Store the last update
long lastUpdateId = 0;
using (var binanceWebSocketClient = new BinanceWebSocketClient(client))
{
    binanceWebSocketClient.ConnectToDepthWebSocket("BNBBTC", data =>
    {
        if (lastUpdateId < data.UpdateId)
        {
            data.BidDepthDeltas.ForEach((bd) =>
            {
                CorrectlyUpdateDepthCache(bd, bnbBtcDepthCache.Bids);
            });
            data.AskDepthDeltas.ForEach((ad) =>
            {
                CorrectlyUpdateDepthCache(ad, bnbBtcDepthCache.Asks);
            });
        }
        lastUpdateId = data.UpdateId;
        System.Console.Clear();
        System.Console.WriteLine($"{JsonConvert.SerializeObject(bnbBtcDepthCache, Formatting.Indented)}");
        System.Console.SetWindowPosition(0, 0);
    });

    Thread.Sleep(480000);
}
```
