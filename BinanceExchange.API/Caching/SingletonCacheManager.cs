namespace BinanceExchange.API.Caching
{
    /// <summary>
    /// Singleton Cache Manager
    /// </summary>
    public static class SingletonCacheManager
    {
        static SingletonCacheManager()
        {
        }

        public static APICacheManager Instance { get; } = new APICacheManager();
    }
}