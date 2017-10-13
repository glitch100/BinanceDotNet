using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Conveter;
using BinanceExchange.API.Enums;
using Newtonsoft.Json;

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
        public string Price { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "origQty")]
        public string OriginalQuantity { get; set; }

        [DataMember(Order = 5)]
        [JsonProperty(PropertyName = "executedQty")]
        public string ExecutedQuantity { get; set; }

        [DataMember(Order = 6)]
        public OrderStatus Status { get; set; }

        [DataMember(Order = 7)]
        public TimeInForce TimeInForce { get; set; }

        [DataMember(Order = 8)]
        public OrderType Type { get; set; }

        [DataMember(Order = 9)]
        public OrderSide Side { get; set; }

        [DataMember(Order = 10)]
        public string StopPrice { get; set; }

        [DataMember(Order = 11)]
        [JsonProperty(PropertyName = "icebergQty")]
        public string IcebergQuantity { get; set; }

        [DataMember(Order = 12)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime Time { get; set; }
    }
}