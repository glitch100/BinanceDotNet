using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    public enum ExchangeInfoSymbolFilterType
    {
        #region Symbol filters

        [EnumMember(Value = "PRICE_FILTER")]
        PriceFilter,
        [EnumMember(Value = "LOT_SIZE")]
        LotSize,
        [EnumMember(Value = "MIN_NOTIONAL")]
        MinNotional,
        [EnumMember(Value = "MAX_NUM_ORDERS")]
        MaxNumOrders,
        [EnumMember(Value = "MAX_NUM_ALGO_ORDERS")]
        MaxNumAlgoOrders,
        [EnumMember(Value = "ICEBERG_PARTS")]
        IcebergParts,

        #endregion

        #region Exchange Filters

        [EnumMember(Value = "EXCHANGE_MAX_NUM_ORDERS")]
        ExchangeMaxNumOrders,
        [EnumMember(Value = "EXCHANGE_MAX_NUM_ALGO_ORDERS")]
        ExchangeMaxNumAlgoOrders,

        #endregion
    }
}