using System;

namespace BinanceExchange.API.Websockets
{
    public class WebSocketConnectionFunc
    {
        public Func<bool> ExitFunction;
        public int Timeout { get; }

        public bool IsTimout => Timeout > 0;

        public WebSocketConnectionFunc(int timeout = 5000)
        {
            Timeout = timeout;
        }

        public WebSocketConnectionFunc(Func<bool> exitFunction)
        {
            ExitFunction = exitFunction;
        }
    }
}