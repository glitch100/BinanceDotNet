using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Conveter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Response following a call to the Get Klines Candlesticks endpoint
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(KlineCandleSticksConverter))]
    public class KlineCandleStickResponse : IResponse
    {
        [DataMember(Order = 1)]
        public DateTime OpenTime { get; set; }

        [DataMember(Order = 2)]
        public string Open { get; set; }

        [DataMember(Order = 3)]
        public string High { get; set; }

        [DataMember(Order = 4)]
        public string Low { get; set; }

        [DataMember(Order = 5)]
        public string Close { get; set; }

        [DataMember(Order = 6)]
        public string Volume { get; set; }

        [DataMember(Order = 7)]
        public DateTime CloseTime { get; set; }

        [DataMember(Order = 7)]
        public string QuoteAssetVolume { get; set; }

        [DataMember(Order = 7)]
        public int NumberOfTrades { get; set; }

        [DataMember(Order = 8)]
        public string TakerBuyBaseAssetVolume { get; set; }

        [DataMember(Order = 9)]
        public string TakerBuyQuoteAssetVolume { get; set; }
    }
}