namespace BinanceExchange.Abstractions.Models.WebSocket.Interfaces
{
    public interface ISymbolWebSocketResponse: IWebSocketResponse
    {
        string Symbol { get; set; }
    }
}