using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Websocket
{
    /// <summary>
    /// Data returned from the Binance WebSocket Kline endpoint
    /// </summary>
    [DataContract]
    public class BinanceKlineData: ISymbolWebSocketResponse
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

        [JsonProperty(PropertyName = "K")]
        [DataMember(Order = 4)]
        public BinanceKline Kline { get; set; }
    }
}
