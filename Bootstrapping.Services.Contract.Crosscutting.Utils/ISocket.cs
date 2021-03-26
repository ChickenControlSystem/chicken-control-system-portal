namespace Bootstrapping.Services.Contract.Crosscutting.Utils
{
    public interface ISocket : ICloseableSocket
    {
        /// <summary>
        ///     sends a UTF-8 data in one go
        /// </summary>
        void Send(byte[] payload);

        /// <summary>
        ///     blocking call to receive from socket
        /// </summary>
        byte[] Recieve();
    }
}