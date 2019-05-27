# BinanceDotNet Changelog

## 4.9.0
- Do not require handlers in websocket client

## 4.8.0
**Release Date: 21/01/2019**
- Additional `ExchangeInfoSymbolFilterType` to fix API issues

## 4.7.0
**Release Date: 10/01/2019**
- Add MAX_NUM_ORDERS, ICEBERG_PARTS to `ExchangeInfoSymbolFilterType`

## 4.6.2
**Release Date: 7/20/2018**
- Additional changes with last release

## 4.6.1
**Release Date: 7/20/2018**
- Added two new WebSocket endpoints:
   - Individual Symbol Ticker
   - All Market Symbol Ticker
- Fix Filter Type for Symbols

## 4.6.0
**Release Date: 6/20/2018**
- Fixed SSL Issues
- More type corrections

## 4.5.0
**Release Date: 5/2/2018**
- Added `IsAlive` for WebSockets
- Adjusted SSL Security Types

**Release Date: 3/17/2018**
## 5.0.0
- Made `RequestClient` non-static and changed `HttpClient` to static for `RequestClient`

**Release Date: 3/17/2018**
## 4.4.0
- Added `DefaultReceiveWindow` to `ClientConfiguration`. Defaults to `5000`

**Release Date: 3/17/2018**
## 4.3.0
- Added fix for WAPI Url with extra trailing space

**Release Date: 2/23/2018**
## 4.2.4
- Fixed data model issues around `AbstractBinanceWebSocketClient`
- Fixes for `string` -> `decimal`
- Added Combined and Partial WebSocket methods

**Release Date: 2/21/2018**
## 4.2.3
- Fixed bugs around deserialized properties failing
- Fixed User data web socket issue

**Release Date: 2/10/2018**
## 4.2.2
- Changed URL to match new API spec
- Removed unnecessary guards
- Added System Status API endpoint and reponse type

**Release Date: 2/6/2018**
## 4.2.1
- Fixed inaccurate websocket endpoint for agg trades
- Removed unnecessary guards

**Release Date: 2/2/2018**
## 4.2.0
- Adjusted `CreateOrder` request to allow you to specify what type of response you want
- Removed `TimeInForce` as required param from New order and Test order
- Changed `Quantity` to be `decimal`
- Added additional Symbol pairs to static class
- Expanded the number of order types available
- Added exchange info endpoint

**Release Date: 1/23/2018**
## 4.0.1
- Fixed bug around QueryOrder Http method

**Release Date: 1/21/2018**
## 4.0.0
- Introduced interface folder seperation
- Fix to Timestamp logic for non dotnetstandard projects
- Changes to types that shouldn't been strings
- Backwards compatible support for older `.NET` versions
- Fixes to some models
- Adjusting some other types
- Removing unused code
_BREAKING CHANGES_
- Removed `NLog` support
- Moved a number of files

**Release Date: 01/16/2018**
## 3.1.0
- Made properties in requests nullable to prevent malformed requests when params not provided
- Additional logging
- Fix for `OrderStatus` issue
- `catch` for deserializing response

**Release Date: 01/11/2018**
## 3.0.1
- Fixed `OrderStatus` enum bug. Issue #7

**Release Date: 11/30/2017**
## 3.0.0
>BREAKING CHANGE
- New trading symbols added
- Added in all new endpoints, for `WithdrawHistory`, `DepositHistory`, `DepositAddress` and `Withdraw` all of which are `SIGNED`
- New models

**Release Date: 11/08/2017**
## 2.2.4
- Added new trading pairs for `NULS`, `RCN`, `KMD`, `POWR` and `VEN`
- Added more test coverage to Market Data call

**Release Date: 11/06/2017**
## 2.2.3
- Added new trading pairs for `BNB/USDT` and `BNB/VEN` - *hooray*

**Release Date: 11/03/2017**
## 2.2.2
- Added new trading pairs for `MOD` and `ENJ`g

**Release Date: 11/02/2017**
## 2.2.1
- Updated `TradingPairSymbols` `static` class with `YOYO`, `ARK`, `XRP` and `POWR`. `BTC` and `ETH` pairs.
- Added more unit tests
- Added defensive logic around async api key additions

**Release Date: 10/31/2017**
## 2.2.0
- Added `TradingPairSymbols` `static` class. Access all symbols that Binance list with ease.

**Release Date: 10/26/2017**
## 2.1.0
- Added in richer exceptions based on contextual errors:
  - `BinanceServerException`
  - `BinanceBadRequestException`
  - `BinanceTimeoutException`
- Added in example code to README and `ExampleProgram.cs`

## 2.0.0
> Breaking Change
- Building out 2 instances of the `BinanceWebSocket` system from a base `Abtract*` class. You can now have the `Disposable*` instance, which works the same as is, or you can have a manual one, which requires manual management of the the individual `WebSockets` with the `InstanceBinanceWebSocketClient`.
- Changed `BinanceClient` requirements

**Release Date: 10/19/2017**
## 1.6.0
- Start of unit test coverage
- Abstraction
- Changed `BinanceClient` requirements

**Release Date: 10/19/2017**
## 1.5.1
- Added `ResultTransformations` `static` class; used to transform API results into meaningful data
- Adjusted examples

**Release Date: 10/18/2017**
## 1.5.0
- Added `NLog` support, and sample `nlog.config`
- Added more advanced example of Kline cache
- Added basic examples
- Minor changes

**Release Date: 10/17/2017**
## 1.4.2
- Adjusted Kline cache models for example

**Release Date: 10/17/2017**
## 1.4.1
- Adjusted Depth cache models for example

**Release Date: 10/17/2017**
## 1.4.0
- Added in API Cache concrete implementation
- Auditing areas for cache implementation
- Added `Examples` section on `README.md` for local cache and into `Program.cs` for demo
- Added new models for cache usage
- Added more comments to code

**Release Date: 10/16/2017**
## 1.3.0
- Added in documentation to `/docs` in repo root
- Added in Test Order endpoints
- Adjusted method name

**Release Date: 10/16/2017**
## 1.2.0
- Added in WebSockets endpoints via the new `BinanceWebSocketClient`
- Added in UserDataStream endpoints
- Type changes
- Additional abstractions around new types
- Enum util added

**Release Date: 10/13/2017**
## 1.0.1
- Type changes in responses
- Adjusted RateLimiter logic

**Release Date: 10/13/2017**
## 1.0.0
- Provided initial commits
- All basic `REST` endpoints
- Rate Limiting
- `IAPICache` abstraction
- Strongly typed requests

