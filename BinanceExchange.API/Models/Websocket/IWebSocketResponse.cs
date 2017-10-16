using System;

namespace BinanceExchange.API.Models.Websocket
{
    public interface IWebSocketResponse
    {
        string EventType { get; set; }

        DateTime EventTime { get; set; }
    }
}