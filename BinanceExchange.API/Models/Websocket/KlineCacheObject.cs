using System.Collections.Generic;
using BinanceExchange.API.Enums;

namespace BinanceExchange.API.Models.Websocket
{
    public class KlineCacheObject
    {
        public Dictionary<KlineInterval, KlineIntervalCacheObject> KlineInterDictionary { get; set; }   
    }
}