namespace Sockets
{
    public interface ICloseableSocket
    {
        /// <summary>
        ///     closes client socket
        /// </summary>
        void Close();
    }
}