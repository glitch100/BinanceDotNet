using System;
using System.Collections.Generic;
using System.Linq;
using BinanceExchange.API.Models.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BinanceExchange.API.Conveter
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
                Open = klineCandlesticks.ElementAt(1).ToString(),
                High = klineCandlesticks.ElementAt(2).ToString(),
                Low = klineCandlesticks.ElementAt(3).ToString(),
                Close = klineCandlesticks.ElementAt(4).ToString(),
                Volume = klineCandlesticks.ElementAt(5).ToString(),
                CloseTime = Epoch.AddMilliseconds((long) klineCandlesticks.ElementAt(6)),
                QuoteAssetVolume = klineCandlesticks.ElementAt(7).ToString(),
                NumberOfTrades = (int) klineCandlesticks.ElementAt(8),
                TakerBuyBaseAssetVolume = klineCandlesticks.ElementAt(9).ToString(),
                TakerBuyQuoteAssetVolume = klineCandlesticks.ElementAt(10).ToString(),
            };
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }

    public class TraderPriceConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var tradePrices = JArray.Load(reader);
            var list = new List<TradeResponse>();
            foreach (var tradePrice in tradePrices)
            {
                var price = tradePrice.ElementAt(0).ToObject<decimal>();
                var quantity = tradePrice.ElementAt(1).ToObject<decimal>();
                list.Add(new TradeResponse
                {
                    Price = price,
                    Quantity = quantity,
                });
            }
            return list;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}