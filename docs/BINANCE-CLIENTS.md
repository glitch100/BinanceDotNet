# Binance Clients

## `BinanceClient: IBinanceClient`

The `BinanceClient` takes in a `ClientConfiguration` object, and a concrete implementation of the `IAPICacheManager` interface.

### `ClientConfiguration`
- `ApiKey: string` - Your API Key
- `SecretKey: string` - Your Secret Key
- `EnableRateLimiting: bool` - Whether you want RateLimiting enabled (_false by default_)
- `Logger: ILogger` - Your own version of the `NLog.ILogger` interface
- `CacheTime: TimeSpan` - The time that cache entries should expire

The API Key and Secret Key must be provided to avoid an exception, where as the `EnableRateLimiting` `bool` argument, which defaults to false, can allow you to have a rate limited API, to prevent excessive calls.


## `BinanceWebSocketClient: IBinanceWebSocketClient, IDisposable`

The `BinanceWebSocketClient` is much more lightweight than the `BinanceClient`, however it needs an instance of the `BinanceClient` for use in various WebSocket calls.

`BinanceWebSocketClient(IBinanceClient, ILogger = null)`
