using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using BinanceExchange.API.Client;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Extensions;
using BinanceExchange.API.Models.Websocket;
using BinanceExchange.API.Utility;
using Newtonsoft.Json;
using NLog;
using WebSocketSharp;

namespace BinanceExchange.API.Websockets
{
    /// <summary>
    /// Abstract class for creating WebSocketClients 
    /// </summary>
    public class AbstractBinanceWebSocketClient
    {
        protected SslProtocols SupportedProtocols { get; } = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;

        /// <summary>
        /// Base WebSocket URI for Binance API
        /// </summary>
        protected string BaseWebsocketUri = "wss://stream.binance.com:9443/ws";

        /// <summary>
        /// Used for deletion on the fly
        /// </summary>
        protected Dictionary<Guid, BinanceWebSocket> ActiveWebSockets;
        protected List<BinanceWebSocket> AllSockets;
        protected readonly IBinanceClient BinanceClient;
        protected ILogger Logger;

        protected const string AccountEventType = "outboundAccountInfo";
        protected const string OrderTradeEventType = "executionReport";

        public AbstractBinanceWebSocketClient(IBinanceClient binanceClient, ILogger logger = null)
        {
            BinanceClient = binanceClient;
            ActiveWebSockets = new Dictionary<Guid, BinanceWebSocket>();
            AllSockets = new List<BinanceWebSocket>();
            Logger = logger ?? LogManager.GetCurrentClassLogger();
        }


        /// <summary>
        /// Connect to the Kline WebSocket
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="messageEventHandler"></param>
        /// <returns></returns>
        public Guid ConnectToKlineWebSocket(string symbol, KlineInterval interval, BinanceWebSocketMessageHandler<BinanceKlineData> messageEventHandler)
        {
            Guard.AgainstNullOrEmpty(symbol, nameof(symbol));
            Logger.Debug("Connecting to Kline Web Socket");
            var endpoint = new Uri($"{BaseWebsocketUri}/{symbol.ToLower()}@kline_{EnumExtensions.GetEnumMemberValue(interval)}");
            return CreateBinanceWebSocket(endpoint, messageEventHandler);
        }

        /// <summary>
        /// Connect to the Depth WebSocket
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="messageEventHandler"></param>
        /// <returns></returns>
        public Guid ConnectToDepthWebSocket(string symbol, BinanceWebSocketMessageHandler<BinanceDepthData> messageEventHandler)
        {
            Guard.AgainstNullOrEmpty(symbol, nameof(symbol));
            Logger.Debug("Connecting to Depth Web Socket");
            var endpoint = new Uri($"{BaseWebsocketUri}/{symbol.ToLower()}@depth");
            return CreateBinanceWebSocket(endpoint, messageEventHandler);
        }

        /// <summary>
        /// Connect to the Trades WebSocket
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="messageEventHandler"></param>
        /// <returns></returns>
        public Guid ConnectToTradesWebSocket(string symbol, BinanceWebSocketMessageHandler<BinanceAggregateTradeData> messageEventHandler)
        {
            Guard.AgainstNullOrEmpty(symbol, nameof(symbol));
            Logger.Debug("Connecting to Trades Web Socket");
            var endpoint = new Uri($"{BaseWebsocketUri}/{symbol.ToLower()}@aggTrades");
            return CreateBinanceWebSocket(endpoint, messageEventHandler);
        }

        /// <summary>
        /// Connect to the UserData WebSocket
        /// </summary>
        /// <param name="userDataMessageHandlers"></param>
        /// <returns></returns>
        public async Task<Guid> ConnectToUserDataWebSocket(UserDataWebSocketMessages userDataMessageHandlers)
        {
            Guard.AgainstNull(BinanceClient, nameof(BinanceClient));
            Logger.Debug("Connecting to User Data Web Socket");
            var listenKey = await BinanceClient.StartUserDataStream();

            var endpoint = new Uri($"{BaseWebsocketUri}/{listenKey}");
            return CreateUserDataBinanceWebSocket(endpoint, userDataMessageHandlers);
        }

