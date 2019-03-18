using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.WebSocket.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BinanceExchange.API.Models.WebSocket
{
    /// <summary>
    /// trade data response from Trades websocket endpoint (<symbol>@ticker || !ticker@arr)
    /// </summary>
    [DataContract]
    public class BinanceTradeData : ISymbolWebSocketResponse
    {
        [JsonProperty(PropertyName = "e")]
        [DataMember(Order = 1)]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "E")]
        [DataMember(Order = 2)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime EventTime { get; set; }

        [JsonProperty(PropertyName = "s")]
        [DataMember(Order = 3)]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "p")]
        [DataMember(Order = 4)]
        public decimal PriceChange { get; set; }

        [JsonProperty(PropertyName = "P")]
        [DataMember(Order = 5)]
        public decimal PriceChangePercent { get; set; }

        [JsonProperty(PropertyName = "w")]
        [DataMember(Order = 6)]
        public decimal WeightedAveragePrice { get; set; }

        [JsonProperty(PropertyName = "x")]
        [DataMember(Order = 7)]
        public decimal FirstTrade { get; set; }

        [JsonProperty(PropertyName = "c")]
        [DataMember(Order = 8)]
        public decimal LastPrice { get; set; }

        [JsonProperty(PropertyName = "Q")]
        [DataMember(Order = 9)]
        public decimal LastQuantity { get; set; }

        [JsonProperty(PropertyName = "b")]
        [DataMember(Order = 10)]
        public decimal BestBidPrice { get; set; }

        [JsonProperty(PropertyName = "B")]
        [DataMember(Order = 11)]
        public decimal BestBidQuantity { get; set; }

        [JsonProperty(PropertyName = "a")]
        [DataMember(Order = 12)]
        public decimal BestAskPrice { get; set; }

        [JsonProperty(PropertyName = "A")]
        [DataMember(Order = 13)]
        public decimal BestAskQuantity { get; set; }

        [JsonProperty(PropertyName = "o")]
        [DataMember(Order = 14)]
        public decimal OpenPrice { get; set; }

        [JsonProperty(PropertyName = "h")]
        [DataMember(Order = 15)]
        public decimal HighPrice { get; set; }

        [JsonProperty(PropertyName = "l")]
        [DataMember(Order = 16)]
        public decimal LowPrice { get; set; }

        [JsonProperty(PropertyName = "v")]
        [DataMember(Order = 17)]
        public decimal TotalTradedBaseAssetVolume { get; set; }

        [JsonProperty(PropertyName = "q")]
        [DataMember(Order = 18)]
        public decimal TotalTradedQuoteAssetVolume { get; set; }

        [JsonProperty(PropertyName = "O")]
        [DataMember(Order = 19)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime StatisticsOpenTime { get; set; }

        [JsonProperty(PropertyName = "C")]
        [DataMember(Order = 20)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime StatisticsCloseTime { get; set; }

        [JsonProperty(PropertyName = "F")]
        [DataMember(Order = 21)]        
        public long FirstTradeId { get; set; }

        [JsonProperty(PropertyName = "L")]
        [DataMember(Order = 22)]
        public long LastTradeId { get; set; }

        [JsonProperty(PropertyName = "n")]
        [DataMember(Order = 23)]
        public long TotalNumberOfTrades { get; set; }

    }
}
