using System.Runtime.Serialization;
using Newtonsoft.Json;
using BinanceExchange.API.Enums;
using Newtonsoft.Json.Converters;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeInfoSymbolFilterType FilterType { get; set; }
    }
}
