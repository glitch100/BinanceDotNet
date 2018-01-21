using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Response.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Response object received when querying a Binance order
    /// </summary>
    [DataContract]
    public class OrderResponse: IResponse
    {
        [DataMember(Order = 1)]
        public string Symbol { get; set; }

        [DataMember(Order = 2)]
        public long OrderId { get; set; }

        [DataMember(Order = 3)]
        public decimal Price { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "origQty")]
        public decimal OriginalQuantity { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "executedQty")]
        public decimal ExecutedQuantity { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [DataMember(Order = 6)]
        public OrderStatus Status { get; set; }

        [DataMember(Order = 7)]
        public TimeInForce TimeInForce { get; set; }

        [DataMember(Order = 8)]
        public OrderType Type { get; set; }

        [DataMember(Order = 9)]
        public OrderSide Side { get; set; }

        [DataMember(Order = 10)]
        public decimal StopPrice { get; set; }

        [DataMember(Order = 11)]
        [JsonProperty(PropertyName = "icebergQty")]
        public decimal IcebergQuantity { get; set; }

        [DataMember(Order = 12)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime Time { get; set; }
    }
}