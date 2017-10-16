using System;
using WebSocketSharp;

namespace BinanceExchange.API.Websockets
{
    /// <summary>
    /// Built around WebSocket for future improvements. For all intents and purposes, this is the same as the WebSocket
    /// </summary>
    public class BinanceWebSocket : WebSocket
    {
        public Guid Id;
        public BinanceWebSocket(string url, params string[] protocols) : base(url, protocols)
        {
            Id = Guid.NewGuid();
        }
    }
}