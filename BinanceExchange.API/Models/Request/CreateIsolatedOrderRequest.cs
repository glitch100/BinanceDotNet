using BinanceExchange.API.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using BinanceExchange.API.Models.Request.Interfaces;
using BinanceExchange.API.Converter;

namespace BinanceExchange.API.Models.Request
{
    /// <summary>
    /// Request object used to create a new Binance Isolated account order
    /// </summary>
    [DataContract]
    public class CreateIsolatedOrderRequest : IRequest
    {
        [DataMember(Order = 1)]
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        [JsonProperty("isIsolated")]
        public string IsIsolated { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty("side")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty("quantity")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal Quantity { get; set; }

        [DataMember(Order = 6)]
        [JsonProperty("timeInForce")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce? TimeInForce { get; set; }

        [DataMember(Order = 7)]
        [JsonProperty("price")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal? Price { get; set; }

        [DataMember(Order = 8)]
        [JsonProperty("newClientOrderId")]
        public string NewClientOrderId { get; set; }

        [DataMember(Order = 9)]
        [JsonProperty("stopPrice")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal? StopPrice { get; set; }

        [DataMember(Order = 10)]
        [JsonProperty("icebergQty")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal? IcebergQuantity { get; set; }

        [DataMember(Order = 11)]
        [JsonProperty("newOrderRespType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NewOrderResponseType? NewOrderResponseType { get; set; }

        [DataMember(Order = 12)]
        [JsonProperty("sideEffectType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SideEffectType? SideEffectType { get; set; }

        [DataMember(Order = 13)]
        [JsonProperty("recvWindow")]
        public long? RecvWindow { get; set; }

        [DataMember(Order = 14)]
        [JsonProperty("timeStamp")]
        public long TimeStamp { get; set; }
    }
}
