using System;
using Crosscutting.Services.Contract.Crosscutting.Interface.Utilities;

namespace Crosscutting.DateTime
{
    public class TimeService : ITimeService
    {
        public double MilisecondsNow()
        {
            return System.DateTime.Now.TimeOfDay.TotalMilliseconds;
        }

        public double MilisecondsInTimeSpan(TimeSpan hour)
        {
            return hour.TotalMilliseconds;
        }
    }
}