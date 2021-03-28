namespace Bootstrapping.Services.Contract.Crosscutting.Utils
{
    public interface ITickerFactory
    {
        public ITicker Create(double interval);

        /// <summary>
        ///     creates a timer that fires event every minute
        /// </summary>
        public ITicker CreateMinute();
    }
}