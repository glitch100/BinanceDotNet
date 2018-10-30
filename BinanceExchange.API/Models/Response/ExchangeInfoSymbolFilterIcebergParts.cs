using System;
using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterIcebergParts : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1 )]
        public Decimal Limit { get; set; }
    }
}