using System.Collections.Generic;

namespace BinanceExchange.API.Models.Websocket
{
    public class SymbolCacheItem
    {
        public string Symbol { get; }
        public List<Trade> Asks;
        public List<Trade> Bids;
        public long CurrentUpdateId;
        public SymbolCacheItem(string symbol, long currentUpdateId)
        {
            Symbol = symbol;
            CurrentUpdateId = currentUpdateId;
        }
    }
    public class Trade
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }
}