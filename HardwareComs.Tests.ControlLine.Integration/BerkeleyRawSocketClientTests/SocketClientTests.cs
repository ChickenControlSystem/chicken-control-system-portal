using System.Threading;
using ControlLine.Contract.Threading;
using ControlLine.Sockets;
using ControlLine.Threading;
using ControlSystem.Tests.Enviroment;

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
                ConfLoader.GetControlLineSettings().GetEndPoint(),
                ServerPayload,
                _threadOperations
            );
            InitClient();
        }

        protected void InitClient()
        {
            Client = new BerkeleyTcpClient(
                ConfLoader.GetControlLineSettings().GetEndPoint(),
                48,
                _threadOperations,
                2500
            );
        }

        protected void CoolDown()
        {
            Thread.Sleep(5000);
        }
    }
}