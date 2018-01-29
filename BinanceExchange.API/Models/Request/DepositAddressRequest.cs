using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;

namespace BinanceExchange.API.Models.Request
{
    [DataContract]
    public class DepositAddressRequest: IRequest
    {
        [DataMember(Order = 1)]
        public string Asset { get; set; }
    }
}