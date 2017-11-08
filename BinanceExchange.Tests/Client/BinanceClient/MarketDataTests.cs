using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceExchange.API;
using BinanceExchange.API.Market;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;
using Moq;
using Xunit;

namespace BinanceExchange.Tests.Client.BinanceClient
{
    public class MarketDataTests : BinanceClientBaseTests
    {
        #region GetOrderBook
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
        #endregion

        #region GetCompressedAggregateTrades
        [Fact]
        public async Task GetCompressedAggregateTrades_Null_Throws()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => ConcreteBinanceClient.GetCompressedAggregateTrades(null));
        }

        [Fact]
        public async Task GetCompressedAggregateTrades_NullSymbol_Throws()
        {
            // Arrange
            var request = new GetCompressedAggregateTradesRequest()
            {
                Symbol = null,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => ConcreteBinanceClient.GetCompressedAggregateTrades(request));
        }

        [Fact]
        public async Task GetCompressedAggregateTrades_StartTimeMin_Throws()
        {
            // Arrange
            var request = new GetCompressedAggregateTradesRequest()
            {
                Symbol = "ETHBTC",
                StartTime = DateTime.MinValue,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => ConcreteBinanceClient.GetCompressedAggregateTrades(request));
        }

        [Fact]
        public async Task GetCompressedAggregateTrades_EndTimeMin_Throws()
        {
            // Arrange
            var request = new GetCompressedAggregateTradesRequest()
            {
                Symbol = "ETHBTC",
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.MinValue,
            };

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => ConcreteBinanceClient.GetCompressedAggregateTrades(request));
        }

        [Fact]
        public async Task GetCompressedAggregateTrades_ValidRequest_CallsProcessGetRequest()
        {
            // Arrange
            var request = new GetCompressedAggregateTradesRequest()
            {
                Symbol = "ETHBTC",
                StartTime = DateTime.UtcNow.AddHours(-1),
                EndTime = DateTime.UtcNow,
            };

            // Act
            await ConcreteBinanceClient.GetCompressedAggregateTrades(request);

            // Assert
            MockAPIProcessor.Verify(i => i.ProcessGetRequest<List<CompressedAggregateTradeResponse>>(
                It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.MarketData.CompressedAggregateTrades(request).Uri)),
                5000),
                Times.Once());
        }

        [Fact]
        public async Task GetCompressedAggregateTrades_InvalidLimitTooLow_CallsProcessGetRequest()
        {
            // Arrange
            var request = new GetCompressedAggregateTradesRequest()
            {
                Symbol = "ETHBTC",
                StartTime = DateTime.UtcNow.AddHours(-1),
                EndTime = DateTime.UtcNow,
                Limit = -1,
            };

            // Act
            await ConcreteBinanceClient.GetCompressedAggregateTrades(request);

            // Assert
            request.Limit = 500;
            MockAPIProcessor.Verify(i => i.ProcessGetRequest<List<CompressedAggregateTradeResponse>>(
                It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.MarketData.CompressedAggregateTrades(request).Uri)),
                5000),
                Times.Once());
        }

        [Fact]
        public async Task GetCompressedAggregateTrades_InvalidLimitTooHigh_CallsProcessGetRequest()
        {
            // Arrange
            var request = new GetCompressedAggregateTradesRequest()
            {
                Symbol = "ETHBTC",
                StartTime = DateTime.UtcNow.AddHours(-1),
                EndTime = DateTime.UtcNow,
                Limit = 501,
            };

            // Act
            await ConcreteBinanceClient.GetCompressedAggregateTrades(request);

            // Assert
            request.Limit = 500;
            MockAPIProcessor.Verify(i => i.ProcessGetRequest<List<CompressedAggregateTradeResponse>>(
                It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.MarketData.CompressedAggregateTrades(request).Uri)),
                5000),
                Times.Once());
        }
        #endregion


    }
}