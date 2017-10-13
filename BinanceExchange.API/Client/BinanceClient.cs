using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceExchange.API.Caching;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;
using BinanceExchange.API.Utility;

namespace BinanceExchange.API.Client
{
    /// <summary>
    /// The Binance Client used to communicate with the official Binance API. For more information on underlying API calls see:
    /// https://www.binance.com/restapipub.html
    /// </summary>
    public class BinanceClient : IBinanceClient
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private readonly APIProcessor _apiProcessor;

        public BinanceClient(ClientConfiguration configuration, IAPICache apiCache = null)
        {
            _apiKey = configuration.ApiKey;
            _secretKey = configuration.SecretKey;
            RequestClient.SetRateLimiting(configuration.EnableRateLimiting);
            RequestClient.SetAPIKey(_apiKey);
            _apiProcessor = new APIProcessor(_apiKey, _secretKey, apiCache);
        }

        /// <summary>
        /// Test the connectivity to the API
        /// </summary>
        public async Task<EmptyResponse> TestConnectivity()
        {
            return await _apiProcessor.ProcessGetRequest<EmptyResponse>(Endpoints.General.TestConnectivity);
        }

        /// <summary>
        /// Get the current server time (UTC)
        /// </summary>
        /// <returns><see cref="ServerTimeResponse"/></returns>
        public async Task<ServerTimeResponse> GetServerTime()
        {
            return await _apiProcessor.ProcessGetRequest<ServerTimeResponse>(Endpoints.General.ServerTime);
        }

        /// <summary>
        /// Gets the current order book for the specified symbol
        /// </summary>
        /// <param name="symbol">The symbole to retrieve the order book for</param>
        /// <param name="useCache"></param>
        /// <param name="limit">Amount to request - defaults to 100</param>
        /// <returns></returns>
        public async Task<OrderBookResponse> GetOrderBook(string symbol, bool useCache = false, int limit = 100)
        {
            Guard.AgainstNull(symbol);
            if (limit > 100)
            {
                throw new ArgumentException("When requesting the order book, you can't request more than 100 at a time.", nameof(limit));
            }
            return await _apiProcessor.ProcessGetRequest<OrderBookResponse>(Endpoints.MarketData.OrderBook(symbol, limit, useCache));
        }

        /// <summary>
        /// Gets the Compressed aggregated trades in the specified window
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CompressedAggregateTradeResponse>> GetCompressedAggregateTrades(GetCompressedAggregateTradesRequest request)
        {
            Guard.AgainstNull(request.Symbol);
            Guard.AgainstDateTimeMin(request.StartTime);
            Guard.AgainstDateTimeMin(request.EndTime);
            if (request.Limit == 0 || request.Limit > 500) 
            {
                request.Limit = 500;
            }

            return await _apiProcessor.ProcessGetRequest<List<CompressedAggregateTradeResponse>>(Endpoints.MarketData.CompressedAggregateTrades(request));
        }

        /// <summary>
        /// Gets the Klines/Candlesticks for the provided request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<KlineCandleStickResponse>> GetKlinesCandlesticks(GetKlinesCandlesticksRequest request)
        {
            Guard.AgainstNull(request.Symbol);
            Guard.AgainstDateTimeMin(request.StartTime);
            Guard.AgainstDateTimeMin(request.EndTime);
            if (request.Limit == 0 || request.Limit > 500) 
            {
                request.Limit = 500;
            }

            return await _apiProcessor.ProcessGetRequest<List<KlineCandleStickResponse>>(Endpoints.MarketData.KlineCandlesticks(request));
        }

        /// <summary>
        /// Gets the Daily price ticker for the provided symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<SymbolPriceChangeTickerResponse> GetDailyTicker(string symbol)
        {
            Guard.AgainstNull(symbol);

            return await _apiProcessor.ProcessGetRequest<SymbolPriceChangeTickerResponse>(Endpoints.MarketData.DayPriceTicker(symbol));
        }

        /// <summary>
        /// Gets all prices for all symbols
        /// </summary>
        /// <returns></returns>
        public async Task<List<SymbolPriceResponse>> GetSymbolsPriceTicker()
        {
             return await _apiProcessor.ProcessGetRequest<List<SymbolPriceResponse>>(Endpoints.MarketData.AllSymbolsPriceTicker);
        }

        /// <summary>
        /// Gets the best and quantity on the order book for all symbols
        /// </summary>
        /// <returns></returns>
        public async Task<List<SymbolOrderBookResponse>> GetSymbolOrderBookTicker()
        {
             return await _apiProcessor.ProcessGetRequest<List<SymbolOrderBookResponse>>(Endpoints.MarketData.SymbolsOrderBookTicker);
        }

        /// <summary>
        /// Creates an order based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="CreateOrderRequest"/> that is used to define the order</param>
        /// <returns></returns>
        public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
        {
            Guard.AgainstNull(request.Symbol);
            Guard.AgainstNull(request.Side);
            Guard.AgainstNull(request.Type);
            Guard.AgainstNull(request.TimeInForce);
            Guard.AgainstNull(request.Quantity);
            Guard.AgainstNull(request.Price);

            return await _apiProcessor.ProcessPostRequest<CreateOrderResponse>(Endpoints.Account.NewOrder(request));
        }

        /// <summary>
        /// Queries an order based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="QueryOrderRequest"/> that is used to define the order</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<OrderResponse> QueryOrder(QueryOrderRequest request, int receiveWindow = 5000)
        {
            Guard.AgainstNull(request.Symbol);
      
            return await _apiProcessor.ProcessPostRequest<OrderResponse>(Endpoints.Account.QueryOrder(request), receiveWindow);
        }

        /// <summary>
        /// Cancels an order based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="CancelOrderRequest"/> that is used to define the order</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request, int receiveWindow = 5000)
        {
            Guard.AgainstNull(request.Symbol);
      
            return await _apiProcessor.ProcessDeleteRequest<CancelOrderResponse>(Endpoints.Account.CancelOrder(request), receiveWindow);
        }

        /// <summary>
        /// Queries all orders based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="CurrentOpenOrdersRequest"/> that is used to define the orders</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<List<OrderResponse>> GetCurrentOpenOrders(CurrentOpenOrdersRequest request, int receiveWindow = 5000)
        {
            Guard.AgainstNull(request.Symbol);
      
            return await _apiProcessor.ProcessGetRequest<List<OrderResponse>>(Endpoints.Account.CurrentOpenOrders(request), receiveWindow);
        }

        /// <summary>
        /// Queries all orders based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="AllOrdersRequest"/> that is used to define the orders</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<List<OrderResponse>> GetAllOrders(AllOrdersRequest request, int receiveWindow = 5000)
        {
            Guard.AgainstNull(request.Symbol);
      
            return await _apiProcessor.ProcessGetRequest<List<OrderResponse>>(Endpoints.Account.AllOrders(request), receiveWindow);
        }

        /// <summary>
        /// Queries the current account information
        /// </summary>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<AccountInformationResponse> GetAccountInformation(int receiveWindow = 5000)
        {
            return await _apiProcessor.ProcessGetRequest<AccountInformationResponse>(Endpoints.Account.AccountInformation, receiveWindow);
        }

        /// <summary>
        /// Queries the all trades against this account
        /// </summary>
        /// <param name="request"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<List<AccountTradeReponse>> GetAccountTrades(AllTradesRequest request, int receiveWindow = 5000)
        {
            return await _apiProcessor.ProcessGetRequest<List<AccountTradeReponse>>(Endpoints.Account.AccountTradeList(request), receiveWindow);
        }
    }
}
