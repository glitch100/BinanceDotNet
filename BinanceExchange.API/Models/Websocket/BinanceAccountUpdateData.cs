using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Websocket
{
    [DataContract]
    public class BinanceAccountUpdateData: IWebSocketResponse
    {
        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "E")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime EventTime { get; set; }

        #region Undefined API Result fields
        //TODO: Update when Binance API updated

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "m")]
        public int M { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "t")]
        public int t { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "b")]
        public int B { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty(PropertyName = "s")]
        public int S { get; set; }

        [DataMember(Order = 7)]
        [JsonProperty(PropertyName = "T")]
        public bool T { get; set; }

        [DataMember(Order = 8)]
        [JsonProperty(PropertyName = "W")]
        public bool W { get; set; }

        [DataMember(Order = 9)]
        [JsonProperty(PropertyName = "D")]
        public bool D { get; set; }
        #endregion

        [DataMember(Order = 10)]
        [JsonProperty(PropertyName = "B")]
        public List<BalanceResponseData> Balances { get; set; }
    }
}