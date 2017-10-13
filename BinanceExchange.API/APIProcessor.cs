using System;
using System.Net.Http;
using System.Threading.Tasks;
using BinanceExchange.API.Caching;
using BinanceExchange.API.Enums;
using BinanceExchange.API.Models.Response.Error;
using Newtonsoft.Json;

namespace BinanceExchange.API
{
    public class APIProcessor
    {
        private readonly string _apiKey;
        private readonly string _secretKey;
        private IAPICache _apiCache;
        private bool _cacheEnabled;

        public APIProcessor(string apiKey, string secretKey, IAPICache apiCache)
        {
            _apiKey = apiKey;
            _secretKey = secretKey;
            if (apiCache != null)
            {
                _apiCache = apiCache;
                _cacheEnabled = true;
            }
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage message) where T : class
        {
            if (message.IsSuccessStatusCode)
            {
                var messageJson = await message.Content.ReadAsStringAsync();
                var messageObject = JsonConvert.DeserializeObject<T>(messageJson);
                if (messageObject == null)
                {
                    throw new Exception("Unable to deserialize to provided type");
                }
                return messageObject;
            }
            var errorJson = await message.Content.ReadAsStringAsync();
            var errorObject = JsonConvert.DeserializeObject<BinanceError>(errorJson);
            if (errorObject != null)
            {
                throw new BinanceException("API Error", errorObject);
            }
            throw new Exception("Error whilst handling the response");

        }

        /// <summary>
        /// Checks the cache for an item, and if it exists returns it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="partialKey">The absolute Uri of the endpoint being hit. This is used in combination with the Type name to generate a unique key</param>
        /// <returns>Whether the item exists</returns>
        private bool CheckAndRetrieveCachedItem<T>(string partialKey, out T item) where T : class
        {
            var key = $"{nameof(T)}-{partialKey}";
            item = null;
            var result = _apiCache.Contains(key);
            item =  result ? _apiCache.Get<T>(key) : null;
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
            if (_cacheEnabled && endpoint.UseCache)
            {
                T item;
                if (CheckAndRetrieveCachedItem<T>(endpoint.Uri.AbsoluteUri, out item))
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
            return await HandleResponse<T>(message);
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
            HttpResponseMessage message;
            switch (endpoint.SecurityType) { 
                case EndpointSecurityType.ApiKey:
                case EndpointSecurityType.None:
                    throw new ArgumentOutOfRangeException();
                case EndpointSecurityType.Signed:
                    message = await RequestClient.SignedDeleteRequest(endpoint.Uri, _apiKey, _secretKey, endpoint.Uri.Query, receiveWindow);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return await HandleResponse<T>(message);
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
            HttpResponseMessage message;
            switch (endpoint.SecurityType) { 
                case EndpointSecurityType.ApiKey:
                case EndpointSecurityType.None:
                    throw new ArgumentOutOfRangeException();
                case EndpointSecurityType.Signed:
                    message = await RequestClient.SignedPostRequest(endpoint.Uri, _apiKey, _secretKey, endpoint.Uri.Query, receiveWindow);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return await HandleResponse<T>(message);
        }
    }
}