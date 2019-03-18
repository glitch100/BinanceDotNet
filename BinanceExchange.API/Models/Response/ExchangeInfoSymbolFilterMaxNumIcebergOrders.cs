using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    [DataContract]
    public class ExchangeInfoSymbolFilterMaxNumIcebergOrders : ExchangeInfoSymbolFilter
    {
        [DataMember(Order = 1)]
        public int MaxNumIcebergOrders { get; set; }
    }
}