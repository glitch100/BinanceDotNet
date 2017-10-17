using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BinanceExchange.API.Caching;
using BinanceExchange.API.Client;
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

            // Get Order Book, and use Cache
            var results = await client.GetOrderBook("ETHBTC", true, 100);

            // User Data Stream run through
            var userData = await client.StartUserDataStream();
            await client.KeepAliveUserDataStream(userData.ListenKey);
            await client.CloseUserDataStream(userData.ListenKey);

            // Code example of building out a Dictionary local cache for a symbol
            // with no expiration on trades/offers.
            var localDepthCache = new Dictionary<string, SymbolCacheItem>();
            var defaultIgnoreValue = 0.00000000M;
            var tradeFactoryFunc = new Func<List<TradeResponse>, List<Trade>>((lt) =>
            {
                return lt.Where(ad => ad.Price != defaultIgnoreValue).Select(ad => new Trade()
                {
                    Price = ad.Price,
                    Quantity = ad.Quantity,
                }).ToList();
            });
            using (var binanceWebSocketClient = new BinanceWebSocketClient(client))
            {
                binanceWebSocketClient.ConnectToDepthWebSocket("ETHBTC", data =>
                {
                    if (localDepthCache.ContainsKey(data.Symbol))
                    {
                        var entry = localDepthCache[data.Symbol];
                        if (entry.CurrentUpdateId < data.UpdateId)
                        {
                            data.BidDepthDeltas.Where(bd => bd.Price != defaultIgnoreValue && bd.Quantity != defaultIgnoreValue).ToList().ForEach(
                                bd => entry.Bids.Add(new Trade()
                                {
                                    Price = bd.Price,
                                    Quantity = bd.Quantity
                                }));
                            data.AskDepthDeltas.Where(ad => ad.Price != defaultIgnoreValue && ad.Quantity != defaultIgnoreValue).ToList().ForEach(
                                ad => entry.Asks.Add(new Trade()
                                {
                                    Price = ad.Price,
                                    Quantity = ad.Quantity
                                }));
                        }
                    }
                    else
                    {
                        localDepthCache.Add(data.Symbol, new SymbolCacheItem(data.Symbol, data.UpdateId)
                        {
                            Asks = new List<Trade>(tradeFactoryFunc(data.AskDepthDeltas)),
                            Bids = new List<Trade>(tradeFactoryFunc(data.BidDepthDeltas)),
                        });
                    }
                    System.Console.WriteLine($"Depth Cal JSON: {JsonConvert.SerializeObject(data)}");
                });

                Thread.Sleep(480000);
            }

            System.Console.WriteLine("Complete...");
            System.Console.ReadLine();
        }
    }
}
