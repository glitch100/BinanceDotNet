using System;
using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterPrice : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MinPrice { get; set; }

        [DataMember(Order = 2)]
        public Decimal MaxPrice { get; set; }

        [DataMember(Order = 3)]
        public Decimal TickSize { get; set; }
    }
}
