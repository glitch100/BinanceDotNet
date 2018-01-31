using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum OrderType
    {
        [EnumMember(Value = "LIMIT")]
        Limit,
        [EnumMember(Value = "MARKET")]
        Market,
        [EnumMember(Value = "STOP_LOSS")]
        StopLoss,
        [EnumMember(Value = "STOP_LOSS_LIMIT")]
        StopLossLimit,
        [EnumMember(Value = "TAKE_PROFIT")]
        TakeProfit,
        [EnumMember(Value = "TAKE_PROFIT_LIMIT")]
        TakeProfitLimit,
        [EnumMember(Value = "LIMIT_MAKER")]
        LimitMaker,
    }
}
