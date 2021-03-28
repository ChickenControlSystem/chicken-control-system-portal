using System.Timers;
using Bootstrapping.Services.Contract.Crosscutting.Utils;

namespace Crosscutting.DateTime
{
    public class Ticker : ITicker
    {
        private readonly Timer _timer;
        public event ElapsedEventHandler TickEvent;

        public Ticker(Timer timer)
        {
            _timer = timer;
            _timer.Elapsed += TickEvent;
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}