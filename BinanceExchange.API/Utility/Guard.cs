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
        public static void AgainstInvalidDepthLevel(string depth)
        {
            AgainstNullOrEmpty(depth, nameof(depth));
            if (depth != "5" && depth != "10" && depth != "20")
            {
                throw new ArgumentException("Valid levels are 5,10 or 20.");
            }
        }
    }
}
