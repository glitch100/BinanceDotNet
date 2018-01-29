using BinanceExchange.API.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to create a new Binance order
    /// </summary>
    [DataContract]
    public class CreateOrderRequest: IRequest
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        [DataMember(Order = 3)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }

        [DataMember(Order = 4)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce? TimeInForce { get; set; }

        [DataMember(Order = 5)]
        public decimal Quantity { get; set; }

        [DataMember(Order = 6)]
        public double Price { get; set; }

        [DataMember(Order = 7)]
        public string NewClientOrderId { get; set; }

        [DataMember(Order = 8)]
        public decimal? StopPrice { get; set; }

        [DataMember(Order = 9)]
        [JsonProperty("icebergQty")]
        public decimal? IcebergQuantity { get; set; }
    }
}
