using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum ExchangeInfoOrderType
    {
        [EnumMember(Value = "LIMIT")]
        Limit,
        [EnumMember(Value = "MARKET")]
        Market,
        [EnumMember(Value = "LIMIT_MAKER")]
        LimitMaker,
        [EnumMember(Value = "STOP_LOSS_LIMIT")]
        StopLossLimit,
        [EnumMember(Value = "TAKE_PROFIT_LIMIT")]
        TakeProfitLimit
    }
}
