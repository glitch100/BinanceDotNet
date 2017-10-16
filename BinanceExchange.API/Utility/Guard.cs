using System;

namespace BinanceExchange.API.Utility
{
    public class Guard
    {
        public static void AgainstNullOrEmpty(string param, string name = null)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentNullException(name ?? "The Guarded argument was null or empty.");
            }
        }

        public static void AgainstNull(object param, string name = null)
        {
            if (param == null)
            {
                throw new ArgumentNullException(name ?? "The Guarded argument was null.");
            }
        }

        public static void AgainstDateTimeMin(DateTime param, string name = null)
        {
            if (param == DateTime.MinValue)
            {
                throw new ArgumentNullException(name ?? "The Guarded argument was DateTime min.");
            }
        }
    }
}
