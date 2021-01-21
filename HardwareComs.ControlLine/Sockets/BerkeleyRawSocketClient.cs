using System.Net;
using System.Net.Sockets;
using ControlLine.Contract.Sockets;

namespace ControlLine.Sockets
{
    public class BerkeleyRawSocketClient : IRawSocketClient
    {
        private readonly IPEndPoint _endPoint;
        private readonly Socket _socket;

        private BerkeleyRawSocketClient(IPEndPoint endPoint, Socket socket)
        {
            _endPoint = endPoint;
            _socket = socket;
        }

        public BerkeleyRawSocketClient(IPEndPoint endPoint) :
            this(endPoint, new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP))
        {
        }

        public void Connect()
        {
            _socket.Connect(_endPoint);
        }

        public void Send(byte[] payload)
        {
            _socket.Send(payload);
        }

        public byte[] Recieve()
        {
            byte[] response = { };
            _socket.Receive(response);
            return response;
        }

        public void Close()
        {
            _socket.Close();
        }
    }
}