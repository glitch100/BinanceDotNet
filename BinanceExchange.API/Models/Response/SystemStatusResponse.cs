using System.Runtime.Serialization;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Response.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class SystemStatusResponse : IResponse
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public SystemStatus Status { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set; }
    }
}