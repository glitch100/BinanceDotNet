# REST API
This page outlines all of the general REST API endpoints available via the `IBinanceClient`.

## User Stream Endpoints
[Binance Documentation](https://www.binance.com/restapipub.html#user-content-user-data-stream-endpoints)

### `StartUserDataStream:UserDataStreamResponse`
Starts a User Data stream
```c#
IBinanceClient.StartUserDataStream()
```

### `KeepAliveUserDataStream:void`
Pings a User Data stream to keep it alive. Despite this method returning the `UserDataStreamResponse` type the API currently doesn't return anything.
```c#
IBinanceClient.KeepAliveUserDataStream(string userDataListenKey)
```

### `CloseUserDataStream:void`
Pings a User Data stream to keep it alive. Despite this method returning the `UserDataStreamResponse` type the API currently doesn't return anything.
```c#
IBinanceClient.KeepAliveUserDataStream(string userDataListenKey)
```

## General Endpoints
[Binance Documentation](https://www.binance.com/restapipub.html#user-content-general-endpoints)
### `TestConnectivity:EmptyResponse`
Tests connectivity to the API
```c#
IBinanceClient.TestConnectivity()
```

### `GetServerTime:ServerTimeResponse`
Gets the server time from the API, for the purposes of syncing
```c#
IBinanceClient.GetServerTime()
```

## Market Data Endpoints
[Binance Documentation](https://www.binance.com/restapipub.html#user-content-general-endpoints)

### `GetOrderBook:OrderBookResponse`
Returns the order book for a provided symbol. Assuming a Cache was provided to the client, the call can go via it.
```c#
IBinanceClient.GetOrderBook(string symbol, bool useCache = false, int limit = 100)
```

### `GetCompressedAggregateTrades:List<CompressedAggregateTradeResponse>`
Get compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
```c#
IBinanceClient.GetCompressedAggregateTrades(GetCompressedAggregateTradesRequest request)
```

### `GetKlinesCandlesticks:List<KlineCandleStickResponse>`
Kline/candlestick bars for a symbol. Klines are uniquely identified by their open time.
```c#
IBinanceClient.GetKlinesCandlesticks(GetKlinesCandlesticksRequest request)
```

### `GetDailyTicker:SymbolPriceChangeTickerResponse`
24 hour price change statistics on the provided symbol
```c#
IBinanceClient.GetDailyTicker(string symbol)
```

### `GetSymbolsPriceTicker:List<SymbolPriceResponse>`
Latest price for all symbols.
```c#
IBinanceClient.GetSymbolsPriceTicker()
```

### `GetSymbolsPriceTicker:List<SymbolPriceResponse>`
Latest price for all symbols.
```c#
IBinanceClient.GetSymbolOrderBookTicker()
```

## Account Endpoints
[Binance Documentation](https://www.binance.com/restapipub.html#user-content-account-endpoints)

### `CreateOrder:CreateOrderResponse`
Send in a new order.
```c#
IBinanceClient.CreateOrder(CreateOrderRequest request)
```

### `CreateTestOrder:CreateTestOrder`
Send in a new test order.
```c#
IBinanceClient.CreateTestOrder(CreateOrderRequest request)
```

### `QueryOrder:OrderResponse`
Check an order's status.
```c#
IBinanceClient.QueryOrder(QueryOrderRequest request, int receiveWindow = 5000)
```

### `CancelOrder:CancelOrderResponse`
Check an order's status.
```c#
IBinanceClient.CancelOrder(CancelOrderRequest request, int receiveWindow = 5000)
```

### `GetCurrentOpenOrders:List<OrderResponse>`
Get current open orders on the account
```c#
IBinanceClient.GetCurrentOpenOrders(CurrentOpenOrdersRequest request, int receiveWindow = 5000)
```

### `GetAllOrders:List<OrderResponse>`
Get all open orders on the account; active, cancelled, or filled.
```c#
IBinanceClient.GetAllOrders(AllOrdersRequest request, int receiveWindow = 5000)
```

### `GetAccountInformation:AccountInformationResponse`
Get current account information.
```c#
IBinanceClient.GetAccountInformation(int receiveWindow = 5000)
```

### `GetAccountTrades:List<AccountTradeReponse>`
Get trades for a specific account and symbol.
```c#
IBinanceClient.GetAccountTrades(AllTradesRequest request, int receiveWindow = 5000)
```
