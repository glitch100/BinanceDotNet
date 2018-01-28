using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to query a Binance order
    /// </summary>
    [DataContract]
    public class QueryOrderRequest : IRequest
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long? OrderId { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "origClientOrderId")]
        public string OriginalClientOrderId { get; set; }
    }
}