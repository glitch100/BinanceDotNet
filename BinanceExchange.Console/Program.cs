using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BinanceExchange.API.Caching;
using BinanceExchange.API.Client;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;
using BinanceExchange.API.Models.Websocket;
using BinanceExchange.API.Websockets;
using Newtonsoft.Json;

namespace BinanceExchange.Console
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //Provide your configuration

            string apiKey = "YOUR_KEY";
            string secretKey = "YOUR_SECRET_KEY";
            System.Console.WriteLine("--------------------------");
            System.Console.WriteLine("BinanceExchange API - Tester");
            System.Console.WriteLine("--------------------------");

            //Init client with config
            var client = new BinanceClient(new ClientConfiguration()
            {
                ApiKey = apiKey,
                SecretKey = secretKey,
            }, new APICacheManager());
            System.Console.WriteLine("Interacting with Binance...");

            //Run Client
            await client.TestConnectivity();

            // Get All Orders
            var request = new AllOrdersRequest()
            {
                Symbol = "ETHBTC",
                Limit = 5,
            };
            var allOrders = client.GetAllOrders(request);


            // User Data Stream run through
            var userData = await client.StartUserDataStream();
            await client.KeepAliveUserDataStream(userData.ListenKey);
            await client.CloseUserDataStream(userData.ListenKey);


            #region Advanced Examples
            //await BuildLocalKlineCache(client);
            //await BuildLocalDepthCache(client);
            #endregion

            System.Console.WriteLine("Complete...");
            System.Console.ReadLine();
        }

        /// <summary>
        /// Build local Depth cache from WebSocket and API Call example.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static async Task BuildLocalDepthCache(IBinanceClient client)
        {
            // Code example of building out a Dictionary local cache for a symbol using deltas from the WebSocket
            var localDepthCache = new Dictionary<string, DepthCacheObject> {{ "BNBBTC", new DepthCacheObject()
            {
                Asks = new Dictionary<decimal, decimal>(),
                Bids = new Dictionary<decimal, decimal>(),
            }}};
            var bnbBtcDepthCache = localDepthCache["BNBBTC"];

            // Get Order Book, and use Cache
            var depthResults = await client.GetOrderBook("BNBBTC", true, 100);
            //Populate our depth cache
            depthResults.Asks.ForEach(a =>
            {
                if (a.Quantity != 0.00000000M)
                {
                    bnbBtcDepthCache.Asks.Add(a.Price, a.Quantity);
                }
            });
            depthResults.Bids.ForEach(a =>
            {
                if (a.Quantity != 0.00000000M)
                {
                    bnbBtcDepthCache.Bids.Add(a.Price, a.Quantity);
                }
            });

            // Store the last update from our result set;
            long lastUpdateId = depthResults.LastUpdateId;
            using (var binanceWebSocketClient = new BinanceWebSocketClient(client))
            {
                binanceWebSocketClient.ConnectToDepthWebSocket("BNBBTC", data =>
                {
                    if (lastUpdateId < data.UpdateId)
                    {
                        data.BidDepthDeltas.ForEach((bd) =>
                        {
                            CorrectlyUpdateDepthCache(bd, bnbBtcDepthCache.Bids);
                        });
                        data.AskDepthDeltas.ForEach((ad) =>
                        {
                            CorrectlyUpdateDepthCache(ad, bnbBtcDepthCache.Asks);
                        });
                    }
                    lastUpdateId = data.UpdateId;
                    System.Console.Clear();
                    System.Console.WriteLine($"{JsonConvert.SerializeObject(bnbBtcDepthCache, Formatting.Indented)}");
                    System.Console.SetWindowPosition(0, 0);
                });

                Thread.Sleep(480000);
            }
        }

        /// <summary>
        /// Build Local Kline Cache from WebSocket and API Call example.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static async Task BuildLocalKlineCache(IBinanceClient client)
        {
            long epochTicks = new DateTime(1970, 1, 1).Ticks;

            // Code example of building out a Dictionary local cache for a symbol using deltas from the WebSocket
            var localKlineCache = new Dictionary<string, KlineCacheObject> {{ "BNBBTC", new KlineCacheObject()
            {
                KlineInterDictionary = new Dictionary<KlineInterval, KlineIntervalCacheObject>()
                {
                    {KlineInterval.OneMinute,  new KlineIntervalCacheObject() }
                }
            }}};

            // Get Kline Results, and use Cache
            var startTime = DateTime.UtcNow.AddHours(-1);
            var startTimeKeyTime = (startTime.Ticks - epochTicks) / TimeSpan.TicksPerSecond;
            var klineResults = await client.GetKlinesCandlesticks(new GetKlinesCandlesticksRequest()
            {
                Symbol = "BNBBTC",
                Interval = KlineInterval.OneMinute,
                StartTime = startTime,
                EndTime = DateTime.UtcNow,
            });

            var oneMinKlineCache = localKlineCache["BNBBTC"].KlineInterDictionary[KlineInterval.OneMinute];
            oneMinKlineCache.TimeKlineDictionary = new Dictionary<long, KlineCandleStick>();
            var instanceKlineCache = oneMinKlineCache.TimeKlineDictionary;
            //Populate our kline cache with initial results
            klineResults.ForEach(k =>
            {
                instanceKlineCache.Add(((k.OpenTime.Ticks - epochTicks) / TimeSpan.TicksPerSecond), new KlineCandleStick()
                {
                    Close = k.Close,
                    High = k.High,
                    Low = k.Low,
                    Open = k.Open,
                    Volume = k.Volume,
                });
            });

            // Store the last update from our result set;
            using (var binanceWebSocketClient = new BinanceWebSocketClient(client))
            {
                binanceWebSocketClient.ConnectToKlineWebSocket("BNBBTC", KlineInterval.OneMinute, data =>
                {
                    var keyTime = ((data.Kline.StartTime.Ticks - epochTicks) / TimeSpan.TicksPerSecond);
                    var klineObj = new KlineCandleStick()
                    {
                        Close = data.Kline.Close,
                        High = data.Kline.High,
                        Low = data.Kline.Low,
                        Open = data.Kline.Open,
                        Volume = data.Kline.Volume,
                    };
                    if (!data.Kline.IsBarFinal)
                    {
                        if (keyTime < startTimeKeyTime)
                        {
                            return;
                        }

                        TryAddUpdateKlineCache(instanceKlineCache, keyTime, klineObj);
                    }
                    else
                    {
                        TryAddUpdateKlineCache(instanceKlineCache, keyTime, klineObj);
                    }
                    System.Console.Clear();
                    System.Console.WriteLine($"{JsonConvert.SerializeObject(instanceKlineCache, Formatting.Indented)}");
                    System.Console.SetWindowPosition(0, 0);
                });

                Thread.Sleep(480000);
            }
        }

        private static void TryAddUpdateKlineCache(Dictionary<long, KlineCandleStick> primary, long keyTime, KlineCandleStick klineObj)
        {
            if (primary.ContainsKey(keyTime))
            {
                primary[keyTime] = klineObj;
            }
            else
            {
                primary.Add(keyTime, klineObj);
            }
        }

        private static void CorrectlyUpdateDepthCache(TradeResponse bd,  Dictionary<decimal, decimal> depthCache)
        {
            const decimal defaultIgnoreValue = 0.00000000M;

            if (depthCache.ContainsKey(bd.Price))
            {
                if (bd.Quantity == defaultIgnoreValue)
                {
                    depthCache.Remove(bd.Price);
                }
                else
                {
                    depthCache[bd.Price] = bd.Quantity;
                }
            }
            else
            {
                if (bd.Quantity != defaultIgnoreValue)
                {
                    depthCache[bd.Price] = bd.Quantity;
                }
            }
        }
    }
}
