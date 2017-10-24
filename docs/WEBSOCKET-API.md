# WebSocket API
This page outlines all of the general Websocket API endpoints available via implementations from `IBinanceWebSocketClient`.

## WebSocket  Endpoints
[Binance Documentation](https://www.binance.com/restapipub.html#wss-endpoint)

All endpoints are access the same way, with the only difference being the data returned, outside of arguments provided.

```c#
Guid ConnectToKlineWebSocket(string symbol, KlineInterval interval, BinanceWebSocketMessageHandler<BinanceKlineData> messageEventHandler);
```
```c#
Guid ConnectToDepthWebSocket(string symbol, BinanceWebSocketMessageHandler<BinanceDepthData> messageEventHandler);
```
```c#
Guid ConnectToTradesWebSocket(string symbol, BinanceWebSocketMessageHandler<BinanceAggregateTradeData> messageEventHandler);
```
```c#
Task<Guid> ConnectToUserDataWebSocket(UserDataWebSocketMessages userDataMessageHandlers);
```

> Sample Usage - Disposable
```c#

using (var binanceWebSocketClient = new DisposableBinanceWebSocketClient(binanceClient))
{
    binanceWebSocketClient.ConnectToTradesWebSocket("ETHBTC", data =>
    {
        System.Console.WriteLine($"KlineCall: {JsonConvert.SerializeObject(data)}");
    });
}
```

> Sample Usage - Manual
```c#

var binanceWebSocketClient = new InstanceBinanceWebSocketClient(binanceClient))
binanceWebSocketClient.ConnectToTradesWebSocket("ETHBTC", data =>
{
    System.Console.WriteLine($"KlineCall: {JsonConvert.SerializeObject(data)}");
});
```

## Additional API

### `CloseWebSocketInstance(Guid id, bool fromError = false): void`
When opening `WebSockets` you are provided with a unique Guid, which can be used to close individual instances through this method
