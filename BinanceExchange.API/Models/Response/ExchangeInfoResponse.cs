using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Response.Interfaces;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoRateLimit
    {
        [DataMember(Order = 1)]
        public string RateLimitType { get; set; }

        [DataMember(Order = 2)]
        public string Interval { get; set; }

        [DataMember(Order = 3)]
        public int Limit { get; set; }
    }

    [DataContract]
    public class ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public string FilterType { get; private set; }
    }

    [DataContract]
    public class ExchangeInfoSymbolFilterPrice : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MinPrice { get; private set; }

        [DataMember(Order = 2)]
        public Decimal MaxPrice { get; private set; }

        [DataMember(Order = 3)]
        public Decimal TickSize { get; private set; }
    }

    [DataContract]
    public class ExchangeInfoSymbolFilterLotSize : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MinQty { get; private set; }

        [DataMember(Order = 2)]
        public Decimal MaxQty { get; private set; }

        [DataMember(Order = 3)]
        public Decimal StepSize { get; private set; }
    }

    [DataContract]
    public class ExchangeInfoSymbolFilterMinNotional : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MinNotional { get; private set; }
    }

    [DataContract]
    public class ExchangeInfoSymbol
    {
        [DataMember(Order = 1)]
        public string Symbol { get; private set; }

        [DataMember(Order = 2)]
        public string Status { get; private set; }

        [DataMember(Order = 3)]
        public string BaseAsset { get; private set; }

        [DataMember(Order = 4)]
        public int BaseAssetPrecision { get; private set; }

        [DataMember(Order = 5)]
        public string QuoteAsset { get; private set; }

        [DataMember(Order = 6)]
        public int QuotePrecision { get; private set; }

        [DataMember(Order = 7)]
        public string[] OrderTypes { get; private set; }

        [DataMember(Order = 8)]
        public bool IcebergAllowed { get; private set; }

        [DataMember(Order = 9)]
        [JsonConverter(typeof(ExchangeInfoSymbolFilterConverter))]
        public List<ExchangeInfoSymbolFilter> Filters { get; private set; }

    }

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
