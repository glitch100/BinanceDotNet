using BinanceExchange.API.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;
using BinanceExchange.API.Converter;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to query Binance Isolated account info
    /// </summary>
    [DataContract]
    public class IsolatedMarginAccountInfoRequest : IRequest
    {
        [DataMember(Order = 1)]
        [JsonProperty("symbols")]
        public string Symbols { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty("recvWindow")]
        public long? RecvWindow { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty("timeStamp")]
        public long TimeStamp { get; set; }
    }
}
