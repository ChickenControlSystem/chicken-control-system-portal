using System.Timers;
using Bootstrapping.Services.Contract.Crosscutting.Utils;

namespace Crosscutting.DateTime
{
    public class TickerFactory : ITickerFactory
    {
        public ITicker Create(double interval) => new Ticker(new Timer {Interval = interval});

        public ITicker CreateMinute() => Create(60000);
    }
}