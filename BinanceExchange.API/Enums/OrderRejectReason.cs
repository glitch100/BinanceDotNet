using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum OrderRejectReason
    {
        [EnumMember(Value = "NONE")]
        None,
        [EnumMember(Value = "UNKNOWN_INSTRUMENT")]
        UnknownInstrument,
        [EnumMember(Value = "MARKET_CLOSED")]
        MarketClosed,
        [EnumMember(Value = "PRICE_QTY_EXCEED_HARD_LIMITS")]
        PriceQuantityExceededHardLimits,
        [EnumMember(Value = "UNKNOWN_ORDER")]
        UnknownOrder,
        [EnumMember(Value = "DUPLICATE_ORDER")]
        DuplicateOrder,
        [EnumMember(Value = "UNKNOWN_ACCOUNT")]
        UnknownAccount,
        [EnumMember(Value = "INSUFFICIENT_BALANCE")]
        InsufficientBalance,
        [EnumMember(Value = "ACCOUNT_INACTIVE")]
        AccountInactive,
        [EnumMember(Value = "ACCOUNT_CANNOT_SETTLE")]
        AccountCannotSettle,
    }
}