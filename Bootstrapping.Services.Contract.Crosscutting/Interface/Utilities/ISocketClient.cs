namespace Bootstrapping.Services.Contract.Crosscutting.Interface.Utilities
{
    /// <summary>
    ///     a module that receives and sends to a raw ip socket server
    /// </summary>
    public interface ISocketClient : ISocket
    {
        /// <summary>
        ///     connects to an ip and port
        /// </summary>
        void Connect();
    }
}