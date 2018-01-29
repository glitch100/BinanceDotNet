using System.Runtime.Serialization;
using BinanceExchange.API.Models.Response.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.WebSocket
{
    [DataContract]
    public class BalanceResponseData: IBalanceResponse
    {
        [JsonProperty(PropertyName = "a")]
        public string Asset { get; set; }

        [JsonProperty(PropertyName = "f")]
        public decimal Free { get; set; }

        [JsonProperty(PropertyName = "l")]
        public decimal Locked { get; set; }
    }
}