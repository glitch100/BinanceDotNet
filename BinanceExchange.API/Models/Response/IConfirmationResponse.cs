
namespace BinanceExchange.API.Models.Response
{
    public interface IConfirmationResponse : IResponse
    {
        bool Success { get; set; }
    }
}