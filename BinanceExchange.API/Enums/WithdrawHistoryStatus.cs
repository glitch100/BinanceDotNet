namespace BinanceExchange.API.Enums
{
    public enum WithdrawHistoryStatus
    {
        EmailSent = 0,
        Cancelled = 1,
        AwaitingApproval = 2,
        Rejected = 3,
        Processing = 4,
        Failure = 5,
        Completed = 6
    }
}