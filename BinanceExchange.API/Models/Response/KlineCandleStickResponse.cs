using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
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
        public decimal Open { get; set; }

        [DataMember(Order = 3)]
        public decimal High { get; set; }

        [DataMember(Order = 4)]
        public decimal Low { get; set; }

        [DataMember(Order = 5)]
        public decimal Close { get; set; }

        [DataMember(Order = 6)]
        public decimal Volume { get; set; }

        [DataMember(Order = 7)]
        public DateTime CloseTime { get; set; }

        [DataMember(Order = 7)]
        public decimal QuoteAssetVolume { get; set; }

        [DataMember(Order = 7)]
        public int NumberOfTrades { get; set; }

        [DataMember(Order = 8)]
        public decimal TakerBuyBaseAssetVolume { get; set; }

        [DataMember(Order = 9)]
        public decimal TakerBuyQuoteAssetVolume { get; set; }
    }
}