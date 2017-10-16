using BinanceExchange.API.Models.Websocket;

namespace BinanceExchange.API.Websockets
{
    public delegate void BinanceWebSocketMessageHandler<in T>(T data) where T: IWebSocketResponse;
}