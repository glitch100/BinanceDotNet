using System.Runtime.Serialization;
using BinanceExchange.API.Models.Response.Interfaces;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Balance respomse providing information on assets
    /// </summary>
    [DataContract]
    public class BalanceResponse : IBalanceResponse
    {
        [DataMember(Order = 1)]
        public string Asset { get; set; }
        
        [DataMember(Order = 2)]
        public decimal Free { get; set; }

        [DataMember(Order = 3)]
        public decimal Locked { get; set; }
    }
}