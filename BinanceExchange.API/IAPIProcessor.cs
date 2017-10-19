using System;
using System.Threading.Tasks;

namespace BinanceExchange.API
{
    public interface IAPIProcessor
    {
        /// <summary>
        /// Set the cache expiry time
        /// </summary>
        /// <param name="time"></param>
        void SetCacheTime(TimeSpan time);

        /// <summary>
        /// Processes a GET request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<T> ProcessGetRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class;

        /// <summary>
        /// Processes a DELETE request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<T> ProcessDeleteRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class;

        /// <summary>
        /// Processes a POST request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<T> ProcessPostRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class;

        /// <summary>
        /// Processes a PUT request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        Task<T> ProcessPutRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class;
    }
}