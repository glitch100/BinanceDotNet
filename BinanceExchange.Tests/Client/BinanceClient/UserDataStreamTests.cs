using System;
using System.Threading.Tasks;
using BinanceExchange.API;
using BinanceExchange.API.Models.Response;
using Moq;
using Xunit;

namespace BinanceExchange.Tests.Client.BinanceClient
{
    public class UserDataStreamTests: BinanceClientBaseTests
    {
        [Fact]
        public async Task StartUserDataStream_Invoke_CallsProcessPostRequest()
        {
            // Arrange

            // Act
            await ConcreteBinanceClient.StartUserDataStream();

            // Assert
            MockAPIProcessor.Verify(a => a.ProcessPostRequest<UserDataStreamResponse>(
                It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.UserStream.StartUserDataStream.Uri)), 
                5000), 
                Times.Once()
            );
        }

        [Fact]
        public async Task KeepAliveUserDataStream_NullListenKey_Throws()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await ConcreteBinanceClient.KeepAliveUserDataStream(null));
        }

        [Fact]
        public async Task KeepAliveUserDataStream_ValidListenKey_CallsProcessPutRequest()
        {
            // Arrange
            var listenKey = "listenKey";

            // Act
            await ConcreteBinanceClient.KeepAliveUserDataStream(listenKey);

            // Assert
            MockAPIProcessor.Verify(a => a.ProcessPutRequest<UserDataStreamResponse>(
                It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.UserStream.KeepAliveUserDataStream(listenKey).Uri)), 
                5000), 
                Times.Once()
            );
        }

        [Fact]
        public async Task CloseUserDataStream_NullListenKey_Throws()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await ConcreteBinanceClient.CloseUserDataStream(null));
        }

        [Fact]
        public async Task CloseUserDataStream_ValidListenKey_CallsProcessPutRequest()
        {
            // Arrange
            var listenKey = "listenKey";

            // Act
            await ConcreteBinanceClient.CloseUserDataStream(listenKey);

            // Assert
            MockAPIProcessor.Verify(a => a.ProcessDeleteRequest<UserDataStreamResponse>(
                It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.UserStream.CloseUserDataStream(listenKey).Uri)), 
                5000), 
                Times.Once()
            );
        }
    }
}
