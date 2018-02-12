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
    public class BinanceCombinedDepthData : IWebSocketResponse
    {
        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "data")]
        public BinanceDepthData Data { get; set; }

        public string EventType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime EventTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    [DataContract]
    public class DataCombined : IWebSocketResponse
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
