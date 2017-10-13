using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Symbol price information
    /// </summary>
    [DataContract]
    public class SymbolPriceResponse
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public decimal Price { get; set; }
    }
}