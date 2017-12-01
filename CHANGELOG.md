# BinanceDotNet Changelog

**Release Date: 11/30/2016**
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

