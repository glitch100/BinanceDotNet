using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Response.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response.Abstract
{

    public abstract class BaseCreateOrderResponse : IResponse
    {
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "orderId")]
        public long OrderId { get; set; }

        [JsonProperty(PropertyName = "clientOrderId")]
        public string ClientOrderId { get; set; }
         
        [JsonProperty(PropertyName = "transactTime")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime TransactionTime { get; set; }
    }
}
