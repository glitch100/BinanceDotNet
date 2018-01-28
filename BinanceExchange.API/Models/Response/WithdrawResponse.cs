using System.Runtime.Serialization;
using BinanceExchange.API.Models.Response.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    public class WithdrawResponse : IConfirmationResponse
    {
        [DataMember(Order = 1)]
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }

        [DataMember(Order = 3)]
        public string Id { get; set; }
    }
}
