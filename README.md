# BinanceDotNet <img src="https://i.imgur.com/x2YPVe6.png" width="56" />

## C# Wrapper for the official Binance exchange API
<img src="https://img.shields.io/nuget/dt/BinanceDotNet.svg" />
<img src="https://img.shields.io/nuget/v/BinanceDotNet.svg" />

This repository provides a C# wrapper for the official Binance API, and provides rate limiting features _(set to 10 by 10 out the box)_, a `IAPICacheManager` interface to allow users to provide their own cache implementations, all `REST` endpoints covered, and a best practice solution coupled with strongly typed responses and requests. It is built on the latest .NET Framework and in .NET Core

Feel free to raise issues and Pull Request to help improve the library. If you found this API useful, and you wanted to give back feel free to sign up to Binance via my referral link [**here**](https://www.binance.com/?ref=10886925).

## Documentation
- [Binance Clients](/docs/BINANCE-CLIENTS.md)
- [REST API Calls](/docs/REST-API.md)
- [WebSocket API Calls](/docs/WEBSOCKET-API.md)

## Features
- Simple, Configurable, Extendable
- Rate limiting, with 10 requests in 10 seconds _(disabled by default)_
- `NLog` support
- dotnet core 2.0
- Binance WebSockets
- Unit test coverage (_in progress_)
- `IAPICacheManager` abstraction for providing your own cache or using the build in concrete implementation. _(Currently only one endpoint has caching)_
- Console app with examples ready to launch _(provide API keys)_

## Examples
More examples are available to play around with within the repositorys Console application which can be found [here](/BinanceExchange.Console/ExampleProgram.cs). Otherwise there are some examples around utilising both `WebSockets` and `REST` API in the `Usage` section below.

## Roadmap
Work will continue on this API wrapper over the coming months adding and extending out the number of features that the `BinanceDotNet` library has. Please raise issues for new features desired

- Start building out Unit Test support - >~2.1.0
- Provide Builder support for queries - 2.5.0~
- Abstract out the HttpClient - 3.0.0

## Contributing to `BinanceDotNet`
```git
git clone git@github.com:glitch100/BinanceDotNet.git
```
- Navigate to `ExampleProgram.cs`
- Add your own Private and Secret keys
- Play around with the API

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
// Build out a client, provide a logger, and more configuration options, or even your own APIProcessor implementation
var client = new BinanceClient(new ClientConfiguration()
{
    ApiKey = "YOUR_API_KEY",
    SecretKey = "YOUR_SECRET_KEY",
});

//You an also specify symbols like this.
var desiredSymbol = TradingPairSymbols.BNBPairs.BNB_BTC,

IReponse response = await client.GetCompressedAggregateTrades(new GetCompressedAggregateTradesRequest(){
  Symbol = "BNBBTC",
  StartTime = DateTime.UtcNow().AddDays(-1),
  EndTime = Date.UtcNow(),
});
```

### Creating a WebSocket Client
For WebSocket endpoints, just instantiate the `BinanceClient`, and provide it into the `BinanceWebSocketClient`
You can use a `using` block or manual management.
```c#
var client = new BinanceClient(new ClientConfiguration()
{
    ApiKey = "YOUR_API_KEY",
    SecretKey = "YOUR_SECRET_KEY",
});


// Manual management
var manualWebSocketClient = new InstanceBinanceWebSocketClient(client);
var socketId = binanceWebSocketClient.ConnectToDepthWebSocket("ETHBTC", data =>
{
    System.Console.WriteLine($"DepthCall: {JsonConvert.SerializeObject(data)}");
});
manualWebSocketClient.CloseWebSocketInstance(socketId);


// Disposable and managed
using (var binanceWebSocketClient = new DisposableBinanceWebSocketClient(client))
{
    binanceWebSocketClient.ConnectToDepthWebSocket("ETHBTC", data =>
    {
        System.Console.WriteLine($"DepthCall: {JsonConvert.SerializeObject(data)}");
    });

    Thread.Sleep(180000);
}
```

### Error Handling
The Binance API provides rich exceptions based on different error types. You can decorate calls like this if you would like to handle the various exceptions.

```c#
// Firing off a request and catching all the different exception types.
try
{
    accountTrades = await client.GetAccountTrades(new AllTradesRequest()
    {
        FromId = 352262,
        Symbol = "ETHBTC",
    });
}
catch (BinanceBadRequestException badRequestException)
{

}
catch (BinanceServerException serverException)
{

}
catch (BinanceTimeoutException timeoutException)
{

}
catch (BinanceException unknownException)
{
}
```

### Building out a local cache per symbol from the depth WebSocket
The example is mainly 'all in one' so you can see a full runthrough of how it works. In your own implementations you may want to have a cache of only the most recent bids/asks, or perhaps will want the empty quanity/price trades.

You can also calculate volume and more from this cache. The following code is _partial_ from the `ExampleProgram.cs`.

```c#
private static async Task<Dictionary<string, DepthCacheObject>> BuildLocalDepthCache(IBinanceClient client)
{
    // Code example of building out a Dictionary local cache for a symbol using deltas from the WebSocket
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

    // Store the last update from our result set;
    long lastUpdateId = depthResults.LastUpdateId;
    using (var binanceWebSocketClient = new DisposableBinanceWebSocketClient(client))
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

        Thread.Sleep(8000);
    }
    return localDepthCache;
}
```

### Result Transformations
You can use the data returned from above to utilise the `ResultTransformations` `static` class, to transform data returned from the API into more meaningful, known shapes, such as Volume etc.

```c#
// This builds a local depth cache from an initial call to the API and then continues to fill
// the cache with data from the WebSocket
var localDepthCache = await BuildLocalDepthCache(client);
// Build the Buy Sell volume from the results
var volume = ResultTransformations.CalculateTradeVolumeFromDepth("BNBBTC", localDepthCache);
```

