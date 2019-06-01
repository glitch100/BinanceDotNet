using System.Runtime.Serialization;

namespace BinanceExchange.API.Enums
{
    /// <see cref="https://github.com/binance-exchange/binance-official-api-docs/blob/master/rest-api.md#filters"/>
    public enum ExchangeInfoSymbolFilterType
    {
        #region Symbol filters

        [EnumMember(Value = "PRICE_FILTER")]
        PriceFilter,
        [EnumMember(Value = "PERCENT_PRICE")]
        PercentPrice,
        [EnumMember(Value = "LOT_SIZE")]
        LotSize,
        [EnumMember(Value = "MIN_NOTIONAL")]
        MinNotional,
        [EnumMember(Value = "MARKET_LOT_SIZE")]
        MarketLotSize,
        [EnumMember(Value = "MAX_NUM_ORDERS")]
        MaxNumOrders,
        [EnumMember(Value = "MAX_NUM_ALGO_ORDERS")]
        MaxNumAlgoOrders,
        [EnumMember(Value = "MAX_NUM_ICEBERG_ORDERS")]
        MaxNumIcebergOrders,

        #endregion

        #region Exchange Filters

        [EnumMember(Value = "EXCHANGE_MAX_NUM_ORDERS")]
        ExchangeMaxNumOrders,
        [EnumMember(Value = "EXCHANGE_MAX_NUM_ALGO_ORDERS")]
        ExchangeMaxNumAlgoOrders,
        #endregion
        [EnumMember(Value = "PERCENTAGE_PRICE")]
        PercentagePrice,
        [EnumMember(Value = "ICEBERG_PARTS")]
        IcebergParts,
    }
}