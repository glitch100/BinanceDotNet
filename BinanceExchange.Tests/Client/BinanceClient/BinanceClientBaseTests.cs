using BinanceExchange.API;
using BinanceExchange.API.Client;
using log4net;
using Moq;

namespace BinanceExchange.Tests.Client.BinanceClient
{
    public class BinanceClientBaseTests
    {
        protected API.Client.BinanceClient ConcreteBinanceClient;
        protected ClientConfiguration DefaultClientConfiguration;

        protected Mock<ILog> MockLogger;
        protected Mock<IAPIProcessor> MockAPIProcessor;

        public BinanceClientBaseTests()
        {
            MockLogger = new Mock<ILog>();
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
