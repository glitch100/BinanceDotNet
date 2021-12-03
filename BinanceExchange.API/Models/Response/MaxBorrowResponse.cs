using System.Runtime.Serialization;
using BinanceExchange.API.Models.Response.Interfaces;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Balance respomse providing information on assets
    /// </summary>
    [DataContract]
    public class MaxBorrowResponse
    {
        [DataMember(Order = 1)]
        public decimal amount { get; set; }

        [DataMember(Order = 2)]
        public decimal borrowLimit { get; set; }
    }
}