using BinanceExchange.API.Models.WebSocket.Interfaces;
using System.Collections.Generic;

namespace BinanceExchange.API.Websockets
{
    public delegate void BinanceWebSocketMessageHandler<in T>(T data) where T: ISocketResponse;
}