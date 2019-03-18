using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.WebSocket;
using Newtonsoft.Json;
using Xunit;

namespace BinanceExchange.Tests.Client.BinanceClient.ModelTests
{
    public class BinanceTradeOrderDataTests
    {
        [Fact]
        public void CanDeserializeCaseSensitiveProperties()
        {
            var jsonPayload =
                "{ \"e\": \"executionReport\", \"E\": 1552602237245, \"s\": \"BNBBTC\", \"c\": \"pc_12cbcdd2ebbe4893a5d5de4ef27b3954\", \"S\": \"SELL\", \"o\": \"LIMIT\", \"f\": \"GTC\", \"q\": \"0.62000000\", \"p\": \"0.00487400\", \"P\": \"0.00000000\", \"F\": \"0.00000000\", \"g\": -1, \"C\": \"pc_31471433322c4166a05c58152650c96a\", \"x\": \"CANCELED\", \"X\": \"CANCELED\", \"r\": \"NONE\", \"i\": 124465346, \"l\": \"0.00000000\", \"z\": \"0.00000000\", \"L\": \"0.00000000\", \"n\": \"0\", \"N\": null, \"T\": 1552602237240, \"t\": -1, \"I\": 286808464, \"w\": false, \"m\": false, \"M\": false, \"O\": 1552601178372, \"Z\": \"0.00000000\", \"Y\": \"0.00000000\" }";

            var result = JsonConvert.DeserializeObject<BinanceTradeOrderData>(jsonPayload);

            Assert.Equal(124465346,result.OrderId);
            Assert.Equal(OrderType.Limit,result.Type);
        }
    }
}
