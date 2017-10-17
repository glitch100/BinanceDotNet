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
- Dotnet core 2.0
- Binance WebSockets
- `IAPICacheManager` abstraction for providing your own cache or using the build in concrete implementation. _(Currently only one endpoint has caching)_
- Basic console app with examples ready to launch _(provide API keys)_

## Roadmap
- Add in `log4net` logger - 1.5.0
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

## Examples

### Building out a local cache per symbol from the depth WebSocket
The example is mainly 'all in one' so you can see a full runthrough of how it works. In your own implementations you may want to have a cache of only the most recent bids/asks, or perhaps will want the empty quanity/price trades.

You can also calculate volume and more from this cache.

```c#
// Code example of building out a Dictionary local cache for a symbol
// with no expiration on trades/offers.
var localDepthCache = new Dictionary<string, SymbolCacheItem>();
// We want to ignore values with 0 quantity and 0 price
var defaultIgnoreValue = 0.00000000M;
// Standard Func for repeated logic
var tradeFactoryFunc = new Func<List<TradeResponse>, List<Trade>>((lt) =>
{
    return lt.Where(ad => ad.Price != defaultIgnoreValue).Select(ad => new Trade()
    {
        Price = ad.Price,
        Quantity = ad.Quantity,
    }).ToList();
});
using (var binanceWebSocketClient = new BinanceWebSocketClient(client))
{
    binanceWebSocketClient.ConnectToDepthWebSocket("ETHBTC", data =>
    {
        // Check our cache contains this key
        if (localDepthCache.ContainsKey(data.Symbol))
        {
            var entry = localDepthCache[data.Symbol];
            if (entry.CurrentUpdateId < data.UpdateId)
            {
                // Double logic to show the same operation on both. We check it's a valid hit, and then
                // add it to our cache. We do not remove items.
                data.BidDepthDeltas.Where(bd => bd.Price != defaultIgnoreValue && bd.Quantity != defaultIgnoreValue).ToList().ForEach(
                    bd => entry.Bids.Add(new Trade()
                    {
                        Price = bd.Price,
                        Quantity = bd.Quantity
                    }));
                data.AskDepthDeltas.Where(ad => ad.Price != defaultIgnoreValue && ad.Quantity != defaultIgnoreValue).ToList().ForEach(
                    ad => entry.Asks.Add(new Trade()
                    {
                        Price = ad.Price,
                        Quantity = ad.Quantity
                    }));
            }
        }
        else
        {
            // First time hits initialize that symbol entry
            localDepthCache.Add(data.Symbol, new SymbolCacheItem(data.Symbol, data.UpdateId)
            {
                Asks = new List<Trade>(tradeFactoryFunc(data.AskDepthDeltas)),
                Bids = new List<Trade>(tradeFactoryFunc(data.BidDepthDeltas)),
            });
        }
        System.Console.WriteLine($"Depth Cal JSON: {JsonConvert.SerializeObject(data)}");
    });

    // Just enough time to fill the cache for this example.
    Thread.Sleep(480000);
}
```
