using System.Threading;
using System.Threading.Tasks;
using BinanceExchange.API.Client;
using BinanceExchange.API.Models.Request;
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
            string secretKey = "YOUR_SECRET";
            System.Console.WriteLine("--------------------------");
            System.Console.WriteLine("BinanceExchange API - Tester");
            System.Console.WriteLine("--------------------------");

            //Init client with config
            var client = new BinanceClient(new ClientConfiguration()
            {
                ApiKey = apiKey,
                SecretKey = secretKey,
            });
            System.Console.WriteLine("Interacting with Binance...");

            //Run Client
            await client.TestConnectivity();

            var results = await client.GetAllOrders(new AllOrdersRequest()
            {
                Symbol = "ETHBTC",
                Limit = 5,
            });

            var userData = await client.StartUserDataStream();
            await client.KeepAliveUserDataStream(userData.ListenKey);
            await client.CloseUserDataStream(userData.ListenKey);

            //Web Socket example.
            using (var binanceWebSocketClient = new BinanceWebSocketClient(client))
            {
                binanceWebSocketClient.ConnectToDepthWebSocket("ETHBTC", data =>
                {
                    System.Console.WriteLine($"DepthCall: {JsonConvert.SerializeObject(data)}");
                });

                Thread.Sleep(180000);
            }

            System.Console.WriteLine("Complete...");
            System.Console.ReadLine();
        }
    }
}
