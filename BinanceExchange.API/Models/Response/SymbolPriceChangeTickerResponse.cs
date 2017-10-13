using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Conveter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class SymbolPriceChangeTickerResponse
    {
        [DataMember(Order = 1)]
        public string PriceChange { get; set; }

        [DataMember(Order = 2)]
        public string PriceChangePercent { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "weightedAvgPrice")]
        public string WeightedAveragePercent { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "prevClosePrice")]
        public string PreviousClosePrice { get; set; }

        [DataMember(Order = 5)]
        public string LastPrice { get; set; }

        [DataMember(Order = 6)]
        public string BidPrice { get; set; }

        [DataMember(Order = 7)]
        public string AskPrice { get; set; }

        [DataMember(Order = 8)]
        public string OpenPrice { get; set; }

        [DataMember(Order = 8)]
        public string HighPrice { get; set; }

        [DataMember(Order = 9)]
        public string Volume { get; set; }

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