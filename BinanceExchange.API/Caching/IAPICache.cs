using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceExchange.API.Caching
{
    /// <summary>
    /// Implement this cache to utilise the cache within the Binance Client
    /// </summary>
    public interface IAPICache
    {
        void Add<T>(T obj, string key, int expiryHours) where T : class;
        T Get<T>(string key) where T : class;
        bool Contains(string key);
        void Remove(string key);
        void RemoveAll();
    }
}
