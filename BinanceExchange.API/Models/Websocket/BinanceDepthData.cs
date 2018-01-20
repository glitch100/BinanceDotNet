using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Response;
using BinanceExchange.API.Models.WebSocket.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.WebSocket
{
    /// <summary>
    /// Binance Depth data returned from the Depth WebSocket endpoint
    /// </summary>
    [DataContract]
    public class BinanceDepthData : IWebSocketResponse
    {
        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "E")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime EventTime { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "u")]
        public long UpdateId { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "b")]
        [JsonConverter(typeof(TraderPriceConverter))]
        public List<TradeResponse> BidDepthDeltas { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty(PropertyName = "a")]
        [JsonConverter(typeof(TraderPriceConverter))]
        public List<TradeResponse> AskDepthDeltas { get; set; }
    }
}