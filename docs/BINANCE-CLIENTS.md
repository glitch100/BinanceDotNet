# Binance Clients

## `BinanceClient: IBinanceClient`

The `BinanceClient` is the main entry point for accessing the Binance Official API. It takes in a `ClientConfiguration` as documented below, or your own configured `IAPIProcessor`, however if not provided it will stand up it's own instance.

`BinanceClient(ClientConfiguration configuration, IAPIProcessor apiProcessor = null)`

### `ClientConfiguration`
- `ApiKey: string` - Your API Key
- `SecretKey: string` - Your Secret Key
- `EnableRateLimiting: bool` - Whether you want RateLimiting enabled (_false by default_)
- `Logger: ILogger` - Your own version of the `NLog.ILogger` interface
- `CacheTime: TimeSpan` - The time that cache entries should expire

## `BinanceWebSocketClient: IBinanceWebSocketClient, IDisposable`

The `BinanceWebSocketClient` is much more lightweight than the `BinanceClient`, however it needs an instance of the `BinanceClient` for use in various WebSocket calls.

`BinanceWebSocketClient(IBinanceClient, ILogger = null)`
