using System.Threading.Tasks;
using BinanceExchange.API;
using BinanceExchange.API.Models.Response;
using Moq;
using Xunit;

namespace BinanceExchange.Tests.Client.BinanceClient
{
    public class GeneralEndpointTests : BinanceClientBaseTests
    {
        [Fact]
        public async Task TestConnectivity_Invoke_CallsProcessGetRequest()
        {
            // Arrange

            // Act
            await ConcreteBinanceClient.TestConnectivity();

            // Assert
            MockAPIProcessor.Verify(a => a.ProcessGetRequest<EmptyResponse>(
                    It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.General.TestConnectivity.Uri)), 
                    5000),
                Times.Once()
            );
        }

        [Fact]
        public async Task GetServerTime_Invoke_CallsProcessGetRequest()
        {
            // Arrange

            // Act
            await ConcreteBinanceClient.GetServerTime();

            // Assert
            MockAPIProcessor.Verify(a => a.ProcessGetRequest<ServerTimeResponse>(
                    It.Is<BinanceEndpointData>(u => u.Uri.Equals(Endpoints.General.ServerTime.Uri)), 
                    5000),
                Times.Once()
            );
        }
    }
}