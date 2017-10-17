using System.Collections.Generic;

namespace BinanceExchange.API.Models.Websocket
{
    public class DepthCacheObject
    {
        public Dictionary<decimal, decimal> Asks { get; set; }
        public Dictionary<decimal, decimal> Bids { get; set; }
    }

}