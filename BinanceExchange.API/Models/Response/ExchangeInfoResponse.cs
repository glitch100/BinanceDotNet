using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Response.Interfaces;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoResponse : IResponse
    {
        [DataMember(Order = 1)]
        public string Timezone { get; set; }

        [DataMember(Order = 2)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime ServerTime { get; set; }

        [DataMember(Order = 3)]
        public List<ExchangeInfoRateLimit> RateLimits { get; set; }

        // ExchangeFilters, array of unknown type

        [DataMember(Order = 5)]
        public List<ExchangeInfoSymbol> Symbols { get; set; }
    }
}
