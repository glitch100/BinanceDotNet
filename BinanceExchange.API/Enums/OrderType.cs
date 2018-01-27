using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum OrderType
    {
        [EnumMember(Value = "LIMIT")]
        Limit,
        [EnumMember(Value = "MARKET")]
        Market,
    }
}
