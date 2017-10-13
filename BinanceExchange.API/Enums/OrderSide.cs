using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum OrderSide
    {
        [EnumMember(Value = "BUY")]
        Buy,
        [EnumMember(Value = "SELL")]
        Sell,
    }
}