using System;
using CodeContracts;

namespace Common.DateTime
{
    public class TimeService : ITimeService
    {
        public double MilisecondsNow()
        {
            return System.DateTime.Now.TimeOfDay.TotalMilliseconds;
        }

        public double MilisecondsInHour(int hour)
        {
            CodeContract.PreCondition<ArgumentException>(hour >= 0);
            CodeContract.PreCondition<ArgumentException>(hour <= 24);

            return new TimeSpan(hour, 0, 0).TotalMilliseconds;
        }
    }
}