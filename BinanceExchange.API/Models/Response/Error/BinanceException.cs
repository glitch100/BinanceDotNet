using System;

namespace BinanceExchange.API.Models.Response.Error
{
    public class BinanceException: Exception
    {
        public BinanceError ErrorDetails { get; set; }

        public BinanceException(string message, BinanceError errorDetails):base(message)
        {
            ErrorDetails = errorDetails;
        }
    }
}