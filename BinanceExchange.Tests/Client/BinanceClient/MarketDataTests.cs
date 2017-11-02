using System;
using System.Threading.Tasks;
using BinanceExchange.API;
using BinanceExchange.API.Market;
using BinanceExchange.API.Models.Response;
using Moq;
using Xunit;

namespace BinanceExchange.Tests.Client.BinanceClient
{
    public class MarketDataTests : BinanceClientBaseTests
    {
        [Fact]
        public async Task GetOrderBook_NullSymbol_Throws()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => ConcreteBinanceClient.GetOrderBook(null));
        }

        [Fact]
        public async Task GetOrderBook_ExcessiveLimit_Throws()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => ConcreteBinanceClient.GetOrderBook(TradingPairSymbols.BNBPairs.BNB_BTC, false, 9999));
        }

        [Fact]
        public async Task GetOrderBook_ValidArguments_CallsProcessGetRequest()
        {
            // Arrange

            // Act
            await ConcreteBinanceClient.GetOrderBook(TradingPairSymbols.BNBPairs.BNB_BTC);

            // Assert
            MockAPIProcessor.Verify(a => a.ProcessGetRequest<OrderBookResponse>(
                    It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.MarketData.OrderBook(TradingPairSymbols.BNBPairs.BNB_BTC, 100, false).Uri)),
                    5000),
                Times.Once()
            );
        }
    }
}