using System.Collections.Generic;
using BinanceExchange.API.Enums;

namespace BinanceExchange.API.Models.WebSocket
{
    public class KlineCacheObject
    {
        public Dictionary<KlineInterval, KlineIntervalCacheObject> KlineInterDictionary { get; set; }   
    }
}