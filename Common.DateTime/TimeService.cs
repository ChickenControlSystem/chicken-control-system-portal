using System;

namespace Common.DateTime
{
    public class TimeService : ITimeService
    {
        public double MilisecondsNow()
        {
            return System.DateTime.Now.TimeOfDay.TotalMilliseconds;
        }

        public double MilisecondsInHour(TimeSpan hour)
        {
            return hour.TotalMilliseconds;
        }
    }
}