using System.Runtime.Serialization;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Response.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Result Response following a call to the Create Order endpoint
    /// </summary> 
    public partial class ResultCreateOrderResponse : BaseCreateOrderResponse
    {
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
         
        [JsonProperty(PropertyName = "origQty")]
        public decimal OriginalQuantity { get; set; }


        [JsonProperty(PropertyName = "executedQty")]
        public decimal ExecutedQuantity { get; set; }

        [JsonProperty(PropertyName = "status")]
        [JsonConverter(typeof(StringEnumConverter))] 
        public OrderStatus Status { get; set; }

        [JsonProperty(PropertyName = "timeInForce")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TimeInForce? TimeInForce { get; set; }

        [JsonProperty(PropertyName = "side")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderSide Side { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType Type { get; set; }



    }
}