using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to retrieve Binance orders
    /// </summary>
    [DataContract]
    public class CurrentOpenOrdersRequest : IRequest
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }
    }
}