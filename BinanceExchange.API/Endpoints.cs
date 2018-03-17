using System;
using System.Linq;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Request.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace BinanceExchange.API
{
    public static class Endpoints
    {

        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            FloatParseHandling = FloatParseHandling.Decimal
        };

        /// <summary>
        /// Defaults to V1
        /// </summary>

        /// <summary>
        /// Defaults to API binance domain (https)
        /// </summary>
        internal static string APIBaseUrl = "https://api.binance.com/api";

        /// <summary>
        /// Defaults to WAPI binance domain (https)
        /// </summary>
        internal static string WAPIBaseUrl = "https://api.binance.com/wapi";

        private static string APIPrefix { get; } = $"{APIBaseUrl}";
        private static string WAPIPrefix { get; } = $"{WAPIBaseUrl}";

        public static class UserStream
        {
            internal static string ApiVersion = "v1";

            /// <summary>
            /// Start a user data stream
            /// </summary>
            public static BinanceEndpointData StartUserDataStream => new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/userDataStream"), EndpointSecurityType.ApiKey);

            /// <summary>
            /// Ping a user data stream to prevent a timeout
            /// </summary>
            public static BinanceEndpointData KeepAliveUserDataStream(string listenKey)
            {
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/userDataStream?listenKey={listenKey}"),
                    EndpointSecurityType.ApiKey);
            }

            /// <summary>
            /// Close a user data stream to prevent
            /// </summary>
            public static BinanceEndpointData CloseUserDataStream(string listenKey)
            {
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/userDataStream?listenKey={listenKey}"),
                    EndpointSecurityType.ApiKey);
            }
        }

        public static class General
        {
            internal static string ApiVersion = "v1";

            /// <summary>
            /// Test connectivity to the Rest API.
            /// </summary>
            public static BinanceEndpointData TestConnectivity => new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/ping"), EndpointSecurityType.None);

            /// <summary>
            /// Test connectivity to the Rest API and get the current server time.
            /// </summary>
            public static BinanceEndpointData ServerTime => new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/time"), EndpointSecurityType.None);

            /// <summary>
            /// Current exchange trading rules and symbol information.
            /// </summary>
            public static BinanceEndpointData ExchangeInfo => new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/exchangeInfo"), EndpointSecurityType.None);

        }

        public static class MarketData
        {
            internal static string ApiVersion = "v1";

            /// <summary>
            /// Gets the order book with all bids and asks
            /// </summary>
            public static BinanceEndpointData OrderBook(string symbol, int limit, bool useCache = false)
            {
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/depth?symbol={symbol}&limit={limit}"), EndpointSecurityType.None, useCache);
            }

            /// <summary>
            /// Get compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
            /// </summary>
            public static BinanceEndpointData CompressedAggregateTrades(GetCompressedAggregateTradesRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/aggTrades?{queryString}"), EndpointSecurityType.None);
            }

            /// <summary>
            /// Kline/candlestick bars for a symbol. Klines are uniquely identified by their open time.
            /// </summary>
            public static BinanceEndpointData KlineCandlesticks(GetKlinesCandlesticksRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/klines?{queryString}"), EndpointSecurityType.None);
            }

            /// <summary>
            /// 24 hour price change statistics.
            /// </summary>
            public static BinanceEndpointData DayPriceTicker(string symbol)
            {
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/ticker/24hr?symbol={symbol}"),
                    EndpointSecurityType.None);
            }

            /// <summary>
            /// Latest price for all symbols.
            /// </summary>
            public static BinanceEndpointData AllSymbolsPriceTicker => new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/ticker/allPrices"), EndpointSecurityType.ApiKey);

            /// <summary>
            /// Best price/qty on the order book for all symbols.
            /// </summary>
            public static BinanceEndpointData SymbolsOrderBookTicker => new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/ticker/allBookTickers"), EndpointSecurityType.ApiKey);
        }

        public static class Account
        {
            internal static string ApiVersion = "v3";

            public static BinanceEndpointData NewOrder(CreateOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/order?{queryString}"), EndpointSecurityType.Signed);
            }            
            public static BinanceEndpointData NewOrderTest(CreateOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/order/test?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData QueryOrder(QueryOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/order?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData CancelOrder(CancelOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/order?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData CurrentOpenOrders(CurrentOpenOrdersRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/openOrders?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData AllOrders(AllOrdersRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/allOrders?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData AccountInformation => new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/account"), EndpointSecurityType.Signed);

            public static BinanceEndpointData AccountTradeList(AllTradesRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{APIPrefix}/{ApiVersion}/myTrades?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData Withdraw(WithdrawRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{WAPIPrefix}/{ApiVersion}/withdraw.html?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData DepositHistory(FundHistoryRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{WAPIPrefix}/{ApiVersion}/depositHistory.html?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData WithdrawHistory(FundHistoryRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{WAPIPrefix}/{ApiVersion}/withdrawHistory.html?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData DepositAddress(DepositAddressRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{WAPIPrefix}/{ApiVersion}/depositAddress.html?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData SystemStatus()
            {
                return new BinanceEndpointData(new Uri($"{WAPIPrefix}/{ApiVersion}/systemStatus.html"), EndpointSecurityType.None);
            }
        }

        private static string GenerateQueryStringFromData(IRequest request)
        {
            if (request == null)
            {
                throw new Exception("No request data provided - query string can't be created");
            }

            //TODO: Refactor to not require double JSON loop
            var obj = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(request, _settings), _settings);

            return String.Join("&", obj.Children()
                .Cast<JProperty>()
                .Where(j => j.Value != null)
                .Select(j => j.Name + "=" + System.Net.WebUtility.UrlEncode(j.Value.ToString())));
        }
    }
}
