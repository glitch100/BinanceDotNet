namespace BinanceExchange.API.Models.Response.Error
{
    /// <summary>
    /// This exception is used when the request is valid but there was an error on the server side
    /// </summary>
    public class BinanceServerException : BinanceException
    {
        public BinanceServerException(BinanceError errorDetails) : base((string) "Request to BinanceAPI is valid but there was an error on the server side", errorDetails)
        {
        }
    }
}