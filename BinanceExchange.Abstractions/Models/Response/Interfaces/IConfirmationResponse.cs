
namespace BinanceExchange.Abstractions.Models.Response.Interfaces
{
    public interface IConfirmationResponse : IResponse
    {
        bool Success { get; set; }
    }
}