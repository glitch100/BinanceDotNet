using BinanceExchange.API.Models.Websocket;

namespace BinanceExchange.API.Websockets
{
    public class UserDataWebSocketMessages
    {
        public BinanceWebSocketMessageHandler<BinanceAccountUpdateData> AccountUpdateMessageHandler { get; set; }
        public BinanceWebSocketMessageHandler<BinanceTradeOrderData> OrderUpdateMessageHandler { get; set; }
        public BinanceWebSocketMessageHandler<BinanceTradeOrderData> TradeUpdateMessageHandler { get; set; }
    }
}