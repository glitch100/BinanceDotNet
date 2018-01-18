using BinanceExchange.Abstractions.Models.WebSocket.Interfaces;

namespace BinanceExchange.API.Websockets
{
    public delegate void BinanceWebSocketMessageHandler<in T>(T data) where T: IWebSocketResponse;
}