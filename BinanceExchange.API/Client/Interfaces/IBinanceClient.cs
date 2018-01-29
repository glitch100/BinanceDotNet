using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;

namespace BinanceExchange.API.Client.Interfaces
{
    public interface IBinanceClient
    {
        /// <summary>
        /// Starts a user data stream
        /// </summary>
        /// /// <returns><see cref="UserDataStreamResponse"/></returns>
        Task<UserDataStreamResponse> StartUserDataStream();

        /// <summary>
        /// Pings a user data stream to prevent timeouts
        /// </summary>
        /// <param name="userDataListenKey"></param>
        /// /// <returns><see cref="UserDataStreamResponse"/></returns>
        Task<UserDataStreamResponse> KeepAliveUserDataStream(string userDataListenKey);

        /// <summary>
        /// Closes a user data stream
        /// </summary>
        /// <param name="userDataListenKey"></param>
        /// /// <returns><see cref="UserDataStreamResponse"/></returns>
        Task<UserDataStreamResponse> CloseUserDataStream(string userDataListenKey);

        /// <summary>
        /// Test the connectivity to the API
        /// </summary>
        Task<EmptyResponse> TestConnectivity();

        /// <summary>
        /// Get the current server time (UTC)
        /// </summary>
        /// <returns><see cref="ServerTimeResponse"/></returns>
        Task<ServerTimeResponse> GetServerTime();

        /// <summary>
        /// Gets the current order book for the specified symbol
        /// </summary>
        /// <param name="symbol">The symbole to retrieve the order book for</param>
        /// <param name="useCache"></param>
        /// <param name="limit">Amount to request - defaults to 100</param>
        /// <returns></returns>
        Task<OrderBookResponse> GetOrderBook(string symbol, bool useCache = false, int limit = 100);

        /// <summary>
        /// Gets the Compressed aggregated trades in the specified window
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<CompressedAggregateTradeResponse>> GetCompressedAggregateTrades(GetCompressedAggregateTradesRequest request);

        /// <summary>
        /// Gets the Klines/Candlesticks for the provided request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<KlineCandleStickResponse>> GetKlinesCandlesticks(GetKlinesCandlesticksRequest request);

        /// <summary>
        /// Gets the Daily price ticker for the provided symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<SymbolPriceChangeTickerResponse> GetDailyTicker(string symbol);

        /// <summary>
        /// Gets all prices for all symbols
        /// </summary>
        /// <returns></returns>
        Task<List<SymbolPriceResponse>> GetSymbolsPriceTicker();

        /// <summary>
        /// Gets the best and quantity on the order book for all symbols
        /// </summary>
        /// <returns></returns>
        Task<List<SymbolOrderBookResponse>> GetSymbolOrderBookTicker();

        /// <summary>
        /// Creates an order based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="CreateOrderRequest"/> that is used to define the order</param>
        /// <returns></returns>
        Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request);

        /// <summary>
        /// Queries an order based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="QueryOrderRequest"/> that is used to define the order</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<OrderResponse> QueryOrder(QueryOrderRequest request, int receiveWindow = 5000);

        /// <summary>
        /// Cancels an order based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="CancelOrderRequest"/> that is used to define the order</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request, int receiveWindow = 5000);

        /// <summary>
        /// Queries all orders based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="CurrentOpenOrdersRequest"/> that is used to define the orders</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<List<OrderResponse>> GetCurrentOpenOrders(CurrentOpenOrdersRequest request, int receiveWindow = 5000);

        /// <summary>
        /// Queries all orders based on the provided request
        /// </summary>
        /// <param name="request">The <see cref="AllOrdersRequest"/> that is used to define the orders</param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<List<OrderResponse>> GetAllOrders(AllOrdersRequest request, int receiveWindow = 5000);

        /// <summary>
        /// Queries the current account information
        /// </summary>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<AccountInformationResponse> GetAccountInformation(int receiveWindow = 5000);

        /// <summary>
        /// Queries the all trades against this account
        /// </summary>
        /// <param name="request"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<List<AccountTradeReponse>> GetAccountTrades(AllTradesRequest request, int receiveWindow = 5000);

        /// <summary>
        /// Sends a request to withdraw to an address
        /// </summary>
        /// <param name="request"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<WithdrawResponse> CreateWithdrawRequest(WithdrawRequest request, int receiveWindow = 5000);

        /// <summary>
        /// Sends a request to withdraw to an address
        /// </summary>
        /// <param name="request"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<DepositListResponse> GetDepositHistory(FundHistoryRequest request, int receiveWindow = 5000);
    }
}