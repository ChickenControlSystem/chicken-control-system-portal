using System.Net;

namespace ControlLine.Contract.Sockets
{
    public interface IRawSocketClient
    {
        void Connect(EndPoint endPoint);

        void Send(string payload);

        string Recieve();
    }
}