using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum SideEffectType
    {
        [EnumMember(Value = "NO_SIDE_EFFECT")]
        NoSideEffect,
        [EnumMember(Value = "MARGIN_BUY")]
        MarginBuy,
        [EnumMember(Value = "AUTO_REPAY")]
        AutoRepay,
    }
}