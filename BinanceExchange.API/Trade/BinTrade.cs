using System;
using System.Threading.Tasks;
using BinanceExchange.API.Client;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;

namespace BinanceExchange.API.Client.Trade
{
    /// <summary>
    /// Be aware that at the time of writing this code, binace had a minimum threshold for comminting trades on 10.0 USD, below that the trade will fail
    /// </summary>
    public class MarginTrade
    {
        /// <summary>
        /// Send isolated account buy command
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol">the trading symbol (BTCUSDT...)</param>
        /// <param name="asset">for which the buy power is queried</param>
        /// <returns></returns>
        public static async Task<decimal> MaxBuyPowerInAsset(BinanceClient client, string symbol, string asset)
        {
            IsolatedMarginAccountInfoResponse accountInfo = await client.QueryIsolatedMarginAccountInfo(new IsolatedMarginAccountInfoRequest()
            {
                Symbols = symbol,
                TimeStamp = DateTime.UtcNow.Ticks
            });

            var x = accountInfo.assets[0];

            // query the account free asset coins:
            decimal freeCoins = 0m;
            if(asset == x.baseAsset.asset)
            {// the requested asset is the base
                freeCoins = x.baseAsset.free;
            }
            else if(asset == x.quoteAsset.asset)
            {// the requested asset is the one used for quotes
                freeCoins = x.quoteAsset.free;
            }
            else
            {
                throw new Exception("The requested asset info was not found under the specified accout.");
            }

            var maxBorrowAsset = await client.QueryMaxBorrow(new MaxBorrowRequest()
            {
                Asset = asset,
                IsolatedSymbol = symbol,
            });

            return maxBorrowAsset.amount + freeCoins;
        }

        private static async Task<long> BuySellCommand(BinanceClient client, string symbol, OrderSide buySell, SideEffectType sideEffectType, string isIsolated, decimal price, decimal quant)
        {
            var isolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
            {
                Price = price,
                Quantity = quant,
                Side = buySell,
                Symbol = symbol,
                Type = OrderType.Limit,
                NewOrderResponseType = NewOrderResponseType.Full,
                TimeInForce = TimeInForce.GTC,
                IsIsolated = isIsolated,
                SideEffectType = sideEffectType,
                TimeStamp = DateTime.UtcNow.Ticks
            });

            return isolatedOrder.OrderId;
        }


        /// <summary>
        /// Send isolated account buy/sell command
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol">the trading symbol (BTCUSDT...)</param>
        /// <param name="asset">the trading asset such as BTC, USDT ...</param>
        /// <param name="price"></param>
        /// <returns>OrderId</returns>
        public static async Task<long> BuyCommandIso(BinanceClient client, string symbol, decimal price, decimal quant)
        {
            var orderId = BuySellCommand(client, symbol, OrderSide.Buy, SideEffectType.MarginBuy, "TRUE", price, quant).Result;
            return orderId;
        }
        public static async Task<long> SellCommandIso(BinanceClient client, string symbol, decimal price, decimal quant)
        {
            var orderId = BuySellCommand(client, symbol, OrderSide.Sell, SideEffectType.AutoRepay, "TRUE", price, quant).Result;
            return orderId;
        }


        /// <summary>
        /// Send isolated account market buy / sell command
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol">the trading symbol (BTCUSDT...)</param>
        /// <param name="quant"></param>
        /// <returns></returns>
        public static async Task<long> BuyCommandMarket(BinanceClient client, string symbol, decimal quant)
        {
            var isolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
            {
                Quantity = quant,
                Side = OrderSide.Buy,
                Symbol = symbol,
                Type = OrderType.Market,
                NewOrderResponseType = NewOrderResponseType.Full,
                IsIsolated = "TRUE",
                SideEffectType = SideEffectType.MarginBuy,
            });

            return isolatedOrder.OrderId;
        }

        public static async Task<long> SellCommandMarket(BinanceClient client, string symbol, decimal quant)
        {
            var isolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
            {
                Quantity = quant,
                Side = OrderSide.Sell,
                Symbol = symbol,
                Type = OrderType.Market,
                NewOrderResponseType = NewOrderResponseType.Full,
                IsIsolated = "TRUE",
                SideEffectType = SideEffectType.MarginBuy,
            });

            return isolatedOrder.OrderId;
        }


        public static async Task<long> CloseBuyPositionMarket(BinanceClient client, string symbol)
        {
            var accountInfo = await client.QueryIsolatedMarginAccountInfo(new IsolatedMarginAccountInfoRequest()
            {
                Symbols = symbol,
                TimeStamp = DateTime.UtcNow.Ticks
            });

            var x = accountInfo.assets[0];
            decimal AssetCoins = x.baseAsset.free;
            decimal UsdtCoins = x.quoteAsset.free;

            decimal CloseBuyQuant = Math.Floor(10m * AssetCoins) / 10m;

            long res = -1;
            // Create an order with varying options
            var isolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
            {
                //Price = 0.5m,
                Quantity = CloseBuyQuant,
                Side = OrderSide.Sell,
                Symbol = symbol,
                Type = OrderType.Market,
                NewOrderResponseType = NewOrderResponseType.Full,
                //TimeInForce = TimeInForce.GTC, //error on market orders
                IsIsolated = "TRUE",
                SideEffectType = SideEffectType.AutoRepay,
                //TimeStamp = 1629287214
            });
            res = isolatedOrder.OrderId;
            return res;
        }


        public static async Task<long> CloseSellPositionMarket(BinanceClient client, string symbol)
        {
            var accountInfo = await client.QueryIsolatedMarginAccountInfo(new IsolatedMarginAccountInfoRequest()
            {
                Symbols = symbol,
                TimeStamp = DateTime.UtcNow.Ticks
            });

            var x = accountInfo.assets[0];
            var AssetCoins = x.baseAsset.netAsset;
            var UsdtCoins = x.quoteAsset.free;

            decimal CloseSellQuant = -Math.Ceiling(10m * AssetCoins) / 10m;
            long res = -1;
            // Create an order with varying options
            var isolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
            {
                //Price = 0.5m,
                Quantity = CloseSellQuant,
                Side = OrderSide.Buy,
                Symbol = symbol,
                Type = OrderType.Market,
                NewOrderResponseType = NewOrderResponseType.Full,
                //TimeInForce = TimeInForce.GTC, //error on market orders
                IsIsolated = "TRUE",
                SideEffectType = SideEffectType.AutoRepay,
                //TimeStamp = 1629287214
            });
            res = isolatedOrder.OrderId;
            return res;
        }
        ////////////////////////////////////////////////////////////////////////////////////////
    }
}
