using System;
using System.Collections.Generic;
using BinanceExchange.API.Models.ResultSets;
using BinanceExchange.API.Models.WebSocket;

namespace BinanceExchange.API
{
    /// <summary>
    /// A set of methods that can transform various API results into more complex and meaningful results
    /// </summary>
    public static class ResultTransformations
    {
        /// <summary>
        /// Calculate the Buy | Sell volume from the provided Depth Cache
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="depthCacheObject"></param>
        /// <returns></returns>
        public static BuySellDepthVolume CalculateTradeVolumeFromDepth(string symbol, Dictionary<string, DepthCacheObject> depthCacheObject)
        {
            if (!depthCacheObject.ContainsKey(symbol))
            {
                throw new Exception($"No such symbol found in DepthCache: '{symbol}'");
            }

            var symbolDepth = depthCacheObject[symbol];
            decimal bidQuantity = 0;
            decimal bidBase = 0;
            decimal askBase = 0;
            decimal askQuantity = 0;
            foreach (var priceKey in symbolDepth.Bids.Keys)
            {
                var quantity = symbolDepth.Bids[priceKey];
                bidBase += bidQuantity * priceKey;
                bidQuantity += quantity;
            }
            foreach (var priceKey in symbolDepth.Asks.Keys)
            {
                var quantity = symbolDepth.Asks[priceKey];
                askBase += askQuantity * priceKey;
                askQuantity += quantity;
            }
            return new BuySellDepthVolume
            {
                AskQuantity = askQuantity,
                AskBase = askBase,
                BidBase = bidBase,
                BidQuantity = bidQuantity,
            };
        }
    }
}
