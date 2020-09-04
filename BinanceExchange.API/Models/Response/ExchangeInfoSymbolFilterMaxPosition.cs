using System;
using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterMaxPosition : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MaxPosition { get; set; }
    }
}