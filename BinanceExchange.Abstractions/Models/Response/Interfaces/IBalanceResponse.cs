namespace BinanceExchange.Abstractions.Models.Response.Interfaces
{
    public interface IBalanceResponse
    {
        string Asset { get; set; }
        decimal Free { get; set; }
        decimal Locked { get; set; }
    }
}