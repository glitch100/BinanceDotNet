using BinanceExchange.API.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;
using BinanceExchange.API.Converter;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to query the accoun't max borrow
    /// </summary>
    [DataContract]
    public class MaxBorrowRequest : IRequest
    {
        [DataMember(Order = 1)]
        [JsonProperty("asset")]
        public string Asset { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty("isolatedSymbol")]
        public string IsolatedSymbol { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty("recvWindow")]
        public long? RecvWindow { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty("timeStamp")]
        public long TimeStamp { get; set; }
    }
}
