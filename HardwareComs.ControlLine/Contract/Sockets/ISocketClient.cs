namespace ControlLine.Contract.Sockets
{
    /// <summary>
    /// a module that receives and sends to a raw ip socket server
    /// </summary>
    public interface ISocketClient
    {
        /// <summary>
        /// connects to an ip and port
        /// </summary>
        void Connect();

        /// <summary>
        /// sends a UTF-8 data in one go
        /// </summary>
        void Send(byte[] payload);

        /// <summary>
        /// blocking call to receive from socket
        /// </summary>
        byte[] Recieve();

        /// <summary>
        /// closes client socket
        /// </summary>
        void Close();
    }
}