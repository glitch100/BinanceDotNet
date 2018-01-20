namespace BinanceExchange.API.Models.WebSocket.Interfaces
{
    public interface ISymbolWebSocketResponse: IWebSocketResponse
    {
        string Symbol { get; set; }
    }
}