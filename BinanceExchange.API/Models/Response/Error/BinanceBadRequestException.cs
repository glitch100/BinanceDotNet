namespace BinanceExchange.API.Models.Response.Error
{
    /// <summary>
    /// This exception is used when malformed requests are sent to the server. Please review the request object
    /// </summary>
    public class BinanceBadRequestException : BinanceException {
        public BinanceBadRequestException(BinanceError errorDetails) : base((string) "Malformed requests are sent to the server. Please review the request object/string", errorDetails)
        {
        }
    }
}