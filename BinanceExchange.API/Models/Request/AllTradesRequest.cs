using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to retrieve all trades
    /// </summary>
    [DataContract]
    public class AllTradesRequest : IRequest
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long FromId { get; set; }

        [DataMember(Order = 3)]
        public int Limit { get; set; }
    }
}