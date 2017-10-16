using System;
using System.Linq;
using BinanceExchange.API.Models.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BinanceExchange.API.Converter
{
    public class KlineCandleSticksConverter : JsonConverter
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var klineCandlesticks = JArray.Load(reader);
            return new KlineCandleStickResponse
            {
                OpenTime = Epoch.AddMilliseconds((long) klineCandlesticks.ElementAt(0)),
                Open = (decimal) klineCandlesticks.ElementAt(1),
                High = (decimal) klineCandlesticks.ElementAt(2),
                Low = (decimal) klineCandlesticks.ElementAt(3),
                Close = (decimal) klineCandlesticks.ElementAt(4),
                Volume = (decimal) klineCandlesticks.ElementAt(5),
                CloseTime = Epoch.AddMilliseconds((long) klineCandlesticks.ElementAt(6)),
                QuoteAssetVolume = (decimal) klineCandlesticks.ElementAt(7),
                NumberOfTrades = (int) klineCandlesticks.ElementAt(8),
                TakerBuyBaseAssetVolume = (decimal) klineCandlesticks.ElementAt(9),
                TakerBuyQuoteAssetVolume = (decimal) klineCandlesticks.ElementAt(10),
            };
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}