using System;

namespace BLL.Common.Contract
{
    public interface IDelay : IRunnable
    {
        public double Period { get; set; }

        public void WaitUntil(double milliseconds);

        public void WaitUntil(TimeSpan time);
    }
}