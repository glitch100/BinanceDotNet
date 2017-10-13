using System;
using System.Linq;
using System.Web;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Request;
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
        };


        /// <summary>
        /// Defaults to V1
        /// </summary>

        /// <summary>
        /// Defaults to binance domain (https)
        /// </summary>
        internal static string BaseUrl = "https://www.binance.com/api";

        private static string Prefix { get; } = $"{BaseUrl}";

        public static class General
        {
            internal static string ApiVersion = "v1";

            /// <summary>
            /// Test connectivity to the Rest API.
            /// </summary>
            public static BinanceEndpointData TestConnectivity => new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/ping"), EndpointSecurityType.None);

            /// <summary>
            /// Test connectivity to the Rest API and get the current server time.
            /// </summary>
            public static BinanceEndpointData ServerTime => new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/time"), EndpointSecurityType.None);
        }

        public static class MarketData
        {
            internal static string ApiVersion = "v1";

            /// <summary>
            /// Gets the order book with all bids and asks
            /// </summary>
            public static BinanceEndpointData OrderBook(string symbol, int limit, bool useCache = false)
            {
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/depth?symbol={symbol}&limit={limit}"), EndpointSecurityType.None, useCache);
            }

            /// <summary>
            /// Get compressed, aggregate trades. Trades that fill at the time, from the same order, with the same price will have the quantity aggregated.
            /// </summary>
            public static BinanceEndpointData CompressedAggregateTrades(GetCompressedAggregateTradesRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/aggTrades?{queryString}"), EndpointSecurityType.None);
            }

            /// <summary>
            /// Kline/candlestick bars for a symbol. Klines are uniquely identified by their open time.
            /// </summary>
            public static BinanceEndpointData KlineCandlesticks(GetKlinesCandlesticksRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/klines?{queryString}"), EndpointSecurityType.None);
            }

            /// <summary>
            /// 24 hour price change statistics.
            /// </summary>
            public static BinanceEndpointData DayPriceTicker(string symbol)
            {
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/ticker/24hr?symbol={symbol}"),
                    EndpointSecurityType.None);
            }

            /// <summary>
            /// Latest price for all symbols.
            /// </summary>
            public static BinanceEndpointData AllSymbolsPriceTicker => new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/ticker/allPrices"), EndpointSecurityType.ApiKey);

            /// <summary>
            /// Best price/qty on the order book for all symbols.
            /// </summary>
            public static BinanceEndpointData SymbolsOrderBookTicker => new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/ticker/allBookTickers"), EndpointSecurityType.ApiKey);
        }

        public static class Account
        {
            internal static string ApiVersion = "v3";

            public static BinanceEndpointData NewOrder(CreateOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/order?{queryString}"), EndpointSecurityType.Signed);
            }            
            public static BinanceEndpointData NewOrderTest(CreateOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/order/test?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData QueryOrder(QueryOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/order?{queryString}"), EndpointSecurityType.Signed);
            }
            public static BinanceEndpointData CancelOrder(CancelOrderRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/order?{queryString}"), EndpointSecurityType.Signed);
            }
            public static BinanceEndpointData CurrentOpenOrders(CurrentOpenOrdersRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/openOrders?{queryString}"), EndpointSecurityType.Signed);
            }
            public static BinanceEndpointData AllOrders(AllOrdersRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/allOrders?{queryString}"), EndpointSecurityType.Signed);
            }

            public static BinanceEndpointData AccountInformation => new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/account"), EndpointSecurityType.Signed);

            public static BinanceEndpointData AccountTradeList(AllTradesRequest request)
            {
                var queryString = GenerateQueryStringFromData(request);
                return new BinanceEndpointData(new Uri($"{Prefix}/{ApiVersion}/myTrades?{queryString}"), EndpointSecurityType.Signed);
            }
        }

        private static string GenerateQueryStringFromData(IRequest request)
        {
            if (request == null)
            {
                throw new Exception("No request data provided - query string can't be created");
            }
            //TODO: Refactor to not require double JSON loop
            var obj = (JObject)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(request, _settings));

            return String.Join("&", obj.Children()
                .Cast<JProperty>()
                .Select(j => j.Name + "=" + HttpUtility.UrlEncode(j.Value.ToString())));
        }
    }
}
