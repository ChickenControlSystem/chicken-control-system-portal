namespace Bootstrapping.Services.Contract.Crosscutting.Utils
{
    /// <summary>
    ///     singleton class
    /// 
    ///     fires event at constant interval, which can be subscribed to
    ///     can be used for polling, by having everything synchronised by the ticker
    /// </summary>
    public interface ITicker
    {
        void Start();
    }
}