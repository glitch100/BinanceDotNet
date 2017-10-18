using NLog;

namespace BinanceExchange.API.Client
{
    public class ClientConfiguration
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public bool EnableRateLimiting { get; set; }
        public ILogger Logger { get; set; }
    }
}