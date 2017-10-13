using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Balance respomse providing information on assets
    /// </summary>
    [DataContract]
    public class BalanceResponse
    {
        [DataMember(Order = 1)]
        public string Asset { get; set; }
        
        [DataMember(Order = 2)]
        public string Free { get; set; }

        [DataMember(Order = 3)]
        public string Locked { get; set; }
    }
}