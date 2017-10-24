using System;
using System.Collections.Generic;
using BinanceExchange.API.Client;
using NLog;
using WebSocketSharp;

namespace BinanceExchange.API.Websockets
{
    /// <summary>
    /// A Disposable instance of the BinanceWebSocketClient is used when wanting to open a connection to retrieve data through the WebSocket protocol. 
    /// Implements IDisposable so that cleanup is managed
    /// </summary>
    public class DisposableBinanceWebSocketClient : AbstractBinanceWebSocketClient, IDisposable, IBinanceWebSocketClient
    {
        public DisposableBinanceWebSocketClient(IBinanceClient binanceClient, ILogger logger = null) : base(binanceClient, logger)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            AllSockets.ForEach(ws =>
            {
                if (ws.IsAlive) ws.Close(CloseStatusCode.Normal);
            });
            AllSockets = new List<BinanceWebSocket>();
            ActiveWebSockets = new Dictionary<Guid, BinanceWebSocket>();
        }
    }
}