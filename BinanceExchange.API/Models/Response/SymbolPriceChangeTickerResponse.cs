using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class SymbolPriceChangeTickerResponse
    {
        [DataMember(Order = 1)]
        public decimal PriceChange { get; set; }

        [DataMember(Order = 2)]
        public decimal PriceChangePercent { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "weightedAvgPrice")]
        public decimal WeightedAveragePercent { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "prevClosePrice")]
        public decimal PreviousClosePrice { get; set; }

        [DataMember(Order = 5)]
        public decimal LastPrice { get; set; }

        [DataMember(Order = 6)]
        public decimal BidPrice { get; set; }

        [DataMember(Order = 7)]
        public decimal AskPrice { get; set; }

        [DataMember(Order = 8)]
        public decimal OpenPrice { get; set; }

        [DataMember(Order = 8)]
        public decimal HighPrice { get; set; }

        [DataMember(Order = 9)]
        public decimal Volume { get; set; }

        [DataMember(Order = 10)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime OpenTime { get; set; }

        [DataMember(Order = 11)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime CloseTime { get; set; }

        [DataMember(Order = 12)]
        [JsonProperty(PropertyName = "firstId")]
        public long FirstTradeId { get; set; }

        [DataMember(Order = 13)]
        [JsonProperty(PropertyName = "lastId")]
        public long LastId { get; set; }

        [DataMember(Order = 14)]
        [JsonProperty(PropertyName = "count")]
        public int TradeCount { get; set; }
    }
}