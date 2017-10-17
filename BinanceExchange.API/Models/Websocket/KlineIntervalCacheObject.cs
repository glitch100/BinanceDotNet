using System.Collections.Generic;

namespace BinanceExchange.API.Models.Websocket
{
    public class KlineIntervalCacheObject
    {
        public Dictionary<long, KlineCandleStick> TimeKlineDictionary { get; set; }
    }
}