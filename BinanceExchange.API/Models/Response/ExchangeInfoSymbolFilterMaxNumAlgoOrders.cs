using System;
using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterMaxNumAlgoOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public Decimal MaxNumAlgoOrders { get; set; }
    }
}
