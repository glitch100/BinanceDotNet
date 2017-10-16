using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Websocket
{
    /// <summary>
    /// Aggregate trade data response from Trades websocket endpoint
    /// </summary>
    [DataContract]
    public class BinanceAggregateTradeData : ISymbolWebSocketResponse
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

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "a")]
        public long AggregateTradeId { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "p")]
        public decimal Price { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "q")]
        public decimal Quantity { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "f")]
        public long FirstTradeId { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "l")]
        public long LastTradeId { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty("T")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime TradeTime { get; set; }

        [DataMember(Order = 7)]
        [JsonProperty("m")]
        public bool WasBuyerMaker { get; set; }
    }
}