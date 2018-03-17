using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Full Response following a call to the Create Order endpoint
    /// </summary>
    [DataContract]
    public class FullCreateOrderResponse : ResultCreateOrderResponse
    {
        [DataMember(Name = "fills")]
        public List<Fill> Fills { get;set; }
    }
}

    