﻿using System;
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
using Logger = NLog.Logger;

namespace BinanceExchange.API.Websockets
{
    /// <summary>
    /// The BinanceWebSocketClient is used when wanting to open a connection to retrieve data through the WebSocket protocol. Implements IDisposable
    /// </summary>
    public class BinanceWebSocketClient : IDisposable, IBinanceWebSocketClient
    {
        /// <summary>
        /// These are provided to bypass an exception which occurs (down to the WebsocketSharp library)
        /// </summary>
        public SslProtocols SupportedProtocols { get; } = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;

        /// <summary>
        /// Base WebSocket URI for Binance API
        /// </summary>
        private string _baseWebsocketUri = "wss://stream.binance.com:9443/ws";

        /// <summary>
        /// Used for deletion on the fly
        /// </summary>
        private Dictionary<Guid, BinanceWebSocket> _activeWebSockets;
        private List<BinanceWebSocket> _allSockets;
        private readonly IBinanceClient _binanceClient;
        private ILogger _logger;

        private const string AccountEventType = "outboundAccountInfo";
        private const string OrderTradeEventType = "executionReport";

        public BinanceWebSocketClient(IBinanceClient binanceClient, ILogger logger = null)
        {
            _binanceClient = binanceClient;
            _activeWebSockets = new Dictionary<Guid, BinanceWebSocket>();
            _allSockets = new List<BinanceWebSocket>();
            _logger = logger ?? LogManager.GetCurrentClassLogger();
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
            _logger.Debug("Connecting to Kline Web Socket");
            var endpoint = new Uri($"{_baseWebsocketUri}/{symbol.ToLower()}@kline_{EnumExtensions.GetEnumMemberValue(interval)}");
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
            _logger.Debug("Connecting to Depth Web Socket");
            var endpoint = new Uri($"{_baseWebsocketUri}/{symbol.ToLower()}@depth");
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
            _logger.Debug("Connecting to Trades Web Socket");
            var endpoint = new Uri($"{_baseWebsocketUri}/{symbol.ToLower()}@aggTrades");
            return CreateBinanceWebSocket(endpoint, messageEventHandler);
        }

        /// <summary>
        /// Connect to the UserData WebSocket
        /// </summary>
        /// <param name="userDataMessageHandlers"></param>
        /// <returns></returns>
        public async Task<Guid> ConnectToUserDataWebSocket(UserDataWebSocketMessages userDataMessageHandlers)
        {
            Guard.AgainstNull(_binanceClient, nameof(_binanceClient));
            _logger.Debug("Connecting to User Data Web Socket");
            var listenKey = await _binanceClient.StartUserDataStream();

            var endpoint = new Uri($"{_baseWebsocketUri}/{listenKey}");
            return CreateUserDataBinanceWebSocket(endpoint, userDataMessageHandlers);
        }

        private Guid CreateUserDataBinanceWebSocket(Uri endpoint, UserDataWebSocketMessages userDataWebSocketMessages)
        {
            var websocket = new BinanceWebSocket(endpoint.AbsoluteUri);
            websocket.OnOpen += (sender, e) =>
            {
                _logger.Debug($"WebSocket Opened:{endpoint.AbsoluteUri}");
            };
            websocket.OnMessage += (sender, e) =>
            {
                _logger.Debug($"WebSocket Message Received on Endpoint: {endpoint.AbsoluteUri}");
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
                _logger.Error($"WebSocket Error on {endpoint.AbsoluteUri}: ",e);
                CloseWebSocketInstance(websocket.Id, true);
                throw new Exception("Binance UserData WebSocket failed")
                {
                    Data =
                    {
                        {"ErrorEventArgs", e}
                    }
                };
            };
            _activeWebSockets.TryAdd(websocket.Id, websocket);
            _allSockets.Add(websocket);
            websocket.SslConfiguration.EnabledSslProtocols = SupportedProtocols;
            websocket.Connect();

            return websocket.Id;
        }

        private Guid CreateBinanceWebSocket<T>(Uri endpoint, BinanceWebSocketMessageHandler<T> messageEventHandler) where T : IWebSocketResponse
        {
            var websocket = new BinanceWebSocket(endpoint.AbsoluteUri);
            websocket.OnOpen += (sender, e) =>
            {
                _logger.Debug($"WebSocket Opened:{endpoint.AbsoluteUri}");
            };
            websocket.OnMessage += (sender, e) =>
            {
                _logger.Debug($"WebSocket Messge Received on: {endpoint.AbsoluteUri}");
                //TODO: Log message received
                var data = JsonConvert.DeserializeObject<T>(e.Data);
                messageEventHandler(data);
            };
            websocket.OnError += (sender, e) =>
            {
                _logger.Debug($"WebSocket Error on {endpoint.AbsoluteUri}:", e);
                CloseWebSocketInstance(websocket.Id, true);
                throw new Exception("Binance WebSocket failed")
                {
                    Data =
                    {
                        {"ErrorEventArgs", e}
                    }
                };
            };
            _activeWebSockets.TryAdd(websocket.Id, websocket);
            _allSockets.Add(websocket);
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
            if (_activeWebSockets.ContainsKey(id))
            {
                var ws = _activeWebSockets[id];
                _activeWebSockets.Remove(id);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _allSockets.ForEach(ws =>
            {
                if (ws.IsAlive) ws.Close(CloseStatusCode.Normal);
            });
            _allSockets = new List<BinanceWebSocket>();
            _activeWebSockets = new Dictionary<Guid, BinanceWebSocket>();
        }
    }
}