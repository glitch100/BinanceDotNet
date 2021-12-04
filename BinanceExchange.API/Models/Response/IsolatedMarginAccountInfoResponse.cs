using System.Collections.Generic;
using System.Runtime.Serialization;
using BinanceExchange.API.Models.Response.Interfaces;

namespace BinanceExchange.API.Models.Response
{
    /// <summary>
    /// Balance respomse providing information on assets
    /// </summary>
    [DataContract]
    public class AssetType
    {
        [DataMember]
        public string asset { get; set; }
        [DataMember]
        public bool borrowEnabled { get; set; }
        [DataMember]
        public long borrowed { get; set; }
        [DataMember]
        public decimal free { get; set; }
        [DataMember]
        public long interest { get; set; }
        [DataMember]
        public long locked { get; set; }
        [DataMember]
        public decimal netAsset { get; set; }
        [DataMember]
        public decimal netAssetOfBtc { get; set; }
        [DataMember]
        public bool repayEnabled { get; set; }
        [DataMember]
        public decimal totalAsset { get; set; }
    }

    [DataContract]
    public class AssetResponse
    {
        [DataMember]
        public AssetType baseAsset { get; set; }
        [DataMember]
        public AssetType quoteAsset { get; set; }
        [DataMember]
        public string symbol { get; set; }
        [DataMember]
        public bool isolatedCreated { get; set; }
        [DataMember]
        public long marginLevel { get; set; }
        [DataMember]
        public string marginLevelStatus { get; set; }
        [DataMember]
        public long marginRatio { get; set; }
        [DataMember]
        public decimal indexPrice { get; set; }
        [DataMember]
        public decimal liquidatePrice { get; set; }
        [DataMember]
        public decimal liquidateRate { get; set; }
        [DataMember]
        public bool tradeEnabled { get; set; }
        [DataMember]
        public bool enabled { get; set; }
    }

    /// <summary>
    /// Balance respomse providing information on assets
    /// </summary>
    [DataContract]
    public class IsolatedMarginAccountInfoResponse
    {
        [DataMember(Order = 1)]
        public List<AssetResponse> assets { get; set; }
    }
}
