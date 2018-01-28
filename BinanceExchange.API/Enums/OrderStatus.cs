using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum OrderStatus
    {
        [EnumMember(Value = "NEW")]
        New,
        [EnumMember(Value = "PARTIALLY_FILLED")]
        PartiallyFilled,
        [EnumMember(Value = "FILLED")]
        Filled,
        [EnumMember(Value = "CANCELED")]
        Cancelled,
        [EnumMember(Value = "PENDING_CANCEL")]
        PendingCancel,
        [EnumMember(Value = "REJECTED")]
        Rejected,
        [EnumMember(Value = "EXPIRED")]
        Expired,
    }
}