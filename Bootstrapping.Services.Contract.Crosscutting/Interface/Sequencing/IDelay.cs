using System;

namespace Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing
{
    public interface IDelay : IRunnable
    {
        /// <summary>
        /// waits for delay specified in parameter
        /// </summary>
        public void WaitUntil(double milliseconds);

        /// <summary>
        /// waits until given time
        /// </summary>
        public void WaitUntil(TimeSpan time);
    }
}