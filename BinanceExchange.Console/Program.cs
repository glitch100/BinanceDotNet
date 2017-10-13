using System.Threading.Tasks;
using BinanceExchange.API.Client;

namespace BinanceExchange.Console
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //Provide your configuration

            string apiKey = "YOUR_API_KEY";
            string secretKey = "YOUR_SECRET_KEY";
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
            await  client.TestConnectivity();
            var response = await client.GetServerTime();
            System.Console.WriteLine($"Server Time: {response.ServerTime}");
            System.Console.WriteLine("Complete...");
            System.Console.ReadLine();
        }
    }
}
