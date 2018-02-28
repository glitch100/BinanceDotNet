using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.WebSocket.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceExchange.API.Models.WebSocket
{
    /// <summary>
    /// Shared class that represents either a trade or an order, and the data returned from the WebSocket endpoint
    /// </summary> 
    public class BinanceTradeOrderData : ISymbolWebSocketResponse
    {

        [JsonProperty(PropertyName = "e")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "E")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime EventTime { get; set; }

        [JsonProperty(PropertyName = "s")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "c")]
        public string NewClientOrderId { get; set; }

        [JsonProperty(PropertyName = "S")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        [JsonProperty(PropertyName = "o")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }

        [JsonProperty(PropertyName = "f")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce TimeInForce { get; set; }

        [JsonProperty(PropertyName = "q")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "p")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "P")]
        public double StopPrice { get; set; }

        [JsonProperty(PropertyName = "F")]
        public double IcebergQuantity { get; set; }

        #region Undefined API Result fields
        //TODO: Update when Binance API updated 


        [JsonProperty(PropertyName = "g")]
        public string g { get; set; }

        [JsonProperty(PropertyName = "C")]
        public string OriginalClientOrderId { get; set; }
        #endregion

        [JsonProperty(PropertyName = "x")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ExecutionType ExecutionType { get; set; }

        [JsonProperty(PropertyName = "X")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }

        [JsonProperty(PropertyName = "r")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderRejectReason OrderRejectReason { get; set; }

        [JsonProperty(PropertyName = "i")]
        public long OrderId { get; set; }

        [JsonProperty(PropertyName = "l")]
        public decimal QuantityOfLastFilledTrade { get; set; }

        [JsonProperty(PropertyName = "z")]
        public decimal AccumulatedQuantityOfFilledTradesThisOrder { get; set; }

        [JsonProperty(PropertyName = "L")]
        public decimal PriceOfLastFilledTrade { get; set; }

        [JsonProperty(PropertyName = "n")]
        public decimal Commission { get; set; }

        /// <summary>
        /// Asset on which commission taken
        /// </summary>
        [JsonProperty(PropertyName = "N")]
        public string AssetCommissionTakenFrom { get; set; }

        [JsonProperty(PropertyName = "T")]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime TimeStamp { get; set; }


        [JsonProperty(PropertyName = "t")]
        public long TradeId { get; set; }

        /// <summary>
        /// Represents Order or Trade time
        /// </summary> 
        #region Undefined API Result fields
        [JsonProperty(PropertyName = "I")]
        public long I { get; set; }

        [JsonProperty(PropertyName = "w")]
        public bool w { get; set; }
        #endregion


        [JsonProperty(PropertyName = "m")]
        public bool IsBuyerMaker { get; set; }

        #region Undefined API Result fields
        //TODO: Update when Binance API updated
        [JsonProperty(PropertyName = "M")]
        public bool M { get; set; }

        [JsonProperty(PropertyName = "O")]
        public int O { get; set; }

        [JsonProperty(PropertyName = "Z")]
        public decimal Z { get; set; }
        #endregion
    }
}