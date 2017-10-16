namespace BinanceExchange.API.Models.Response
{
    public interface IBalanceResponse
    {
        string Asset { get; set; }
        decimal Free { get; set; }
        decimal Locked { get; set; }
    }
}