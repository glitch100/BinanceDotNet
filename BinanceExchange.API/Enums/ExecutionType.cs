using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum ExecutionType
    {
        [EnumMember(Value = "NEW")]
        New,
        [EnumMember(Value = "CANCELLED")]
        Cancelled,
        [EnumMember(Value = "REPLACED")]
        Replaced,
        [EnumMember(Value = "REJECTED")]
        Rejected,
        [EnumMember(Value = "TRADE")]
        Trade,
        [EnumMember(Value = "EXPIRED")]
        Expired,
    }
}