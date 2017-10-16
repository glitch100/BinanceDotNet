using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to retrieve current account information
    /// </summary>
    [DataContract]
    public class AccountRequest : IRequest
    {
        [DataMember(Order = 1)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime TimeStamp { get; set; }
    }
}