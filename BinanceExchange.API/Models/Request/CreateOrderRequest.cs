using BinanceExchange.API.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;
using BinanceExchange.API.Converter;

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
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal Quantity { get; set; }

        [DataMember(Order = 6)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal? Price { get; set; }

        [DataMember(Order = 7)]
        public string NewClientOrderId { get; set; }

        [DataMember(Order = 8)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal? StopPrice { get; set; }

        [DataMember(Order = 9)]
        [JsonProperty("icebergQty")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal? IcebergQuantity { get; set; }

        [DataMember(Order = 10)]
        [JsonProperty("newOrderRespType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NewOrderResponseType NewOrderResponseType { get; set; }
    }
}
