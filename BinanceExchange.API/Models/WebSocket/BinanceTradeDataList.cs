using BinanceExchange.API.Converter;
using BinanceExchange.API.Models.WebSocket.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BinanceExchange.API.Models.WebSocket
{
    [DataContract]
    public class BinanceTradeDataList : List<BinanceTradeData>, ISocketResponse
    {
    }
}
