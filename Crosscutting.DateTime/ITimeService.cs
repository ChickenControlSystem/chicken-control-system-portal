using System;

namespace Crosscutting.DateTime
{
    public interface ITimeService
    {
        /// <exception cref="ArgumentException"></exception>
        /// <summary>
        /// gets seconds for current time after midnight
        /// </summary>
        public double MilisecondsNow();

        /// <exception cref="ArgumentException"></exception>
        /// <summary>
        /// gets seconds after midnight for given hour (0-24)
        /// </summary>
        public double MilisecondsInTimeSpan(TimeSpan hour);
    }
}