        private Guid CreateUserDataBinanceWebSocket(Uri endpoint, UserDataWebSocketMessages userDataWebSocketMessages)
        {
            var websocket = new BinanceWebSocket(endpoint.AbsoluteUri);
            websocket.OnOpen += (sender, e) =>
            {
                Logger.Debug($"WebSocket Opened:{endpoint.AbsoluteUri}");
            };
            websocket.OnMessage += (sender, e) =>
            {
                Logger.Debug($"WebSocket Message Received on Endpoint: {endpoint.AbsoluteUri}");
                var primitive = JsonConvert.DeserializeObject<IWebSocketResponse>(e.Data);
                switch (primitive.EventType)
                {
                    case AccountEventType:
                        var userData = JsonConvert.DeserializeObject<BinanceAccountUpdateData>(e.Data);
                        userDataWebSocketMessages.AccountUpdateMessageHandler(userData);
                        break;
                    case OrderTradeEventType:
                        var orderTradeData = JsonConvert.DeserializeObject<BinanceTradeOrderData>(e.Data);
                        if (orderTradeData.ExecutionType == ExecutionType.Trade)
                        {
                            userDataWebSocketMessages.TradeUpdateMessageHandler(orderTradeData);
                        }
                        else
                        {
                            userDataWebSocketMessages.OrderUpdateMessageHandler(orderTradeData);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
            websocket.OnError += (sender, e) =>
            {
                Logger.Error($"WebSocket Error on {endpoint.AbsoluteUri}: ", e);
                CloseWebSocketInstance(websocket.Id, true);
                throw new Exception("Binance UserData WebSocket failed")
                {
                    Data =
                    {
                        {"ErrorEventArgs", e}
                    }
                };
            };
            ActiveWebSockets.TryAdd(websocket.Id, websocket);
            AllSockets.Add(websocket);
            websocket.SslConfiguration.EnabledSslProtocols = SupportedProtocols;
            websocket.Connect();

            return websocket.Id;
        }

        private Guid CreateBinanceWebSocket<T>(Uri endpoint, BinanceWebSocketMessageHandler<T> messageEventHandler) where T : IWebSocketResponse
        {
            var websocket = new BinanceWebSocket(endpoint.AbsoluteUri);
            websocket.OnOpen += (sender, e) =>
            {
                Logger.Debug($"WebSocket Opened:{endpoint.AbsoluteUri}");
            };
            websocket.OnMessage += (sender, e) =>
            {
                Logger.Debug($"WebSocket Messge Received on: {endpoint.AbsoluteUri}");
                //TODO: Log message received
                var data = JsonConvert.DeserializeObject<T>(e.Data);
                messageEventHandler(data);
            };
            websocket.OnError += (sender, e) =>
            {
                Logger.Debug($"WebSocket Error on {endpoint.AbsoluteUri}:", e);
                CloseWebSocketInstance(websocket.Id, true);
                throw new Exception("Binance WebSocket failed")
                {
                    Data =
                    {
                        {"ErrorEventArgs", e}
                    }
                };
            };
            ActiveWebSockets.TryAdd(websocket.Id, websocket);
            AllSockets.Add(websocket);
            websocket.SslConfiguration.EnabledSslProtocols = SupportedProtocols;
            websocket.Connect();

            return websocket.Id;
        }

        /// <summary>
        /// Close a specific WebSocket instance using the Guid provided on creation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromError"></param>
        public void CloseWebSocketInstance(Guid id, bool fromError = false)
        {
            if (ActiveWebSockets.ContainsKey(id))
            {
                var ws = ActiveWebSockets[id];
                ActiveWebSockets.Remove(id);
                if (!fromError)
                {
                    ws.CloseAsync(CloseStatusCode.PolicyViolation);
                }
            }
            else
            {
                throw new Exception($"No Websocket exists with the Id {id.ToString()}");
            }
        }
    }
}
