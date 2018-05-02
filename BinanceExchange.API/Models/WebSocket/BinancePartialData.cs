using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Response;
using BinanceExchange.API.Models.WebSocket.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.WebSocket
{
    [DataContract]
    public class BinancePartialData : IWebSocketResponse
    {
        public string EventType { get; set; } = "PartialDepthBook";

        public DateTime EventTime { get; set; } = DateTime.UtcNow;

        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "lastUpdateId")]
        public int LastUpdateId { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "bids")]
        [JsonConverter(typeof(TraderPriceConverter))]
        public List<TradeResponse> Bids { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "asks")]
        [JsonConverter(typeof(TraderPriceConverter))]
        public List<TradeResponse> Asks { get; set; }
 
    }
}
