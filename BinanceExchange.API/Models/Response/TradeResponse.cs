using System.Runtime.Serialization;
using BinanceExchange.Abstractions.Models.Response.Interfaces;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Trade response, providing price and quantity information
    /// </summary>
    [DataContract]
    public class TradeResponse: IResponse
    {
        [DataMember(Order = 1)]
        public decimal Price { get; set; }

        [DataMember(Order = 2)]
        public decimal Quantity { get; set; }
    }
}