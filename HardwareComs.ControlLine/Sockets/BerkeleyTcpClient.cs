using System;
using System.Net;
using System.Net.Sockets;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;

namespace ControlLine.Sockets
{
    public class BerkeleyTcpClient : ISocketClient
    {
        private readonly IPEndPoint _endPoint;
        private readonly Socket _socket;
        private readonly int _packetLength;
        private readonly IThreadOperations _threadOperations;
        private readonly int _timeout;

        private BerkeleyTcpClient(IPEndPoint endPoint, Socket socket, int packetLength,
            IThreadOperations threadOperations, int timeout)
        {
            _endPoint = endPoint;
            _socket = socket;
            _packetLength = packetLength;
            _threadOperations = threadOperations;
            _timeout = timeout;
        }

        public BerkeleyTcpClient(IPEndPoint endPoint, int packetLength, IThreadOperations threadOperations,
            int timeout) :
            this(endPoint, new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), packetLength,
                threadOperations, timeout)
        {
        }

        public void Connect()
        {
            HandleSocketAction(() => _socket.Connect(_endPoint));
        }

        public void Send(byte[] payload)
        {
            HandleSocketAction(() => _socket.Send(payload));
        }

        public byte[] Recieve()
        {
            var response = new byte[_packetLength];
            HandleSocketAction(() => _socket.Receive(response));
            return response;
        }

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
            catch (System.Exception e)
            {
                throw new SocketException();
            }
        }

        private void HandleSocketFunc<T>(Func<T> func)
        {
            try
            {
                _threadOperations.WaitUntilFuncTimeout(func, _timeout);
            }
            catch (System.Exception e)
            {
                throw new SocketException();
            }
        }
    }
}