using System;

namespace BinanceExchange.API.Models.WebSocket.Interfaces
{
    public interface IWebSocketResponse
    {
        string EventType { get; set; }

        DateTime EventTime { get; set; }
    }
}