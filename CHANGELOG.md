# BinanceDotNet Changelog

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

