using System;
using System.Threading.Tasks;
using BinanceExchange.API.Client;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Request;

//"dynamic" requires a reference to Microsoft.CSharp

namespace BinanceExchange.API.Client.Trade
{
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
            dynamic accountInfo = await client.QueryIsolatedMarginAccountInfo(new IsolatedMarginAccountInfoRequest()
            {
                Symbols = symbol,
                TimeStamp = DateTime.Now.Ticks
            });

            dynamic x = accountInfo.assets[0];

            // query the account free asset coins:
            decimal freeCoins = 0m;
            if(asset == ((dynamic)x).baseAsset.asset.Value)
            {// the requested asset is the base
                freeCoins = decimal.Parse(((dynamic)x).baseAsset.free.Value);
            }
            else if(asset == ((dynamic)x).quoteAsset.asset.Value)
            {// the requested asset is the one used for quotes
                freeCoins = decimal.Parse(((dynamic)x).quoteAsset.free.Value);
            }
            else
            {
                throw new Exception("The requested asset info was not found under the specified account");
            }

            var maxBorrowAsset = await client.QueryMaxBorrow(new MaxBorrowRequest()
            {
                Asset = asset,
                IsolatedSymbol = symbol,
            });

            return maxBorrowAsset.amount + freeCoins;
        }

        private static async Task<bool> _BuySellCommand(BinanceClient client, string symbol, OrderSide buySell, SideEffectType sideEffectType, string isIsolated, decimal price, decimal quant)
        {
            var createIsolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
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

            return (true);
        }


        /// <summary>
        /// Send isolated account buy/sell command
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol">the trading symbol (BTCUSDT...)</param>
        /// <param name="asset">the trading asset such as BTC, USDT ...</param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static async Task<bool> BuyCommandIso(BinanceClient client, string symbol, decimal price, decimal quant)
        {
            bool res = _BuySellCommand(client, symbol, OrderSide.Buy, SideEffectType.MarginBuy, "TRUE", price, quant).Result;
            return res;
        }
        public static async Task<bool> SellCommandIso(BinanceClient client, string symbol, decimal price, decimal quant)
        {
            bool res = _BuySellCommand(client, symbol, OrderSide.Sell, SideEffectType.AutoRepay, "TRUE", price, quant).Result;
            return res;
        }


        /// <summary>
        /// Send isolated account market buy command
        /// </summary>
        /// <param name="client"></param>
        /// <param name="symbol">the trading symbol (BTCUSDT...)</param>
        /// <param name="asset">the trading asset such as BTC, USDT ...</param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static async Task<bool> BuyCommandMarket(BinanceClient client, string symbol, decimal quant)
        {
            var createIsolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
            {
                Quantity = quant,
                Side = OrderSide.Buy,
                Symbol = symbol,
                Type = OrderType.Market,
                NewOrderResponseType = NewOrderResponseType.Full,
                IsIsolated = "TRUE",
                SideEffectType = SideEffectType.MarginBuy,
            });

            return true;
        }


        public static async Task<bool> CloseBuyPosition(BinanceClient client, string symbol, decimal lastPrice)
        {
            var accountInfo = await client.QueryIsolatedMarginAccountInfo(new IsolatedMarginAccountInfoRequest()
            {
                Symbols = symbol,
                TimeStamp = DateTime.Now.Ticks
            });

            var x = accountInfo.assets[0];
            var AssetCoins = decimal.Parse(((dynamic)x).baseAsset.free.Value);
            var UsdtCoins = decimal.Parse(((dynamic)x).quoteAsset.free.Value);

            decimal CloseBuyQuant = Math.Floor(10m * AssetCoins) / 10m;

            if (CloseBuyQuant / lastPrice > 10m)
            {
                // Create an order with varying options
                var createIsolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
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
            }

            return true;
        }


        public static async Task<bool> CloseSellPosition(BinanceClient client, string symbol, decimal lastPrice)
        {
            var accountInfo = await client.QueryIsolatedMarginAccountInfo(new IsolatedMarginAccountInfoRequest()
            {
                Symbols = symbol,
                TimeStamp = DateTime.Now.Ticks
            });

            var x = accountInfo.assets[0];
            var AssetCoins = decimal.Parse(((dynamic)x).baseAsset.netAsset.Value);
            var UsdtCoins = decimal.Parse(((dynamic)x).quoteAsset.free.Value);

            decimal CloseSellQuant = -Math.Ceiling(10m * AssetCoins) / 10m;

            if (CloseSellQuant / lastPrice > 10m)
            {
                // Create an order with varying options
                var createIsolatedOrder = await client.CreateIsolatedOrder(new CreateIsolatedOrderRequest()
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
            }

            return true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////
    }
}
