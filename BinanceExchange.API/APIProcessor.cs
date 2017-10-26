using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BinanceExchange.API.Caching;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Response.Error;
using Newtonsoft.Json;
using NLog;

namespace BinanceExchange.API
{
    /// <summary>
    /// The API Processor is the chief piece of functionality responsible for handling and creating requests to the API
    /// </summary>
    public class APIProcessor : IAPIProcessor
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private IAPICacheManager _apiCache;
        private ILogger _logger;
        private bool _cacheEnabled;
        private TimeSpan _cacheTime;

        public APIProcessor(string apiKey, string secretKey, IAPICacheManager apiCache)
        {
            _apiKey = apiKey;
            _secretKey = secretKey;
            if (apiCache != null)
            {
                _apiCache = apiCache;
                _cacheEnabled = true;
            }
            _logger = LogManager.GetCurrentClassLogger();
            _logger.Debug($"API Processor set up. Cache Enabled={_cacheEnabled}");
        }

        /// <summary>
        /// Set the cache expiry time
        /// </summary>
        /// <param name="time"></param>
        public void SetCacheTime(TimeSpan time)
        {
            _cacheTime = time;
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage message, string fullCacheKey) where T : class
        {
            if (message.IsSuccessStatusCode)
            {
                var messageJson = await message.Content.ReadAsStringAsync();
                var messageObject = JsonConvert.DeserializeObject<T>(messageJson);
                _logger.Debug($"Successful Message Response={messageJson}");

                if (messageObject == null)
                {
                    throw new Exception("Unable to deserialize to provided type");
                }
                if (_apiCache.Contains(fullCacheKey))
                {
                    _apiCache.Remove(fullCacheKey);
                }
                _apiCache.Add(messageObject, fullCacheKey, _cacheTime);
                return messageObject;
            }
            var errorJson = await message.Content.ReadAsStringAsync();
            var errorObject = JsonConvert.DeserializeObject<BinanceError>(errorJson);
            if (errorObject == null) throw new BinanceException("Unexpected Error whilst handling the response", null);
            _logger.Error($"Error Message Recevied", errorObject);
            throw CreateBinanceException(message.StatusCode, errorObject);
        }

        private BinanceException CreateBinanceException(HttpStatusCode statusCode, BinanceError errorObject)
        {
            if (statusCode == HttpStatusCode.GatewayTimeout)
            {
                return new BinanceTimeoutException(errorObject);
            }
            var parsedStatusCode = (int)statusCode;
            if (parsedStatusCode >= 400 && parsedStatusCode <= 500)
            {
                return new BinanceBadRequestException(errorObject);
            }
            return parsedStatusCode >= 500 ? 
                new BinanceServerException(errorObject) : 
                new BinanceException("Binance API Error", errorObject);
        }

        /// <summary>
        /// Checks the cache for an item, and if it exists returns it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partialKey">The absolute Uri of the endpoint being hit. This is used in combination with the Type name to generate a unique key</param>
        /// <param name="item"></param>
        /// <returns>Whether the item exists</returns>
        private bool CheckAndRetrieveCachedItem<T>(string fullKey, out T item) where T : class
        {
            item = null;
            var result = _apiCache.Contains(fullKey);
            item =  result ? _apiCache.Get<T>(fullKey) : null;
            return result;
        }

        /// <summary>
        /// Processes a GET request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<T> ProcessGetRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class
        {
            var fullKey = $"{typeof(T).Name}-{endpoint.Uri.AbsoluteUri}";
            if (_cacheEnabled && endpoint.UseCache)
            {
                T item;
                if (CheckAndRetrieveCachedItem<T>(fullKey, out item))
                {
                    return item;
                }
            }
            HttpResponseMessage message;
            switch (endpoint.SecurityType) { 
                case EndpointSecurityType.ApiKey:
                case EndpointSecurityType.None:
                    message = await RequestClient.GetRequest(endpoint.Uri);
                    break;
                case EndpointSecurityType.Signed:
                    message = await RequestClient.SignedGetRequest(endpoint.Uri, _apiKey, _secretKey, endpoint.Uri.Query, receiveWindow);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return await HandleResponse<T>(message, fullKey);
        }

        /// <summary>
        /// Processes a DELETE request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<T> ProcessDeleteRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class
        {
            var fullKey = $"{typeof(T).Name}-{endpoint.Uri.AbsoluteUri}";
            if (_cacheEnabled && endpoint.UseCache)
            {
                T item;
                if (CheckAndRetrieveCachedItem<T>(fullKey, out item))
                {
                    return item;
                }
            }
            HttpResponseMessage message;
            switch (endpoint.SecurityType) { 
                case EndpointSecurityType.ApiKey:
                    message = await RequestClient.DeleteRequest(endpoint.Uri);
                    break;
                case EndpointSecurityType.Signed:
                    message = await RequestClient.SignedDeleteRequest(endpoint.Uri, _apiKey, _secretKey, endpoint.Uri.Query, receiveWindow);
                    break;
                case EndpointSecurityType.None:
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return await HandleResponse<T>(message, fullKey);
        }

        /// <summary>
        /// Processes a POST request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<T> ProcessPostRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class
        {
            var fullKey = $"{typeof(T).Name}-{endpoint.Uri.AbsoluteUri}";
            if (_cacheEnabled && endpoint.UseCache)
            {
                T item;
                if (CheckAndRetrieveCachedItem<T>(fullKey, out item))
                {
                    return item;
                }
            }
            HttpResponseMessage message;
            switch (endpoint.SecurityType) { 
                case EndpointSecurityType.ApiKey:
                    message = await RequestClient.PostRequest(endpoint.Uri);
                    break;
                case EndpointSecurityType.None:
                    throw new ArgumentOutOfRangeException();
                case EndpointSecurityType.Signed:
                    message = await RequestClient.SignedPostRequest(endpoint.Uri, _apiKey, _secretKey, endpoint.Uri.Query, receiveWindow);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return await HandleResponse<T>(message, fullKey);
        }

        /// <summary>
        /// Processes a PUT request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="receiveWindow"></param>
        /// <returns></returns>
        public async Task<T> ProcessPutRequest<T>(BinanceEndpointData endpoint, int receiveWindow = 5000) where T : class
        {
            var fullKey = $"{typeof(T).Name}-{endpoint.Uri.AbsoluteUri}";
            if (_cacheEnabled && endpoint.UseCache)
            {
                T item;
                if (CheckAndRetrieveCachedItem<T>(fullKey, out item))
                {
                    return item;
                }
            }
            HttpResponseMessage message;
            switch (endpoint.SecurityType) { 
                case EndpointSecurityType.ApiKey:
                    message = await RequestClient.PutRequest(endpoint.Uri);
                    break;
                case EndpointSecurityType.None:
                case EndpointSecurityType.Signed:
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return await HandleResponse<T>(message, fullKey);
        }
    }
}