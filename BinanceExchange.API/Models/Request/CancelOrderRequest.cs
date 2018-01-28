using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to cancel a Binance order
    /// </summary>
    [DataContract]
    public class CancelOrderRequest : IRequest
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long? OrderId { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "origClientOrderId")]
        public string OriginalClientOrderId { get; set; }

        [DataMember(Order = 4)]
        public string NewClientOrderId { get; set; }
    }
}