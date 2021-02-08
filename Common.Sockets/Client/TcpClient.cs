using System;
using System.Net;
using System.Net.Sockets;
using Threading;

namespace Sockets.Client
{
    public class TcpClient : ISocketClient
    {
        private readonly IPEndPoint _endPoint;
        private readonly int _packetLength;
        private readonly Socket _socket;
        private readonly IThreadOperations _threadOperations;
        private readonly int _timeout;

        private TcpClient(IPEndPoint endPoint, Socket socket, int packetLength,
            IThreadOperations threadOperations, int timeout)
        {
            _endPoint = endPoint;
            _socket = socket;
            _packetLength = packetLength;
            _threadOperations = threadOperations;
            _timeout = timeout;
        }

        public TcpClient(IPEndPoint endPoint, int packetLength, IThreadOperations threadOperations,
            int timeout) :
            this(endPoint, new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), packetLength,
                threadOperations, timeout)
        {
        }

        /// <exception cref="SocketException"></exception>
        public void Connect()
        {
            HandleSocketAction(() => _socket.Connect(_endPoint));
        }

        /// <exception cref="SocketException"></exception>
        public void Send(byte[] payload)
        {
            HandleSocketAction(() => _socket.Send(payload));
        }

        /// <exception cref="SocketException"></exception>
        public byte[] Recieve()
        {
            var response = new byte[_packetLength];
            HandleSocketAction(() => _socket.Receive(response));
            return response;
        }

        /// <exception cref="SocketException"></exception>
        public void Close()
        {
            _socket.Close();
        }

        private void HandleSocketAction(Action action)
        {
            try
            {
                _threadOperations.WaitUntilActionTimeout(action, _timeout);
            }
            catch (System.Exception)
            {
                //TODO: log
                throw new SocketException();
            }
        }
    }
}