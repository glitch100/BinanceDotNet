# BinanceDotNet
## C# Wrapper for the official Binance exchange API

This repository provides a C# wrapper for the official Binance API, and provides rate limiting features _(set to 10 by 10 out the box)_, a `IAPICache` interface to allow users to provide their own cache implementations, all `REST` endpoints covered, and a best practice solution coupled with strongly typed responses and requests.

## Features
- Rate limiting, with 10 requests in 10 seconds _(disabled by default)_
- `IAPICache` abstraction for providing your own cache. _(Currently only one endpoint has caching)_
- Basic console app with examples ready to launch _(provide API keys)_


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

## Roadmap
- Add in WebSocket API
- Provide additional CacheLayer
- Provide Builder support for queries
- Build out Unit Test support
- Abstract out the HttpClient
- Add in `log4net` logger


