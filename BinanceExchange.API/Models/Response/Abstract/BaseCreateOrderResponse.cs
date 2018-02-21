using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Response.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response.Abstract
{
    [DataContract]
    public abstract class BaseCreateOrderResponse : IResponse
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long OrderId { get; set; }

        [DataMember(Order = 3)]
        public string ClientOrderId { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty("transactTime")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }
}
