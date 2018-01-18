using System;

namespace BinanceExchange.Abstractions.Models.WebSocket.Interfaces
{
    public interface IWebSocketResponse
    {
        string EventType { get; set; }

        DateTime EventTime { get; set; }
    }
}