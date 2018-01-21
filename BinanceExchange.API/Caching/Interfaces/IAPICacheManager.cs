using System;

namespace BinanceExchange.API.Caching.Interfaces
{
    public interface IAPICacheManager
    {
        /// <summary>
        /// Add an object to the cache
        /// </summary>
        /// <typeparam name="T">Type of object to be added</typeparam>
        /// <param name="obj">The object to add</param>
        /// <param name="key">The key to identify the cache entry</param>
        /// <param name="expiry">When the cache should expire</param>
        void Add<T>(T obj, string key, TimeSpan expiry = default(TimeSpan)) where T : class;

        /// <summary>
        /// Retrieve an item from the cache
        /// </summary>
        /// <typeparam name="T">The type of the object being retrieved</typeparam>
        /// <param name="key">The key to identify the cache entry</param>
        /// <returns></returns>
        T Get<T>(string key) where T : class;

        /// <summary>
        /// Check if the cache contains an item
        /// </summary>
        /// <param name="key">The key to identify the cache entry</param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// Remove an entry from the cache
        /// </summary>
        /// <param name="key">The key to identify the cache entry</param>
        void Remove(string key);

        /// <summary>
        /// Remove all items from the cache
        /// </summary>
        void RemoveAll();
    }
}