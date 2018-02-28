﻿using System;
using System.Runtime.Serialization;
using BinanceExchange.API.Converter;
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
        public long OrderId { get; set; }

        [DataMember(Order = 3)]
        public decimal Price { get; set; }

        [DataMember(Order = 4)]
        [JsonProperty(PropertyName = "qty")]
        public decimal Quantity { get; set; }

        [DataMember(Order = 5)]
        public decimal Commission { get; set; }

        [DataMember(Order = 6)]
        public string CommissionAsset { get; set; }

        [DataMember(Order = 7)]
        [JsonConverter(typeof(EpochTimeConverter))]
        public DateTime Time { get; set; }

        [DataMember(Order = 8)]
        public bool IsBuyer { get; set; }

        [DataMember(Order = 9)]
        public bool IsMaker { get; set; }

        [DataMember(Order = 10)]
        public bool IsBestMatch { get; set; }
    }
}
