using BinanceExchange.API;
using BinanceExchange.API.Client;
using Moq;
using NLog;

namespace BinanceExchange.Tests.Client.BinanceClient
{
    public class BinanceClientBaseTests
    {
        protected API.Client.BinanceClient ConcreteBinanceClient;
        protected ClientConfiguration DefaultClientConfiguration;

        protected Mock<ILogger> MockLogger;
        protected Mock<IAPIProcessor> MockAPIProcessor;

        public BinanceClientBaseTests()
        {
            MockLogger = new Mock<ILogger>();
            MockAPIProcessor = new Mock<IAPIProcessor>();

            DefaultClientConfiguration = new ClientConfiguration()
            {
                ApiKey = "APIKEY",
                SecretKey = "SECRETKEY",
                Logger = MockLogger.Object,
            };
            ConcreteBinanceClient = new API.Client.BinanceClient(DefaultClientConfiguration, MockAPIProcessor.Object);
        }
    }
}
