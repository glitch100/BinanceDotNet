using System.Collections.Generic;

namespace BinanceExchange.API.Models.WebSocket
{
    public class KlineIntervalCacheObject
    {
        public Dictionary<long, KlineCandleStick> TimeKlineDictionary { get; set; }
    }
}