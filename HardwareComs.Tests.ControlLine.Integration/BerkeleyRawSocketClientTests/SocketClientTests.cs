using System.Net;
using ControlLine.Contract.Threading;
using ControlLine.Sockets;
using ControlLine.Threading;

namespace ControlLineIntegrationTests.BerkeleyRawSocketClientTests
{
    public class SocketClientTests
    {
        protected FakeBerkeleyTcpServer Server;
        protected BerkeleyTcpClient Client;
        protected byte[] ServerPayload;
        private IThreadOperations _threadOperations;
        protected int Port;

        protected void Init()
        {
            _threadOperations = new ThreadOperations();
            Server = new FakeBerkeleyTcpServer(
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port),
                ServerPayload,
                _threadOperations
            );
            InitClient();
        }

        protected void InitClient()
        {
            Client = new BerkeleyTcpClient(
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port),
                48,
                _threadOperations,
                5000
            );
        }
    }
}