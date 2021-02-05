using System.Collections.Generic;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
using ControlLine.ControlLineStatusValidator;
using ControlLine.Sockets;
using ControlLine.Threading;
using ControlSystem.Tests.Enviroment.ControlSystem.Configuration;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation
{
    public class SendOperationTests
    {
        private IControlLineStatusValidator _controlLineStatusValidator;
        private FakeControlLineServer _fakeControlLineServer;
        private ISocketClient _socketClient;
        private IThreadOperations _threadOperations;
        protected IControlLine Sut;

        protected void Init()
        {
            _threadOperations = new ThreadOperations();
            _socketClient = new BerkeleyTcpClient(
                ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
                ControlLineSockets.MaxPayloadLength,
                _threadOperations,
                250
            );
            _fakeControlLineServer = new FakeControlLineServer(
                ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
                new Dictionary<byte[], byte[]>
                {
                    {
                        //mock request: move door absolute, 120mm 
                        //mock response: device timeout
                        new byte[] {2, 2, 1, 120, 0, 0, 0, 0},
                        new byte[] {4, 0, 0, 0, 0, 0, 0, 0}
                    }
                },
                _threadOperations
            );
            _fakeControlLineServer.Run();

            _controlLineStatusValidator = new ControlLineStatusValidator();
            Sut = new ControlLineSockets(
                _socketClient,
                _controlLineStatusValidator,
                _threadOperations
            );
        }
    }
}