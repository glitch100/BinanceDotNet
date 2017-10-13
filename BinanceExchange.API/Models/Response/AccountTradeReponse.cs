using System.Runtime.Serialization;
using BinanceExchange.API.Conveter;
using Newtonsoft.Json;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Response providing Account Trade information
    /// </summary>
    [DataContract]
    public class AccountTradeReponse
    {
        [DataMember(Order = 1)]
        public long Id { get; set; }

        [DataMember(Order = 2)]
        public string Price { get; set; }

        [DataMember(Order = 3)]
        [JsonProperty(PropertyName = "qty")]
        public string Quantity { get; set; }

        [DataMember(Order = 4)]
        public string Commission { get; set; }

        [DataMember(Order = 5)]
        public string CommissionAsset { get; set; }

        [DataMember(Order = 5)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public string Time { get; set; }

        [DataMember(Order = 6)]
        public bool IsBuyer { get; set; }

        [DataMember(Order = 7)]
        public bool IsMaker { get; set; }

        [DataMember(Order = 8)]
        public bool IsBestMatch { get; set; }
    }
}