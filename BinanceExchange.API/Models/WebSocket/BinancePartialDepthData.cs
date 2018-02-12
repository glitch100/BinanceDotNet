using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Response;
using Newtonsoft.Json;
using BinanceExchange.API.Models.WebSocket.Interfaces;

namespace BinanceExchange.API.Models.WebSocket
{
    [DataContract]
    public class BinancePartialDepthData : IWebSocketResponse
    {

        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "stream")]
        public string Stream { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "data")]
        public Data Data { get; set; }


        public string EventType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime EventTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
    public class Data
    {
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
