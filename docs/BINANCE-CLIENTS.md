# Binance Clients

## `BinanceClient: IBinanceClient`

The `BinanceClient` takes in a `ClientConfiguration` object, and a concrete implementation of the `IAPICacheManager` interface.

### `ClientConfiguration`
- `ApiKey: string`
- `SecretKey: string`
- `EnableRateLimiting: bool`
- `Logger: ILogger`

The API Key and Secret Key must be provided to avoid an exception, where as the `EnableRateLimiting` `bool` argument, which defaults to false, can allow you to have a rate limited API, to prevent excessive calls.


## `BinanceWebSocketClient: IBinanceWebSocketClient, IDisposable`

The `BinanceWebSocketClient` is much more lightweight than the `BinanceClient`, however it needs an instance of the `BinanceClient` for use in various WebSocket calls.

`BinanceWebSocketClient(IBinanceClient, ILogger = null)`
