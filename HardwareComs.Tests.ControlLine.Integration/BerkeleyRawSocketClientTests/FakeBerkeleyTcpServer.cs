using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ControlLine.Contract.Threading;

namespace ControlLineIntegrationTests.BerkeleyRawSocketClientTests
{
    /// <summary>
    /// protocol: takes in some byte[] and concatenates a payload to it
    /// </summary>
    public class FakeBerkeleyTcpServer
    {
        private readonly Socket _socket;
        private readonly byte[] _payload;
        private readonly IThreadOperations _threadOperations;

        private FakeBerkeleyTcpServer(Socket socket, byte[] payload, IThreadOperations threadOperations)
        {
            _socket = socket;
            _payload = payload;
            _threadOperations = threadOperations;
        }

        public FakeBerkeleyTcpServer(EndPoint endPoint, byte[] payload, IThreadOperations threadOperations)
            : this(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), payload,
                threadOperations)
        {
            _socket.Bind(endPoint);
        }

        /// <summary>
        /// runs test server on new thread, when server receives then responds in a timely manner
        /// </summary>
        public void RunRecieveRespond()
        {
            _threadOperations.RunBackground(RecieveRespond);
        }

        /// <summary>
        /// runs test server on new thread, when server closes after receiving
        /// </summary>
        public void RunRecieveClose()
        {
            _threadOperations.RunBackground(RecieveClose);
        }

        /// <summary>
        /// runs test server on new thread, when server responds slowly
        /// </summary>
        public void RunBadConnection()
        {
            _threadOperations.RunBackground(BadConnection);
        }

        private void RecieveRespond()
        {
            _socket.Listen(10);
            var client = _socket.Accept();
            Thread.Sleep(50);
            var buffer = new byte[_payload.Length + 1];
            client.Receive(buffer);
            Thread.Sleep(50);
            client.Send(buffer.Concat(_payload).ToArray());
            client.Close();
            _socket.Close();
        }

        private void RecieveClose()
        {
            _socket.Listen(10);
            var client = _socket.Accept();
            Thread.Sleep(50);
            var buffer = new byte[_payload.Length + 1];
            client.Receive(buffer);
            client.Close();
            _socket.Close();
        }

        private void BadConnection()
        {
            _socket.Listen(10);
            var client = _socket.Accept();
            Thread.Sleep(5500);
            var buffer = new byte[_payload.Length + 1];
            client.Receive(buffer);
            client.Close();
            _socket.Close();
        }
    }
}