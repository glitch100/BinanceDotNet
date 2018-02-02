using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class Fill
    {
        [DataMember(Order = 1)]
        public decimal Price { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "qty")]
        public int Quantity { get; set; }

        [DataMember(Order = 3)]
        public decimal Commission { get; set; }

        [DataMember(Order = 5)]
        public string CommissionAsset { get; set; }
    }
}