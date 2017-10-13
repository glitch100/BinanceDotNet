using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Response following a call to the Cancel Order endpoint
    /// </summary>
    [DataContract]
    public class CancelOrderResponse : IResponse
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long OrderId { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "origClientOrderId")]
        public long OriginalClientOrderId { get; set; }

        [DataMember(Order = 4)]
        public long ClientOrderId { get; set; }

    }
}