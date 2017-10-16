namespace BinanceExchange.API.Models.Websocket
{
    public interface ISymbolWebSocketResponse: IWebSocketResponse
    {
        string Symbol { get; set; }
    }
}