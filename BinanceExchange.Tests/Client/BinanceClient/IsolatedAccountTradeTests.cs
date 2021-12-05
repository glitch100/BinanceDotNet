using System.Threading.Tasks;
using BinanceExchange.API;
using BinanceExchange.API.Client.Trade;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Request;
using BinanceExchange.API.Models.Response;
using Moq;
using Xunit;

namespace BinanceExchange.Tests.Client.BinanceClient
{
    public class IsolatedAccountTradeTests : BinanceClientBaseTests
    {
        [Fact]
        public async Task TestBuyMarket_Throws()
        {
            // Arrange
            const decimal percentOfMaxWillingToUse = 0.2m;            //if the cash+borrow is 100 and percentOfMaxWillingToUse is 0.2 you'll risk 20
            await ConcreteBinanceClient.TestConnectivity();
            decimal initialAmount = await MarginTrade.MaxBuyPowerInAsset(ConcreteBinanceClient, "BTCUSDT", "BTC");
            decimal useAmount = percentOfMaxWillingToUse * initialAmount;

            // Act
            await MarginTrade.BuyCommandMarket(ConcreteBinanceClient, "BTCUSDT", useAmount);
            decimal afterBuyAmount = await MarginTrade.MaxBuyPowerInAsset(ConcreteBinanceClient, "BTCUSDT", "BTC");  // make sure that after buy the amount get's lower
            await MarginTrade.CloseBuyPositionMarket(ConcreteBinanceClient, "BTCUSDT");


            // Assert
            decimal finalAmount = await MarginTrade.MaxBuyPowerInAsset(ConcreteBinanceClient, "BTCUSDT", "BTC");
            // gestimate -> upon successful buy the amount available should be reduced at list by x:  (subtract 5% to avoid false alarms)
            decimal x = (percentOfMaxWillingToUse * initialAmount) * 0.95m;
            Assert.True(initialAmount - afterBuyAmount > x, "");
            Assert.True(initialAmount * 0.9m < finalAmount, "");
        }
    }
}