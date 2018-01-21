using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using BinanceExchange.API.Models.Response;

namespace BinanceExchange.API.Converter
{
    class ExchangeInfoSymbolFilterConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType) => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var arr = JArray.Load(reader);
            List<ExchangeInfoSymbolFilter> items = new List<ExchangeInfoSymbolFilter>();
            foreach (var obj in arr)
            {
                string discriminator = (string)obj["filterType"];

                ExchangeInfoSymbolFilter item;
                switch (discriminator)
                {
                    case "PRICE_FILTER":
                        item = new ExchangeInfoSymbolFilterPrice();
                        break;
                    case "LOT_SIZE":
                        item = new ExchangeInfoSymbolFilterLotSize();
                        break;
                    case "MIN_NOTIONAL":
                        item = new ExchangeInfoSymbolFilterMinNotional();
                        break;
                    default:
                        throw new NotImplementedException();
                }

                serializer.Populate(obj.CreateReader(), item);
                items.Add(item);
            }

            return items;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}