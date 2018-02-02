using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.Request.Interfaces;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to retrieve exchange information
    /// </summary>
    [DataContract]
    public class ExchangeInfo : IRequest
    {
    }
}
