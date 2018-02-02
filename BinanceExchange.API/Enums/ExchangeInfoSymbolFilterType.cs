using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum ExchangeInfoSymbolFilterType
    {
        [EnumMember(Value = "PRICE_FILTER")]
        PriceFilter,
        [EnumMember(Value = "LOT_SIZE")]
        LotSize,
        [EnumMember(Value = "MIN_NOTIONAL")]
        MinNotional
    }
}