using System.Net;

namespace ControlLine.Contract.Sockets
{
    /// <summary>
    /// a module that receives and sends to a raw ip socket server
    /// </summary>
    public interface IRawSocketClient
    {
        /// <summary>
        /// connects to an ip and port
        /// </summary>
        void Connect(EndPoint endPoint);

        /// <summary>
        /// sends a UTF-8 data in one go
        /// </summary>
        void Send(string payload);

        /// <summary>
        /// blocking call to receive from socket
        /// </summary>
        string Recieve();

        /// <summary>
        /// closes client socket
        /// </summary>
        void Close();
    }
}