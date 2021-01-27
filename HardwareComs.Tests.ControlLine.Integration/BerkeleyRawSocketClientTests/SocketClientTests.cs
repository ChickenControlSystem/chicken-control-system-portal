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

        protected void Init()
        {
            _threadOperations = new ThreadOperations();
            Server = new FakeBerkeleyTcpServer(
                ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
                ServerPayload,
                _threadOperations
            );
            InitClient();
        }

        protected void InitClient()
        {
            Client = new BerkeleyTcpClient(
                ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
                8,
                _threadOperations,
                100
            );
        }
    }
}